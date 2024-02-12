using DevExpress.Data.Mask.Internal;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Filtering.Templates;
using System;
using System.Windows.Forms;
using Unity;
namespace v4posme_window_form.Domain.Services
{
    public class CoreWebPermission : ICoreWebPermission
    {
        private readonly ICoreWebParameter _coreWebParameter = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebParameter>();
        
        public string GetLicenseMessage(int companyId)
        {
            var getParameter1 = _coreWebParameter.GetParameter("CORE_CUST_PRICE_LICENCES_EXPIRED", companyId);
            var parameterFechaExpiration = getParameter1.Value;
            
            var getParameter2 = _coreWebParameter.GetParameter("CORE_CUST_PRICE_LICENCES_EXPIRED",companyId);
            var objParameterExpiredLicense = getParameter2.Value;
            
            var getParameter3 = _coreWebParameter.GetParameter("CORE_CUST_PRICE_TIPO_PLAN",companyId);
            var objParameterTipoPlan = getParameter3.Value;
            
            var getParameter4 = _coreWebParameter.GetParameter("CORE_CUST_PRICE_TIPO_PLAN",companyId);
            var objParameterCreditosId = getParameter4.ParameterID;
            var objParameterCreditos = getParameter4.Value;

            var getParameter5 = _coreWebParameter.GetParameter("CORE_CUST_PRICE_BY_INVOICE", companyId);
            var objParameterPriceByInvoice = getParameter5.Value;
            
            var fechaNow = System.DateTime.Now.AddDays(7).ToString("yyyy-M-d");
            if (DateTime.Parse(fechaNow) > DateTime.Parse(parameterFechaExpiration))
            {
                //XtraMessageBox.Show("LICENCIA EXPIRA EN 7 DIAS", "Licencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return "LICENCIA EXPIRA EN 7 DIAS";
            }
            //Validar Saldo				
            if(objParameterTipoPlan  == "CONSUMIBLE")
            {
                if(int.Parse(objParameterCreditos)  < (int.Parse(objParameterPriceByInvoice) *  30) )
                {				
                    return "CREDITOS PRONTO VENCERAN";
                }
			
            }
            return "";
        }
    }
}
