using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class CatalogModel : ICatalogModel
{
    public TbCatalog? GetRowByCatalogId(int catalogId)
    {
        using var context = new DataContext();
        return context.TbCatalogs.AsNoTracking()
            .FirstOrDefault(catalog => catalog != null 
                                       && catalog.CatalogId == catalogId 
                                       && catalog.IsActive!.Value);
    }

    public TbCatalog? GetRowByName(string name)
    {
        using var context = new DataContext();
        return context.TbCatalogs.AsNoTracking()
            .First(catalog => catalog.Name == name && catalog.IsActive!.Value);
    }
}