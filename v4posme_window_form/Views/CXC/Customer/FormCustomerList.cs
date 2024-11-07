using System.ComponentModel;
using System.Diagnostics;
using DevExpress.XtraEditors;
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

namespace v4posme_window.Views.CXC.Customer
{
    public partial class FormCustomerList : FormTypeList, IFormTypeList
    {
        private readonly CoreWebRenderInView coreWebRender = new();
        private readonly ICoreWebPermission coreWebPermission = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebPermission>();
        private readonly ICoreWebTools coreWebTools = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();
        private readonly ICoreWebView coreWebView = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebView>();
        private readonly ICustomerModel customerModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerModel>();
        private static readonly string? appNeedAuthentication = VariablesGlobales.ConfigurationBuilder["APP_NEED_AUTHENTICATION"];
        private static readonly int? permissionNone = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_NONE"]);
        private static readonly string? urlSuffix = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX"];

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

            if (appNeedAuthentication!.Equals("true"))
            {
                var permited = coreWebPermission.UrlPermited("app_cxc_customer", "index", urlSuffix!,
                    VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                    VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                    VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    coreWebRender.GetMessageAlert(TypeError.Error, "Permisos", "No tiene acceso a los controles", this);
                    return;
                }

                resultPermission = coreWebPermission.UrlPermissionCmd("app_cxc_customer", "index", urlSuffix!,
                    VariablesGlobales.Instance.Role, VariablesGlobales.Instance.User,
                    VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                    VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                    VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    coreWebRender.GetMessageAlert(TypeError.Error, "Permisos", "No se encontraron permisos", this);
                    return;
                }
            }

            var objComponent = coreWebTools.GetComponentIdByComponentName("tb_customer");
            if (objComponent is null)
            {
                coreWebRender.GetMessageAlert(TypeError.Error, "Error", "00409 EL COMPONENTE 'tb_customer' NO EXISTE...", this);
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

        private void RefreshData()
        {
            CoreWebRenderInView.RenderGrid(_dataViewData!, "customer", ObjGridControl, true,true);
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
            var result = coreWebRender.XtraMessageBoxArgs(TypeError.Error, "Eliminar", "¿Seguro desea eliminar los clientes seleccionados? Esta acción no se puede revertir.");

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
                coreWebRender.GetMessageAlert(TypeError.Error, @"Error eliminando", "Debe seleccionar un registro", this);
                return;
            }
            

            backgroundWorker=new BackgroundWorker();
            backgroundWorker.DoWork += (ob, ev) =>
            {
                var rowIndex = _gridViewData.GetSelectedRows();
                foreach (var indexRow in rowIndex)
                {
                    var entityId = Convert.ToInt32(_gridViewData.GetRowCellValue(indexRow, "entityID").ToString());
                    var objFormCustomerEdit = new FormCustomerEdit(TypeOpenForm.Init, entityId);
                    objFormCustomerEdit.ComandDelete();
                }
            };
            
            backgroundWorker.RunWorkerCompleted += (ob, ev) =>
            {   
                Debug.WriteLine(ev);
                if (ev.Error is not null)
                {
                    coreWebRender.GetMessageAlert(TypeError.Error, "Error Eliminar", $"Se ha producido un error al eliminar {ev.Error.Message}", this);

                }else if (ev.Cancelled)
                {
                    coreWebRender.GetMessageAlert(TypeError.Warning, "Eliminar", "Se ha cancelado la eliminación de la cliente", this);
                }
                else
                {
                    coreWebRender.GetMessageAlert(TypeError.Informacion, "Eliminar", "Se han eliminado los clientes de forma correcta", this);
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
            var resultPermission = 0;
            var user = VariablesGlobales.Instance.User;
            if (user is null)
            {
                return;
            }

            if (string.Compare(appNeedAuthentication, "true", StringComparison.InvariantCultureIgnoreCase)==0)
            {
                var permited = coreWebPermission.UrlPermited("app_cxc_customer", "index", urlSuffix!,
                    VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                    VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                    VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    coreWebRender.GetMessageAlert(TypeError.Error, "Permisos", "No tiene acceso a los controles", this);
                    return;
                }

                resultPermission = coreWebPermission.UrlPermissionCmd("app_cxc_customer", "index", urlSuffix!,
                    VariablesGlobales.Instance.Role, user,
                    VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                    VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                    VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    coreWebRender.GetMessageAlert(TypeError.Error, "Permisos", "No se encontraron permisos", this);
                    return;
                }
            }

            var objComponent = coreWebTools.GetComponentIdByComponentName("tb_customer");
            if (objComponent is null)
            {
                coreWebRender.GetMessageAlert(TypeError.Error, "Error", "00409 EL COMPONENTE 'tb_customer' NO EXISTE...", this);
                return;
            }

            if (_gridViewData is null)
            {
                return;
            }

            var selectedValue = _gridViewData.GetRowCellValue(_gridViewData.FocusedRowHandle, "entityID").ToString();
            var selectedCliente = customerModel.GetRowByEntity(user.CompanyID, Convert.ToInt32(selectedValue));
            if (selectedCliente is null)
            {
                coreWebRender.GetMessageAlert(TypeError.Error, "Error", "No existe el cliente seleccionado, intente nuevamente", this);
                return;
            }

            var frmCustomerEdit = new FormCustomerEdit(TypeOpenForm.Init, selectedCliente.EntityId)
            {
                MdiParent = CoreFormList.Principal()
            };
            frmCustomerEdit.Show();
        }

        public void New(object? sender, EventArgs? args)
        {
            var frmCustomerEdit = new FormCustomerEdit(TypeOpenForm.Init, 0)
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

            if (string.Compare(appNeedAuthentication, "true", StringComparison.InvariantCultureIgnoreCase)==0)
            {
                var permited = coreWebPermission.UrlPermited("app_cxc_customer", "index", urlSuffix!,
                    VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                    VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                    VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    coreWebRender.GetMessageAlert(TypeError.Error, "Permisos", "No tiene acceso a los controles", this);
                    return;
                }

                resultPermission = coreWebPermission.UrlPermissionCmd("app_cxc_customer", "index", urlSuffix!,
                    VariablesGlobales.Instance.Role, user,
                    VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                    VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                    VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    coreWebRender.GetMessageAlert(TypeError.Error, "Permisos", "No se encontraron permisos", this);
                    return;
                }
            }

            var objComponent = coreWebTools.GetComponentIdByComponentName("tb_customer");
            if (objComponent is null)
            {
                coreWebRender.GetMessageAlert(TypeError.Error, "Error", "00409 EL COMPONENTE 'tb_customer' NO EXISTE...", this);
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

            var selectedCliente = customerModel.GetRowByIdentification(user.CompanyID, filterText);
            if (selectedCliente is null)
            {
                coreWebRender.GetMessageAlert(TypeError.Error, "Error", "No existe el cliente seleccionado, intente nuevamente", this);
                return;
            }

            var frmCustomerEdit = new FormCustomerEdit(TypeOpenForm.Init, selectedCliente.EntityID)
            {
                MdiParent = CoreFormList.Principal()
            };
            frmCustomerEdit.Show();
        }

    }
}