using DevExpress.Map.Native;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraWaitForm;
using Mysqlx.Cursor;
using System.Dynamic;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomHelper;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;
using v4posme_library.Models;
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

        private readonly ICoreWebTransaction _coreWebTransaction = 
            VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTransaction>();

        private readonly ICoreWebView _coreWebView = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebView>();

        public GridControl ObjGridControl { get; set; }
        public int? DataViewId { get; set; }
        public DateTime? Fecha { get; set; }

        private static readonly string? UserNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];
        private static readonly string? NotParameter = VariablesGlobales.ConfigurationBuilder["NOT_PARAMETER"];
        private static readonly string? AppNeedAuthentication = VariablesGlobales.ConfigurationBuilder["APP_NEED_AUTHENTICATION"];
        private static readonly string? NotAccessFunction = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_FUNCTION"];
        private static readonly string? NotAccessControl = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_CONTROL"];
        private static readonly int? PermissionNone = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_NONE"]);
        private static readonly string? UrlSuffix = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX"];

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
            btnSearchTransaction.Click += SearchTransactionMaster;
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

            if (AppNeedAuthentication!.Equals("true"))
            {
                var permited = _coreWebPermission.UrlPermited("app_invoice_billing", "index", UrlSuffix!,
                    VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                    VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                    VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    _coreWebRender.GetMessageAlert(TypeError.Error, "Permisos", "No tiene acceso a los controles", this);
                    return;
                }

                resultPermission = _coreWebPermission.UrlPermissionCmd("app_invoice_billing", "index", UrlSuffix!,
                    VariablesGlobales.Instance.Role, VariablesGlobales.Instance.User,
                    VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                    VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                    VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == PermissionNone)
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
                ObjGridControl.MainView.RefreshData();
                ObjGridControl.Refresh();
            }
            else
            {
                parameters = new Dictionary<string, string>
                {
                    { "{companyID}", VariablesGlobales.Instance.User!.CompanyId.ToString() }
                };

                var dataViewData = _coreWebView.GetViewByDataViewId(VariablesGlobales.Instance.User, objComponent.ComponentId, DataViewId!.Value, callerIdList, resultPermission, parameters);
                CoreWebRenderInView.RenderGrid(dataViewData, "invoice", ObjGridControl);
                ObjGridControl.MainView.RefreshData();
                ObjGridControl.Refresh();
            }
        }

        private void RepositoryItemButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            throw new NotImplementedException();
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
            if (((GridView)ObjGridControl.MainView).SelectedRowsCount > 0)
            {
                
                var rowIndex = ((GridView)ObjGridControl.MainView).GetSelectedRows().ToList();
                foreach (var indexRow in rowIndex)
                {
                    var companyId = Convert.ToInt32(((GridView)ObjGridControl.MainView).GetRowCellValue(indexRow, "companyID").ToString());
                    var transactionId = Convert.ToInt32(((GridView)ObjGridControl.MainView).GetRowCellValue(indexRow, "transactionID").ToString());
                    var transactionMasterId = Convert.ToInt32(((GridView)ObjGridControl.MainView).GetRowCellValue(indexRow, "transactionMasterID").ToString());
                    var objFormInvoiceBillingEdit = new FormInvoiceBillingEdit(TypeOpenForm.NotInit, companyId, transactionId, transactionMasterId);
                    

                    var formInvoiceBillingEdit = new FormInvoiceBillingEdit(
                        TypeOpenForm.Init, companyId, transactionId, 
                        transactionMasterId){MdiParent = CoreFormList.Principal()};
                    formInvoiceBillingEdit.Show();
                    break;

                }

                
            }
            else
            {
                CoreWebRenderInView objCoreWebRenderInView = new CoreWebRenderInView();
                objCoreWebRenderInView.GetMessageAlert(TypeError.Error, @"Error editando", "Debe seleccionar un registro", this);
            }


            
        }

        public void New(object? sender, EventArgs? args)
        {
            var transactionID = _coreWebTransaction.GetTransactionId(VariablesGlobales.Instance.User!.CompanyId, "tb_transaction_master_billing",0);

            var objFormInvoiceList = new FormInvoiceBillingEdit(
                TypeOpenForm.Init,
                VariablesGlobales.Instance.User!.CompanyId,
                transactionID!.Value,
                0
            ){ MdiParent = CoreFormList.Principal() };
            objFormInvoiceList.Show();
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

        public void SearchTransactionMaster(object? sender, EventArgs? args)
        {
            try
            {
                var user = VariablesGlobales.Instance.User;
                if (user is null)
                {
                    throw new Exception(UserNotAutenticated);
                }

                var role = VariablesGlobales.Instance.Role;
                if (role is null)
                {
                    throw new Exception("No hay roles asignados");
                }

                // Permiso sobre la función
                if (AppNeedAuthentication == "true")
                {
                    var menuTop = VariablesGlobales.Instance.ListMenuTop;
                    var menuLeft = VariablesGlobales.Instance.ListMenuLeft;
                    var menuBodyReport = VariablesGlobales.Instance.ListMenuBodyReport;
                    var menuBodyTop = VariablesGlobales.Instance.ListMenuBodyTop;
                    var menuHiddenPopup = VariablesGlobales.Instance.ListMenuHiddenPopup;
                    var permited = _coreWebPermission.UrlPermited("app_invoice_billing", "index", UrlSuffix!, menuTop, menuLeft, menuBodyReport, menuBodyTop, menuHiddenPopup);

                    if (!permited)
                        throw new Exception(NotAccessControl);

                    var resultPermission = _coreWebPermission.UrlPermissionCmd("app_invoice_billing", "index", UrlSuffix!, role, user, menuTop, menuLeft, menuBodyReport, menuBodyTop, menuHiddenPopup);
                    if (resultPermission == PermissionNone)
                        throw new Exception(NotAccessFunction);
                }

                var transactionNumber = txtFiltrar.Text;

                if (string.IsNullOrEmpty(transactionNumber))
                throw new Exception(NotParameter);

                var objTm = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterModel>().GetRowByTransactionNumber(user.CompanyId, transactionNumber);

                if (objTm == null)
                throw new Exception("NO SE ENCONTRO EL DOCUMENTO");


                var formInvoiceBillingEdit = new FormInvoiceBillingEdit(
                      TypeOpenForm.Init, objTm.CompanyId, objTm.TransactionId,
                      objTm.TransactionMasterId
                )
                { MdiParent = CoreFormList.Principal() };
                formInvoiceBillingEdit.Show();


            }
            catch (Exception ex)
            {
                new CoreWebRenderInView().GetMessageAlert(TypeError.Error, "Error", $"Se produjo un error en {ex.Source} {ex.Message}", this);
            }
        }
    }
}