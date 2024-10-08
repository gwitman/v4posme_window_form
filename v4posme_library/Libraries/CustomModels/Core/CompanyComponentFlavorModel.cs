﻿using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class CompanyComponentFlavorModel(DataContext context) : ICompanyComponentFlavorModel
{
    public TbCompanyComponentFlavor? GetRowByCompanyAndComponentAndComponentItemId(int companyId, int componentId,
        int componentItemId)
    {
        return context.TbCompanyComponentFlavors.AsNoTracking()
            .FirstOrDefault(flavor => flavor!.CompanyID == companyId
                             && flavor.ComponentID == componentId
                             && flavor.ComponentItemID == componentItemId);
    }
}