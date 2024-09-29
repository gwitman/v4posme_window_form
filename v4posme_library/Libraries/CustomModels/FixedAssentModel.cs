using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class FixedAssentModel : IFixedAssentModel
{
    public void UpdateAppPosme(int companyId, int branchId, int fixedAssentId, TbFixedAssent data)
    {
        using var context = new DataContext();
        var find = context.TbFixedAssents
            .Single(assent => assent.CompanyID == companyId
                              && assent.BranchID == branchId
                              && assent.FixedAssentID == fixedAssentId);
        data.FixedAssentID = find.FixedAssentID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteAppPosme(int companyId, int branchId, int fixedAssentId)
    {
        using var context = new DataContext();
        context.TbFixedAssents
            .Where(assent => assent.CompanyID == companyId
                             && assent.BranchID == branchId
                             && assent.FixedAssentID == fixedAssentId)
            .ExecuteUpdate(calls =>
                calls.SetProperty(assent => assent.IsActive, false));
    }

    public int InsertAppPosme(TbFixedAssent data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.FixedAssentID;
    }

    public TbFixedAssent GetRowByPk(int companyId, int branchId, int fixedAssentId)
    {
        using var context = new DataContext();
        return context.TbFixedAssents
            .Single(assent => assent.CompanyID == companyId
                              && assent.BranchID == branchId
                              && assent.FixedAssentID == fixedAssentId);
    }
}