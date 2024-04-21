using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraVerticalGrid;
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

        public TbNaturale? ObjEmployeeNatural { get; private set; }

        public TbLegal? ObjLegalDefault { get; private set; }

        public TbNaturale? ObjNaturalDefault { get; private set; }

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

        public string? ObjParameterUrlPrinterDirect { get; set; }

        public string? ObjParaemterStatusCanceled { get; set; }

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
        public DataTable? ObjSELECCIONAR_ITEM_BILLING_BACKGROUND { get; set; }

        #endregion


        #region Variables internas

        private int? CompanyId { get; set; }
        private int? TransactionId { get; set; }
        private int? TransactionMasterId { get; set; }
        private TypeOpenForm TypeOpen { get; set; }
        private int TxtCustomerId { get; set; }
        private int? TxtStatusOldId { get; set; }
        private int? TxtStatusId { get; set; }

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
                LoadRender(TypeRender.Edit);
            }

            if (TypeOpen == TypeOpenForm.Init && TransactionMasterId == 0)
            {
                LoadNew();
                LoadRender(TypeRender.New);
            }
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
            try
            {
                var user = VariablesGlobales.Instance.User;
                if (user is null)
                {
                    throw new Exception("Usuario no logeado");
                }

                var transactionID = TransactionId.Value;
                var transactionMasterID = TransactionMasterId.Value;
                var companyID = user.CompanyId;

                //Get Component
                var objComponent = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_company");
                var objParameter = _objInterfazCoreWebParameter.GetParameter("CORE_COMPANY_LOGO", companyID);
                var objParameterTelefono = _objInterfazCoreWebParameter.GetParameter("CORE_PHONE", companyID);
                var objParameterRuc = _objInterfazCoreWebParameter.GetParameter("CORE_COMPANY_IDENTIFIER", companyID)!.Value;
                var objCompany = VariablesGlobales.Instance.Company;
                var spacing = 0.5;

                // Obtener datos del documento
                var objTm = _objInterfazTransactionMasterModel.GetRowByPk(companyID, transactionID, transactionMasterID) ?? throw new ArgumentNullException("_objInterfazTransactionMasterModel.GetRowByPk(companyID, transactionID, transactionMasterID)");
                var objTmi = _objInterfazTransactionMasterInfoModel.GetRowByPk(companyID, transactionID, transactionMasterID);
                var objTmd = _objInterfazTransactionMasterDetailModel.GetRowByTransaction(companyID, transactionID, transactionMasterID);
                var objTc = _objInterfazTransactionCausalModel.GetByCompanyAndTransactionAndCausal(companyID, transactionID, objTm.TransactionCausalId!.Value);

                // Formatear la fecha de la transacción
                var transactionDate = objTm.TransactionOn;
                string formattedTransactionDate = transactionDate!.Value.ToString("yyyy-M-d");

                // Obtener el identificador de la empresa
                var identifier = _objInterfazCoreWebParameter.GetParameter("CORE_COMPANY_IDENTIFIER", companyID);

                // Obtener información de la sucursal
                var objBranch = VariablesGlobales.Instance.UnityContainer.Resolve<IBranchModel>().GetRowByPk(objTm.CompanyId, objTm.BranchId!.Value);

                // Obtener información de la etapa de flujo de trabajo
                var objStage = _objInterfazCoreWebWorkflow.GetWorkflowStage("tb_transaction_master_billing", "statusID", objTm.StatusId!.Value, objTm.CompanyId, objTm.BranchId.Value, Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["APP_ROL_SUPERADMIN"]));

                // Obtener el tipo de transacción
                var objTipo = _objInterfazTransactionCausalModel.GetByCompanyAndTransactionAndCausal(companyID, objTm.TransactionId, objTm.TransactionCausalId!.Value);

                // Obtener información del cliente
                var objCustomer = _objInterfazCustomerModel.GetRowByEntity(companyID, objTm.EntityId!.Value);

                // Obtener información de la moneda
                var objCurrency = VariablesGlobales.Instance.UnityContainer.Resolve<ICurrencyModel>().GetRowByPk(objTm.CurrencyId!.Value);

                // Obtener información del cliente natural
                var objNatural = VariablesGlobales.Instance.UnityContainer.Resolve<INaturalModel>().GetRowByPk(companyID, objCustomer.BranchId, objCustomer.EntityId);

                // Calcular el tipo de cambio
                var exchangeSale = Convert.ToInt32(_objInterfazCoreWebParameter.GetParameter("ACCOUNTING_EXCHANGE_SALE", companyID)!.Value);
                var tipoCambio = Math.Round(objTm.ExchangeRate!.Value + exchangeSale, 2);

                // Obtener información del usuario que creó la transacción
                var objUserCreated = VariablesGlobales.Instance.UnityContainer.Resolve<IUserModel>().GetRowByPk(companyID, objTm.CreatedAt!.Value, objTm.CreatedBy!.Value);

                // Prefijo de la moneda
                var prefixCurrency = objCurrency.Simbol + " ";

                var confiDetalleHeader = new List<Dictionary<string, string>>();

                var row1 = new Dictionary<string, string>
                {
                    { "style", "text-align:left;width:auto" },
                    { "colspan", "1" },
                    { "prefix", "" },
                    { "style_row_data", "text-align:left;width:auto" },
                    { "colspan_row_data", "3" },
                    { "prefix_row_data", "" },
                    { "nueva_fila_row_data", "1" }
                };
                confiDetalleHeader.Add(row1);

                var row2 = new Dictionary<string, string>
                {
                    { "style", "text-align:left;width:50px" },
                    { "colspan", "1" },
                    { "prefix", "" },
                    { "style_row_data", "text-align:right;width:auto" },
                    { "colspan_row_data", "2" },
                    { "prefix_row_data", "" },
                    { "nueva_fila_row_data", "0" }
                };
                confiDetalleHeader.Add(row2);

                var row3 = new Dictionary<string, string>
                {
                    { "style", "text-align:right;width:90px" },
                    { "colspan", "1" },
                    { "prefix", "datView[\"objCurrency\"]->simbol" },
                    { "style_row_data", "text-align:right;width:90px" },
                    { "colspan_row_data", "1" },
                    { "prefix_row_data", "datView[\"objCurrency\"]->simbol" },
                    { "nueva_fila_row_data", "0" }
                };
                confiDetalleHeader.Add(row3);

                var detalle = new List<string[]> { (["PRODUCTO", "CANT", "TOTAL"]) };

                detalle.AddRange(objTmd.Select(detail => (string[])[detail.ItemName + " " + detail.SkuFormatoDescription, $"{Math.Round(detail.Quantity!.Value, 2):0.00}", $"{Math.Round(detail.Amount!.Value, 2):0.00}"]));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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


                //obtener el primer estado  de la factura o el estado inicial.
                ObjParameterInvoiceBillingQuantityZero = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_QUANTITY_ZERO", user.CompanyId)!.Value;
                ObjListWorkflowStage = _objInterfazCoreWebWorkflow.GetWorkflowAllStage("tb_transaction_master_billing", "statusID", user.CompanyId, user.BranchId, role!.RoleId);
                //Saber si se va autoaplicar
                ObjParameterInvoiceAutoApply = _objInterfazCoreWebParameter.GetParameter("INVOICE_AUTOAPPLY_CASH", user.CompanyId)!.Value;
                ObjParaemterStatusCanceled = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_CANCEL", user.CompanyId)!.Value;
                ObjParameterUrlPrinterDirect = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_PRINTER_DIRECT_URL", user.CompanyId)!.Value;
                ObjParameterImprimirPorCadaFactura = _objInterfazCoreWebParameter.GetParameter("INVOICE_PRINT_BY_INVOICE", user.CompanyId)!.Value;

                //Saber si es al credito
                ParameterCausalTypeCredit = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_CREDIT", user.CompanyId);
                var causalIdTypeCredit = ParameterCausalTypeCredit!.Value!.Split(',');
                // Buscar el valor en la matriz
                var exisCausalInCredit = Array.IndexOf(causalIdTypeCredit, txtCausalID.Text) > 0;
                //Si esta configurado como auto aplicado
                //y es al credito. cambiar el estado por el estado inicial, que es registrada
                int? statusId = 0;


                if (ObjParameterInvoiceAutoApply == "true" && exisCausalInCredit)
                {
                    statusId = ObjListWorkflowStage?[0].WorkflowStageId;
                }

                if (ObjParameterInvoiceAutoApply == "true" && exisCausalInCredit == false)
                {
                    statusId = Convert.ToInt32(ObjParaemterStatusCanceled);
                }
                else
                {
                    statusId = TxtStatusId;
                }


                var currencyId = (txtCurrencyID.SelectedItem as TbCurrency)?.CurrencyId;
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
                    EntityIdsecondary = Convert.ToInt32(txtEmployeeID.SelectedItem)
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
                    var itemNameDetail = gridViewValues.GetRowCellValue(i, colTransactionDetailName.Name).ToString()!.Replace("'", "");
                    var quantity = WebToolsHelper.ConvertToNumber<decimal>(gridViewValues.GetRowCellValue(i, colQuantity.Name).ToString());
                    var listPrice = Convert.ToDecimal(gridViewValues.GetRowCellValue(i, colPrice.Name));
                    var listLote = (string?)gridViewValues.GetRowCellValue(i, colTransactionDetailName.Name);
                    var listVencimiento = gridViewValues.GetRowCellValue(i, colDetailVencimiento.Name).ToString();
                    var skuCatalogItemId = gridViewValues.GetRowCellValue(i, colSkuQuantityBySku.Name);
                    var skuFormatoDescription = gridViewValues.GetRowCellValue(i, colSkuFormatoDescripton.Name).ToString();
                    var itemId = (int)gridViewValues.GetRowCellValue(i, colItemNumber.Name);


                    var lote = string.IsNullOrEmpty(listLote) ? "" : listLote;
                    var vencimiento = string.IsNullOrWhiteSpace(listVencimiento) ? "" : listVencimiento;
                    var warehouseId = objTm.SourceWarehouseId;
                    var objItem = _objInterfazItemModel.GetRowByPk(user.CompanyId, itemId);
                    var objItemWarehouse = VariablesGlobales.Instance.UnityContainer.Resolve<IItemWarehouseModel>().GetByPk(user.CompanyId, itemId, warehouseId.Value);
                    var objPrice = _objInterfazPriceModel.GetRowByPk(user.CompanyId, objListPrice!.ListPriceId, itemId, (int)typePriceId);
                    var objCompanyComponentConcept = _objInterfazCompanyComponentConceptModel.GetRowByPk(user.CompanyId, objComponentItem.ComponentId, itemId, "IVA");
                    var objItemSku = _objInterfazItemSkuModel.GetByPk(itemId, (int)skuCatalogItemId);


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
                        UnitaryAmount = unitaryAmount,
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
                    objTmd.Cost = objTmd.Quantity * objTmd.UnitaryCost;
                    objTmd.Amount = objTmd.Quantity * objTmd.UnitaryAmount;


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
                if (validateWorkflowStage!.Value)
                {
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
                                ComandPrinter();
                            }

                            break;
                        }
                        //Error 
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", $"Se produjo el siguiente error: {e.Message}", this);
            }
        }


        public void SaveUpdate()
        {
            try
            {
                var userNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];
                var notAccessControl = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_CONTROL"];
                var notAllInsert = VariablesGlobales.ConfigurationBuilder["NOT_ALL_INSERT"];
                var permissionNone = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_NONE"]);
                var appNeedAuthentication = VariablesGlobales.ConfigurationBuilder["APP_NEED_AUTHENTICATION"];
                var urlSuffix = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX"];
                var permissionMe = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_ME"]);
                var notEdit = VariablesGlobales.ConfigurationBuilder["NOT_EDIT"];
                var user = VariablesGlobales.Instance.User;
                if (user is null)
                {
                    throw new Exception(userNotAutenticated);
                }

                var role = VariablesGlobales.Instance.Role;
                if (role is null)
                {
                    throw new Exception("No hay roles asignados a este usuario");
                }

                var resultPermission = 0;
                if (appNeedAuthentication == "true")
                {
                    var permited = _objInterfazCoreWebPermission.UrlPermited("app_invoice_billing", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                    if (!permited)
                    {
                        throw new Exception(notAccessControl);
                    }

                    resultPermission = _objInterfazCoreWebPermission.UrlPermissionCmd("app_invoice_billing", "edit", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
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

                var objTm = _objInterfazTransactionMasterModel.GetRowByPk(user.CompanyId, TransactionId!.Value, TransactionMasterId!.Value);
                var oldStatusId = objTm.StatusId;
                ParameterCausalTypeCredit = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_CREDIT", user.CompanyId);

                //Valores de tasa de cambio
                ObjCurrencyDolares = _objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyId);
                ObjCurrencyCordoba = _objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyId);
                ExchangeRate = _objInterfazCoreWebCurrency.GetRatio(user.CompanyId, DateOnly.FromDateTime(DateTime.Now), 1, ObjCurrencyDolares!.CurrencyId, ObjCurrencyCordoba!.CurrencyId);

                //Validar Edicion por el Usuario
                if (resultPermission == permissionMe && objTm.CreatedBy != user.UserId) throw new Exception(notEdit);

                //Validar si el estado permite editar
                var notWorkflowEdit = VariablesGlobales.ConfigurationBuilder["NOT_WORKFLOW_EDIT"];
                var commandEditableTotal = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_EDITABLE_TOTAL"]);
                var commandEditable = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_EDITABLE"]);
                var validateWorkflowStage = _objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_billing", "statusID", objTm.StatusId!.Value, commandEditableTotal, user.CompanyId, user.BranchId, role.RoleId);
                if (validateWorkflowStage is null || !validateWorkflowStage.Value)
                {
                    throw new Exception(notWorkflowEdit);
                }

                if (_objInterfazCoreWebAccounting.CycleIsCloseByDate(user.CompanyId, objTm.TransactionOn!.Value))
                {
                    throw new Exception("EL DOCUMENTO NO PUEDE ACTUALIZARCE, EL CICLO CONTABLE ESTA CERRADO");
                }

                var objParameterAll = _objInterfazCoreWebParameter.GetParameterAll(user.CompanyId);
                ObjParameterInvoiceBillingQuantityZero = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_QUANTITY_ZERO", user.CompanyId)!.Value;
                ObjParameterImprimirPorCadaFactura = _objInterfazCoreWebParameter.GetParameter("INVOICE_PRINT_BY_INVOICE", user.CompanyId)!.Value;
                var objParameterRegrearANuevo = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_SAVE_AFTER_TO_ADD", user.CompanyId)!.Value;

                //Actualizar Maestro
                var typePriceId = txtTypePriceID.SelectedItem ?? throw new ArgumentNullException("txtTypePriceID.SelectedItem");
                var objListPrice = _objInterfazListPriceModel.GetListPriceToApply(user.CompanyId);
                var objTmNew = new TbTransactionMaster
                {
                    TransactionCausalId = Convert.ToInt32(txtCausalID.Text),
                    EntityId = TxtCustomerId,
                    TransactionOn = DateTime.Now.Date, // Fecha actual sin la parte de la hora
                    TransactionOn2 = txtDateFirst.DateTime, // Asignar el valor directamente
                    StatusIdchangeOn = DateTime.Now, // Fecha y hora actual
                    Note = txtNote.Text,
                    Reference1 = txtReference1.Text,
                    DescriptionReference = "reference1:entityId del proveedor de credito para las facturas al credito,reference4: customerCreditLineId linea de credito del cliente",
                    Reference2 = txtReference2.Text,
                    Reference3 = txtReference3.Text,
                    Reference4 = string.IsNullOrEmpty(txtCustomerCreditLineID.Text) ? "0" : txtCustomerCreditLineID.Text,
                    StatusId = TxtStatusId,
                    Amount = 0,
                    CurrencyId = ((TbCurrency)txtCurrencyID.SelectedItem).CurrencyId,
                    SourceWarehouseId = (int)txtWarehouseID.SelectedItem, //validar antes de pasar valor
                    PeriodPay = (int)txtPeriodPay.SelectedItem, //validar antes de pasar valor
                    NextVisit = txtNextVisit.DateTime,
                    NumberPhone = txtNumberPhone.Text,
                    EntityIdsecondary = ((TbEmployee)txtEmployeeID.SelectedItem).EmployeeId
                };
                objTmNew.CurrencyId2 = _objInterfazCoreWebCurrency.GetTarget(user.CompanyId, objTmNew.CurrencyId!.Value);
                objTmNew.ExchangeRate = _objInterfazCoreWebCurrency.GetRatio(user.CompanyId, DateOnly.FromDateTime(DateTime.Now), 1, objTmNew.CurrencyId2!.Value, objTmNew.CurrencyId!.Value);

                var objTmInfoNew = new TbTransactionMasterInfo
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

                //El Estado solo permite editar el workflow
                var dataContext = new DataContext();
                if (_objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_billing", "statusID", objTm.StatusId!.Value, commandEditable, user.CompanyId, user.BranchId, role.RoleId)!.Value)
                {
                    var objTmFind = dataContext.TbTransactionMasters.First(u =>
                        u.CompanyId == user.CompanyId &&
                        u.TransactionId == TransactionId!.Value &&
                        u.TransactionMasterId == TransactionMasterId!.Value
                    );
                    objTmFind.StatusId = TxtStatusId;
                    _objInterfazTransactionMasterModel.UpdateAppPosme(user.CompanyId, TransactionId!.Value, TransactionMasterId!.Value, objTmFind);
                }
                else
                {
                    _objInterfazTransactionMasterModel.UpdateAppPosme(user.CompanyId, TransactionId!.Value, TransactionMasterId!.Value, objTmNew);
                    _objInterfazTransactionMasterInfoModel.UpdateAppPosme(user.CompanyId, TransactionId!.Value, TransactionMasterId!.Value, objTmInfoNew);
                }

                //Leer archivo
                // Obtener la referencia al ensamblado actual
                var assembly = Assembly.GetEntryAssembly();

                // Obtener la ruta del archivo ejecutable
                var executablePath = assembly!.Location;
                var path = $"{executablePath}/company_{user.CompanyId}/component_{objComponentBilling.ComponentId}/component_item_{TransactionMasterId}/procesar.csv";
                var pathNew = $"{executablePath}/company_{user.CompanyId}/component_{objComponentBilling.ComponentId}/component_item_{TransactionMasterId}/procesado.csv";
                var listTransactionDetalId = new List<int>();
                var arrayListItemId = new List<int>();
                var arrayListItemName = new List<string>();
                var arrayListQuantity = new List<int>();
                var arrayListPrice = new List<decimal>();
                var arrayListSubTotal = new List<decimal>();
                var arrayListIva = new List<decimal>();
                var arrayListLote = new List<string>();
                var arrayListVencimiento = new List<string>();
                var arrayListSku = new List<int>();
                var arrayListSkuFormatoDescription = new List<string>();

                if (File.Exists(path))
                {
                    //Actualizar Detalle
                    //Declarar e inicializar las listas
                    var objParameterDeliminterCsv = _objInterfazCoreWebParameter.GetParameter("CORE_CSV_SPLIT", user.CompanyId);
                    var characterSplie = objParameterDeliminterCsv!.Value!;

                    //Obtener los registro del archivo
                    var csvReader = new CsvReader();
                    csvReader.Separator = Convert.ToChar(characterSplie);
                    var table = csvReader.ParseFile(path);
                    var fila = 0;
                    File.Move(path, pathNew);

                    if (table.Count > 0)
                    {
                        foreach (var row in table)
                        {
                            fila++;
                            var codigo = row["Codigo"];
                            var description = row["Nombre"];
                            var cantidad = Convert.ToInt32(row["Cantidad"]);
                            var precio = Convert.ToDecimal(row["Precio"]);


                            var objItem = _objInterfazItemModel.GetRowByCode(user.CompanyId, codigo);
                            // Añadir los valores a las listas
                            listTransactionDetalId.Add(0);
                            arrayListItemId.Add(objItem.ItemId);
                            arrayListItemName.Add(objItem.Name);
                            arrayListQuantity.Add(cantidad);
                            arrayListPrice.Add(precio);
                            arrayListLote.Add("");
                            arrayListVencimiento.Add("");
                            arrayListSku.Add(0);
                            arrayListSkuFormatoDescription.Add("");
                        }
                    }
                }
                else
                {
                    //Actualizar Detalle
                    for (var i = 0; i < gridViewValues.RowCount; i++)
                    {
                        listTransactionDetalId.Add((int)gridViewValues.GetRowCellValue(i, colItemNumber.Name));
                        arrayListItemId.Add(Convert.ToInt32(gridViewValues.GetRowCellValue(i, colItemId)));
                        arrayListItemName.Add(gridViewValues.GetRowCellValue(i, colTransactionDetailName.Name).ToString()!);
                        arrayListQuantity.Add(Convert.ToInt32(gridViewValues.GetRowCellValue(i, colQuantity)));
                        arrayListPrice.Add(Convert.ToDecimal(gridViewValues.GetRowCellValue(i, colPrice)));
                        arrayListSubTotal.Add(Convert.ToDecimal(gridViewValues.GetRowCellValue(i, colSubTotal)));
                        arrayListIva.Add(Convert.ToDecimal(gridViewValues.GetRowCellValue(i, colIva)));
                        arrayListLote.Add("");
                        arrayListVencimiento.Add("");
                        arrayListSku.Add((int)gridViewValues.GetRowCellValue(i, colSku));
                        arrayListSkuFormatoDescription.Add(gridViewValues.GetRowCellValue(i, colSkuFormatoDescripton).ToString()!);
                    }
                }

                //Ingresar la configuracion de precios			
                var objParameterPriceDefault = _objInterfazCoreWebParameter.GetParameter("INVOICE_DEFAULT_PRICELIST", user.CompanyId);
                var listPriceId = objParameterPriceDefault!.Value;
                var objTipePrice = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_price", "typePriceID", user.CompanyId);
                var objParameterUpdatePrice = _objInterfazCoreWebParameter.GetParameter("INVOICE_UPDATEPRICE_ONLINE", user.CompanyId);
                var objUpdatePrice = objParameterUpdatePrice!.Value;
                var objParameterAmortizationDuranteFactura = _objInterfazCoreWebParameter.GetParameter("INVOICE_PARAMTER_AMORITZATION_DURAN_INVOICE", user.CompanyId)!.Value;
                decimal? amountTotal = decimal.Zero;
                var tax1Total = decimal.Zero;
                decimal subAmountTotal = 0;
                _objInterfazTransactionMasterDetailModel.DeleteWhereIdNotIn(user.CompanyId, TransactionId.Value, TransactionMasterId.Value, listTransactionDetalId);

                if (arrayListItemId.Count > 0)
                {
                    for (var i = 0; i < arrayListItemId.Count; i++)
                    {
                        var itemID = arrayListItemId[i];
                        var lote = arrayListLote == null ? "" : arrayListLote[i];
                        var vencimiento = arrayListVencimiento == null ? "" : arrayListVencimiento[i];
                        var warehouseID = objTmNew.SourceWarehouseId;
                        var objItem = _objInterfazItemModel.GetRowByPk(user.CompanyId, itemID);
                        var objItemWarehouse = VariablesGlobales.Instance.UnityContainer.Resolve<IItemWarehouseModel>().GetByPk(user.CompanyId, itemID, warehouseID!.Value);
                        var quantity = WebToolsHelper.ConvertToNumber<int>(arrayListQuantity[i].ToString());
                        var unitaryCost = objItem.Cost;
                        var objPrice = _objInterfazPriceModel.GetRowByPk(user.CompanyId, objListPrice!.ListPriceId, itemID, (int)typePriceId);
                        var objCompanyComponentConcept = _objInterfazCompanyComponentConceptModel.GetRowByPk(user.CompanyId, objComponentItem.ComponentId, itemID, "IVA");
                        var skuCatalogItemID = arrayListSku[i];
                        string itemNameDetail = arrayListItemName[i].Replace("\"", "").Replace("'", "");
                        var objItemSku = _objInterfazItemSkuModel.GetByPk(itemID, skuCatalogItemID);

                        // Precio
                        var price = arrayListPrice[i] / objItemSku.Value;
                        var skuFormatoDescription = arrayListSkuFormatoDescription[i];
                        var ivaPercentage = (objCompanyComponentConcept != null ? objCompanyComponentConcept.ValueOut : decimal.Zero);
                        var unitaryAmount = price * (1 + ivaPercentage);
                        var tax1 = price * ivaPercentage;
                        int transactionMasterDetailID = listTransactionDetalId[i];
                        decimal comisionPorcentage = 0;

                        // Obtener porcentaje de comisión
                        var coreWebTransactionMasterDetail = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTransactionMasterDetail>();
                        comisionPorcentage = coreWebTransactionMasterDetail.GetPercentageCommission(user.CompanyId, Convert.ToInt32(listPriceId), itemID.ToString(), price);

                        // Obtener costo unitario del cliente
                        unitaryCost = coreWebTransactionMasterDetail.GetCostCustomer(user.CompanyId, itemID.ToString(), unitaryCost, price);

                        // Actualizar nombre
                        if (objParameterAll["INVOICE_UPDATENAME_IN_TRANSACTION_ONLY"] == "false")
                        {
                            // Crear nuevo objeto de item
                            var objItemNew = _objInterfazItemModel.GetRowByPk(user.CompanyId, itemID);
                            objItemNew.Name = itemNameDetail.Trim();

                            // Actualizar el nombre del item
                            _objInterfazItemModel.UpdateAppPosme(user.CompanyId, itemID, objItemNew);

                            if (itemNameDetail.Contains("NC."))
                            {
                                // Actualizar nombre y código de barras
                                objItemNew.Name = itemNameDetail.Split("NC.")[0].Trim();
                                objItemNew.BarCode = objItem.BarCode + "," + itemNameDetail.Split("NC.")[1].Trim();
                                itemNameDetail = objItemNew.Name;
                                _objInterfazItemModel.UpdateAppPosme(user.CompanyId, itemID, objItemNew);
                            }

                            if (itemNameDetail.Contains("CC."))
                            {
                                // Actualizar nombre y código de barras
                                objItemNew.Name = itemNameDetail.Split("CC.")[0].Trim();
                                objItemNew.BarCode = itemNameDetail.Split("CC.")[1].Trim();
                                itemNameDetail = objItemNew.Name;

                                _objInterfazItemModel.UpdateAppPosme(user.CompanyId, itemID, objItemNew);
                            }
                        }

                        //Validar Cantidades
                        var messageException = $"La cantidad de '{objItem.ItemNumber} {objItem.Name} ' es mayor que la disponible en bodega, en bodega existen {objItemWarehouse.Quantity} y esta solicitando : {quantity}";
                        if (objItemWarehouse.Quantity < quantity && objItem.IsInvoiceQuantityZero == 0 && ObjParameterInvoiceBillingQuantityZero == "false")
                        {
                            throw new Exception(messageException);
                        }

                        //Nuevo Detalle
                        if (transactionMasterDetailID == 0)
                        {
                            var objTmd = new TbTransactionMasterDetail
                            {
                                CompanyId = objTm.CompanyId,
                                TransactionId = objTm.TransactionId,
                                TransactionMasterId = TransactionMasterId!.Value,
                                ComponentId = objComponentItem.ComponentId,
                                ComponentItemId = itemID,
                                Quantity = quantity * objItemSku.Value, // cantIdad
                                SkuQuantity = quantity, // cantIdad
                                SkuQuantityBySku = objItemSku.Value, // cantIdad
                                UnitaryCost = unitaryCost,
                                UnitaryPrice = price, // precio de lista
                                UnitaryAmount = unitaryAmount, // precio de lista con impuesto
                                Tax1 = tax1,
                                Discount = 0,
                                PromotionId = 0,
                                Reference1 = lote,
                                Reference2 = vencimiento,
                                Reference3 = "0",
                                ItemNameLog = itemNameDetail,
                                CatalogStatusId = 0,
                                InventoryStatusId = 0,
                                IsActive = true,
                                QuantityStock = 0,
                                QuantiryStockInTraffic = 0,
                                QuantityStockUnaswared = 0,
                                RemaingStock = 0,
                                ExpirationDate = null,
                                InventoryWarehouseSourceId = objTmNew.SourceWarehouseId,
                                InventoryWarehouseTargetId = objTmNew.TargetWarehouseId,
                                SkuCatalogItemId = skuCatalogItemID,
                                SkuFormatoDescription = skuFormatoDescription,
                                AmountCommision = price * comisionPorcentage * quantity // impuesto de lista
                            };
                            objTmd.Cost = objTmd.Quantity * unitaryCost; // costo por unIdad
                            objTmd.Amount = objTmd.Quantity * unitaryAmount; // precio de lista con impuesto por cantIdad

                            tax1Total = decimal.Add(tax1Total, (decimal)tax1!);
                            subAmountTotal = subAmountTotal + (quantity * price);
                            amountTotal = amountTotal + objTmd.Amount;
                            transactionMasterDetailID = _objInterfazTransactionMasterDetailModel.InsertAppPosme(objTmd);

                            var objTmdc = new TbTransactionMasterDetailCredit();
                            objTmdc.TransactionMasterId = TransactionMasterId.Value;
                            objTmdc.TransactionMasterDetailId = transactionMasterDetailID;
                            objTmdc.Reference1 = txtFixedExpenses.Text;
                            objTmdc.Reference2 = txtReportSinRiesgo.IsOn ? "true" : "false";
                            objTmdc.Reference3 = "txtLayFirstLineProtocolo";
                            objTmdc.Reference4 = "";
                            objTmdc.Reference5 = "";
                            objTmdc.Reference9 = "reference1: Porcentaje de Gastos fijos para las facturas de credito,reference2: Escritura Publica,reference3: Primer Linea del Protocolo";
                            _objInterfazTransactionMasterDetailCreditModel.InsertAppPosme(objTmdc);

                            // Actualizar el Precio
                            if (objUpdatePrice == "true") // Si objUpdatePrice es un bool
                            {
                                // Calcular el porcentaje
                                var percentage = (unitaryCost == 0) ? (price / 100) : (((100 * price) / unitaryCost) - 100);

                                // Crear un diccionario para almacenar los datos de actualización del precio
                                var dataUpdatePrice = _objInterfazPriceModel.GetRowByPk(user.CompanyId, Convert.ToInt32(listPriceId), itemID, (int)typePriceId);
                                if (dataUpdatePrice is not null)
                                {
                                    dataUpdatePrice.Price = price;
                                    dataUpdatePrice.Percentage = percentage;

                                    // Llamar al método de actualización de precio en el modelo de precio
                                    _objInterfazPriceModel.UpdateAppPosme(user.CompanyId, Convert.ToInt32(listPriceId), itemID, (int)typePriceId, dataUpdatePrice);
                                }
                            }
                        }
                        else
                        {
                            var objTmdc = _objInterfazTransactionMasterDetailCreditModel.GetRowByPk(transactionMasterDetailID);
                            var objTmdNew = dataContext.TbTransactionMasterDetails.Where(u => u.TransactionMasterDetailId == transactionMasterDetailID).First();

                            objTmdNew.Quantity = quantity * objItemSku.Value; // cantidad
                            objTmdNew.SkuQuantity = quantity; // cantidad
                            objTmdNew.SkuQuantityBySku = objItemSku.Value; // cantidad
                            objTmdNew.UnitaryCost = unitaryCost; // costo
                            objTmdNew.UnitaryPrice = price; // precio de lista
                            objTmdNew.UnitaryAmount = unitaryAmount; // precio de lista con impuesto
                            objTmdNew.Tax1 = tax1; // impuesto de lista
                            objTmdNew.Reference1 = lote;
                            objTmdNew.Reference2 = vencimiento;
                            objTmdNew.Reference3 = "0";
                            objTmdNew.InventoryWarehouseSourceId = objTmNew.SourceWarehouseId;
                            objTmdNew.ItemNameLog = itemNameDetail;
                            objTmdNew.SkuCatalogItemId = skuCatalogItemID;
                            objTmdNew.SkuFormatoDescription = skuFormatoDescription;
                            objTmdNew.AmountCommision = price * comisionPorcentage * quantity;
                            objTmdNew.Cost = objTmdNew.Quantity * unitaryCost; // costo por cantidad
                            objTmdNew.Amount = objTmdNew.Quantity * unitaryAmount; // precio de lista con impuesto por cantidad


                            tax1Total = decimal.Add(tax1Total, (decimal)tax1!);
                            subAmountTotal = subAmountTotal + (quantity * price);
                            amountTotal = amountTotal + objTmdNew.Amount;
                            _objInterfazTransactionMasterDetailModel.UpdateAppPosme(user.CompanyId, TransactionId.Value, TransactionMasterId.Value, transactionMasterDetailID, objTmdNew);

                            objTmdc.Reference1 = txtFixedExpenses.Text;
                            objTmdc.Reference2 = txtReportSinRiesgo.IsOn ? "true" : "false";
                            objTmdc.Reference3 = "txtLayFirstLineProtocolo";
                            objTmdc.Reference4 = "";
                            objTmdc.Reference5 = "";
                            objTmdc.Reference9 = "reference1: Porcentaje de Gastos Fijos para las Facturas de Credito,reference2: Escritura Publica,reference3: Primer Linea del Protocolo";
                            _objInterfazTransactionMasterDetailCreditModel.UpdateAppPosme(transactionMasterDetailID, objTmdc);

                            //Actualizar el Precio
                            if (objUpdatePrice == "true")
                            {
                                var dataUpdatePrice = _objInterfazPriceModel.GetRowByPk(user.CompanyId, Convert.ToInt32(listPriceId), itemID, (int)typePriceId);
                                if (dataUpdatePrice is not null)
                                {
                                    dataUpdatePrice.Price = price;
                                    dataUpdatePrice.Percentage = unitaryCost == 0 ? (price / 100) : (((100 * price) / unitaryCost) - 100);
                                    _objInterfazPriceModel.UpdateAppPosme(user.CompanyId, Convert.ToInt32(listPriceId), itemID, (int)typePriceId, dataUpdatePrice);
                                }
                            }
                        }
                    }
                }

                //Actualizar Transaccion			
                objTmNew.Amount = amountTotal;
                objTmNew.Tax1 = tax1Total;
                objTmNew.SubAmount = subAmountTotal;
                _objInterfazTransactionMasterModel.UpdateAppPosme(user.CompanyId, TransactionId.Value, TransactionMasterId.Value, objTmNew);

                //Aplicar el Documento?
                var COMMAND_APLICABLE = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_APLICABLE"]);
                if (
                    _objInterfazCoreWebWorkflow.ValidateWorkflowStage
                    (
                        "tb_transaction_master_billing",
                        "statusID",
                        objTmNew.StatusId.Value,
                        COMMAND_APLICABLE,
                        user.CompanyId,
                        user.BranchId,
                        role.RoleId
                    )!.Value && oldStatusId != objTmNew.StatusId
                )
                {
                    //Actualizar el numero de factura
                    var objTmNew003 = dataContext.TbTransactionMasters.Where(x => x.TransactionMasterId == TransactionMasterId.Value).First();
                    objTmNew003.TransactionNumber = _objInterfazCoreWebCounter.GoNextNumber(user.CompanyId, user.BranchId, "tb_transaction_master_billing", 0);
                    _objInterfazTransactionMasterModel.UpdateAppPosme(user.CompanyId, TransactionId.Value, TransactionMasterId.Value, objTmNew003);


                    //Acumular punto del cliente.
                    if (objTmInfoNew.ReceiptAmountPoint <= 0 && objTmNew.CurrencyId == ObjCurrencyCordoba.CurrencyId)
                    {
                        var objCustomer = dataContext.TbCustomers.Where(x => x.EntityId == objTmNew.EntityId.Value).First();
                        objCustomer.BalancePoint = objCustomer.BalancePoint + amountTotal;
                        _objInterfazCustomerModel.UpdateAppPosme(objCustomer.CompanyId, objCustomer.BranchId, objCustomer.EntityId, objCustomer);
                    }

                    //Es pago con punto restar puntos
                    if (objTmInfoNew.ReceiptAmountPoint > 0 && objTmNew.CurrencyId == ObjCurrencyCordoba.CurrencyId)
                    {
                        var objCustomer = dataContext.TbCustomers.Where(x => x.EntityId == objTmNew.EntityId.Value).First();
                        objCustomer.BalancePoint = objCustomer.BalancePoint - objTmInfoNew.ReceiptAmountPoint;
                        _objInterfazCustomerModel.UpdateAppPosme(objCustomer.CompanyId, objCustomer.BranchId, objCustomer.EntityId, objCustomer);
                    }

                    //Ingresar en Kardex.
                    VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebInventory>().CalculateKardexNewOutput(user.CompanyId, TransactionId.Value, TransactionMasterId.Value);

                    //Crear Conceptos.
                    VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebConcept>().Billing(user.CompanyId, TransactionId.Value, TransactionMasterId.Value);

                    //Si es al credito crear tabla de amortizacion
                    string[] causalIDTypeCredit = ParameterCausalTypeCredit!.Value!.Split(',');
                    var exisCausalInCredit = Array.IndexOf(causalIDTypeCredit, objTmNew.TransactionCausalId) > 0;

                    //si la factura es de credito
                    if (exisCausalInCredit)
                    {
                        //Crear documento del modulo
                        var objCustomerCreditLine = _objInterfazCustomerCreditLineModel.GetRowByPk(Convert.ToInt32(objTmNew.Reference4));
                        var objCustomerCreditDocument = new TbCustomerCreditDocument
                        {
                            CompanyId = user.CompanyId,
                            EntityId = objCustomerCreditLine.EntityId,
                            CustomerCreditLineId = objCustomerCreditLine.CustomerCreditLineId,
                            DocumentNumber = objTmNew003.TransactionNumber,
                            DateOn = DateOnly.FromDateTime(objTmNew.TransactionOn.Value),
                            ExchangeRate = objTmNew.ExchangeRate.Value,
                            Interes = objCustomerCreditLine.InterestYear,
                            Term = objCustomerCreditLine!.Term!.Value,
                            Amount = amountTotal!.Value,
                            Balance = amountTotal.Value
                        };


                        if (objParameterAmortizationDuranteFactura == "true" && objTmNew.CurrencyId == 1 /*cordoba*/)
                        {
                            objCustomerCreditDocument.Term = Convert.ToInt32(objTmNew.Reference2);
                            var amount = amountTotal -
                                         objTmInfoNew.ReceiptAmountPoint.Value -
                                         objTmInfoNew.ReceiptAmount.Value -
                                         objTmInfoNew.ReceiptAmountBank -
                                         objTmInfoNew.ReceiptAmountCard -
                                         Math.Round(objTmInfoNew.ReceiptAmountBankDol * objTmNew.ExchangeRate.Value, 2) -
                                         Math.Round(objTmInfoNew.ReceiptAmountCardDol * objTmNew.ExchangeRate.Value, 2) -
                                         Math.Round(objTmInfoNew.ReceiptAmountDol * objTmNew.ExchangeRate.Value, 2);

                            // Asignar el valor calculado a la propiedad Amount de objCustomerCreditDocument
                            objCustomerCreditDocument.Amount = amount.Value;
                            objCustomerCreditDocument.Balance = amount.Value;
                        }

                        if (objParameterAmortizationDuranteFactura == "true" && objTmNew.CurrencyId == 2 /*dolares*/)
                        {
                            objCustomerCreditDocument.Term = Convert.ToInt32(objTmNew.Reference2);
                            objCustomerCreditDocument.Amount = amountTotal.Value -
                                                               objTmInfoNew.ReceiptAmountPoint.Value -
                                                               objTmInfoNew.ReceiptAmount.Value -
                                                               objTmInfoNew.ReceiptAmountBank -
                                                               objTmInfoNew.ReceiptAmountCard -
                                                               Math.Round(objTmInfoNew.ReceiptAmountBankDol / objTmNew.ExchangeRate.Value, 2) -
                                                               Math.Round(objTmInfoNew.ReceiptAmountCardDol / objTmNew.ExchangeRate.Value, 2) -
                                                               Math.Round(objTmInfoNew.ReceiptAmountDol / objTmNew.ExchangeRate.Value, 2);
                            objCustomerCreditDocument.Balance = objCustomerCreditDocument.Amount;
                        }

                        objCustomerCreditDocument.CurrencyId = objTmNew.CurrencyId.Value;
                        objCustomerCreditDocument.StatusId = _objInterfazCoreWebWorkflow.GetWorkflowInitStage("tb_customer_credit_document", "statusID", user.CompanyId, user.BranchId, role.RoleId)![0].WorkflowStageId;
                        objCustomerCreditDocument.Reference1 = objTmNew.Note;
                        objCustomerCreditDocument.Reference2 = "";
                        objCustomerCreditDocument.Reference3 = "";
                        objCustomerCreditDocument.IsActive = 1;
                        objCustomerCreditDocument.ProviderIdcredit = Convert.ToInt32(objTmNew.Reference1);
                        objCustomerCreditDocument.PeriodPay = objCustomerCreditLine.PeriodPay;

                        if (objParameterAmortizationDuranteFactura == "true")
                        {
                            objCustomerCreditDocument.PeriodPay = objTmNew.PeriodPay.Value;
                        }

                        objCustomerCreditDocument.TypeAmortization = objCustomerCreditLine.TypeAmortization;
                        objCustomerCreditDocument.ReportSinRiesgo = txtReportSinRiesgo.IsOn ? 1 : 0;
                        var customerCreditDocumentID = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerCreditDocumentModel>().InsertAppPosme(objCustomerCreditDocument);
                        var periodPay = VariablesGlobales.Instance.UnityContainer.Resolve<ICatalogItemModel>().GetRowByCatalogItemId(objCustomerCreditLine.PeriodPay);

                        if (objParameterAmortizationDuranteFactura == "true")
                        {
                            periodPay = VariablesGlobales.Instance.UnityContainer.Resolve<ICatalogItemModel>().GetRowByCatalogItemId(objTmNew.PeriodPay.Value);
                        }

                        var objCatalogItem_DiasNoCobrables = _objInterfazCoreWebCatalog.GetCatalogAllItemByNameCatalogo("CXC_NO_COBRABLES", user.CompanyId);
                        var objCatalogItem_DiasFeriados365 = _objInterfazCoreWebCatalog.GetCatalogAllItemByNameCatalogo("CXC_NO_COBRABLES_FERIADOS_365", user.CompanyId);
                        var objCatalogItem_DiasFeriados366 = _objInterfazCoreWebCatalog.GetCatalogAllItemByNameCatalogo("CXC_NO_COBRABLES_FERIADOS_366", user.CompanyId);

                        //Crear tabla de amortizacion
                        VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebFinancialAmort>().Amort(
                            objCustomerCreditDocument.Amount, /*monto*/
                            objCustomerCreditDocument.Interes, /*interes anual*/
                            objCustomerCreditDocument.Term, /*numero de pagos*/
                            periodPay.Sequence!.Value, /*frecuencia de pago en dia*/
                            objTmNew.TransactionOn2, /*fecha del credito*/
                            objCustomerCreditLine.TypeAmortization /*tipo de amortizacion*/,
                            objCatalogItem_DiasNoCobrables,
                            objCatalogItem_DiasFeriados365,
                            objCatalogItem_DiasFeriados366
                        );
                        var tableAmortization = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebFinancialAmort>().GetTable();
                        if (tableAmortization.ListDetailDto is not null && tableAmortization.ListDetailDto.Count > 0)
                        {
                            foreach (var itemAmortization in tableAmortization.ListDetailDto)
                            {
                                var objCustomerAmoritizacion = new TbCustomerCreditAmortization
                                {
                                    CustomerCreditDocumentId = customerCreditDocumentID,
                                    BalanceStart = itemAmortization!.SaldoInicial!.Value,
                                    DateApply = itemAmortization.Date!.Value,
                                    Interest = itemAmortization.Interes!.Value,
                                    Capital = itemAmortization.Principal!.Value,
                                    Share = itemAmortization.Cuota!.Value,
                                    BalanceEnd = itemAmortization.Saldo!.Value,
                                    Remaining = itemAmortization.Cuota.Value,
                                    DayDelay = 0,
                                    Note = "",
                                    StatusId = _objInterfazCoreWebWorkflow.GetWorkflowInitStage("tb_customer_credit_amoritization", "statusID", user.CompanyId, user.BranchId, role.RoleId)![0].WorkflowStageId,
                                    IsActive = 1
                                };
                                _objInterfazCustomerCreditAmortizationModel.InsertAppPosme(objCustomerAmoritizacion);
                            }
                        }

                        //Crear las personas relacionadas a la factura
                        var objEntityRelated = new TbCustomerCreditDocumentEntityRelated();
                        objEntityRelated.CustomerCreditDocumentId = customerCreditDocumentID;
                        objEntityRelated.EntityId = objCustomerCreditLine.EntityId;
                        objEntityRelated.Type = Convert.ToInt32(_objInterfazCoreWebParameter.GetParameter("CXC_PROPIETARIO_DEL_CREDITO", user.CompanyId)!.Value);
                        objEntityRelated.TypeCredit = 401; // Comercial
                        objEntityRelated.StatusCredit = 429; // Activo
                        objEntityRelated.TypeGarantia = 444; // Pagare
                        objEntityRelated.TypeRecuperation = 450; // Recuperación normal
                        objEntityRelated.RatioDesembolso = 1;
                        objEntityRelated.RatioBalance = 1;
                        objEntityRelated.RatioBalanceExpired = 1;
                        objEntityRelated.RatioShare = 1;
                        objEntityRelated.IsActive = 1;
                        VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebAuditoria>().SetAuditCreated(objEntityRelated, user, "");
                        VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerCreditDocumentEntityRelatedModel>().InsertAppPosme(objEntityRelated);

                        var montoTotalCordobaCredit = objTmNew.CurrencyId == 1 ? objCustomerCreditDocument.Amount : Math.Round((objCustomerCreditDocument.Amount * objTmNew.ExchangeRate.Value), 2);
                        var montoTotalDolaresCredit = objTmNew.CurrencyId == 2 ? objCustomerCreditDocument.Amount : Math.Round((objCustomerCreditDocument.Amount / objTmNew.ExchangeRate.Value), 2);


                        //disminuir el balance de general	
                        var objCustomerCredit = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerCreditModel>().GetRowByPk(objCustomerCreditLine.CompanyId, objCustomerCreditLine.BranchId, objCustomerCreditLine.EntityId);
                        objCustomerCredit.BalanceDol = objCustomerCredit.BalanceDol - montoTotalDolaresCredit;
                        VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerCreditModel>().UpdateAppPosme(objCustomerCreditLine.CompanyId, objCustomerCreditLine.BranchId, objCustomerCreditLine.EntityId, objCustomerCredit);

                        //disminuir el balance de linea
                        decimal balance;
                        if (objCustomerCreditLine.CurrencyId == ObjCurrencyCordoba.CurrencyId)
                            balance = objCustomerCreditLine.Balance - montoTotalCordobaCredit;
                        else
                            balance = objCustomerCreditLine.Balance - montoTotalDolaresCredit;

                        var objCustomerCreditLineNew = _objInterfazCustomerCreditLineModel.GetRowByPk(objCustomerCreditLine.CustomerCreditLineId);
                        objCustomerCreditLineNew.Balance = balance;
                        _objInterfazCustomerCreditLineModel.UpdateAppPosme(objCustomerCreditLine.CustomerCreditLineId, objCustomerCreditLineNew);
                    }
                }
            }
            catch (Exception e)
            {
                new CoreWebRenderInView().GetMessageAlert(TypeError.Error, "Error", e.Message, this);
            }
        }

        public void OpenCashbox()
        {
            try
            {
                var user = VariablesGlobales.Instance.User;
                if (user is null)
                {
                    throw new Exception("No existe el usuario");
                }

                //obtener nombre de impresora por defecto
                var objParameterPrinterName = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_PRINTER_DIRECT_NAME_DEFAULT", user.CompanyId)!.Value;
                var coreWebPrinter = new CoreWebPrinterDirect();
                var pd = coreWebPrinter.ConfigurationPrinter(objParameterPrinterName!);
                //PrintDocument maneja el evento para imprimir
                //pd.PrintPage + = new PrintPageEventHandler (pd_PrintPage);  

                //Print the document  
                //pd.Print();  
            }
            catch (Exception ex)
            {
                new CoreWebRenderInView().GetMessageAlert(TypeError.Error, "Error", $"Error al imprimir: {ex.Message}", this);
            }
        }

        public void PreRender()
        {
            var imagenInvoice = VariablesGlobales.ConfigurationBuilder["PATH_IMAGE_IN_INVOICE_POSME"];
            if (imagenInvoice is not null)
            {
                if (File.Exists(imagenInvoice))
                {
                    pictureEdit2.Image = System.Drawing.Image.FromFile(imagenInvoice);
                }
            }

            var imageCustomer = VariablesGlobales.ConfigurationBuilder["PATH_IMAGE_IN_INVOICE_CUSTOMER"];
            if (imageCustomer is not null)
            {
                if (File.Exists(imageCustomer))
                {
                    pictureEdit1.Image = System.Drawing.Image.FromFile(imageCustomer);
                }
            }
        }

        public void LoadRender(TypeRender typeRender)
        {
            var objCoreWebRenderInView = new CoreWebRenderInView();
            switch (typeRender)
            {
                case TypeRender.New:
                    var employerDefault = ObjListParameterAll["INVOICE_BILLING_EMPLOYEE_DEFAULT"];
                    if (employerDefault == "true")
                        objCoreWebRenderInView.LlenarComboBox(ObjListEmployee, txtEmployeeID, "entityID", "firstName", null, 0);
                    else
                        objCoreWebRenderInView.LlenarComboBox(ObjListEmployee, txtEmployeeID, "entityID", "firstName", null, null);

                    objCoreWebRenderInView.LlenarComboBox(ObjListCurrency, txtCurrencyID, "currencyID", "name", null, 0);
                    objCoreWebRenderInView.LlenarComboBox(ObjListZone, txtZoneID, "catalogItemID", "name", null, 0);
                    objCoreWebRenderInView.LlenarComboBox(ObjListWarehouse, txtWarehouseID, "warehouseID", "name", null, 0);
                    objCoreWebRenderInView.LlenarComboBox(ObjListMesa, txtMesaID, "catalogItemID", "name", null, 0);
                    objCoreWebRenderInView.LlenarComboBox(ListProvider, txtReference1, "entityID", "firstName", null, 0);
                    objCoreWebRenderInView.LlenarComboBox(ObjListPay, txtPeriodPay, "catalogItemID", "name", ObjParameterCxcFrecuenciaPayDefault, 0);
                    objCoreWebRenderInView.LlenarComboBox(ObjCausal, txtCausalID, "transactionCausalID", "name", null, 0);
                    objCoreWebRenderInView.LlenarComboBox(ObjListCustomerCreditLine, txtCustomerCreditLineID, "customerCreditLineID", "accountNumber", null, 0);
                    objCoreWebRenderInView.LlenarComboBox(ObjListTypePrice, txtTypePriceID, "catalogItemID", "display", null, 0);
                    objCoreWebRenderInView.LlenarComboBox(ObjListBank, txtReceiptAmountTarjeta_BankID, "bankID", "name", null, 0);
                    objCoreWebRenderInView.LlenarComboBox(ObjListBank, txtReceiptAmountTarjetaDol_BankID, "bankID", "name", null, 0);
                    objCoreWebRenderInView.LlenarComboBox(ObjListBank, txtReceiptAmountBank_BankID, "bankID", "name", null, 0);
                    objCoreWebRenderInView.LlenarComboBox(ObjListBank, txtReceiptAmountBankDol_BankID, "bankID", "name", null, 0);
                    lblTitulo.Text = @"Factura: #00000000";                    
                    txtExchangeRate.Text = ExchangeRate.ToString(CultureInfo.InvariantCulture);
                    txtCustomerDescription.Text = ObjNaturalDefault is not null ?
                                                    ($"{ObjCustomerDefault.CustomerNumber} {ObjNaturalDefault!.FirstName!.ToUpper()} {ObjNaturalDefault!.LastName!.ToUpper()}") :
                                                    ($"{ObjCustomerDefault.CustomerNumber} {ObjLegalDefault!.ComercialName!.ToUpper()}");
                    txtDate.DateTime = DateTime.Now;
                    txtNote.Text = string.Empty;
                    TxtCustomerId = ObjCustomerDefault.EntityId;
                    txtReferenceClientName.Text = string.Empty;
                    txtReferenceClientIdentifier.Text = string.Empty;
                    txtNumberPhone.Text = string.Empty;
                    txtNextVisit.Text = string.Empty;
                    txtFixedExpenses.Text = string.Empty;
                    txtReference2.Text = ObjParameterCxcPlazoDefault;
                    txtReference3.Text = ObjEmployeeNatural is null ? "N/D" : ObjEmployeeNatural.FirstName;
                    txtIsApplied.Checked = false;
                    txtDateFirst.DateTime = DateTime.Now;
                    txtDesembolsoEfectivo.IsOn = true;
                    txtReportSinRiesgo.IsOn = true;
                    TxtStatusOldId = ObjListWorkflowStage!.ElementAt(0).WorkflowStageId;
                    TxtStatusId = ObjListWorkflowStage!.ElementAt(0).WorkflowStageId;
                    txtSubTotal.Text = @"0.0";
                    txtIva.Text = @"0.0";
                    txtTotal.Text = @"0.0";
                    txtChangeAmount.Text = @"0.0";
                    txtReceiptAmount.Text = @"0.0";
                    txtReceiptAmountDol.Text = @"0.0";
                    txtReceiptAmountTarjeta.Text = @"0.0";
                    txtReceiptAmountTarjeta_Reference.Text = @"0.0";
                    txtReceiptAmountTarjetaDol.Text = @"0.0";
                    txtReceiptAmountTarjetaDol_Reference.Text = string.Empty;
                    txtReceiptAmountBank.Text = @"0.0";
                    txtReceiptAmountBank_Reference.Text = string.Empty;
                    txtReceiptAmountBankDol.Text = @"0.0";
                    txtReceiptAmountBankDol_Reference.Text = string.Empty;
                    txtReceiptAmountPoint.Text = @"0.0";
                    break;
                case TypeRender.Edit:
                    objCoreWebRenderInView.LlenarComboBox(ObjListCurrency, txtCurrencyID, "currencyID", "name", ObjTransactionMaster.CurrencyId, 0);
                    objCoreWebRenderInView.LlenarComboBox(ObjListZone, txtZoneID, "catalogItemID", "display", ObjTransactionMasterInfo.ZoneId, 0);
                    objCoreWebRenderInView.LlenarComboBox(ObjListWarehouse, txtWarehouseID, "warehouseID", "name", ObjTransactionMaster.SourceWarehouseId, 0);
                    objCoreWebRenderInView.LlenarComboBox(ObjListEmployee, txtEmployeeID, "entityID", "firstName", ObjTransactionMaster.EntityIdsecondary, 0);
                    objCoreWebRenderInView.LlenarComboBox(ObjListMesa, txtMesaID, "catalogItemID", "name", ObjTransactionMasterInfo.MesaId, 0);
                    objCoreWebRenderInView.LlenarComboBox(ListProvider, txtReference1, "entityID", "firstName", ObjTransactionMaster.Reference1, 0);
                    objCoreWebRenderInView.LlenarComboBox(ObjListPay, txtPeriodPay, "catalogItemID", "name", ObjTransactionMaster.PeriodPay, 0);
                    objCoreWebRenderInView.LlenarComboBox(ObjCausal, txtCausalID, "transactionCausalID", "name", ObjTransactionMaster.TransactionCausalId, 0);
                    objCoreWebRenderInView.LlenarComboBox(ObjListCustomerCreditLine, txtCustomerCreditLineID, "customerCreditLineID", "accountNumber", ObjTransactionMaster.Reference4, 0);
                    objCoreWebRenderInView.LlenarComboBox(ObjListTypePrice, txtTypePriceID, "catalogItemID", "display", null, 0);
                    objCoreWebRenderInView.LlenarComboBox(ObjListBank, txtReceiptAmountTarjeta_BankID, "catalogItemID", "display", ObjTransactionMasterInfo.ReceiptAmountCardBankId, 0);
                    objCoreWebRenderInView.LlenarComboBox(ObjListBank, txtReceiptAmountTarjetaDol_BankID, "bankID", "name", ObjTransactionMasterInfo.ReceiptAmountCardBankDolId, 0);
                    objCoreWebRenderInView.LlenarComboBox(ObjListBank, txtReceiptAmountBank_BankID, "bankID", "name", ObjTransactionMasterInfo.ReceiptAmountBankId, 0);
                    objCoreWebRenderInView.LlenarComboBox(ObjListBank, txtReceiptAmountBankDol_BankID, "bankID", "name", ObjTransactionMasterInfo.ReceiptAmountBankDolId, 0);
                    lblTitulo.Text = ObjTransactionMaster.TransactionNumber is not null ? $@"Factura: #{ObjTransactionMaster.TransactionNumber}" : @"Factura: #00000000";
                    txtExchangeRate.Text = ExchangeRate.ToString(CultureInfo.InvariantCulture);
                    txtCustomerDescription.Text = ObjNaturalDefault is not null ?
                                                   ($"{ObjCustomerDefault.CustomerNumber} {ObjNaturalDefault!.FirstName!.ToUpper()} {ObjNaturalDefault!.LastName!.ToUpper()}") :
                                                   ($"{ObjCustomerDefault.CustomerNumber} {ObjLegalDefault!.ComercialName!.ToUpper()}");
                    CompanyId = ObjTransactionMaster.CompanyId;
                    TransactionId = ObjTransactionMaster.TransactionId;
                    TransactionMasterId = ObjTransactionMaster.TransactionMasterId;
                    TxtCustomerId = ObjTransactionMaster.EntityId!.Value;
                    txtDate.EditValue = ObjTransactionMaster.TransactionOn;
                    txtNote.Text = ObjTransactionMaster.Note;
                    txtReferenceClientName.Text = ObjTransactionMasterInfo.ReferenceClientName;
                    txtReferenceClientIdentifier.Text = ObjTransactionMasterInfo.ReferenceClientIdentifier;
                    txtReference3.Text = ObjTransactionMaster.Reference3;
                    txtNumberPhone.Text = ObjTransactionMaster.NumberPhone;
                    txtNextVisit.DateTime = ObjTransactionMaster.NextVisit!.Value;
                    txtFixedExpenses.Text = ObjTransactionMasterDetailCredit.Reference1;
                    txtIsApplied.Checked = ObjTransactionMaster.IsApplied!.Value;
                    txtDateFirst.DateTime = ObjTransactionMaster.TransactionOn2!.Value;
                    txtReference2.Text = ObjTransactionMaster.Reference2;
                    txtDesembolsoEfectivo.IsOn = true; //txtCheckDeEfectivo
                    txtReportSinRiesgo.IsOn = Convert.ToInt32(ObjTransactionMasterDetailCredit.Reference2) == 1;
                    TxtStatusOldId = ObjTransactionMaster.StatusId;
                    TxtStatusId = ObjTransactionMaster.StatusId;
                    txtChangeAmount.Text = ObjTransactionMasterInfo.ReceiptAmount!.Value.ToString("#0,000.00");
                    txtReceiptAmount.Text = ObjTransactionMasterInfo.ReceiptAmount!.Value.ToString("#0,000.00");
                    txtReceiptAmountDol.Text = ObjTransactionMasterInfo.ReceiptAmountDol.ToString("#0,000.00");
                    txtReceiptAmountTarjeta.Text = ObjTransactionMasterInfo.ReceiptAmountCard.ToString("#0,000.00");
                    txtReceiptAmountTarjeta_Reference.Text = ObjTransactionMasterInfo.ReceiptAmountCardBankReference;
                    txtReceiptAmountTarjetaDol.Text = ObjTransactionMasterInfo.ReceiptAmountCardDol.ToString("#0,000.00");
                    txtReceiptAmountTarjetaDol_Reference.Text = ObjTransactionMasterInfo.ReceiptAmountBankDolReference;
                    txtReceiptAmountBank.Text = ObjTransactionMasterInfo.ReceiptAmountBank.ToString("#0,000.00");
                    txtReceiptAmountBank_Reference.Text = ObjTransactionMasterInfo.ReceiptAmountBankReference;
                    txtReceiptAmountBankDol.Text = ObjTransactionMasterInfo.ReceiptAmountBankDol.ToString("#0,000.00");
                    txtReceiptAmountBankDol_Reference.Text = ObjTransactionMasterInfo.ReceiptAmountBankDolReference;
                    txtReceiptAmountPoint.Text = ObjTransactionMasterInfo.ReceiptAmountPoint!.Value.ToString("#0,000.00");
                    txtSubTotal.Text = @"0.0";
                    txtIva.Text = @"0.0";
                    txtTotal.Text = @"0.0";



                    if (ObjTransactionMasterDetail is not null)
                    {
                        foreach (var itemDto in ObjTransactionMasterDetail)
                        {
                            var Precio2 = ObjTransactionMasterItemPrice.Where(c => c.ItemId == itemDto.ComponentItemId && c.TypePriceId == (int)TypePrice.PorMayor).First().Price;
                            var Precio3 = ObjTransactionMasterItemPrice.Where(c => c.ItemId == itemDto.ComponentItemId && c.TypePriceId == (int)TypePrice.Credito).First().Price;
                            var ObjConcept = ObjTransactionMasterDetailConcept.Where(c => c.ComponentItemId == itemDto.ComponentItemId && c.Name == "IVA").ToList();
                            var Iva = ObjConcept.Count == 0 ? 0 : ObjConcept.ElementAt(0).ValueOut;

                            gridViewValues.AddNewRow();
                            var handle = gridViewValues.FocusedRowHandle;
                            gridViewValues.SetRowCellValue(handle, colCheckDetail, false);
                            gridViewValues.SetRowCellValue(handle, colTransactionMasterDetailId, itemDto.TransactionMasterDetailId);
                            gridViewValues.SetRowCellValue(handle, colItemId, itemDto.ComponentItemId);
                            gridViewValues.SetRowCellValue(handle, colItemNumber, itemDto.ItemNumber);
                            gridViewValues.SetRowCellValue(handle, colTransactionDetailName, itemDto.ItemNameLog);
                            gridViewValues.SetRowCellValue(handle, colSku, itemDto.SkuCatalogItemId);
                            gridViewValues.SetRowCellValue(handle, colQuantity, itemDto.SkuQuantity);
                            gridViewValues.SetRowCellValue(handle, colPrice, itemDto.UnitaryPrice * itemDto.SkuQuantityBySku);
                            gridViewValues.SetRowCellValue(handle, colSubTotal, itemDto.UnitaryPrice * itemDto.SkuQuantityBySku * itemDto.SkuQuantity);
                            gridViewValues.SetRowCellValue(handle, colIva, Iva);
                            gridViewValues.SetRowCellValue(handle, colSkuQuantityBySku, itemDto.SkuQuantityBySku);
                            gridViewValues.SetRowCellValue(handle, colUnitaryPriceIndividual, itemDto.UnitaryPrice);
                            gridViewValues.SetRowCellValue(handle, colAccion, "");
                            gridViewValues.SetRowCellValue(handle, colSkuFormatoDescripton, itemDto.SkuFormatoDescription);
                            gridViewValues.SetRowCellValue(handle, colItemPrecio2, Precio2);
                            gridViewValues.SetRowCellValue(handle, colItemPrecio3, Precio3);
                            gridViewValues.UpdateCurrentRow();



                        }
                    }

                    break;
            }


            //Incializar Focos
            if (ObjParameterScanerProducto == "true")
            {
                txtScanerCodigo.Focus();
            }

        }

        public string FnDeleteCerosIzquierdos(string Texto)
        {

            var array = Texto.Split("");
            var newTexto = "";
            var encontradoPrimerElemento = false;

            for (var i = 0; i < array.Length; i++)
            {
                if (array[i] != "0" && encontradoPrimerElemento == false)
                {
                    newTexto = newTexto + array[i];
                    encontradoPrimerElemento = true;
                }
                else
                {
                    if (encontradoPrimerElemento == true)
                    {
                        newTexto = newTexto + array[i];
                    }
                }
            }

            return newTexto;

        }

        public void FnCalculateAmountPay()
        {
            if(Convert.ToInt32( txtCurrencyID.EditValue)  == 1 /*cordoba*/ )
            {

                var ingresoCordoba = Convert.ToDecimal(txtReceiptAmount.Text);
                var bancoCordoba = Convert.ToDecimal(txtReceiptAmountBank.Text);
                var puntoCordoba = Convert.ToDecimal(txtReceiptAmountPoint.Text);

                var tarjetaCordoba = Convert.ToDecimal(txtReceiptAmountTarjeta.Text);
                var tarejtaDolares = Convert.ToDecimal(txtReceiptAmountTarjetaDol.Text);
                var bancoDolares = Convert.ToDecimal(txtReceiptAmountBankDol.Text);

                var ingresoDol = Convert.ToDecimal(txtReceiptAmountDol.Text);
                var tipoCambio = Convert.ToDecimal(txtExchangeRate.Text);
                var total = Convert.ToDecimal( txtTotal.Text);


                var resultTotal = (ingresoCordoba + bancoCordoba + puntoCordoba + tarjetaCordoba + (bancoDolares / tipoCambio) + (tarejtaDolares / tipoCambio) + (ingresoDol / tipoCambio)) - total;
                txtChangeAmount.Text = resultTotal.ToString();
            }
            if (Convert.ToInt32(txtCurrencyID.EditValue) ==2  /*dolar*/ )
            {

                var ingresoCordoba = Convert.ToDecimal(txtReceiptAmount.Text);
                var bancoCordoba = Convert.ToDecimal(txtReceiptAmountBank.Text);
                var puntoCordoba = Convert.ToDecimal(txtReceiptAmountPoint.Text);

                var tarjetaCordoba = Convert.ToDecimal(txtReceiptAmountTarjeta.Text);
                var tarejtaDolares = Convert.ToDecimal(txtReceiptAmountTarjetaDol.Text);
                var bancoDolares = Convert.ToDecimal(txtReceiptAmountBankDol.Text);

                var ingresoDol = Convert.ToDecimal(txtReceiptAmountDol.Text);
                var tipoCambio = Convert.ToDecimal(txtExchangeRate.Text);
                var total = Convert.ToDecimal(txtTotal.Text);

                var resultTotal = (ingresoCordoba + bancoCordoba + puntoCordoba + tarjetaCordoba + (bancoDolares * tipoCambio) + (tarejtaDolares * tipoCambio) + (ingresoDol * tipoCambio)) - total;
                txtChangeAmount.Text = resultTotal.ToString();

            }


        }

        public void FnCreateTableSearchProductos()
        {
            var warehouseID_ = Convert.ToInt32(txtWarehouseID.EditValue);
            var listPrice_ = ObjListPrice!.ListPriceId;
            var typePrice_ = Convert.ToInt32(txtTypePriceID.EditValue);
            var currencyID_ = Convert.ToInt32(txtCurrencyID.EditValue);


            var formTypeListSearch = new FormTypeListSearch("Lista de Productos", 33,
                "SELECCIONAR_ITEM_BILLING_POPUP_INVOICE", true,
                 @"{warehouseID:" + warehouseID_ + ", listPriceID:" + listPrice_ + ",typePriceID:" + typePrice_ + ",currencyID:" + currencyID_ + "}",
                 false, "", 0, 5, "");
            formTypeListSearch.EventoCallBackAceptar_ += EventoCallBackAceptarItem;
            formTypeListSearch.ShowDialog(this);
        }

        public void FnClearData()
        {

            gridViewTbTransactionMasterDetail.DataSource = null;
            gridViewValues.RefreshData();
            gridViewTbTransactionMasterDetail.RefreshDataSource();

            txtReceiptAmount.Text = "0";
			txtReceiptAmountDol.Text = "0";
            txtReceiptAmountBank.Text = "0";
            txtReceiptAmountPoint.Text = "0";
            txtChangeAmount.Text = "0";
            txtSubTotal.Text = "0";
            txtIva.Text = "0";
            txtTotal.Text = "0";
            txtReceiptAmountTarjeta.Text = "0";
            txtReceiptAmountTarjetaDol.Text = "0";
            txtReceiptAmountBankDol.Text = "0";
        }

        public void FnRecalculateDetail(bool clearRecibo, string sourceEvent)
        {

            var typePriceID = Convert.ToInt32(txtTypePriceID.EditValue);
            var cantidad = 0.0m;
            var iva = 0.0m;
            var precio = 0.0m;
            var subtotal = 0.0m;
            var total = 0.0m;

            var cantidadGeneral = 0.0m;
            var ivaGeneral = 0.0m;
            var precioGeneral = 0.0m;
            var subtotalGeneral = 0.0m;
            var totalGeneral = 0.0m;

            var priceTemporal = 0.0m;
            var cantidadTemporal = 0.0m;


            var NSSystemDetailInvoice = gridViewValues;
            for (var i = 0; i < NSSystemDetailInvoice.RowCount; i++)
            {

                var skuSelecte = Convert.ToInt32( NSSystemDetailInvoice.GetRowCellValue(i,colSku));
                var skuCatalogItemID = skuSelecte;
                var skuSelecteOption = 25;
                var skuValue = 30;
                var skuValuePrimceUnit = 25; //uniary price
                var skuValueDescription = "Litro , Unidad";


                cantidadTemporal =  Convert.ToInt32( NSSystemDetailInvoice.GetRowCellValue(i,colQuantity)); 
                priceTemporal = Convert.ToDecimal(NSSystemDetailInvoice.GetRowCellValue(i, colPrice));


                
                NSSystemDetailInvoice.SetRowCellValue(i, colQuantity, cantidadTemporal);
                NSSystemDetailInvoice.SetRowCellValue(i, colPrice, priceTemporal);
                NSSystemDetailInvoice.SetRowCellValue(i, colSkuFormatoDescripton, skuValueDescription);
                
                cantidad = Convert.ToInt32(NSSystemDetailInvoice.GetRowCellValue(i, colQuantity));
                precio = Convert.ToDecimal(NSSystemDetailInvoice.GetRowCellValue(i, colPrice));
                iva = Convert.ToDecimal(NSSystemDetailInvoice.GetRowCellValue(i, colIva));


                subtotal = precio * cantidad;
                iva = (precio * cantidad) * iva;
                total = iva + subtotal;
                
                
                cantidadGeneral = cantidadGeneral + cantidad;
                precioGeneral = precioGeneral + precio;
                ivaGeneral = ivaGeneral + iva;
                subtotalGeneral = subtotalGeneral + subtotal;
                totalGeneral = totalGeneral + total;
                
                NSSystemDetailInvoice.SetRowCellValue(i, colSubTotal, subtotal);
            }

            txtSubTotal.Text = subtotalGeneral.ToString();
            txtIva.Text = ivaGeneral.ToString();
            txtTotal.Text = totalGeneral.ToString();

            txtReceiptAmount.Text = "0";
            txtReceiptAmountDol.Text = "0";
            txtChangeAmount.Text = "0";
            txtReceiptAmountBank.Text = "0";
            txtReceiptAmountPoint.Text = "0";
            txtReceiptAmountTarjeta.Text = "0";
            txtReceiptAmountTarjetaDol.Text = "0";
            txtReceiptAmountBankDol.Text = "0";


        }


        public void FnGetConcept(int itemID, string concepName)
        {


            //Recalculoa el concepto via AJAX 2023-12-05 Inicio		
            DataContext dataContext = new DataContext();
            var index = 0;
            var encontrado = false;
            var objConcept = dataContext.TbCompanyComponentConcepts.Where(c =>
                    c.ComponentItemId == itemID &&
                    c.Name == concepName &&
                    c.ComponentId == ObjComponentItem!.ComponentId
            ).FirstOrDefault();

            for (index = 0; index < gridViewValues.RowCount; index++)
            {
                if (itemID == Convert.ToInt32(gridViewValues.GetRowCellValue(index, colItemId)))
                {
                    encontrado = true;
                    break;
                }
            }

            if (encontrado == false)
                return;

            if (objConcept != null)
            {
                gridViewValues.SetRowCellValue(index, colIva, objConcept.ValueOut);
            }

            FnRecalculateDetail(true, "");

        }


        public void FnOnCompleteNewItem(Dictionary<string, string> diccionario , bool sumar)
        {
            txtScanerCodigo.Focus();
            var index       = 0;
            var encontrado  = false;
            var itemID = Convert.ToInt32(diccionario["itemID"]);

            //Buscar Item
            for(index = 0; index < gridViewValues.RowCount; index++)
            {
                if(itemID == Convert.ToInt32( gridViewValues.GetRowCellValue(index,colItemId)))
                {
                    encontrado = true;
                    break;
                }
            }

            //Actualizar
            if (encontrado)
            {
                var quantity = Convert.ToInt32( gridViewValues.GetRowCellValue(index, colQuantity));
                gridViewValues.SetRowCellValue(index, colQuantity, (quantity + 1 ));

            }
            //Nuevo
            else
            {

                gridViewValues.AddNewRow();
                var handle = gridViewValues.FocusedRowHandle;
                gridViewValues.SetRowCellValue(handle, colCheckDetail, false);
                gridViewValues.SetRowCellValue(handle, colTransactionMasterDetailId, 0);
                gridViewValues.SetRowCellValue(handle, colItemId, diccionario["itemID"]);
                gridViewValues.SetRowCellValue(handle, colItemNumber, diccionario["Codigo"]);
                gridViewValues.SetRowCellValue(handle, colTransactionDetailName, diccionario["Nombre"] );
                gridViewValues.SetRowCellValue(handle, colSku, diccionario["unitMeasureID"]);
                gridViewValues.SetRowCellValue(handle, colQuantity,1);
                gridViewValues.SetRowCellValue(handle, colPrice, diccionario["Precio"]);
                gridViewValues.SetRowCellValue(handle, colSubTotal, 1 * Convert.ToInt32( diccionario["Precio"]) );
                gridViewValues.SetRowCellValue(handle, colIva, 0);
                gridViewValues.SetRowCellValue(handle, colSkuQuantityBySku, 1 );
                gridViewValues.SetRowCellValue(handle, colUnitaryPriceIndividual, Convert.ToInt32(diccionario["Precio"] ));
                gridViewValues.SetRowCellValue(handle, colAccion, "");
                gridViewValues.SetRowCellValue(handle, colSkuFormatoDescripton, "");
                gridViewValues.SetRowCellValue(handle, colItemPrecio2, diccionario["Precio2"]);
                gridViewValues.SetRowCellValue(handle, colItemPrecio3, diccionario["Precio3"]);
                gridViewValues.UpdateCurrentRow();

            }		
		
		    FnGetConcept(itemID, "IVA");		
        }

        #endregion



        #region Eventos Formulario


        private void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            if (ObjComponentItem is null)
            {
                return;
            }

            var formTypeListSearch = new FormTypeListSearch("Lista de Cliente", ObjComponentItem.ComponentId,
                "SELECCIONAR_BILLING_REGISTER", true,
                "{warehouseID:4,listPriceID:12,typePriceID:154,currencyID:1}", false, "", 0, 5, "");
            formTypeListSearch.EventoCallBackAceptar_ += EventoCallBackAceptarCusomter;
            formTypeListSearch.ShowDialog(this);
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            
            var warehouseID_ = Convert.ToInt32(txtWarehouseID.EditValue);
            var listPrice_ = ObjListPrice!.ListPriceId;
            var typePrice_ = Convert.ToInt32(txtTypePriceID.EditValue);
            var currencyID_ = Convert.ToInt32(txtCurrencyID.EditValue);


            var formTypeListSearch = new FormTypeListSearch("Lista de Productos", 33,
                "SELECCIONAR_ITEM_BILLING_POPUP_INVOICE", true,
                 @"{warehouseID:" + warehouseID_ + ", listPriceID:" + listPrice_ + ",typePriceID:" + typePrice_ + ",currencyID:" + currencyID_ + "}", 
                 false, "", 0, 5, "");
            formTypeListSearch.EventoCallBackAceptar_ += EventoCallBackAceptarItem;
            formTypeListSearch.ShowDialog(this);
        }

        private void EventoCallBackAceptarCusomter(dynamic mensaje)
        {
            // Realizar la lógica que desees en el formulario padre
            WebToolsHelper objWebToolsHelper = new WebToolsHelper();
            MessageBox.Show("Evento en el formulario hijo: " +
                            objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "itemID", "0"));
        }


        private void EventoCallBackAceptarItem(dynamic mensaje)
        {
            // Realizar la lógica que desees en el formulario padre
            WebToolsHelper objWebToolsHelper = new WebToolsHelper();
            MessageBox.Show("Evento en el formulario hijo: " +
                            objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "itemID", "0"));
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

        private void txtScanerCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.M)
            {
                // La tecla presionada es la letra "M"
                // Aquí puedes agregar la lógica que deseas ejecutar
                ComandPrinter();

            }
            if (e.KeyCode == Keys.K)
            {
                // La tecla presionada es la letra "M"
                // Aquí puedes agregar la lógica que deseas ejecutar
                LoadNew();
                LoadRender(TypeRender.New);

            }
            if (e.KeyCode == Keys.I)
            {
                OpenCashbox();

            }

        }

        private void txtScanerCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter)
            {
                return;
            }

            var currencyID_ = Convert.ToInt32(txtCurrencyID.EditValue);
            var codigoABuscar = txtScanerCodigo.Text.ToUpper();
            txtScanerCodigo.Text = "";


            //++Abrir popup de productos
            if (codigoABuscar == "++")
            {
                FnCreateTableSearchProductos();
                return;
            }

            if (codigoABuscar == "")
            {
                txtReceiptAmount.Focus();
            }


            if (ObjSELECCIONAR_ITEM_BILLING_BACKGROUND is null)
                return;


            bool encontrado = false;
            int i           = 0;
            for ( i = 0; i < ObjSELECCIONAR_ITEM_BILLING_BACKGROUND.Rows.Count; i++ )
            {
                if (encontrado == true)
                {
                    i--;
                    break;
                }

                //buscar por codigo de sistema					
                var currencyTemp = Convert.ToInt32( ObjSELECCIONAR_ITEM_BILLING_BACKGROUND.Rows[i]["currencyID"]);
                var currencyID = Convert.ToInt32( txtCurrencyID.EditValue);

                var warehouseIDTemp = Convert.ToInt32(ObjSELECCIONAR_ITEM_BILLING_BACKGROUND.Rows[i]["warehouseID"]);
                var warehouseID = Convert.ToInt32(txtWarehouseID.EditValue);
                var codigoTable = ObjSELECCIONAR_ITEM_BILLING_BACKGROUND.Rows[i]["Codigo"].ToString();

                if (
                    currencyID == currencyTemp &&
                    FnDeleteCerosIzquierdos(codigoABuscar) == FnDeleteCerosIzquierdos(codigoTable!.Replace("BITT", "").Replace("ITT", "").ToUpper()) &&
                    warehouseID == warehouseIDTemp
                )
                {
                    encontrado = true;
                    break;
                }


                //buscar por codigo de barra
                var listCodigTmp = ObjSELECCIONAR_ITEM_BILLING_BACKGROUND.Rows[i]["Barra"].ToString();
                currencyTemp = Convert.ToInt32(ObjSELECCIONAR_ITEM_BILLING_BACKGROUND.Rows[i]["currencyID"]);
                currencyID = Convert.ToInt32(txtCurrencyID.EditValue);
                encontrado = false;

                if (encontrado == false)
                {
                    for (var ii = 0; ii < listCodigTmp!.Length; ii++)
                    {
                        if (
                            FnDeleteCerosIzquierdos(listCodigTmp[ii].ToString().ToUpper()) == FnDeleteCerosIzquierdos(codigoABuscar) &&
                            currencyID == currencyTemp &&
                            warehouseID == warehouseIDTemp
                        )
                        {
                            encontrado = true;
                            break;
                        }
                    }
                }

            }

            if (encontrado == true)
            {
                var sumar                               = true;
                var filterResult                        = ObjSELECCIONAR_ITEM_BILLING_BACKGROUND.Rows[i];
                Dictionary<string, string> diccionario  = new Dictionary<string, string>();

                diccionario.Add("itemID", filterResult["itemID"].ToString()!);
                diccionario.Add("Codigo", filterResult["Codigo"].ToString()!);
                diccionario.Add("Nombre", filterResult["Nombre"].ToString()!);
                diccionario.Add("Medida", filterResult["Medida"].ToString()!);
                diccionario.Add("Cantidad", filterResult["Cantidad"].ToString()!);
                diccionario.Add("unitMeasureID", filterResult["unitMeasureID"].ToString()!);
                diccionario.Add("Nombre", filterResult["Nombre"].ToString()!);
                diccionario.Add("Precio", filterResult["Precio"].ToString()!);
                diccionario.Add("Precio2", filterResult["Precio2"].ToString()!);
                diccionario.Add("Precio3", filterResult["Precio3"].ToString()!);

                //Agregar el Item a la Fila
                FnOnCompleteNewItem(diccionario, sumar);
            }




        }

        private void downButtonProducto_Click(object sender, EventArgs e)
        {

        }

        private void barManager1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item.Name == btnActualizarCatalogo.Name )
            {
                var formInvoiceApi = new FormInvoiceApi();
                var warehouseID_ = Convert.ToInt32(txtWarehouseID.EditValue);
                var listPrice_ = ObjListPrice!.ListPriceId;
                var typePrice_ = Convert.ToInt32(txtTypePriceID.EditValue);
                var currencyID_ = Convert.ToInt32(txtCurrencyID.EditValue);

                ObjSELECCIONAR_ITEM_BILLING_BACKGROUND   = 
                        formInvoiceApi.getViewApi(
                            ObjComponentItem!.ComponentId, 
                            @"SELECCIONAR_ITEM_BILLING_BACKGROUND", 
                            @"{warehouseID:"+ warehouseID_+", listPriceID:"+listPrice_+",typePriceID:" + typePrice_ + ",currencyID:"+ currencyID_ + "}"
                );


            }
        }
    }
}