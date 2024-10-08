using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class CompanyParameterModel(DataContext context) : ICompanyParameterModel
{
    public void UpdateAppPosme(int companyId, int parameterId, TbCompanyParameter data)
    {
        
        var finds = context.TbCompanyParameters
            .FirstOrDefault(parameter => parameter.CompanyID == companyId
                                         && parameter.ParameterID == parameterId);
        if (finds is null) return;
        data.CompanyParameterID = finds.CompanyParameterID;
        context.Entry(finds).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public TbCompanyParameter? GetRowByParameterIdCompanyId(int companyId, int parameterId)
    {
        
        return context.TbCompanyParameters.AsNoTracking()
            .FirstOrDefault(parameter => parameter.CompanyID == companyId
                                         && parameter.ParameterID == parameterId);
    }
}