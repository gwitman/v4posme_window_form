using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IEntityEmailModel
{
    int DeleteAppPosme(int companyId, int branchId, int entityId, int entityEmailId);

    int DeleteByEntity(int companyId, int branchId, int entityId);

    long InsertAppPosme(TbEntityEmail data);

    void UpdateAppPosme(int companyId, int branchId, int entityId, int entityEmailId, TbEntityEmail data);

    TbEntityEmail GetRowByPk(int companyId, int branchId, int entityId, int entityEmailId);
    
    List<TbEntityEmail> GetRowByEntity(int companyId,int branchId,int entityId);
    
    List<TbEntityEmail> GetRowByCompany(int companyId);
}