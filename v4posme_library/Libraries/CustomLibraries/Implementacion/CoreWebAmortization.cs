using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion;

public class CoreWebAmortization : ICoreWebAmortization
{
    private static readonly string? AppTimezone = VariablesGlobales.ConfigurationBuilder["APP_TIMEZONE"];

    private readonly ICoreWebParameter _coreWebParameter;

    private readonly ICustomerCreditDocumentModel _customerCreditDocumentModel;

    private readonly ICustomerCreditAmortizationModel _customerCreditAmortizationModel;

    private readonly ICustomerCreditLineModel _customerCreditLineModel;

    private readonly ICatalogItemModel _catalogItemModel;

    private readonly ICoreWebCatalog _coreWebCatalog;

    private readonly ICoreWebFinancialAmort _financialAmort;

    public CoreWebAmortization(ICoreWebParameter coreWebParameter, ICustomerCreditDocumentModel customerCreditDocumentModel, ICustomerCreditAmortizationModel customerCreditAmortizationModel, ICustomerCreditLineModel customerCreditLineModel, ICatalogItemModel catalogItemModel, ICoreWebCatalog coreWebCatalog, ICoreWebFinancialAmort financialAmort)
    {
        _coreWebParameter = coreWebParameter;
        _customerCreditDocumentModel = customerCreditDocumentModel;
        _customerCreditAmortizationModel = customerCreditAmortizationModel;
        _customerCreditLineModel = customerCreditLineModel;
        _catalogItemModel = catalogItemModel;
        _coreWebCatalog = coreWebCatalog;
        _financialAmort = financialAmort;
    }

    public void CancelDocument(int companyId, int customerCreditDocumentId, decimal amount)
    {
        // Set the default time zone
        var shareDocumentCancel = _coreWebParameter.GetParameter("SHARE_DOCUMENT_CANCEL", companyId);
        if (shareDocumentCancel is null)
        {
            throw new Exception($"NO EXISTE EL PARAMETRO SHARE_DOCUMENT_CANCEL EN EL COMPANYId {companyId}");
        }

        var documentCancel = shareDocumentCancel.Value;
        var shareCancel = _coreWebParameter.GetParameter("SHARE_CANCEL", companyId);
        if (shareCancel is null)
        {
            throw new Exception($"NO EXISTE EL PARAMETRO SHARE_CANCEL EN EL COMPANYId {companyId}");
        }

        var amortizationCancel          = shareCancel.Value;
        var objCustomerCreditDocument   = _customerCreditDocumentModel.GetRowByPk(customerCreditDocumentId);

        // Cancel Document
        if (amount >= objCustomerCreditDocument.Balance)
        {
            //mapeamos los valores para q no queden en null lo sque no se usaran
            var objCustomerCreditDocumentNew        = objCustomerCreditDocument.Mapper(objCustomerCreditDocument);
            objCustomerCreditDocumentNew.Balance    = 0;
            objCustomerCreditDocumentNew.StatusID   = Convert.ToInt32(documentCancel);
            _customerCreditDocumentModel.UpdateAppPosme(objCustomerCreditDocument.CustomerCreditDocumentId!.Value,objCustomerCreditDocumentNew);
        }
        else
        {
            throw new Exception("EL IMPORTE NO ES SUFICIENTE PARA CANCELAR EL DOCUMENTO");
        }

        // Cancel Amortization
        var objListCustomerCreditDocumentAmortization = _customerCreditAmortizationModel.GetRowByDocumentAndVinculable(customerCreditDocumentId);
        if (objListCustomerCreditDocumentAmortization is null) return;


        foreach (var itemAmortization in objListCustomerCreditDocumentAmortization)
        {
            var itemAmortizationNew = itemAmortization;
            itemAmortizationNew.Remaining = 0;
            _customerCreditAmortizationModel.UpdateAppPosme(itemAmortization.CreditAmortizationID, itemAmortizationNew);
        }

    }

    public void ShareCapital(int companyId, int customerCreditDocumentId, decimal amount)
    {
        

        var objCustomerCreditDocument                   = _customerCreditDocumentModel.GetRowByPk(customerCreditDocumentId);
        var objListCustomerCreditDocumentAmortization   =_customerCreditAmortizationModel.GetRowByDocumentAndVinculable(customerCreditDocumentId);
        var objCustomerCreditLine                       = _customerCreditLineModel.GetRowByPk(objCustomerCreditDocument.CustomerCreditLineId);
        var periodPay                                   = _catalogItemModel.GetRowByCatalogItemId(objCustomerCreditLine.PeriodPay);
        decimal? totalCapital                           = 0;
            
        var numCuotas       = objListCustomerCreditDocumentAmortization.Count;
        totalCapital        = objCustomerCreditDocument!.Balance - amount;
        var dateApplyFirst  = objListCustomerCreditDocumentAmortization.First().DateApply;

        var objCatalogItemDiasNoCobrables =
            _coreWebCatalog.GetCatalogAllItemByNameCatalogo("CXC_NO_COBRABLES", companyId);
        var objCatalogItemDiasFeriados365 =
            _coreWebCatalog.GetCatalogAllItemByNameCatalogo("CXC_NO_COBRABLES_FERIADOS_365", companyId);
        var objCatalogItemDiasFeriados366 =
            _coreWebCatalog.GetCatalogAllItemByNameCatalogo("CXC_NO_COBRABLES_FERIADOS_366", companyId);
           

        _financialAmort.Amort(
            totalCapital,
            objCustomerCreditDocument.Interes,
            numCuotas,
            periodPay.Sequence!.Value,
            objCustomerCreditDocument.DateOn.Date,
            objCustomerCreditLine.TypeAmortization,
            objCatalogItemDiasNoCobrables,
            objCatalogItemDiasFeriados365,
            objCatalogItemDiasFeriados366
        );

        var tableAmortization = _financialAmort.GetTable();
        var aux = 0;
        if (objListCustomerCreditDocumentAmortization is not null)
        {
            foreach (var itemAmortization in objListCustomerCreditDocumentAmortization)
            {
                var itemAmortizationNew = new TbCustomerCreditAmoritization();

                if (dateApplyFirst == itemAmortization.DateApply)
                {
                    itemAmortizationNew.ShareCapital = amount;
                }

                var detailDto = tableAmortization.ListDetailDto?[aux + 1];
                if (detailDto is null)
                {
                    continue;
                }

                itemAmortizationNew.BalanceStart = detailDto.SaldoInicial!.Value;
                itemAmortizationNew.BalanceEnd = detailDto.Saldo!.Value;
                itemAmortizationNew.Capital = detailDto.Principal!.Value;
                itemAmortizationNew.Interest = detailDto.Interes!.Value;
                itemAmortizationNew.Share = itemAmortizationNew.Interest + itemAmortizationNew.Capital;
                itemAmortizationNew.Remaining = itemAmortizationNew.Share;

                _customerCreditAmortizationModel.UpdateAppPosme(itemAmortization.CreditAmortizationID, itemAmortizationNew);
                aux++;
            }
        }
        
        

        var objCustomerCreditDocumentNew        = objCustomerCreditDocument.Mapper(objCustomerCreditDocument);
        objCustomerCreditDocumentNew.Balance    = totalCapital!.Value;
        _customerCreditDocumentModel.UpdateAppPosme(objCustomerCreditDocument.CustomerCreditDocumentId!.Value,objCustomerCreditDocumentNew);
    }

    public void ChangeStatus(int companyId, int customerCreditDocumentId)
    {
        var companyParameter = _coreWebParameter.GetParameter("CREDIT_DOCUMENT_PROVISIONED", companyId);
        if (companyParameter is null)
        {
            throw new Exception("NO existe la compañia con ese parametro");
        }

        var documentProvisioned = companyParameter.Value;
        var objCustomerCreditDocument = _customerCreditDocumentModel.GetRowByPk(customerCreditDocumentId);
        if (objCustomerCreditDocument is null)
        {
            throw new Exception("NO existe la customerCreditDocument con el id indicacod");
        }

        if (objCustomerCreditDocument.BalanceProvicioned >= objCustomerCreditDocument.Balance)
        {
            var objCustomerCreditDocumentNew = objCustomerCreditDocument.Mapper(objCustomerCreditDocument);
            objCustomerCreditDocumentNew.StatusID = Convert.ToInt32(documentProvisioned);
            _customerCreditDocumentModel.UpdateAppPosme(objCustomerCreditDocument.CustomerCreditDocumentId!.Value,objCustomerCreditDocumentNew);
        }
    }

    public void ApplyCuote(int companyId, int customerCreditDocumentId, decimal amount, int amoritizationId)
    {
        var shareDocumentCancel = _coreWebParameter.GetParameter("SHARE_DOCUMENT_CANCEL", companyId);
        var shareCancel = _coreWebParameter.GetParameter("SHARE_CANCEL", companyId);
        if (shareDocumentCancel is null)
        {
            throw new Exception("NO existe la compañia con el parametro SHARE_DOCUMENT_CANCEL");
        }

        if (shareCancel is null)
        {
            throw new Exception("NO existe la compañia con el parametro SHARE_CANCEL");
        }

        var documentCancel = shareDocumentCancel.Value;
        var amortizationCancel = shareCancel.Value;
        var objCustomerCreditDocument = _customerCreditDocumentModel.GetRowByPk(customerCreditDocumentId);
        var objListCustomerCreditDocumentAmortization = _customerCreditAmortizationModel.GetRowByDocumentAndVinculable(customerCreditDocumentId);
        var capital = decimal.Zero;
        var interes = decimal.Zero;

        if (objListCustomerCreditDocumentAmortization is not  null)
        {
            foreach (var itemAmortization in objListCustomerCreditDocumentAmortization)
            {
                var now = DateOnly.FromDateTime(DateTime.Now);
                var interval            = now.DayNumber-DateOnly.FromDateTime(itemAmortization.DateApply).DayNumber;
                var itemAmortizationNew = itemAmortization;
                if (amount >= itemAmortization.Remaining && amount != decimal.Zero)                {
                    amount = amount - itemAmortization.Remaining;				
				    var dif = itemAmortization.Remaining - amount;
                    itemAmortizationNew.StatusID = Convert.ToInt32(amortizationCancel);
                    itemAmortizationNew.Remaining = decimal.Zero;
                    itemAmortizationNew.DayDelay = interval;     
                    
                    if (itemAmortization.Remaining == itemAmortization.Share)
                    {
                        capital = capital + itemAmortization.Capital;
                        interes = interes + itemAmortization.Interest;
                    }
                    else if (itemAmortization.Remaining > itemAmortization.Interest)
                    {
                        capital = capital + (itemAmortization.Remaining - itemAmortization.Interest);
                        interes = interes + itemAmortization.Interest;
                    }
                    else if (itemAmortization.Remaining < itemAmortization.Interest)
                    {
                        capital = capital + 0;
                        interes = interes + itemAmortization.Remaining;
                    }
                    else if (itemAmortization.Remaining == itemAmortization.Interest)
                    {
                        capital = capital + 0;
                        interes = interes + itemAmortization.Interest;
                    }

                    _customerCreditAmortizationModel.UpdateAppPosme(itemAmortization.CreditAmortizationID,itemAmortizationNew);
                }
                else if (amount != decimal.Zero)
                {
                    var dif = itemAmortization.Remaining - amount;
                    itemAmortizationNew.Remaining = dif;
                    itemAmortizationNew.DayDelay = interval;
                    if (dif > itemAmortization.Interest)
                    {
                        capital = capital + amount;
                        interes = interes + 0;
                    }
                    else if (dif == itemAmortization.Interest)
                    {
                        capital = capital + amount;
                        interes = interes + 0;
                    }
                    else if (dif < itemAmortization.Interest && itemAmortization.Remaining <= itemAmortization.Interest)
                    {
                        capital = capital + 0;
                        interes = interes + amount;
                    }
                    else if (dif < itemAmortization.Interest && itemAmortization.Remaining > itemAmortization.Interest)
                    {
                        var capital001 = itemAmortization.Remaining - itemAmortization.Interest;
                        var interes001 = amount - capital001;
                        capital = capital + capital001;
                        interes = interes + interes001;
                    }

                    _customerCreditAmortizationModel.UpdateAppPosme(itemAmortization.CreditAmortizationID,itemAmortizationNew);
                    amount = 0;
                }
            }
        }

        

        //Actualizar Balance del Documento
        var objCustomerCreditDocumentNew        = objCustomerCreditDocument!.Mapper(objCustomerCreditDocument);
        objCustomerCreditDocumentNew.Balance    = objCustomerCreditDocument.Balance!.Value - capital;
        _customerCreditDocumentModel.UpdateAppPosme(objCustomerCreditDocument.CustomerCreditDocumentId!.Value,objCustomerCreditDocumentNew);

        //Cancel Document
        objListCustomerCreditDocumentAmortization = _customerCreditAmortizationModel.GetRowByDocumentAndVinculable(customerCreditDocumentId);
        if (objListCustomerCreditDocumentAmortization is not null)
        {
            objCustomerCreditDocumentNew.StatusID = Convert.ToInt32(documentCancel);
            _customerCreditDocumentModel.UpdateAppPosme(objCustomerCreditDocument.CustomerCreditDocumentId!.Value, objCustomerCreditDocumentNew);
        }

    }
}