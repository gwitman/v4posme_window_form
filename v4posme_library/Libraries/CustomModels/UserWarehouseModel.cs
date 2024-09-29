using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class UserWarehouseModel : IUserWarehouseModel
{
    public int DeleteByUser(int companyId, int userId)
    {
        using var context = new DataContext();
        return context.TbUserWarehouses
            .Where(warehouse => warehouse.CompanyID == companyId
                                && warehouse.UserID == userId)
            .ExecuteDelete();
    }

    public int insert_app_posme(TbUserWarehouse data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.UserWarehouseID;
    }

    public List<TbUserWarehouseDto> GetRowByUserIdAndFacturable(int companyId, int userId)
    {
        using var context = new DataContext();
        var result = from uw in context.TbUserWarehouses
            join w in context.TbWarehouses on uw.WarehouseID equals w.WarehouseID
            where uw.CompanyID == companyId
                  && uw.UserID == userId
                  && w.IsActive
                  && w.TypeWarehouse == 480 // Tipo despacho
            select new TbUserWarehouseDto
            {
                CompanyId = uw.CompanyID,
                WarehouseId = uw.WarehouseID,
                BranchId = uw.BranchID,
                UserId = uw.UserID,
                Number = w.Number,
                Name = w.Name,
                StatusId = w.StatusID,
                IsActive = w.IsActive,
                TypeWarehouse = w.TypeWarehouse
            };
        return result.ToList();
    }

    public List<TbUserWarehouseDto> GetRowByUserId(int companyId, int userId)
    {
        using var context = new DataContext();
        var result = from uw in context.TbUserWarehouses
            join w in context.TbWarehouses on uw.WarehouseID equals w.WarehouseID
            where uw.CompanyID == companyId
                  && uw.UserID == userId
                  && w.IsActive
            select new TbUserWarehouseDto
            {
                CompanyId = uw.CompanyID,
                WarehouseId = uw.WarehouseID,
                BranchId = uw.BranchID,
                UserId = uw.UserID,
                Number = w.Number,
                Name = w.Name,
                StatusId = w.StatusID,
                IsActive = w.IsActive,
                TypeWarehouse = w.TypeWarehouse
            };
        return result.ToList();
    }

    public List<TbUserWarehouseDto> GetRowByBranchId(int companyId, int branchId)
    {
        using var context = new DataContext();
        var result = from uw in context.TbUserWarehouses
            join w in context.TbWarehouses on uw.WarehouseID equals w.WarehouseID
            where uw.CompanyID == companyId
                  && uw.BranchID == branchId
                  && w.IsActive
            select new TbUserWarehouseDto
            {
                CompanyId = uw.CompanyID,
                WarehouseId = uw.WarehouseID,
                BranchId = uw.BranchID,
                UserId = uw.UserID,
                Number = w.Number,
                Name = w.Name,
                StatusId = w.StatusID,
                IsActive = w.IsActive,
                TypeWarehouse = w.TypeWarehouse
            };
        return result.ToList();
    }

    public List<TbWarehouse> GetRowByCompanyId(int companyId)
    {
        using var context = new DataContext();
        return context.TbWarehouses
            .Where(warehouse => warehouse.CompanyID == companyId
                                && warehouse.IsActive)
            .ToList();
    }
}