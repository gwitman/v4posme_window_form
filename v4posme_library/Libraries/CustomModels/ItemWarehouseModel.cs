using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class ItemWarehouseModel : IItemWarehouseModel
{
    public void DeleteWhereIdNotIn(int companyId, int itemId, List<int> listWarehouseId)
    {
        using var context = new DataContext();
        context.TbItemWarehouses
            .Where(warehouse => warehouse.CompanyId == companyId
                                && warehouse.ItemId == itemId
                                && warehouse.Quantity > 0
                                && !listWarehouseId.Contains(warehouse.WarehouseId))
            .ExecuteDelete();
    }

    public void UpdateAppPosme(int companyId, int itemId, int warehouseId, TbItemWarehouse data)
    {
        using var context = new DataContext();
        var find = context.TbItemWarehouses
            .Where(warehouse => warehouse.CompanyId == companyId
                                && warehouse.ItemId == itemId
                                && warehouse.WarehouseId == warehouseId)
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

    public List<TbItemWarehouse> GetByWarehouse(int companyId, int warehouseId)
    {
        using var context = new DataContext();
        var result = from i in context.TbItems
            join w in context.TbItemWarehouses on i.ItemId equals w.ItemId
            join ci in context.TbCatalogItems on Convert.ToInt32(i.UnitMeasureId) equals ci.CatalogItemId
            where i.CompanyId == companyId && w.WarehouseId == warehouseId && i.IsActive.Value
            select new TbItemWarehouse
            {
                Codigo = i.ItemNumber,
                Producto = i.Name,
                UM = ci.Display,
                ItemId = i.ItemId,
                Quantity = w.Quantity,
                Cost = i.Cost
            };
        return result.ToList();
    }

    public List<TbItemWarehouse> GetRowLowMinimus(int companyId)
    {
        var dbContext = new DataContext();
        var result = from iw in dbContext.TbItemWarehouses
            join w in dbContext.TbWarehouses on iw.WarehouseId equals w.WarehouseId
            join i in dbContext.TbItems on iw.ItemId equals i.ItemId
            where i.CompanyId == companyId
                  && i.IsActive.Value
                  && w.IsActive == 1
                  && iw.Quantity < iw.QuantityMin
            select new TbItemWarehouse
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

    public List<TbItemWarehouse> GetRowByItemId(int companyId, int itemId)
    {
        var dbContext = new DataContext();
        var result = from i in dbContext.TbItemWarehouses
            join w in dbContext.TbWarehouses on i.WarehouseId equals w.WarehouseId
            where i.CompanyId == companyId && i.ItemId == itemId
            select new TbItemWarehouse
            {
                CompanyId = i.CompanyId,
                BranchId = i.BranchId,
                WarehouseId = i.WarehouseId,
                ItemId = i.ItemId,
                Quantity = i.Quantity,
                QuantityMax = i.QuantityMax,
                QuantityMin = i.QuantityMin,
                WarehouseName = w.Name
            };
        return result.ToList();
    }

    public TbItemWarehouse GetByPk(int companyId, int itemId, int warehouseId)
    {
        var dbContext = new DataContext();
        var result = from i in dbContext.TbItemWarehouses
            join w in dbContext.TbWarehouses on i.WarehouseId equals w.WarehouseId
            where i.CompanyId == companyId && i.ItemId == itemId && i.WarehouseId == warehouseId
            select new TbItemWarehouse
            {
                CompanyId = i.CompanyId,
                BranchId = i.BranchId,
                WarehouseId = i.WarehouseId,
                ItemId = i.ItemId,
                Quantity = i.Quantity,
                QuantityMax = i.QuantityMax,
                QuantityMin = i.QuantityMin,
                WarehouseName = w.Name
            };
        return result.Single();
    }

    public int WarehouseIsEmpty(int companyId, int warehouseId)
    {
        using var context = new DataContext();
        return context.TbItemWarehouses
            .Count(warehouse => warehouse.CompanyId == companyId
                                && warehouse.WarehouseId == warehouseId
                                && warehouse.Quantity > decimal.Zero);
    }
}