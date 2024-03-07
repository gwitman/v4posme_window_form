using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class ItemSkuModel : IItemSkuModel
{
    public void DeleteAppPosme(int itemId)
    {
        using var context = new DataContext();
        context.TbItemSkus
            .Where(sku => sku.ItemId == itemId)
            .ExecuteDelete();
    }

    public void UpdateAppPosme(int itemId, int catalogItemId, TbItemSku data)
    {
        using var context = new DataContext();
        var find = context.TbItemSkus
            .Where(sku => sku.ItemId == itemId
                          && sku.CatalogItemId == catalogItemId)
            .ExecuteUpdate(calls =>
                calls.SetProperty(sku => sku.Value, data.Value));
    }

    public int InsertAppPosme(TbItemSku data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.SkuId;
    }

    public List<TbItemSku> GetRowByItemId(int itemId)
    {
        using var context = new DataContext();
        var result = from sku in context.TbItemSkus
            join w in context.TbCatalogItems on sku.CatalogItemId equals w.CatalogItemId
            where sku.ItemId == itemId
            select new TbItemSku
            {
                SkuId = sku.SkuId,
                ItemId = sku.ItemId,
                CatalogItemId = sku.CatalogItemId,
                Value = sku.Value,
                Sku = w.Display
            };
        return result.ToList();
    }

    public TbItemSku GetByPk(int itemId, int catalogItemId)
    {
        using var context = new DataContext();
        var result = from sku in context.TbItemSkus
            join w in context.TbCatalogItems on sku.CatalogItemId equals w.CatalogItemId
            where sku.ItemId == itemId && sku.CatalogItemId == catalogItemId
            select new TbItemSku
            {
                SkuId = sku.SkuId,
                ItemId = sku.ItemId,
                CatalogItemId = sku.CatalogItemId,
                Value = sku.Value,
                Sku = w.Display
            };
        return result.Single();
    }

    public List<TbItemSku> GetRowByTransactionMasterId(int companyId, int transactionMasterId)
    {
        using var context = new DataContext();
        var result = from tm in context.TbTransactionMasters
            join tmd in context.TbTransactionMasterDetails on tm.TransactionMasterId equals tmd.TransactionMasterId
            join i in context.TbItemSkus on tmd.ComponentItemId equals i.ItemId
            join w in context.TbCatalogItems on i.CatalogItemId equals w.CatalogItemId
            where tm.TransactionMasterId == transactionMasterId
            select new TbItemSku
            {
                SkuId = i.SkuId,
                ItemId = i.ItemId,
                CatalogItemId = i.CatalogItemId,
                Value = i.Value,
                Sku = w.Display
            };
        return result.ToList();
    }
}