using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface ILegalModel
{
    void UpdateAppPosme(int companyId,int branchId,int entityId,TbLegal data);
    
    void DeleteAppPosme(int companyId,int branchId,int entityId);
    
    int InsertAppPosme(TbLegal data);
    
    TbLegal get_rowByPK(int companyId,int branchId,int entityId);
}