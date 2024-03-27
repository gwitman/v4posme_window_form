using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels.Core
{
    public interface IUserPermissionModel
    {
        int InsertAppPosme(TbUserPermission data);
        
        int DeleteByRole(int companyId,int branchId,int roleId);

        List<TbUserPermissionDto> GetRowByCompanyIdyBranchIdyRoleId(int companyId, int branchId, int roleId);
        
        TbUserPermission get_rowByPK(int companyId,int branchId,int roleId,int elementId);
    }
}