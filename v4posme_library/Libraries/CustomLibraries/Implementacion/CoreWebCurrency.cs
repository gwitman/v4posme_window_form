using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion;

public class CoreWebCurrency(ICoreWebParameter coreWebParameter, ICurrencyModel currencyModel)
    : ICoreWebCurrency
{
    private readonly IExchangerateModel _exchangerateModel =
        VariablesGlobales.Instance.UnityContainer.Resolve<IExchangerateModel>();

    private TbCompanyParameter MoneyFuncionalName(string? parameterName, int companyId)
    {
        var moneyFuncionalName =
            coreWebParameter.GetParameter(parameterName, companyId);
        if (moneyFuncionalName is null)
        {
            throw new Exception("NO EXISTE MONEY_FUNCIONAL_NAME");
        }

        return moneyFuncionalName;
    }

    public TbCurrency? GetCurrencyDefault(int companyId)
    {
        var moneyFuncionalName = MoneyFuncionalName("ACCOUNTING_CURRENCY_NAME_FUNCTION", companyId);
        return GetCurrencyName(moneyFuncionalName.Value);
    }

    public TbCurrency? GetCurrencyReport(int companyId)
    {
        var moneyFuncionalName = MoneyFuncionalName("ACCOUNTING_CURRENCY_NAME_REPORT", companyId);
        return GetCurrencyName(moneyFuncionalName.Value);
    }

    public TbCurrency? GetCurrencyExternal(int companyId)
    {
        var moneyFuncionalName = MoneyFuncionalName("ACCOUNTING_CURRENCY_NAME_EXTERNAL", companyId);
        return GetCurrencyName(moneyFuncionalName.Value);
    }

    public TbCurrency? GetCurrencyName(string? name)
    {
        return currencyModel.GetRowName(name);
    }

    public int GetTarget(int companyId, int currencySourceId)
    {
        var defaultCurrency = GetCurrencyDefault(companyId)!.CurrencyID;
        var report = GetCurrencyExternal(companyId)!.CurrencyID;
        return currencySourceId == defaultCurrency ? report : defaultCurrency;
    }

    public decimal GetRatio(int companyId, DateTime dateRatio, decimal quantity, int currencyId, int targetCurrencyId)
    {
        // Obtener monedas por defecto
        var exchangeRate = decimal.One;
        var exchangeRate2 = decimal.One;
        var objConvertionDefault = GetCurrencyDefault(companyId);
        if (objConvertionDefault is null)
            throw new Exception("NO EXISTE LA MONEDA POR DEFECTO");

        // Cuantos CORDOBAS son en tantos CORDOBAS
        var objConvertionSource = _exchangerateModel.GetRowByPk(companyId, dateRatio, objConvertionDefault.CurrencyID, currencyId);
        if (currencyId != objConvertionDefault.CurrencyID)
        {
            if (objConvertionSource is null)
                throw new Exception("NO EXISTE LA TASA DE CAMBIO [012555]:  " + dateRatio);
            else
                exchangeRate = (decimal)objConvertionSource.Ratio!;
        }

        // Cuantos DOLARES son en tantos CORDOBAS
        var objConvertionTarget = _exchangerateModel.GetRowByPk(companyId, dateRatio, objConvertionDefault.CurrencyID, targetCurrencyId);
        if (targetCurrencyId != objConvertionDefault.CurrencyID)
        {
            if (objConvertionTarget is null)
                throw new Exception("NO EXISTE LA TASA DE CAMBIO [012556]:  " + dateRatio);
            exchangeRate2 = (decimal)objConvertionTarget.Ratio!;
        }

        if (targetCurrencyId == currencyId)
            return quantity;
        return (quantity * exchangeRate) / exchangeRate2;
    }
}