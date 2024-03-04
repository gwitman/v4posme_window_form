using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class EntityModel : IEntityModel
{
    public void UpdateAppPosme(int companyId, int branchId, int entityId, TbEntity data)
    {
        using var context = new DataContext();
        var find = context.TbEntities
            .Single(entity => entity.CompanyId == companyId
                              && entity.BranchId == branchId
                              && entity.EntityId == entityId);
        data.EntityId = find.EntityId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int DeleteAppPosme(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        return context.TbEntities
            .Where(entity => entity.CompanyId == companyId
                             && entity.BranchId == branchId
                             && entity.EntityId == entityId)
            .ExecuteDelete();
    }

    public int InsertAppPosme(TbEntity data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.EntityId;
    }

    public TbEntity GetRowByPk(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        return context.TbEntities
            .Single(entity => entity.CompanyId == companyId
                              && entity.BranchId == branchId
                              && entity.EntityId == entityId);
    }

    public TbEntity GetRowByEntity(int companyId, int entityId)
    {
        using var context = new DataContext();
        return context.TbEntities
            .Single(entity => entity.CompanyId == companyId
                              && entity.EntityId == entityId);
    }
}