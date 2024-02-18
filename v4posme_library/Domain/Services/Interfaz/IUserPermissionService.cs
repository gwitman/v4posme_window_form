using System.Collections.Generic;
using v4posme_library.ModelsViews;
namespace v4posme_library.Domain.Services.Interfaz
{
    public interface IUserPermissionService
    {
        List<UserPermissionView> GetRowByCompanyIDyBranchIDyRoleId(int companyId, int branchId, int roleId);
    }
}
