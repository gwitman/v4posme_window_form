using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class CompanyDefaultDataViewModel : ICompanyDefaultDataViewModel
{
    public TbCompanyDefaultDataview? GetRowByCcct(int companyId, int componentId, int callerId, int targetComponentId)
    {
        using var context = new DataContext();
        return context.TbCompanyDefaultDataviews.AsNoTracking()
            .FirstOrDefault(dataview => dataview!.CompanyId == companyId
                                        && dataview.ComponentId == componentId
                                        && dataview.CallerId == callerId
                                        && dataview.TargetComponentId == targetComponentId);
    }
}