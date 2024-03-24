using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class CatalogItemModel : ICatalogItemModel
{
    public List<TbCatalogItem> GetRowByCatalogIdAndFlavorId(int catalogId, int flavorId)
    {
        using var context = new DataContext();
        return context.TbCatalogItems
            .Where(item => item.CatalogId == catalogId
                           && item.FlavorId == flavorId)
            .ToList();
    }

    public List<TbCatalogItem> GetRowByCatalogIdAndFlavorIdParent(int catalogId, int flavorId, int parentCatalogItemId)
    {
        using var context = new DataContext();
        return context.TbCatalogItems
            .Where(item => item.CatalogId == catalogId
                           && item.FlavorId == flavorId
                           && item.ParentCatalogItemId == parentCatalogItemId)
            .ToList();
    }

    public TbCatalogItem GetRowByCatalogItemId(int catalogItemId)
    {
        using var context = new DataContext();
        return context.TbCatalogItems
            .First(item => item.CatalogItemId == catalogItemId);
    }
}