using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class CompanyComponentModel : ICompanyComponentModel
{
    public TbCompanyComponent GetRowByPk(int companyId, int componentId)
    {
        using var context = new DataContext();
        return context.TbCompanyComponents.AsNoTracking()
            .Single(component => component.CompanyId == companyId
                                 && component.ComponentId == componentId);
    }
}