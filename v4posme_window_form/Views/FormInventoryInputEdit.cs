using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomHelper;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Models;
using v4posme_library.ModelsDto;
using v4posme_window.Dto;
using v4posme_window.Interfaz;
using v4posme_window.Libraries;
using v4posme_window.Reportes;
using v4posme_window.Template;
using Exception = System.Exception;

namespace v4posme_window.Views
{
    public partial class FormInventoryInputEdit : FormTypeHeadEdit, IFormTypeEdit
    {
        #region Campos

        private TypeOpenForm TypeOpen { get; set; }
        private int TransactionMasterId = 0;
        private int TransactionId = 0;
        private int _txtProviderId = 0;
        private TypeRender _typeRender;
        private int? txtTransactionMasterIDOrdenCompra;
        private BackgroundWorker? _backgroundWorker;
        private RenderFileGridControl _renderGridFiles;
        private string? objParameterCoreViewCustomScrollInDetatailPurshase;
        private string? objParameterInventoryUrlPrinterInputunpostOnlyQuantity;
        private string? objParameterInventoryUrlPrinterInputunpostShowOpciones;
        private string? objParameterUrlPrinter;
        private string? objParameterMasive;

        #endregion

        #region Modelos

        public List<TbCompanyCurrencyDto>? ObjListCurrency { get; set; }

        public TbListPrice? ObjListPrice { get; set; }

        public List<TbCatalogItem>? ObjListTypePreice { get; set; }

        public TbNaturale? ProviderNaturalDefault { get; set; }

        public TbProvider ProviderDefault { get; set; }

        public TbLegal? ObjLegalDefault { get; set; }

        public TbNaturale? ObjNaturalDefault { get; set; }

        public TbProvider ObjProvider { get; set; }

        public TbTransactionMaster? ObjTmOrdenCompra { get; set; }

        public List<TbWorkflowStage>? ObjListWorkflowStage { get; set; }

        public List<TbTransactionMasterDetailDto> ObjTmd { get; set; }

        public TbTransactionMasterDto? ObjTm { get; set; }

        public List<TbUserWarehouseDto>? ObjlistWarehouse { get; set; }

        public string WarehouseDefault { get; set; }

        public TbComponent? ObjComponentOrdenCompra { get; set; }

        public TbComponent? ObjComponent { get; set; }

        public TbComponent? ObjComponentProvider { get; set; }

        public TbComponent? ObjComponentItem { get; set; }

        #endregion

        #region Librerias

        private readonly ICoreWebAccounting _objInterfazCoreWebAccounting = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebAccounting>();
        private readonly ICoreWebAuditoria _objInterfazCoreWebAuditoria = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebAuditoria>();
        private readonly ICoreWebCatalog _objInterfazCoreWebCatalog = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCatalog>();
        private readonly ICoreWebConcept _objInterfazCoreWebConcept = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebConcept>();
        private readonly ICoreWebParameter _objInterfazCoreWebParameter = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebParameter>();
        private readonly ICoreWebTransaction _objInterfazCoreWebTransaction = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTransaction>();
        private readonly ICoreWebCurrency _objInterfazCoreWebCurrency = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCurrency>();
        private readonly ICoreWebTools _objInterfazCoreWebTools = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();
        private readonly ICoreWebCounter _objInterfazCoreWebCounter = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCounter>();
        private readonly ICoreWebWorkflow _objInterfazCoreWebWorkflow = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebWorkflow>();
        private readonly ICoreWebInventory _objInterfazCoreWebInventory = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebInventory>();
        private readonly CoreWebRenderInView _objInterfazCoreWebRenderInView = new();
        private readonly ICoreWebPermission _objInterfazCoreWebPermission = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebPermission>();
        private readonly IProviderModel _providerModel = VariablesGlobales.Instance.UnityContainer.Resolve<IProviderModel>();
        private readonly IProviderItemModel _providerItemModel = VariablesGlobales.Instance.UnityContainer.Resolve<IProviderItemModel>();
        private readonly IPriceModel _priceModel = VariablesGlobales.Instance.UnityContainer.Resolve<IPriceModel>();
        private readonly INaturalModel _naturalModel = VariablesGlobales.Instance.UnityContainer.Resolve<INaturalModel>();
        private readonly IListPriceModel _listPriceModel = VariablesGlobales.Instance.UnityContainer.Resolve<IListPriceModel>();
        private readonly ICompanyCurrencyModel _companyCurrencyModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyCurrencyModel>();
        private readonly IUserWarehouseModel _userWarehouseModel = VariablesGlobales.Instance.UnityContainer.Resolve<IUserWarehouseModel>();
        private readonly ITransactionMasterModel _transactionMasterModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterModel>();
        private readonly ITransactionModel _transactionModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionModel>();
        private readonly ITransactionMasterDetailModel _transactionMasterDetailModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterDetailModel>();
        private readonly ILegalModel _legalModel = VariablesGlobales.Instance.UnityContainer.Resolve<ILegalModel>();
        private readonly IItemModel _itemModel = VariablesGlobales.Instance.UnityContainer.Resolve<IItemModel>();
        private readonly IItemCategoryModel _itemCategoryModel = VariablesGlobales.Instance.UnityContainer.Resolve<IItemCategoryModel>();
        private readonly BindingList<FormInventoryInputTransactionMasterDetailDto> _bindingListTransactionMasterDetail;

        #endregion

        #region Init

        public FormInventoryInputEdit()
        {
            InitializeComponent();
        }

        public FormInventoryInputEdit(TypeOpenForm typeOpen, int companyId, int transactionMasterId, int transactionId)
        {
            InitializeComponent();
            TypeOpen = typeOpen;
            TransactionMasterId = transactionMasterId;
            TransactionId = transactionId;
            btnRegresar.Click += CommandRegresar;
            btnGuardar.Click += CommandSave;
            btnEliminar.Click += BtnEliminarOnClick;
            btnNuevo.Click += CommandNew;
            btnImprmir.Click += BtnImprmir_Click;
            _bindingListTransactionMasterDetail = new BindingList<FormInventoryInputTransactionMasterDetailDto>();
        }

        public void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
        }

        public void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
        }

        private void FormInventoryInputEdit_Load(object sender, EventArgs e)
        {
            _backgroundWorker = new BackgroundWorker();
            if (!progressPanel.Visible)
            {
                progressPanel.Width = Width;
                progressPanel.Height = Height;
                progressPanel.Visible = true;
            }

            _backgroundWorker.DoWork += (ob, ev) =>
            {
                if (TypeOpen == TypeOpenForm.Init && TransactionMasterId > 0 && TransactionId > 0)
                {
                    LoadEdit();
                }

                if (TypeOpen == TypeOpenForm.Init && TransactionMasterId == 0 && TransactionId == 0)
                {
                    LoadNew();
                }
            };
            _backgroundWorker.RunWorkerCompleted += (ob, ev) =>
            {
                if (ev.Error is not null)
                {
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", ev.Error.Message, this);
                }
                else if (ev.Cancelled)
                {
                    //se canceló por el usuario
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Warning, "Error", "Operación cancelada por el usuario", this);
                }
                else
                {
                    // Aquí puedes actualizar otros controles con los datos cargados
                    if (TypeOpen == TypeOpenForm.Init)
                    {
                        PreRender();
                    }

                    if (TypeOpen == TypeOpenForm.Init && TransactionMasterId > 0 && TransactionId > 0)
                    {
                        LoadRender(TypeRender.Edit);
                    }

                    if (TypeOpen == TypeOpenForm.Init && TransactionMasterId == 0 && TransactionId == 0)
                    {
                        LoadRender(TypeRender.New);
                    }

                    if (progressPanel.Visible)
                    {
                        progressPanel.Visible = false;
                    }
                }

                if (progressPanel.Visible)
                {
                    progressPanel.Visible = false;
                }
            };

            if (!progressPanel.Visible)
            {
                progressPanel.Size = Size;
                progressPanel.Visible = true;
            }

            if (!_backgroundWorker.IsBusy)
            {
                _backgroundWorker.RunWorkerAsync();
            }
        }

        #endregion

        #region Metodos

        public void ComandDelete()
        {
            var userNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];
            var notAccessControl = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_CONTROL"];
            var notAllEdit = VariablesGlobales.ConfigurationBuilder["NOT_ALL_EDIT"];
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

            var resultPermission = 0;
            if (appNeedAuthentication == "true")
            {
                var permited = _objInterfazCoreWebPermission.UrlPermited("app_cxc_customer", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(notAccessControl);
                }

                resultPermission = _objInterfazCoreWebPermission.UrlPermissionCmd("app_cxc_customer", "add", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    throw new Exception(notAllEdit);
                }
            }


            if (TransactionId == 0 && TransactionMasterId == 0)
            {
                throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_PARAMETER"]);
            }

            var objTM = _transactionMasterModel.GetRowByPk(user.CompanyID, TransactionId, TransactionMasterId);
            if (objTM == null)
            {
                throw new Exception($"No existe la transacción el id indicado {TransactionMasterId}");
            }

            var permissionMe = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_ME"]);
            if (resultPermission == permissionMe && objTM.CreatedBy != user.UserID)
            {
                throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_DELETE"]);
            }

            var commandEliminable = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_ELIMINABLE"]);
            if (!_objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_inputunpost", "statusID", objTM.StatusId!.Value, commandEliminable, user.CompanyID, user.BranchID, role.RoleID)!.Value)
            {
                throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_WORKFLOW_DELETE"]);
            }

            //Eliminar el Registro
            _transactionMasterModel.DeleteAppPosme(user.CompanyID, TransactionId, TransactionMasterId);
            _transactionMasterDetailModel.DeleteWhereTm(user.CompanyID, TransactionId, TransactionMasterId);
        }

        public async void ComandPrinter()
        {
            var userNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];
            var notAccessControl = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_CONTROL"];
            var notAllEdit = VariablesGlobales.ConfigurationBuilder["NOT_ALL_EDIT"];
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
                var permited = _objInterfazCoreWebPermission.UrlPermited("app_inventory_inputunpost", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(notAccessControl);
                }

                var resultPermission = _objInterfazCoreWebPermission.UrlPermissionCmd("app_inventory_inputunpost", "edit", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    throw new Exception(notAllEdit);
                }
            }

            if (TransactionId == 0 && TransactionMasterId == 0)
            {
                throw new Exception("No hay valores a imprimir");
            }

            ObjTm = _transactionMasterModel.GetRowByPk(user.CompanyID, TransactionId, TransactionMasterId);
            if (ObjTm is null)
            {
                throw new Exception($"No existe la transacción con el número {TransactionMasterId}");
            }

            var urlWeb = VariablesGlobales.ConfigurationBuilder["APP_URL_RESOURCE_CSS_JS"];
            objParameterUrlPrinter = _objInterfazCoreWebParameter.GetParameterValue("INVENTORY_URL_PRINTER_INPUTUNPOST", user.CompanyID);
            if (string.IsNullOrWhiteSpace(objParameterUrlPrinter))
            {
                throw new Exception("No hay parametro de url configurado");
            }

            //http://localhost/posmev4/app_inventory_inputunpost/viewRegisterFormato80mm/companyID/2/transactionID/21/transactionMasterID/1704
            var urlPdf = $"{urlWeb}/{objParameterUrlPrinter}/companyID/{user.CompanyID}/transactionID/{TransactionId}/transactionMasterID/{TransactionMasterId}";
            // Descargar el archivo PDF
            var urlLogin = $"{urlWeb}/core_acount/login"; // URL de autenticación
            var usuario = user.Nickname ?? "";
            var contraseña = user.Password ?? "";
            var rutaArchivoPdf = $"documento_generado_{TransactionMasterId}_{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}.pdf";

            using var httpClient = new HttpClient();
            // Credenciales en el cuerpo de la solicitud (si tu API las requiere así)
            var loginData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("txtNickname", usuario),
                new KeyValuePair<string, string>("txtPassword", contraseña)
            });

            // Enviar solicitud de autenticación
            var req = new HttpRequestMessage(HttpMethod.Post, urlLogin)
            {
                Content = loginData
            };
            var responseLogin = await httpClient.SendAsync(req);
            if (responseLogin.IsSuccessStatusCode)
            {
                // Configurar el cliente para descargar el PDF
                responseLogin.Headers.TryGetValues("Set-Cookie", out var cookies);
                if (cookies != null)
                {
                    httpClient.DefaultRequestHeaders.Add("Cookie", string.Join(";", cookies));
                }

                // Descargar el PDF
                var responsePdf = await httpClient.GetAsync(urlPdf);
                if (responsePdf.IsSuccessStatusCode)
                {
                    var pdfBytes = await responsePdf.Content.ReadAsByteArrayAsync();
                    await File.WriteAllBytesAsync(rutaArchivoPdf, pdfBytes);

                    Console.WriteLine("PDF descargado correctamente.");

                    // Abrir el PDF con el visor predeterminado
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = rutaArchivoPdf,
                        UseShellExecute = true
                    });
                }
                else
                {
                    throw new Exception("Error al descargar el PDF.");
                }
            }
            else
            {
                throw new Exception("Error de autenticación.");
            }
        }

        public void LoadEdit()
        {
            var userNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];
            var notAccessControl = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_CONTROL"];
            var notAllEdit = VariablesGlobales.ConfigurationBuilder["NOT_ALL_EDIT"];
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
                var permited = _objInterfazCoreWebPermission.UrlPermited("app_inventory_inputunpost", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(notAccessControl);
                }

                var resultPermission = _objInterfazCoreWebPermission.UrlPermissionCmd("app_inventory_inputunpost", "edit", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    throw new Exception(notAllEdit);
                }
            }

            if (TransactionId == 0 || TransactionMasterId == 0)
            {
                throw new Exception("No hay valores a editar");
            }

            //Obtener el componente de Item
            ObjComponentItem = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_item");
            if (ObjComponentItem is null)
            {
                throw new Exception("EL COMPONENTE 'tb_item' NO EXISTE...");
            }

            //Obtener el componente de Proveedor
            ObjComponentProvider = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_provider");
            if (ObjComponentProvider is null)
            {
                throw new Exception("EL COMPONENTE 'tb_provider' NO EXISTE...");
            }

            //Obtener el componente de Entrada sin postear
            ObjComponent = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_transaction_master_inputunpost");
            if (ObjComponent is null)
            {
                throw new Exception("EL COMPONENTE 'tb_transaction_master_inputunpost' NO EXISTE...");
            }

            //Obtener el componente de Orden de Compra
            ObjComponentOrdenCompra = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_transaction_master_purchaseorden");
            if (ObjComponentOrdenCompra is null)
            {
                throw new Exception("EL COMPONENTE 'tb_transaction_master_purchaseorden' NO EXISTE...");
            }

            ObjListTypePreice = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_price", "typePriceID", user.CompanyID);
            ObjListPrice = _listPriceModel.GetListPriceToApply(user.CompanyID);
            ObjlistWarehouse = _userWarehouseModel.GetRowByUserId(user.CompanyID, user.UserID);
            ObjTm = _transactionMasterModel.GetRowByPk(user.CompanyID, TransactionId, TransactionMasterId);
            if (ObjTm is null)
            {
                throw new Exception($"No existe la transaction con el id indicado {TransactionMasterId}");
            }

            ObjTmd = _transactionMasterDetailModel.GetRowByTransaction(user.CompanyID, TransactionId, TransactionMasterId);
            ObjListWorkflowStage = _objInterfazCoreWebWorkflow.GetWorkflowStageByStageInit("tb_transaction_master_inputunpost", "statusID", ObjTm.StatusId!.Value, user.CompanyID, user.BranchID, role.RoleID);
            ObjTmOrdenCompra = _transactionMasterModel.GetRowByTransactionMasterId(user.CompanyID, string.IsNullOrWhiteSpace(ObjTm.Reference4) ? 0 : Convert.ToInt32(ObjTm.Reference4));
            ObjProvider = _providerModel.GetRowByEntity(user.CompanyID, ObjTm.EntityId!.Value);
            ObjNaturalDefault = _naturalModel.GetRowByPk(user.CompanyID, user.BranchID, ObjTm.EntityId!.Value);
            ObjLegalDefault = _legalModel.GetRowByPk(user.CompanyID, user.BranchID, ObjTm.EntityId!.Value);
            ObjListCurrency = _companyCurrencyModel.GetByCompany(user.CompanyID);
            objParameterCoreViewCustomScrollInDetatailPurshase = _objInterfazCoreWebParameter.GetParameterValue("CORE_VIEW_CUSTOM_SCROLL_IN_DETATAIL_PURSHASE", user.CompanyID);
            objParameterInventoryUrlPrinterInputunpostOnlyQuantity = _objInterfazCoreWebParameter.GetParameterValue("INVENTORY_URL_PRINTER_INPUTUNPOST_ONLY_QUANTITY", user.CompanyID);
            objParameterInventoryUrlPrinterInputunpostShowOpciones = _objInterfazCoreWebParameter.GetParameterValue("INVENTORY_URL_PRINTER_INPUTUNPOST_SHOW_OPCIONES", user.CompanyID);
            objParameterUrlPrinter = _objInterfazCoreWebParameter.GetParameterValue("INVENTORY_URL_PRINTER_INPUTUNPOST", user.CompanyID);
            objParameterMasive = _objInterfazCoreWebParameter.GetParameterValue("ITEM_PRINTER_BARCODE_MASIVE", user.CompanyID);
        }

        public void LoadNew()
        {
            var userNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];
            var notAccessControl = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_CONTROL"];
            var notAllEdit = VariablesGlobales.ConfigurationBuilder["NOT_ALL_EDIT"];
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
                var permited = _objInterfazCoreWebPermission.UrlPermited("app_inventory_inputunpost", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(notAccessControl);
                }

                var resultPermission = _objInterfazCoreWebPermission.UrlPermissionCmd("app_inventory_inputunpost", "add", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    throw new Exception(notAllEdit);
                }
            }

            //Obtener el componente de Item
            ObjComponentItem = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_item");
            if (ObjComponentItem is null)
            {
                throw new Exception("EL COMPONENTE 'tb_item' NO EXISTE...");
            }

            //Obtener el componente de Proveedor
            ObjComponentProvider = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_provider");
            if (ObjComponentProvider is null)
            {
                throw new Exception("EL COMPONENTE 'tb_provider' NO EXISTE...");
            }

            //Obtener el componente de Entrada sin postear
            ObjComponent = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_transaction_master_inputunpost");
            if (ObjComponent is null)
            {
                throw new Exception("EL COMPONENTE 'tb_transaction_master_inputunpost' NO EXISTE...");
            }

            //Obtener el componente de Orden de Compra
            ObjComponentOrdenCompra = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_transaction_master_purchaseorden");
            if (ObjComponentOrdenCompra is null)
            {
                throw new Exception("EL COMPONENTE 'tb_transaction_master_purchaseorden' NO EXISTE...");
            }

            var objParameterWarehouseDefault = _objInterfazCoreWebParameter.GetParameter("INVENTORY_ITEM_WAREHOUSE_DEFAULT", user.CompanyID);
            if (objParameterWarehouseDefault is null)
            {
                throw new Exception("NO existe el parametro de bodega por default");
            }

            WarehouseDefault = objParameterWarehouseDefault.Value;

            var objParameterProviderDefault = _objInterfazCoreWebParameter.GetParameter("CXP_PROVIDER_DEFAULT", user.CompanyID);
            if (objParameterProviderDefault is null)
            {
                throw new Exception("NO existe el parametro de proveedor por default");
            }

            ObjlistWarehouse = _userWarehouseModel.GetRowByUserId(user.CompanyID, user.UserID);
            ObjListWorkflowStage = _objInterfazCoreWebWorkflow.GetWorkflowInitStage("tb_transaction_master_inputunpost", "statusID", user.CompanyID, user.BranchID, role.RoleID);
            ProviderDefault = _providerModel.GetRowByProviderNumber(user.CompanyID, objParameterProviderDefault.Value);
            ProviderNaturalDefault = _naturalModel.GetRowByPk(user.CompanyID, user.BranchID, ProviderDefault.EntityID);

            //Obtener el catalogo de tipos de precios
            ObjListTypePreice = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_price", "typePriceID", user.CompanyID);
            ObjListPrice = _listPriceModel.GetListPriceToApply(user.CompanyID);
            ObjListCurrency = _companyCurrencyModel.GetByCompany(user.CompanyID);
            objParameterCoreViewCustomScrollInDetatailPurshase = _objInterfazCoreWebParameter.GetParameterValue("CORE_VIEW_CUSTOM_SCROLL_IN_DETATAIL_PURSHASE", user.CompanyID);
        }

        public void SaveInsert()
        {
            var userNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];
            var notAccessControl = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_CONTROL"];
            var notAllEdit = VariablesGlobales.ConfigurationBuilder["NOT_ALL_EDIT"];
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
                var permited = _objInterfazCoreWebPermission.UrlPermited("app_inventory_inputunpost", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(notAccessControl);
                }

                var resultPermission = _objInterfazCoreWebPermission.UrlPermissionCmd("app_inventory_inputunpost", "add", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    throw new Exception(notAllEdit);
                }
            }

            ObjComponent = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_transaction_master_inputunpost");
            if (ObjComponent is null)
            {
                throw new Exception("EL COMPONENTE 'tb_transaction_master_inputunpost' NO EXISTE...");
            }

            ObjComponentItem = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_item");
            if (ObjComponentItem is null)
            {
                throw new Exception("EL COMPONENTE 'tb_item' NO EXISTE...");
            }

            _objInterfazCoreWebPermission.GetValueLicense(user.CompanyID, "app_inventory_inputunpost/index");
            //Obtener transaccion
            TransactionId = _objInterfazCoreWebTransaction.GetTransactionId(user.CompanyID, "tb_transaction_master_inputunpost", 0) ?? 0;
            var objT = _transactionModel.GetByCompanyAndTransaction(user.CompanyID, TransactionId);
            if (objT is null)
            {
                throw new Exception("No existe la Transacción para la compañia");
            }

            var selectedWarehouse = txtWarehouseID.SelectedItem as ComboBoxItem;
            var selectedItemStatus = txtStatusID.SelectedItem as ComboBoxItem;
            var selectedItemCurrency = txtCurrencyID.SelectedItem as ComboBoxItem;
            var currency1 = Convert.ToInt32(selectedItemCurrency!.Key);
            var currency2 = _objInterfazCoreWebCurrency.GetTarget(user.CompanyID, currency1);
            var objTm = new TbTransactionMaster
            {
                CompanyID = user.CompanyID,
                TransactionNumber = _objInterfazCoreWebCounter.GoNextNumber(user.CompanyID, user.BranchID, "tb_transaction_master_inputunpost", 0) ?? "",
                TransactionID = TransactionId,
                BranchID = user.BranchID,
                TransactionCausalID = _objInterfazCoreWebTransaction.GetDefaultCausalId(user.CompanyID, TransactionId),
                EntityID = _txtProviderId,
                TransactionOn = txtTransactionOn.DateTime,
                TransactionOn2 = DateTime.Now,
                StatusIDChangeOn = DateTime.Now,
                ComponentID = ObjComponent.ComponentID,
                Note = txtDescription.Text,
                Sign = (short?)(objT.SignInventory ?? 0),
                CurrencyID = currency1,
                CurrencyID2 = currency2,
                ExchangeRate = _objInterfazCoreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, decimal.One, currency2, currency1),
                Reference1 = txtReference1.Text,
                Reference2 = txtReference2.Text,
                Reference3 = txtReference3.Text,
                Reference4 = txtTransactionMasterIDOrdenCompra.HasValue ? $"{txtTransactionMasterIDOrdenCompra}" : "",
                DescriptionReference = "0",
                StatusID = Convert.ToInt32(selectedItemStatus!.Key),
                Amount = Convert.ToDecimal(txtTotal.EditValue),
                Tax1 = Convert.ToDecimal(txtIva.EditValue),
                Tax2 = decimal.Zero,
                Tax3 = decimal.Zero,
                Tax4 = decimal.Zero,
                Discount = Convert.ToDecimal(txtDiscount.EditValue),
                SubAmount = Convert.ToDecimal(txtSubTotal.EditValue),
                IsApplied = false,
                JournalEntryID = 0,
                ClassID = null,
                AreaID = null,
                PriorityID = null,
                SourceWarehouseID = null,
                TargetWarehouseID = Convert.ToInt32(selectedWarehouse!.Key),
                IsActive = true,
                IsTemplate = txtIsTemplate.Checked ? 1 : 0
            };

            _objInterfazCoreWebAuditoria.SetAuditCreated(objTm, user, "");

            TransactionMasterId = _transactionMasterModel.InsertAppPosme(objTm);
            var pathFileOfApp = VariablesGlobales.ConfigurationBuilder["PATH_FILE_OF_APP"] ?? "";
            var path = $"{pathFileOfApp}/company_{user.CompanyID}/component_{ObjComponent.ComponentID}/component_item_{TransactionMasterId}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Generar un archivo template de ejemplo
            var objParameterCharacterSplite = _objInterfazCoreWebParameter.GetParameter("CORE_CSV_SPLIT", user.CompanyID);
            var characterSplie = objParameterCharacterSplite.Value;
            var date = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
            var pathTemplate = $"{pathFileOfApp}/company_{user.CompanyID}/component_{ObjComponent.ComponentID}/component_item_{TransactionMasterId}";
            if (!Directory.Exists(pathTemplate))
            {
                Directory.CreateDirectory(pathTemplate);
            }

            pathTemplate = $"{pathTemplate}/ejemplo_{date}.csv";
            using (var fppathTemplate = new StreamWriter(pathTemplate, false, Encoding.UTF8))
            {
                var fieldTemplate = new List<string> { "Codigo", "Nombre", "Cantidad", "Costo", "Precio", "Lote", "Vencimiento" };
                fppathTemplate.WriteLine(string.Join(characterSplie, fieldTemplate));
            }

            //Recorrer la lista del detalle del documento
            if (_bindingListTransactionMasterDetail.Count > 0)
            {
                foreach (var detailDto in _bindingListTransactionMasterDetail)
                {
                    var objTmd = new TbTransactionMasterDetail
                    {
                        CompanyID = user.CompanyID,
                        TransactionID = TransactionId,
                        TransactionMasterID = TransactionMasterId,
                        ComponentID = ObjComponentItem.ComponentID,
                        ComponentItemID = detailDto.ItemId,
                        PromotionID = 0,
                        Amount = decimal.Zero,
                        Cost = decimal.Multiply(detailDto.Costo, detailDto.Quantity),
                        Quantity = detailDto.Quantity,
                        Discount = decimal.Zero,
                        UnitaryAmount = detailDto.Precio,
                        UnitaryCost = detailDto.Costo,
                        UnitaryPrice = detailDto.Precio,
                        Reference3 = $"{detailDto.Precio2}|{detailDto.Precio3}",
                        Reference4 = detailDto.Reference4.Trim().Replace(Environment.NewLine, ",").Replace(",,", ","),
                        CatalogStatusID = 0,
                        InventoryStatusID = 0,
                        IsActive = true,
                        QuantityStock = decimal.Zero,
                        QuantiryStockInTraffic = decimal.Zero,
                        QuantityStockUnaswared = decimal.Zero,
                        RemaingStock = decimal.Zero,
                        Lote = detailDto.Lote,
                        ExpirationDate = detailDto.Vencimiento,
                        InventoryWarehouseSourceID = objTm.SourceWarehouseID,
                        InventoryWarehouseTargetID = objTm.TargetWarehouseID,
                    };
                    _transactionMasterDetailModel.InsertAppPosme(objTmd);
                }
            }
            else
            {
                var transactionMasterIdThemplate = objTm.Reference4;
                if (!string.IsNullOrWhiteSpace(transactionMasterIdThemplate))
                {
                    var objTransactionMasterDetailTemplate = _transactionMasterDetailModel.GetRowByTransaction(user.CompanyID, TransactionId, Convert.ToInt32(transactionMasterIdThemplate));
                    if (objTransactionMasterDetailTemplate.Count > 0)
                    {
                        foreach (var detailDto in objTransactionMasterDetailTemplate)
                        {
                            var objTmd = new TbTransactionMasterDetail
                            {
                                CompanyID = objTm.CompanyID,
                                TransactionID = objTm.TransactionID,
                                TransactionMasterID = TransactionMasterId,
                                ComponentID = ObjComponentItem.ComponentID,
                                ComponentItemID = detailDto.ComponentItemId,
                                Quantity = detailDto.Quantity,
                                UnitaryCost = detailDto.UnitaryCost,
                                Cost = decimal.Multiply(detailDto.Quantity!.Value, detailDto.UnitaryCost!.Value),
                                UnitaryAmount = detailDto.UnitaryAmount,
                                Amount = decimal.Zero,
                                Discount = decimal.Zero,
                                UnitaryPrice = detailDto.UnitaryPrice,
                                PromotionID = 0,
                                Lote = detailDto.Lote,
                                ExpirationDate = detailDto.ExpirationDate,
                                Reference3 = detailDto.Reference3,
                                Reference4 = "",
                                CatalogStatusID = 0,
                                InventoryStatusID = 0,
                                IsActive = true,
                                QuantityStock = decimal.Zero,
                                QuantiryStockInTraffic = decimal.Zero,
                                QuantityStockUnaswared = decimal.Zero,
                                RemaingStock = decimal.Zero,
                                InventoryWarehouseSourceID = objTm.SourceWarehouseID,
                                InventoryWarehouseTargetID = objTm.TargetWarehouseID
                            };
                            _transactionMasterDetailModel.InsertAppPosme(objTmd);
                        }
                    }
                }
            }
        }

        public void SaveUpdate()
        {
            var userNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];
            var notAccessControl = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_CONTROL"];
            var notAllEdit = VariablesGlobales.ConfigurationBuilder["NOT_ALL_EDIT"];
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

            var resultPermission = 0;
            if (appNeedAuthentication == "true")
            {
                var permited = _objInterfazCoreWebPermission.UrlPermited("app_inventory_inputunpost", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(notAccessControl);
                }

                resultPermission = _objInterfazCoreWebPermission.UrlPermissionCmd("app_inventory_inputunpost", "edit", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    throw new Exception(notAllEdit);
                }
            }

            ObjComponent = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_transaction_master_inputunpost");
            if (ObjComponent is null)
            {
                throw new Exception("EL COMPONENTE 'tb_transaction_master_inputunpost' NO EXISTE...");
            }

            ObjComponentItem = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_item");
            if (ObjComponentItem is null)
            {
                throw new Exception("EL COMPONENTE 'tb_item' NO EXISTE...");
            }

            _objInterfazCoreWebPermission.GetValueLicense(user.CompanyID, "app_inventory_inputunpost/index");
            var objTm = _transactionMasterModel.GetRowByPk(user.CompanyID, TransactionId, TransactionMasterId);
            if (objTm is null)
            {
                throw new Exception($"No existe la transacción con ID: {TransactionId}-{TransactionMasterId}");
            }

            var oldStatusId = objTm.StatusId;
            var permissionMe = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_ME"]);
            if (resultPermission == permissionMe && ObjTm.CreatedBy != user.UserID)
            {
                throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_EDIT"]);
            }

            //Validar si el estado permite editar
            var commandEditableTotal = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_EDITABLE_TOTAL"]);
            if (!_objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_inputunpost", "statusID", oldStatusId!.Value, commandEditableTotal, user.CompanyID, user.BranchID, role.RoleID)!.Value)
            {
                throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_WORKFLOW_EDIT"]);
            }

            if (_objInterfazCoreWebAccounting.CycleIsCloseByDate(user.CompanyID, objTm.TransactionOn!.Value.Date))
            {
                throw new Exception("EL DOCUMENTO NO PUEDE ACTUALIZARCE, EL CICLO CONTABLE ESTA CERRADO");
            }

            //Obtener lista de precio
            var objParameterPriceDefault = _objInterfazCoreWebParameter.GetParameter("INVOICE_DEFAULT_PRICELIST", user.CompanyID);
            if (objParameterPriceDefault is null)
            {
                throw new Exception("No se ha ingresado el parametro de lista de precio por default");
            }

            var listPriceID = objParameterPriceDefault.Value;
            var selectedCurrency = txtCurrencyID.SelectedItem as ComboBoxItem;
            if (selectedCurrency is null)
            {
                throw new Exception("No se ha seleccionado el tipo de moneda");
            }

            var selectedStatus = txtStatusID.SelectedItem as ComboBoxItem;
            if (selectedStatus is null)
            {
                throw new Exception("No se ha sleccionado el Status de la transacción");
            }

            var selectedWarehouse = txtWarehouseID.SelectedItem as ComboBoxItem;
            if (selectedWarehouse is null)
            {
                throw new Exception("No se ha sleccionado una bodega");
            }

            var currencySourceId = Convert.ToInt32(selectedCurrency.Key);
            var targetCurrency2 = _objInterfazCoreWebCurrency.GetTarget(user.CompanyID, currencySourceId);
            var ratio = _objInterfazCoreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, decimal.One, targetCurrency2, currencySourceId);
            var objTmNew = new TbTransactionMaster
            {
                CompanyID = user.CompanyID,
                TransactionNumber = objTm.TransactionNumber ?? "",
                TransactionID = TransactionId,
                BranchID = user.BranchID,
                TransactionCausalID = objTm.TransactionCausalId,
                EntityID = _txtProviderId,
                TransactionOn = txtTransactionOn.DateTime,
                TransactionOn2 = objTm.TransactionOn2,
                StatusIDChangeOn = DateTime.Now,
                ComponentID = objTm.ComponentId,
                Note = txtDescription.Text,
                Sign = objTm.Sign,
                CurrencyID = currencySourceId,
                CurrencyID2 = targetCurrency2,
                ExchangeRate = ratio,
                Reference1 = txtReference1.Text,
                Reference2 = txtReference2.Text,
                Reference3 = txtReference3.Text,
                Reference4 = txtTransactionMasterIDOrdenCompra.HasValue ? $"{txtTransactionMasterIDOrdenCompra}" : "",
                StatusID = Convert.ToInt32(selectedStatus.Key),
                Amount = Convert.ToDecimal(txtTotal.EditValue),
                Tax1 = Convert.ToDecimal(txtIva.EditValue),
                Tax2 = decimal.Zero,
                Tax3 = decimal.Zero,
                Tax4 = decimal.Zero,
                Discount = Convert.ToDecimal(txtDiscount.EditValue),
                SubAmount = Convert.ToDecimal(txtSubTotal.EditValue),
                IsApplied = objTm.IsApplied,
                JournalEntryID = objTm.JournalEntryId,
                ClassID = objTm.ClassId,
                AreaID = objTm.AreaId,
                PriorityID = objTm.PriorityId,
                SourceWarehouseID = null,
                TargetWarehouseID = Convert.ToInt32(selectedWarehouse.Key),
                CreatedBy = objTm.CreatedBy,
                CreatedAt = objTm.CreatedAt,
                CreatedOn = objTm.CreatedOn,
                CreatedIn = objTm.CreatedIn,
                IsActive = objTm.IsActive,
                IsTemplate = txtIsTemplate.Checked ? 1 : 0
            };
            //$this->core_web_workflow->validateWorkflowStage("tb_transaction_master_inputunpost","statusID",$objTM->statusID,COMMAND_EDITABLE,$dataSession["user"]->companyID,$dataSession["user"]->branchID,$dataSession["role"]->roleID
            var commandEditable = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_EDITABLE"]);
            var validateWorkflowStage = _objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_inputunpost", "statusID", oldStatusId ?? 0, commandEditable, user.CompanyID, user.BranchID, role.RoleID);
            if (validateWorkflowStage.HasValue && validateWorkflowStage.Value)
            {
                var tbTransactionMaster = _transactionMasterModel.GetRowByPKK(TransactionMasterId);
                if (tbTransactionMaster is null)
                {
                    throw new Exception($"No existe la transaccicon con el ID: {TransactionMasterId}");
                }

                tbTransactionMaster.StatusID = Convert.ToInt32(selectedStatus.Key);
                _transactionMasterModel.UpdateAppPosme(user.CompanyID, TransactionId, TransactionMasterId, tbTransactionMaster);
            }
            else
            {
                _transactionMasterModel.UpdateAppPosme(user.CompanyID, TransactionId, TransactionMasterId, objTmNew);
            }

            var pathFileOfApp = VariablesGlobales.ConfigurationBuilder["PATH_FILE_OF_APP"] ?? "";
            if (!string.IsNullOrWhiteSpace(txtFileImport.Text))
            {
                _transactionMasterDetailModel.DeleteWhereTm(user.CompanyID, TransactionId, TransactionMasterId);

                var path = $"{pathFileOfApp}/company_{user.CompanyID}/component_{ObjComponent.ComponentID}/component_item_{TransactionMasterId}";
                if (!Directory.Exists(path))
                {
                    throw new Exception($"No existe el dictorio: {path}");
                }

                var file = $"{path}/{txtFileImport.Text}.csv";
                if (!File.Exists(file))
                {
                    throw new Exception($"No existe el archivo: {file}");
                }

                var extension = Path.GetExtension(file);
                if (!extension.Equals(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception("El archivo no es un tipo de archivo CSV.");
                }

                var table = FnParseCsvFile(file);
                if (table.Count > 0)
                {
                    var rowHeader = table[0];
                    // Verificar que las columnas requeridas existan en la primera fila
                    if (!rowHeader.ContainsKey("Codigo"))
                    {
                        throw new Exception("Columna 'Codigo' no existe en el archivo .csv");
                    }

                    if (!rowHeader.ContainsKey("Nombre"))
                    {
                        throw new Exception("Columna 'Nombre' no existe en el archivo .csv");
                    }

                    if (!rowHeader.ContainsKey("Cantidad"))
                    {
                        throw new Exception("Columna 'Cantidad' no existe en el archivo .csv");
                    }

                    if (!rowHeader.ContainsKey("Costo"))
                    {
                        throw new Exception("Columna 'Costo' no existe en el archivo .csv");
                    }

                    if (!rowHeader.ContainsKey("Precio"))
                    {
                        throw new Exception("Columna 'Precio' no existe en el archivo .csv");
                    }

                    if (!rowHeader.ContainsKey("Lote"))
                    {
                        throw new Exception("Columna 'Lote' no existe en el archivo .csv");
                    }

                    if (!rowHeader.ContainsKey("Vencimiento"))
                    {
                        throw new Exception("Columna 'Vencimiento' no existe en el archivo .csv");
                    }

                    // Recorrer la lista de filas para manejar los datos
                    foreach (var row in table)
                    {
                        var codigo = row["Codigo"];
                        var nombre = row["Nombre"];
                        var cantidad = int.Parse(row["Cantidad"]);
                        var costo = decimal.Parse(row["Costo"]);
                        var precio = decimal.Parse(row["Precio"]);
                        var lote = row["Lote"];
                        DateTime dateVencimiento;
                        DateTime? vencimiento;
                        if (DateTime.TryParse(row["Vencimiento"], out dateVencimiento))
                        {
                            vencimiento = dateVencimiento;
                        }
                        else
                        {
                            vencimiento = null;
                        }

                        var objItem = _itemModel.GetRowByCode(user.CompanyID, codigo) ?? _itemModel.GetRowByCodeBarra(user.CompanyID, codigo);
                        var itemId = 0;
                        //Agregar productos nuevos
                        if (objItem is null)
                        {
                            itemId = FnCreateNewItemCsv(user.CompanyID, user.BranchID, role.RoleID, codigo, nombre, objTmNew);
                        }
                        else
                        {
                            itemId = objItem.ItemID;
                        }

                        objItem = _itemModel.GetRowByPk(user.CompanyID, itemId);
                        if (objItem is null)
                        {
                            continue;
                        }

                        var transactionMasterDetailID = 0;
                        itemId = objItem.ItemID;
                        var quantity = cantidad;
                        var cost = costo;

                        //Ingrear al provedor si no existe. 
                        var objProviderItemModel = _providerItemModel.GetByPk(user.CompanyID, itemId, objTmNew.EntityID!.Value);
                        if (objProviderItemModel is null)
                        {
                            var objPimNew = new TbProviderItem
                            {
                                CompanyID = user.CompanyID,
                                BranchID = user.BranchID,
                                EntityID = objTmNew.EntityID!.Value,
                                ItemID = itemId
                            };
                            _providerItemModel.InsertAppPosme(objPimNew);
                        }

                        //Nuevo Detalle
                        {
                            var objTmd = new TbTransactionMasterDetail
                            {
                                CompanyID = user.CompanyID,
                                TransactionID = TransactionId,
                                TransactionMasterID = TransactionMasterId,
                                ComponentID = ObjComponentItem.ComponentID,
                                ComponentItemID = itemId,
                                Quantity = quantity,
                                UnitaryCost = cost,
                                Cost = cost * quantity,
                                UnitaryAmount = precio,
                                Amount = decimal.Zero,
                                Discount = decimal.Zero,
                                UnitaryPrice = precio,
                                PromotionID = 0,
                                Lote = lote,
                                ExpirationDate = vencimiento,
                                Reference3 = "0|0",
                                Reference4 = "", //si se carga mediatne un excel no exsite el reference4, por que los valores de exencion de codigo, se deben de modificar, en la pantalla propiamente
                                CatalogStatusID = 0,
                                InventoryStatusID = 0,
                                IsActive = true,
                                QuantityStock = decimal.Zero,
                                QuantiryStockInTraffic = decimal.Zero,
                                QuantityStockUnaswared = decimal.Zero,
                                RemaingStock = decimal.Zero,
                                InventoryWarehouseSourceID = objTmNew.SourceWarehouseID,
                                InventoryWarehouseTargetID = objTmNew.TargetWarehouseID
                            };
                            _transactionMasterDetailModel.InsertAppPosme(objTmd);
                        }
                    }
                }
            }
            else
            {
                var listTmdId = _bindingListTransactionMasterDetail.Select(dto => dto.TransactionMasterDetailId).ToList();
                _transactionMasterDetailModel.DeleteWhereIdNotIn(user.CompanyID, TransactionId, TransactionMasterId, listTmdId);
                if (_bindingListTransactionMasterDetail.Count > 0)
                {
                    foreach (var dto in _bindingListTransactionMasterDetail)
                    {
                        var transactionMasterDetailId = dto.TransactionMasterDetailId;
                        var objItem = _itemModel.GetRowByPk(objTm.CompanyId, dto.ItemId);
                        var objItemInactive = _itemModel.GetRwByPkAndInactive(objTm.CompanyId, dto.ItemId);

                        if (objItem is null && objItemInactive is not null)
                        {
                            throw new Exception($"Revisar el producto: {objItemInactive.ItemNumber}, Códio de barra {objItemInactive.BarCode}, revisar configuracion no se encuentra en sistema");
                        }

                        //Actualizar Codigo de barra
                        if (!string.IsNullOrEmpty(dto.BarCodeExtende))
                        {
                            if (!objItem.BarCode!.Contains(dto.BarCodeExtende))
                            {
                                var cleanedBarCodeExtende = dto.BarCodeExtende.Trim();
                                cleanedBarCodeExtende = cleanedBarCodeExtende.Replace("\n", ",");
                                cleanedBarCodeExtende = cleanedBarCodeExtende.Replace(",,", ",");
                                objItem.BarCode = objItem.BarCode + "," + cleanedBarCodeExtende;
                                // Actualización del ítem en la base de datos
                                _itemModel.UpdateAppPosme(objItem.CompanyID, objItem.ItemID, objItem);
                            }
                        }

                        //Actualizar tipo de precio 1 ---> 154 ---->PUBLICO
                        if (dto.Precio > 0)
                        {
                            int typePriceID = 154;

                            // Cálculo del porcentaje
                            var percentage = (objItem.Cost == 0) ? (dto.Precio / 100) : (((100 * dto.Precio) / objItem.Cost) - 100);

                            // Llamada al método para actualizar el precio
                            var findPrice = _priceModel.GetRowByPk(user.CompanyID, Convert.ToInt32(listPriceID), objItem.ItemID, typePriceID);
                            findPrice.Percentage = percentage;
                            _priceModel.UpdateAppPosme(user.CompanyID, Convert.ToInt32(listPriceID), objItem.ItemID, typePriceID, findPrice);
                        }

                        //Ingrear al provedor si no existe. 
                        var objProviderItemModel = _providerItemModel.GetByPk(user.CompanyID, objItem.ItemID, objTmNew.EntityID!.Value);
                        if (objProviderItemModel is null)
                        {
                            var objPimNew = new TbProviderItem
                            {
                                CompanyID = user.CompanyID,
                                BranchID = user.BranchID,
                                EntityID = objTmNew.EntityID!.Value,
                                ItemID = objItem.ItemID
                            };
                            _providerItemModel.InsertAppPosme(objPimNew);
                        }

                        //Nuevo Detalle
                        if (transactionMasterDetailId == 0)
                        {
                            var objTmd = new TbTransactionMasterDetail
                            {
                                CompanyID = user.CompanyID,
                                TransactionID = TransactionId,
                                TransactionMasterID = TransactionMasterId,
                                ComponentID = ObjComponentItem.ComponentID,
                                ComponentItemID = objItem.ItemID,
                                Quantity = dto.Quantity,
                                UnitaryCost = dto.Costo,
                                Cost = dto.Costo * dto.Quantity,
                                UnitaryAmount = dto.Precio,
                                Amount = dto.Costo * dto.Quantity,
                                Discount = decimal.Zero,
                                UnitaryPrice = dto.Precio,
                                PromotionID = 0,
                                Lote = dto.Lote,
                                ExpirationDate = dto.Vencimiento,
                                Reference3 = $"{dto.Precio2}|{dto.Precio3}",
                                Reference4 = dto.Reference4!.Trim().Replace("\n", ",").Replace(",,", ","),
                                CatalogStatusID = 0,
                                InventoryStatusID = 0,
                                IsActive = true,
                                QuantityStock = decimal.Zero,
                                QuantiryStockInTraffic = decimal.Zero,
                                QuantityStockUnaswared = decimal.Zero,
                                RemaingStock = decimal.Zero,
                                InventoryWarehouseSourceID = objTmNew.SourceWarehouseID,
                                InventoryWarehouseTargetID = objTmNew.TargetWarehouseID
                            };
                            _transactionMasterDetailModel.InsertAppPosme(objTmd);
                        }
                        else
                        {
                            //Editar Detalle
                            var objTmd = _transactionMasterDetailModel.GetRowByPKK(transactionMasterDetailId);
                            if (objTmd is null)
                            {
                                throw new Exception($"No fue posible recuperar el transaction master detail con ID: {transactionMasterDetailId}");
                            }

                            objTmd.UnitaryPrice = dto.Precio;
                            objTmd.Quantity = dto.Quantity;
                            objTmd.UnitaryCost = dto.Costo;
                            objTmd.Amount = dto.Costo * dto.Quantity;
                            objTmd.Reference3 = $"{dto.Precio2}|{dto.Precio3}";
                            objTmd.Reference4 = dto.Reference4!.Trim().Replace("\n", ",").Replace(",,", ",");
                            objTmd.UnitaryAmount = dto.Precio;
                            objTmd.Cost = dto.Quantity * dto.Costo;
                            objTmd.Lote = dto.Lote;
                            objTmd.ExpirationDate = dto.Vencimiento;
                            objTmd.InventoryWarehouseSourceID = objTmNew.SourceWarehouseID;
                            objTmd.InventoryWarehouseTargetID = objTmNew.TargetWarehouseID;
                            _transactionMasterDetailModel.UpdateAppPosme(user.CompanyID, TransactionId, TransactionMasterId, dto.TransactionMasterDetailId, objTmd);
                        }
                    }
                }
            }

            //Aplicar el Documento?
            var commandAplicable = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_APLICABLE"]);
            if (_objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_inputunpost", "statusID", objTmNew.StatusID!.Value, commandAplicable, user.CompanyID, user.BranchID, role.RoleID)!.Value)
            {
                //Ingresar en Kardex.
                _objInterfazCoreWebInventory.CalculateKardexNewInput(user.CompanyID, TransactionId, TransactionMasterId);

                //Crear Conceptos.
                _objInterfazCoreWebConcept.InputUnPost(user.CompanyID, TransactionId, TransactionMasterId);
            }
        }

        public void CommandNew(object? sender, EventArgs e)
        {
            Close();
            var frmInventoryInputEdit = new FormInventoryInputEdit(TypeOpenForm.Init, 0, 0, 0)
            {
                MdiParent = CoreFormList.Principal()
            };
            frmInventoryInputEdit.Show();
        }

        public void CommandSave(object? sender, EventArgs e)
        {
            if (FnValidateForm())
            {
                _backgroundWorker = new BackgroundWorker();
                if (!progressPanel.Visible)
                {
                    progressPanel.Width = Width;
                    progressPanel.Height = Height;
                    progressPanel.Visible = true;
                }

                _backgroundWorker.DoWork += (ob, ev) =>
                {
                    switch (_typeRender)
                    {
                        case TypeRender.New:
                            SaveInsert();
                            break;
                        default:
                            SaveUpdate();
                            break;
                    }
                };
                _backgroundWorker.RunWorkerCompleted += (ob, ev) =>
                {
                    if (ev.Error is not null)
                    {
                        _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Registrar", $"No se registraron los valores. {ev.Error.Message}", this);
                    }
                    else if (ev.Cancelled)
                    {
                        //cancelado por el usuario   
                        _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Warning, "Registrar", "Se ha cancelado la operación actual. Linea 3424", this);
                    }
                    else
                    {
                        _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Informacion, "Registrar", "Se han registrdo los datos de forma correcta", this);

                        LoadEdit();
                        LoadRender(TypeRender.Edit);
                    }

                    if (progressPanel.Visible)
                    {
                        progressPanel.Visible = false;
                    }
                };

                if (!_backgroundWorker.IsBusy)
                {
                    _backgroundWorker.RunWorkerAsync();
                }
            }
        }

        public void CommandRegresar(object? sender, EventArgs e)
        {
            Close();
        }

        public void PreRender()
        {
            HelperMethods.OnlyNumberDecimals(txtSubTotal);
            HelperMethods.OnlyNumberDecimals(txtDiscount);
            HelperMethods.OnlyNumberDecimals(txtIva);
            HelperMethods.OnlyNumberDecimals(txtTotal);
        }

        public void LoadRender(TypeRender typeRedner)
        {
            _typeRender = typeRedner;
            var user = VariablesGlobales.Instance.User;
            txtFileImport.Clear();
            switch (typeRedner)
            {
                case TypeRender.New:
                    Text = @"AGREGAR COMPRA";
                    lblTitulo.Text = @"NUMERO:#00000000";
                    _txtProviderId = ProviderDefault.EntityID;
                    txtProviderDescription.Text = $@"{ProviderDefault!.ProviderNumber}/{ProviderNaturalDefault!.FirstName!}";
                    txtTransactionOn.DateTime = DateTime.Today;
                    tabPageArchivos.PageVisible = false;
                    btnImprmir.Visible = false;
                    btnNuevo.Visible = false;
                    btnEliminar.Visible = false;
                    stackPanelArchivo.Visible = false;
                    var tbUserWarehouseDto = ObjlistWarehouse!.Single(dto => dto.Number!.Contains(WarehouseDefault));
                    CoreWebRenderInView.LlenarComboBox(ObjListWorkflowStage, txtStatusID, "WorkflowStageID", "Name", ObjListWorkflowStage!.First().WorkflowStageID);
                    CoreWebRenderInView.LlenarComboBox(ObjlistWarehouse, txtWarehouseID, "WarehouseId", "Name", tbUserWarehouseDto.WarehouseId);
                    CoreWebRenderInView.LlenarComboBox(ObjListCurrency, txtCurrencyID, "CurrencyId", "Name", ObjListCurrency!.First().CurrencyId);
                    btnPrinterDetailTransaction.Visible = false;
                    btnAgregarArchivo.Visible = false;
                    txtIva.EditValue = decimal.Zero;
                    txtSubTotal.EditValue = decimal.Zero;
                    txtDiscount.EditValue = decimal.Zero;
                    txtTotal.EditValue = decimal.Zero;
                    gridControlArchivos.DataSource = null;
                    _bindingListTransactionMasterDetail.Clear();
                    gridControlTransactionMasterDetail.DataSource = _bindingListTransactionMasterDetail;
                    break;
                case TypeRender.Edit:
                    Text = @"EDITAR COMPRA";
                    lblTitulo.Text = @$"NUMERO:#{ObjTm.TransactionNumber}";
                    _txtProviderId = ObjProvider.EntityID;
                    txtTransactionMasterIDOrdenCompra = string.IsNullOrWhiteSpace(ObjTm.Reference4) ? 0 : Convert.ToInt32(ObjTm.Reference4);
                    stackPanelArchivo.Visible = true;
                    gridControlArchivos.DataSource = null;
                    gridControlArchivos.MainView = null;
                    _renderGridFiles = new RenderFileGridControl(user.CompanyID, ObjComponent!.ComponentID, TransactionMasterId);
                    _renderGridFiles.RenderGridControl(gridControlArchivos);
                    _renderGridFiles.LoadFiles();
                    CoreWebRenderInView.LlenarComboBox(ObjListWorkflowStage, txtStatusID, "WorkflowStageID", "Name", ObjTm.StatusId);
                    CoreWebRenderInView.LlenarComboBox(ObjlistWarehouse, txtWarehouseID, "WarehouseId", "Name", ObjTm.TargetWarehouseId);
                    CoreWebRenderInView.LlenarComboBox(ObjListCurrency, txtCurrencyID, "CurrencyId", "Name", ObjTm.CurrencyId);
                    txtTransactionNumberOrdenCompra.Text = ObjTmOrdenCompra != null ? ObjTmOrdenCompra.TransactionNumber : "";
                    txtIsTemplate.Checked = ObjTm.IsTemplate!.Value == 1;
                    txtReference1.Text = ObjTm.Reference1;
                    txtReference2.Text = ObjTm.Reference2;
                    txtReference3.Text = ObjTm.Reference3;
                    txtDescription.Text = ObjTm.Note;
                    txtProviderDescription.Text = $@"{ObjProvider!.ProviderNumber}/{ObjLegalDefault!.ComercialName!}";
                    txtTransactionOn.DateTime = ObjTm.TransactionOn!.Value;
                    txtIva.Text = (Math.Floor(ObjTm.Tax1!.Value * 100) / 100).ToString("N2");
                    txtSubTotal.Text = (Math.Floor(ObjTm.SubAmount!.Value * 100) / 100).ToString("N2");
                    txtDiscount.Text = (Math.Floor(ObjTm.Discount!.Value * 100) / 100).ToString("N2");
                    txtTotal.Text = (Math.Floor(ObjTm.Amount!.Value * 100) / 100).ToString("N2");
                    tabPageArchivos.PageVisible = true;
                    btnImprmir.Visible = true;
                    btnNuevo.Visible = true;
                    btnEliminar.Visible = true;
                    btnPrinterDetailTransaction.Visible = true;
                    btnAgregarArchivo.Visible = true;
                    _bindingListTransactionMasterDetail.Clear();
                    if (ObjTmd.Count > 0)
                    {
                        foreach (var detailDto in ObjTmd)
                        {
                            var splitPrecio = detailDto.Reference3.Split("|");
                            var precio2 = decimal.Parse(splitPrecio[0]);
                            var precio3 = decimal.Parse(splitPrecio[1]);
                            _bindingListTransactionMasterDetail.Add(new FormInventoryInputTransactionMasterDetailDto
                            {
                                ItemId = detailDto.ItemId,
                                TransactionMasterDetailId = detailDto.TransactionMasterDetailId,
                                Codigo = detailDto.ItemNumber,
                                Nombre = detailDto.ItemName,
                                UnidadMedida = detailDto.UnitMeasureName,
                                Quantity = detailDto.Quantity!.Value,
                                Costo = detailDto.UnitaryCost!.Value,
                                Precio = detailDto.UnitaryAmount!.Value,
                                Precio2 = precio2,
                                Precio3 = precio3,
                                Lote = detailDto.Lote,
                                Reference4 = detailDto.Reference4,
                                Vencimiento = detailDto.ExpirationDate,
                                BarCodeExtende = detailDto.Reference4
                            });
                        }
                    }

                    gridControlTransactionMasterDetail.DataSource = _bindingListTransactionMasterDetail;
                    break;
            }
        }

        public void InitializeControl()
        {
        }

        #endregion

        #region Funciones

        private bool FnValidateForm()
        {
            if (_txtProviderId <= 0)
            {
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", "Seleccione un proveedor", this);
                return false;
            }

            var selectedStatusId = txtStatusID.SelectedItem as ComboBoxItem;
            if (selectedStatusId is null)
            {
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", "Establecer Estado", this);
                return false;
            }

            if (txtTransactionOn.EditValue is null)
            {
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", "Escriba la Fecha del Documento", this);
                return false;
            }

            if (gridViewTransactionMasterDetail.RowCount <= 0)
            {
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", "Agregar el Detalle del Documento", this);
                return false;
            }

            for (var i = 0; i < gridViewTransactionMasterDetail.RowCount; i++)
            {
                var rowCellValueCantidad = gridViewTransactionMasterDetail.GetRowCellValue(i, colCantidad);
                var rowCellValuePrecio = gridViewTransactionMasterDetail.GetRowCellValue(i, colPrecio);
                if (rowCellValueCantidad is null || string.IsNullOrWhiteSpace(rowCellValueCantidad.ToString())
                                                 || rowCellValuePrecio is null || string.IsNullOrWhiteSpace(rowCellValuePrecio.ToString()))
                {
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", "No puede haber campos vacios", this);
                    return false;
                }

                var cantidad = Convert.ToDecimal(rowCellValueCantidad);
                var precio = Convert.ToDecimal(rowCellValuePrecio);

                if (cantidad.CompareTo(decimal.Zero) <= 0)
                {
                    // Si encuentras un valor en 0, devuelve falso.
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", "No puede haber cantidades en negativos o ceros", this);
                    return false;
                }

                if (precio.CompareTo(decimal.Zero) <= 0)
                {
                    // Si encuentras un valor en 0, devuelve falso.
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", "No puede haber precios en negativos o ceros", this);
                    return false;
                }
            }

            return true;
        }

        private void FnOnCompleteNewItemPopPub(dynamic mensaje)
        {
            Dictionary<string, string> diccionario = new Dictionary<string, string>();
            WebToolsHelper objWebToolsHelper = new WebToolsHelper();
            diccionario.Add("itemID", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "itemID", "0"));
            diccionario.Add("Codigo", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Codigo", "0"));
            diccionario.Add("Nombre", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Nombre", "0"));
            diccionario.Add("Medida", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Medida", "0"));

            diccionario.Add("Cantidad", "1");
            diccionario.Add("Precio", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Precio", "0"));
            diccionario.Add("Total", Convert.ToString(1 * Convert.ToDecimal(objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Precio", "0"))));

            diccionario.Add("Iva", "0");
            diccionario.Add("Lote", "");
            diccionario.Add("Vencimiento", "");
            diccionario.Add("Costo", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Costo", "0"));
            diccionario.Add("Precio2", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "pricePorMayor", "0"));
            diccionario.Add("Precio3", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "priceCredito", "0"));
            FnOnCompleteNewItem(diccionario, true);
        }

        private void FnOnCompleteNewItem(Dictionary<string, string> diccionario, bool sumar)
        {
            Invoke(() =>
            {
                var index = 0;
                var indexEncontrado = 0;
                var encontrado = false;
                var itemID = Convert.ToInt32(diccionario["itemID"]);
                if (itemID <= 0)
                {
                    return;
                }

                //Buscar Item
                if (_bindingListTransactionMasterDetail.Count > 0)
                {
                    foreach (FormInventoryInputTransactionMasterDetailDto detailDto in _bindingListTransactionMasterDetail)
                    {
                        if (detailDto.ItemId == itemID)
                        {
                            detailDto.Quantity = decimal.Add(detailDto.Quantity, decimal.One);
                            encontrado = true;
                            break;
                        }
                    }
                }

                if (encontrado)
                {
                    FnUpdateDetail();
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Informacion, "Agregar", "Ya existe el articulo", this);
                    return;
                }

                var billingEdit = new FormInventoryInputTransactionMasterDetailDto
                {
                    ItemId = Convert.ToInt32(diccionario["itemID"]),
                    TransactionMasterDetailId = 0,
                    Codigo = diccionario["Codigo"],
                    Nombre = diccionario["Nombre"],
                    Quantity = decimal.One,
                    Costo = WebToolsHelper.ConvertToNumber<decimal>(diccionario["Costo"]),
                    Precio = WebToolsHelper.ConvertToNumber<decimal>(diccionario["Precio"]),
                    UnidadMedida = diccionario["Medida"],
                    Precio2 = WebToolsHelper.ConvertToNumber<decimal>(diccionario["Precio2"]),
                    Precio3 = WebToolsHelper.ConvertToNumber<decimal>(diccionario["Precio3"]),
                    Lote = "",
                    Reference4 = "",
                    Vencimiento = null,
                    BarCodeExtende = ""
                };
                _bindingListTransactionMasterDetail.Add(billingEdit);
                FnUpdateDetail();
            });
        }

        private void FnOnCompleteNewProviderPopPub(dynamic mensaje)
        {
            var diccionario = new Dictionary<string, string>();
            var objWebToolsHelper = new WebToolsHelper();
            diccionario.Add("entityID", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "entityID", "0"));
            diccionario.Add("Codigo", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Codigo", "0"));
            diccionario.Add("Nombre", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Nombre", "0"));
            FnOnCompleteNewProvider(diccionario, true);
        }

        private void FnOnCompleteNewProvider(Dictionary<string, string> diccionario, bool b)
        {
            Invoke(() =>
            {
                _txtProviderId = WebToolsHelper.ConvertToNumber<int>(diccionario["entityID"]);
                var codigo = diccionario["Codigo"];
                var nombre = diccionario["Nombre"];
                txtProviderDescription.Text = @$"{codigo}/{nombre}";
            });
        }

        private void FnOnCompleteNewOrdenCompraPopPub(dynamic mensaje)
        {
            var diccionario = new Dictionary<string, string>();
            var objWebToolsHelper = new WebToolsHelper();
            diccionario.Add("transactionMasterID", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "transactionMasterID", "0"));
            diccionario.Add("Transaccion", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Transaccion", "0"));
            FnOnCompleteNewOrdenCompra(diccionario, true);
        }

        private void FnOnCompleteNewOrdenCompra(Dictionary<string, string> diccionario, bool b)
        {
            Invoke(() =>
            {
                txtTransactionMasterIDOrdenCompra = WebToolsHelper.ConvertToNumber<int>(diccionario["transactionMasterID"]);
                var codigo = diccionario["Transaccion"];
                txtTransactionNumberOrdenCompra.Text = @$"{codigo}";
            });
        }

        private void FnUpdateDetail()
        {
            var subtotal = decimal.Zero;
            var iva = decimal.Parse(txtIva.Text);
            if (string.IsNullOrWhiteSpace(txtDiscount.Text))
            {
                txtDiscount.EditValue = decimal.Zero;
            }

            var discount = decimal.Parse(txtDiscount.Text);
            var total = decimal.Zero;

            txtIva.Text = iva.ToString();
            txtDiscount.Text = discount.ToString();

            // Recorrer el detalle de la transacción (GridControl y GridView)
            for (int i = 0; i < gridViewTransactionMasterDetail.RowCount; i++)
            {
                var canitdad = Convert.ToDecimal(gridViewTransactionMasterDetail.GetRowCellValue(i, colCantidad));
                var costo = Convert.ToDecimal(gridViewTransactionMasterDetail.GetRowCellValue(i, colCosto));
                subtotal += decimal.Multiply(canitdad, costo);
            }

            subtotal = Math.Round(subtotal, 2);

            total = Math.Round((subtotal + iva - discount), 2);

            txtSubTotal.Text = subtotal.ToString("N2");
            txtTotal.Text = total.ToString("N2");
        }

        // Método para parsear el archivo CSV
        private List<Dictionary<string, string>> FnParseCsvFile(string path)
        {
            var user = VariablesGlobales.Instance.User;
            var rows = new List<Dictionary<string, string>>();
            var coreCsvSplit = _objInterfazCoreWebParameter.GetParameterValue("CORE_CSV_SPLIT", user.CompanyID);
            using var reader = new StreamReader(path);
            // Leer la primera línea para obtener los encabezados
            var headers = reader.ReadLine()!.Split(coreCsvSplit);

            // Leer las siguientes líneas y crear diccionarios con los datos
            while (!reader.EndOfStream)
            {
                var values = reader.ReadLine()!.Split(coreCsvSplit);
                var row = new Dictionary<string, string>();

                for (var i = 0; i < headers.Length; i++)
                {
                    row[headers[i]] = values[i];
                }

                rows.Add(row);
            }

            return rows;
        }

        private int FnCreateNewItemCsv(int companyId, int branchId, int roleId, string codigo, string description, TbTransactionMaster objTmNew)
        {
            var frmInventoryItem = new FormInventoryItemEdit(TypeOpenForm.NotInit, 0);
            frmInventoryItem.InitializeControl();
            var txtDetailListPriceID = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_DEFAULT_PRICELIST", companyId);
            var txtDetailTypePriceID0 = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_price", "typePriceID", companyId)!.ElementAt(0);
            var txtDetailTypePriceID1 = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_price", "typePriceID", companyId)!.ElementAt(1);
            var txtDetailTypePriceID2 = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_price", "typePriceID", companyId)!.ElementAt(2);
            var txtDetailTypePriceID3 = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_price", "typePriceID", companyId)!.ElementAt(3);
            var txtDetailTypePriceID4 = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_price", "typePriceID", companyId)!.ElementAt(4);
            var txtDetailSkuCatalogItemID = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_item", "unitMeasureID", companyId)!.First();
            frmInventoryItem.txtName.Text = description;
            frmInventoryItem.txtBarCode.Text = codigo;
            frmInventoryItem.txtDescription.Text = description;
            frmInventoryItem.txtCapacity.EditValue = decimal.One;
            frmInventoryItem.txtQuantityMax.EditValue = new decimal(1000);
            frmInventoryItem.txtQuantityMin.EditValue = decimal.Zero;
            frmInventoryItem.txtReference1.EditValue = "-";
            frmInventoryItem.txtReference2.EditValue = "-";
            frmInventoryItem.txtReference3.EditValue = "-";
            frmInventoryItem.txtIsPerishable.Checked = false;
            frmInventoryItem.txtIsServices.Checked = false;
            frmInventoryItem.txtIsInvoiceQuantityZero.Checked = true;
            frmInventoryItem.txtIsInvoice.Checked = true;
            frmInventoryItem.txtFactorBox.EditValue = decimal.One;
            frmInventoryItem.txtFactorProgram.EditValue = decimal.One;
            frmInventoryItem.txtCurrencyID.Properties.Items.Add(new ComboBoxItem(objTmNew.CurrencyID!.Value.ToString("N0"), "Moneda"));
            frmInventoryItem.txtCurrencyID.SelectedIndex = 0;
            frmInventoryItem.txtQuantity.EditValue = decimal.Zero;
            frmInventoryItem.txtCost.EditValue = decimal.Zero;
            frmInventoryItem.ObjListUnitMeasure = new List<TbCatalogItem>();
            frmInventoryItem.WarehouseDtoBindingList = new BindingList<FormInventoryItemEditWarehouseDTO>();
            var formInventoryItemEditWarehouseDto = new FormInventoryItemEditWarehouseDTO
            {
                WarehouseId = objTmNew.TargetWarehouseID!.Value,
                WarehouseName = "",
                Quantity = 0,
                QuantityMin = 0,
                QuantityMax = new decimal(1000)
            };
            frmInventoryItem.WarehouseDtoBindingList.Add(formInventoryItemEditWarehouseDto);
            var sku = new List<TbItemSkuDto>
            {
                new()
                {
                    CatalogItemId = txtDetailSkuCatalogItemID.CatalogItemID,
                    Value = decimal.One,
                }
            };
            frmInventoryItem.ObjItemSku = new BindingList<TbItemSkuDto>(sku);
            var tbPriceDto0 = new TbPriceDto
            {
                CompanyId = companyId,
                ListPriceId = Convert.ToInt32(txtDetailListPriceID ?? "0"),
                TypePriceId = txtDetailTypePriceID0.CatalogItemID,
                Cost = 0,
                PercentageCommision = 0,
            };
            var tbPriceDto1 = new TbPriceDto
            {
                CompanyId = companyId,
                ListPriceId = Convert.ToInt32(txtDetailListPriceID ?? "0"),
                TypePriceId = txtDetailTypePriceID1.CatalogItemID,
                Cost = 0,
                PercentageCommision = 0,
            };
            var tbPriceDto2 = new TbPriceDto
            {
                CompanyId = companyId,
                ListPriceId = Convert.ToInt32(txtDetailListPriceID ?? "0"),
                TypePriceId = txtDetailTypePriceID2.CatalogItemID,
                Cost = 0,
                PercentageCommision = 0,
            };
            var tbPriceDto3 = new TbPriceDto
            {
                CompanyId = companyId,
                ListPriceId = Convert.ToInt32(txtDetailListPriceID ?? "0"),
                TypePriceId = txtDetailTypePriceID3.CatalogItemID,
                Cost = 0,
                PercentageCommision = 0,
            };
            var tbPriceDto4 = new TbPriceDto
            {
                CompanyId = companyId,
                ListPriceId = Convert.ToInt32(txtDetailListPriceID ?? "0"),
                TypePriceId = txtDetailTypePriceID4.CatalogItemID,
                Cost = 0,
                PercentageCommision = 0,
            };
            var tbPriceDtos = new List<TbPriceDto> { tbPriceDto0, tbPriceDto1, tbPriceDto2, tbPriceDto3, tbPriceDto4 };
            frmInventoryItem.ObjListPriceItem = new BindingList<TbPriceDto>(tbPriceDtos);
            frmInventoryItem.txtRealStateEmail.Text = "";
            frmInventoryItem.txtRealStatePhone.Text = "";
            frmInventoryItem.SaveInsert();
            return frmInventoryItem.ItemId;
        }

        #endregion

        #region Eventos

        private void BtnImprmir_Click(object? sender, EventArgs e)
        {
            _backgroundWorker = new BackgroundWorker();
            if (!progressPanel.Visible)
            {
                progressPanel.Width = Width;
                progressPanel.Height = Height;
                progressPanel.Visible = true;
            }

            _backgroundWorker.DoWork += (ob, ev) => { ComandPrinter(); };

            _backgroundWorker.RunWorkerCompleted += (ob, ev) =>
            {
                if (ev.Error is not null)
                {
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", ev.Error.Message, this);
                }
                else if (ev.Cancelled)
                {
                    //se canceló por el usuario
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Warning, "Error", "Operación cancelada por el usuario", this);
                }

                if (progressPanel.Visible)
                {
                    progressPanel.Visible = false;
                }
            };

            if (!progressPanel.Visible)
            {
                progressPanel.Size = Size;
                progressPanel.Visible = true;
            }

            if (!_backgroundWorker.IsBusy)
            {
                _backgroundWorker.RunWorkerAsync();
            }
        }

        private void btnAgregarArchivo_Click(object sender, EventArgs e)
        {
            var openFileDialog = new XtraOpenFileDialog();
            openFileDialog.Title = @"Seleccionar archivo";
            var dialogResult = openFileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                var file = openFileDialog.SafeFileName;
                _renderGridFiles.AddRow(file);
            }
        }

        private void BtnEliminarOnClick(object? sender, EventArgs e)
        {
            var result = _objInterfazCoreWebRenderInView.XtraMessageBoxArgs(TypeError.Error, "Eliminar", "¿Seguro desea eliminar el cliente seleccionado? Esta acción no se puede revertir.");

            if (result == DialogResult.No)
            {
                return;
            }

            _backgroundWorker = new BackgroundWorker();
            if (!progressPanel.Visible)
            {
                progressPanel.Width = Width;
                progressPanel.Height = Height;
                progressPanel.Visible = true;
            }

            _backgroundWorker.DoWork += (ob, ev) => { ComandDelete(); };

            _backgroundWorker.RunWorkerCompleted += (ob, ev) =>
            {
                if (ev.Error is not null)
                {
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", ev.Error.Message, this);
                }
                else if (ev.Cancelled)
                {
                    //se canceló por el usuario
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Warning, "Error", "Operación cancelada por el usuario", this);
                }
                else
                {
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Informacion, "Eliminar", "Se ha eliminado el registro de forma correcta", this);
                    Close();
                }

                if (progressPanel.Visible)
                {
                    progressPanel.Visible = false;
                }
            };

            if (!progressPanel.Visible)
            {
                progressPanel.Size = Size;
                progressPanel.Visible = true;
            }

            if (!_backgroundWorker.IsBusy)
            {
                _backgroundWorker.RunWorkerAsync();
            }
        }

        private void btnNewDetailTransaction_Click(object sender, EventArgs e)
        {
            var currency = (ComboBoxItem)txtCurrencyID.SelectedItem;
            var currencyIdKey = Convert.ToInt32(currency.Key);
            if (_txtProviderId == 0)
            {
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Proveedor", "Seleccione un proveedo", this);
                return;
            }

            var formTypeListSearch = new FormTypeListSearch("Lista de Productos", ObjComponentItem!.ComponentID,
                "SELECCIONAR_ITEM_TO_PROVIDER", true, @"{providerID:" + _txtProviderId + ",currencyID:" + currencyIdKey + "}",
                true, "__app_inventory_item__add__callback__fnObtenerListadoProductos__comando__pantalla_abierta_desde_la_compra", 0, 5, "", false);
            formTypeListSearch.EventoCallBackAceptarEvent += EventoCallBackAceptarItem;
            formTypeListSearch.ShowDialog(this);
        }

        private void EventoCallBackAceptarItem(dynamic mensaje)
        {
            var objWebToolsHelper = new WebToolsHelper();
            FnOnCompleteNewItemPopPub(mensaje);
        }

        private void btnDeleteDetailTransaction_Click(object sender, EventArgs e)
        {
            var selectedRowsCount = gridViewTransactionMasterDetail.SelectedRowsCount;
            if (selectedRowsCount <= 0)
            {
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Warning, "Eliminar", "Seleccione un valor a eliminar de la tabla", this);
                return;
            }

            gridViewTransactionMasterDetail.DeleteSelectedRows();
            FnUpdateDetail();
        }

        private void btnMasInformacion_Click(object sender, EventArgs e)
        {
            var selectedValue = _bindingListTransactionMasterDetail.ElementAtOrDefault(gridViewTransactionMasterDetail.FocusedRowHandle);
            if (selectedValue is null) return;
            var formInventoryInputEditMasInfo = new FormInventoryInputEditMasInfo(selectedValue);
            formInventoryInputEditMasInfo.ShowDialog(this);
        }

        private void gridViewTransactionMasterDetail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            FnUpdateDetail();
        }

        private void txtIva_EditValueChanged(object sender, EventArgs e)
        {
            FnUpdateDetail();
        }

        private void txtDiscount_EditValueChanged(object sender, EventArgs e)
        {
            FnUpdateDetail();
        }

        private void btnClearProvider_Click(object sender, EventArgs e)
        {
            _txtProviderId = 0;
            txtProviderDescription.Clear();
        }

        private void btnSearchProvider_Click(object sender, EventArgs e)
        {
            var formTypeListSearch = new FormTypeListSearch("Lista de Proveedores", ObjComponentProvider!.ComponentID,
                "SELECCIONAR_PROVEEDOR", true, @"", false, "", 0, 5, "", true);
            formTypeListSearch.EventoCallBackAceptarEvent += EventoCallBackAceptarProveedor;
            formTypeListSearch.ShowDialog(this);
        }

        private void EventoCallBackAceptarProveedor(dynamic mensaje)
        {
            FnOnCompleteNewProviderPopPub(mensaje);
        }

        private void btnClearOrdenCompra_Click(object sender, EventArgs e)
        {
            txtTransactionMasterIDOrdenCompra = 0;
            txtTransactionNumberOrdenCompra.Clear();
        }

        private void btnSearchOrdenCompra_Click(object sender, EventArgs e)
        {
            var formTypeListSearch = new FormTypeListSearch("Lista de Proveedores", ObjComponent!.ComponentID,
                "SELECCIONAR_PLANTILLA_DE_COMPRA", true, @"", false, "", 0, 5, "", true);
            formTypeListSearch.EventoCallBackAceptarEvent += EventoCallBackAceptarOrdenCompra;
            formTypeListSearch.ShowDialog(this);
        }

        private void EventoCallBackAceptarOrdenCompra(dynamic mensaje)
        {
            FnOnCompleteNewOrdenCompraPopPub(mensaje);
        }

        private void txtCurrencyID_SelectedIndexChanged(object sender, EventArgs e)
        {
            _bindingListTransactionMasterDetail.Clear();
        }

        private void btnPrinterDetailTransaction_Click(object sender, EventArgs e)
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

            if (gridViewTransactionMasterDetail.SelectedRowsCount <= 0)
            {
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Imprimir", "No se ha seleccionado un articulo a imprimir", this);
                return;
            }

            foreach (var selectedRow in gridViewTransactionMasterDetail.GetSelectedRows())
            {
                var itemId = gridViewTransactionMasterDetail.GetRowCellValue(selectedRow, colItemId);
                if (itemId is null)
                {
                    continue;
                }

                var item = _itemModel.GetRowByPk(user.CompanyID, Convert.ToInt32(itemId));
                if (item is null)
                {
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Imprimir", $"No existe el articulo con el Id {itemId}", this);
                    return;
                }

                _objInterfazCoreWebRenderInView.PrintBarCodeItem(item, cantidadImprimirFrm.CantidadImprimir);
            }
        }

        private void gridViewTransactionMasterDetail_LostFocus(object sender, EventArgs e)
        {
            colMas.AppearanceCell.ForeColor = Color.Black;
            btnMasInformacion.Appearance.ForeColor = Color.Black;
        }

        private void btnNuevoArticulo_Click(object sender, EventArgs e)
        {
            var frmItemEdit = new FormInventoryItemEdit(TypeOpenForm.Init, 0)
            {
                MdiParent = CoreFormList.Principal()
            };
            frmItemEdit.Show();
        }

        #endregion
    }
}