using DevExpress.Xpo;
using System.Linq;
using v4posme_library.Domain.Services.Interfaz;
using v4posme_library.ModelsCode;
namespace v4posme_library.Domain.Services.Implementacion
{
    internal class MembershipService : IMembershipService
    {
        public Membership GetRowByCompanyIdBranchIdUserId(int companyId, int branchId, int userId)
        {
            if (companyId == 0) { return null; }
            if (branchId == 0) { return null; }
            if (userId == 0) { return null; }
            var query = Session.DefaultSession.Query<Membership>();
            return query.First(membership=>membership.CompanyID == companyId && membership.BranchID == branchId && membership.UserID == userId);
        }
    }
}
