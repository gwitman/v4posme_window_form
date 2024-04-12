using System.Data;
using DevExpress.DataAccess.Native.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_window.Libraries;
using DataTable = System.Data.DataTable;
using GridView = DevExpress.XtraGrid.Views.Grid.GridView;

namespace v4posme_window.Template
{
    public partial class FormTypeListSearch : XtraForm
    {
        // Declarar un delegado que represente la firma del método que quieres llamar en el formulario padre
        public delegate void EventoCallBackAceptar(string mensaje);

        // Declarar un evento que se disparará cuando ocurra el evento en el formulario hijo
        public event EventoCallBackAceptar EventoCallBackAceptar_;
        private const int PageSize = 10; // Tamaño de página
        private int _currentPage; // Página actual
        private DataTable _dataSource;
        private int ComponentId { get; set; }
        private string ViewName { get; set; }
        private bool AutoClose { get; set; }
        private string Filter { get; set; }
        private bool MultiSelect { get; set; }
        private string UrlRedictWhenEmpty { get; set; }
        private int DisplayStart { get; set; }
        private int DisplayLength { get; set; }
        private string SSearch { get; set; }
        private GridView ObjGridView { get; set; }

        public FormTypeListSearch(int componentId, string viewName, bool autoClose, string filter, bool multiSelect,
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
            InitializeComponent();
        }

        private void FormTypeListSearch_Load(object sender, EventArgs e)
        {
            var title = VariablesGlobales.ConfigurationBuilder["NAME_FORM_TYPE_LIST"];
            Text = title;
            ShowViewByNamePaginate();
            if (_currentPage == 0)
            {
                btnAtras.Enabled = false;
            }

            if ((_dataSource.Rows.Count - 1) / PageSize < 1)
            {
                btnSiguiente.Enabled = false;
            }
        }


        public void ShowViewByNamePaginate()
        {
            PanelControl controlParent = this.centerPane;
            int componentID = ComponentId;
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
                ["{componentID}"] = componentID.ToString(),
                ["{iDisplayStart}"] = iDisplayStart.ToString(),
                ["{iDisplayStartDB}"] = "0",
                ["{iDisplayLength}"] = iDisplayLength.ToString(),
                ["{sSearchDB}"] = sSearch,
                ["{sSearch}"] = sSearch,
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


            var datos = coreWebView.GetViewByName(usuario, componentID, viewName, calleridSearch, null, parameter);
            var datosTotales = coreWebView.GetViewByName(usuario, componentID, viewName + "_TOTAL", calleridSearch,
                null, parameter);
            var datosDisplay = coreWebView.GetViewByName(usuario, componentID, viewName + "_DISPLAY", calleridSearch,
                null, parameter);


            if (datos is null)
                return;


            //aqui vamos a validar si seleccion multiple
            ObjGridView = CoreWebRenderInView.RenderGrid(datos, "ListView", iDisplayLength, controlParent);
            ObjGridView.KeyDown += GridView_KeyDown!;
            ObjGridView.OptionsView.ShowGroupPanel = false;
            ObjGridView.OptionsBehavior.Editable = false;
            _dataSource = (DataTable)ObjGridView.GridControl.DataSource;
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
            var resultJson = "{";
            if (ObjGridView.SelectedRowsCount > 0)
            {
                // Obtener el índice de la fila seleccionada
                List<int> rowIndex = ObjGridView.GetSelectedRows().ToList();
                foreach (var indexRow in rowIndex)
                {
                    foreach (GridColumn column in ObjGridView.Columns)
                    {
                        var nombreColumna = column.FieldName;
                        var valueColumn = ObjGridView.GetRowCellValue(indexRow, nombreColumna).ToString();
                        resultJson = resultJson + "" + nombreColumna + ":'" + valueColumn + "'";
                    }
                }
            }
            else
            {
                // No se ha seleccionado ninguna fila, maneja este caso según tus requerimientos
            }

            resultJson += "}";
            EventoCallBackAceptar_.Invoke(resultJson);
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            if (_currentPage > 0)
            {
                ChangePage(_currentPage - 1);
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            _dataSource = (DataTable)ObjGridView.GridControl.DataSource;
            if (_currentPage < (_dataSource.Rows.Count - 1) / PageSize)
            {
                ChangePage(_currentPage + 1);
                btnAtras.Enabled = true;
            }
        }

        private void ChangePage(int page)
        {
            _currentPage = page;
            LoadData();
        }

        private void LoadData()
        {
            
            var rows = _dataSource.AsEnumerable().Skip(_currentPage * PageSize).Take(PageSize).CopyToDataTable();
            ObjGridView.GridControl.DataSource = rows;
        }
    }
}