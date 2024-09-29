using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class ItemWarehouseExpiredModel : IItemWarehouseExpiredModel
{
    public void DeleteByPk(int companyId, int itemWarehouseExpiredId)
    {
        using var context = new DataContext();
        context.TbItemWarehouseExpireds
            .Where(expired => expired.CompanyID == companyId
                              && expired.ItemWarehouseExpiredID == itemWarehouseExpiredId)
            .ExecuteDelete();
    }

    public void UpdateAppPosme(int companyId, int itemWarehouseExpiredId, TbItemWarehouseExpired data)
    {
        using var context = new DataContext();
        var find = context.TbItemWarehouseExpireds
            .Single(expired => expired.CompanyID == companyId
                               && expired.ItemWarehouseExpiredID == itemWarehouseExpiredId);
        data.ItemWarehouseExpiredID = find.ItemWarehouseExpiredID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int InsertAppPosme(TbItemWarehouseExpired data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.ItemWarehouseExpiredID;
    }

    public List<TbItemWarehouseExpired> GetByItemIdAndWarehouse(int companyId, int warehouseId, int itemId)
    {
        using var context = new DataContext();
        return context.TbItemWarehouseExpireds
            .Where(expired => expired.CompanyID == companyId
                              && expired.WarehouseID == warehouseId
                              && expired.ItemID == itemId)
            .ToList();
    }

    public List<TbItemWarehouseExpired> GetByItemIdAndWarehouseAndLote(int companyId, int warehouseId, int itemId,
        string? lote)
    {
        using var context = new DataContext();
        return context.TbItemWarehouseExpireds
            .Where(expired => expired.CompanyID == companyId
                              && expired.WarehouseID == warehouseId
                              && expired.ItemID == itemId
                              && expired.Lote!.Contains(lote))
            .ToList();
    }

    public TbItemWarehouseExpired getBy_ItemIDAndWarehouseAndLoteAndExpired(int companyId, int warehouseId, int itemId,
        string? lote, DateTime expiredDate)
    {
        using var context = new DataContext();
        return context.TbItemWarehouseExpireds
            .Single(expired => expired.CompanyID == companyId
                               && expired.WarehouseID == warehouseId
                               && expired.ItemID == itemId
                               && expired.Lote!.Contains(lote)
                               && expired.DateExpired == expiredDate);
    }

    public TbItemWarehouseExpired GetByPk(int companyId, int itemWarehouseExpiredId)
    {
        using var context = new DataContext();
        return context.TbItemWarehouseExpireds
            .Single(expired => expired.CompanyID == companyId
                               && expired.ItemWarehouseExpiredID == itemWarehouseExpiredId);
    }

    public List<TbItemWarehouseExpiredDto> GetByItemIdAproxVencimiento(int companyId)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbItems
            join iw in dbContext.TbItemWarehouses on i.ItemID equals iw.ItemID
            join w in dbContext.TbWarehouses on iw.WarehouseID equals w.WarehouseID
            join e in dbContext.TbItemWarehouseExpireds on new { iw.ItemID, iw.WarehouseID } equals new { e.ItemID, e.WarehouseID }
            where i.CompanyID == companyId 
                  && DateTime.Now.AddMonths(6) > e.DateExpired
            select new TbItemWarehouseExpiredDto
            {
                ItemNumber = i.ItemNumber,
                ItemName = i.Name,
                WarehouseNumber = w.WarehouseID,
                WarehouseName = w.Name,
                Quantity = e.Quantity,
                Lote = e.Lote,
                DateExpired = e.DateExpired
            };
        return result.ToList();
    }
}