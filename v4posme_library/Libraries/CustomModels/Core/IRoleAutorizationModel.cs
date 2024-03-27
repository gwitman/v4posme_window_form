using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface IRoleAutorizationModel
{
    int DeleteByRole(int companyId, int branchId, int roleId);

    int DeleteAppPosme(int companyId, int branchId, int roleId, int componentAutorizationId);

    int InsertAppPosme(TbRoleAutorization data);

    List<TbRoleAutorizationDto> GetRowByRoleAutorization(int companyId, int branchId, int roleId);
    
    List<TbRoleAutorizationDto> GetRowByRole(int companyId,int branchId,int roleId);
    
    List<TbRoleAutorizationDto> GetRowByPk(int companyId, int branchId, int roleId, int componentAutorizationId);
}