using DevExpress.Map.Native;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using Mysqlx.Cursor;
using System.Dynamic;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomHelper;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_window.Interfaz;
using v4posme_window.Libraries;
using v4posme_window.Template;
using GridView = DevExpress.XtraGrid.Views.Grid.GridView;

namespace v4posme_window.Views
{
    public sealed partial class FormInvoiceBillingList : FormTypeList, IFormTypeList
    {
        private readonly CoreWebRenderInView _coreWebRender = new CoreWebRenderInView();
        private readonly WebToolsHelper _webToolsHelper = new WebToolsHelper();

        private readonly ICoreWebPermission _coreWebPermission =
            VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebPermission>();

        private readonly ICoreWebTools _coreWebTools =
            VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();

        private readonly ICoreWebView _coreWebView = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebView>();

        public GridControl ObjGridControl { get; set; }
        public int? DataViewId { get; set; }
        public DateTime? Fecha { get; set; }

        public FormInvoiceBillingList()
        {
            InitializeComponent();

            // Suscribir al manejador de excepciones global
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Fecha = DateTime.Now;
            DataViewId = 0;
            ObjGridControl = new GridControl();
            btnEditar.Click += Edit;
            btnEliminar.Click += Delete;
            btnNuevo.Click += New;
        }


        private void FormInvoiceBillingList_Load(object sender, EventArgs e)
        {
            PreRender();
            List();
        }

        
        public void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            CustomException.LogException(e.Exception);
        }

        public void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            CustomException.LogException((Exception)e.ExceptionObject);
        }


        public void List()
        {
            var coreWebRenderInView = new CoreWebRenderInView();
            var resultPermission = 0;
            var appNeedAuthentication = VariablesGlobales.ConfigurationBuilder["APP_NEED_AUTHENTICATION"];
            var permissionNone = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_NONE"]);
            var urlSuffix = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX"];
            if (appNeedAuthentication!.Equals("true"))
            {
                var permited = _coreWebPermission.UrlPermited("app_invoice_billing", "index", urlSuffix!,
                    VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                    VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                    VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    _coreWebRender.GetMessageAlert(TypeError.Error, "Permisos", "No tiene acceso a los controles", this);
                    return;
                }

                resultPermission = _coreWebPermission.UrlPermissionCmd("app_invoice_billing", "index", urlSuffix!,
                    VariablesGlobales.Instance.Role, VariablesGlobales.Instance.User,
                    VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                    VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                    VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    _coreWebRender.GetMessageAlert(TypeError.Error, "Permisos", "No se encontraron permisos", this);
                    return;
                }
            }

            var objComponent = _coreWebTools.GetComponentIdByComponentName("tb_transaction_master_billing");
            if (objComponent is null)
            {
                _coreWebRender.GetMessageAlert(TypeError.Error, "Error",
                    "00409 EL COMPONENTE 'tb_transaction_master_billing' NO EXISTE...", this);
                return;
            }

            var callerIdList = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["CALLERID_LIST"]);
            Dictionary<string, string> parameters;
            if (DataViewId == 0)
            {
                var targetComponentId = VariablesGlobales.Instance.Company!.FlavorId!;
                parameters = new Dictionary<string, string>
                {
                    { "{companyID}", VariablesGlobales.Instance.User!.CompanyId.ToString() },
                    { "{fecha}", Fecha!.Value.ToShortDateString() }
                };

                var dataViewData = _coreWebView.GetViewDefault(VariablesGlobales.Instance.User, objComponent.ComponentId, callerIdList, targetComponentId, resultPermission, parameters);
                if (dataViewData is null)
                {
                    targetComponentId = 0;
                    parameters = new Dictionary<string, string>
                    {
                        { "{companyID}", VariablesGlobales.Instance.User.CompanyId.ToString() },
                        { "{fecha}", Fecha.Value.ToShortDateString() }
                    };
                    dataViewData = _coreWebView.GetViewDefault(VariablesGlobales.Instance.User, objComponent.ComponentId, callerIdList, targetComponentId, resultPermission, parameters);
                }


                CoreWebRenderInView.RenderGrid(dataViewData!, "invoice", ObjGridControl);
                ObjGridControl.Refresh();
            }
            else
            {
                parameters = new Dictionary<string, string>
                {
                    { "{companyID}", VariablesGlobales.Instance.User!.CompanyId.ToString() }
                };

                var dataViewData = _coreWebView.GetViewByDataViewId(VariablesGlobales.Instance.User, objComponent.ComponentId, DataViewId!.Value, callerIdList, resultPermission, parameters);
                CoreWebRenderInView.RenderGrid(dataViewData!, "invoice", ObjGridControl);
                ObjGridControl.Refresh();
            }


        }

        public void Delete(object? sender, EventArgs? args)
        {

            if (((GridView)ObjGridControl.MainView).SelectedRowsCount > 0)
            {
                // Obtener el índice de la fila seleccionada
                var rowIndex = ((GridView)ObjGridControl.MainView).GetSelectedRows().ToList();
                foreach (var indexRow in rowIndex)
                {

                    var companyId = Convert.ToInt32(((GridView)ObjGridControl.MainView).GetRowCellValue(indexRow, "companyID").ToString());
                    var transactionId = Convert.ToInt32(((GridView)ObjGridControl.MainView).GetRowCellValue(indexRow, "transactionID").ToString());
                    var transactionMasterId = Convert.ToInt32(((GridView)ObjGridControl.MainView).GetRowCellValue(indexRow, "transactionMasterID").ToString());
                    var objFormInvoiceBillingEdit = new FormInvoiceBillingEdit(TypeOpenForm.NotInit, companyId, transactionId, transactionMasterId);
                    objFormInvoiceBillingEdit.ComandDelete();
                    
                }
                //Listar los registros nuevamente
                List();

            }
            else
            {
                CoreWebRenderInView objCoreWebRenderInView = new CoreWebRenderInView();
                objCoreWebRenderInView.GetMessageAlert(TypeError.Error, @"Error eliminando", "Debe seleccionar un registro", this);
            }

        }

        public void Edit(object? sender, EventArgs? args)
        {
            var formInvoiceBillingEdit = new FormInvoiceBillingEdit(TypeOpenForm.Init, 0, 0, 0)
            {
                MdiParent = CoreFormList.Principal()
            };
            formInvoiceBillingEdit.Show();
        }

        public void New(object? sender, EventArgs? args)
        {
            new FormInvoiceBillingEdit(TypeOpenForm.Init, 0, 0, 2).ShowDialog();
        }

        public void PreRender()
        {
            lblTitulo.Text = @"LISTA DE FACTURAS";
            Text = lblTitulo.Text;


            PanelControl controlParent = this.centerPane;
            ObjGridControl.Name = "ObjGridControl";
            ObjGridControl.Parent = controlParent;
            ObjGridControl.Dock = DockStyle.Fill;


        }


    }
}