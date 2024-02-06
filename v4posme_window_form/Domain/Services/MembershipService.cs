using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using v4posme_window_form.Models.Tablas;

namespace v4posme_window_form.Domain.Services
{
    internal class MembershipService : IMembershipService
    {
        public Membership getRowByCompanyIDBranchIDUserID(int companyId, int branchId, int userId)
        {
            if(companyId == 0) { return null; }
            if(branchId == 0) { return null;  }
            if(userId == 0) { return null; }
            var query = Session.DefaultSession.Query<Membership>();
            var membership = query.Where(m => m.CompanyID == companyId && m.BranchID==branchId && m.UserID == userId).First();
            return membership;
            //throw new NotImplementedException();
        }
    }
}
