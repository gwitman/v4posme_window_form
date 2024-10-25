using System.Xml.Linq;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IPublicCatalogDetailModel
{
    List<TbPublicCatalogDetail>? GetView(int publicCatalogId);

    List<TbPublicCatalogDetail> GetRowByCatalogIDAndName(int publicCatalogID, string name);
}