using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class UserWarehouseModel : IUserWarehouseModel
{
    public int DeleteByUser(int companyId, int userId)
    {
        using var context = new DataContext();
        return context.TbUserWarehouses
            .Where(warehouse => warehouse.CompanyId == companyId
                                && warehouse.UserId == userId)
            .ExecuteDelete();
    }

    public int insert_app_posme(TbUserWarehouse data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.UserWarehouseId;
    }

    public List<TbUserWarehouse> GetRowByUserIdAndFacturable(int companyId, int userId)
    {
        using var context = new DataContext();
        var result = from uw in context.TbUserWarehouses
            join w in context.TbWarehouses on uw.WarehouseId equals w.WarehouseId
            where uw.CompanyId == companyId
                  && uw.UserId == userId
                  && w.IsActive == 1
                  && w.TypeWarehouse == 480 // Tipo despacho
            select new TbUserWarehouse
            {
                CompanyId = uw.CompanyId,
                WarehouseId = uw.WarehouseId,
                BranchId = uw.BranchId,
                UserId = uw.UserId,
                Number = w.Number,
                Name = w.Name,
                StatusId = w.StatusId,
                IsActive = w.IsActive,
                TypeWarehouse = w.TypeWarehouse
            };
        return result.ToList();
    }

    public List<TbUserWarehouse> GetRowByUserId(int companyId, int userId)
    {
        using var context = new DataContext();
        var result = from uw in context.TbUserWarehouses
            join w in context.TbWarehouses on uw.WarehouseId equals w.WarehouseId
            where uw.CompanyId == companyId
                  && uw.UserId == userId
                  && w.IsActive == 1
            select new TbUserWarehouse
            {
                CompanyId = uw.CompanyId,
                WarehouseId = uw.WarehouseId,
                BranchId = uw.BranchId,
                UserId = uw.UserId,
                Number = w.Number,
                Name = w.Name,
                StatusId = w.StatusId,
                IsActive = w.IsActive,
                TypeWarehouse = w.TypeWarehouse
            };
        return result.ToList();
    }

    public List<TbUserWarehouse> GetRowByBranchId(int companyId, int branchId)
    {
        using var context = new DataContext();
        var result = from uw in context.TbUserWarehouses
            join w in context.TbWarehouses on uw.WarehouseId equals w.WarehouseId
            where uw.CompanyId == companyId
                  && uw.BranchId == branchId
                  && w.IsActive == 1
            select new TbUserWarehouse
            {
                CompanyId = uw.CompanyId,
                WarehouseId = uw.WarehouseId,
                BranchId = uw.BranchId,
                UserId = uw.UserId,
                Number = w.Number,
                Name = w.Name,
                StatusId = w.StatusId,
                IsActive = w.IsActive,
                TypeWarehouse = w.TypeWarehouse
            };
        return result.ToList();
    }

    public List<TbWarehouse> GetRowByCompanyId(int companyId)
    {
        using var context = new DataContext();
        return context.TbWarehouses
            .Where(warehouse => warehouse.CompanyId == companyId
                                && warehouse.IsActive == 1)
            .ToList();
    }
}