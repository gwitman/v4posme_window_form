using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class CompanyParameterModel(DataContext context) : ICompanyParameterModel
{
    public void UpdateAppPosme(int companyId, int parameterId, TbCompanyParameter data)
    {
        
        var finds = context.TbCompanyParameters
            .FirstOrDefault(parameter => parameter.CompanyId == companyId
                                         && parameter.ParameterId == parameterId);
        if (finds is null) return;
        data.CompanyParameterId = finds.CompanyParameterId;
        context.Entry(finds).CurrentValues.SetValues(data);
        context.BulkSaveChanges();
    }

    public TbCompanyParameter? GetRowByParameterIdCompanyId(int companyId, int parameterId)
    {
        
        return context.TbCompanyParameters.AsNoTracking()
            .FirstOrDefault(parameter => parameter.CompanyId == companyId
                                         && parameter.ParameterId == parameterId);
    }
}