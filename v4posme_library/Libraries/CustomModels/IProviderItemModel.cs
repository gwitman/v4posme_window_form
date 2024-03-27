using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public interface IProviderItemModel
{
    int DeleteWhereItemId(int companyId,int itemId);

    int DeleteWhereItemIdyProviderId(int companyId, int itemId, int providerId);
    
    int InsertAppPosme(TbProviderItem data);
    
    List<TbProviderItemDto> GetRowByItemId(int companyId, int itemId);
    
    TbProviderItemDto GetByPk(int companyId, int itemId, int providerId);
}