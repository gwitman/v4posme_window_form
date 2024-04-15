using System.Data;
using System.Dynamic;
using System.Text;
using System.Windows.Forms;
using DevExpress.DataAccess.Native.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraVerticalGrid;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_window.Libraries;
using DataColumn = System.Data.DataColumn;
using DataTable = System.Data.DataTable;
using DataView = System.Data.DataView;
using GridView = DevExpress.XtraGrid.Views.Grid.GridView;

namespace v4posme_window.Template
{
    public partial class FormTypeListSearch : XtraForm
    {
        // Declarar un delegado que represente la firma del método que quieres llamar en el formulario padre
        public delegate void EventoCallBackAceptar(dynamic mensaje);

        // Declarar un evento que se disparará cuando ocurra el evento en el formulario hijo
        public event EventoCallBackAceptar EventoCallBackAceptar_;
        private int ComponentId { get; set; }
        private string ViewName { get; set; }
        private bool AutoClose { get; set; }
        private string Filter { get; set; }
        private bool MultiSelect { get; set; }
        private string UrlRedictWhenEmpty { get; set; }
        private int DisplayStart { get; set; }
        private int DisplayLength { get; set; }
        private string SSearch { get; set; }
        private int PageCurrent { get; set; }
        private string TitleWindow { get; set; }
        private GridControl ObjGridControl { get; set; }

        public FormTypeListSearch(string title, int componentId, string viewName, bool autoClose, string filter,
            bool multiSelect,
            string urlRedictWhenEmpty, int iDisplayStart, int iDisplayLength, string sSearch)
        {
            ComponentId = componentId;
            ViewName = viewName;
            AutoClose = autoClose;
            Filter = filter;
            MultiSelect = multiSelect;
            UrlRedictWhenEmpty = urlRedictWhenEmpty;
            DisplayStart = iDisplayStart;
            DisplayLength = iDisplayLength;
            SSearch = sSearch;
            TitleWindow = title;
            PageCurrent = 0;
            ObjGridControl = new GridControl();
            InitializeComponent();
        }

        private void FormTypeListSearch_Load(object sender, EventArgs e)
        {
            PanelControl controlParent = this.centerPane;
            Text = TitleWindow;
            ObjGridControl.Name = "ObjGridControl";
            ObjGridControl.Parent = controlParent;
            ObjGridControl.Dock = DockStyle.Fill;

            ObjGridControl.KeyDown += GridView_KeyDown!;
            ((GridView)ObjGridControl.MainView).OptionsView.ShowGroupPanel = false;
            ((GridView)ObjGridControl.MainView).OptionsBehavior.Editable = false;
            
        }


        private void ShowViewByNamePaginate()
        {
            int componentId = ComponentId;
            string viewName = ViewName;
            bool autoClose = AutoClose;
            string filter = Filter;
            bool multiSelect = MultiSelect;
            string urlRedictWhenEmpty = UrlRedictWhenEmpty;
            int iDisplayStart = DisplayStart;
            int iDisplayLength = DisplayLength;
            string sSearch = SSearch;

            var coreWebTools = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();
            var coreWebView = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebView>();
            var calleridSearch = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["CALLERID_SEARCH"]);
            var coreWebRenderInView = new CoreWebRenderInView();
            var usuario = VariablesGlobales.Instance.User;


            // Crear un diccionario para los parámetros staticos
            var parameter = new Dictionary<string, string>
            {
                ["{companyID}"] = usuario!.CompanyId.ToString(),
                ["{componentID}"] = componentId.ToString(),
                ["{iDisplayLength}"] = iDisplayLength.ToString(),
                ["{iDisplayStartDB}"] = (PageCurrent * iDisplayLength).ToString(),
                ["{sSearchDB}"] = sSearch,
                ["{isWindowForm}"] = "1"
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


            var datos = coreWebView.GetViewByName(usuario, componentId, viewName, calleridSearch, null, parameter);
            if (datos is null)
                return;


            CoreWebRenderInView.RenderGrid(datos, "ListView", ObjGridControl);
            ObjGridControl.Refresh();
        }

        private void GridView_KeyDown(object sender, KeyEventArgs e)
        {
            // Verificar si la tecla presionada es Enter
            if (e.KeyCode == Keys.Enter)
            {
                FnSelectedRow();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            FnSelectedRow();
        }

        private void FnSelectedRow()
        {
            // Verificar si se ha seleccionado alguna fila
            dynamic dynamicObject = new ExpandoObject();
            var dictionaryObject = (IDictionary<string, object>)dynamicObject;
            if (((GridView)ObjGridControl.MainView).SelectedRowsCount > 0)
            {
                // Obtener el índice de la fila seleccionada
                List<int> rowIndex = ((GridView)ObjGridControl.MainView).GetSelectedRows().ToList();
                foreach (var indexRow in rowIndex)
                {
                    foreach (GridColumn column in ((GridView)ObjGridControl.MainView).Columns)
                    {
                        string nombreColumna    = column.FieldName == null ? "" : column.FieldName;
                        var valueColumn         = ((GridView)ObjGridControl.MainView).GetRowCellValue(indexRow, nombreColumna).ToString();
                        dictionaryObject[nombreColumna] = valueColumn!;
                    }
                }
            }
            else
            {
                // No se ha seleccionado ninguna fila, maneja este caso según tus requerimientos
            }

            EventoCallBackAceptar_?.Invoke(dynamicObject);
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            if (PageCurrent >= 1)
            {
                PageCurrent--;
                ShowViewByNamePaginate();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            PageCurrent++;
            ShowViewByNamePaginate();
           
        }


        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si se presionó la tecla "Enter"
            if (e.KeyChar == (char)Keys.Enter)
            {
                SSearch = txtFilter.Text;
                PageCurrent = 0;
                ShowViewByNamePaginate();
            }
        }
    }
}