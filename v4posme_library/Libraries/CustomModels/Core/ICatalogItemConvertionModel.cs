using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface ICatalogItemConvertionModel
{
    TbCatalogItemConvertion  GetDefault(int companyId,int catalogId);
    
    TbCatalogItemConvertion  GetRowByPk(int companyId,int catalogId,
        int catalogItemIdSource,int catalogItemIdTarget);
}