using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels.Core;

class ExchangerateModel(DataContext context) : IExchangerateModel
{
    public void UpdateAppPosme(int companyId, DateOnly date, int currencyIdSource, int currencyIdTarget,
        TbExchangeRate data)
    {
        
        context.TbExchangeRates
            .Where(rate => rate.CompanyID == companyId
                           && Equals(rate.Date, date)
                           && rate.CurrencyID == currencyIdSource
                           && rate.TargetCurrencyID == currencyIdTarget)
            .ExecuteUpdate(calls => calls
                .SetProperty(rate => rate.Ratio, data.Ratio)
                .SetProperty(rate => rate.Value, data.Value));
    }

    public int InsertAppPosme(TbExchangeRate data)
    {
        
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.ExchangeRateID;
    }

    public TbExchangeRate? GetDefault(int companyId)
    {
        
        return context.TbExchangeRates.AsNoTracking()
            .First(rate => Math.Abs(rate.Ratio!.Value - 1) < 1E8
                           && rate.CompanyID == companyId);
    }

    public TbExchangeRate? GetRowByPk(int companyId, DateTime date, int currencyIdSource, int currencyIdTarget)
    {
        
        return context.TbExchangeRates.AsNoTracking()
            .FirstOrDefault(rate => rate!.CompanyID == companyId
                           && rate.CurrencyID == currencyIdSource
                           && rate.TargetCurrencyID == currencyIdTarget
                           && Equals(rate.Date.Date, date));
    }

    public List<TbExchangeRateDto> GetByCompanyAndDate(int companyId, DateOnly dateStartOn, DateOnly dateEndOn)
    {
        
        var result = from er in context.TbExchangeRates.AsNoTracking()
            join cc in context.TbCompanyCurrencies.AsNoTracking()
                on new { er.CompanyID, er.CurrencyID } equals new { cc.CompanyID, cc.CurrencyID }
            join c in context.TbCurrencies.AsNoTracking() on cc.CurrencyID equals c.CurrencyID
            join cct in context.TbCompanyCurrencies.AsNoTracking()
                on new { er.CompanyID, TargetCurrencyId = er.TargetCurrencyID } equals new
                    { cct.CompanyID, TargetCurrencyId = cct.CurrencyID }
            join ct in context.TbCurrencies.AsNoTracking()  on cct.CurrencyID equals ct.CurrencyID
            where er.CompanyID == companyId &&
                  DateOnly.FromDateTime(er.Date) >= dateStartOn && DateOnly.FromDateTime(er.Date) <= dateEndOn
            orderby er.Date, c.Name
            select new TbExchangeRateDto
            {
                Date = DateOnly.FromDateTime(er.Date),
                Value = er.Value,
                Ratio = er.Ratio,
                NameSource = c.Name,
                SimbSource = cc.Simb,
                NameTarget = ct.Name,
                SimbTarget = cct.Simb
            };
        return result.ToList();
    }
}