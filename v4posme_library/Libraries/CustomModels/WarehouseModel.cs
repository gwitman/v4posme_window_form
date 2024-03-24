using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class WarehouseModel : IWarehouseModel
{
    public void DeleteAppPosme(int companyId, int warehouseId)
    {
        using var context = new DataContext();
        context.TbWarehouses
            .Where(warehouse => warehouse.CompanyId == companyId
                                && warehouse.WarehouseId == warehouseId)
            .ExecuteUpdate(calls =>
                calls.SetProperty(warehouse => warehouse.IsActive, (ulong)0));
    }

    public void UpdateAppPosme(int companyId, int branchId, int warehouseId, TbWarehouse data)
    {
        using var context = new DataContext();
        var find = context.TbWarehouses
            .FirstOrDefault(warehouse => warehouse.CompanyId == companyId
                                         && warehouse.BranchId == branchId
                                         && warehouse.WarehouseId == warehouseId);
        if (find is null) return;
        data.WarehouseId = find.WarehouseId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int InsertAppPosme(TbWarehouse data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.WarehouseId;
    }

    public List<TbWarehouse> GetByCode(int companyId, string code)
    {
        using var context = new DataContext();
        return context.TbWarehouses
            .Where(warehouse => warehouse.CompanyId == companyId
                                && warehouse.IsActive == 1
                                && warehouse.Number == code)
            .ToList();
    }

    public TbWarehouse GetRowByPk(int companyId, int warehouseId)
    {
        using var context = new DataContext();
        return context.TbWarehouses
            .First(warehouse => warehouse.CompanyId == companyId
                                && warehouse.IsActive == 1
                                && warehouse.WarehouseId == warehouseId);
    }

    public List<TbWarehouse> GetByCompany(int companyId)
    {
        using var context = new DataContext();
        return context.TbWarehouses
            .Where(warehouse => warehouse.CompanyId == companyId
                                && warehouse.IsActive == 1)
            .ToList();
    }
}