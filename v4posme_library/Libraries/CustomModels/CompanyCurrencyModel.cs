using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public class CompanyCurrencyModel : ICompanyCurrencyModel
{
    public int DeleteAppPosme(int companyId, int currencyId)
    {
        using var context = new DataContext();
        return context.TbCompanyCurrencies
            .Where(currency => currency.CurrencyID == currencyId
                               && currency.CompanyID == companyId)
            .ExecuteDelete();
    }

    public void UpdateAppPosme(int companyId, int currencyId, TbCompanyCurrency data)
    {
        using var context = new DataContext();
        var find = context.TbCompanyCurrencies
            .Single(currency =>
                currency.CurrencyID == currencyId
                && currency.CompanyID == companyId);
        data.CompanyCurrencyID = find.CompanyCurrencyID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int InsertAppPosme(TbCompanyCurrency data)
    {
        using var context = new DataContext();
        var add = context.TbCompanyCurrencies.Add(data);
        context.BulkSaveChanges();
        return add.Entity.CompanyCurrencyID;
    }

    public TbCompanyCurrency GetRowByPk(int companyId, int currencyId)
    {
        using var context = new DataContext();
        return context.TbCompanyCurrencies
            .Join(context.TbCurrencies,
                currency => currency.CurrencyID,
                tbCurrency => tbCurrency.CurrencyID,
                (currency, tbCurrency) => new { currency, tbCurrency })
            .Where(k => k.tbCurrency.IsActive!.Value)
            .Select(k => k.currency)
            .Single(currency =>
                currency.CurrencyID == currencyId
                && currency.CompanyID == companyId);
    }

    public List<TbCompanyCurrencyDto> GetByCompany(int companyId)
    {
        using var context = new DataContext();
        return context.TbCompanyCurrencies
            .Join(context.TbCurrencies, currency => currency.CurrencyID, 
                currency => currency.CurrencyID, 
                (currency, tbCurrency) => new {currency, tbCurrency})
            .Where(key => key.currency.CompanyID==companyId
            && key.tbCurrency.IsActive!.Value)
            .Select(arg => new TbCompanyCurrencyDto
            {
                CompanyId = arg.currency.CompanyID,
                CurrencyId = arg.currency.CurrencyID,
                Simb = arg.currency.Simb,
                Simbol = arg.tbCurrency.Simbol,
                Name = arg.tbCurrency.Name
            })
            .ToList();
    }
}