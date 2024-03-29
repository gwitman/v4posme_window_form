using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class CompanyParameterModel : ICompanyParameterModel
{
    public void UpdateAppPosme(int companyId, int parameterId, TbCompanyParameter data)
    {
        using var context = new DataContext();
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
        using var context = new DataContext();
        return context.TbCompanyParameters
            .FirstOrDefault(parameter => parameter.CompanyId == companyId
                                         && parameter.ParameterId == parameterId);
    }
}