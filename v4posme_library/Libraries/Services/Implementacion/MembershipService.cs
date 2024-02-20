using v4posme_library.Libraries.Services.Interfaz;
using v4posme_library.Models;
namespace v4posme_library.Libraries.Services.Implementacion
{
    internal class MembershipService : IMembershipService
    {
        public TbMembership? GetRowByCompanyIdBranchIdUserId(int companyId, int branchId, int userId)
        {
            if (companyId == 0) { return null!; }
            if (branchId == 0) { return null!; }
            if (userId == 0) { return null!; }
            using (var context = new DataContext())
            {
                return context.TbMemberships.First(membership=>membership.CompanyId == companyId && membership.BranchId == branchId && membership.UserId == userId);
            }
        }
    }
}
