using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IEntityModel
{
    void UpdateAppPosme(int companyId,int  branchId,int entityId,TbEntity data);
    
    int DeleteAppPosme(int companyId,int branchId,int entityId);
    
    int InsertAppPosme(TbEntity data);

    TbEntity GetRowByPk(int companyId, int branchId, int entityId);
    
    TbEntity GetRowByEntity(int companyId,int entityId);
}