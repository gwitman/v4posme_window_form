using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IProviderItemModel
{
    int DeleteWhereItemId(int companyId,int itemId);

    int DeleteWhereItemIdyProviderId(int companyId, int itemId, int providerId);
    
    int InsertAppPosme(TbProviderItem data);
    
    List<TbProviderItem> GetRowByItemId(int companyId,int itemId);
    
    TbProviderItem GetByPk(int companyId,int itemId,int providerId);
}