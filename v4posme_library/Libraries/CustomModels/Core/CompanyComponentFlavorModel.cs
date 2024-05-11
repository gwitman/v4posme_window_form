using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class CompanyComponentFlavorModel : ICompanyComponentFlavorModel
{
    public TbCompanyComponentFlavor? GetRowByCompanyAndComponentAndComponentItemId(int companyId, int componentId,
        int componentItemId)
    {
        using var context = new DataContext();
        return context.TbCompanyComponentFlavors.AsNoTracking()
            .FirstOrDefault(flavor => flavor!.CompanyId == companyId
                             && flavor.ComponentId == componentId
                             && flavor.ComponentItemId == componentItemId);
    }
}