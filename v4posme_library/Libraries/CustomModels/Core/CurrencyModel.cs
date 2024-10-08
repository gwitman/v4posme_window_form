﻿using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class CurrencyModel : ICurrencyModel
{
    public TbCurrency GetRowName(string? name)
    {
        using var context = new DataContext();
        return context.TbCurrencies.AsNoTracking()
            .First(currency => currency.Name == name
                               && currency.IsActive!.Value);
    }

    public TbCurrency GetRowByPk(int currencyId)
    {
        using var context = new DataContext();
        return context.TbCurrencies.AsNoTracking()
            .First(currency => currency.CurrencyID == currencyId
                               && currency.IsActive!.Value);
    }

    public List<TbCurrency> GetList()
    {
        using var context = new DataContext();
        return context.TbCurrencies.AsNoTracking().ToList();
    }
}