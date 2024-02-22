using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public class CompanyParameterModel : ICompanyParameterModel
{
    public TbCompanyParameter? GetRowByParameterIdCompanyId(int companyId, int parameterId)
    {
        using var context = new DataContext();
        return context.TbCompanyParameters.SingleOrDefault(parameter=>parameter != null && parameter.CompanyId == companyId && parameter.ParameterId == parameterId);
    }

    public void UpdateAppPosme(int companyId, int parameterId, TbCompanyParameter data)
    {
        using (var context = new DataContext())
        {
            var find = context.TbCompanyParameters.SingleOrDefault(parameter=>parameter != null && parameter.CompanyId == companyId && parameter.ParameterId == parameterId);
            if (find != null)
            {
                context.Entry(find).CurrentValues.SetValues(data);
                context.SaveChanges();
            }
        }
    }
}
