using v4posme_library.Models;
namespace v4posme_library.Libraries.CustomModels
{
    class PublicCatalogModel : IPublicCatalogModel
    {
        public List<TbPublicCatalog> GetBySystemNameAndFlavorID(string systemName, int flavorID)
        {
            using var context = new DataContext();
            return context.TbPublicCatalogs
                .Where(detail => detail!.SystemName!.Contains(systemName)
                                 && detail.IsActive! == 1 
                                 && detail.FlavorId == flavorID
                                 )
                .ToList();
        }
    }
}
