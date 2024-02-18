using Unity;
using v4posme_library.Domain.Services.Interfaz;
using v4posme_library.ModelsCode;

namespace v4posme_library.Domain.Services.Implementacion
{
    public class CoreWebParameter : ICoreWebParameter
    {
        private readonly ICompanyParameterService _companyParameterService = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyParameterService>();
        private readonly ICoreParameterService _coreParameterService = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreParameterService>();

        public CompanyParameter GetParameter(string parameterName, int companyId)
        {
            var parameter = _coreParameterService.GetRowByName(parameterName);
            if (parameter == null)
            {
                //XtraMessageBox.Show($"No existe el parametro {parameterName}", "Parametro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            var companyParameter = _companyParameterService.GetRowByParameterIdCompanyId(companyId, parameter.ParameterID);
            if (companyParameter == null)
            {
                //XtraMessageBox.Show($"No existe el parametro {parameterName} para la COMPANY {companyId}", "Parametro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return companyParameter;
        }
    }
}
