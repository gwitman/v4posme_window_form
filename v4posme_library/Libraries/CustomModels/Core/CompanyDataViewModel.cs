using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class CompanyDataViewModel : ICompanyDataViewModel
{
    public TbCompanyDataview? GetRowByCompanyIdDataViewId(int companyId, int dataViewId, int callerId, int componentId)
    {
        using var context = new DataContext();
        return context.TbCompanyDataviews.AsNoTracking()
            .FirstOrDefault(datafile => datafile!.CompanyID == companyId
                                         && datafile.DataViewID == dataViewId
                                         && datafile.CallerID == callerId
                                         && datafile.ComponentID == componentId
                                         && datafile.IsActive!.Value);
    }

    public TbCompanyDataview? GetRowByCompanyIdDataViewIdAndFlavor(int companyId, int dataViewId, int callerId,
        int componentId, int flavorId)
    {
        using var context = new DataContext();
        return context.TbCompanyDataviews.AsNoTracking()
            .SingleOrDefault(datafile => datafile!.CompanyID == companyId
                                         && datafile.DataViewID == dataViewId
                                         && datafile.CallerID == callerId
                                         && datafile.ComponentID == componentId
                                         && datafile.FlavorID == flavorId
                                         && datafile.IsActive!.Value);
    }
}