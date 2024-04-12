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
using K4os.Hash.xxHash;
using GridView = DevExpress.XtraGrid.Views.Grid.GridView;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraExport.Helpers;

namespace v4posme_window.Template
{
    public partial class FormTypeListSearch : XtraForm
    {
        // Declarar un delegado que represente la firma del método que quieres llamar en el formulario padre
        public delegate void EventoCallBackAceptar(string mensaje);

        // Declarar un evento que se disparará cuando ocurra el evento en el formulario hijo
        public event EventoCallBackAceptar EventoCallBackAceptar_;

        private int componentID_ { get; set; }        
        private string viewName_ { get; set; }
        private bool autoClose_ { get; set; }
        private string filter_ { get; set; }
        private bool multiSelect_ { get; set; }
        private string urlRedictWhenEmpty_ { get; set; }
        private int iDisplayStart_ { get; set; }
        private int iDisplayLength_ { get; set; }
        private string sSearch_ { get; set; }
        private GridView objGridView {  get; set; }

        public FormTypeListSearch(int componentID, string viewName, bool autoClose, string filter, bool multiSelect, string urlRedictWhenEmpty, int iDisplayStart, int iDisplayLength, string sSearch)
        {
            componentID_ = componentID;            
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
            this.ShowViewByNamePaginate();
        }


        public void ShowViewByNamePaginate()
        {
            PanelControl controlParent = this.centerPane;            
            int componentID = componentID_;
            string viewName = viewName_;
            bool autoClose = autoClose_;
            string filter = filter_;
            bool multiSelect = multiSelect_;
            string urlRedictWhenEmpty = urlRedictWhenEmpty_;
            int iDisplayStart = iDisplayStart_;
            int iDisplayLength = iDisplayLength_;
            string sSearch = sSearch_;

            var coreWebTools = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();
            var coreWebView = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebView>();
            var calleridSearch = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["CALLERID_SEARCH"]);
            var coreWebRenderInView = new CoreWebRenderInView();


            var usuario = VariablesGlobales.Instance.User;


            // Crear un diccionario para los parámetros staticos
            var parameter = new Dictionary<string, string>
            {

                ["{companyID}"] = usuario!.CompanyId.ToString(),
                ["{componentID}"] = componentID.ToString(),
                ["{iDisplayStart}"] = iDisplayStart.ToString(),
                ["{iDisplayStartDB}"] = "0",
                ["{iDisplayLength}"] = iDisplayLength.ToString(),
                ["{sSearchDB}"] = sSearch,
                ["{sSearch}"] = sSearch
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
            objGridView = coreWebRenderInView.RenderGrid(datos, "ListView", iDisplayLength, controlParent);
            objGridView.KeyDown += GridView_KeyDown;
            objGridView.OptionsView.ShowGroupPanel  = false;
            objGridView.OptionsBehavior.Editable    = false;



        }

        private void GridView_KeyDown(object sender, KeyEventArgs e)
        {
            // Verificar si la tecla presionada es Enter
            if (e.KeyCode == Keys.Enter)
            {
                fnSelectedRow();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            fnSelectedRow();
        }

        void fnSelectedRow()
        {
            // Verificar si se ha seleccionado alguna fila
            string resultJson = "{";
            if (objGridView.SelectedRowsCount > 0)
            {

                // Obtener el índice de la fila seleccionada
                List<int> rowIndex = objGridView.GetSelectedRows().ToList();
                foreach (var indexRow in rowIndex)
                {
                    foreach (GridColumn column in objGridView.Columns)
                    {
                        string nombreColumna = column.FieldName;
                        string valueColumn = objGridView.GetRowCellValue(indexRow, nombreColumna).ToString();
                        resultJson = resultJson + "" + nombreColumna + ":'" + valueColumn + "'";

                    }
                }

            }
            else
            {
                // No se ha seleccionado ninguna fila, maneja este caso según tus requerimientos
            }

            resultJson = resultJson + "}";
            EventoCallBackAceptar_?.Invoke(resultJson);
        }
    }
}