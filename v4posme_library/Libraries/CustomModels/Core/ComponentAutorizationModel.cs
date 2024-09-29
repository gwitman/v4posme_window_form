using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class ComponentAutorizationModel : IComponentAutorizationModel
{
    public List<TbComponentAutorization> GetRowByCompanyId(int companyId)
    {
        using var context = new DataContext();
        return context.TbComponentAutorizations.AsNoTracking()
            .Where(autorization => autorization.CompanyID == companyId)
            .ToList();
    }
}