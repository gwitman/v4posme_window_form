using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class EntityModel : IEntityModel
{
    public void UpdateAppPosme(int companyId, int branchId, int entityId, TbEntity data)
    {
        using var context = new DataContext();
        var find = context.TbEntities
            .Single(entity => entity.CompanyID == companyId
                              && entity.BranchID == branchId
                              && entity.EntityID == entityId);
        data.EntityID = find.EntityID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int DeleteAppPosme(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        return context.TbEntities
            .Where(entity => entity.CompanyID == companyId
                             && entity.BranchID == branchId
                             && entity.EntityID == entityId)
            .ExecuteDelete();
    }

    public int InsertAppPosme(TbEntity data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.EntityID;
    }

    public TbEntity GetRowByPk(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        return context.TbEntities
            .Single(entity => entity.CompanyID == companyId
                              && entity.BranchID == branchId
                              && entity.EntityID == entityId);
    }

    public TbEntity GetRowByEntity(int companyId, int entityId)
    {
        using var context = new DataContext();
        return context.TbEntities
            .Single(entity => entity.CompanyID == companyId
                              && entity.EntityID == entityId);
    }
}