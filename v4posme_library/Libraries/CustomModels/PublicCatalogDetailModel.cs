using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class PublicCatalogDetailModel : IPublicCatalogDetailModel
{
    public List<TbPublicCatalogDetail> GetRowByCatalogIDAndName(int publicCatalogID, string name)
    {
        //el campo IsActive se recuperó como string dado q es varchar, revisar si es así
        using var context = new DataContext();
        return context.TbPublicCatalogDetails
            .Where(detail => detail.PublicCatalogId == publicCatalogID
                             && detail.IsActive!.Contains("1") 
                             && detail.Name!.Contains(name) )
            .ToList();
    }

    public List<TbPublicCatalogDetail> GetView(int publicCatalogId)
    {
        //el campo IsActive se recuperó como string dado q es varchar, revisar si es así
        using var context = new DataContext();
        return context.TbPublicCatalogDetails
            .Where(detail => detail.PublicCatalogId == publicCatalogId
                             && detail.IsActive!.Contains("true"))
            .ToList();
    }
}