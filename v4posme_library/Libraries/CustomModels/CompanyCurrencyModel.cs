using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class CompanyCurrencyModel : ICompanyCurrencyModel
{
    public int DeleteAppPosme(int companyId, int currencyId)
    {
        using var context = new DataContext();
        var find = GetRowByPk(companyId, currencyId);
        context.TbCompanyCurrencies.Remove(find);
        return context.SaveChanges();
    }

    public void UpdateAppPosme(int companyId, int currencyId, TbCompanyCurrency data)
    {
        using var context = new DataContext();
        var find = GetRowByPk(companyId, currencyId);
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int InsertAppPosme(TbCompanyCurrency data)
    {
        using var context = new DataContext();
        var add = context.TbCompanyCurrencies.Add(data);
        context.BulkSaveChanges();
        return add.Entity.CompanyCurrencyId;
    }

    public TbCompanyCurrency GetRowByPk(int companyId, int currencyId)
    {
        using var context = new DataContext();
        return context.TbCompanyCurrencies
            .Single(currency =>
                currency.CompanyCurrencyId == currencyId
                && currency.CompanyId == companyId);
    }

    public List<TbCompanyCurrency> GetByCompany(int companyId)
    {
        using var context = new DataContext();
        return context.TbCompanyCurrencies
            .Where(currency => currency.CompanyId == companyId)
            .ToList();
    }
}