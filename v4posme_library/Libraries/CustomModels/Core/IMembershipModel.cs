using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface IMembershipModel
{
    int DeleteAppPosme(int companyId,int branchId,int userId);
    
    int InsertAppPosme(TbMembership data);
    
    TbMembership? GetRowByCompanyIdBranchIdUserId(int companyId,int branchId,int userId);
}