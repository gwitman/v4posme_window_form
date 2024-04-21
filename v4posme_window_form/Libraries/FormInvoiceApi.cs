using System.Data;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomLibraries.Interfaz;

namespace v4posme_window.Libraries
{
    public class FormInvoiceApi
    {
        public DataTable? GetViewApi(int componentId,string viewName = "",string filter = "")
        {
            
            var coreWebTools = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();
            var coreWebView = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebView>();
            var usuario = VariablesGlobales.Instance.User;
            var calleridSearch = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["CALLERID_SEARCH"]);

            //Parametros
            var parameter = new Dictionary<string, string>
            {
                ["{companyID}"] = usuario!.CompanyId.ToString()
            };

            // Agregar al diccionarios los parametros dinamicos
            var result = coreWebTools.FormatParameter(filter);
            if (result is not null)
            {
                foreach (var kvp in result)
                {
                    parameter[kvp.Key] = kvp.Value.ToString()!;
                }
            }

            var resultdo = coreWebView.GetViewByName(usuario, componentId, viewName, calleridSearch, null, parameter);
            var viewData = (List<Dictionary<string, object>>)resultdo!.Data!;
            var table = CoreWebRenderInView.FillGridControl(viewData);
            return table;


        }
    }
}
