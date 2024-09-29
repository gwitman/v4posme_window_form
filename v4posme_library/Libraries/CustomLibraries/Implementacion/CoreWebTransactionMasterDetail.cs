using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;
using v4posme_library.Libraries.CustomModels.Core;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion;

class CoreWebTransactionMasterDetail : ICoreWebTransactionMasterDetail
{
    public decimal GetCostCustomer(int companyId, string? itemId, decimal unitaryCost, decimal unitaryPrice)
    {
        var companyModel = VariablesGlobales.Instance.UnityContainer.Resolve<CompanyModel>();
        var itemModel = VariablesGlobales.Instance.UnityContainer.Resolve<IItemModel>();

        var objCompany = companyModel.GetRowByPk(companyId);
        var objItem = itemModel.GetRowByPk(companyId, Convert.ToInt32(itemId));

        var finalUnitaryCost = unitaryCost;

        // Obtener el costo global pro para ciertos productos.
        if (
            objCompany.Type == "globalpro" && 
            new[] { "ITT00000217", "ITT00000218", "ITT00000219", "ITT00000220", "ITT00000221" }.Contains(objItem.BarCode)
        )
        {
            finalUnitaryCost = unitaryPrice - (unitaryPrice * 0.35m);
        }

        return finalUnitaryCost;
    }

    public decimal GetPercentageCommission(int companyId, int listPriceId, string? itemId, decimal price)
    {
        var priceModel = VariablesGlobales.Instance.UnityContainer.Resolve<IPriceModel>();
        var companyModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyModel>();

        var objCompany = companyModel.GetRowByPk(companyId);
        var objPriceToCommission =
            priceModel.GetRowByItemIdAndAmountAndComission(companyId, listPriceId, Convert.ToInt32(itemId), price);
        var objPriceToCommissionZero =
            priceModel.GetRowByItemIdAndAmountAndComission(companyId, listPriceId, Convert.ToInt32(itemId), 0);
        var commissionPercentage = decimal.Zero;

        // Obtener el porcentaje de comisión comportamiento normal
        if (objPriceToCommission is not null)
        {
            commissionPercentage = objPriceToCommission.PercentageCommision;
        }

        // Obtener el porcentaje de comisión de globalpro cuando no tiene comisión para un precio en específico
        if (objCompany.Type == "globalpro" &&
            objPriceToCommissionZero != null &&
            commissionPercentage == 0)
        {
            commissionPercentage = objPriceToCommissionZero.PercentageCommision;
        }

        return commissionPercentage;
    }
}