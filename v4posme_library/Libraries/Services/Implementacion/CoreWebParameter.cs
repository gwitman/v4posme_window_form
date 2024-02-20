using Unity;
using v4posme_library.Libraries.ComponentModels;
using v4posme_library.Libraries.Services.Interfaz;
using v4posme_library.Models;
namespace v4posme_library.Libraries.Services.Implementacion
{
    public class CoreWebParameter : ICoreWebParameter
    {
        private readonly IParameterModel _coreParameterService = VariablesGlobales.Instance.UnityContainer.Resolve<IParameterModel>();
        private readonly CompanyParameterModel _companyParameterModel = new CompanyParameterModel();
        public TbCompanyParameter GetParameter(string parameterName, int companyId)
        {
            var parameter = _coreParameterService.GetRowByName(parameterName);
            var companyParameter = _companyParameterModel.GetRowByParameterIdCompanyId(companyId, parameter.ParameterId);
            return companyParameter!;
        }
    }
}
