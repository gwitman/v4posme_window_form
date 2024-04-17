using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class PriceModel : IPriceModel
{
    public void DeleteAppPosme(int companyId, int listPriceId)
    {
        using var context = new DataContext();
        context.TbPrices
            .Where(price => price!.CompanyId == companyId
                            && price.ListPriceId == listPriceId)
            .ExecuteDelete();
    }

    public int InsertAppPosme(TbPrice data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.PriceId;
    }

    public void UpdateAppPosme(int companyId, int listPriceId, int itemId, int typePriceId, TbPrice data)
    {
        using var context = new DataContext();
        context.TbPrices.Where(price => price!.CompanyId == companyId
                                        && price.ListPriceId == listPriceId && price.ItemId == itemId
                                        && price.TypePriceId == typePriceId)
            .ExecuteUpdate(calls => calls
                .SetProperty(price => price!.Percentage, data.Percentage)
                .SetProperty(price => price!.Price, data.Price)
                .SetProperty(price => price!.PercentageCommision, data.PercentageCommision));
    }

    public List<TbPriceDto> GetRowByAll(int companyId, int listPriceId)
    {
        using var context = new DataContext();
        var result = from i in context.TbPrices
            join it in context.TbItems on i.ItemId equals it.ItemId
            join ci in context.TbCatalogItems on i.TypePriceId equals ci.CatalogItemId
            where i.CompanyId == companyId && i.ListPriceId == listPriceId
            select new TbPriceDto
            {
                CompanyId = i.CompanyId,
                ListPriceId = i.ListPriceId,
                ItemId = i.ItemId,
                PriceId = i.PriceId,
                TypePriceId = i.TypePriceId,
                Percentage = i.Percentage,
                Price = i.Price,
                TipoPrice = ci.Name,
                ItemNumber = it.ItemNumber,
                ItemName = it.Name,
                Cost = it.Cost,
                PercentageCommision = i.PercentageCommision
            };
        return result.ToList();
    }

    public TbPrice? GetRowByPk(int companyId, int listPriceId, int itemId, int typePriceId)
    {
        using var context = new DataContext();
        return context.TbPrices
            .FirstOrDefault(price => price!.CompanyId == companyId
                            && price.ListPriceId == listPriceId
                            && price.ItemId == itemId
                            && price.TypePriceId == typePriceId);
    }

    public List<TbPrice> GetRowByTransactionMasterId(int companyId, int listPriceId, int transactionMasterId)
    {
        using var context = new DataContext();
        return context.TbTransactionMasters
            .Join(context.TbTransactionMasterDetails,
                tm => tm.TransactionMasterId,
                tmd => tmd.TransactionMasterId,
                (tm, tmd) => new { tm, tmd })
            .Join(context.TbPrices,
                t => t.tmd.ComponentItemId,
                i => i.ItemId,
                (t, i) => new { t, i })
            .Where(t =>
                t.i.CompanyId == companyId && t.i.ListPriceId == listPriceId &&
                t.t.tm.TransactionMasterId == transactionMasterId)
            .Select(t => t.i)
            .ToList();
    }

    public TbPrice? GetRowByItemIdAndAmountAndComission(int companyId, int listPriceId, int itemId, decimal amount)
    {
        using var context = new DataContext();
        return context.TbPrices
            .Where(price => price!.CompanyId == companyId
                            && price.ListPriceId == listPriceId
                            && price.ItemId == itemId
                            && price.PercentageCommision > decimal.Zero
                            && price.Price >= amount)
            .OrderBy(price => price!.Price)
            .FirstOrDefault();
    }

    public List<TbPriceDto> GetRowByItemId(int companyId, int listPriceId, int itemId)
    {
        using var context = new DataContext();
        return context.TbPrices
            .Join(context.TbCatalogItems,
                i => i.TypePriceId,
                c => c.CatalogItemId,
                (i, c) => new { i, c })
            .Where(t => t.i.CompanyId == companyId
                        && t.i.ListPriceId == listPriceId
                        && t.i.ItemId == itemId)
            .Select(t => new TbPriceDto
            {
                CompanyId = t.i.CompanyId,
                ListPriceId = t.i.ListPriceId,
                ItemId = t.i.ItemId,
                PriceId = t.i.PriceId,
                TypePriceId = t.i.TypePriceId,
                Percentage = t.i.Percentage,
                Price = t.i.Price,
                NameTypePrice = t.c.Name,
                PercentageCommision = t.i.PercentageCommision
            }).ToList();
    }
}