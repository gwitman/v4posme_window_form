using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IProviderModel
{
    void UpdateAppPosme(int companyId, int branchId, int entityId, TbProvider data);
    
    void DeleteAppPosme(int companyId,int branchId,int entityId);
    
    int InsertAppPosme(TbProvider data);
    
    TbProvider GetRowByEntity(int companyId,int entityId);
    
    List<TbProvider>  GetRowByCompany(int companyId);
    
    TbProvider GetRowByPk(int companyId,int branchId,int entityId);
    
    TbProvider GetRowByProviderNumber(int companyId, string providerNumber);
}