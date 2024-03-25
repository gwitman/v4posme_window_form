using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core
{
    public interface IRoleModel
    {
        int UpdateAppPosme(int companyId,int branchId,int roleId,TbRole obj);
        
        int InsertAppPosme(TbRole obj);
        
        List<TbRole> GetRowByCompanyIDyBranchId(int companyId,int branchId);
        
        TbRole? GetRowByPk(int companyId, int branchId, int roleId);
    }
}
