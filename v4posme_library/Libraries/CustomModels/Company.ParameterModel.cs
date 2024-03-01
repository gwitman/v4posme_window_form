using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public class CompanyParameterModel : ICompanyParameterModel
{
    public TbCompanyParameter? GetRowByParameterIdCompanyId(int companyId, int parameterId)
    {
        using var context = new DataContext();
        return context.TbCompanyParameters
            .Single(parameter =>
                parameter.CompanyId == companyId && parameter.ParameterId == parameterId);
    }

    public void UpdateAppPosme(int companyId, int parameterId, TbCompanyParameter data)
    {
        using var context = new DataContext();
        var find = context.TbCompanyParameters
            .Single(parameter =>
                parameter.CompanyId == companyId
                && parameter.ParameterId == parameterId);
        data.CompanyParameterId = find.CompanyParameterId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }
}