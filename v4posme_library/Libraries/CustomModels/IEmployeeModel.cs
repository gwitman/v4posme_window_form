using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IEmployeeModel
{
    void UpdateAppPosme(int companyId,int branchId,int entityId,TbEmployee data);
    
    void DeleteAppPosme(int companyId,int branchId,int entityId);
    
    int InsertAppPosme(TbEmployee data);

    List<TbEmployee> GetRowByBranchIdAndType(int companyId, int branchId, int typeEmployer);
    
    List<TbEmployee> GetRowByBranchId(int companyId,int branchId);
    
    TbEmployee GetRowByPk(int companyId,int branchId,int entityId);
    
    TbEmployee GetRowByEntityId(int companyId,int entityId);
}