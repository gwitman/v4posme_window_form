using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface IBranchModel
{
    void DeleteAppPosme(int companyId, int branchId);

    void UpdateAppPosme(int companyId, int branchId, TbBranch data);

    int InsertAppPosme(int companyId, TbBranch data);
    
    TbBranch GetRowByPk(int companyId,int branchId);
    
    List<TbBranch>  GetByCompany(int companyId);
}