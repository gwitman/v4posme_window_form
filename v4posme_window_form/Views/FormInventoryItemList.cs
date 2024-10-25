using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using v4posme_library.Libraries;
using v4posme_library.ModelsDto;
using v4posme_window.Interfaz;
using v4posme_window.Libraries;
using v4posme_window.Template;
using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Models;
using DevExpress.XtraScheduler.Outlook.Interop;
using v4posme_library.Libraries.CustomModels;
using v4posme_window.ControlCustom;
using Exception = System.Exception;
using ESC_POS_USB_NET.Printer;

namespace v4posme_window.Views
{
    public partial class FormInventoryItemList : FormTypeList, IFormTypeList
    {
        private readonly CoreWebRenderInView _coreWebRender = new CoreWebRenderInView();
        private readonly ICoreWebParameter _objInterfazCoreWebParameter = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebParameter>();
        private readonly ICoreWebPermission _coreWebPermission = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebPermission>();
        private readonly ICoreWebTools _coreWebTools = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();
        private readonly ICoreWebTools _objInterfazCoreWebTools = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();
        private readonly ICoreWebWorkflow _objInterfazCoreWebWorkflow = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebWorkflow>();
        private readonly ICoreWebView _coreWebView = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebView>();
        private readonly IItemModel _itemModel = VariablesGlobales.Instance.UnityContainer.Resolve<IItemModel>();
        private static readonly string? UserNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];
        private static readonly string? NotParameter = VariablesGlobales.ConfigurationBuilder["NOT_PARAMETER"];
        private static readonly string? AppNeedAuthentication = VariablesGlobales.ConfigurationBuilder["APP_NEED_AUTHENTICATION"];
        private static readonly string? NotAccessFunction = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_FUNCTION"];
        private static readonly string? NotAccessControl = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_CONTROL"];
        private static readonly int? PermissionNone = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_NONE"]);
        private static readonly string? UrlSuffix = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX"];

        private TableCompanyDataViewDto? _dataViewData;
        private CustomGridView? _gridViewData;

        public int? DataViewId { get; set; }
        public DateTime? Fecha { get; set; }
        public GridControl ObjGridControl { get; set; }

        public FormInventoryItemList()
        {
            InitializeComponent();

            // Suscribir al manejador de excepciones global
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            btnImprimir.Visible = true;
            Fecha = DateTime.Now;
            DataViewId = 0;
            ObjGridControl = new GridControlCustom();
            btnEditar.Click += Edit;
            btnEliminar.Click += Delete;
            btnNuevo.Click += New;
            btnImprimir.Click += Print;
            btnSearchTransaction.Click += SearchTransactionMaster;
        }

        private void FormInventoryItemList_Enter(object sender, EventArgs e)
        {
            //este evento es cada vez que el formulario tiene el focus
            if (!backgroundWorker.IsBusy)
            {
                FormInventoryItemList_Load(sender, e);
            }
        }

        private void FormInventoryItemList_Load(object sender, EventArgs e)
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
            var resultPermission = 0;

            if (AppNeedAuthentication!.Equals("true"))
            {
                var permited = _coreWebPermission.UrlPermited("app_inventory_item", "index", UrlSuffix!,
                    VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                    VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                    VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    _coreWebRender.GetMessageAlert(TypeError.Error, "Permisos", "No tiene acceso a los controles", this);
                    return;
                }

                resultPermission = _coreWebPermission.UrlPermissionCmd("app_inventory_item", "index", UrlSuffix!,
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

            var objComponent = _coreWebTools.GetComponentIdByComponentName("tb_item");
            if (objComponent is null)
            {
                _coreWebRender.GetMessageAlert(TypeError.Error, "Error", "00409 EL COMPONENTE 'tb_item' NO EXISTE...", this);
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
            CoreWebRenderInView.RenderGrid(_dataViewData!, "item", ObjGridControl, true);
            _gridViewData = (CustomGridView)ObjGridControl.MainView;
            _gridViewData.RefreshData();
            _gridViewData.Columns.ForEach(column =>
            {
                column.OptionsColumn.ReadOnly = true;
                column.OptionsColumn.FixedWidth = true;
                switch (column.Name)
                {
                    case "colCodigo":
                        column.Width = 200;
                        break;
                    case "colBarra":
                        column.Width = 300;
                        break;
                }
            });
            ObjGridControl.Refresh();
        }

        public void Delete(object? sender, EventArgs? args)
        {
            var result = _coreWebRender.XtraMessageBoxArgs(TypeError.Error, "Eliminar", "¿Seguro desea eliminar los articulos seleccionados? Esta acción no se puede revertir.");

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


            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += (ob, ev) =>
            {
                var rowIndex = _gridViewData.GetSelectedRows();
                foreach (var indexRow in rowIndex)
                {
                    var itemId = Convert.ToInt32(_gridViewData.GetRowCellValue(indexRow, "itemID").ToString());
                    var objFormCustomerEdit = new FormInventoryItemEdit(TypeOpenForm.NotInit, itemId);
                    objFormCustomerEdit.ComandDelete();
                }
            };

            backgroundWorker.RunWorkerCompleted += (ob, ev) =>
            {
                Debug.WriteLine(ev);
                if (ev.Error is not null)
                {
                    _coreWebRender.GetMessageAlert(TypeError.Error, "Error Eliminar", $"Se ha producido un error al eliminar {ev.Error.Message}", this);
                }
                else if (ev.Cancelled)
                {
                    _coreWebRender.GetMessageAlert(TypeError.Warning, "Eliminar", "Se ha cancelado la eliminación de la factura", this);
                }
                else
                {
                    _coreWebRender.GetMessageAlert(TypeError.Informacion, "Eliminar", "Se han eliminado los articulos de forma correcta", this);
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
            CoreWebRenderInView objCoreWebRenderInView = new CoreWebRenderInView();
            if (((GridView)ObjGridControl.MainView).SelectedRowsCount > 0)
            {
                var itemRow = ((GridView)ObjGridControl.MainView).GetFocusedRowCellValue("itemID");
                var formEdit = new FormInventoryItemEdit(TypeOpenForm.Init, (int)itemRow)
                {
                    MdiParent = CoreFormList.Principal()
                };
                formEdit.Show();
            }
        }

        public void New(object? sender, EventArgs? args)
        {
            var formEdit = new FormInventoryItemEdit(TypeOpenForm.Init, 0)
            {
                MdiParent = CoreFormList.Principal()
            };
            formEdit.Show();
        }

        public void Print(object? sender, EventArgs? args)
        {
            var cantidadImprimirFrm = new FormInventoryItemCantidadImprimir();
            var dialogResult = cantidadImprimirFrm.ShowDialog(this);
            if (dialogResult == DialogResult.Cancel)
            {
                return;
            }

            var user = VariablesGlobales.Instance.User;
            if (user is null)
            {
                throw new Exception("Usuario no logeado");
            }

            var selectedRowsCount = _gridViewData.SelectedRowsCount;
            if (selectedRowsCount > 0)
            {
                for (int i = 0; i < selectedRowsCount; i++)
                {
                    var itemId = _gridViewData.GetRowCellValue(i, "itemID");
                    var item = _itemModel.GetRowByPk(user.CompanyID, Convert.ToInt32(itemId));
                    if (item is null)
                    {
                        continue;
                    }

                    _coreWebRender.PrintBarCodeItem(item, cantidadImprimirFrm.CantidadImprimir);
                }
            }
        }

        public void PreRender()
        {
            lblTitulo.Text = @"LISTA DE PRODUCTOS";
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
                    var permited = _coreWebPermission.UrlPermited("app_inventory_item", "index", UrlSuffix!, menuTop, menuLeft, menuBodyReport, menuBodyTop, menuHiddenPopup);

                    if (!permited)
                        throw new Exception(NotAccessControl);

                    var resultPermission = _coreWebPermission.UrlPermissionCmd("app_inventory_item", "index", UrlSuffix!, role, user, menuTop, menuLeft, menuBodyReport, menuBodyTop, menuHiddenPopup);
                    if (resultPermission == PermissionNone)
                        throw new Exception(NotAccessFunction);
                }

                var filtrarText = txtFiltrar.Text;

                if (string.IsNullOrEmpty(filtrarText))
                    throw new Exception(NotParameter);

                //var itemRow = _gridViewData.GetFocusedRowCellValue("Codigo");
                var objItem = _itemModel.GetRowByCode(user.CompanyID, filtrarText);

                if (objItem is null)
                {
                    XtraMessageBox.Show("No se encontró el articulo el número indicado", "Buscar");
                    return;
                }

                var formInventoryItem = new FormInventoryItemEdit(TypeOpenForm.Init, objItem.ItemID)
                    { MdiParent = CoreFormList.Principal() };
                formInventoryItem.Show();
            }
            catch (Exception ex)
            {
                new CoreWebRenderInView().GetMessageAlert(TypeError.Error, "Error", $"Se produjo un error en {ex.Source} {ex.Message}", this);
            }
        }
    }
}