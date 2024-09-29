using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class ItemSkuModel : IItemSkuModel
{
    public int DeleteAppPosme(int itemId)
    {
        using var context = new DataContext();
        return context.TbItemSkus
            .Where(sku => sku.ItemID == itemId)
            .ExecuteDelete();
    }

    public void UpdateAppPosme(int itemId, int catalogItemId, TbItemSku data)
    {
        using var context = new DataContext();
        var find = context.TbItemSkus
            .Where(sku => sku.ItemID == itemId
                          && sku.CatalogItemID == catalogItemId)
            .ExecuteUpdate(calls =>
                calls.SetProperty(sku => sku.Value, data.Value));
    }

    public int InsertAppPosme(TbItemSku data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.SkuID;
    }

    public List<TbItemSkuDto> GetRowByItemId(int itemId)
    {
        using var context = new DataContext();
        var result = from sku in context.TbItemSkus
            join w in context.TbCatalogItems on sku.CatalogItemID equals w.CatalogItemID
            where sku.ItemID == itemId
            select new TbItemSkuDto
            {
                SkuId = sku.SkuID,
                ItemId = sku.ItemID,
                CatalogItemId = sku.CatalogItemID,
                Value = sku.Value,
                Sku = w.Display
            };
        return result.ToList();
    }

    public TbItemSkuDto? GetByPk(int itemId, int catalogItemId)
    {
        using var context = new DataContext();
        var result = from sku in context.TbItemSkus.AsNoTracking()
            join w in context.TbCatalogItems.AsNoTracking() on sku.CatalogItemID equals w.CatalogItemID
            where sku.ItemID == itemId && sku.CatalogItemID == catalogItemId
            select new TbItemSkuDto
            {
                SkuId = sku.SkuID,
                ItemId = sku.ItemID,
                CatalogItemId = sku.CatalogItemID,
                Value = sku.Value,
                Sku = w.Display
            };
        return result.SingleOrDefault();
    }

    public List<TbItemSkuDto> GetRowByTransactionMasterId(int companyId, int transactionMasterId)
    {
        using var context = new DataContext();
        var result = from tm in context.TbTransactionMasters
            join tmd in context.TbTransactionMasterDetails on tm.TransactionMasterID equals tmd.TransactionMasterID
            join i in context.TbItemSkus on tmd.ComponentItemID equals i.ItemID
            join w in context.TbCatalogItems on i.CatalogItemID equals w.CatalogItemID
            where tm.TransactionMasterID == transactionMasterId
            select new TbItemSkuDto
            {
                SkuId = i.SkuID,
                ItemId = i.ItemID,
                CatalogItemId = i.CatalogItemID,
                Value = i.Value,
                Sku = w.Display
            };
        return result.ToList();
    }
}