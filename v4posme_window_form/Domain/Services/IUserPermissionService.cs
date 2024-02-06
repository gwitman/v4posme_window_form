using System.Collections.Generic;
using v4posme_window_form.Models;
namespace v4posme_window_form.Domain.Services
{
    public interface IUserPermissionService
    {
        List<UserPermissionView> getRowByCompanyIDyBranchIDyRoleID(int companyId, int branchId, int roleId);
    }
}
