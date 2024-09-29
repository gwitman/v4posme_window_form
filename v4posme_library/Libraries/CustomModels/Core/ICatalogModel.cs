using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface ICatalogModel
{
    TbCatalog? GetRowByCatalogId(int catalogId);
    
    TbCatalog? GetRowByName(string? name);
}