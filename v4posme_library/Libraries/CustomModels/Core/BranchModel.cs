using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class BranchModel : IBranchModel
{
    public void DeleteAppPosme(int companyId, int branchId)
    {
        using var context = new DataContext();
        context.TbBranches.AsNoTracking()
            .Where(branch => branch.CompanyID == companyId
                             && branch.BranchID == branchId)
            .ExecuteUpdate(calls =>
                calls.SetProperty(branch => branch.IsActive, false));
    }

    public void UpdateAppPosme(int companyId, int branchId, TbBranch data)
    {
        using var context = new DataContext();
        var find = context.TbBranches.AsNoTracking()
            .FirstOrDefault(branch => branch.CompanyID == companyId
                                      && branch.BranchID == branchId);
        if (find is null) return;
        data.BranchID = find.BranchID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int InsertAppPosme(int companyId, TbBranch data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.BranchID;
    }

    public TbBranch GetRowByPk(int companyId, int branchId)
    {
        using var context = new DataContext();
        return context.TbBranches .AsNoTracking()
            .First(branch => branch.BranchID == branchId
                             && branch.CompanyID == companyId
                             && branch.IsActive!.Value);
    }

    public List<TbBranch> GetByCompany(int companyId)
    {
        using var context = new DataContext();
        return context.TbBranches.AsNoTracking()
            .Where(branch => branch.CompanyID == companyId
                             && branch.IsActive!.Value)
            .ToList();
    }
}