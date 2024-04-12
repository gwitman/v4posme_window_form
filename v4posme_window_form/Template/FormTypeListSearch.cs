using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.Design.ParameterEditor;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries;
using System.Windows.Controls;
using Unity;
using v4posme_window.Libraries;


namespace v4posme_window.Template
{
    public partial class FormTypeListSearch : XtraForm
    {
        private int componentID_ { get; set; }
        private EventHandler fnCollback_ { get; set; }
        private string viewName_ { get; set; }
        private bool autoClose_  { get; set; }
        private string filter_  { get; set; }
        private bool multiSelect_  { get; set; }
        private string urlRedictWhenEmpty_  { get; set; }
        private int iDisplayStart_  { get; set; }
        private int iDisplayLength_  { get; set; }
        private string sSearch_  { get; set; }

        public FormTypeListSearch(int componentID,EventHandler fnCollback,string viewName,bool autoClose,string filter,bool multiSelect,string urlRedictWhenEmpty,int iDisplayStart,int iDisplayLength,string sSearch)
        {
            componentID_ = componentID;
            fnCollback_ = fnCollback;
            viewName_ = viewName;
            autoClose_ = autoClose;
            filter_ = filter;
            multiSelect_ = multiSelect;
            urlRedictWhenEmpty_ = urlRedictWhenEmpty;
            iDisplayStart_ = iDisplayStart;
            iDisplayLength_ = iDisplayLength;
            sSearch_ = sSearch;

            InitializeComponent();

        }

        private void FormTypeListSearch_Load(object sender, EventArgs e)
        {
            this.ShowViewByNamePaginate(  componentID_ ,fnCollback_ ,viewName_ ,autoClose_ ,filter_ ,multiSelect_ ,urlRedictWhenEmpty_ ,iDisplayStart_ ,iDisplayLength_ ,sSearch_ );
        }

        
        public void ShowViewByNamePaginate(int componentID, EventHandler fnCollback, string viewName, bool autoClose, string filter, bool multiSelect, string urlRedictWhenEmpty, int iDisplayStart, int iDisplayLength, string sSearch)
        {
            PanelControl controlParent  = this.centerPane;
            var coreWebTools            = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();
            var coreWebView             = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebView>();
            var calleridSearch          = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["CALLERID_SEARCH"]);
            var coreWebRenderInView     = new CoreWebRenderInView();


            var usuario     = VariablesGlobales.Instance.User;


            // Crear un diccionario para los parámetros staticos
            var parameter   = new Dictionary<string, string>
            {
                
                ["{companyID}"]         = usuario!.CompanyId.ToString(),
                ["{componentID}"]       = componentID.ToString(),                
                ["{iDisplayStart}"]     = iDisplayStart.ToString(),
                ["{iDisplayStartDB}"]   = iDisplayStart.ToString() == "1" ? "0" : ((iDisplayStart - 1) * iDisplayLength).ToString(),
                ["{iDisplayLength}"]    = iDisplayLength.ToString(),
                ["{sSearchDB}"]         = sSearch,
                ["{sSearch}"]           = sSearch
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




            var datos           = coreWebView.GetViewByName(usuario, componentID, viewName, calleridSearch, null, parameter);
            var datosTotales    = coreWebView.GetViewByName(usuario, componentID, viewName + "_TOTAL", calleridSearch, null, parameter);
            var datosDisplay    = coreWebView.GetViewByName(usuario, componentID, viewName + "_DISPLAY", calleridSearch, null, parameter);
            

            if (datos is null)
                return;
            

            //aqui vamos a validar si seleccion multiple
            var objDataViewRender = coreWebRenderInView.RenderGrid(datos, "ListView",iDisplayLength, controlParent);

            
           
        }
        
    }
}