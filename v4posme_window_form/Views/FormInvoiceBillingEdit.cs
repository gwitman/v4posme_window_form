using System.IO;
using System.Reflection;
using DevExpress.XtraEditors;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomHelper;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Models;
using v4posme_library.ModelsDto;
using v4posme_window.Interfaz;
using v4posme_window.Libraries;
using v4posme_window.Template;

namespace v4posme_window.Views
{
    public partial class FormInvoiceBillingEdit : Form, IFormTypeEdit
    {
        #region Libreria Window

        private readonly CoreWebRenderInView _objInterfazCoreWebRenderInView = new CoreWebRenderInView();

        #endregion

        #region Helper Dll

        private WebToolsHelper _objInterfazWebToolsHelper = new WebToolsHelper();

        #endregion

        #region Libreria DLL

        private readonly ICoreWebPermission _objInterfazCoreWebPermission = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebPermission>();

        private readonly ICoreWebWorkflow _objInterfazCoreWebWorkflow = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebWorkflow>();

        private readonly ICoreWebParameter _objInterfazCoreWebParameter = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebParameter>();

        private readonly ICoreWebAccounting _objInterfazCoreWebAccounting = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebAccounting>();

        private readonly ICoreWebTransaction _objInterfazCoreWebTransaction = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTransaction>();

        private readonly ICoreWebCurrency _objInterfazCoreWebCurrency = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCurrency>();

        private readonly ICoreWebTools _objInterfazCoreWebTools = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();

        private readonly ICoreWebCounter _objInterfazCoreWebCounter = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCounter>();

        #endregion

        #region Libreria Model Custom DLL

        private readonly IItemModel _objInterfazItemModel = VariablesGlobales.Instance.UnityContainer.Resolve<IItemModel>();

        private readonly IItemSkuModel _objInterfazItemSkuModel = VariablesGlobales.Instance.UnityContainer.Resolve<IItemSkuModel>();

        private readonly ICustomerCreditDocumentModel _objInterfazCustomerCreditDocument = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerCreditDocumentModel>();

        private readonly ICustomerCreditModel _objInterfazCustomerCredit = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerCreditModel>();

        private readonly ICustomerCreditLineModel _objInterfazCustomerCreditLine = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerCreditLineModel>();

        private readonly IListPriceModel _objInterfazListPriceModel = VariablesGlobales.Instance.UnityContainer.Resolve<IListPriceModel>();

        private readonly ICompanyCurrencyModel _objInterfazCompanyCurrencyModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyCurrencyModel>();

        private readonly ICompanyComponentConceptModel _objInterfazCompanyComponentConceptModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyComponentConceptModel>();

        private readonly IEmployeeModel _objInterfazEmployeeModel = VariablesGlobales.Instance.UnityContainer.Resolve<IEmployeeModel>();

        private readonly IBankModel _objInterfazBankModel = VariablesGlobales.Instance.UnityContainer.Resolve<IBankModel>();

        private readonly ITransactionMasterDetailCreditModel _objInterfazTransactionMasterDetailCreditModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterDetailCreditModel>();

        private readonly ITransactionCausalModel _objInterfazTransactionCausalModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionCausalModel>();

        private readonly ITransactionMasterModel _objInterfazTransactionMasterModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterModel>();

        private readonly ITransactionMasterInfoModel _objInterfazTransactionMasterInfoModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterInfoModel>();

        private readonly ITransactionMasterDetailModel _objInterfazTransactionMasterDetailModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterDetailModel>();

        private readonly ITransactionMasterConceptModel _objInterfazTransactionMasterConceptModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterConceptModel>();

        private readonly IUserWarehouseModel _objInterfazUserWarehouseModel = VariablesGlobales.Instance.UnityContainer.Resolve<IUserWarehouseModel>();

        private readonly ICustomerModel _objInterfazCustomerModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerModel>();

        private readonly ICoreWebCatalog _objInterfazCoreWebCatalog = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCatalog>();

        private readonly IProviderModel _objInterfazProviderModel = VariablesGlobales.Instance.UnityContainer.Resolve<IProviderModel>();

        private readonly IPriceModel _objInterfazPriceModel = VariablesGlobales.Instance.UnityContainer.Resolve<IPriceModel>();

        private readonly INaturalModel _objInterfazNaturalModel = VariablesGlobales.Instance.UnityContainer.Resolve<INaturalModel>();

        private readonly ILegalModel _objInterfazLegalModel = VariablesGlobales.Instance.UnityContainer.Resolve<ILegalModel>();

        private readonly ICustomerCreditAmortizationModel _objInterfazCustomerCreditAmortizationModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerCreditAmortizationModel>();

        private readonly ICustomerCreditLineModel _objInterfazCustomerCreditLineModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerCreditLineModel>();

        #endregion

        #region Properties

        public List<TbWorkflowStage>? ObjListWorkflowStage { get; private set; }
        public TbComponent? ObjComponentTransactionBilling { get; private set; }
        public decimal ExchangeRate { get; private set; }
        public List<TbCompanyCurrencyDto> ObjListCurrency { get; private set; }
        public TbCurrency? ObjCurrency { get; private set; }
        public TbListPrice? ObjListPrice { get; private set; }
        public List<TbEmployeeDto> ObjListEmployee { get; private set; }
        public List<TbBank> ObjListBank { get; private set; }
        public TbComponent? ObjComponentItem { get; private set; }
        public TbComponent? ObjComponentCustomer { get; private set; }
        public List<TbTransactionCausal?> ObjCausal { get; private set; }
        public int? WarehouseId { get; private set; }
        public List<TbUserWarehouseDto> ObjListWarehouse { get; private set; }
        public TbCustomer ObjCustomerDefault { get; private set; }
        public List<TbCatalogItem> ObjListTypePrice { get; private set; }
        public List<TbCatalogItem> ObjListPay { get; private set; }

        public List<TbCatalogItem> ObjListMesa { get; private set; }

        public List<TbCatalogItem> ObjListZone { get; private set; }
        public List<TbProviderDto> ListProvider { get; private set; }

        public List<TbCustomerCreditLineDto> ObjListCustomerCreditLine { get; private set; }

        public List<TbCustomerCreditAmortizationDto> ObjCustomerCreditAmoritizationAll { get; private set; }

        public TbCompanyParameter? ParameterCausalTypeCredit { get; private set; }

        public TbCurrency? ObjCurrencyCordoba { get; private set; }

        public TbCurrency? ObjCurrencyDolares { get; private set; }

        public TbNaturale ObjEmployeeNatural { get; private set; }

        public TbLegal ObjLegalDefault { get; private set; }

        public TbNaturale ObjNaturalDefault { get; private set; }

        public string? ObjParameterPantallaParaFacturar { get; private set; }

        public string? ObjParameterMostrarImagenEnSeleccion { get; private set; }

        public string? ObjParameterRegresarAListaDespuesDeGuardar { get; private set; }

        public string? ObjParameterInvoiceBillingQuantityZero { get; private set; }

        public string? ObjParameterInvoiceBillingSelectitem { get; private set; }

        public Dictionary<string, string?> ObjListParameterAll { get; private set; }

        public string? ObjParameterobjParameterInvoiceBillingPrinterUrlBar { get; private set; }

        public string? ObjParameterInvoiceBillingPrinterDirectUrlBar { get; private set; }

        public string? ObjParameterInvoiceBillingPrinterDirectNameDefaultBar { get; private set; }

        public string? ObjParameterInvoiceBillingShowCommandBar { get; private set; }

        public string? ObjParameterInvoiceBillingApplyTypePriceOnDayPorMayor { get; private set; }

        public string? ObjParameterCustomPopupFacturacion { get; private set; }

        public string? ObjParameterCxcFrecuenciaPayDefault { get; private set; }

        public string? ObjParameterCxcPlazoDefault { get; private set; }

        public string? ObjParameterScrollDelModalDeSeleccionProducto { get; private set; }

        public string? ObjParameterAlturaDelModalDeSeleccionProducto { get; private set; }

        public string? ObjParameterAmortizationDuranteFactura { get; private set; }

        public string? ObjParameterHidenFiledItemNumber { get; private set; }

        public string? ObjParameterCantidadItemPoup { get; private set; }

        public string? ObjParameterScanerProducto { get; private set; }

        public string? ObjParameterImprimirPorCadaFactura { get; private set; }

        public string? ObjParameterInvoiceAutoApply { get; private set; }

        public string? ObjParameterTipoWarehouseDespacho { get; private set; }

        public string? ObjParameterTypePreiceDefault { get; private set; }

        public List<TbItemDto> ObjTransactionMasterItem { get; set; }

        public List<TbItemSkuDto> ObjTransactionMasterItemSku { get; set; }

        public List<TbCompanyComponentConcept> ObjTransactionMasterItemConcepto { get; set; }

        public List<TbPrice> ObjTransactionMasterItemPrice { get; set; }

        public TbTransactionMasterDetailCredit ObjTransactionMasterDetailCredit { get; set; }

        public string? ObjParameterInvoiceBillingPrinterDirectNameDefaDefaultBar { get; set; }

        public string? ObjParameterInvoiceBillingPrinterUrlBar { get; set; }

        public string? ObjParameterInvoiceBillingPrinterDirectUrlPrinterDirectUrlBar { get; set; }

        public string? ObjParameterTipoPrinterDonwload { get; set; }

        public string? ObjParameterInvoiceOpenCashPassword { get; set; }

        public string? ObjParameterInvoiceOpenCashWhenPrinterInvoice { get; set; }


        public List<TbCompanyComponentConcept> ObjTransactionMasterDetailConcept { get; set; }

        public List<TbTransactionMasterDetailDto> ObjTransactionMasterDetailWarehouse { get; set; }

        public List<TbTransactionMasterDetailDto> ObjTransactionMasterDetail { get; set; }

        public TbTransactionMasterInfoDto ObjTransactionMasterInfo { get; set; }

        public TbTransactionMasterDto ObjTransactionMaster { get; set; }

        public string? UrlPrinterDocument { get; set; }

        public string? ObjParameterInvoiceButtomPrinterFidLocalPaymentAndAmortization { get; set; }

        public string? UrlPrinterDocumentCocinaDirect { get; set; }

        public string? UrlPrinterDocumentCocina { get; set; }

        public string? ObjParameterShowComandoDeCocina { get; set; }

        public string? ObjParameterInvoiceBillingPrinterDirectUrl { get; set; }

        public string? ObjParameterInvoiceBillingPrinterDirect { get; set; }

        #endregion


        #region Variables internas

        private int? CompanyId { get; set; }
        private int? TransactionId { get; set; }
        private int? TransactionMasterId { get; set; }
        private TypeOpenForm TypeOpen { get; set; }
        private int TxtCustomerId { get; set; }
        private int TxtStatusOldId { get; set; }
        private int TxtStatusId { get; set; }

        #endregion

        #region Init

        public FormInvoiceBillingEdit(TypeOpenForm typeOpen, int companyId, int transactionId, int transactionMasterId)
        {
            InitializeComponent();

            // Suscribir al manejador de excepciones global
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            CompanyId = companyId;
            TransactionId = transactionId;
            TransactionMasterId = transactionMasterId;
            TypeOpen = typeOpen;
        }


        public void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            CustomException.LogException(e.Exception);
        }

        public void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            CustomException.LogException((Exception)e.ExceptionObject);
        }


        private void FormInvoiceBillingEdit_Load(object sender, EventArgs e)
        {
            if (TypeOpen == TypeOpenForm.Init)
            {
                PreRender();
            }

            if (TypeOpen == TypeOpenForm.Init && TransactionMasterId > 0)
            {
                LoadEdit();
            }

            if (TypeOpen == TypeOpenForm.Init && TransactionMasterId == 0)
            {
                LoadNew();
            }
        }

        #endregion


        #region Eventos Formulario

        private void EventoCallBackAceptar(dynamic mensaje)
        {
            // Realizar la lógica que desees en el formulario padre
            WebToolsHelper objWebToolsHelper = new WebToolsHelper();
            MessageBox.Show("Evento en el formulario hijo: " +
                            objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "itemID", "0"));
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            var formTypeListSearch = new FormTypeListSearch("Lista de Productos", 33,
                "SELECCIONAR_ITEM_BILLING_POPUP_INVOICE", true,
                "{warehouseID:4,listPriceID:12,typePriceID:154,currencyID:1}", false, "", 0, 5, "");
            formTypeListSearch.EventoCallBackAceptar_ += EventoCallBackAceptar;
            formTypeListSearch.ShowDialog(this);
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {
        }

        private void dateEdit1_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void tablePanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void tablePanel2_Paint(object sender, PaintEventArgs e)
        {
        }

        #endregion

        #region Metodos

        public void ComandDelete()
        {
            if (VariablesGlobales.Instance.User is null)
            {
                return;
            }

            try
            {
                var permissionNone = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_NONE"]);
                var commandEliminable = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_ELIMINABLE"]);
                var commandAplicable = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_APLICABLE"]);
                var urlSuffix = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX"];
                var appNeedAuthentication = VariablesGlobales.ConfigurationBuilder["APP_NEED_AUTHENTICATION"];
                var resultPermission = 0;

                if (appNeedAuthentication == "true")
                {
                    var permited = _objInterfazCoreWebPermission.UrlPermited("app_invoice_billing", "delete",
                        urlSuffix!,
                        VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                        VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                        VariablesGlobales.Instance.ListMenuHiddenPopup);
                    if (!permited)
                    {
                        _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Permisos",
                            "No tiene acceso a controlador", this);
                        return;
                    }

                    resultPermission = _objInterfazCoreWebPermission.UrlPermissionCmd("app_invoice_billing", "delete",
                        urlSuffix!,
                        VariablesGlobales.Instance.Role, VariablesGlobales.Instance.User,
                        VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                        VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                        VariablesGlobales.Instance.ListMenuHiddenPopup);

                    if (resultPermission == permissionNone)
                    {
                        _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Permisos",
                            "No se encontraron permisos", this);
                        return;
                    }
                }

                //Obtener variables de autenticacion
                var objUser = VariablesGlobales.Instance.User;
                var objRole = VariablesGlobales.Instance.Role;

                //Validacion
                if (CompanyId == 0 && TransactionId == 0 && TransactionMasterId == 0)
                {
                    throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_PARAMETER"]);
                }

                //Obtener registros
                var objTm = _objInterfazTransactionMasterModel.GetRowByPk(CompanyId!.Value, TransactionId!.Value, TransactionMasterId!.Value);
                var objCustomerCreditDocument =
                    _objInterfazCustomerCreditDocument.GetRowByDocument(objTm.CompanyId, objTm.EntityId!.Value,
                        objTm.TransactionNumber!);

                //Validaciones
                if (resultPermission == permissionNone && (objTm.CreatedBy != objUser.UserId))
                {
                    throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_DELETE"]);
                }

                if (_objInterfazCoreWebAccounting.CycleIsCloseByDate(objUser.CompanyId, objTm.TransactionOn!.Value))
                {
                    throw new Exception("EL DOCUMENTO NO PUEDE SE ELIMINADO, EL CICLO CONTABLE ESTA CERRADO");
                }

                var workflowStage = _objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_billing",
                    "statusID", objTm.StatusId!.Value, commandEliminable, objUser.CompanyId,
                    objUser.BranchId, objRole!.RoleId);
                if (!(workflowStage!.Value))
                {
                    throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_WORKFLOW_DELETE"]);
                }

                ////Validar si la factura es de credito y esta aplicada y tiene abono	
                var parameterCausalTypeCredit =
                    _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_CREDIT", objUser.CompanyId);
                var causalIdTypeCredit = parameterCausalTypeCredit!.Value.Split(",");
                var exisCausalInCredit =
                    causalIdTypeCredit.Any(elemento => elemento == objTm.TransactionCausalId.ToString());

                var validateWorkflowStage = _objInterfazCoreWebWorkflow.ValidateWorkflowStage(
                    "tb_transaction_master_billing", "statusID", objTm.StatusId!.Value, commandAplicable,
                    objUser.CompanyId, objUser.BranchId, objRole.RoleId
                )!.Value;

                if (
                    validateWorkflowStage &&
                    exisCausalInCredit &&
                    objCustomerCreditDocument!.Amount != objCustomerCreditDocument.Balance &&
                    objCustomerCreditDocument.Balance > 0
                )
                {
                    throw new Exception("Factura con abonos y balance mayor que 1");
                }

                if (validateWorkflowStage)
                {
                    //Actualizar fecha en la transacciones oroginal
                    var dataContext = new DataContext();
                    var dataNewTm = dataContext.TbTransactionMasters.First(u =>
                        u.CompanyId == objTm.CompanyId && u.TransactionId == objTm.TransactionId &&
                        u.TransactionMasterId == objTm.TransactionMasterId);
                    dataNewTm.StatusIdchangeOn = DateTime.Now;
                    _objInterfazTransactionMasterModel.UpdateAppPosme(dataNewTm.CompanyId, dataNewTm.TransactionId,
                        dataNewTm.TransactionMasterId, dataNewTm);

                    //Ejecutar el procedimiento de reversion
                    var transactionIdRevert = _objInterfazCoreWebParameter.GetParameter("INVOICE_TRANSACTION_REVERSION_TO_BILLING",
                        objUser.CompanyId);
                    var transactionIdRevertValue = Convert.ToInt32(transactionIdRevert!.Value);
                    _objInterfazCoreWebTransaction.CreateInverseDocumentByTransaccion(objTm.CompanyId, objTm.TransactionId, objTm.TransactionMasterId, transactionIdRevertValue, 0);


                    if (exisCausalInCredit)
                    {
                        //Valores de tasa de cambio          
                        var objCurrencyDolares = _objInterfazCoreWebCurrency.GetCurrencyExternal(objTm.CompanyId);
                        var objCurrencyCordoba = _objInterfazCoreWebCurrency.GetCurrencyDefault(objTm.CompanyId);
                        var dateOn = DateOnly.FromDateTime(DateTime.Now);
                        var exchangeRate = _objInterfazCoreWebCurrency.GetRatio(objTm.CompanyId, dateOn, 1, objCurrencyDolares!.CurrencyId, objCurrencyCordoba!.CurrencyId);

                        //cancelar el documento de credito					
                        var shareDocumentAnuladoStatusID = Convert.ToInt32(_objInterfazCoreWebParameter.GetParameter("SHARE_DOCUMENT_ANULADO", objUser!.CompanyId)!.Value);

                        var objCustomerCreditDocumentNew = dataContext.TbCustomerCreditDocuments.Where(
                            c => c.CustomerCreditDocumentId ==
                                 objCustomerCreditDocument!.CustomerCreditDocumentId!.Value).FirstOrDefault();

                        objCustomerCreditDocumentNew!.StatusId = shareDocumentAnuladoStatusID;
                        _objInterfazCustomerCreditDocument.UpdateAppPosme(objCustomerCreditDocument!.CustomerCreditDocumentId!.Value, objCustomerCreditDocumentNew);

                        var amountDol = objCustomerCreditDocument.Balance / exchangeRate;
                        var amountCor = objCustomerCreditDocument.Balance;


                        //aumentar el blance de la linea
                        var tbCustomerCreditLine =
                            _objInterfazCustomerCreditLine.GetRowByPk(objCustomerCreditDocument.CustomerCreditLineId);
                        tbCustomerCreditLine.Balance += (tbCustomerCreditLine.CurrencyId ==
                                                         objCurrencyDolares.CurrencyId
                            ? amountDol!.Value
                            : amountCor!.Value);
                        _objInterfazCustomerCreditLine.UpdateAppPosme(objCustomerCreditDocument.CustomerCreditLineId, tbCustomerCreditLine);

                        //aumentar el balance de credito
                        var objCustomerCredit = _objInterfazCustomerCredit.GetRowByPk(objTm.CompanyId, objTm.BranchId!.Value, objTm.EntityId!.Value);
                        objCustomerCredit.BalanceDol = objCustomerCredit.BalanceDol + amountDol!.Value;
                        _objInterfazCustomerCredit.UpdateAppPosme(objCustomerCredit.CompanyId, objCustomerCredit.BranchId, objCustomerCredit.EntityId, objCustomerCredit);
                    }
                }
                else
                {
                    //	//Eliminar el Registro			
                    _objInterfazTransactionMasterModel.DeleteAppPosme(objUser.CompanyId, objTm.TransactionId, objTm.TransactionMasterId);
                    _objInterfazTransactionMasterDetailModel.DeleteWhereTm(objUser.CompanyId, objTm.TransactionId, objTm.TransactionMasterId);
                }
            }
            catch (Exception ex)
            {
                var objCoreWebRenderInView = new CoreWebRenderInView();
                Console.WriteLine(ex);
                objCoreWebRenderInView.GetMessageAlert(TypeError.Error, @"Error eliminando", ex.ToString(), this);
            }
        }

        public void ComandPrinter()
        {
            throw new NotImplementedException();
        }

        public void LoadEdit()
        {
            try
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
                if (appNeedAuthentication == "true")
                {
                    var permited = _objInterfazCoreWebPermission.UrlPermited("app_invoice_billing", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                    if (!permited)
                    {
                        throw new Exception(notAccessControl);
                    }

                    var resultPermission = _objInterfazCoreWebPermission.UrlPermissionCmd("app_invoice_billing", "edit", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                    if (resultPermission == permissionNone)
                    {
                        throw new Exception(notAllEdit);
                    }
                }

                ObjComponentCustomer = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_customer");
                if (ObjComponentCustomer is null)
                {
                    throw new Exception("EL COMPONENTE 'tb_customer' NO EXISTE...");
                }

                ObjComponentItem = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_item");
                if (ObjComponentItem is null)
                {
                    throw new Exception("EL COMPONENTE 'tb_item' NO EXISTE...");
                }

                ObjComponentTransactionBilling = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_transaction_master_billing");
                if (ObjComponentTransactionBilling is null)
                {
                    throw new Exception("EL COMPONENTE 'tb_transaction_master_billing' NO EXISTE...");
                }

                TransactionId = _objInterfazCoreWebTransaction.GetTransactionId(user.CompanyId, "tb_transaction_master_billing", 0)!.Value;
                ObjCurrency = _objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyId);
                ObjCurrencyDolares = _objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyId);
                var customerDefault = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_CLIENTDEFAULT", user.CompanyId);
                ObjListPrice = _objInterfazListPriceModel.GetListPriceToApply(user.CompanyId);
                ObjListCurrency = _objInterfazCompanyCurrencyModel.GetByCompany(user.CompanyId);
                if (ObjListPrice is null)
                {
                    throw new Exception("NO EXISTE UNA LISTA DE PRECIO PARA SER APLICADA");
                }

                var objParameterAll = _objInterfazCoreWebParameter.GetParameterAll(user.CompanyId);
                var parameterValue = _objInterfazCoreWebParameter.GetParameter("INVOICE_BUTTOM_PRINTER_FIDLOCAL_PAYMENT_AND_AMORTIZACION", user.CompanyId);
                ObjParameterInvoiceButtomPrinterFidLocalPaymentAndAmortization = parameterValue!.Value;

                ObjParameterInvoiceBillingQuantityZero = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_QUANTITY_ZERO", user.CompanyId)!.Value;
                ObjParameterInvoiceBillingPrinterDirect = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_PRINTER_DIRECT", user.CompanyId)!.Value;
                ObjParameterInvoiceBillingPrinterDirectUrl = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_PRINTER_DIRECT_URL", user.CompanyId)!.Value;
                ObjParameterShowComandoDeCocina = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_SHOW_COMMAND_FOOT", user.CompanyId)!.Value;
                UrlPrinterDocumentCocina = _objInterfazCoreWebParameter.GetParameter("INVOICE_URL_PRINTER_COCINA", user.CompanyId)!.Value;
                UrlPrinterDocumentCocinaDirect = _objInterfazCoreWebParameter.GetParameter("INVOICE_URL_PRINTER_COCINA_DIRECT", user.CompanyId)!.Value;
                ObjParameterImprimirPorCadaFactura = _objInterfazCoreWebParameter.GetParameter("INVOICE_PRINT_BY_INVOICE", user.CompanyId)!.Value;
                ObjParameterRegresarAListaDespuesDeGuardar = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_SAVE_AFTER_TO_LIST", user.CompanyId)!.Value;
                ObjParameterScanerProducto = _objInterfazCoreWebParameter.GetParameter("INVOICE_SHOW_POPUP_FIND_PRODUCTO_NOT_SCANER", user.CompanyId)!.Value;
                ObjParameterCantidadItemPoup = _objInterfazCoreWebParameter.GetParameter("INVOICE_CANTIDAD_ITEM", user.CompanyId)!.Value;
                ObjParameterHidenFiledItemNumber = _objInterfazCoreWebParameter.GetParameter("INVOICE_HIDEN_ITEMNUMBER_IN_POPUP", user.CompanyId)!.Value;
                ObjParameterAmortizationDuranteFactura = _objInterfazCoreWebParameter.GetParameter("INVOICE_PARAMTER_AMORITZATION_DURAN_INVOICE", user.CompanyId)!.Value;
                ObjParameterAlturaDelModalDeSeleccionProducto = _objInterfazCoreWebParameter.GetParameter("INVOICE_ALTO_MODAL_DE_SELECCION_DE_PRODUCTO_AL_FACTURAR", user.CompanyId)!.Value;
                ObjParameterScrollDelModalDeSeleccionProducto = _objInterfazCoreWebParameter.GetParameter("INVOICE_SCROLL_DE_MODAL_EN_SELECCION_DE_PRODUTO_AL_FACTURAR", user.CompanyId)!.Value;
                ObjParameterMostrarImagenEnSeleccion = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_SHOW_IMAGE_IN_DETAIL_SELECTION", user.CompanyId)!.Value;
                ObjParameterPantallaParaFacturar = _objInterfazCoreWebParameter.GetParameter("INVOICE_PANTALLA_FACTURACION", user.CompanyId)!.Value;
                UrlPrinterDocument = _objInterfazCoreWebParameter.GetParameter("INVOICE_URL_PRINTER", user.CompanyId)!.Value;

                ObjTransactionMaster = _objInterfazTransactionMasterModel.GetRowByPk(user.CompanyId, TransactionId!.Value, TransactionMasterId!.Value);
                ObjTransactionMasterInfo = _objInterfazTransactionMasterInfoModel.GetRowByPk(user.CompanyId, TransactionId!.Value, TransactionMasterId!.Value);
                ObjTransactionMasterDetail = _objInterfazTransactionMasterDetailModel.GetRowByTransaction(user.CompanyId, TransactionId!.Value, TransactionMasterId!.Value);
                ObjTransactionMasterDetailWarehouse = _objInterfazTransactionMasterDetailModel.GetRowByTransactionAndWarehouse(user.CompanyId, TransactionId!.Value, TransactionMasterId!.Value);
                ObjTransactionMasterDetailConcept = _objInterfazTransactionMasterConceptModel.GetRowByTransactionMasterConcept(user.CompanyId, TransactionId!.Value, TransactionMasterId!.Value, ObjComponentItem.ComponentId);

                ExchangeRate = _objInterfazCoreWebCurrency.GetRatio(user.CompanyId, DateOnly.FromDateTime(DateTime.Now), decimal.One, ObjCurrencyDolares!.CurrencyId, ObjCurrency!.CurrencyId);
                ObjListEmployee = _objInterfazEmployeeModel.GetRowByBranchIdAndType(user.CompanyId, user.BranchId, Convert.ToInt32(objParameterAll["INVOICE_TYPE_EMPLOYEER"]));
                ObjListBank = _objInterfazBankModel.GetByCompany(user.CompanyId);
                ObjCausal = _objInterfazTransactionCausalModel.GetCausalByBranch(user.CompanyId, TransactionId!.Value, user.BranchId);
                WarehouseId = ObjCausal.First()!.WarehouseSourceId;
                ObjListWarehouse = _objInterfazUserWarehouseModel.GetRowByUserIdAndFacturable(user.CompanyId, user.UserId);
                ObjCustomerDefault = _objInterfazCustomerModel.GetRowByCode(user.CompanyId, customerDefault!.Value);
                ObjListTypePrice = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_price", "typePriceID", user.CompanyId);
                ObjListZone = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_transaction_master_info_billing", "zoneID", user.CompanyId);
                ObjListMesa = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_transaction_master_info_billing", "mesaID", user.CompanyId);
                ObjListPay = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer_credit_line", "periodPay", user.CompanyId);
                ListProvider = _objInterfazProviderModel.GetRowByCompany(user.CompanyId);
                ObjListWorkflowStage = _objInterfazCoreWebWorkflow.GetWorkflowStageByStageInit("tb_transaction_master_billing", "statusID", ObjTransactionMaster.StatusId!.Value, role!.CompanyId, role.BranchId, role.RoleId);

                ObjParameterInvoiceOpenCashWhenPrinterInvoice = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_OPEN_CASH_WHEN_PRINTER_INVOICE", user.CompanyId);
                ObjParameterInvoiceOpenCashPassword = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_OPEN_CASH_PASSWORD", user.CompanyId);
                ObjParameterCustomPopupFacturacion = _objInterfazCoreWebParameter.GetParameterValue("CORE_VIEW_CUSTOM_PANTALLA_DE_FACTURACION_POPUP_SELECCION_PRODUCTO_FORMA_MOSTRAR", user.CompanyId);
                ObjParameterTipoPrinterDonwload = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_PRINTER_DOWNLOAD", user.CompanyId);
                ObjParameterInvoiceBillingApplyTypePriceOnDayPorMayor = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_APPLY_TYPE_PRICE_ON_DAY_POR_MAYOR", user.CompanyId);
                ObjParameterInvoiceBillingShowCommandBar = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_SHOW_COMMAND_BAR", user.CompanyId);
                ObjParameterInvoiceBillingPrinterDirectNameDefaDefaultBar = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_PRINTER_DIRECT_NAME_DEFAULT_BAR", user.CompanyId);
                ObjParameterInvoiceBillingPrinterDirectUrlPrinterDirectUrlBar = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_PRINTER_DIRECT_URL_BAR", user.CompanyId);
                ObjParameterInvoiceBillingPrinterUrlBar = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_PRINTER_URL_BAR", user.CompanyId);
                ObjParameterInvoiceBillingSelectitem = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_SELECTITEM", user.CompanyId);


                if (ObjCustomerDefault is null)
                {
                    throw new Exception("NO EXISTE EL CLIENTE POR DEFECTO");
                }

                ObjNaturalDefault = _objInterfazNaturalModel.GetRowByPk(user.CompanyId, ObjCustomerDefault.BranchId, ObjCustomerDefault.EntityId);
                ObjLegalDefault = _objInterfazLegalModel.GetRowByPk(user.CompanyId, ObjCustomerDefault.BranchId, ObjCustomerDefault.EntityId);

                //Procesar Datos
                if (ObjTransactionMasterDetail.Count > 0)
                {
                    foreach (var masterDetailDto in ObjTransactionMasterDetail)
                    {
                        ObjTransactionMasterDetailCredit = _objInterfazTransactionMasterDetailCreditModel.GetRowByPk(masterDetailDto.TransactionMasterDetailId);
                    }
                }


                //Obtener la linea de credito del cliente por defecto
                ObjCurrencyDolares = _objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyId);
                ObjCurrencyCordoba = _objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyId);
                ParameterCausalTypeCredit = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_CREDIT", user.CompanyId);
                ObjCustomerCreditAmoritizationAll = _objInterfazCustomerCreditAmortizationModel.GetRowByCustomerId(ObjCustomerDefault.EntityId);
                ObjListCustomerCreditLine = _objInterfazCustomerCreditLineModel.GetRowByEntityBalanceMayorCero(user.CompanyId, user.BranchId, this.ObjCustomerDefault.EntityId);

                //Obtener los datos de precio, sku y conceptos de la transaccoin
                ObjTransactionMasterItemPrice = _objInterfazPriceModel.GetRowByTransactionMasterId(user.CompanyId, ObjListPrice.ListPriceId, ObjTransactionMaster.TransactionMasterId);
                ObjTransactionMasterItemConcepto = _objInterfazCompanyComponentConceptModel.GetRowByTransactionMasterId(user.CompanyId, ObjComponentItem.ComponentId, ObjTransactionMaster.TransactionMasterId);
                ObjTransactionMasterItemSku = _objInterfazItemSkuModel.GetRowByTransactionMasterId(user.CompanyId, ObjTransactionMaster.TransactionMasterId);
                ObjTransactionMasterItem = _objInterfazItemModel.GetRowByTransactionMasterId(ObjTransactionMaster.TransactionMasterId);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Se produjo el siguiente error: {ex.Message}");
            }
        }


        public void LoadNew()
        {
            try
            {
                var userNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];
                var notAccessControl = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_CONTROL"];
                var notAllInsert = VariablesGlobales.ConfigurationBuilder["NOT_ALL_INSERT"];
                var permissionNone = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_NONE"]);
                var appNeedAuthentication = VariablesGlobales.ConfigurationBuilder["APP_NEED_AUTHENTICATION"];
                var urlSuffix = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX"];
                var user = VariablesGlobales.Instance.User;
                if (user is null)
                {
                    throw new Exception(userNotAutenticated);
                }

                var role = VariablesGlobales.Instance.Role;
                if (appNeedAuthentication == "true")
                {
                    var permited = _objInterfazCoreWebPermission.UrlPermited("app_invoice_billing", "index", urlSuffix!,
                        VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                        VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                        VariablesGlobales.Instance.ListMenuHiddenPopup);
                    if (!permited)
                    {
                        throw new Exception(notAccessControl);
                    }

                    var resultPermission = _objInterfazCoreWebPermission.UrlPermissionCmd("app_invoice_billing",
                        "add", urlSuffix!, role, user,
                        VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                        VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                        VariablesGlobales.Instance.ListMenuHiddenPopup);
                    if (resultPermission == permissionNone)
                    {
                        throw new Exception(notAllInsert);
                    }
                }

                ObjComponentCustomer = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_customer");
                if (ObjComponentCustomer is null)
                {
                    throw new Exception("EL COMPONENTE 'tb_customer' NO EXISTE...");
                }

                this.ObjComponentItem = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_item");
                if (ObjComponentItem is null)
                {
                    throw new Exception("EL COMPONENTE 'tb_item' NO EXISTE...");
                }

                this.ObjComponentTransactionBilling =
                    _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_transaction_master_billing");
                if (ObjComponentTransactionBilling is null)
                {
                    throw new Exception("EL COMPONENTE 'tb_transaction_master_billing' NO EXISTE...");
                }

                this.TransactionId =
                    _objInterfazCoreWebTransaction.GetTransactionId(user.CompanyId, "tb_transaction_master_billing", 0)!.Value;
                ObjCurrency = _objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyId);
                ObjCurrencyDolares = _objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyId);
                var customerDefault = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_CLIENTDEFAULT", user.CompanyId);
                ObjListPrice = _objInterfazListPriceModel.GetListPriceToApply(user.CompanyId);
                ObjListCurrency = _objInterfazCompanyCurrencyModel.GetByCompany(user.CompanyId);
                if (ObjListPrice is null)
                {
                    throw new Exception("NO EXISTE UNA LISTA DE PRECIO PARA SER APLICADA");
                }

                ObjListParameterAll = _objInterfazCoreWebParameter.GetParameterAll(user.CompanyId);
                ObjParameterInvoiceAutoApply = _objInterfazCoreWebParameter.GetParameter("INVOICE_AUTOAPPLY_CASH", user.CompanyId)!.Value;
                ObjParameterTypePreiceDefault = _objInterfazCoreWebParameter.GetParameter("INVOICE_DEFAULT_TYPE_PRICE", user.CompanyId)!.Value;
                ObjParameterTipoWarehouseDespacho = _objInterfazCoreWebParameter.GetParameter("INVOICE_TYPE_WAREHOUSE_DESPACHO", user.CompanyId)!.Value;
                ObjParameterImprimirPorCadaFactura = _objInterfazCoreWebParameter.GetParameter("INVOICE_PRINT_BY_INVOICE", user.CompanyId)!.Value;
                ObjParameterScanerProducto = _objInterfazCoreWebParameter.GetParameter("INVOICE_SHOW_POPUP_FIND_PRODUCTO_NOT_SCANER", user.CompanyId)!.Value;
                ObjParameterCantidadItemPoup = _objInterfazCoreWebParameter.GetParameter("INVOICE_CANTIDAD_ITEM", user.CompanyId)!.Value;
                ObjParameterHidenFiledItemNumber = _objInterfazCoreWebParameter.GetParameter("INVOICE_HIDEN_ITEMNUMBER_IN_POPUP", user.CompanyId)!.Value;
                ObjParameterAmortizationDuranteFactura = _objInterfazCoreWebParameter.GetParameter("INVOICE_PARAMTER_AMORITZATION_DURAN_INVOICE", user.CompanyId)!.Value;
                ObjParameterAlturaDelModalDeSeleccionProducto = _objInterfazCoreWebParameter.GetParameter("INVOICE_ALTO_MODAL_DE_SELECCION_DE_PRODUCTO_AL_FACTURAR", user.CompanyId)!.Value;
                ObjParameterScrollDelModalDeSeleccionProducto = _objInterfazCoreWebParameter.GetParameter("INVOICE_SCROLL_DE_MODAL_EN_SELECCION_DE_PRODUTO_AL_FACTURAR", user.CompanyId)!.Value;
                //Obtener la lista de estados
                if (ObjParameterInvoiceAutoApply == "true")
                {
                    ObjListWorkflowStage = _objInterfazCoreWebWorkflow.GetWorkflowStageApplyFirst(
                        "tb_transaction_master_billing", "statusID", user.CompanyId, user.BranchId, role!.RoleId);
                }
                else
                {
                    ObjListWorkflowStage = _objInterfazCoreWebWorkflow.GetWorkflowInitStage("tb_transaction_master_billing", "statusID", user.CompanyId, user.BranchId, role.RoleId);
                }

                ExchangeRate = _objInterfazCoreWebCurrency.GetRatio(user.CompanyId, DateOnly.FromDateTime(DateTime.Now), decimal.One, ObjCurrencyDolares!.CurrencyId, ObjCurrency!.CurrencyId);
                ObjListEmployee = _objInterfazEmployeeModel.GetRowByBranchIdAndType(user.CompanyId, user.BranchId, Convert.ToInt32(ObjListParameterAll["INVOICE_TYPE_EMPLOYEER"]));
                ObjListBank = _objInterfazBankModel.GetByCompany(user.CompanyId);
                ObjCausal = _objInterfazTransactionCausalModel.GetCausalByBranch(user.CompanyId, TransactionId.Value, user.BranchId);
                WarehouseId = ObjCausal.First()!.WarehouseSourceId;
                ObjListWarehouse = _objInterfazUserWarehouseModel.GetRowByUserIdAndFacturable(user.CompanyId, user.UserId);
                ObjCustomerDefault = _objInterfazCustomerModel.GetRowByCode(user.CompanyId, customerDefault!.Value);
                ObjListTypePrice = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_price", "typePriceID", user.CompanyId);
                ObjListZone = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_transaction_master_info_billing", "zoneID", user.CompanyId);
                ObjListMesa = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_transaction_master_info_billing", "mesaID", user.CompanyId);
                ObjListPay = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer_credit_line", "periodPay", user.CompanyId);
                ListProvider = _objInterfazProviderModel.GetRowByCompany(user.CompanyId);
                ObjParameterCxcPlazoDefault = _objInterfazCoreWebParameter.GetParameterValue("CXC_PLAZO_DEFAULT", user.CompanyId);
                ObjParameterCxcFrecuenciaPayDefault = _objInterfazCoreWebParameter.GetParameterValue("CXC_FRECUENCIA_PAY_DEFAULT", user.CompanyId);
                ObjParameterCustomPopupFacturacion = _objInterfazCoreWebParameter.GetParameterValue("CORE_VIEW_CUSTOM_PANTALLA_DE_FACTURACION_POPUP_SELECCION_PRODUCTO_FORMA_MOSTRAR", user.CompanyId);
                ObjParameterInvoiceBillingApplyTypePriceOnDayPorMayor = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_APPLY_TYPE_PRICE_ON_DAY_POR_MAYOR", user.CompanyId);
                ObjParameterInvoiceBillingShowCommandBar = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_SHOW_COMMAND_BAR", user.CompanyId);
                ObjParameterInvoiceBillingPrinterDirectNameDefaultBar = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_PRINTER_DIRECT_NAME_DEFAULT_BAR", user.CompanyId);
                ObjParameterInvoiceBillingPrinterDirectUrlBar = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_PRINTER_DIRECT_URL_BAR", user.CompanyId);
                ObjParameterobjParameterInvoiceBillingPrinterUrlBar = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_PRINTER_URL_BAR", user.CompanyId);
                ObjParameterInvoiceBillingSelectitem = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_SELECTITEM", user.CompanyId);
                ObjParameterInvoiceBillingQuantityZero = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_QUANTITY_ZERO", user.CompanyId)!.Value;
                ObjParameterRegresarAListaDespuesDeGuardar = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_SAVE_AFTER_TO_LIST", user.CompanyId)!.Value;
                ObjParameterMostrarImagenEnSeleccion = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_SHOW_IMAGE_IN_DETAIL_SELECTION", user.CompanyId)!.Value;
                ObjParameterPantallaParaFacturar = _objInterfazCoreWebParameter.GetParameter("INVOICE_PANTALLA_FACTURACION", user.CompanyId)!.Value;

                if (ObjCustomerDefault is null)
                {
                    throw new Exception("NO EXISTE EL CLIENTE POR DEFECTO");
                }

                ObjNaturalDefault = _objInterfazNaturalModel.GetRowByPk(user.CompanyId, ObjCustomerDefault.BranchId, ObjCustomerDefault.EntityId);
                ObjLegalDefault = _objInterfazLegalModel.GetRowByPk(user.CompanyId, ObjCustomerDefault.BranchId, ObjCustomerDefault.EntityId);
                ObjEmployeeNatural = _objInterfazNaturalModel.GetRowByPk(user.CompanyId, user.BranchId, user.EmployeeId);

                //Obtener la linea de credito del cliente por defecto
                ObjCurrencyDolares = _objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyId);
                ObjCurrencyCordoba = _objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyId);
                ParameterCausalTypeCredit = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_CREDIT", user.CompanyId);
                ObjCustomerCreditAmoritizationAll = _objInterfazCustomerCreditAmortizationModel.GetRowByCustomerId(ObjCustomerDefault.EntityId);
                ObjListCustomerCreditLine = _objInterfazCustomerCreditLineModel.GetRowByEntityBalanceMayorCero(user.CompanyId, user.BranchId, this.ObjCustomerDefault.EntityId);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Se produjo el siguiente error: {ex.Message}");
            }
        }


        public void SaveInsert()
        {
            try
            {
                var userNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];
                var notAccessControl = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_CONTROL"];
                var notAllInsert = VariablesGlobales.ConfigurationBuilder["NOT_ALL_INSERT"];
                var permissionNone = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_NONE"]);
                var appNeedAuthentication = VariablesGlobales.ConfigurationBuilder["APP_NEED_AUTHENTICATION"];
                var urlSuffix = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX"];
                var user = VariablesGlobales.Instance.User;
                if (user is null)
                {
                    throw new Exception(userNotAutenticated);
                }

                var role = VariablesGlobales.Instance.Role;
                if (appNeedAuthentication == "true")
                {
                    var permited = _objInterfazCoreWebPermission.UrlPermited("app_invoice_billing", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                    if (!permited)
                    {
                        throw new Exception(notAccessControl);
                    }

                    var resultPermission = _objInterfazCoreWebPermission.UrlPermissionCmd("app_invoice_billing", "add", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                    if (resultPermission == permissionNone)
                    {
                        throw new Exception(notAllInsert);
                    }
                }

                //Obtener el Componente de Transacciones Facturacion
                var objComponentBilling = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_transaction_master_billing");
                if (objComponentBilling is null) throw new Exception("EL COMPONENTE 'tb_transaction_master_billing' NO EXISTE...");


                var objComponentItem = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_item");
                if (objComponentItem is null) throw new Exception("EL COMPONENTE 'tb_item' NO EXISTE...");

                //Obtener transaccion
                //txtCausalID")
                var causalId = Convert.ToInt32(txtCausalID.Text);
                TransactionId = _objInterfazCoreWebTransaction.GetTransactionId(user.CompanyId, "tb_transaction_master_billing", 0);
                var objT = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionModel>().GetByCompanyAndTransaction(user.CompanyId, TransactionId!.Value);
                var objTransactionCausal = _objInterfazTransactionCausalModel.GetByCompanyAndTransactionAndCausal(user.CompanyId, TransactionId!.Value, causalId);


                //Valores de tasa de cambio
                ObjCurrencyDolares = _objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyId);
                ObjCurrencyCordoba = _objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyId);
                ExchangeRate = _objInterfazCoreWebCurrency.GetRatio(user.CompanyId, DateOnly.FromDateTime(DateTime.Now), 1, ObjCurrencyDolares!.CurrencyId, ObjCurrencyCordoba!.CurrencyId);
                var cycleIsCloseByDate = _objInterfazCoreWebAccounting.CycleIsCloseByDate(user.CompanyId, txtDate.DateTime);
                if (cycleIsCloseByDate) throw new Exception("EL DOCUMENTO NO PUEDE INGRESAR, EL CICLO CONTABLE ESTA CERRADO");
                _objInterfazCoreWebPermission.GetValueLicense(user.CompanyId, "app_invoice_billing/index");

                ObjParameterInvoiceBillingQuantityZero = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_QUANTITY_ZERO", user.CompanyId)!.Value;

                //obtener el primer estado  de la factura o el estado inicial.
                ObjListWorkflowStage = _objInterfazCoreWebWorkflow.GetWorkflowAllStage("tb_transaction_master_billing", "statusID", user.CompanyId, user.BranchId, role.RoleId);
                //Saber si se va autoaplicar
                ObjParameterInvoiceAutoApply = _objInterfazCoreWebParameter.GetParameter("INVOICE_AUTOAPPLY_CASH", user.CompanyId)!.Value;
                ObjParaemterStatusCanceled = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_CANCEL", user.CompanyId)!.Value;
                ObjParameterUrlPrinterDirect = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_PRINTER_DIRECT_URL", user.CompanyId)!.Value;
                ObjParameterImprimirPorCadaFactura = _objInterfazCoreWebParameter.GetParameter("INVOICE_PRINT_BY_INVOICE", user.CompanyId)!.Value;

                //Saber si es al credito
                ParameterCausalTypeCredit = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_CREDIT", user.CompanyId);
                var causalIdTypeCredit = ParameterCausalTypeCredit!.Value!.Split(',');
                //$exisCausalInCredit						= null;
                //$exisCausalInCredit						= array_search(txtCausalID"),$causalIDTypeCredit);
                // Buscar el valor en la matriz
                var exisCausalInCredit = Array.IndexOf(causalIdTypeCredit, txtCausalID.Text) > 0;
                //Si esta configurado como auto aplicado
                //y es al credito. cambiar el estado por el estado inicial, que es registrada
                int? statusId = ObjParameterInvoiceAutoApply switch
                {
                    "true" when exisCausalInCredit => ObjListWorkflowStage?[0].WorkflowStageId,
                    //si la factura es al contado, y esta como auto aplicada cambiar el estado
                    "true" when !exisCausalInCredit => Convert.ToInt32(ObjParaemterStatusCanceled),
                    _ => TxtStatusId
                };
                var currencyId = (txtCurrencyID.SelectedItem as TbCurrency)?.CurrencyId; //verificar valor
                var objTm = new TbTransactionMaster
                {
                    CompanyId = user.CompanyId,
                    TransactionId = TransactionId!.Value,
                    BranchId = user.BranchId,
                    TransactionNumber = _objInterfazCoreWebCounter.GoNextNumber(user.CompanyId, user.BranchId, "tb_transaction_master_proforma", 0),
                    TransactionCausalId = Convert.ToInt32(txtCausalID.Text),
                    EntityId = TxtCustomerId,
                    TransactionOn = txtDate.DateTime,
                    TransactionOn2 = txtDateFirst.DateTime,
                    StatusIdchangeOn = DateTime.Now,
                    ComponentId = objComponentBilling.ComponentId,
                    Note = txtNote.Text,
                    Sign = (short?)objT!.SignInventory,
                    CurrencyId = currencyId, //validar este campo
                    CurrencyId2 = _objInterfazCoreWebCurrency.GetTarget(user.CompanyId, currencyId!.Value),
                    Reference1 = txtReference1.Text,
                    DescriptionReference = "reference1:entityID del proveedor de credito para las facturas al credito,reference4: customerCreditLineID linea de credito del cliente",
                    Reference2 = txtReference2.Text,
                    Reference3 = txtReference3.Text,
                    Reference4 = string.IsNullOrEmpty(txtCustomerCreditLineID.Text) ? "0" : txtCustomerCreditLineID.Text,
                    StatusId = statusId,
                    Amount = decimal.Zero,
                    IsApplied = false,
                    JournalEntryId = 0,
                    ClassId = null,
                    AreaId = null,
                    SourceWarehouseId = Convert.ToInt32(txtWarehouseID.Text),
                    TargetWarehouseId = null,
                    IsActive = true,
                    PeriodPay = Convert.ToInt32(txtPeriodPay.Text),
                    NextVisit = txtNextVisit.DateTime,
                    NumberPhone = txtNumberPhone.Text,
                    EntityIdsecondary = Convert.ToInt32(txtEmployeeID.SelectedItem) //revisar el tipo de valores
                };
                objTm.ExchangeRate = _objInterfazCoreWebCurrency.GetRatio(user.CompanyId, DateOnly.FromDateTime(DateTime.Now), decimal.One, objTm.CurrencyId2!.Value, objTm.CurrencyId!.Value);
                VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebAuditoria>().SetAuditCreated(objTm, user, "");
                var objParameterAll = _objInterfazCoreWebParameter.GetParameterAll(user.CompanyId);
                var transactionMasterId = _objInterfazTransactionMasterModel.InsertAppPosme(objTm);

                var assembly = Assembly.GetEntryAssembly();
                var documentoPath = "";
                if (assembly is not null)
                {
                    //Crear la Carpeta para almacenar los Archivos del Documento
                    documentoPath = $"{assembly.Location}/company_{user.CompanyId}/component_{objComponentBilling.ComponentId}/component_item_{transactionMasterId}";
                }

                var objTmInfo = new TbTransactionMasterInfo
                {
                    CompanyId = objTm.CompanyId,
                    TransactionId = objTm.TransactionId,
                    TransactionMasterId = TransactionMasterId!.Value,
                    ZoneId = Convert.ToInt32(txtZoneID.SelectedItem), //Varificar valor
                    MesaId = Convert.ToInt32(txtMesaID.Text),
                    RouteId = 0,
                    ReferenceClientName = txtReferenceClientName.Text,
                    ReferenceClientIdentifier = txtReferenceClientIdentifier.Text,
                    ReceiptAmount = WebToolsHelper.ConvertToNumber<decimal>(txtReceiptAmount.Text),
                    ReceiptAmountDol = WebToolsHelper.ConvertToNumber<decimal>(txtReceiptAmountDol.Text),
                    ReceiptAmountPoint = WebToolsHelper.ConvertToNumber<decimal>(txtReceiptAmountPoint.Text),
                    ReceiptAmountBank = WebToolsHelper.ConvertToNumber<decimal>(txtReceiptAmountBank.Text),
                    ReceiptAmountBankDol = WebToolsHelper.ConvertToNumber<decimal>(txtReceiptAmountBankDol.Text),
                    ReceiptAmountCardDol = WebToolsHelper.ConvertToNumber<decimal>(txtReceiptAmountTarjetaDol.Text),
                    ReceiptAmountCard = WebToolsHelper.ConvertToNumber<decimal>(txtReceiptAmountTarjeta.Text),
                    ChangeAmount = WebToolsHelper.ConvertToNumber<decimal>(txtChangeAmount.Text),
                    ReceiptAmountBankReference = txtReceiptAmountBank_Reference.Text,
                    ReceiptAmountBankDolReference = txtReceiptAmountBankDol_Reference.Text,
                    ReceiptAmountCardBankReference = txtReceiptAmountTarjeta_Reference.Text,
                    ReceiptAmountCardBankDolReference = txtReceiptAmountTarjetaDol_Reference.Text,
                    ReceiptAmountBankId = WebToolsHelper.ConvertToNumber<int>(txtReceiptAmountBank_BankID.Text),
                    ReceiptAmountBankDolId = WebToolsHelper.ConvertToNumber<int>(txtReceiptAmountBankDol_BankID.Text),
                    ReceiptAmountCardBankId = WebToolsHelper.ConvertToNumber<int>(txtReceiptAmountTarjeta_BankID.Text),
                    ReceiptAmountCardBankDolId = WebToolsHelper.ConvertToNumber<int>(txtReceiptAmountTarjetaDol_BankID.Text),
                };

                objTmInfo.TransactionMasterInfoId = _objInterfazTransactionMasterInfoModel.InsertAppPosme(objTmInfo);


                //Ingresar la configuracion de precios		
                var amountTotal = decimal.Zero;
                var tax1Total = decimal.Zero;
                var subAmountTotal = decimal.Zero;

                //Tipo de precio seleccionado por el usuario,
                //Actualmente no se esta usando
                var typePriceId = txtTypePriceID.SelectedItem; //verificar valor
                var objListPrice = _objInterfazListPriceModel.GetListPriceToApply(user.CompanyId);
                //obtener la lista de precio por defecto
                var objParameterPriceDefault = _objInterfazCoreWebParameter.GetParameter("INVOICE_DEFAULT_PRICELIST", user.CompanyId);
                var listPriceId = objParameterPriceDefault!.Value;
                //obener los tipos de precio de la lista de precio por defecto
                var objTipePrice = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_price", "typePriceID", user.CompanyId);

                //Parametro para validar si se cambian los precios en la facturacion
                var objParameterUpdatePrice = _objInterfazCoreWebParameter.GetParameter("INVOICE_UPDATEPRICE_ONLINE", user.CompanyId);
                var objUpdatePrice = objParameterUpdatePrice!.Value;

                var rowCount = gridViewValues.RowCount;
                for (var i = 0; i < rowCount; i++)
                {
                    //Recorrer la lista del detalle del documento
                    var itemNameDetail = gridViewValues.GetRowCellValue(i, colTransactionDetailName.Name).ToString();
                    var quantity = WebToolsHelper.ConvertToNumber<decimal>(gridViewValues.GetRowCellValue(i, colCantidad.Name).ToString());
                    var listPrice = Convert.ToDecimal(gridViewValues.GetRowCellValue(i, colPrecio.Name));
                    var listLote = (string?)gridViewValues.GetRowCellValue(i, colDescripcion.Name);
                    var listVencimiento = gridViewValues.GetRowCellValue(i, colDetailVencimiento.Name).ToString();
                    var skuCatalogItemId = gridViewValues.GetRowCellValue(i, colSkuQuantityBySku.Name);
                    var skuFormatoDescription = gridViewValues.GetRowCellValue(i, colSkuFormatoDescripton.Name).ToString();

                    var itemId = (int)gridViewValues.GetRowCellValue(i, colCodigo.Name);
                    var lote = string.IsNullOrEmpty(listLote) ? "" : listLote;
                    var vencimiento = string.IsNullOrWhiteSpace(listVencimiento) ? "" : listVencimiento;
                    var warehouseId = objTm.SourceWarehouseId;
                    var objItem = _objInterfazItemModel.GetRowByPk(user.CompanyId, itemId);
                    var objItemWarehouse = VariablesGlobales.Instance.UnityContainer.Resolve<IItemWarehouseModel>().GetByPk(user.CompanyId, itemId, warehouseId.Value);
                    var objPrice = _objInterfazPriceModel.GetRowByPk(user.CompanyId, objListPrice!.ListPriceId, itemId, (int)typePriceId);
                    var objCompanyComponentConcept = _objInterfazCompanyComponentConceptModel.GetRowByPk(user.CompanyId, objComponentItem.ComponentId, itemId, "IVA");
                    itemNameDetail = itemNameDetail!.Replace("'", "");
                    ;
                    var objItemSku = _objInterfazItemSkuModel.GetByPk(itemId, (int)skuCatalogItemId);

                    //price								= objItem->cost * ( 1 + (objPrice->percentage/100));
                    var price = listPrice / objItemSku.Value;
                    var ivaPercentage = (objCompanyComponentConcept is not null) ? objCompanyComponentConcept.ValueOut : decimal.Zero;
                    var unitaryAmount = price * (1 + ivaPercentage);
                    var tax1 = price * ivaPercentage;

                    //Actualisar nombre 
                    if (objParameterAll["INVOICE_UPDATENAME_IN_TRANSACTION_ONLY"] == "false")
                    {
                        var objItemNew = _objInterfazItemModel.GetRowByPk(user.CompanyId, itemId);
                        objItemNew.Name = itemNameDetail;
                        _objInterfazItemModel.UpdateAppPosme(user.CompanyId, itemId, objItemNew);
                    }


                    if (objItemWarehouse.Quantity < quantity && objItem.IsInvoiceQuantityZero == 0 && ObjParameterInvoiceBillingQuantityZero == "false")
                    {
                        throw new Exception($"La cantidad de '{objItem.ItemNumber} {objItem.Name}' es mayor que la disponible en bodega");
                    }


                    var objTmd = new TbTransactionMasterDetail
                    {
                        CompanyId = objTm.CompanyId,
                        TransactionId = objTm.TransactionId,
                        TransactionMasterId = transactionMasterId,
                        ComponentId = objComponentItem.ComponentId,
                        ComponentItemId = itemId,
                        Quantity = quantity * objItemSku.Value,
                        SkuQuantity = quantity,
                        SkuQuantityBySku = objItemSku.Value,
                        UnitaryCost = objItem.Cost,
                        UnitaryPrice = price,
                        Tax1 = tax1,
                        Discount = 0,
                        PromotionId = 0,
                        Reference1 = lote,
                        Reference2 = vencimiento,
                        Reference3 = "0", // Asumiendo que Reference3 es una cadena
                        ItemNameLog = itemNameDetail,
                        CatalogStatusId = 0,
                        InventoryStatusId = 0,
                        IsActive = true,
                        QuantityStock = 0,
                        QuantiryStockInTraffic = 0,
                        QuantityStockUnaswared = 0,
                        RemaingStock = 0,
                        ExpirationDate = null,
                        InventoryWarehouseSourceId = (int)objTm.SourceWarehouseId,
                        InventoryWarehouseTargetId = (int)objTm.TargetWarehouseId!,
                        SkuCatalogItemId = (int)skuCatalogItemId,
                        SkuFormatoDescription = skuFormatoDescription
                    };
                    objTmd.Cost = objTmd.Quantity * objItem.Cost;
                    objTmd.Amount = objTmd.Quantity * unitaryAmount;
                    objTmd.UnitaryAmount = unitaryAmount;


                    tax1Total = decimal.Add(tax1Total, tax1!.Value);
                    subAmountTotal = subAmountTotal + (quantity * price);
                    amountTotal = decimal.Add(amountTotal, (decimal)objTmd.Amount!);

                    var transactionMasterDetailId = _objInterfazTransactionMasterDetailModel.InsertAppPosme(objTmd);

                    var objTmdc = new TbTransactionMasterDetailCredit();
                    objTmdc.TransactionMasterId = transactionMasterId;
                    objTmdc.TransactionMasterDetailId = transactionMasterDetailId;
                    objTmdc.Reference1 = txtFixedExpenses.Text;
                    objTmdc.Reference2 = txtReportSinRiesgo.IsOn.ToString();
                    objTmdc.Reference3 = "txtLayFirstLineProtocolo";
                    objTmdc.Reference4 = "";
                    objTmdc.Reference5 = "";
                    objTmdc.Reference9 = "reference1: Porcentaje de Gastos Fijo para las facturas de credito,reference2: Escritura Publica,reference3: Primer Linea del Protocolo";
                    _objInterfazTransactionMasterDetailCreditModel.InsertAppPosme(objTmdc);
                    //Actualizar tipo de precio
                    if (objUpdatePrice == "true")
                    {
                        var dataUpdatePrice = _objInterfazPriceModel.GetRowByPk(user.CompanyId, Convert.ToInt32(listPriceId), itemId, (int)typePriceId);
                        if (dataUpdatePrice is not null)
                        {
                            dataUpdatePrice.Price = price;
                            dataUpdatePrice.Percentage = objItem.Cost == 0 ? (price / 100) : (((100 * price) / objItem.Cost) - 100);
                            _objInterfazPriceModel.UpdateAppPosme(user.CompanyId, Convert.ToInt32(listPriceId), itemId, (int)typePriceId, dataUpdatePrice!);
                        }
                    }
                }

                //Actualizar Transaccion
                objTm.Amount = amountTotal;
                objTm.Tax1 = tax1Total;
                objTm.SubAmount = subAmountTotal;
                _objInterfazTransactionMasterModel.UpdateAppPosme(user.CompanyId, TransactionId!.Value, transactionMasterId, objTm);

                //Aplicar el Documento?
                //Las factuas de credito no se auto aplican auque este el parametro, por que hay que crer el documento
                //y esto debe ser revisado cuidadosamente
                var commandAplicable = VariablesGlobales.ConfigurationBuilder["COMMAND_APLICABLE"];
                var validateWorkflowStage = _objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_billing", "statusID", (int)objTm.StatusId!, Convert.ToInt32(commandAplicable), user.CompanyId, user.BranchId, role.RoleId);
                if ( validateWorkflowStage!.Value){
                    //Ingresar en Kardex.
                    VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebInventory>().CalculateKardexNewOutput(user.CompanyId, TransactionId!.Value, transactionMasterId);

                    //Crear Conceptos.
                    VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebConcept>().Billing(user.CompanyId, TransactionId!.Value, transactionMasterId);
                }


                switch (ObjParameterInvoiceAutoApply)
                {
                    //No auto aplicar
                    //Si auto aplicar
                    case "false":
                        return;
                    case "true":
                    {
                        //si es auto aplicadao mandar a imprimir
                        if (ObjParameterInvoiceAutoApply == "true" && ObjParameterImprimirPorCadaFactura == "true")
                        {
                        }

                        /*$this->core_web_notification->set_message(false, SUCCESS);
                            $this->response->redirect(base_url()."/".'app_invoice_billing/add');*/
                        break;
                    }
                    //Error 
                    /*default:
                        $ db->transRollback();
                            $errorCode = $db->error()["code"];
                            $errorMessage = $db->error()["message"];
                            $this->core_web_notification->set_message(true,  $errorCode." ".$errorMessage );
                        $this->response->redirect(base_url()."/".'app_invoice_billing/add');
                        break;*/
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", $"Se produjo el siguiente error: {e.Message}", this);
            }
        }

        public string? ObjParameterUrlPrinterDirect { get; set; }

        public string? ObjParaemterStatusCanceled { get; set; }

        public void SaveUpdate()
        {
            throw new NotImplementedException();
        }


        public void PreRender()
        {
            var imagenInvoice = VariablesGlobales.ConfigurationBuilder["PATH_IMAGE_IN_INVOICE_POSME"];
            if (imagenInvoice is not null)
            {
                if (File.Exists(imagenInvoice))
                {
                    pictureEdit2.Image = Image.FromFile(imagenInvoice);
                }
            }

            var imageCustomer = VariablesGlobales.ConfigurationBuilder["PATH_IMAGE_IN_INVOICE_CUSTOMER"];
            if (imageCustomer is not null)
            {
                if (File.Exists(imageCustomer))
                {
                    pictureEdit1.Image = Image.FromFile(imageCustomer);
                }
            }
        }

        #endregion
    }
}