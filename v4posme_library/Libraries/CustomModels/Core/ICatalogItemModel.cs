using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface ICatalogItemModel
{
    List<TbCatalogItem> GetRowByCatalogIdAndFlavorId(int catalogId,int flavorId);
    
    List<TbCatalogItem> GetRowByCatalogIdAndFlavorIdParent(int catalogId,int flavorId,int parentCatalogItemId);
    
    TbCatalogItem? GetRowByCatalogItemId(int catalogItemId);
}