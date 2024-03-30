using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion
{
    public class CoreWebPermission : ICoreWebPermission
    {
        private readonly ICoreWebParameter _coreWebParameter = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebParameter>();

        public string? GetLicenseMessage(int companyId)
        {
            var getParameter1 = _coreWebParameter.GetParameter("CORE_CUST_PRICE_LICENCES_EXPIRED", companyId);
            var parameterFechaExpiration = getParameter1.Value;

            var getParameter2 = _coreWebParameter.GetParameter("CORE_CUST_PRICE_LICENCES_EXPIRED", companyId);
            var objParameterExpiredLicense = getParameter2.Value;

            var getParameter3 = _coreWebParameter.GetParameter("CORE_CUST_PRICE_TIPO_PLAN", companyId);
            var objParameterTipoPlan = getParameter3.Value;

            var getParameter4 = _coreWebParameter.GetParameter("CORE_CUST_PRICE_TIPO_PLAN", companyId);
            var objParameterCreditosId = getParameter4.ParameterId;
            var objParameterCreditos = getParameter4.Value;

            var getParameter5 = _coreWebParameter.GetParameter("CORE_CUST_PRICE_BY_INVOICE", companyId);
            var objParameterPriceByInvoice = getParameter5.Value;

            var fechaNow = DateTime.Now.AddDays(7).ToString("yyyy-M-d");
            if (DateTime.Parse(fechaNow) > DateTime.Parse(parameterFechaExpiration))
            {
                //XtraMessageBox.Show("LICENCIA EXPIRA EN 7 DIAS", "Licencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return "LICENCIA EXPIRA EN 7 DIAS";
            }
            //Validar Saldo				
            if (objParameterTipoPlan == "CONSUMIBLE")
            {
                if (int.Parse(objParameterCreditos) < (int.Parse(objParameterPriceByInvoice) * 30))
                {
                    return "CREDITOS PRONTO VENCERAN";
                }
                 
            }
            return "";
        }
    }
}
