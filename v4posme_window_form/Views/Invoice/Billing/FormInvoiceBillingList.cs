﻿using System.ComponentModel;
using System.Diagnostics;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomHelper;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;
using v4posme_library.ModelsDto;
using v4posme_window.Interfaz;
using v4posme_window.Libraries;
using v4posme_window.Template;
using GridView = DevExpress.XtraGrid.Views.Grid.GridView;

namespace v4posme_window.Views.Invoice.Billing
{
    public sealed partial class FormInvoiceBillingList : FormTypeList, IFormTypeList
    {
        private readonly CoreWebRenderInView _coreWebRender = new CoreWebRenderInView();
        private readonly WebToolsHelper _webToolsHelper = new WebToolsHelper();
        private readonly ICoreWebPermission coreWebPermission = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebPermission>();
        private readonly ICoreWebTools coreWebTools = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();
        private readonly ICoreWebTransaction coreWebTransaction = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTransaction>();
        private readonly ICoreWebView coreWebView = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebView>();

        public GridControl ObjGridControl { get; set; }
        public int? DataViewId { get; set; }
        public DateTime? Fecha { get; set; }
        private bool EsMesero { get; set; }

        private static readonly string? UserNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];
        private static readonly string? NotParameter = VariablesGlobales.ConfigurationBuilder["NOT_PARAMETER"];
        private static readonly string? AppNeedAuthentication = VariablesGlobales.ConfigurationBuilder["APP_NEED_AUTHENTICATION"];
        private static readonly string? NotAccessFunction = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_FUNCTION"];
        private static readonly string? NotAccessControl = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_CONTROL"];
        private static readonly int? PermissionNone = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_NONE"]);
        private static readonly string? UrlSuffix = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX"];
        
        private TableCompanyDataViewDto? _dataViewData;
        private GridView? _gridViewData=null;

        public FormInvoiceBillingList()
        {
            InitializeComponent();

            // Suscribir al manejador de excepciones global
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Fecha = DateTime.Now;
            DataViewId = 0;
            ObjGridControl = new GridControl();
            btnEditar.Click += Edit;
            btnEliminar.Click += Delete;
            btnNuevo.Click += New;
            btnSearchTransaction.Click += SearchTransactionMaster;
        }

        private void FormInvoiceBillingList_Enter(object sender, EventArgs e)
        {
            //este evento es cada vez que el formulario tiene el focus
            if (!backgroundWorker.IsBusy)
            {
                FormInvoiceBillingList_Load(sender, e);
            }
        }

        private void FormInvoiceBillingList_Load(object sender, EventArgs e)
        {
            backgroundWorker = new BackgroundWorker();            
            backgroundWorker.DoWork += (ob, ev) =>
            {
                List();
            };
            backgroundWorker.RunWorkerCompleted += (obb, evb) =>
            {
                // Ocultar el mensaje de carga
                if (progressPanel.Visible)
                {
                    progressPanel.Visible = false;
                }

                // Verificar si hubo algún error durante la carga de datos
                if (evb.Error is not null)
                {
                    _coreWebRender.GetMessageAlert(TypeError.Error, "Error", $@"Error al cargar datos: {evb.Error.Message}", this);
                    return;
                }

                if (evb.Cancelled)
                {
                    _coreWebRender.GetMessageAlert(TypeError.Error, "Error", $@"Operación cancelada por el usuario", this);
                    return;
                }

                // Actualizar la interfaz de usuario con los datos cargados
                PreRender();
                RefreshData();
            };
            progressPanel.Size = Size;
            if (!progressPanel.Visible)
            {
                progressPanel.Visible = true;
            }
            backgroundWorker.RunWorkerAsync();
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
                var permited = coreWebPermission.UrlPermited("app_invoice_billing", "index", UrlSuffix!,
                    VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                    VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                    VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    _coreWebRender.GetMessageAlert(TypeError.Error, "Permisos", "No tiene acceso a los controles", this);
                    return;
                }

                resultPermission = coreWebPermission.UrlPermissionCmd("app_invoice_billing", "index", UrlSuffix!,
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

            var objComponent = coreWebTools.GetComponentIdByComponentName("tb_transaction_master_billing");
            if (objComponent is null)
            {
                _coreWebRender.GetMessageAlert(TypeError.Error, "Error",
                    "00409 EL COMPONENTE 'tb_transaction_master_billing' NO EXISTE...", this);
                return;
            }

            //Validar si es cajero
            var esMesero = coreWebPermission.UrlPermited("es_mesero", "index", UrlSuffix!,
                    VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                    VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                    VariablesGlobales.Instance.ListMenuHiddenPopup);

            EsMesero = VariablesGlobales.Instance.Role!.IsAdmin == true ? false : esMesero;


            var callerIdList = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["CALLERID_LIST"]);
            Dictionary<string, string> parameters;
            if (DataViewId == 0)
            {
                var targetComponentId = VariablesGlobales.Instance.Company!.FlavorID!;
                parameters = new Dictionary<string, string>
                {
                    { "{companyID}", VariablesGlobales.Instance.User!.CompanyID.ToString() },
                    { "{fecha}", Fecha!.Value.ToString("yyyy-MM-dd")  }
                };

                _dataViewData = coreWebView.GetViewDefault(VariablesGlobales.Instance.User, objComponent.ComponentID, callerIdList, targetComponentId, resultPermission, parameters);
                if (_dataViewData is null)
                {
                    targetComponentId = 0;
                    parameters = new Dictionary<string, string>
                    {
                        { "{companyID}", VariablesGlobales.Instance.User.CompanyID.ToString() },
                        { "{fecha}", Fecha.Value.ToString("yyyy-MM-dd") }
                    };
                    _dataViewData = coreWebView.GetViewDefault(VariablesGlobales.Instance.User, objComponent.ComponentID, callerIdList, targetComponentId, resultPermission, parameters);
                }
            }
            else
            {
                parameters = new Dictionary<string, string>
                {
                    { "{companyID}", VariablesGlobales.Instance.User!.CompanyID.ToString() }
                };

                _dataViewData = coreWebView.GetViewByDataViewId(VariablesGlobales.Instance.User, objComponent.ComponentID, DataViewId!.Value, callerIdList, resultPermission, parameters);
            }
        }

        public void RefreshData()
        {
            CoreWebRenderInView.RenderGrid(_dataViewData!, "invoice", ObjGridControl);
            _gridViewData = (GridView)ObjGridControl.MainView;
            _gridViewData.RefreshData();
            _gridViewData.Columns.ForEach(column => column.OptionsColumn.ReadOnly=true);
            ObjGridControl.Refresh();
        }

        public void Delete(object? sender, EventArgs? args)
        {
            var result = XtraMessageBox.Show("¿Estás seguro de que deseas eliminar este registro? Esta acción no se puede revertir", "Eliminar", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                return;
            }

            if (!progressPanel.Visible)
            {
                progressPanel.Visible = true;
            }
            var countRows = _gridViewData!.SelectedRowsCount > 0;
            if (!countRows)
            {
                _coreWebRender.GetMessageAlert(TypeError.Error, @"Error eliminando", "Debe seleccionar un registro", this);
                return;
            }
            

            backgroundWorker=new BackgroundWorker();
            backgroundWorker.DoWork += (ob, ev) =>
            {
                
                var rowIndex = _gridViewData.GetSelectedRows();
                foreach (var indexRow in rowIndex)
                {
                    var companyId = Convert.ToInt32(_gridViewData.GetRowCellValue(indexRow, "companyID").ToString());
                    var transactionId = Convert.ToInt32(_gridViewData.GetRowCellValue(indexRow, "transactionID").ToString());
                    var transactionMasterId = Convert.ToInt32(_gridViewData.GetRowCellValue(indexRow, "transactionMasterID").ToString());
                    var objFormInvoiceBillingEdit = new FormInvoiceBillingEdit(TypeOpenForm.NotInit, companyId, transactionId, transactionMasterId,"none");
                    objFormInvoiceBillingEdit.ComandDelete();
                }
            };
            
            backgroundWorker.RunWorkerCompleted += (ob, ev) =>
            {   
                Debug.WriteLine(ev);
                if (ev.Error is not null)
                {
                    _coreWebRender.GetMessageAlert(TypeError.Error, "Error Eliminar", $"Se ha producido un error al eliminar {ev.Error.Message}", this);

                }else if (ev.Cancelled)
                {
                    _coreWebRender.GetMessageAlert(TypeError.Warning, "Eliminar", "Se ha cancelado la eliminación de la factura", this);
                }
                else
                {
                    _coreWebRender.GetMessageAlert(TypeError.Informacion, "Eliminar", "Se ha eliminado la factura de forma correcta", this);
                    List();
                    RefreshData();
                }
                if (progressPanel.Visible)
                {
                    progressPanel.Visible = false;
                }
            };

            if (backgroundWorker.IsBusy) return;
            backgroundWorker.RunWorkerAsync();
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

                    if (EsMesero == true)
                    {
                        using (var inputForm = new FormTypeCustomInputForm())
                        {
                            if (inputForm.ShowDialog() == DialogResult.OK)
                            {
                                var formInvoiceBillingEdit = new FormInvoiceBillingEdit(
                                        TypeOpenForm.Init, companyId, transactionId,
                                        transactionMasterId , inputForm.InputText)
                                { MdiParent = CoreFormList.Principal() };
                                formInvoiceBillingEdit.Show();
                            }
                        }
                    }
                    else
                    {
                        var formInvoiceBillingEdit = new FormInvoiceBillingEdit(
                                        TypeOpenForm.Init, companyId, transactionId,
                                        transactionMasterId,"none")
                        { MdiParent = CoreFormList.Principal() };
                        formInvoiceBillingEdit.Show();
                    }
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
            var transactionID = coreWebTransaction.GetTransactionId(VariablesGlobales.Instance.User!.CompanyID, "tb_transaction_master_billing", 0);
            if (EsMesero == true)
            {
                using (var inputForm = new FormTypeCustomInputForm())
                {
                    if (inputForm.ShowDialog() == DialogResult.OK)
                    {

                        var objFormInvoiceList = new FormInvoiceBillingEdit(
                                TypeOpenForm.Init,
                                VariablesGlobales.Instance.User!.CompanyID,
                                transactionID!.Value,
                                0, inputForm.InputText
                            )
                        { MdiParent = CoreFormList.Principal() };
                        objFormInvoiceList.Show();

                    }
                }
            }
            else
            {
                var objFormInvoiceList = new FormInvoiceBillingEdit(
                                TypeOpenForm.Init,
                                VariablesGlobales.Instance.User!.CompanyID,
                                transactionID!.Value,
                                0,"none"
                            )
                { MdiParent = CoreFormList.Principal() };
                objFormInvoiceList.Show();
            }
        }
        public void Print(object? sender, EventArgs? args)
        {
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
                    var permited = coreWebPermission.UrlPermited("app_invoice_billing", "index", UrlSuffix!, menuTop, menuLeft, menuBodyReport, menuBodyTop, menuHiddenPopup);

                    if (!permited)
                        throw new Exception(NotAccessControl);

                    var resultPermission = coreWebPermission.UrlPermissionCmd("app_invoice_billing", "index", UrlSuffix!, role, user, menuTop, menuLeft, menuBodyReport, menuBodyTop, menuHiddenPopup);
                    if (resultPermission == PermissionNone)
                        throw new Exception(NotAccessFunction);
                }

                var transactionNumber = txtFiltrar.Text;

                if (string.IsNullOrEmpty(transactionNumber))
                    throw new Exception(NotParameter);

                var objTm = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterModel>().GetRowByTransactionNumber(user.CompanyID, transactionNumber);

                if (objTm == null)
                    throw new Exception("NO SE ENCONTRO EL DOCUMENTO");


                var formInvoiceBillingEdit = new FormInvoiceBillingEdit(
                        TypeOpenForm.Init, objTm.CompanyID, objTm.TransactionID,
                        objTm.TransactionMasterID,"none"
                    )
                    { MdiParent = CoreFormList.Principal() };
                formInvoiceBillingEdit.Show();
            }
            catch (Exception ex)
            {
                new CoreWebRenderInView().GetMessageAlert(TypeError.Error, "Error", $"Se produjo un error en {ex.Source} {ex.Message}", this);
            }
        }

        private void FormInvoiceBillingList_Resize(object sender, EventArgs e)
        {
            progressPanel.Size = Size;
        }
    }
}