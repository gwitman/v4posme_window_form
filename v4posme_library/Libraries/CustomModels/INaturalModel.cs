using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface INaturalModel
{
    void UpdateAppPosme(int companyId, int branchId, int entityId, TbNaturale data);
    
    void DeleteAppPosme(int companyId,int branchId,int entityId);
    
    int InsertAppPosme(TbNaturale data);
    
    TbNaturale GetRowByPk(int companyId,int branchId,int entityId);
}