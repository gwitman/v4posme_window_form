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
            .Where(rate => rate.CompanyId == companyId
                           && rate.Date == date
                           && rate.CurrencyId == currencyIdSource
                           && rate.TargetCurrencyId == currencyIdTarget)
            .ExecuteUpdate(calls => calls
                .SetProperty(rate => rate.Ratio, data.Ratio)
                .SetProperty(rate => rate.Value, data.Value));
    }

    public int InsertAppPosme(TbExchangeRate data)
    {
        
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.ExchangeRateId;
    }

    public TbExchangeRate? GetDefault(int companyId)
    {
        
        return context.TbExchangeRates.AsNoTracking()
            .First(rate => Math.Abs(rate.Ratio!.Value - 1) < 1E8
                           && rate.CompanyId == companyId);
    }

    public TbExchangeRate? GetRowByPk(int companyId, DateOnly date, int currencyIdSource, int currencyIdTarget)
    {
        
        return context.TbExchangeRates.AsNoTracking()
            .FirstOrDefault(rate => rate!.CompanyId == companyId
                           && rate.CurrencyId == currencyIdSource
                           && rate.TargetCurrencyId == currencyIdTarget
                           && rate.Date==date);
    }

    public List<TbExchangeRateDto> GetByCompanyAndDate(int companyId, DateOnly dateStartOn, DateOnly dateEndOn)
    {
        
        var result = from er in context.TbExchangeRates.AsNoTracking()
            join cc in context.TbCompanyCurrencies.AsNoTracking()
                on new { er.CompanyId, er.CurrencyId } equals new { cc.CompanyId, cc.CurrencyId }
            join c in context.TbCurrencies.AsNoTracking() on cc.CurrencyId equals c.CurrencyId
            join cct in context.TbCompanyCurrencies.AsNoTracking()
                on new { er.CompanyId, TargetCurrencyId = er.TargetCurrencyId } equals new
                    { cct.CompanyId, TargetCurrencyId = cct.CurrencyId }
            join ct in context.TbCurrencies.AsNoTracking()  on cct.CurrencyId equals ct.CurrencyId
            where er.CompanyId == companyId &&
                  er.Date >= dateStartOn && er.Date <= dateEndOn
            orderby er.Date, c.Name
            select new TbExchangeRateDto
            {
                Date = er.Date,
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