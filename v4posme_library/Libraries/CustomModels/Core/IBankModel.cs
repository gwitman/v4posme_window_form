using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface IBankModel
{
    void DeleteAppPosme(int companyId, int bankId);

    void UpdateAppPosme(int companyId, int bankId, TbBank data);

    int InsertAppPosme(int companyId, TbBank data);
    
    TbBank GetRowByPk(int companyId,int bankId);
    
    List<TbBank>? GetByCompany(int companyId);
}