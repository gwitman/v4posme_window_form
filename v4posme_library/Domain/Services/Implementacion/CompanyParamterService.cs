using v4posme_library.Domain.Services.Interfaz;
using v4posme_library.Models;
namespace v4posme_library.Domain.Services.Implementacion
{
    public class CompanyParamterService : ICompanyParameterService
    {

        public TbCompanyParameter GetRowByParameterIdCompanyId(int companyId, int parameterId)
        {
            using var context = new DataContext();
            return context.TbCompanyParameters.First(parameter=>parameter.CompanyId == companyId && parameter.ParameterId == parameterId);
        }
    }
}
