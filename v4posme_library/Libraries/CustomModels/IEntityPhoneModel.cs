using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public interface IEntityPhoneModel
{
    void DeleteAppPosme(int companyId, int branchId, int entityId, int entityPhoneId);

    void DeleteByEntity(int companyId, int branchId, int entityId);

    long InsertAppPosme(TbEntityPhone data);

    void UpdateAppPosme(int companyId, int branchId, int entityId, int entityPhoneId, TbEntityPhone data);

    TbEntityPhoneDto GetRowByPk(int companyId, int branchId, int entityId, int entityPhoneId);
    
    List<TbEntityPhoneDto> GetRowByEntity(int companyId, int branchId, int entityId);
}