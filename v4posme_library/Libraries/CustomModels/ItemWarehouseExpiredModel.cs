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
            .Where(expired => expired.CompanyId == companyId
                              && expired.ItemWarehouseExpiredId == itemWarehouseExpiredId)
            .ExecuteDelete();
    }

    public void UpdateAppPosme(int companyId, int itemWarehouseExpiredId, TbItemWarehouseExpired data)
    {
        using var context = new DataContext();
        var find = context.TbItemWarehouseExpireds
            .Single(expired => expired.CompanyId == companyId
                               && expired.ItemWarehouseExpiredId == itemWarehouseExpiredId);
        data.ItemWarehouseExpiredId = find.ItemWarehouseExpiredId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int InsertAppPosme(TbItemWarehouseExpired data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.ItemWarehouseExpiredId;
    }

    public List<TbItemWarehouseExpired> GetByItemIdAndWarehouse(int companyId, int warehouseId, int itemId)
    {
        using var context = new DataContext();
        return context.TbItemWarehouseExpireds
            .Where(expired => expired.CompanyId == companyId
                              && expired.WarehouseId == warehouseId
                              && expired.ItemId == itemId)
            .ToList();
    }

    public List<TbItemWarehouseExpired> GetByItemIdAndWarehouseAndLote(int companyId, int warehouseId, int itemId,
        string lote)
    {
        using var context = new DataContext();
        return context.TbItemWarehouseExpireds
            .Where(expired => expired.CompanyId == companyId
                              && expired.WarehouseId == warehouseId
                              && expired.ItemId == itemId
                              && expired.Lote!.Contains(lote))
            .ToList();
    }

    public TbItemWarehouseExpired getBy_ItemIDAndWarehouseAndLoteAndExpired(int companyId, int warehouseId, int itemId,
        string lote, DateTime expiredDate)
    {
        using var context = new DataContext();
        return context.TbItemWarehouseExpireds
            .Single(expired => expired.CompanyId == companyId
                               && expired.WarehouseId == warehouseId
                               && expired.ItemId == itemId
                               && expired.Lote!.Contains(lote)
                               && expired.DateExpired == expiredDate);
    }

    public TbItemWarehouseExpired GetByPk(int companyId, int itemWarehouseExpiredId)
    {
        using var context = new DataContext();
        return context.TbItemWarehouseExpireds
            .Single(expired => expired.CompanyId == companyId
                               && expired.ItemWarehouseExpiredId == itemWarehouseExpiredId);
    }

    public List<TbItemWarehouseExpiredDto> GetByItemIdAproxVencimiento(int companyId)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbItems
            join iw in dbContext.TbItemWarehouses on i.ItemId equals iw.ItemId
            join w in dbContext.TbWarehouses on iw.WarehouseId equals w.WarehouseId
            join e in dbContext.TbItemWarehouseExpireds on new { iw.ItemId, iw.WarehouseId } equals new { e.ItemId, e.WarehouseId }
            where i.CompanyId == companyId 
                  && DateTime.Now.AddMonths(6) > e.DateExpired
            select new TbItemWarehouseExpiredDto
            {
                ItemNumber = i.ItemNumber,
                ItemName = i.Name,
                WarehouseNumber = w.WarehouseId,
                WarehouseName = w.Name,
                Quantity = e.Quantity,
                Lote = e.Lote,
                DateExpired = e.DateExpired
            };
        return result.ToList();
    }
}