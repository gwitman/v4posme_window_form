using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class MembershipModel : IMembershipModel
{
    public int DeleteAppPosme(int companyId, int branchId, int userId)
    {
        using var context = new DataContext();
        return context.TbMemberships.AsNoTracking()
            .Where(membership => membership.CompanyId == companyId
                                 && membership.BranchId == branchId
                                 && membership.UserId == userId)
            .ExecuteDelete();
    }

    public int InsertAppPosme(TbMembership data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.MembershipId;
    }

    public TbMembership? GetRowByCompanyIdBranchIdUserId(int companyId, int branchId, int userId)
    {
        using var context = new DataContext();
        return context.TbMemberships.AsNoTracking()
            .SingleOrDefault(membership => membership!.CompanyId == companyId
                                 && membership.BranchId == branchId
                                 && membership.UserId == userId);
    }
}