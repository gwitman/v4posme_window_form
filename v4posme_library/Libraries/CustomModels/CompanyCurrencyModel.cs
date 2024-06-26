﻿using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public class CompanyCurrencyModel : ICompanyCurrencyModel
{
    public int DeleteAppPosme(int companyId, int currencyId)
    {
        using var context = new DataContext();
        return context.TbCompanyCurrencies
            .Where(currency => currency.CurrencyId == currencyId
                               && currency.CompanyId == companyId)
            .ExecuteDelete();
    }

    public void UpdateAppPosme(int companyId, int currencyId, TbCompanyCurrency data)
    {
        using var context = new DataContext();
        var find = context.TbCompanyCurrencies
            .Single(currency =>
                currency.CurrencyId == currencyId
                && currency.CompanyId == companyId);
        data.CompanyCurrencyId = find.CompanyCurrencyId;
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
            .Join(context.TbCurrencies,
                currency => currency.CurrencyId,
                tbCurrency => tbCurrency.CurrencyId,
                (currency, tbCurrency) => new { currency, tbCurrency })
            .Where(k => k.tbCurrency.IsActive!.Value)
            .Select(k => k.currency)
            .Single(currency =>
                currency.CurrencyId == currencyId
                && currency.CompanyId == companyId);
    }

    public List<TbCompanyCurrencyDto> GetByCompany(int companyId)
    {
        using var context = new DataContext();
        return context.TbCompanyCurrencies
            .Join(context.TbCurrencies, currency => currency.CurrencyId, 
                currency => currency.CurrencyId, 
                (currency, tbCurrency) => new {currency, tbCurrency})
            .Where(key => key.currency.CompanyId==companyId
            && key.tbCurrency.IsActive!.Value)
            .Select(arg => new TbCompanyCurrencyDto
            {
                CompanyId = arg.currency.CompanyId,
                CurrencyId = arg.currency.CurrencyId,
                Simb = arg.currency.Simb,
                Simbol = arg.tbCurrency.Simbol,
                Name = arg.tbCurrency.Name
            })
            .ToList();
    }
}