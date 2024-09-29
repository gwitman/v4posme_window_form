using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class ItemWarehouseModel : IItemWarehouseModel
{
    public void DeleteWhereIdNotIn(int companyId, int itemId, List<int> listWarehouseId)
    {
        using var context = new DataContext();
        context.TbItemWarehouses
            .Where(warehouse => warehouse.CompanyID == companyId
                                && warehouse.ItemID == itemId
                                && warehouse.Quantity == decimal.Zero
                                && !listWarehouseId.Contains(warehouse.WarehouseID))
            .ExecuteDelete();
    }

    public void UpdateAppPosme(int companyId, int itemId, int warehouseId, TbItemWarehouse data)
    {
        using var context = new DataContext();
        var find = context.TbItemWarehouses
            .Where(warehouse => warehouse.CompanyID == companyId
                                && warehouse.ItemID == itemId
                                && warehouse.WarehouseID == warehouseId)
            .ExecuteUpdate(calls =>
                calls.SetProperty(warehouse => warehouse.Quantity, data.Quantity)
                    .SetProperty(warehouse => warehouse.Cost, data.Cost)
                    .SetProperty(warehouse => warehouse.QuantityMax, data.QuantityMax)
                    .SetProperty(warehouse => warehouse.QuantityMin, data.QuantityMin));
    }

    public int InsertAppPosme(TbItemWarehouse data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.ItemWarehouseId;
    }

    public List<TbItemWarehouseDto> GetByWarehouse(int companyId, int warehouseId)
    {
        using var context = new DataContext();
        var result = from i in context.TbItems
            join w in context.TbItemWarehouses on i.ItemID equals w.ItemID
            join ci in context.TbCatalogItems on Convert.ToInt32(i.UnitMeasureID) equals ci.CatalogItemID
            where i.CompanyID == companyId && w.WarehouseID == warehouseId && i.IsActive.Value
            select new TbItemWarehouseDto
            {
                Codigo = i.ItemNumber,
                Producto = i.Name,
                UM = ci.Display,
                ItemId = i.ItemID,
                Quantity = w.Quantity,
                Cost = i.Cost
            };
        return result.ToList();
    }

    public List<TbItemWarehouseDto> GetRowLowMinimus(int companyId)
    {
        using var dbContext = new DataContext();
        var result = from iw in dbContext.TbItemWarehouses
            join w in dbContext.TbWarehouses on iw.WarehouseID equals w.WarehouseID
            join i in dbContext.TbItems on iw.ItemID equals i.ItemID
            where i.CompanyID == companyId
                  && i.IsActive.Value
                  && w.IsActive 
                  && iw.Quantity < iw.QuantityMin
            select new TbItemWarehouseDto
            {
                ItemNumber = i.ItemNumber,
                ItemName = i.Name,
                Quantity = iw.Quantity,
                QuantityMin = iw.QuantityMin,
                Number = w.Number,
                WarehouseName = w.Name
            };
        return result.ToList();
    }

    public List<TbItemWarehouseDto> GetRowByItemId(int companyId, int itemId)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbItemWarehouses
            join w in dbContext.TbWarehouses on i.WarehouseID equals w.WarehouseID
            where i.CompanyID == companyId && i.ItemID == itemId
            select new TbItemWarehouseDto
            {
                CompanyId = i.CompanyID,
                BranchId = i.BranchID,
                WarehouseId = i.WarehouseID,
                ItemId = i.ItemID,
                Quantity = i.Quantity,
                QuantityMax = i.QuantityMax,
                QuantityMin = i.QuantityMin,
                WarehouseName = w.Name
            };
        return result.ToList();
    }

    public TbItemWarehouseDto? GetByPk(int companyId, int itemId, int warehouseId)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbItemWarehouses.AsNoTracking()
            join w in dbContext.TbWarehouses.AsNoTracking() on i.WarehouseID equals w.WarehouseID
            where i.CompanyID == companyId && i.ItemID == itemId && i.WarehouseID == warehouseId
            select new TbItemWarehouseDto
            {
                CompanyId = i.CompanyID,
                BranchId = i.BranchID,
                WarehouseId = i.WarehouseID,
                ItemId = i.ItemID,
                Quantity = i.Quantity,
                QuantityMax = i.QuantityMax,
                QuantityMin = i.QuantityMin,
                WarehouseName = w.Name
            };
        return result.SingleOrDefault();
    }

    public int WarehouseIsEmpty(int companyId, int warehouseId)
    {
        using var context = new DataContext();
        return context.TbItemWarehouses
            .Count(warehouse => warehouse.CompanyID == companyId
                                && warehouse.WarehouseID == warehouseId
                                && warehouse.Quantity > decimal.Zero);
    }
}