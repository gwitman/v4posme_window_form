using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class CompanyModel : ICompanyModel
{
    public TbCompany GetRowByPk(int companyId)
    {
        using var context = new DataContext();
        return context.TbCompanies.AsNoTracking()
            .First(company => company.CompanyId == companyId
                              && company.IsActive!.Value);
    }

    public List<TbCompany> GetRows()
    {
        using var context = new DataContext();
        return context.TbCompanies.AsNoTracking()
            .Where(company => company.IsActive!.Value)
            .ToList();
    }

    public List<TbCompany> FnMergeGetRowsDbPosMeMergeRowByCompanyId(int companyId)
    {
        using var context = new DataContext();
        return context.TbCompanies.AsNoTracking().ToList();
    }

    public int FnMergeUpdateRowsDbPosMe(TbCompany data)
    {
        //no implementar
        return 0;
    }
}