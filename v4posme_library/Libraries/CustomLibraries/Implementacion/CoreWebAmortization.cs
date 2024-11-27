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
    private readonly ICoreWebTools _coreWebTools;
    private readonly ITransactionMasterDetailReferencesModel _transactionMasterDetailReferencesModel;

    public CoreWebAmortization(ICoreWebParameter coreWebParameter, ICustomerCreditDocumentModel customerCreditDocumentModel, ICustomerCreditAmortizationModel customerCreditAmortizationModel, ICustomerCreditLineModel customerCreditLineModel, ICatalogItemModel catalogItemModel, ICoreWebCatalog coreWebCatalog, ICoreWebFinancialAmort financialAmort, ICoreWebTools coreWebTools, ITransactionMasterDetailReferencesModel transactionMasterDetailReferencesModel)
    {
        _coreWebParameter = coreWebParameter;
        _customerCreditDocumentModel = customerCreditDocumentModel;
        _customerCreditAmortizationModel = customerCreditAmortizationModel;
        _customerCreditLineModel = customerCreditLineModel;
        _catalogItemModel = catalogItemModel;
        _coreWebCatalog = coreWebCatalog;
        _financialAmort = financialAmort;
        _coreWebTools = coreWebTools;
        _transactionMasterDetailReferencesModel = transactionMasterDetailReferencesModel;
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

        var amortizationCancel = shareCancel.Value;
        var objCustomerCreditDocument = _customerCreditDocumentModel.GetRowByPk(customerCreditDocumentId);

        // Cancel Document
        if (amount >= objCustomerCreditDocument.Balance)
        {
            //mapeamos los valores para q no queden en null lo sque no se usaran
            var objCustomerCreditDocumentNew = _customerCreditDocumentModel.GetRowByPkk(objCustomerCreditDocument.CustomerCreditDocumentId.Value);
            objCustomerCreditDocumentNew.Balance = 0;
            objCustomerCreditDocumentNew.StatusID = Convert.ToInt32(documentCancel);
            _customerCreditDocumentModel.UpdateAppPosme(objCustomerCreditDocument.CustomerCreditDocumentId!.Value, objCustomerCreditDocumentNew);
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
        var objCustomerCreditDocument = _customerCreditDocumentModel.GetRowByPk(customerCreditDocumentId);
        var objListCustomerCreditDocumentAmortization = _customerCreditAmortizationModel.GetRowByDocumentAndVinculable(customerCreditDocumentId);
        var objCustomerCreditLine = _customerCreditLineModel.GetRowByPk(objCustomerCreditDocument.CustomerCreditLineId);
        var periodPay = _catalogItemModel.GetRowByCatalogItemId(objCustomerCreditLine.PeriodPay);
        decimal? totalCapital = 0;

        var numCuotas = objListCustomerCreditDocumentAmortization.Count;
        totalCapital = objCustomerCreditDocument!.Balance - amount;
        var dateApplyFirst = objListCustomerCreditDocumentAmortization.First().DateApply;

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


        var objCustomerCreditDocumentNew = _customerCreditDocumentModel.GetRowByPkk(objCustomerCreditDocument.CustomerCreditDocumentId.Value);
        objCustomerCreditDocumentNew.Balance = totalCapital!.Value;
        _customerCreditDocumentModel.UpdateAppPosme(objCustomerCreditDocument.CustomerCreditDocumentId!.Value, objCustomerCreditDocumentNew);
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
            var objCustomerCreditDocumentNew = _customerCreditDocumentModel.GetRowByPkk(objCustomerCreditDocument.CustomerCreditDocumentId.Value);
            objCustomerCreditDocumentNew.StatusID = Convert.ToInt32(documentProvisioned);
            _customerCreditDocumentModel.UpdateAppPosme(objCustomerCreditDocument.CustomerCreditDocumentId!.Value, objCustomerCreditDocumentNew);
        }
    }

    public void ApplyCuote(int companyId, int customerCreditDocumentId, decimal amount, int amoritizationId, int transactionMasterDetailID)
    {
        var shareDocumentCancel = _coreWebParameter.GetParameter("SHARE_DOCUMENT_CANCEL", companyId);
        var shareCancel = _coreWebParameter.GetParameter("SHARE_CANCEL", companyId);
        if (shareDocumentCancel is null)
        {
            throw new Exception("NO el parametro SHARE_DOCUMENT_CANCEL");
        }

        if (shareCancel is null)
        {
            throw new Exception("NO el parametro SHARE_CANCEL");
        }

        var objComponentAmortization = _coreWebTools.GetComponentIdByComponentName("tb_customer_credit_amoritization");
        if (objComponentAmortization is null)
        {
            throw new Exception("EL COMPONENTE 'tb_customer_credit_amoritization' NO EXISTE...");
        }

        var documentCancel = shareDocumentCancel.Value;
        var amortizationCancel = shareCancel.Value;
        var objCustomerCreditDocument = _customerCreditDocumentModel.GetRowByPk(customerCreditDocumentId);
        var objListCustomerCreditDocumentAmortization = _customerCreditAmortizationModel.GetRowByDocumentAndVinculable(customerCreditDocumentId);
        var objConceptos = new Dictionary<string, decimal>
        {
            { "capital", 0 },
            { "interes", 0 }
        };

        if (objListCustomerCreditDocumentAmortization is not null)
        {
            foreach (var itemAmortization in objListCustomerCreditDocumentAmortization)
            {
                var interval = (DateTime.Now - itemAmortization.DateApply).Days;

                var tempRemaining = itemAmortization.Remaining;
                if (amount >= tempRemaining && amount != decimal.Zero)
                {
                    amount = amount - tempRemaining;
                    var dif = tempRemaining - amount;
                    var itemAmortizationNew = new TbCustomerCreditAmoritization();
                    itemAmortizationNew = itemAmortization;
                    itemAmortizationNew.StatusID = Convert.ToInt32(amortizationCancel);
                    itemAmortizationNew.Remaining = decimal.Zero;
                    itemAmortizationNew.DayDelay = interval;

                    // Abonar a la cuota completa
                    if (tempRemaining == itemAmortization.Share)
                    {
                        objConceptos["capital"] = objConceptos["capital"] + itemAmortization.Capital;
                        objConceptos["interes"] = objConceptos["interes"] + itemAmortization.Interest;
                    }
                    else if (tempRemaining > itemAmortization.Interest)
                    {
                        objConceptos["capital"] = objConceptos["capital"] + (tempRemaining - itemAmortization.Interest);
                        objConceptos["interes"] = objConceptos["interes"] + itemAmortization.Interest;
                    }
                    else if (tempRemaining < itemAmortization.Interest)
                    {
                        objConceptos["capital"] = objConceptos["capital"] + 0;
                        objConceptos["interes"] = objConceptos["interes"] + tempRemaining;
                    }
                    else if (tempRemaining == itemAmortization.Interest)
                    {
                        objConceptos["capital"] = objConceptos["capital"] + 0;
                        objConceptos["interes"] = objConceptos["interes"] + itemAmortization.Interest;
                    }


                    _customerCreditAmortizationModel.UpdateAppPosme(itemAmortization.CreditAmortizationID, itemAmortizationNew);
                    var dataTMDR = new TbTransactionMasterDetailReference
                    {
                        TransactionMasterDetailID = transactionMasterDetailID,
                        ComponentID = objComponentAmortization.ComponentID,
                        ComponentItemID = itemAmortization.CreditAmortizationID,
                        Quantity = tempRemaining.ToString("N2"),
                        IsActive = 1,
                        CreatedOn = DateTime.Now,
                    };
                    _transactionMasterDetailReferencesModel.InsertAppPosme(dataTMDR);
                }
                else if (amount != decimal.Zero)
                {
                    var itemAmortizationNew = itemAmortization;
                    var dif = tempRemaining - amount;
                    itemAmortizationNew.Remaining = tempRemaining - amount;
                    itemAmortizationNew.DayDelay = interval;
                    if (dif > itemAmortization.Interest)
                    {
                        objConceptos["capital"] = objConceptos["capital"] + amount;
                        objConceptos["interes"] = objConceptos["interes"] + 0;
                    }
                    else if (dif == itemAmortization.Interest)
                    {
                        objConceptos["capital"] = objConceptos["capital"] + amount;
                        objConceptos["interes"] = objConceptos["interes"] + 0;
                    }
                    else if (dif < itemAmortization.Interest && tempRemaining <= itemAmortization.Interest)
                    {
                        objConceptos["capital"] = objConceptos["capital"] + 0;
                        objConceptos["interes"] = objConceptos["interes"] + amount;
                    }
                    else if (dif < itemAmortization.Interest && tempRemaining > itemAmortization.Interest)
                    {
                        var capital001 = tempRemaining - itemAmortization.Interest;
                        var interes001 = amount - capital001;
                        objConceptos["capital"] += capital001;
                        objConceptos["interes"] += interes001;
                    }


                    _customerCreditAmortizationModel.UpdateAppPosme(itemAmortization.CreditAmortizationID, itemAmortizationNew);
                    var dataTMDR = new TbTransactionMasterDetailReference
                    {
                        TransactionMasterDetailID = transactionMasterDetailID,
                        ComponentID = objComponentAmortization.ComponentID,
                        ComponentItemID = itemAmortization.CreditAmortizationID,
                        Quantity = tempRemaining.ToString("N2"),
                        IsActive = 1,
                        CreatedOn = DateTime.Now,
                    };
                    _transactionMasterDetailReferencesModel.InsertAppPosme(dataTMDR);
                    amount = 0;
                }
            }
        }


        //Actualizar Balance del Documento
        var objCustomerCreditDocumentNew = _customerCreditDocumentModel.GetRowByPkk(objCustomerCreditDocument.CustomerCreditDocumentId.Value);
        objCustomerCreditDocumentNew.Balance = objCustomerCreditDocument.Balance!.Value - objConceptos["capital"];
        _customerCreditDocumentModel.UpdateAppPosme(objCustomerCreditDocument.CustomerCreditDocumentId!.Value, objCustomerCreditDocumentNew);

        //Cancel Document
        objListCustomerCreditDocumentAmortization = _customerCreditAmortizationModel.GetRowByDocumentAndVinculable(customerCreditDocumentId);
        if (objListCustomerCreditDocumentAmortization is not null)
        {
            if (objListCustomerCreditDocumentAmortization.Count == 0)
            {
                objCustomerCreditDocumentNew.StatusID = Convert.ToInt32(documentCancel);
                _customerCreditDocumentModel.UpdateAppPosme(objCustomerCreditDocument.CustomerCreditDocumentId!.Value, objCustomerCreditDocumentNew);
            }
        }
    }
}