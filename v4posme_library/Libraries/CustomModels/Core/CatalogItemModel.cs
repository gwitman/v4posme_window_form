using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class CatalogItemModel : ICatalogItemModel
{
    public List<TbCatalogItem> GetRowByCatalogIdAndFlavorId(int catalogId, int flavorId)
    {
        using var context = new DataContext();
        return context.TbCatalogItems.AsNoTracking()
            .Where(item => item.CatalogID == catalogId
                           && item.FlavorID == flavorId)
            .ToList();
    }

    public List<TbCatalogItem> GetRowByCatalogIdAndFlavorIdParent(int catalogId, int flavorId, int parentCatalogItemId)
    {
        using var context = new DataContext();
        return context.TbCatalogItems.AsNoTracking()
            .Where(item => item.CatalogID == catalogId
                           && item.FlavorID == flavorId
                           && item.ParentCatalogItemID == parentCatalogItemId)
            .ToList();
    }

    public TbCatalogItem GetRowByCatalogItemId(int catalogItemId)
    {
        using var context = new DataContext();
        return context.TbCatalogItems.AsNoTracking()
            .First(item => item.CatalogItemID == catalogItemId);
    }
}