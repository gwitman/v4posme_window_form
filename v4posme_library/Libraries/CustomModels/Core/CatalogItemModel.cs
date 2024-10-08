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
            .OrderBy(item => item.Sequence)
            .ToList();
    }

    public List<TbCatalogItem> GetRowByCatalogIdAndFlavorIdParent(int catalogId, int flavorId, int parentCatalogItemId)
    {
        using var context = new DataContext();
        return context.TbCatalogItems.AsNoTracking()
            .Where(item => item.CatalogID == catalogId
                           && item.FlavorID == flavorId
                           && item.ParentCatalogItemID == parentCatalogItemId)
            .OrderBy(item => item.Sequence)
            .ToList();
    }

    public TbCatalogItem? GetRowByCatalogItemId(int catalogItemId)
    {
        using var context = new DataContext();
        return context.TbCatalogItems.AsNoTracking()
            .FirstOrDefault(item => item.CatalogItemID == catalogItemId);
    }
}