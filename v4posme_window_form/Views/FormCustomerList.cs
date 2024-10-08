using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;
using v4posme_library.ModelsDto;
using v4posme_window.ControlCustom;
using v4posme_window.Interfaz;
using v4posme_window.Template;
using v4posme_window.Libraries;
using v4posme_library.Models;

namespace v4posme_window.Views
{
    public partial class FormCustomerList : FormTypeList, IFormTypeList
    {
        private readonly CoreWebRenderInView _coreWebRender = new CoreWebRenderInView();
        private readonly ICoreWebPermission _coreWebPermission = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebPermission>();
        private readonly ICoreWebTools _coreWebTools = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();
        private readonly ICoreWebTransaction _coreWebTransaction = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTransaction>();
        private readonly ICoreWebWorkflow _objInterfazCoreWebWorkflow = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebWorkflow>();
        private readonly ICoreWebView _coreWebView = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebView>();
        private readonly ICustomerModel _customerModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerModel>();
        private static readonly string? UserNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];
        private static readonly string? NotParameter = VariablesGlobales.ConfigurationBuilder["NOT_PARAMETER"];
        private static readonly string? AppNeedAuthentication = VariablesGlobales.ConfigurationBuilder["APP_NEED_AUTHENTICATION"];
        private static readonly string? NotAccessFunction = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_FUNCTION"];
        private static readonly string? NotAccessControl = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_CONTROL"];
        private static readonly int? PermissionNone = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_NONE"]);
        private static readonly string? UrlSuffix = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX"];

        private TableCompanyDataViewDto? _dataViewData;
        private CustomGridView? _gridViewData;
        private BackgroundWorker _backgroundWorker;
        public int? DataViewId { get; set; }
        public DateTime? Fecha { get; set; }
        public GridControl ObjGridControl { get; set; }

        public FormCustomerList()
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
            _backgroundWorker = new();
        }

        private void FormCustomerList_Enter(object sender, EventArgs e)
        {
            //este evento es cada vez que el formulario tiene el focus
            if (!_backgroundWorker.IsBusy)
            {
                FormCustomerList_Load(sender, e);
            }
        }

        private void FormCustomerList_Load(object sender, EventArgs e)
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += (ob, ev) => { List(); };
            _backgroundWorker.RunWorkerCompleted += (obb, evb) =>
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

            _backgroundWorker.RunWorkerAsync();
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
                var permited = _coreWebPermission.UrlPermited("app_cxc_customer", "index", UrlSuffix!,
                    VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                    VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                    VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    _coreWebRender.GetMessageAlert(TypeError.Error, "Permisos", "No tiene acceso a los controles", this);
                    return;
                }

                resultPermission = _coreWebPermission.UrlPermissionCmd("app_cxc_customer", "index", UrlSuffix!,
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

            var objComponent = _coreWebTools.GetComponentIdByComponentName("tb_customer");
            if (objComponent is null)
            {
                _coreWebRender.GetMessageAlert(TypeError.Error, "Error", "00409 EL COMPONENTE 'tb_customer' NO EXISTE...", this);
                return;
            }

            var callerIdList = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["CALLERID_LIST"]);
            Dictionary<string, string> parameters;
            if (DataViewId == 0)
            {
                var targetComponentId = VariablesGlobales.Instance.Company!.FlavorID!;
                parameters = new Dictionary<string, string>
                {
                    { "{companyID}", VariablesGlobales.Instance.User!.CompanyID.ToString() },
                    { "{fecha}", Fecha!.Value.ToString("yyyy-MM-dd") }
                };

                _dataViewData = _coreWebView.GetViewDefault(VariablesGlobales.Instance.User, objComponent.ComponentID, callerIdList, targetComponentId, resultPermission, parameters);
                if (_dataViewData is null)
                {
                    targetComponentId = 0;
                    parameters = new Dictionary<string, string>
                    {
                        { "{companyID}", VariablesGlobales.Instance.User.CompanyID.ToString() },
                        { "{fecha}", Fecha.Value.ToString("yyyy-MM-dd") }
                    };
                    _dataViewData = _coreWebView.GetViewDefault(VariablesGlobales.Instance.User, objComponent.ComponentID, callerIdList, targetComponentId, resultPermission, parameters);
                }
            }
            else
            {
                parameters = new Dictionary<string, string>
                {
                    { "{companyID}", VariablesGlobales.Instance.User!.CompanyID.ToString() }
                };

                _dataViewData = _coreWebView.GetViewByDataViewId(VariablesGlobales.Instance.User, objComponent.ComponentID, DataViewId!.Value, callerIdList, resultPermission, parameters);
            }
        }

        private void RefreshData()
        {
            CoreWebRenderInView.RenderGrid(_dataViewData!, "customer", ObjGridControl, false,true);
            _gridViewData = (CustomGridView)ObjGridControl.MainView;
            _gridViewData.RefreshData();
            Debug.WriteLine(_gridViewData.Columns);
            _gridViewData.Columns.ForEach(column =>
            {
                column.OptionsColumn.ReadOnly = true;
                column.OptionsColumn.FixedWidth = true;
                switch (column.Name)
                {
                    case "colCodigo":
                        column.Width = 200;
                        break;
                    case "colNombre":
                        column.Width = 400;
                        break;
                }
            });
            ObjGridControl.Refresh();
        }

        public void Delete(object? sender, EventArgs? args)
        {
            var objInterfazCoreWebRenderInView = new CoreWebRenderInView();
            var selectedValue = _gridViewData!.SelectedRowsCount;
            if (selectedValue <= 0)
            {
                objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Warning, "Eliminar", "Seleccione un valor de la lista para eliminar", this);
                return;
            }

            var result = objInterfazCoreWebRenderInView.XtraMessageBoxArgs(TypeError.Informacion, "Eliminar", "¿Seguro desea el iminar el cliente seleccionado? Esta acción no se peude revertir");
            if (result==DialogResult.No)
            {
                return;
            }
            var objInterfazCoreWebPermission = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebPermission>();
            var customerModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerModel>();
            var notAllEdit = VariablesGlobales.ConfigurationBuilder["NOT_ALL_EDIT"];
            var user = VariablesGlobales.Instance.User;
            if (user is null)
            {
                throw new Exception(UserNotAutenticated);
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

            var resultPermission = 0;
            if (AppNeedAuthentication == "true")
            {
                var permited = objInterfazCoreWebPermission.UrlPermited("app_cxc_customer", "index", UrlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(NotAccessControl);
                }

                resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd("app_cxc_customer", "delete", UrlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == PermissionNone)
                {
                    throw new Exception(notAllEdit);
                }
            }

            var entityId = Convert.ToInt32(_gridViewData.GetRowCellValue(_gridViewData.FocusedRowHandle, "entityID"));
            if (entityId == 0)
            {
                throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_PARAMETER"]);
            }

            var objCustomer = customerModel.GetRowByPk(user.CompanyID, user.BranchID, entityId);
            if (objCustomer == null)
            {
                throw new Exception("No es posible eliminar el cliente, no existe el registro");
            }

            var appCustomer01 = VariablesGlobales.ConfigurationBuilder["APP_CUSTOMER01"];
            var appCustomer02 = VariablesGlobales.ConfigurationBuilder["APP_CUSTOMER02"];
            if (entityId == Convert.ToInt32(appCustomer01))
            {
                throw new Exception("No es posible eliminar el cliente, edite el nombre");
            }

            if (entityId == Convert.ToInt32(appCustomer02))
            {
                throw new Exception("No es posible eliminar el cliente, edite el nombre");
            }

            //PERMISO SOBRE EL REGISTRO
            var permissionMe = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_ME"]);
            if (resultPermission == permissionMe && objCustomer.CreatedBy!.Value != user.UserID)
            {
                throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_DELETE"]);
            }

            //PERMISO PUEDE ELIMINAR EL REGISTRO SEGUN EL WORKFLOW
            var commandEliminable = VariablesGlobales.ConfigurationBuilder["COMMAND_ELIMINABLE"];
            if (!_objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_customer", "statusID", objCustomer.StatusID!.Value, Convert.ToInt32(commandEliminable), user.CompanyID, user.BranchID, role.RoleID)!.Value)
            {
                throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_WORKFLOW_DELETE"]);
            }

            try
            {
                _customerModel.DeleteAppPosme(user.CompanyID, user.BranchID, entityId);
                objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Informacion, "Eliminar", "Se ha eliminado con exito", this);
                FormCustomerList_Load(sender!, args!);
            }
            catch (Exception e)
            {
                objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Eliminar", $"Se ha producido el siguiente error: {e.Message}", this);
            }
            
        }

        public void Edit(object? sender, EventArgs? args)
        {
            var resultPermission = 0;
            var user = VariablesGlobales.Instance.User;
            if (user is null)
            {
                return;
            }

            if (string.Compare(AppNeedAuthentication, "true", StringComparison.InvariantCultureIgnoreCase)==0)
            {
                var permited = _coreWebPermission.UrlPermited("app_cxc_customer", "index", UrlSuffix!,
                    VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                    VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                    VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    _coreWebRender.GetMessageAlert(TypeError.Error, "Permisos", "No tiene acceso a los controles", this);
                    return;
                }

                resultPermission = _coreWebPermission.UrlPermissionCmd("app_cxc_customer", "index", UrlSuffix!,
                    VariablesGlobales.Instance.Role, user,
                    VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                    VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                    VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == PermissionNone)
                {
                    _coreWebRender.GetMessageAlert(TypeError.Error, "Permisos", "No se encontraron permisos", this);
                    return;
                }
            }

            var objComponent = _coreWebTools.GetComponentIdByComponentName("tb_customer");
            if (objComponent is null)
            {
                _coreWebRender.GetMessageAlert(TypeError.Error, "Error", "00409 EL COMPONENTE 'tb_customer' NO EXISTE...", this);
                return;
            }

            if (_gridViewData is null)
            {
                return;
            }

            var selectedValue = _gridViewData.GetRowCellValue(_gridViewData.FocusedRowHandle, "entityID").ToString();
            var selectedCliente = _customerModel.GetRowByEntity(user.CompanyID, Convert.ToInt32(selectedValue));
            if (selectedCliente is null)
            {
                _coreWebRender.GetMessageAlert(TypeError.Error, "Error", "No existe el cliente seleccionado, intente nuevamente", this);
                return;
            }

            var frmCustomerEdit = new FormCustomerEdit(TypeRender.Edit, selectedCliente.EntityId)
            {
                MdiParent = CoreFormList.Principal()
            };
            frmCustomerEdit.Show();
        }

        public void New(object? sender, EventArgs? args)
        {
            var frmCustomerEdit = new FormCustomerEdit(TypeRender.New, 0)
            {
                MdiParent = CoreFormList.Principal()
            };
            frmCustomerEdit.Show();
        }

        public void Print(object? sender, EventArgs? args)
        {
        }

        public void PreRender()
        {
            lblTitulo.Text = @"LISTA DE CLIENTES";
            Text = lblTitulo.Text;
            PanelControl controlParent = this.centerPane;
            ObjGridControl.Name = "ObjGridControl";
            ObjGridControl.Parent = controlParent;
            ObjGridControl.Dock = DockStyle.Fill;
        }

        public void SearchTransactionMaster(object? sender, EventArgs? args)
        {
             var resultPermission = 0;
            var user = VariablesGlobales.Instance.User;
            if (user is null)
            {
                return;
            }

            if (string.Compare(AppNeedAuthentication, "true", StringComparison.InvariantCultureIgnoreCase)==0)
            {
                var permited = _coreWebPermission.UrlPermited("app_cxc_customer", "index", UrlSuffix!,
                    VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                    VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                    VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    _coreWebRender.GetMessageAlert(TypeError.Error, "Permisos", "No tiene acceso a los controles", this);
                    return;
                }

                resultPermission = _coreWebPermission.UrlPermissionCmd("app_cxc_customer", "index", UrlSuffix!,
                    VariablesGlobales.Instance.Role, user,
                    VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                    VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                    VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == PermissionNone)
                {
                    _coreWebRender.GetMessageAlert(TypeError.Error, "Permisos", "No se encontraron permisos", this);
                    return;
                }
            }

            var objComponent = _coreWebTools.GetComponentIdByComponentName("tb_customer");
            if (objComponent is null)
            {
                _coreWebRender.GetMessageAlert(TypeError.Error, "Error", "00409 EL COMPONENTE 'tb_customer' NO EXISTE...", this);
                return;
            }

            if (_gridViewData is null)
            {
                return;
            }

            var filterText = txtFiltrar.Text;
            if (string.IsNullOrWhiteSpace(filterText))
            {
                return;
            }

            var selectedCliente = _customerModel.GetRowByIdentification(user.CompanyID, filterText);
            if (selectedCliente is null)
            {
                _coreWebRender.GetMessageAlert(TypeError.Error, "Error", "No existe el cliente seleccionado, intente nuevamente", this);
                return;
            }

            var frmCustomerEdit = new FormCustomerEdit(TypeRender.Edit, selectedCliente.EntityID)
            {
                MdiParent = CoreFormList.Principal()
            };
            frmCustomerEdit.Show();
        }

    }
}