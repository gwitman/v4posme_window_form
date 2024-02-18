using DevExpress.Xpo;
using System.Linq;
using v4posme_library.ModelsCode;
namespace v4posme_library.Domain.Services
{
    public class RoleService : IRoleService
    {
        public Role GetRowByPk(int companyId, int branchId, int roleId)
        {
            if (companyId == 0) { return null; }
            if (branchId == 0) { return null; }
            if (roleId == 0) { return null; }
            var query = Session.DefaultSession.Query<Role>();
            return query.First(r=>r.CompanyID == companyId && r.BranchID == branchId && r.RoleID == roleId && r.IsActive);
            //throw new NotImplementedException();
        }
    }
}
