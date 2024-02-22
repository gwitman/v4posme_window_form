using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IAccountLevelModel
{
    void DeleteAppPosme(int companyId, int accountLevelId);
    
    void UpdateAppPosme(int companyId,int accountLevelId,TbAccountLevel data);

    int InsertAppPosme(TbAccountLevel data);

    int GetCountInAccount(int companyId, int accountLevelId);

    List<TbAccountLevel> GetByCompany(int companyId);

    TbAccountLevel GetRowByPk(int companyId, int accountLevelId);
}