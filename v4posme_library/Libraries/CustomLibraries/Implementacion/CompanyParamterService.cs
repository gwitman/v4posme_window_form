using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion
{
    public class CompanyParamterService
    {

        public TbCompanyParameter? GetRowByParameterIdCompanyId(int companyId, int parameterId)
        {
            using var context = new DataContext();
            return context.TbCompanyParameters.First(parameter=>parameter.CompanyId == companyId && parameter.ParameterId == parameterId);
        }
    }
}
