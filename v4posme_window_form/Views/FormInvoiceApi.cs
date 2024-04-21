using DevExpress.XtraReports.Design.ParameterEditor;
using DevExpress.XtraTreeList.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries;
using Unity;
using DevExpress.XtraGrid;
using v4posme_window.Libraries;
using System.Data;

namespace v4posme_window.Views
{
    public class FormInvoiceApi
    {
        public DataTable? getViewApi(int componentId,string viewName = "",string filter = "")
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
