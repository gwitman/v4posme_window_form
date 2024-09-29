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
            .Where(price => price!.CompanyID == companyId
                            && price.ListPriceID == listPriceId)
            .ExecuteDelete();
    }

    public int InsertAppPosme(TbPrice data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.PriceID;
    }

    public void UpdateAppPosme(int companyId, int listPriceId, int itemId, int typePriceId, TbPrice data)
    {
        using var context = new DataContext();
        context.TbPrices.Where(price => price!.CompanyID == companyId
                                        && price.ListPriceID == listPriceId && price.ItemID == itemId
                                        && price.TypePriceID == typePriceId)
            .ExecuteUpdate(calls => calls
                .SetProperty(price => price!.Percentage, data.Percentage)
                .SetProperty(price => price!.Price, data.Price)
                .SetProperty(price => price!.PercentageCommision, data.PercentageCommision));
    }

    public List<TbPriceDto> GetRowByAll(int companyId, int listPriceId)
    {
        using var context = new DataContext();
        var result = from i in context.TbPrices
            join it in context.TbItems on i.ItemID equals it.ItemID
            join ci in context.TbCatalogItems on i.TypePriceID equals ci.CatalogItemID
            where i.CompanyID == companyId && i.ListPriceID == listPriceId
            select new TbPriceDto
            {
                CompanyId = i.CompanyID,
                ListPriceId = i.ListPriceID,
                ItemId = i.ItemID,
                PriceId = i.PriceID,
                TypePriceId = i.TypePriceID,
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
            .FirstOrDefault(price => price!.CompanyID == companyId
                            && price.ListPriceID == listPriceId
                            && price.ItemID == itemId
                            && price.TypePriceID == typePriceId);
    }

    public List<TbPrice> GetRowByTransactionMasterId(int companyId, int listPriceId, int transactionMasterId)
    {
        using var context = new DataContext();
        return context.TbTransactionMasters
            .Join(context.TbTransactionMasterDetails,
                tm => tm.TransactionMasterID,
                tmd => tmd.TransactionMasterID,
                (tm, tmd) => new { tm, tmd })
            .Join(context.TbPrices,
                t => t.tmd.ComponentItemID,
                i => i.ItemID,
                (t, i) => new { t, i })
            .Where(t =>
                t.i.CompanyID == companyId && t.i.ListPriceID == listPriceId &&
                t.t.tm.TransactionMasterID == transactionMasterId)
            .Select(t => t.i)
            .ToList();
    }

    public TbPrice? GetRowByItemIdAndAmountAndComission(int companyId, int listPriceId, int itemId, decimal amount)
    {
        using var context = new DataContext();
        return context.TbPrices
            .Where(price => price!.CompanyID == companyId
                            && price.ListPriceID == listPriceId
                            && price.ItemID == itemId
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
                i => i.TypePriceID,
                c => c.CatalogItemID,
                (i, c) => new { i, c })
            .Where(t => t.i.CompanyID == companyId
                        && t.i.ListPriceID == listPriceId
                        && t.i.ItemID == itemId)
            .Select(t => new TbPriceDto
            {
                CompanyId = t.i.CompanyID,
                ListPriceId = t.i.ListPriceID,
                ItemId = t.i.ItemID,
                PriceId = t.i.PriceID,
                TypePriceId = t.i.TypePriceID,
                Percentage = t.i.Percentage,
                Price = t.i.Price,
                NameTypePrice = t.c.Name,
                PercentageCommision = t.i.PercentageCommision
            }).ToList();
    }
}