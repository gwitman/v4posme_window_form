﻿using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class CompanyDataViewModel : ICompanyDataViewModel
{
    public TbCompanyDataview GetRowByCompanyIdDataViewId(int companyId, int dataViewId, int callerId, int componentId)
    {
        using var context = new DataContext();
        return context.TbCompanyDataviews
            .First(datafile => datafile.CompanyId == companyId
                               && datafile.DataViewId == dataViewId
                               && datafile.CallerId == callerId
                               && datafile.ComponentId == componentId);
    }

    public TbCompanyDataview GetRowByCompanyIdDataViewIdAndFlavor(int companyId, int dataViewId, int callerId,
        int componentId, int flavorId)
    {
        using var context = new DataContext();
        return context.TbCompanyDataviews
            .First(datafile => datafile.CompanyId == companyId
                               && datafile.DataViewId == dataViewId
                               && datafile.CallerId == callerId
                               && datafile.ComponentId == componentId
                               && datafile.FlavorId == flavorId
                               && datafile.IsActive!.Value);
    }
}