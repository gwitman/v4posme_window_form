using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IEntityPhoneModel
{
    void DeleteAppPosme(int companyId, int branchId, int entityId, int entityPhoneId);

    void DeleteByEntity(int companyId, int branchId, int entityId);

    long InsertAppPosme(TbEntityPhone data);

    void UpdateAppPosme(int companyId, int branchId, int entityId, int entityPhoneId, TbEntityPhone data);

    TbEntityPhone GetRowByPk(int companyId, int branchId, int entityId, int entityPhoneId);
    
    List<TbEntityPhone> GetRowByEntity(int companyId,int branchId,int entityId);
}