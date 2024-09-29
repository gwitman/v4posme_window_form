using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Interfaz;

public interface ICoreWebCatalog
{
    List<TbCatalogItem> GetCatalogAllItem(string? table, string? field, int companyId);

    List<TbCatalogItem> GetCatalogAllItemByNameCatalogo(string? name, int companyId);

    List<TbCatalogItem> GetCatalogAllItemParent(string? table, string? field, int companyId, int parentCatalogItemId);
    
    TbCatalogItem GetCatalogItem(string? table,string? field,int companyId,int catalogItemId);
}