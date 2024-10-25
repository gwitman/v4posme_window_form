using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class WarehouseModel : IWarehouseModel
{
    public void DeleteAppPosme(int companyId, int warehouseId)
    {
        using var context = new DataContext();
        context.TbWarehouses
            .Where(warehouse => warehouse.CompanyID == companyId
                                && warehouse.WarehouseID == warehouseId)
            .ExecuteUpdate(calls =>
                calls.SetProperty(warehouse => warehouse.IsActive, false));
    }

    public void UpdateAppPosme(int companyId, int branchId, int warehouseId, TbWarehouse data)
    {
        using var context = new DataContext();
        var find = context.TbWarehouses
            .FirstOrDefault(warehouse => warehouse.CompanyID == companyId
                                         && warehouse.BranchID == branchId
                                         && warehouse.WarehouseID == warehouseId);
        if (find is null) return;
        data.WarehouseID = find.WarehouseID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int InsertAppPosme(TbWarehouse data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.WarehouseID;
    }

    public List<TbWarehouse> GetByCode(int companyId, string? code)
    {
        using var context = new DataContext();
        return context.TbWarehouses
            .Where(warehouse => warehouse.CompanyID == companyId
                                && warehouse.IsActive
                                && warehouse.Number == code)
            .ToList();
    }

    public TbWarehouse? GetRowByPk(int companyId, int warehouseId)
    {
        using var context = new DataContext();
        return context.TbWarehouses
            .SingleOrDefault(warehouse => warehouse.CompanyID == companyId
                                && warehouse.IsActive
                                && warehouse.WarehouseID == warehouseId);
    }

    public List<TbWarehouse>? GetByCompany(int companyId)
    {
        using var context = new DataContext();
        return context.TbWarehouses
            .Where(warehouse => warehouse.CompanyID == companyId
                                && warehouse.IsActive)
            .ToList();
    }
}