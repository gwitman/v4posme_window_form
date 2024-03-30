using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Interfaz;

public interface ICoreWebCurrency
{
    TbCurrency? GetCurrencyDefault(int companyId);
    TbCurrency? GetCurrencyReport(int companyId);
    TbCurrency? GetCurrencyExternal(int companyId);
    TbCurrency? GetCurrencyName(string name);
    int GetTarget(int companyId, int currencySourceId);
    decimal GetRatio(int companyId, DateOnly dateRatio, decimal quantity, int currencyId, int targetCurrencyId);
}