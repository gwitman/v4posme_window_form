using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IEntityAccountModel
{
    void UpdateAppPosme(int entityAccountId,TbEntityAccount data);
    
    int InsertAppPosme(TbEntityAccount data);
    
    void DeleteAppPosme(int entityAccountId);
    
    List<TbEntityAccount> GetRowByEntity(int companyId,int componentId,int componentItemId);
    
    TbEntityAccount GetRowByPk(int entityAccountId);
    
    TbEntityAccount GetRowByAccountId(int companyId,int componentId,int componentItemId,int accountId);
}