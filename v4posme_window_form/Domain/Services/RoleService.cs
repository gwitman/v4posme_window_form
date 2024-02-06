using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using v4posme_window_form.Models.Tablas;

namespace v4posme_window_form.Domain.Services
{
    public class RoleService : IRoleService
    {
        public Role getRowByPK(int companyId, int branchId, int roleId)
        {
            if(companyId == 0) { return null; }
            if(branchId == 0) { return null; }
            if(roleId == 0) { return null; }
            var query = Session.DefaultSession.Query<Role>();
            var role = query.Where(r=> r.CompanyID==companyId && r.BranchID==branchId&& r.RoleID==roleId && r.IsActive).First();
            return role;
            //throw new NotImplementedException();
        }
    }
}
