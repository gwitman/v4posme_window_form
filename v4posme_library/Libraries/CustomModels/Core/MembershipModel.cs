using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class MembershipModel : IMembershipModel
{
    public int DeleteAppPosme(int companyId, int branchId, int userId)
    {
        using var context = new DataContext();
        return context.TbMemberships.AsNoTracking()
            .Where(membership => membership.CompanyID == companyId
                                 && membership.BranchID == branchId
                                 && membership.UserID == userId)
            .ExecuteDelete();
    }

    public int InsertAppPosme(TbMembership data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.MembershipID;
    }

    public TbMembership? GetRowByCompanyIdBranchIdUserId(int companyId, int branchId, int userId)
    {
        using var context = new DataContext();
        return context.TbMemberships.AsNoTracking()
            .SingleOrDefault(membership => membership!.CompanyID == companyId
                                 && membership.BranchID == branchId
                                 && membership.UserID == userId);
    }
}