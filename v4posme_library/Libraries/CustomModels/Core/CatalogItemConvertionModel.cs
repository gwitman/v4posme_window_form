using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class CatalogItemConvertionModel : ICatalogItemConvertionModel
{
    public TbCatalogItemConvertion GetDefault(int companyId, int catalogId)
    {
        using var context = new DataContext();
        return context.TbCatalogItemConvertions
            .First(convertion => convertion.CompanyId == companyId
                                 && convertion.CatalogId == catalogId
                                 && convertion.IsActive!.Value
                                 && convertion.Ratio == decimal.One);
    }

    public TbCatalogItemConvertion GetRowByPk(int companyId, int catalogId, int catalogItemIdSource,
        int catalogItemIdTarget)
    {
        using var context = new DataContext();
        return context.TbCatalogItemConvertions
            .First(convertion => convertion.CompanyId == companyId
                                 && convertion.CatalogId == catalogId
                                 && convertion.CatalogItemId == catalogItemIdSource
                                 && convertion.TargetCatalogItemId == catalogItemIdTarget
                                 && convertion.IsActive!.Value);
    }
}