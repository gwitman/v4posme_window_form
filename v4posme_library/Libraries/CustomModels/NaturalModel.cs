using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class NaturalModel : INaturalModel
{
    public void UpdateAppPosme(int companyId, int branchId, int entityId, TbNaturale data)
    {
        using var context = new DataContext();
        var find = context.TbNaturales
            .Single(naturale => naturale.CompanyID == companyId
                                && naturale.BranchID == branchId
                                && naturale.EntityID == entityId);
        data.NaturalesID = find.NaturalesID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteAppPosme(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        context.TbNaturales
            .Where(naturale => naturale.CompanyID == companyId
                               && naturale.BranchID == branchId
                               && naturale.EntityID == entityId)
            .ExecuteUpdate(calls => calls.SetProperty(naturale => naturale.IsActive, false));
    }

    public int InsertAppPosme(TbNaturale data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.NaturalesID;
    }

    public TbNaturale GetRowByPk(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        return context.TbNaturales
            .Single(naturale => naturale.CompanyID == companyId
                                && naturale.BranchID == branchId
                                && naturale.EntityID == entityId
                                && naturale.IsActive!.Value);
    }
}