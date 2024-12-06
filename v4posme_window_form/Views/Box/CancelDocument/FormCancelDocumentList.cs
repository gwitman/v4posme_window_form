using System.ComponentModel;
using System.Diagnostics;
using DevExpress.XtraGrid;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;
using v4posme_library.ModelsDto;
using v4posme_window.ControlCustom;
using v4posme_window.Interfaz;
using v4posme_window.Libraries;
using v4posme_window.Template;
using v4posme_window.Views.Box.ShareCapital;

namespace v4posme_window.Views.Box.CancelDocument
{
    public partial class FormCancelDocumentList : FormTypeList, IFormTypeList
    {
        private CoreWebRenderInView coreWebRender = new CoreWebRenderInView();
        private readonly ICoreWebPermission coreWebPermission = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebPermission>();
        private readonly ICoreWebTools coreWebTools = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();
        private readonly ICoreWebView coreWebView = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebView>();
        private readonly ITransactionMasterModel transactionMasterModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterModel>();
        private static readonly string? AppNeedAuthentication = VariablesGlobales.ConfigurationBuilder["APP_NEED_AUTHENTICATION"];
        private static readonly int? permissionNone = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_NONE"]);
        private static readonly string? urlSuffix = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX"];
        private TableCompanyDataViewDto? dataViewData;
        private CustomGridView? gridViewData;
        private BackgroundWorker backgroundWorker;
        public int? DataViewId { get; set; }
        public DateTime? Fecha { get; set; }
        public GridControl ObjGridControl { get; set; }

        public FormCancelDocumentList()
        {
            InitializeComponent();
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Fecha = DateTime.Now;
            DataViewId = 0;
            ObjGridControl = new GridControlCustom();
            btnEditar.Click += Edit;
            btnEliminar.Click += Delete;
            btnNuevo.Click += New;
            btnSearchTransaction.Click += SearchTransactionMaster;
            backgroundWorker = new();
        }

        private void FormCancelDocumentList_Enter(object sender, EventArgs e)
        {
            //este evento es cada vez que el formulario tiene el focus
            if (!backgroundWorker.IsBusy)
            {
                FormCancelDocumentList_Load(sender, e);
            }
        }

        private void FormCancelDocumentList_Load(object sender, EventArgs e)
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += (ob, ev) => { List(); };
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
                    coreWebRender.GetMessageAlert(TypeError.Error, "Error", $@"Error al cargar datos: {evb.Error.Message}", this);
                    return;
                }

                if (evb.Cancelled)
                {
                    coreWebRender.GetMessageAlert(TypeError.Error, "Error", $@"Operación cancelada por el usuario", this);
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
            var resultPermission = 0;

            if (AppNeedAuthentication!.Equals("true"))
            {
                var permited = coreWebPermission.UrlPermited("app_box_canceldocument", "index", urlSuffix!,
                    VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                    VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                    VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_CONTROL"]);
                }

                resultPermission = coreWebPermission.UrlPermissionCmd("app_box_canceldocument", "index", urlSuffix!,
                    VariablesGlobales.Instance.Role, VariablesGlobales.Instance.User,
                    VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                    VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                    VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_FUNCTION"]);
                }
            }

            var objComponent = coreWebTools.GetComponentIdByComponentName("tb_transaction_master_cancel_invoice");
            if (objComponent is null)
            {
                coreWebRender.GetMessageAlert(TypeError.Error, "Error", "00409 EL COMPONENTE 'tb_transaction_master_cancel_invoice' NO EXISTE...", this);
                return;
            }

            var callerIdList = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["CALLERID_LIST"]);
            Dictionary<string, string> parameters;
            if (DataViewId == 0)
            {
                var targetComponentId = VariablesGlobales.Instance.Company!.FlavorID!;
                parameters = new Dictionary<string, string>
                {
                    { "{companyID}", VariablesGlobales.Instance.User!.CompanyID.ToString() }
                };

                dataViewData = coreWebView.GetViewDefault(VariablesGlobales.Instance.User, objComponent.ComponentID, callerIdList, targetComponentId, resultPermission, parameters);
                if (dataViewData is null)
                {
                    targetComponentId = 0;
                    parameters = new Dictionary<string, string>
                    {
                        { "{companyID}", VariablesGlobales.Instance.User.CompanyID.ToString() }
                    };
                    dataViewData = coreWebView.GetViewDefault(VariablesGlobales.Instance.User, objComponent.ComponentID, callerIdList, targetComponentId, resultPermission, parameters);
                }
            }
            else
            {
                parameters = new Dictionary<string, string>
                {
                    { "{companyID}", VariablesGlobales.Instance.User!.CompanyID.ToString() }
                };

                dataViewData = coreWebView.GetViewByDataViewId(VariablesGlobales.Instance.User, objComponent.ComponentID, DataViewId!.Value, callerIdList, resultPermission, parameters);
            }
        }

        public void RefreshData()
        {
            ObjGridControl.DataSource = null;
            CoreWebRenderInView.RenderGrid(dataViewData!, "sharecapital", ObjGridControl, true, true);
            gridViewData = (CustomGridView)ObjGridControl.MainView;
            gridViewData.RefreshData();
            gridViewData.Columns.ForEach(column =>
            {
                Debug.WriteLine(column.FieldName);
                column.OptionsColumn.ReadOnly = true;
                column.OptionsColumn.FixedWidth = true;
            });
            ObjGridControl.Refresh();
        }

        public void Delete(object? sender, EventArgs? args)
        {
            var result = coreWebRender.XtraMessageBoxArgs(TypeError.Error, "Eliminar", "¿Seguro desea eliminar el abono seleccionado? Esta acción no se puede revertir.");

            if (result == DialogResult.No)
            {
                return;
            }

            if (gridViewData.RowCount < 0)
            {
                coreWebRender.GetMessageAlert(TypeError.Warning, "Cancelacion Factura", "No hay datos en la tabla", this);
                return;
            }

            var focused = gridViewData.FocusedRowHandle;
            if (focused < 0)
            {
                coreWebRender.GetMessageAlert(TypeError.Warning, "Cancelacion Factura", "Seleccione un abono a editar", this);
                return;
            }


            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += (ob, ev) =>
            {
                var rowIndex = gridViewData.GetSelectedRows();
                foreach (var indexRow in rowIndex)
                {
                    var companyID = Convert.ToInt32(gridViewData.GetRowCellValue(focused, "companyID"));
                    var transactionId = Convert.ToInt32(gridViewData.GetRowCellValue(focused, "transactionID"));
                    var transactionMasterID = Convert.ToInt32(gridViewData.GetRowCellValue(focused, "transactionMasterID"));
                    var formShareEdit = new FormCancelDocumentEdit(TypeOpenForm.Init, companyID, transactionMasterID, transactionId);
                    formShareEdit.ComandDelete();
                }
            };

            backgroundWorker.RunWorkerCompleted += (ob, ev) =>
            {
                coreWebRender = new();
                if (ev.Error is not null)
                {
                    coreWebRender.GetMessageAlert(TypeError.Error, "Error Eliminar", $"Se ha producido un error al eliminar {ev.Error.Message}", this);
                }
                else if (ev.Cancelled)
                {
                    coreWebRender.GetMessageAlert(TypeError.Warning, "Eliminar", "Se ha cancelado la eliminación de la factura", this);
                }
                else
                {
                    coreWebRender.GetMessageAlert(TypeError.Informacion, "Eliminar", "Se ha eliminado el abono de forma correcta", this);
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
            if (gridViewData.RowCount < 0)
            {
                coreWebRender.GetMessageAlert(TypeError.Warning, "Cancelacion Factura", "No hay datos en la tabla", this);
                return;
            }

            var focused = gridViewData.FocusedRowHandle;
            if (focused < 0)
            {
                coreWebRender.GetMessageAlert(TypeError.Warning, "Cancelacion Factura", "Seleccione un abono a editar", this);
                return;
            }

            var companyID = Convert.ToInt32(gridViewData.GetRowCellValue(focused, "companyID"));
            var transactionId = Convert.ToInt32(gridViewData.GetRowCellValue(focused, "transactionID"));
            var transactionMasterID = Convert.ToInt32(gridViewData.GetRowCellValue(focused, "transactionMasterID"));
            var formShareCapital = new FormCancelDocumentEdit(TypeOpenForm.Init, companyID, transactionMasterID, transactionId)
            {
                MdiParent = CoreFormList.Principal()
            };
            formShareCapital.Show();
        }

        public void New(object? sender, EventArgs? args)
        {
            var formShareCapital = new FormCancelDocumentEdit(TypeOpenForm.Init, 0, 0, 0)
            {
                MdiParent = CoreFormList.Principal()
            };
            formShareCapital.Show();
        }

        public void Print(object? sender, EventArgs? args)
        {
            var focused = gridViewData.FocusedRowHandle;
            if (focused < 0)
            {
                coreWebRender.GetMessageAlert(TypeError.Warning, "Cancelacion Factura", "Seleccione un abono a editar", this);
                return;
            }

            var companyID = Convert.ToInt32(gridViewData.GetRowCellValue(focused, "companyID"));
            var transactionId = Convert.ToInt32(gridViewData.GetRowCellValue(focused, "transactionID"));
            var transactionMasterID = Convert.ToInt32(gridViewData.GetRowCellValue(focused, "transactionMasterID"));
            var formShareEdit = new FormShareCapitalEdit(TypeOpenForm.NotInit, companyID, transactionMasterID, transactionId);
            formShareEdit.ComandPrinter();
        }

        public void PreRender()
        {
            lblTitulo.Text = @"LISTA DE DOCUNENTOS CANCELADOS";
            Text = lblTitulo.Text;
            var controlParent = centerPane;
            ObjGridControl.Name = "ObjGridControl";
            ObjGridControl.Parent = controlParent;
            ObjGridControl.Dock = DockStyle.Fill;
        }

        public void SearchTransactionMaster(object? sender, EventArgs? args)
        {
            var userNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];
            var notAccessControl = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_CONTROL"];
            var permissionNone = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_NONE"]);
            var appNeedAuthentication = VariablesGlobales.ConfigurationBuilder["APP_NEED_AUTHENTICATION"];
            var urlSuffix = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX"];
            var user = VariablesGlobales.Instance.User;
            if (user is null)
            {
                throw new Exception(userNotAutenticated);
            }

            var role = VariablesGlobales.Instance.Role;
            if (role is null)
            {
                throw new Exception("No hay configurado un Rol");
            }

            var company = VariablesGlobales.Instance.Company;
            if (company is null)
            {
                throw new Exception("No hay una compañía configurada");
            }

            if (appNeedAuthentication == "true")
            {
                var permited = coreWebPermission.UrlPermited("app_box_canceldocument", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(notAccessControl);
                }

                var resultPermission = coreWebPermission.UrlPermissionCmd("app_box_canceldocument", "index", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    throw new Exception(notAccessControl);
                }
            }

            var transactionNumber = txtFiltrar.Text;
            if (string.IsNullOrWhiteSpace(transactionNumber))
            {
                coreWebRender.GetMessageAlert(TypeError.Warning, "Filtrar", VariablesGlobales.ConfigurationBuilder["NOT_PARAMETER"], this);
                return;
            }

            var objTm = transactionMasterModel.GetRowByTransactionNumber(user.CompanyID, transactionNumber);
            if (objTm == null)
            {
                coreWebRender.GetMessageAlert(TypeError.Warning, "Filtrar", $"No se ha encontró el documento con número {transactionNumber}", this);
                return;
            }

            var frm = new FormCancelDocumentEdit(TypeOpenForm.Init, objTm.CompanyID, objTm.TransactionID, objTm.TransactionMasterID)
            {
                MdiParent = CoreFormList.Principal()
            };
            frm.Show();
        }
    }
}