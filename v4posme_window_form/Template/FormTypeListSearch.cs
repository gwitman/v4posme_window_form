using System.ComponentModel;
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
using v4posme_library.ModelsDto;
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
        public event EventoCallBackAceptar? EventoCallBackAceptarEvent;
        private int? ComponentId { get; set; }
        private string? ViewName { get; set; }
        private bool? AutoClose { get; set; }

        private bool ShowRow { get; set; }
        private string? Filter { get; set; }
        private bool? MultiSelect { get; set; }
        private string? UrlRedictWhenEmpty { get; set; }
        private int? DisplayStart { get; set; }
        private int? DisplayLength { get; set; }
        private string? SSearch { get; set; }
        private int? PageCurrent { get; set; }
        private string? TitleWindow { get; set; }
        private GridControl ObjGridControl { get; set; }

        private GridView? _gridView = null;
        private TableCompanyDataViewDto? _datos;

        public FormTypeListSearch(string title, int componentId, string viewName, bool autoClose, string filter,
            bool multiSelect, string urlRedictWhenEmpty, int iDisplayStart, int iDisplayLength, string sSearch, bool showRow)
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
            ObjGridControl = new GridControl
            {
                TabIndex = 8
            };
            _gridView = ObjGridControl.MainView as GridView;
            ShowRow = showRow;
            InitializeComponent();
        }

        private void FormTypeListSearch_Load(object sender, EventArgs e)
        {
            var controlParent = centerPane;
            if (TitleWindow != null) Text = TitleWindow;
            ObjGridControl.Name = "ObjGridControl";
            ObjGridControl.Parent = controlParent;
            ObjGridControl.Dock = DockStyle.Fill;
            if (ShowRow) ShowViewByNamePaginate();
            ObjGridControl.KeyDown += GridView_KeyDown!;
            ((GridView)ObjGridControl.MainView).OptionsView.ShowGroupPanel = false;
            ((GridView)ObjGridControl.MainView).OptionsBehavior.Editable = false;
            txtFilter.SelectAll();
            txtFilter.Focus();
        }


        private void ShowViewByNamePaginate()
        {
            backgroundWorker1 = new();
            if (!progressPanel.Visible)
            {
                progressPanel.Visible = true;
            }

            var componentId = ComponentId!.Value;
            var viewName = ViewName!;
            var autoClose = AutoClose!.Value;
            var filter = Filter;
            var multiSelect = MultiSelect!.Value;
            var urlRedictWhenEmpty = UrlRedictWhenEmpty;
            var iDisplayStart = DisplayStart;
            var iDisplayLength = DisplayLength;
            var sSearch = SSearch;
            backgroundWorker1.DoWork += (ob, ev) =>
            {
                var coreWebTools = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();
                var coreWebView = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebView>();
                var calleridSearch = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["CALLERID_SEARCH"]);
                var coreWebRenderInView = new CoreWebRenderInView();
                var usuario = VariablesGlobales.Instance.User;


                // Crear un diccionario para los parámetros staticos
                var parameter = new Dictionary<string, string>
                {
                    ["{companyID}"] = usuario!.CompanyID.ToString(),
                    ["{componentID}"] = componentId.ToString(),
                    ["{iDisplayLength}"] = iDisplayLength!.Value.ToString(),
                    ["{iDisplayStartDB}"] = (PageCurrent * iDisplayLength.Value).ToString()!,
                    ["{sSearchDB}"] = sSearch!,
                    ["{isWindowForm}"] = "1"
                };

                // Agregar al diccionarios los parametros dinamicos
                var result = coreWebTools.FormatParameter(filter!);
                if (result is not null)
                {
                    foreach (var kvp in result)
                    {
                        parameter[kvp.Key] = kvp.Value.ToString()!;
                    }
                }


                _datos = coreWebView.GetViewByName(usuario, componentId, viewName, calleridSearch, null, parameter);
            };

            backgroundWorker1.RunWorkerCompleted += (ob, ev) =>
            {
                var coreWebRender = new CoreWebRenderInView();
                if (ev.Error is not null)
                {
                    coreWebRender.GetMessageAlert(TypeError.Error, "Seleccionar", $"No se pudo realizar la operacion debido al siguiente error {ev.Error.Message}", this);
                }
                else if (ev.Cancelled)
                {
                    coreWebRender.GetMessageAlert(TypeError.Warning, "Seleccionar", "Se ha cancelado la operacion", this);
                }
                else
                {
                    progressPanel.Visible = false;
                    CoreWebRenderInView.RenderGrid(_datos, "ListView", ObjGridControl);
                    _gridView = (GridView)ObjGridControl.MainView;
                    _gridView.OptionsSelection.MultiSelect = multiSelect;
                    ObjGridControl.Refresh();
                }
            };
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
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
            backgroundWorker1 = new BackgroundWorker();
            if (!progressPanel.Visible)
            {
                progressPanel.Visible = true;
            }

            backgroundWorker1.DoWork += (ob, ev) => { FnSelectedRow(); };
            backgroundWorker1.RunWorkerCompleted += (ob, ev) =>
            {
                var coreWebRender = new CoreWebRenderInView();
                if (ev.Error is not null)
                {
                    coreWebRender.GetMessageAlert(TypeError.Error, "Seleccionar", $"No se pudo realizar la operacion debido al siguiente error {ev.Error.Message}", this);
                }
                else if (ev.Cancelled)
                {
                    coreWebRender.GetMessageAlert(TypeError.Warning, "Seleccionar", "Se ha cancelado la operacion", this);
                }
                else
                {
                    progressPanel.Visible = false;
                    if (AutoClose is not null && AutoClose.Value) Close();
                }
            };
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void FnSelectedRow()
        {
            // Verificar si se ha seleccionado alguna fila
            dynamic dynamicObject = new ExpandoObject();
            var dictionaryObject = (IDictionary<string, object>)dynamicObject;
            if (((GridView)ObjGridControl.MainView).SelectedRowsCount > 0)
            {
                // Obtener el índice de la fila seleccionada
                List<int> rowIndex = _gridView!.GetSelectedRows().ToList();
                foreach (var indexRow in rowIndex)
                {
                    foreach (GridColumn column in _gridView!.Columns)
                    {
                        var nombreColumna = column.FieldName ?? "";
                        var valueColumn = ((GridView)ObjGridControl.MainView).GetRowCellValue(indexRow, nombreColumna).ToString();
                        dictionaryObject[nombreColumna] = valueColumn!;
                    }
                }
            }

            EventoCallBackAceptarEvent?.Invoke(dynamicObject);
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