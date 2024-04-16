using System.IO;
using System.Net;
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

        private int CompanyId { get; set; }
        private int TransactionId { get; set; }
        private int TransactionMasterId { get; set; }
        private TypeOpenForm TypeOpen { get; set; }
        private int TxtCustomerId { get; set; }
        private int TxtStatusOldId { get; set; }
        private int TxtStatusId { get; set; }

        #endregion

        #region Init

        public FormInvoiceBillingEdit(TypeOpenForm typeOpen, int companyId, int transactionId, int transactionMasterId, List<TbTransactionMasterDetailDto> objTransactionMasterDetailWarehouse)
        {
            InitializeComponent();

            // Suscribir al manejador de excepciones global
            Application.ThreadException +=
                new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            CompanyId = companyId;
            TransactionId = transactionId;
            TransactionMasterId = transactionMasterId;
            this.ObjTransactionMasterDetailWarehouse = objTransactionMasterDetailWarehouse;
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


        private void EventoCallBackAceptar(dynamic mensaje)
        {
            // Realizar la lógica que desees en el formulario padre
            WebToolsHelper objWebToolsHelper = new WebToolsHelper();
            MessageBox.Show("Evento en el formulario hijo: " +
                            objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "itemID", "0"));
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
                var objTm = _objInterfazTransactionMasterModel.GetRowByPk(CompanyId, TransactionId, TransactionMasterId);
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
                var targetCurrency = _objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyId);
                var customerDefault = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_CLIENTDEFAULT", user.CompanyId);
                ObjListPrice = _objInterfazListPriceModel.GetListPriceToApply(user.CompanyId);
                ObjListCurrency = _objInterfazCompanyCurrencyModel.GetByCompany(user.CompanyId);
                if (ObjListPrice is null)
                {
                    throw new Exception("NO EXISTE UNA LISTA DE PRECIO PARA SER APLICADA");
                }

                var objParameterAll = _objInterfazCoreWebParameter.GetParameterAll(user.CompanyId);
                var parameterValue = _objInterfazCoreWebParameter.GetParameter("INVOICE_BUTTOM_PRINTER_FIDLOCAL_PAYMENT_AND_AMORTIZACION", user.CompanyId);
                ObjParameterInvoiceButtomPrinterFidLocalPaymentAndAmortization = parameterValue.Value;

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
                ObjTransactionMaster = _objInterfazTransactionMasterModel.GetRowByPk(user.CompanyId, TransactionId, TransactionMasterId);
                ObjTransactionMasterInfo = _objInterfazTransactionMasterInfoModel.GetRowByPk(user.CompanyId, TransactionId, TransactionMasterId);
                ObjTransactionMasterDetail = _objInterfazTransactionMasterDetailModel.GetRowByTransaction(user.CompanyId, TransactionId, TransactionMasterId);
                ObjTransactionMasterDetailWarehouse = _objInterfazTransactionMasterDetailModel.GetRowByTransactionAndWarehouse(user.CompanyId, TransactionId, TransactionMasterId);
                ObjTransactionMasterDetailConcept = _objInterfazTransactionMasterConceptModel.GetRowByTransactionMasterConcept(user.CompanyId, TransactionId, TransactionMasterId, ObjComponentItem.ComponentId);

                //ObjTransactionMasterDetailCredit= null;	
                ObjListEmployee = _objInterfazEmployeeModel.GetRowByBranchIdAndType(user.CompanyId, user.BranchId, Convert.ToInt32(objParameterAll["INVOICE_TYPE_EMPLOYEER"]));
                ObjListBank = _objInterfazBankModel.GetByCompany(user.CompanyId);
                ObjCausal = _objInterfazTransactionCausalModel.GetCausalByBranch(user.CompanyId, TransactionId, user.BranchId);
                WarehouseId = ObjCausal.First()!.WarehouseSourceId;
                ObjListWarehouse = _objInterfazUserWarehouseModel.GetRowByUserIdAndFacturable(user.CompanyId, user.UserId);
                ObjCustomerDefault = _objInterfazCustomerModel.GetRowByCode(user.CompanyId, customerDefault!.Value);
                ObjListTypePrice = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_price", "typePriceID", user.CompanyId);
                ObjListZone = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_transaction_master_info_billing", "zoneID", user.CompanyId);
                ObjListMesa = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_transaction_master_info_billing", "mesaID", user.CompanyId);
                ObjListPay = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer_credit_line", "periodPay", user.CompanyId);
                ListProvider = _objInterfazProviderModel.GetRowByCompany(user.CompanyId);

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
                        masterDetailDto.ItemName = HtmlEncodeWithQuotes(masterDetailDto.ItemName!);
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
        
        private static string HtmlEncodeWithQuotes(string value)
        {
            // Codifica el valor HTML
            string encodedValue = WebUtility.HtmlEncode(value);
            // Reemplaza las comillas dobles y simples codificadas por sus entidades HTML
            encodedValue = encodedValue.Replace("'", "&#39;").Replace("\"", "&quot;");
            return encodedValue;
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
                        "index", urlSuffix!, role, user,
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

                var transactionId =
                    _objInterfazCoreWebTransaction.GetTransactionId(user.CompanyId, "tb_transaction_master_billing", 0);
                ObjCurrency = _objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyId);
                var targetCurrency = _objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyId);
                var customerDefault =
                    _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_CLIENTDEFAULT", user.CompanyId);
                ObjListPrice = _objInterfazListPriceModel.GetListPriceToApply(user.CompanyId);
                ObjListCurrency = _objInterfazCompanyCurrencyModel.GetByCompany(user.CompanyId);
                if (ObjListPrice is null)
                {
                    throw new Exception("NO EXISTE UNA LISTA DE PRECIO PARA SER APLICADA");
                }

                var objParameterAll = _objInterfazCoreWebParameter.GetParameterAll(user.CompanyId);
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
                        "tb_transaction_master_billing", "statusID", user.CompanyId, user.BranchId, role.RoleId);
                }
                else
                {
                    ObjListWorkflowStage = _objInterfazCoreWebWorkflow.GetWorkflowInitStage("tb_transaction_master_billing", "statusID", user.CompanyId, user.BranchId, role.RoleId);
                }

                ExchangeRate = _objInterfazCoreWebCurrency.GetRatio(user.CompanyId, DateOnly.FromDateTime(DateTime.Now), decimal.One, targetCurrency.CurrencyId, ObjCurrency.CurrencyId);

                ObjListEmployee = _objInterfazEmployeeModel.GetRowByBranchIdAndType(user.CompanyId, user.BranchId, Convert.ToInt32(objParameterAll["INVOICE_TYPE_EMPLOYEER"]));
                ObjListBank = _objInterfazBankModel.GetByCompany(user.CompanyId);
                ObjCausal = _objInterfazTransactionCausalModel.GetCausalByBranch(user.CompanyId, transactionId!.Value, user.BranchId);
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

                ObjListParameterAll = _objInterfazCoreWebParameter.GetParameterAll(user.CompanyId);
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
            throw new NotImplementedException();
        }

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