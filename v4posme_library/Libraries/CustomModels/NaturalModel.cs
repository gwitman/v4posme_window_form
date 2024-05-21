using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class NaturalModel : INaturalModel
{
    public void UpdateAppPosme(int companyId, int branchId, int entityId, TbNaturale data)
    {
        using var context = new DataContext();
        var find = context.TbNaturales
            .Single(naturale => naturale.CompanyId == companyId
                                && naturale.BranchId == branchId
                                && naturale.EntityId == entityId);
        data.NaturalesId = find.NaturalesId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteAppPosme(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        context.TbNaturales
            .Where(naturale => naturale.CompanyId == companyId
                               && naturale.BranchId == branchId
                               && naturale.EntityId == entityId)
            .ExecuteUpdate(calls => calls.SetProperty(naturale => naturale.IsActive, false));
    }

    public int InsertAppPosme(TbNaturale data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.NaturalesId;
    }

    public TbNaturale? GetRowByPk(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        return context.TbNaturales
            .FirstOrDefault(naturale => naturale.CompanyId == companyId
                                && naturale.BranchId == branchId
                                && naturale.EntityId == entityId
                                && naturale.IsActive!.Value);
    }
}