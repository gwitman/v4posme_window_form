using v4posme_library.Models;
namespace v4posme_library.Libraries.CustomModels;

public interface IPublicCatalogModel
{
    List<TbPublicCatalog> GetBySystemNameAndFlavorID(string systemName,int flavorID);
}

