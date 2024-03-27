using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public interface IRememberModel
{
    void DeleteAppPosme(int rememberId);

    void UpdateAppPosme(int rememberId, TbRemember data);

    int InsertAppPosme(TbRemember data);

    TbRemember GetRowByPk(int rememberId);

    List<TbRemember> GetByCompany(int companyId);

    List<TbRemember> GetNotificationCompanyByTagId(int companyId, int tagId);

    List<TbRemember> GetNotificationCompany(int companyId);

    TbRememberDto GetProcessNotification(int rememberId, DateTime fechaProcess);
}