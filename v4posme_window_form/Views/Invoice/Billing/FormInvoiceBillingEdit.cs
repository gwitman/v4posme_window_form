using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using DevExpress.Utils;
using DevExpress.Utils.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraBars.InternalItems;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ESC_POS_USB_NET.Printer;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomHelper;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Models;
using v4posme_library.ModelsDto;
using v4posme_window.Api;
using v4posme_window.Dto;
using v4posme_window.Interfaz;
using v4posme_window.Libraries;
using v4posme_window.Template;
using ComboBoxItem = v4posme_window.Libraries.ComboBoxItem;
using Exception = System.Exception;
using Image = System.Drawing.Image;

namespace v4posme_window.Views.Invoice.Billing
{
    public partial class FormInvoiceBillingEdit : XtraForm, IFormTypeEdit
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

        private readonly IPublicCatalogModel _objInterfazPublicCatalogModel = VariablesGlobales.Instance.UnityContainer.Resolve<IPublicCatalogModel>();

        private readonly IPublicCatalogDetailModel _objInterfazPublicCatalogDetailModel = VariablesGlobales.Instance.UnityContainer.Resolve<IPublicCatalogDetailModel>();

        private readonly IItemModel _objInterfazItemModel = VariablesGlobales.Instance.UnityContainer.Resolve<IItemModel>();

        private readonly ICatalogItemModel _objInterfazCatalogItemModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICatalogItemModel>();

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

        private readonly ITransactionMasterDetailReferencesModel _objInterfazTransactionMasterDetailReferencesModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterDetailReferencesModel>();

        private readonly ITransactionCausalModel _objInterfazTransactionCausalModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionCausalModel>();

        private readonly ITransactionMasterModel _objInterfazTransactionMasterModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterModel>();

        private readonly ITransactionMasterInfoModel _objInterfazTransactionMasterInfoModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterInfoModel>();

        private readonly ITransactionMasterDetailModel _objInterfazTransactionMasterDetailModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterDetailModel>();

        private readonly ITransactionMasterConceptModel _objInterfazTransactionMasterConceptModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterConceptModel>();

        private readonly ITransactionMasterReferencesModel _objInterfazTransactionMasterReferencesModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterReferencesModel>();

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

        private BindingList<FormInvoiceBillingEditDetailDTO> _bindingListTransactionMasterDetail = new BindingList<FormInvoiceBillingEditDetailDTO>();
        private string? ObjCompanyParameter_Key_INVOICE_VALIDATE_BALANCE { get; set; }
        private string? objCompanyParameter_Key_INVOICE_BILLING_CREDIT { get; set; }

        private bool varPermisosEsPermitidoModificarPrecio;
        private bool varPermisosEsPermitidoModificarNombre;
        private bool varPermisosEsPermitidoSeleccionarPrecioPublico;
        private bool varPermisosEsPermitidoSeleccionarPrecioMayor;
        private bool varPermisosEsPermitidoSeleccionarPrecioCredito;
        public List<TbMenuElement>? ObjListPermisos { get; set; }
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
        public List<TbTransactionCausal> ObjCausal { get; private set; }
        public int? WarehouseId { get; private set; }
        public List<TbUserWarehouseDto> ObjListWarehouse { get; private set; }
        public TbCustomer? ObjCustomerDefault { get; private set; }
        public List<TbCatalogItem> ObjListTypePrice { get; private set; }
        public List<TbCatalogItem> ObjListPay { get; private set; }

        public List<TbCatalogItem> ObjListDayExcluded { get; private set; }

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

        public string? ObjParameterInvoiceTypeEmployer { get; private set; }
        public string? ObjParameterInvoiceBillingEmployeeDefault { get; private set; }
        public string? ObjParameterInvoiceUpdateNameInTransactionOnly { get; private set; }
        public string? ObjParameterobjParameterInvoiceBillingPrinterUrlBar { get; private set; }

        public string? ObjParameterInvoiceBillingPrinterDirectUrlBar { get; private set; }

        public string? ObjParameterInvoiceBillingPrinterDirectNameDefaultBar { get; private set; }

        public string? ObjParameterInvoiceBillingShowCommandBar { get; private set; }

        public string? ObjParameterInvoiceBillingApplyTypePriceOnDayPorMayor { get; private set; }

        public string? ObjParameterCustomPopupFacturacion { get; private set; }

        public string? ObjParameterCxcFrecuenciaPayDefault { get; private set; }

        public string? objParameterCXC_DAY_EXCLUDED_IN_CREDIT { get; private set; }

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

        public TbTransactionMasterDetailCredit? ObjTransactionMasterDetailCredit { get; set; }

        public string? ObjParameterInvoiceBillingPrinterDirectNameDefaDefaultBar { get; set; }

        public string? ObjParameterInvoiceBillingPrinterUrlBar { get; set; }

        public string? ObjParameterInvoiceBillingPrinterDirectUrlPrinterDirectUrlBar { get; set; }

        public string? ObjParameterTipoPrinterDonwload { get; set; }

        public string? ObjParameterInvoiceOpenCashPassword { get; set; }

        public string? ObjParameterInvoiceOpenCashWhenPrinterInvoice { get; set; }


        public List<TbCompanyComponentConcept> ObjTransactionMasterDetailConcept { get; set; }

        public List<TbTransactionMasterDetailDto> ObjTransactionMasterDetailWarehouse { get; set; }

        public List<TbTransactionMasterDetailDto> ObjTransactionMasterDetail { get; set; }

        public TbTransactionMasterReference? ObjTransactionMasterReferences { get; set; }

        public TbTransactionMasterInfoDto? ObjTransactionMasterInfo { get; set; } = new();

        public TbTransactionMasterDto? ObjTransactionMaster { get; set; }

        public string? UrlPrinterDocument { get; set; }

        public string? ObjParameterInvoiceButtomPrinterFidLocalPaymentAndAmortization { get; set; }

        public string? UrlPrinterDocumentCocinaDirect { get; set; }

        public string? UrlPrinterDocumentCocina { get; set; }

        public string? ObjParameterShowComandoDeCocina { get; set; }

        public string? ObjParameterINVOICE_BILLING_VALIDATE_EXONERATION { get; set; }

        public string? ObjParameterInvoiceBillingPrinterDirectUrl { get; set; }

        public string? ObjParameterInvoiceBillingPrinterDirect { get; set; }
        private DataTable? ObjSELECCIONAR_ITEM_BILLING_BACKGROUND { get; set; }

        private FormInvoiceBillingEditPaymentDialog FormInvoiceBillingEditPayment;

        #endregion

        #region Variables internas

        private string CodigoMesero { get; set; }
        private int? CompanyId { get; set; }
        private int? TransactionId { get; set; }
        private int? TransactionMasterId { get; set; }
        private TypeOpenForm TypeOpen { get; set; }
        private int TxtCustomerId { get; set; }
        private int? TxtStatusOldId { get; set; }
        private int? TxtStatusId { get; set; }

        private const string FormatDecimal = "N2";
        private BackgroundWorker? _backgroundWorker = null;

        #endregion

        #region Init

        public FormInvoiceBillingEdit()
        {
            InitializeComponent();
            FormInvoiceBillingEditPayment = new(this);
            SetTabOrder();
        }

        public FormInvoiceBillingEdit(TypeOpenForm typeOpen, int companyId, int transactionId, int transactionMasterId, string codeMesero)
        {
            InitializeComponent();
            SetTabOrder();
            // Suscribir al manejador de excepciones global
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            CompanyId = companyId;
            TransactionId = transactionId;
            TransactionMasterId = transactionMasterId;
            TypeOpen = typeOpen;
            CodigoMesero = codeMesero;
            FormInvoiceBillingEditPayment = new(this);
            FormInvoiceBillingEditPayment.SetTabOrder();
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
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += (ob, ev) =>
            {
                if (TypeOpen == TypeOpenForm.Init && TransactionMasterId > 0)
                {
                    LoadEdit();
                }

                if (TypeOpen == TypeOpenForm.Init && TransactionMasterId == 0)
                {
                    LoadNew();
                }
            };

            _backgroundWorker.RunWorkerCompleted += (ob, ev) =>
            {
                if (ev.Error is not null)
                {
                    CustomException.LogException(ev.Error);
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

                    if (TypeOpen == TypeOpenForm.Init && TransactionMasterId > 0)
                    {
                        LoadRender(TypeRender.Edit);
                    }

                    if (TypeOpen == TypeOpenForm.Init && TransactionMasterId == 0)
                    {
                        LoadRender(TypeRender.New);
                    }

                    if (progressPanel.Visible)
                    {
                        progressPanel.Visible = false;
                    }
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

        private void SetTabOrder()
        {
            var controls = new Control[]
            {
                txtScanerCodigo,
                gridViewTbTransactionMasterDetail,
                txtDate,
                txtNote,
                btnSearchCustomer,
                txtReferenceClientName,
                txtReferenceClientIdentifier,
                txtCausalID,
                txtCustomerCreditLineID,
                txtCurrencyID
            };
            var controlCount = controls.Length;
            ResetTabIndex(this, ref controlCount);

            for (var i = 0; i < controls.Length; i++)
            {
                controls[i].TabIndex = i;
                controls[i].TabStop = true;
            }
        }

        private void ResetTabIndex(Control parent, ref int controlCount)
        {
            foreach (Control control in parent.Controls)
            {
                control.TabIndex = controlCount;
                controlCount++;
                control.TabStop = false;

                if (control.HasChildren)
                {
                    ResetTabIndex(control, ref controlCount);
                }
            }
        }

        private void GetTabIndexInfo(Control parent, StringBuilder tabIndexInfo, ref int controlCount)
        {
            foreach (Control control in parent.Controls)
            {
                tabIndexInfo.AppendLine($"{controlCount} Control: {control.Name}, TabIndex: {control.TabIndex}");
                controlCount++;

                if (control.HasChildren)
                {
                    GetTabIndexInfo(control, tabIndexInfo, ref controlCount);
                }
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
            if (resultPermission == permissionNone && (objTm.CreatedBy != objUser.UserID))
            {
                throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_DELETE"]);
            }

            if (_objInterfazCoreWebAccounting.CycleIsCloseByDate(objUser.CompanyID, objTm.TransactionOn!.Value))
            {
                throw new Exception("EL DOCUMENTO NO PUEDE SE ELIMINADO, EL CICLO CONTABLE ESTA CERRADO");
            }

            var workflowStage = _objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_billing",
                "statusID", objTm.StatusId!.Value, commandEliminable, objUser.CompanyID,
                objUser.BranchID, objRole!.RoleID);
            if (!workflowStage!.Value)
            {
                throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_WORKFLOW_DELETE"]);
            }

            ////Validar si la factura es de credito y esta aplicada y tiene abono	
            var parameterCausalTypeCredit =
                _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_CREDIT", objUser.CompanyID);
            var causalIdTypeCredit = parameterCausalTypeCredit!.Value.Split(",");
            var exisCausalInCredit =
                causalIdTypeCredit.Any(elemento => elemento == objTm.TransactionCausalId.ToString());

            var validateWorkflowStage = _objInterfazCoreWebWorkflow.ValidateWorkflowStage(
                "tb_transaction_master_billing", "statusID", objTm.StatusId!.Value, commandAplicable,
                objUser.CompanyID, objUser.BranchID, objRole.RoleID
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
                    u.CompanyID == objTm.CompanyId && u.TransactionID == objTm.TransactionId &&
                    u.TransactionMasterID == objTm.TransactionMasterId);
                dataNewTm.StatusIDChangeOn = DateTime.Now;
                _objInterfazTransactionMasterModel.UpdateAppPosme(dataNewTm.CompanyID, dataNewTm.TransactionID,
                    dataNewTm.TransactionMasterID, dataNewTm);

                //Ejecutar el procedimiento de reversion
                var transactionIdRevert = _objInterfazCoreWebParameter.GetParameter("INVOICE_TRANSACTION_REVERSION_TO_BILLING",
                    objUser.CompanyID);
                var transactionIdRevertValue = Convert.ToInt32(transactionIdRevert!.Value);
                _objInterfazCoreWebTransaction.CreateInverseDocumentByTransaccion(objTm.CompanyId, objTm.TransactionId, objTm.TransactionMasterId, transactionIdRevertValue, 0);


                if (exisCausalInCredit)
                {
                    //Valores de tasa de cambio          
                    var objCurrencyDolares = _objInterfazCoreWebCurrency.GetCurrencyExternal(objTm.CompanyId);
                    var objCurrencyCordoba = _objInterfazCoreWebCurrency.GetCurrencyDefault(objTm.CompanyId);
                    var dateOn = DateTime.Now.Date;
                    var exchangeRate = _objInterfazCoreWebCurrency.GetRatio(objTm.CompanyId, dateOn, 1, objCurrencyDolares!.CurrencyID, objCurrencyCordoba!.CurrencyID);

                    //cancelar el documento de credito					
                    var shareDocumentAnuladoStatusID = Convert.ToInt32(_objInterfazCoreWebParameter.GetParameter("SHARE_DOCUMENT_ANULADO", objUser!.CompanyID)!.Value);

                    var objCustomerCreditDocumentNew = dataContext.TbCustomerCreditDocuments.Where(
                        c => c.CustomerCreditDocumentID ==
                             objCustomerCreditDocument!.CustomerCreditDocumentId!.Value).FirstOrDefault();

                    objCustomerCreditDocumentNew!.StatusID = shareDocumentAnuladoStatusID;
                    _objInterfazCustomerCreditDocument.UpdateAppPosme(objCustomerCreditDocument!.CustomerCreditDocumentId!.Value, objCustomerCreditDocumentNew);

                    var amountDol = objCustomerCreditDocument.Balance / exchangeRate;
                    var amountCor = objCustomerCreditDocument.Balance;


                    //aumentar el blance de la linea
                    var tbCustomerCreditLine =
                        _objInterfazCustomerCreditLine.GetRowByPk(objCustomerCreditDocument.CustomerCreditLineId);
                    tbCustomerCreditLine.Balance += (tbCustomerCreditLine.CurrencyID ==
                                                     objCurrencyDolares.CurrencyID
                        ? amountDol!.Value
                        : amountCor!.Value);
                    _objInterfazCustomerCreditLine.UpdateAppPosme(objCustomerCreditDocument.CustomerCreditLineId, tbCustomerCreditLine);

                    //aumentar el balance de credito
                    var objCustomerCredit = _objInterfazCustomerCredit.GetRowByPk(objTm.CompanyId, objTm.BranchId!.Value, objTm.EntityId!.Value);
                    objCustomerCredit.BalanceDol = objCustomerCredit.BalanceDol + amountDol!.Value;
                    _objInterfazCustomerCredit.UpdateAppPosme(objCustomerCredit.CompanyID, objCustomerCredit.BranchID, objCustomerCredit.EntityID, objCustomerCredit);
                }
            }
            else
            {
                //	//Eliminar el Registro			
                _objInterfazTransactionMasterModel.DeleteAppPosme(objUser.CompanyID, objTm.TransactionId, objTm.TransactionMasterId);
                _objInterfazTransactionMasterDetailModel.DeleteWhereTm(objUser.CompanyID, objTm.TransactionId, objTm.TransactionMasterId);
            }
        }

        public void ComandPrinter()
        {
            var objCompany = VariablesGlobales.Instance.Company;
            if (objCompany.FlavorID == 577 /*Rosie Collection*/)
            {
                ComandPrinterRosies();
            }
            else
            {
                ComandPrinterDefault();
            }
        }

        public void ComandPrinterDefault()
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
                var companyID = user.CompanyID;

                //Get Component
                var objComponent = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_company");
                var objParameterCompanyLogo = _objInterfazCoreWebParameter.GetParameter("CORE_COMPANY_LOGO", companyID);
                var objParameterTelefono = _objInterfazCoreWebParameter.GetParameter("CORE_PHONE", companyID);
                var objParameterRuc = _objInterfazCoreWebParameter.GetParameter("CORE_COMPANY_IDENTIFIER", companyID)!.Value;
                var objCompany = VariablesGlobales.Instance.Company;
                var spacing = 0.5;

                // Obtener datos del documento
                var objTm = _objInterfazTransactionMasterModel.GetRowByPk(companyID, transactionID, transactionMasterID) ?? throw new ArgumentNullException("_objInterfazTransactionMasterModel.GetRowByPk(companyID, transactionID, transactionMasterID)");
                var objTmi = _objInterfazTransactionMasterInfoModel.GetRowByPk(companyID, transactionID, transactionMasterID);
                var objTmd = _objInterfazTransactionMasterDetailModel.GetRowByTransaction(companyID, transactionID, transactionMasterID);
                var objTc = _objInterfazTransactionCausalModel.GetByCompanyAndTransactionAndCausal(companyID, transactionID, objTm.TransactionCausalId!.Value);
                if (objTmd is null || objCompany is null)
                {
                    return;
                }

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
                var exchangeSale = WebToolsHelper.ConvertToNumber<decimal>(_objInterfazCoreWebParameter.GetParameter("ACCOUNTING_EXCHANGE_SALE", companyID)!.Value);
                var tipoCambio = Math.Round(objTm.ExchangeRate!.Value + exchangeSale, 2);

                // Obtener información del usuario que creó la transacción
                var objUserCreated = VariablesGlobales.Instance.UnityContainer.Resolve<IUserModel>().GetRowByPk(companyID, objTm.CreatedAt!.Value, objTm.CreatedBy!.Value);

                // Imprimir el documento               
                var printerName = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_PRINTER_DIRECT_NAME_DEFAULT", companyID);
                var pathOfLogo = VariablesGlobales.ConfigurationBuilder["PATH_FILE_OF_APP_ROOT"];
                var printer = new Printer(printerName!.Value);

                printer.AlignCenter();
                if (objParameterCompanyLogo is not null)
                {
                    objParameterCompanyLogo.Value = "direct-ticket-" + objParameterCompanyLogo.Value;
                    var imagePath = $"{pathOfLogo}/img/logos/{objParameterCompanyLogo.Value!}";
                    if (File.Exists(imagePath))
                    {
                        // Define el tamaño fijo deseado                        
                        using var originalImage = Image.FromFile(imagePath);
                        using var resizedImage = new Bitmap(originalImage);
                        printer.Append(HelperMethods.Print(resizedImage, 500 /*entre mas pequeño, la imagen es mas pequeña*/));
                    }
                }

                printer.AlignCenter();
                printer.Append(objCompany.Name);
                printer.Append($"RUC: {objParameterRuc}");
                printer.BoldMode("FACTURA");
                printer.Append(objTm.TransactionNumber);
                printer.Append($"FECHA: {objTm.CreatedOn.Value:yyyy-M-d hh:mm:ss tt} ");
                printer.Separator();
                printer.AlignLeft();
                var datos = $"""
                             VENDEDOR: {objUserCreated.Nickname}
                             CODIGO:   {objCustomer.CustomerNumber}
                             TIPO:     {objTipo.Name}
                             ESTADO:   {objStage.First().Name}
                             MONEDA:   {objCurrency.Name}
                             """;
                printer.Append(datos);
                printer.Append("CLIENTE");
                printer.Append($"{objCustomer.FirstName} {objCustomer.LastName}");
                printer.Append(" ");
                printer.Append($"{objTm.Note}");
                printer.Append(" ");
                printer.Append("PRODUCTO               CANT            TOTAL");
                //printer.CondensedMode(PrinterModeState.On);
                foreach (var detailDto in objTmd)
                {
                    printer.AlignLeft();
                    printer.Append($"{detailDto.ItemNameLog}");

                    printer.AlignRight();
                    var subTotal = detailDto.Quantity * detailDto.UnitaryAmount;
                    var detail = $"{detailDto.Quantity!.Value:N2}          {subTotal!.Value:C}";
                    printer.Append(detail);
                }

                //printer.CondensedMode(PrinterModeState.Off);
                printer.Separator();
                printer.AlignLeft();
                var totalstring = $"""
                                   TOTAL:      {objTm!.Amount!.Value:C}
                                   RECIBIDO:   {objTmi!.ReceiptAmount!.Value:C}
                                   CAMBIO:     {objTmi.ChangeAmount:C}
                                   """;
                printer.Append(totalstring);
                printer.Separator();


                printer.AlignCenter();
                printer.Append(objCompany.Address);
                printer.Append($"Tel.: {objParameterTelefono!.Value}");
                printer.FullPaperCut();
                printer.PrintDocument();
            }
            catch (Exception e)
            {
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Imprimir", $"Se produjo un error al imprimir, revisar los datos. Error: {e.Message}", this);
            }
        }

        public void ComandPrinterRosies()
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
                var companyID = user.CompanyID;

                //Get Component
                var objComponent = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_company");
                var objParameterCompanyLogo = _objInterfazCoreWebParameter.GetParameter("CORE_COMPANY_LOGO", companyID);
                var objParameterTelefono = _objInterfazCoreWebParameter.GetParameter("CORE_PHONE", companyID);
                var objParameterRuc = _objInterfazCoreWebParameter.GetParameter("CORE_COMPANY_IDENTIFIER", companyID)!.Value;
                var objCompany = VariablesGlobales.Instance.Company;
                var spacing = 0.5;

                // Obtener datos del documento
                var objTm = _objInterfazTransactionMasterModel.GetRowByPk(companyID, transactionID, transactionMasterID) ?? throw new ArgumentNullException("_objInterfazTransactionMasterModel.GetRowByPk(companyID, transactionID, transactionMasterID)");
                var objTmi = _objInterfazTransactionMasterInfoModel.GetRowByPk(companyID, transactionID, transactionMasterID);
                var objTmd = _objInterfazTransactionMasterDetailModel.GetRowByTransaction(companyID, transactionID, transactionMasterID);
                var objTc = _objInterfazTransactionCausalModel.GetByCompanyAndTransactionAndCausal(companyID, transactionID, objTm.TransactionCausalId!.Value);
                if (objTmd is null || objCompany is null)
                {
                    return;
                }

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
                var exchangeSale = WebToolsHelper.ConvertToNumber<decimal>(_objInterfazCoreWebParameter.GetParameter("ACCOUNTING_EXCHANGE_SALE", companyID)!.Value);
                var tipoCambio = Math.Round(objTm.ExchangeRate!.Value + exchangeSale, 2);

                // Obtener información del usuario que creó la transacción
                var objUserCreated = VariablesGlobales.Instance.UnityContainer.Resolve<IUserModel>().GetRowByPk(companyID, objTm.CreatedAt!.Value, objTm.CreatedBy!.Value);

                // Imprimir el documento               
                var printerName = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_PRINTER_DIRECT_NAME_DEFAULT", companyID);
                var pathOfLogo = VariablesGlobales.ConfigurationBuilder["PATH_FILE_OF_APP_ROOT"];
                var printer = new Printer(printerName!.Value);

                printer.AlignCenter();
                if (objParameterCompanyLogo is not null)
                {
                    objParameterCompanyLogo.Value = "direct-ticket-" + objParameterCompanyLogo.Value;
                    var imagePath = $"{pathOfLogo}/img/logos/{objParameterCompanyLogo.Value!}";
                    if (File.Exists(imagePath))
                    {
                        var logoCompany = new Bitmap(Image.FromFile(imagePath));
                        //el logo que se esta mostrando no se redimensiona 
                        //printer.Image(logoCompany);
                    }
                }

                printer.AlignCenter();
                printer.Append(objCompany.Name);
                printer.Append($"RUC: {objParameterRuc}");
                printer.BoldMode("FACTURA");
                printer.Append(objTm.TransactionNumber);
                printer.Append($"FECHA: {objTm.CreatedOn.Value:yyyy-M-d hh:mm:ss tt} ");
                printer.Separator();
                printer.AlignLeft();
                var datos = $"""
                             VENDEDOR:      {objUserCreated.Nickname}
                             CODIGO:        {objCustomer.CustomerNumber}
                             TIPO:          {objTipo.Name}
                             ESTADO:        {objStage.First().Name}
                             MONEDA:        {objCurrency.Name}
                             TIPO DE PAGO:  N/D 
                             """;
                printer.Append(datos);
                printer.Append("CLIENTE");
                printer.Append($"{objCustomer.FirstName} {objCustomer.LastName}");
                printer.Append(" ");
                printer.Append($"{objTm.Note}");
                printer.Append(" ");
                printer.Append("PRODUCTO               CANT            TOTAL");
                //printer.CondensedMode(PrinterModeState.On);
                foreach (var detailDto in objTmd)
                {
                    printer.AlignLeft();
                    printer.Append($"{detailDto.ItemNameLog}");

                    printer.AlignRight();
                    var subTotal = detailDto.Quantity * detailDto.UnitaryAmount;
                    var detail = $"{detailDto.Quantity!.Value:N2}          {subTotal!.Value:C}";
                    printer.Append(detail);
                }

                //printer.CondensedMode(PrinterModeState.Off);
                printer.Separator();
                printer.AlignLeft();
                var totalstring = $"""
                                   TOTAL:      {objTm!.Amount!.Value:C}
                                   RECIBIDO:   {objTmi!.ReceiptAmount!.Value:C}
                                   CAMBIO:     {objTmi.ChangeAmount:C}
                                   """;
                printer.Append(totalstring);
                printer.Separator();


                printer.AlignCenter();
                printer.Append(objCompany.Address);
                printer.Append($"Tel.: {objParameterTelefono!.Value}");
                printer.FullPaperCut();
                printer.PrintDocument();
            }
            catch (Exception e)
            {
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Imprimir", $"Se produjo un error al imprimir, revisar los datos. Error: {e.Message}", this);
            }
        }

        public void LoadEdit()
        {
            //using var tx = new TransactionScope();
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

            _bindingListTransactionMasterDetail.Clear();
            TransactionId = _objInterfazCoreWebTransaction.GetTransactionId(user.CompanyID, "tb_transaction_master_billing", 0)!.Value;
            ObjCurrency = _objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyID);
            ObjCurrencyDolares = _objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyID);
            var customerDefault = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_CLIENTDEFAULT", user.CompanyID);
            ObjListPrice = _objInterfazListPriceModel.GetListPriceToApply(user.CompanyID);
            ObjListCurrency = _objInterfazCompanyCurrencyModel.GetByCompany(user.CompanyID);
            if (ObjListPrice is null)
            {
                throw new Exception("NO EXISTE UNA LISTA DE PRECIO PARA SER APLICADA");
            }


            CodigoMesero = "none";
            int publicCatalogID = 0;
            List<TbPublicCatalog> objPubliCatalogMesasConfig = _objInterfazPublicCatalogModel.GetBySystemNameAndFlavorID("tb_transaction_master_billing.mesas_x_meseros", VariablesGlobales.Instance.Company!.FlavorID);
            if (CodigoMesero != "none")
            {
                if (objPubliCatalogMesasConfig is null)
                    throw new Exception("CONFIGURAR EL CATALOGO DE MESAS tb_transaction_master_billing.mesas_x_meseros");

                if (objPubliCatalogMesasConfig.Count == 0)
                    throw new Exception("CONFIGURAR EL CATALOGO DE MESAS tb_transaction_master_billing.mesas_x_meseros");
            }

            publicCatalogID = CodigoMesero == "none" ? 0 : objPubliCatalogMesasConfig.ElementAt(0).PublicCatalogID;
            List<TbPublicCatalogDetail> objPubliCatalogDetailMesasConfiguradas = _objInterfazPublicCatalogDetailModel.GetRowByCatalogIDAndName(publicCatalogID, CodigoMesero);


            ObjParameterInvoiceTypeEmployer = _objInterfazCoreWebParameter.GetParameter("INVOICE_TYPE_EMPLOYEER", user.CompanyID)!.Value;
            ObjParameterInvoiceBillingEmployeeDefault = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_EMPLOYEE_DEFAULT", user.CompanyID)!.Value;

            var parameterValue = _objInterfazCoreWebParameter.GetParameter("INVOICE_BUTTOM_PRINTER_FIDLOCAL_PAYMENT_AND_AMORTIZACION", user.CompanyID);
            ObjParameterInvoiceButtomPrinterFidLocalPaymentAndAmortization = parameterValue!.Value;

            ObjParameterInvoiceBillingQuantityZero = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_QUANTITY_ZERO", user.CompanyID)!.Value;
            ObjParameterInvoiceBillingPrinterDirect = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_PRINTER_DIRECT", user.CompanyID)!.Value;
            ObjParameterInvoiceBillingPrinterDirectUrl = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_PRINTER_DIRECT_URL", user.CompanyID)!.Value;
            ObjParameterShowComandoDeCocina = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_SHOW_COMMAND_FOOT", user.CompanyID)!.Value;
            UrlPrinterDocumentCocina = _objInterfazCoreWebParameter.GetParameter("INVOICE_URL_PRINTER_COCINA", user.CompanyID)!.Value;
            UrlPrinterDocumentCocinaDirect = _objInterfazCoreWebParameter.GetParameter("INVOICE_URL_PRINTER_COCINA_DIRECT", user.CompanyID)!.Value;
            ObjParameterImprimirPorCadaFactura = _objInterfazCoreWebParameter.GetParameter("INVOICE_PRINT_BY_INVOICE", user.CompanyID)!.Value;
            ObjParameterRegresarAListaDespuesDeGuardar = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_SAVE_AFTER_TO_LIST", user.CompanyID)!.Value;
            ObjParameterScanerProducto = _objInterfazCoreWebParameter.GetParameter("INVOICE_SHOW_POPUP_FIND_PRODUCTO_NOT_SCANER", user.CompanyID)!.Value;
            ObjParameterCantidadItemPoup = _objInterfazCoreWebParameter.GetParameter("INVOICE_CANTIDAD_ITEM", user.CompanyID)!.Value;
            ObjParameterHidenFiledItemNumber = _objInterfazCoreWebParameter.GetParameter("INVOICE_HIDEN_ITEMNUMBER_IN_POPUP", user.CompanyID)!.Value;
            ObjParameterAmortizationDuranteFactura = _objInterfazCoreWebParameter.GetParameter("INVOICE_PARAMTER_AMORITZATION_DURAN_INVOICE", user.CompanyID)!.Value;
            ObjParameterAlturaDelModalDeSeleccionProducto = _objInterfazCoreWebParameter.GetParameter("INVOICE_ALTO_MODAL_DE_SELECCION_DE_PRODUCTO_AL_FACTURAR", user.CompanyID)!.Value;
            ObjParameterScrollDelModalDeSeleccionProducto = _objInterfazCoreWebParameter.GetParameter("INVOICE_SCROLL_DE_MODAL_EN_SELECCION_DE_PRODUTO_AL_FACTURAR", user.CompanyID)!.Value;
            ObjParameterMostrarImagenEnSeleccion = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_SHOW_IMAGE_IN_DETAIL_SELECTION", user.CompanyID)!.Value;
            ObjParameterPantallaParaFacturar = _objInterfazCoreWebParameter.GetParameter("INVOICE_PANTALLA_FACTURACION", user.CompanyID)!.Value;
            UrlPrinterDocument = _objInterfazCoreWebParameter.GetParameter("INVOICE_URL_PRINTER", user.CompanyID)!.Value;
            ObjCompanyParameter_Key_INVOICE_VALIDATE_BALANCE = _objInterfazCoreWebParameter.GetParameter("INVOICE_VALIDATE_BALANCE", user.CompanyID)!.Value;
            objCompanyParameter_Key_INVOICE_BILLING_CREDIT = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_CREDIT", user.CompanyID)!.Value;
            objParameterCXC_DAY_EXCLUDED_IN_CREDIT = _objInterfazCoreWebParameter.GetParameterValue("CXC_DAY_EXCLUDED_IN_CREDIT", user.CompanyID);
            ObjTransactionMaster = _objInterfazTransactionMasterModel.GetRowByPk(user.CompanyID, TransactionId!.Value, TransactionMasterId!.Value);
            ObjParameterINVOICE_BILLING_VALIDATE_EXONERATION = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_VALIDATE_EXONERATION", user.CompanyID)!.Value;

            if (ObjTransactionMaster is null)
            {
                throw new Exception("No existe el ObjTransactionMaster");
            }

            ObjTransactionMasterReferences = _objInterfazTransactionMasterReferencesModel.GetRowByTransactionMasterId(TransactionMasterId.Value);
            ObjTransactionMasterInfo = _objInterfazTransactionMasterInfoModel.GetRowByPk(user.CompanyID, TransactionId!.Value, TransactionMasterId!.Value);
            ObjTransactionMasterDetail = _objInterfazTransactionMasterDetailModel.GetRowByTransaction(user.CompanyID, TransactionId!.Value, TransactionMasterId!.Value);
            ObjTransactionMasterDetailWarehouse = _objInterfazTransactionMasterDetailModel.GetRowByTransactionAndWarehouse(user.CompanyID, TransactionId!.Value, TransactionMasterId!.Value);
            ObjTransactionMasterDetailConcept = _objInterfazTransactionMasterConceptModel.GetRowByTransactionMasterConcept(user.CompanyID, TransactionId!.Value, TransactionMasterId!.Value, ObjComponentItem.ComponentID);
            var dateTimeNow = DateTime.Now;
            var dateRatio = DateTime.Now.Date;
            ExchangeRate = _objInterfazCoreWebCurrency.GetRatio(user.CompanyID, dateRatio, decimal.One, ObjCurrencyDolares!.CurrencyID, ObjCurrency!.CurrencyID);
            ObjListEmployee = _objInterfazEmployeeModel.GetRowByBranchIdAndType(user.CompanyID, user.BranchID, Convert.ToInt32(ObjParameterInvoiceTypeEmployer));
            ObjListBank = _objInterfazBankModel.GetByCompany(user.CompanyID);
            ObjCausal = _objInterfazTransactionCausalModel!.GetCausalByBranch(user!.CompanyID, TransactionId!.Value, user!.BranchID);
            WarehouseId = ObjCausal.First()!.WarehouseSourceID;
            ObjListWarehouse = _objInterfazUserWarehouseModel.GetRowByUserIdAndFacturable(user.CompanyID, user.UserID);
            ObjCustomerDefault = _objInterfazCustomerModel.GetRowByPKK(ObjTransactionMaster!.EntityId!.Value);
            ObjListTypePrice = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_price", "typePriceID", user.CompanyID);
            ObjListZone = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_transaction_master_info_billing", "zoneID", user.CompanyID);
            ObjListMesa = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_transaction_master_info_billing", "mesaID", user.CompanyID);

            //Filtrar la Lista de mesas
            var obListMesasFiltradas = ObjListMesa.Where(item1 => objPubliCatalogDetailMesasConfiguradas.Any(item2 => item2.Display == item1.Name)).ToList();
            ObjListMesa = CodigoMesero == "none" ? ObjListMesa : obListMesasFiltradas;

            if (ObjListMesa is null)
                throw new Exception("No se puede avanzar configurar catalogo de MESS");

            if (ObjListMesa.Count == 0)
                throw new Exception("No se puede avanzar configurar catalogo de MESS");

            if (!ObjListMesa.Any(u => u.CatalogItemID == ObjTransactionMasterInfo!.MesaId))
            {
                throw new Exception("No tiene acceso al catalogo MESS");
            }


            ObjListPay = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer_credit_line", "periodPay", user.CompanyID);
            ObjListDayExcluded = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer_credit_line", "dayExcluded", user.CompanyID);
            ListProvider = _objInterfazProviderModel.GetRowByCompany(user.CompanyID);
            ObjListWorkflowStage = _objInterfazCoreWebWorkflow.GetWorkflowStageByStageInit("tb_transaction_master_billing", "statusID", ObjTransactionMaster!.StatusId!.Value, role!.CompanyID, role.BranchID, role.RoleID);

            ObjParameterInvoiceOpenCashWhenPrinterInvoice = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_OPEN_CASH_WHEN_PRINTER_INVOICE", user.CompanyID);
            ObjParameterInvoiceOpenCashPassword = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_OPEN_CASH_PASSWORD", user.CompanyID);
            ObjParameterCustomPopupFacturacion = _objInterfazCoreWebParameter.GetParameterValue("CORE_VIEW_CUSTOM_PANTALLA_DE_FACTURACION_POPUP_SELECCION_PRODUCTO_FORMA_MOSTRAR", user.CompanyID);
            ObjParameterTipoPrinterDonwload = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_PRINTER_DOWNLOAD", user.CompanyID);
            ObjParameterInvoiceBillingApplyTypePriceOnDayPorMayor = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_APPLY_TYPE_PRICE_ON_DAY_POR_MAYOR", user.CompanyID);
            ObjParameterInvoiceBillingShowCommandBar = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_SHOW_COMMAND_BAR", user.CompanyID);
            ObjParameterInvoiceBillingPrinterDirectNameDefaDefaultBar = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_PRINTER_DIRECT_NAME_DEFAULT_BAR", user.CompanyID);
            ObjParameterInvoiceBillingPrinterDirectUrlPrinterDirectUrlBar = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_PRINTER_DIRECT_URL_BAR", user.CompanyID);
            ObjParameterInvoiceBillingPrinterUrlBar = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_PRINTER_URL_BAR", user.CompanyID);
            ObjParameterInvoiceBillingSelectitem = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_SELECTITEM", user.CompanyID);

            ObjListPermisos = VariablesGlobales.Instance.ListMenuHiddenPopup;
            varPermisosEsPermitidoModificarPrecio = ObjListPermisos!.Count(element => element.Display == "ES_PERMITIDO_MODIFICAR_PRECIO_EN_FACTURACION") > 0;
            varPermisosEsPermitidoModificarNombre = ObjListPermisos!.Count(element => element.Display == "ES_PERMITIDO_MODIFICAR_NOMBRE_EN_FACTURACION") > 0;
            varPermisosEsPermitidoSeleccionarPrecioPublico = ObjListPermisos!.Count(element => element.Display == "ES_PERMITIDO_SELECCIONAR_PRECIO_PUBLICO") > 0;
            varPermisosEsPermitidoSeleccionarPrecioMayor = ObjListPermisos!.Count(element => element.Display == "ES_PERMITIDO_SELECCIONAR_PRECIO_PORMAYOR") > 0;
            varPermisosEsPermitidoSeleccionarPrecioCredito = ObjListPermisos!.Count(element => element.Display == "ES_PERMITIDO_SELECCIONAR_PRECIO_CREDITO") > 0;

            if (ObjCustomerDefault is null)
            {
                throw new Exception("NO EXISTE EL CLIENTE POR DEFECTO");
            }

            ObjNaturalDefault = _objInterfazNaturalModel.GetRowByPk(user.CompanyID, ObjCustomerDefault.BranchID, ObjCustomerDefault.EntityID);
            ObjLegalDefault = _objInterfazLegalModel.GetRowByPk(user.CompanyID, ObjCustomerDefault.BranchID, ObjCustomerDefault.EntityID);

            //Procesar Datos
            if (ObjTransactionMasterDetail is not null && ObjTransactionMasterDetail.Count > 0)
            {
                foreach (var masterDetailDto in ObjTransactionMasterDetail)
                {
                    ObjTransactionMasterDetailCredit = _objInterfazTransactionMasterDetailCreditModel.GetRowByPk(masterDetailDto.TransactionMasterDetailId);
                }
            }

            //Obtener la linea de credito del cliente por defecto
            ObjCurrencyDolares = _objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyID);
            ObjCurrencyCordoba = _objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyID);
            ParameterCausalTypeCredit = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_CREDIT", user.CompanyID);
            ObjCustomerCreditAmoritizationAll = _objInterfazCustomerCreditAmortizationModel.GetRowByCustomerId(ObjCustomerDefault.EntityID);
            ObjListCustomerCreditLine = _objInterfazCustomerCreditLineModel.GetRowByEntityBalanceMayorCero(user.CompanyID, user.BranchID, this.ObjCustomerDefault.EntityID);

            //Obtener los datos de precio, sku y conceptos de la transaccoin
            ObjTransactionMasterItemPrice = _objInterfazPriceModel.GetRowByTransactionMasterId(user.CompanyID, ObjListPrice.ListPriceID, ObjTransactionMaster.TransactionMasterId);
            ObjTransactionMasterItemConcepto = _objInterfazCompanyComponentConceptModel.GetRowByTransactionMasterId(user.CompanyID, ObjComponentItem.ComponentID, ObjTransactionMaster.TransactionMasterId);
            ObjTransactionMasterItemSku = _objInterfazItemSkuModel.GetRowByTransactionMasterId(user.CompanyID, ObjTransactionMaster.TransactionMasterId);
            ObjTransactionMasterItem = _objInterfazItemModel.GetRowByTransactionMasterId(ObjTransactionMaster.TransactionMasterId);
            //tx.Complete();
        }

        public void LoadNew()
        {
            //using var tx = new TransactionScope();
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

            ObjComponentTransactionBilling =
                _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_transaction_master_billing");
            if (ObjComponentTransactionBilling is null)
            {
                throw new Exception("EL COMPONENTE 'tb_transaction_master_billing' NO EXISTE...");
            }

            TransactionId = _objInterfazCoreWebTransaction.GetTransactionId(user.CompanyID, "tb_transaction_master_billing", 0)!.Value;
            ObjCurrency = _objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyID);
            ObjCurrencyDolares = _objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyID);
            var customerDefault = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_CLIENTDEFAULT", user.CompanyID);
            ObjListPrice = _objInterfazListPriceModel.GetListPriceToApply(user.CompanyID);
            ObjListCurrency = _objInterfazCompanyCurrencyModel.GetByCompany(user.CompanyID);
            if (ObjListPrice is null)
            {
                throw new Exception("NO EXISTE UNA LISTA DE PRECIO PARA SER APLICADA");
            }

            ObjParameterInvoiceTypeEmployer = _objInterfazCoreWebParameter.GetParameter("INVOICE_TYPE_EMPLOYEER", user.CompanyID)!.Value;
            ObjParameterInvoiceBillingEmployeeDefault = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_EMPLOYEE_DEFAULT", user.CompanyID)!.Value;
            ObjCompanyParameter_Key_INVOICE_VALIDATE_BALANCE = _objInterfazCoreWebParameter.GetParameter("INVOICE_VALIDATE_BALANCE", user.CompanyID)!.Value;
            objCompanyParameter_Key_INVOICE_BILLING_CREDIT = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_CREDIT", user.CompanyID)!.Value;
            ObjParameterInvoiceAutoApply = _objInterfazCoreWebParameter.GetParameter("INVOICE_AUTOAPPLY_CASH", user.CompanyID)!.Value;
            ObjParameterTypePreiceDefault = _objInterfazCoreWebParameter.GetParameter("INVOICE_DEFAULT_TYPE_PRICE", user.CompanyID)!.Value;
            ObjParameterTipoWarehouseDespacho = _objInterfazCoreWebParameter.GetParameter("INVOICE_TYPE_WAREHOUSE_DESPACHO", user.CompanyID)!.Value;
            ObjParameterImprimirPorCadaFactura = _objInterfazCoreWebParameter.GetParameter("INVOICE_PRINT_BY_INVOICE", user.CompanyID)!.Value;
            ObjParameterScanerProducto = _objInterfazCoreWebParameter.GetParameter("INVOICE_SHOW_POPUP_FIND_PRODUCTO_NOT_SCANER", user.CompanyID)!.Value;
            ObjParameterCantidadItemPoup = _objInterfazCoreWebParameter.GetParameter("INVOICE_CANTIDAD_ITEM", user.CompanyID)!.Value;
            ObjParameterHidenFiledItemNumber = _objInterfazCoreWebParameter.GetParameter("INVOICE_HIDEN_ITEMNUMBER_IN_POPUP", user.CompanyID)!.Value;
            ObjParameterAmortizationDuranteFactura = _objInterfazCoreWebParameter.GetParameter("INVOICE_PARAMTER_AMORITZATION_DURAN_INVOICE", user.CompanyID)!.Value;
            ObjParameterAlturaDelModalDeSeleccionProducto = _objInterfazCoreWebParameter.GetParameter("INVOICE_ALTO_MODAL_DE_SELECCION_DE_PRODUCTO_AL_FACTURAR", user.CompanyID)!.Value;
            ObjParameterScrollDelModalDeSeleccionProducto = _objInterfazCoreWebParameter.GetParameter("INVOICE_SCROLL_DE_MODAL_EN_SELECCION_DE_PRODUTO_AL_FACTURAR", user.CompanyID)!.Value;
            ObjParameterShowComandoDeCocina = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_SHOW_COMMAND_FOOT", user.CompanyID)!.Value;
            ObjParameterINVOICE_BILLING_VALIDATE_EXONERATION = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_VALIDATE_EXONERATION", user.CompanyID)!.Value;
            //Obtener la lista de estados
            if (ObjParameterInvoiceAutoApply == "true")
            {
                ObjListWorkflowStage = _objInterfazCoreWebWorkflow.GetWorkflowStageApplyFirst("tb_transaction_master_billing", "statusID", user.CompanyID, user.BranchID, role!.RoleID);
            }
            else
            {
                ObjListWorkflowStage = _objInterfazCoreWebWorkflow.GetWorkflowInitStage("tb_transaction_master_billing", "statusID", user.CompanyID, user.BranchID, role.RoleID);
            }


            //Obtener lista de mesas configuradas            
            int publicCatalogID = 0;
            List<TbPublicCatalog> objPubliCatalogMesasConfig = _objInterfazPublicCatalogModel.GetBySystemNameAndFlavorID("tb_transaction_master_billing.mesas_x_meseros", VariablesGlobales.Instance.Company!.FlavorID);
            if (CodigoMesero != "none")
            {
                if (objPubliCatalogMesasConfig is null)
                    throw new Exception("CONFIGURAR EL CATALOGO DE MESAS tb_transaction_master_billing.mesas_x_meseros");

                if (objPubliCatalogMesasConfig.Count == 0)
                    throw new Exception("CONFIGURAR EL CATALOGO DE MESAS tb_transaction_master_billing.mesas_x_meseros");
            }


            publicCatalogID = CodigoMesero == "none" ? 0 : objPubliCatalogMesasConfig.ElementAt(0).PublicCatalogID;
            List<TbPublicCatalogDetail> objPubliCatalogDetailMesasConfiguradas = _objInterfazPublicCatalogDetailModel.GetRowByCatalogIDAndName(publicCatalogID, CodigoMesero);

            ExchangeRate = _objInterfazCoreWebCurrency.GetRatio(user.CompanyID, DateTime.Today, decimal.One, ObjCurrencyDolares!.CurrencyID, ObjCurrency!.CurrencyID);
            ObjListEmployee = _objInterfazEmployeeModel.GetRowByBranchIdAndType(user.CompanyID, user.BranchID, Convert.ToInt32(ObjParameterInvoiceTypeEmployer));
            ObjListBank = _objInterfazBankModel.GetByCompany(user.CompanyID);
            ObjCausal = _objInterfazTransactionCausalModel.GetCausalByBranch(user.CompanyID, TransactionId.Value, user.BranchID);
            WarehouseId = ObjCausal.First()!.WarehouseSourceID;
            ObjListWarehouse = _objInterfazUserWarehouseModel.GetRowByUserIdAndFacturable(user.CompanyID, user.UserID);
            ObjCustomerDefault = _objInterfazCustomerModel.GetRowByCode(user.CompanyID, customerDefault!.Value);
            ObjListTypePrice = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_price", "typePriceID", user.CompanyID);
            ObjListZone = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_transaction_master_info_billing", "zoneID", user.CompanyID);
            ObjListMesa = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_transaction_master_info_billing", "mesaID", user.CompanyID);

            //Validar Banco
            if (ObjListBank is null)
                throw new Exception("No se puede avanzar configurar bancos");

            if (ObjListBank.Count == 0)
                throw new Exception("No se puede avanzar configurar bancos");


            //Filtrar la Lista de mesas
            var obListMesasFiltradas = ObjListMesa.Where(item1 => objPubliCatalogDetailMesasConfiguradas.Any(item2 => item2.Display == item1.Name)).ToList();
            ObjListMesa = CodigoMesero == "none" ? ObjListMesa : obListMesasFiltradas;

            if (ObjListMesa is null)
                throw new Exception("No se puede avanzar configurar catalogo de MESS");

            if (ObjListMesa.Count == 0)
                throw new Exception("No se puede avanzar configurar catalogo de MESS");


            ObjListPay = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer_credit_line", "periodPay", user.CompanyID);
            ObjListDayExcluded = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer_credit_line", "dayExcluded", user.CompanyID);
            ListProvider = _objInterfazProviderModel.GetRowByCompany(user.CompanyID);
            ObjParameterCxcPlazoDefault = _objInterfazCoreWebParameter.GetParameterValue("CXC_PLAZO_DEFAULT", user.CompanyID);
            ObjParameterCxcFrecuenciaPayDefault = _objInterfazCoreWebParameter.GetParameterValue("CXC_FRECUENCIA_PAY_DEFAULT", user.CompanyID);
            objParameterCXC_DAY_EXCLUDED_IN_CREDIT = _objInterfazCoreWebParameter.GetParameterValue("CXC_DAY_EXCLUDED_IN_CREDIT", user.CompanyID);
            ObjParameterCustomPopupFacturacion = _objInterfazCoreWebParameter.GetParameterValue("CORE_VIEW_CUSTOM_PANTALLA_DE_FACTURACION_POPUP_SELECCION_PRODUCTO_FORMA_MOSTRAR", user.CompanyID);
            ObjParameterInvoiceBillingApplyTypePriceOnDayPorMayor = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_APPLY_TYPE_PRICE_ON_DAY_POR_MAYOR", user.CompanyID);
            ObjParameterInvoiceBillingShowCommandBar = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_SHOW_COMMAND_BAR", user.CompanyID);
            ObjParameterInvoiceBillingPrinterDirectNameDefaultBar = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_PRINTER_DIRECT_NAME_DEFAULT_BAR", user.CompanyID);
            ObjParameterInvoiceBillingPrinterDirectUrlBar = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_PRINTER_DIRECT_URL_BAR", user.CompanyID);
            ObjParameterobjParameterInvoiceBillingPrinterUrlBar = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_PRINTER_URL_BAR", user.CompanyID);
            ObjParameterInvoiceBillingSelectitem = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_SELECTITEM", user.CompanyID);
            ObjParameterInvoiceBillingQuantityZero = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_QUANTITY_ZERO", user.CompanyID)!.Value;
            ObjParameterRegresarAListaDespuesDeGuardar = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_SAVE_AFTER_TO_LIST", user.CompanyID)!.Value;
            ObjParameterMostrarImagenEnSeleccion = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_SHOW_IMAGE_IN_DETAIL_SELECTION", user.CompanyID)!.Value;
            ObjParameterPantallaParaFacturar = _objInterfazCoreWebParameter.GetParameter("INVOICE_PANTALLA_FACTURACION", user.CompanyID)!.Value;

            if (ObjCustomerDefault is null)
            {
                throw new Exception("NO EXISTE EL CLIENTE POR DEFECTO");
            }


            ObjNaturalDefault = _objInterfazNaturalModel.GetRowByPk(user.CompanyID, ObjCustomerDefault.BranchID, ObjCustomerDefault.EntityID);
            ObjLegalDefault = _objInterfazLegalModel.GetRowByPk(user.CompanyID, ObjCustomerDefault.BranchID, ObjCustomerDefault.EntityID);
            ObjEmployeeNatural = _objInterfazNaturalModel.GetRowByPk(user.CompanyID, user.BranchID, user.EmployeeID);
            if (ObjEmployeeNatural == null)
                throw new Exception("USUARIO DEBE DE TENER CONFIGURADO UN COLABORADOR ");

            //Obtener la linea de credito del cliente por defecto
            ObjCurrencyDolares = _objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyID);
            ObjCurrencyCordoba = _objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyID);
            ParameterCausalTypeCredit = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_CREDIT", user.CompanyID);
            ObjCustomerCreditAmoritizationAll = _objInterfazCustomerCreditAmortizationModel.GetRowByCustomerId(ObjCustomerDefault.EntityID);
            ObjListCustomerCreditLine = _objInterfazCustomerCreditLineModel.GetRowByEntityBalanceMayorCero(user.CompanyID, user.BranchID, ObjCustomerDefault.EntityID);


            ObjListPermisos = VariablesGlobales.Instance.ListMenuHiddenPopup;
            varPermisosEsPermitidoModificarPrecio = ObjListPermisos!.Count(element => element.Display == "ES_PERMITIDO_MODIFICAR_PRECIO_EN_FACTURACION") > 0;
            varPermisosEsPermitidoModificarNombre = ObjListPermisos!.Count(element => element.Display == "ES_PERMITIDO_MODIFICAR_NOMBRE_EN_FACTURACION") > 0;
            varPermisosEsPermitidoSeleccionarPrecioPublico = ObjListPermisos!.Count(element => element.Display == "ES_PERMITIDO_SELECCIONAR_PRECIO_PUBLICO") > 0;
            varPermisosEsPermitidoSeleccionarPrecioMayor = ObjListPermisos!.Count(element => element.Display == "ES_PERMITIDO_SELECCIONAR_PRECIO_PORMAYOR") > 0;
            varPermisosEsPermitidoSeleccionarPrecioCredito = ObjListPermisos!.Count(element => element.Display == "ES_PERMITIDO_SELECCIONAR_PRECIO_CREDITO") > 0;
            //tx.Complete();
        }

        public void SaveInsert()
        {
            //try
            //{
            var coreWebAuditoria = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebAuditoria>();
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
            var causalIdEditValue = (ComboBoxItem)txtCausalID.SelectedItem;
            var causalId = Convert.ToInt32(causalIdEditValue.Key);
            TransactionId = _objInterfazCoreWebTransaction.GetTransactionId(user.CompanyID, "tb_transaction_master_billing", 0);
            var objT = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionModel>().GetByCompanyAndTransaction(user.CompanyID, TransactionId!.Value);
            var objTransactionCausal = _objInterfazTransactionCausalModel.GetByCompanyAndTransactionAndCausal(user.CompanyID, TransactionId!.Value, causalId);


            //Valores de tasa de cambio
            ObjCurrencyDolares = _objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyID);
            ObjCurrencyCordoba = _objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyID);
            ExchangeRate = _objInterfazCoreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, 1, ObjCurrencyDolares!.CurrencyID, ObjCurrencyCordoba!.CurrencyID);
            var cycleIsCloseByDate = _objInterfazCoreWebAccounting.CycleIsCloseByDate(user.CompanyID, txtDate.DateTime);
            if (cycleIsCloseByDate) throw new Exception("EL DOCUMENTO NO PUEDE INGRESAR, EL CICLO CONTABLE ESTA CERRADO");
            _objInterfazCoreWebPermission.GetValueLicense(user.CompanyID, "app_invoice_billing/index");


            //obtener el primer estado  de la factura o el estado inicial.
            ObjParameterInvoiceBillingQuantityZero = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_QUANTITY_ZERO", user.CompanyID)!.Value;
            ObjListWorkflowStage = _objInterfazCoreWebWorkflow.GetWorkflowAllStage("tb_transaction_master_billing", "statusID", user.CompanyID, user.BranchID, role!.RoleID);
            //Saber si se va autoaplicar
            ObjParameterInvoiceAutoApply = _objInterfazCoreWebParameter.GetParameter("INVOICE_AUTOAPPLY_CASH", user.CompanyID)!.Value;
            ObjParaemterStatusCanceled = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_CANCEL", user.CompanyID)!.Value;
            ObjParameterUrlPrinterDirect = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_PRINTER_DIRECT_URL", user.CompanyID)!.Value;
            ObjParameterImprimirPorCadaFactura = _objInterfazCoreWebParameter.GetParameter("INVOICE_PRINT_BY_INVOICE", user.CompanyID)!.Value;
            var objParameterINVOICE_BILLING_TRAKING_BAR = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_TRAKING_BAR", user.CompanyID);
            var objParameterSendEmailInInsert = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_SEND_EMAIL_IN_INSERT", user.CompanyID);

            //Saber si es al credito
            ParameterCausalTypeCredit = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_CREDIT", user.CompanyID);
            var causalIdTypeCredit = ParameterCausalTypeCredit!.Value!.Split(',');
            // Buscar el valor en la matriz
            var exisCausalInCredit = Array.IndexOf(causalIdTypeCredit, ((ComboBoxItem)txtCausalID.SelectedItem).Key) > 0;
            //Si esta configurado como auto aplicado
            //y es al credito. cambiar el estado por el estado inicial, que es registrada
            int? statusId = 0;


            if (ObjParameterInvoiceAutoApply == "true" && exisCausalInCredit)
            {
                statusId = ObjListWorkflowStage?[0].WorkflowStageID;
            }
            else if (ObjParameterInvoiceAutoApply == "true" && exisCausalInCredit != true)
            {
                statusId = Convert.ToInt32(ObjParaemterStatusCanceled);
            }
            else
            {
                statusId = TxtStatusId;
            }

            var varDescuento = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtDescuento.Text);
            var varPorcentajeDescuento = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtPorcentajeDescuento.Text);
            var currencyId = Convert.ToInt32(((ComboBoxItem)txtCurrencyID.SelectedItem).Key);
            var currencyId2 = _objInterfazCoreWebCurrency.GetTarget(user.CompanyID, currencyId);
            var goNextNumber = _objInterfazCoreWebCounter.GoNextNumber(user.CompanyID, user.BranchID, "tb_transaction_master_proforma", 0);
            var sourceWarehouseId = Convert.ToInt32(((ComboBoxItem)txtWarehouseID.SelectedItem).Key);
            var creditLine = txtCustomerCreditLineID.SelectedItem is null ? "0" : ((ComboBoxItem)txtCustomerCreditLineID.SelectedItem).Key;
            var objTm = new TbTransactionMaster
            {
                CompanyID = user.CompanyID,
                TransactionID = TransactionId!.Value,
                BranchID = user.BranchID,
                TransactionNumber = string.IsNullOrEmpty(goNextNumber) ? "" : goNextNumber,
                TransactionCausalID = Convert.ToInt32(causalIdEditValue.Key),
                EntityID = TxtCustomerId,
                TransactionOn = txtDate.DateTime,
                TransactionOn2 = txtDateFirst.DateTime,
                StatusIDChangeOn = DateTime.Now,
                ComponentID = objComponentBilling.ComponentID,
                Note = txtNote.Text,
                Sign = (short?)objT!.SignInventory,
                CurrencyID = currencyId, //validar este campo
                CurrencyID2 = currencyId2,
                ExchangeRate = _objInterfazCoreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, decimal.One, currencyId2, currencyId2),
                Reference1 = ((ComboBoxItem)txtReference1.SelectedItem).Key,
                DescriptionReference = "reference1:entityID del proveedor de credito para las facturas al credito,reference4: customerCreditLineID linea de credito del cliente",
                Reference2 = txtReference2.Text,
                Reference3 = txtReference3.Text,
                Reference4 = creditLine,
                StatusID = statusId,
                Amount = decimal.Zero,
                IsApplied = false,
                JournalEntryID = 0,
                ClassID = null,
                AreaID = null,
                SourceWarehouseID = sourceWarehouseId,
                TargetWarehouseID = null,
                IsActive = true,
                PeriodPay = Convert.ToInt32(((ComboBoxItem)txtPeriodPay.SelectedItem).Key),
                NextVisit = txtNextVisit.DateTime,
                NumberPhone = txtNumberPhone.Text,
                EntityIDSecondary = Convert.ToInt32(((ComboBoxItem)txtEmployeeID.SelectedItem).Key),
                DayExcluded = Convert.ToInt32(((ComboBoxItem)txtDayExcluded.SelectedItem).Key)
            };
            objTm.ExchangeRate = _objInterfazCoreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, decimal.One, objTm.CurrencyID2!.Value, objTm.CurrencyID!.Value);
            coreWebAuditoria.SetAuditCreated(objTm, user, "");
            TransactionMasterId = _objInterfazTransactionMasterModel.InsertAppPosme(objTm);
            var assembly = Assembly.GetEntryAssembly();
            var documentoPath = "";
            if (assembly is not null)
            {
                //Crear la Carpeta para almacenar los Archivos del Documento
                documentoPath = $"{assembly.Location}/company_{user.CompanyID}/component_{objComponentBilling.ComponentID}/component_item_{TransactionMasterId.Value}";
            }

            var objTmInfo = new TbTransactionMasterInfo
            {
                CompanyID = objTm.CompanyID,
                TransactionID = objTm.TransactionID,
                TransactionMasterID = TransactionMasterId.Value,
                ZoneID = Convert.ToInt32(((ComboBoxItem)txtZoneID.SelectedItem).Key),
                MesaID = Convert.ToInt32(((ComboBoxItem)txtMesaID.SelectedItem).Key),
                RouteID = 0,
                ReferenceClientName = txtReferenceClientName.Text,
                ReferenceClientIdentifier = txtReferenceClientIdentifier.Text,
                ReceiptAmount = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmount.Text),
                ReceiptAmountDol = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmountDol.Text),
                ReceiptAmountPoint = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmountPoint.Text),
                ReceiptAmountBank = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmountBank.Text),
                ReceiptAmountBankDol = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmountBankDol.Text),
                ReceiptAmountCardDol = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmountTarjetaDol.Text),
                ReceiptAmountCard = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmountTarjeta.Text),
                ChangeAmount = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtChangeAmount.Text),
                ReceiptAmountBankReference = FormInvoiceBillingEditPayment.txtReceiptAmountBank_Reference.Text,
                ReceiptAmountBankDolReference = FormInvoiceBillingEditPayment.txtReceiptAmountBankDol_Reference.Text,
                ReceiptAmountCardBankReference = FormInvoiceBillingEditPayment.txtReceiptAmountTarjeta_Reference.Text,
                ReceiptAmountCardBankDolReference = FormInvoiceBillingEditPayment.txtReceiptAmountTarjetaDol_Reference.Text,
                ReceiptAmountBankID = WebToolsHelper.ConvertToNumber<int>(((ComboBoxItem)FormInvoiceBillingEditPayment.txtReceiptAmountBank_BankID.SelectedItem).Key),
                ReceiptAmountBankDolID = WebToolsHelper.ConvertToNumber<int>(((ComboBoxItem)FormInvoiceBillingEditPayment.txtReceiptAmountBankDol_BankID.SelectedItem).Key),
                ReceiptAmountCardBankID = WebToolsHelper.ConvertToNumber<int>(((ComboBoxItem)FormInvoiceBillingEditPayment.txtReceiptAmountTarjeta_BankID.SelectedItem).Key),
                ReceiptAmountCardBankDolID = WebToolsHelper.ConvertToNumber<int>(((ComboBoxItem)FormInvoiceBillingEditPayment.txtReceiptAmountTarjetaDol_BankID.SelectedItem).Key),
                Reference1 = txtTMIReference1.Text,
                Reference2 = "not_used"
            };
            objTmInfo.TransactionMasterInfoID = _objInterfazTransactionMasterInfoModel.InsertAppPosme(objTmInfo);

            //Ingresar TransactionMaster Reference
            var objTMReference = new TbTransactionMasterReference
            {
                TransactionMasterID = TransactionMasterId,
                Reference1 = txtLayFirstLineProtocolo.Text,
                Reference2 = txtCheckApplyExoneracion.IsOn ? "1" : "0",
                CreatedOn = DateTime.Now,
                IsActive = true
            };
            _objInterfazTransactionMasterReferencesModel.InsertAppPosme(objTMReference);

            //Ingresar la configuracion de precios		
            var amountTotal = decimal.Zero;
            var tax1Total = decimal.Zero;
            var tax2Total = decimal.Zero;
            var subAmountTotal = decimal.Zero;

            //Tipo de precio seleccionado por el usuario,
            //Actualmente no se esta usando
            var typePriceId = ((ComboBoxItem)txtTypePriceID.SelectedItem).Key; //verificar valor
            var objListPrice = _objInterfazListPriceModel.GetListPriceToApply(user.CompanyID);
            //obtener la lista de precio por defecto
            var objParameterPriceDefault = _objInterfazCoreWebParameter.GetParameter("INVOICE_DEFAULT_PRICELIST", user.CompanyID);
            var listPriceId = objParameterPriceDefault!.Value;
            //obener los tipos de precio de la lista de precio por defecto
            var objTipePrice = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_price", "typePriceID", user.CompanyID);

            //Parametro para validar si se cambian los precios en la facturacion
            var objParameterUpdatePrice = _objInterfazCoreWebParameter.GetParameter("INVOICE_UPDATEPRICE_ONLINE", user.CompanyID);
            var objUpdatePrice = objParameterUpdatePrice!.Value;

            var ObjParameterUpdateNameInTransactionOnly = _objInterfazCoreWebParameter.GetParameter("INVOICE_UPDATENAME_IN_TRANSACTION_ONLY", user.CompanyID);
            ObjParameterInvoiceUpdateNameInTransactionOnly = ObjParameterUpdateNameInTransactionOnly!.Value;

            var rowCount = gridViewValues.RowCount;
            for (var i = 0; i < rowCount; i++)
            {
                //Recorrer la lista del detalle del documento
                var transactionDetailName = gridViewValues.GetRowCellValue(i, colTransactionDetailName).ToString();
                var transactionDetailNameDescription = gridViewValues.GetRowCellValue(i, colTransactionDetailNameDescription).ToString();
                var itemNameDetail = transactionDetailName is null ? "" : transactionDetailName.Replace("'", "");
                var quantity = WebToolsHelper.ConvertToNumber<decimal>(gridViewValues.GetRowCellValue(i, colQuantity).ToString());
                var listPrice = Convert.ToDecimal(gridViewValues.GetRowCellValue(i, colPrice));
                var listLote = string.Empty;
                var listVencimiento = string.Empty;
                var skuCatalogItemId = Convert.ToInt32(gridViewValues.GetRowCellValue(i, colSku));
                var skuFormatoDescription = gridViewValues.GetRowCellValue(i, colSkuFormatoDescripton).ToString();
                var itemId = Convert.ToInt32(gridViewValues.GetRowCellValue(i, colItemId));


                var lote = string.IsNullOrEmpty(listLote) ? "0" : listLote;
                var vencimiento = string.IsNullOrEmpty(listVencimiento) ? "" : listVencimiento;
                var warehouseId = objTm.SourceWarehouseID;
                var objItem = _objInterfazItemModel.GetRowByPk(user.CompanyID, itemId);
                var objItemWarehouse = VariablesGlobales.Instance.UnityContainer.Resolve<IItemWarehouseModel>().GetByPk(user.CompanyID, itemId, warehouseId.Value);
                var objPrice = _objInterfazPriceModel.GetRowByPk(user.CompanyID, objListPrice!.ListPriceID, itemId, Convert.ToInt32(typePriceId));
                var objCompanyComponentConcept = _objInterfazCompanyComponentConceptModel.GetRowByPk(user.CompanyID, objComponentItem.ComponentID, itemId, "IVA");
                var objCompanyComponentConceptTaxServices = _objInterfazCompanyComponentConceptModel.GetRowByPk(user.CompanyID, objComponentItem.ComponentID, itemId, "TAX_SERVICES");
                var objItemSku = _objInterfazItemSkuModel.GetByPk(itemId, skuCatalogItemId);
                decimal price;
                if (objItemSku is null)
                {
                    throw new Exception("No hay un objeto del tipo item sku");
                }

                price = listPrice / objItemSku.Value;
                var ivaPercentage = (objCompanyComponentConcept is not null) ? objCompanyComponentConcept.ValueOut : decimal.Zero;
                var taxServicesPercentage = (objCompanyComponentConceptTaxServices is not null) ? objCompanyComponentConceptTaxServices.ValueOut : decimal.Zero;
                ivaPercentage = !txtCheckApplyExoneracion.IsOn ? ivaPercentage : decimal.Zero; //$objTMReference["reference2"] == "0"
                taxServicesPercentage = !txtCheckApplyExoneracion.IsOn ? taxServicesPercentage : decimal.Zero; //$objTMReference["reference2"] == "0"

                var unitaryAmount = price * (1 + ivaPercentage);
                var tax1 = price * ivaPercentage;
                var tax2 = price * taxServicesPercentage;
                //Actualisar nombre 
                if (ObjParameterInvoiceUpdateNameInTransactionOnly == "false")
                {
                    var objItemNew = _objInterfazItemModel.GetRowByPk(user.CompanyID, itemId);
                    objItemNew.Name = itemNameDetail;
                    _objInterfazItemModel.UpdateAppPosme(user.CompanyID, itemId, objItemNew);
                }


                if (objItemWarehouse.Quantity < quantity && objItem.IsInvoiceQuantityZero == 0 && ObjParameterInvoiceBillingQuantityZero == "false")
                {
                    throw new Exception($"La cantidad de '{objItem.ItemNumber} {objItem.Name}' es mayor que la disponible en bodega");
                }


                var objTmd = new TbTransactionMasterDetail
                {
                    CompanyID = objTm.CompanyID,
                    TransactionID = objTm.TransactionID,
                    TransactionMasterID = TransactionMasterId.Value,
                    ComponentID = objComponentItem.ComponentID,
                    ComponentItemID = itemId,
                    Quantity = quantity * objItemSku.Value,
                    SkuQuantity = quantity,
                    SkuQuantityBySku = objItemSku.Value,
                    UnitaryCost = objItem.Cost,
                    UnitaryPrice = price,
                    UnitaryAmount = unitaryAmount,
                    Tax1 = tax1, //impuesto de lista
                    Tax2 = tax2, //impuesto de servicio
                    Discount = 0,
                    PromotionID = 0,
                    Reference1 = lote,
                    Reference2 = vencimiento,
                    Reference3 = "0", // Asumiendo que Reference3 es una cadena
                    ItemNameLog = itemNameDetail,
                    ItemNameDescriptionLog = transactionDetailNameDescription,
                    CatalogStatusID = 0,
                    InventoryStatusID = 0,
                    IsActive = true,
                    QuantityStock = 0,
                    QuantiryStockInTraffic = 0,
                    QuantityStockUnaswared = 0,
                    RemaingStock = 0,
                    ExpirationDate = null,
                    InventoryWarehouseSourceID = objTm.SourceWarehouseID,
                    InventoryWarehouseTargetID = objTm.TargetWarehouseID,
                    SkuCatalogItemID = skuCatalogItemId,
                    SkuFormatoDescription = skuFormatoDescription
                };

                objTmd.Cost = objTmd.Quantity * objTmd.UnitaryCost;
                objTmd.Amount = objTmd.Quantity * objTmd.UnitaryAmount; //precio de lista con inpuesto por cantidad


                tax1Total = tax1Total + tax1!.Value;
                tax2Total = tax2Total + tax2.Value;
                subAmountTotal = subAmountTotal + (quantity * price);
                amountTotal = amountTotal + objTmd.Amount!.Value;

                var transactionMasterDetailId = _objInterfazTransactionMasterDetailModel.InsertAppPosme(objTmd);

                var objTmdc = new TbTransactionMasterDetailCredit
                {
                    TransactionMasterID = TransactionMasterId.Value,
                    TransactionMasterDetailID = transactionMasterDetailId,
                    Reference1 = string.IsNullOrEmpty(txtFixedExpenses.Text) ? "0" : txtFixedExpenses.Text,
                    Reference2 = txtReportSinRiesgo.IsOn ? "1" : "0",
                    Reference3 = "", //no existe el campo
                    Reference4 = "",
                    Reference5 = "",
                    Reference9 = "reference1: Porcentaje de Gastos Fijo para las facturas de credito,reference2: Escritura Publica,reference3: Primer Linea del Protocolo"
                };
                _objInterfazTransactionMasterDetailCreditModel.InsertAppPosme(objTmdc);

                //Actualizar tipo de precio
                if (objUpdatePrice == "true")
                {
                    var dataUpdatePrice = _objInterfazPriceModel.GetRowByPk(user.CompanyID, Convert.ToInt32(listPriceId), itemId, Convert.ToInt32(typePriceId));
                    if (dataUpdatePrice is not null)
                    {
                        dataUpdatePrice.Price = price;
                        dataUpdatePrice.Percentage = objItem.Cost == 0 ? (price / 100) : (((100 * price) / objItem.Cost) - 100);
                        _objInterfazPriceModel.UpdateAppPosme(user.CompanyID, Convert.ToInt32(listPriceId), itemId, Convert.ToInt32(typePriceId), dataUpdatePrice!);
                    }
                }

                //Ingresar la lista de productos de RESTAURANTE
                if (objParameterINVOICE_BILLING_TRAKING_BAR.Equals("true", StringComparison.InvariantCultureIgnoreCase))
                {
                    var objTMDRNew = new TbTransactionMasterDetailReference
                    {
                        IsActive = 1,
                        CreatedOn = DateTime.Now,
                        Quantity = objTmd.Quantity.Value.ToString("F2"),
                        ComponentID = objTmd.ComponentID,
                        ComponentItemID = objTmd.ComponentItemID,
                        TransactionMasterDetailID = transactionMasterDetailId
                    };
                    _objInterfazTransactionMasterDetailReferencesModel.InsertAppPosme(objTMDRNew);
                }
            }

            amountTotal = amountTotal - varDescuento;
            //Actualizar Transaccion
            objTm.SubAmount = subAmountTotal;
            objTm.Tax1 = tax1Total;
            objTm.Tax2 = tax2Total;
            objTm.Tax4 = varPorcentajeDescuento;
            objTm.Discount = varDescuento;
            objTm.Amount = amountTotal;
            _objInterfazTransactionMasterModel.UpdateAppPosme(user.CompanyID, TransactionId!.Value, TransactionMasterId.Value, objTm);

            //Aplicar el Documento?
            //Las factuas de credito no se auto aplican auque este el parametro, por que hay que crer el documento
            //y esto debe ser revisado cuidadosamente
            var commandAplicable = VariablesGlobales.ConfigurationBuilder["COMMAND_APLICABLE"];
            var validateWorkflowStage = _objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_billing", "statusID", (int)objTm.StatusID!, Convert.ToInt32(commandAplicable), user.CompanyID, user.BranchID, role.RoleID);
            if (validateWorkflowStage!.Value)
            {
                //Ingresar en Kardex.
                VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebInventory>().CalculateKardexNewOutput(user.CompanyID, TransactionId!.Value, TransactionMasterId.Value);

                //Crear Conceptos.
                VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebConcept>().Billing(user.CompanyID, TransactionId!.Value, TransactionMasterId.Value);

                //Actualizar el numero de factura
                objTm.TransactionNumber = _objInterfazCoreWebCounter.GoNextNumber(user.CompanyID, user.BranchID, "tb_transaction_master_billing", 0);
                _objInterfazTransactionMasterModel.UpdateAppPosme(user.CompanyID, TransactionId.Value, TransactionMasterId.Value, objTm);
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
            }

            if (objParameterSendEmailInInsert.Equals("true", StringComparison.InvariantCultureIgnoreCase) && txtNextVisit.DateTime > DateTime.MinValue)
            {
                var userTools = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();
                var emailProperty = _objInterfazCoreWebParameter.GetParameterValue("CORE_PROPIETARY_EMAIL", user.CompanyID);

                var subject = $@" Cita de: {objTmInfo.ReferenceClientName}";
                var body = $@"
                    Estimados Señores de {VariablesGlobales.Instance.Company.Name}
                    En sus manos:
                    Cita de: {objTmInfo.ReferenceClientName} programada para {txtNextVisit.DateTime.ToShortDateString()}
                    Fecha {DateTime.Now.ToLongDateString()}
                 ";

                userTools.SendEmail(emailProperty, subject, body);
            }
            //}
            //catch (Exception e)
            //{
            //    Debug.WriteLine($"{e.Message} {e.StackTrace}");
            //    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", $"Se produjo el siguiente error: {e.Message}", this);
            //}
        }

        public void CommandNew(object? sender, EventArgs e)
        {
        }

        public void CommandSave(object? sender, EventArgs e)
        {
        }

        public void CommandRegresar(object? sender, EventArgs e)
        {
        }

        public void SaveUpdate()
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

            var objTm = _objInterfazTransactionMasterModel.GetRowByPk(user.CompanyID, TransactionId!.Value, TransactionMasterId!.Value);
            if (objTm is null) throw new Exception("EL COMPONENTE 'objTm' NO EXISTE...");
            var oldStatusId = objTm.StatusId;
            ParameterCausalTypeCredit = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_CREDIT", user.CompanyID);

            //Valores de tasa de cambio
            ObjCurrencyDolares = _objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyID);
            ObjCurrencyCordoba = _objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyID);
            ExchangeRate = _objInterfazCoreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, 1, ObjCurrencyDolares!.CurrencyID, ObjCurrencyCordoba!.CurrencyID);

            //Validar Edicion por el Usuario
            if (resultPermission == permissionMe && objTm.CreatedBy != user.UserID) throw new Exception(notEdit);

            //Validar si el estado permite editar
            var notWorkflowEdit = VariablesGlobales.ConfigurationBuilder["NOT_WORKFLOW_EDIT"];
            var commandEditableTotal = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_EDITABLE_TOTAL"]);
            var commandEditable = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_EDITABLE"]);
            var validateWorkflowStage = _objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_billing", "statusID", objTm.StatusId!.Value, commandEditableTotal, user.CompanyID, user.BranchID, role.RoleID);
            if (validateWorkflowStage is null || !validateWorkflowStage.Value)
            {
                throw new Exception(notWorkflowEdit);
            }

            if (_objInterfazCoreWebAccounting.CycleIsCloseByDate(user.CompanyID, objTm.TransactionOn!.Value))
            {
                throw new Exception("EL DOCUMENTO NO PUEDE ACTUALIZARCE, EL CICLO CONTABLE ESTA CERRADO");
            }

            ObjParameterInvoiceAutoApply = _objInterfazCoreWebParameter.GetParameter("INVOICE_AUTOAPPLY_CASH", user.CompanyID)!.Value;
            ObjParameterInvoiceBillingQuantityZero = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_QUANTITY_ZERO", user.CompanyID)!.Value;
            ObjParameterImprimirPorCadaFactura = _objInterfazCoreWebParameter.GetParameter("INVOICE_PRINT_BY_INVOICE", user.CompanyID)!.Value;
            var objParameterRegrearANuevo = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_SAVE_AFTER_TO_ADD", user.CompanyID)!.Value;
            var objParameterINVOICE_BILLING_TRAKING_BAR = _objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_TRAKING_BAR", user.CompanyID);

            //Actualizar Maestro
            var typePriceId = Convert.ToInt32(((ComboBoxItem)txtTypePriceID.SelectedItem).Key);
            var varDescuento = WebToolsHelper.HelperstringToNumber(FormInvoiceBillingEditPayment.txtDescuento.Text);
            var varPorcentajeDescuento = WebToolsHelper.HelperstringToNumber(FormInvoiceBillingEditPayment.txtPorcentajeDescuento.Text);
            var objListPrice = _objInterfazListPriceModel.GetListPriceToApply(user.CompanyID);
            var customerCreditlineIdEditValue = (ComboBoxItem)txtCustomerCreditLineID.SelectedItem; //esta dando null
            var objTmNew = _objInterfazTransactionMasterModel.GetRowByPKK(TransactionMasterId.Value);
            objTmNew.TransactionCausalID = Convert.ToInt32(((ComboBoxItem)txtCausalID.SelectedItem).Key);
            objTmNew.EntityID = TxtCustomerId;
            objTmNew.TransactionOn = DateTime.Now.Date;
            objTmNew.TransactionOn2 = txtDateFirst.DateTime;
            objTmNew.StatusIDChangeOn = DateTime.Now;
            objTmNew.Note = txtNote.Text;
            objTmNew.Reference1 = ((ComboBoxItem)txtReference1.SelectedItem).Key;
            objTmNew.DescriptionReference = "reference1:entityId del proveedor de credito para las facturas al credito;reference4: customerCreditLineId linea de credito del cliente";
            objTmNew.Reference2 = txtReference2.Text;
            objTmNew.Reference3 = txtReference3.Text;
            objTmNew.Reference4 = customerCreditlineIdEditValue is null ? "0" : customerCreditlineIdEditValue.Key;
            objTmNew.StatusID = TxtStatusId;
            objTmNew.Amount = 0;
            objTmNew.CurrencyID = Convert.ToInt32(((ComboBoxItem)txtCurrencyID.SelectedItem).Key);
            objTmNew.CurrencyID2 = _objInterfazCoreWebCurrency.GetTarget(user.CompanyID, objTmNew.CurrencyID.Value);
            objTmNew.ExchangeRate = _objInterfazCoreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, decimal.One, objTmNew.CurrencyID2.Value, objTmNew.CurrencyID.Value);
            objTmNew.SourceWarehouseID = Convert.ToInt32(((ComboBoxItem)txtWarehouseID.SelectedItem).Key);
            objTmNew.PeriodPay = Convert.ToInt32(((ComboBoxItem)txtPeriodPay.SelectedItem).Key);
            objTmNew.NextVisit = txtNextVisit.DateTime;
            objTmNew.NumberPhone = txtNumberPhone.Text;
            objTmNew.EntityIDSecondary = Convert.ToInt32(((ComboBoxItem)txtEmployeeID.SelectedItem).Key);
            objTmNew.DayExcluded = Convert.ToInt32(((ComboBoxItem)txtDayExcluded.SelectedItem).Key);
            objTmNew.CurrencyID2 = _objInterfazCoreWebCurrency.GetTarget(user.CompanyID, objTmNew.CurrencyID!.Value);
            objTmNew.ExchangeRate = _objInterfazCoreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, 1, objTmNew.CurrencyID2!.Value, objTmNew.CurrencyID!.Value);

            var receiptAmountBankBankIdkey = FormInvoiceBillingEditPayment.txtReceiptAmountBank_BankID.SelectedItem is null ? "0" : ((ComboBoxItem)FormInvoiceBillingEditPayment.txtReceiptAmountBank_BankID.SelectedItem).Key;
            var receiptAmountBankDolBankIdkey = FormInvoiceBillingEditPayment.txtReceiptAmountBankDol_BankID.SelectedItem is null ? "0" : ((ComboBoxItem)FormInvoiceBillingEditPayment.txtReceiptAmountBankDol_BankID.SelectedItem).Key;
            var receiptAmountTajertaBankIdKey = FormInvoiceBillingEditPayment.txtReceiptAmountTarjeta_BankID.SelectedItem is null ? "0" : ((ComboBoxItem)FormInvoiceBillingEditPayment.txtReceiptAmountTarjeta_BankID.SelectedItem).Key;
            var receiptAmountTarjetDolBankIdKey = FormInvoiceBillingEditPayment.txtReceiptAmountTarjetaDol_BankID.SelectedItem is null ? "0" : ((ComboBoxItem)FormInvoiceBillingEditPayment.txtReceiptAmountTarjetaDol_BankID.SelectedItem).Key;
            var objTmInfoNew = _objInterfazTransactionMasterInfoModel.GetRowByPkPk(TransactionMasterId.Value);
            objTmInfoNew.CompanyID = objTm.CompanyId;
            objTmInfoNew.TransactionID = TransactionId.Value;
            objTmInfoNew.TransactionMasterID = TransactionMasterId.Value;
            objTmInfoNew.ZoneID = Convert.ToInt32(((ComboBoxItem)txtZoneID.SelectedItem).Key);
            objTmInfoNew.MesaID = Convert.ToInt32(((ComboBoxItem)txtMesaID.SelectedItem).Key);
            objTmInfoNew.RouteID = 0;
            objTmInfoNew.ReferenceClientName = txtReferenceClientName.Text;
            objTmInfoNew.ReferenceClientIdentifier = txtReferenceClientIdentifier.Text;
            objTmInfoNew.ReceiptAmount = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmount.Text);
            objTmInfoNew.ReceiptAmountDol = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmountDol.Text);
            objTmInfoNew.ReceiptAmountPoint = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmountPoint.Text);
            objTmInfoNew.ReceiptAmountBank = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmountBank.Text);
            objTmInfoNew.ReceiptAmountBankDol = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmountBankDol.Text);
            objTmInfoNew.ReceiptAmountCardDol = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmountTarjetaDol.Text);
            objTmInfoNew.ReceiptAmountCard = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmountTarjeta.Text);
            objTmInfoNew.ChangeAmount = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtChangeAmount.Text);
            objTmInfoNew.ReceiptAmountBankReference = FormInvoiceBillingEditPayment.txtReceiptAmountBank_Reference.Text;
            objTmInfoNew.ReceiptAmountBankDolReference = FormInvoiceBillingEditPayment.txtReceiptAmountBankDol_Reference.Text;
            objTmInfoNew.ReceiptAmountCardBankReference = FormInvoiceBillingEditPayment.txtReceiptAmountTarjeta_Reference.Text;
            objTmInfoNew.ReceiptAmountCardBankDolReference = FormInvoiceBillingEditPayment.txtReceiptAmountTarjetaDol_Reference.Text;
            objTmInfoNew.ReceiptAmountBankID = WebToolsHelper.ConvertToNumber<int>(receiptAmountBankBankIdkey);
            objTmInfoNew.ReceiptAmountBankDolID = WebToolsHelper.ConvertToNumber<int>(receiptAmountBankDolBankIdkey);
            objTmInfoNew.ReceiptAmountCardBankID = WebToolsHelper.ConvertToNumber<int>(receiptAmountTajertaBankIdKey);
            objTmInfoNew.ReceiptAmountCardBankDolID = WebToolsHelper.ConvertToNumber<int>(receiptAmountTarjetDolBankIdKey);
            objTmInfoNew.Reference1 = txtTMIReference1.Text;
            objTmInfoNew.Reference2 = "not_used";

            var objTMReferenceNew = _objInterfazTransactionMasterReferencesModel.GetRowByTransactionMasterId(TransactionMasterId.Value);
            objTMReferenceNew.Reference1 = txtLayFirstLineProtocolo.Text;
            objTMReferenceNew.Reference2 = txtCheckApplyExoneracion.IsOn ? "1" : "0";

            //El Estado solo permite editar el workflow                
            if (_objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_billing", "statusID", objTm.StatusId!.Value, commandEditable, user.CompanyID, user.BranchID, role.RoleID)!.Value)
            {
                var tbTransactionMasterDto = _objInterfazTransactionMasterModel.GetRowByPKK(TransactionMasterId.Value);
                if (tbTransactionMasterDto is null)
                {
                    throw new Exception("NO existe el transaction master a actualizar");
                }

                tbTransactionMasterDto.StatusID = TxtStatusId;
                _objInterfazTransactionMasterModel.UpdateAppPosme(user.CompanyID, TransactionId!.Value, TransactionMasterId!.Value, tbTransactionMasterDto);
            }
            else
            {
                _objInterfazTransactionMasterModel.UpdateAppPosme(user.CompanyID, TransactionId!.Value, TransactionMasterId!.Value, objTmNew);
                _objInterfazTransactionMasterInfoModel.UpdateAppPosme(user.CompanyID, TransactionId!.Value, TransactionMasterId!.Value, objTmInfoNew);
                _objInterfazTransactionMasterReferencesModel.UpdateAppPosmeByTransactionMasterId(TransactionMasterId.Value, objTMReferenceNew);
            }

            //Leer archivo
            // Obtener la referencia al ensamblado actual
            var assembly = Assembly.GetEntryAssembly();

            // Obtener la ruta del archivo ejecutable
            var executablePath = assembly!.Location;
            var path = $"{executablePath}/company_{user.CompanyID}/component_{objComponentBilling.ComponentID}/component_item_{TransactionMasterId}/procesar.csv";
            var pathNew = $"{executablePath}/company_{user.CompanyID}/component_{objComponentBilling.ComponentID}/component_item_{TransactionMasterId}/procesado.csv";
            var listTransactionDetalId = new List<int>();
            var arrayListItemId = new List<int>();
            var arrayListItemName = new List<string>();
            var arrayListItemNameDescription = new List<string>();
            var arrayListQuantity = new List<decimal>();
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
                var objParameterDeliminterCsv = _objInterfazCoreWebParameter.GetParameter("CORE_CSV_SPLIT", user.CompanyID);
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


                        var objItem = _objInterfazItemModel.GetRowByCode(user.CompanyID, codigo);
                        // Añadir los valores a las listas
                        listTransactionDetalId.Add(0);
                        arrayListItemId.Add(objItem.ItemID);
                        arrayListItemName.Add(objItem.Name);
                        arrayListItemNameDescription.Add(objItem.Name);
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
                foreach (FormInvoiceBillingEditDetailDTO formInvoiceBillingEditDetailDto in _bindingListTransactionMasterDetail)
                {
                    var transactionMasterDetailId = formInvoiceBillingEditDetailDto.TransactionMasterDetailId;
                    listTransactionDetalId.Add(transactionMasterDetailId is null ? 0 : transactionMasterDetailId.Value);
                    arrayListItemId.Add(formInvoiceBillingEditDetailDto.ItemId!.Value);
                    arrayListItemName.Add(formInvoiceBillingEditDetailDto.TransactionDetailName!);
                    arrayListItemNameDescription.Add(formInvoiceBillingEditDetailDto.TransactionDetailNameDescription);
                    arrayListQuantity.Add(formInvoiceBillingEditDetailDto.Quantity);
                    arrayListPrice.Add(formInvoiceBillingEditDetailDto.Price);
                    arrayListSubTotal.Add(formInvoiceBillingEditDetailDto.SubTotal);
                    arrayListIva.Add(formInvoiceBillingEditDetailDto.Iva);
                    arrayListLote.Add("");
                    arrayListVencimiento.Add("");
                    arrayListSku.Add(formInvoiceBillingEditDetailDto.Sku);
                    arrayListSkuFormatoDescription.Add(formInvoiceBillingEditDetailDto.SkuFormatoDescription!);
                }
            }

            //Ingresar la configuracion de precios			
            var objParameterPriceDefault = _objInterfazCoreWebParameter.GetParameter("INVOICE_DEFAULT_PRICELIST", user.CompanyID);
            var listPriceId = objParameterPriceDefault!.Value;
            var objTipePrice = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_price", "typePriceID", user.CompanyID);

            var objParameterUpdatePrice = _objInterfazCoreWebParameter.GetParameter("INVOICE_UPDATEPRICE_ONLINE", user.CompanyID);
            var objUpdatePrice = objParameterUpdatePrice!.Value;

            var ObjParameterUpdateName = _objInterfazCoreWebParameter.GetParameter("INVOICE_UPDATENAME_IN_TRANSACTION_ONLY", user.CompanyID);
            ObjParameterInvoiceUpdateNameInTransactionOnly = ObjParameterUpdateName!.Value;

            var objParameterAmortizationDuranteFactura = _objInterfazCoreWebParameter.GetParameter("INVOICE_PARAMTER_AMORITZATION_DURAN_INVOICE", user.CompanyID)!.Value;

            var amountTotal = decimal.Zero;
            var tax1Total = decimal.Zero;
            var tax2Total = decimal.Zero;
            var subAmountTotal = decimal.Zero;
            _objInterfazTransactionMasterDetailModel.DeleteWhereIdNotIn(user.CompanyID, TransactionId.Value, TransactionMasterId.Value, listTransactionDetalId);
            _objInterfazTransactionMasterDetailCreditModel.DeleteWhereIdNotIn(TransactionMasterId.Value, listTransactionDetalId);

            if (objParameterINVOICE_BILLING_TRAKING_BAR.Equals("true", StringComparison.InvariantCultureIgnoreCase))
            {
                _objInterfazTransactionMasterDetailReferencesModel.DeleteWhereIdNotIn(listTransactionDetalId);
            }

            if (arrayListItemId.Count > 0)
            {
                for (var i = 0; i < arrayListItemId.Count; i++)
                {
                    var itemId = arrayListItemId[i];
                    var lote = "0";
                    var vencimiento = string.Empty;
                    var warehouseId = objTmNew.SourceWarehouseID;
                    var objItem = _objInterfazItemModel.GetRowByPk(user.CompanyID, itemId);
                    var objItemWarehouse = VariablesGlobales.Instance.UnityContainer.Resolve<IItemWarehouseModel>().GetByPk(user.CompanyID, itemId, warehouseId!.Value);
                    var quantity = WebToolsHelper.ConvertToNumber<int>(arrayListQuantity[i].ToString());
                    var unitaryCost = objItem.Cost;
                    var objPrice = _objInterfazPriceModel.GetRowByPk(user.CompanyID, objListPrice!.ListPriceID, itemId, (int)typePriceId);
                    var objCompanyComponentConcept = _objInterfazCompanyComponentConceptModel.GetRowByPk(user.CompanyID, objComponentItem.ComponentID, itemId, "IVA");
                    var objCompanyComponentConceptTaxServices = _objInterfazCompanyComponentConceptModel.GetRowByPk(user.CompanyID, objComponentItem.ComponentID, itemId, "TAX_SERVICES");
                    var skuCatalogItemId = arrayListSku[i];
                    var itemNameDetail = arrayListItemName[i].Replace("\"", "").Replace("'", "");
                    var itemNameDetailDecription = string.IsNullOrWhiteSpace(arrayListItemNameDescription[i]) ? "" : arrayListItemNameDescription[i].Replace("\"", "").Replace("'", "");
                    var objItemSku = _objInterfazItemSkuModel.GetByPk(itemId, skuCatalogItemId);
                    if (objItemSku is null)
                    {
                        throw new Exception("No existe el objeto objItemSku");
                    }

                    // Precio
                    var price = arrayListPrice[i] / objItemSku.Value;
                    var skuFormatoDescription = arrayListSkuFormatoDescription[i];
                    var ivaPercentage = (objCompanyComponentConcept != null ? objCompanyComponentConcept.ValueOut : decimal.Zero);
                    var taxServicesPorcentage = (objCompanyComponentConceptTaxServices != null ? objCompanyComponentConceptTaxServices.ValueOut : decimal.Zero);
                    ivaPercentage = txtCheckApplyExoneracion.IsOn ? decimal.Zero : ivaPercentage;
                    taxServicesPorcentage = txtCheckApplyExoneracion.IsOn ? decimal.Zero : taxServicesPorcentage;
                    var unitaryAmount = price * (1 + ivaPercentage);
                    var tax1 = price * ivaPercentage;
                    var tax2 = price * taxServicesPorcentage;
                    var transactionMasterDetailId = listTransactionDetalId[i];
                    var comisionPorcentage = decimal.Zero;

                    // Obtener porcentaje de comisión
                    var coreWebTransactionMasterDetail = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTransactionMasterDetail>();
                    comisionPorcentage = coreWebTransactionMasterDetail.GetPercentageCommission(user.CompanyID, Convert.ToInt32(listPriceId), itemId.ToString(), price);

                    // Obtener costo unitario del cliente
                    unitaryCost = coreWebTransactionMasterDetail.GetCostCustomer(user.CompanyID, itemId.ToString(), unitaryCost, price);

                    // Actualizar nombre
                    if (ObjParameterInvoiceUpdateNameInTransactionOnly == "false")
                    {
                        // Crear nuevo objeto de item
                        var objItemNew = _objInterfazItemModel.GetRowByPk(user.CompanyID, itemId);
                        objItemNew.Name = itemNameDetail.Trim();

                        // Actualizar el nombre del item
                        _objInterfazItemModel.UpdateAppPosme(user.CompanyID, itemId, objItemNew);

                        if (itemNameDetail.Contains("NC."))
                        {
                            // Actualizar nombre y código de barras
                            objItemNew.Name = itemNameDetail.Split("NC.")[0].Trim();
                            objItemNew.BarCode = objItem.BarCode + "," + itemNameDetail.Split("NC.")[1].Trim();
                            itemNameDetail = objItemNew.Name;
                            _objInterfazItemModel.UpdateAppPosme(user.CompanyID, itemId, objItemNew);
                        }

                        if (itemNameDetail.Contains("CC."))
                        {
                            // Actualizar nombre y código de barras
                            objItemNew.Name = itemNameDetail.Split("CC.")[0].Trim();
                            objItemNew.BarCode = itemNameDetail.Split("CC.")[1].Trim();
                            itemNameDetail = objItemNew.Name;

                            _objInterfazItemModel.UpdateAppPosme(user.CompanyID, itemId, objItemNew);
                        }
                    }

                    //Validar Cantidades
                    var messageException = $"La cantidad de '{objItem.ItemNumber} {objItem.Name} ' es mayor que la disponible en bodega, en bodega existen {objItemWarehouse.Quantity} y esta solicitando : {quantity}";
                    if (objItemWarehouse.Quantity < quantity && objItem.IsInvoiceQuantityZero == 0 && ObjParameterInvoiceBillingQuantityZero == "false")
                    {
                        throw new Exception(messageException);
                    }

                    //Nuevo Detalle
                    if (transactionMasterDetailId == 0)
                    {
                        var objTmd = new TbTransactionMasterDetail
                        {
                            CompanyID = objTm.CompanyId,
                            TransactionID = TransactionId.Value,
                            TransactionMasterID = TransactionMasterId!.Value,
                            ComponentID = objComponentItem.ComponentID,
                            ComponentItemID = itemId,
                            Quantity = quantity * objItemSku.Value, // cantIdad
                            SkuQuantity = quantity, // cantIdad
                            SkuQuantityBySku = objItemSku.Value, // cantIdad
                            UnitaryCost = unitaryCost,
                            UnitaryPrice = price, // precio de lista
                            UnitaryAmount = unitaryAmount, // precio de lista con impuesto
                            Tax1 = tax1,
                            Tax2 = tax2,
                            Discount = 0,
                            PromotionID = 0,
                            Reference1 = lote,
                            Reference2 = vencimiento,
                            Reference3 = "0",
                            ItemNameLog = itemNameDetail,
                            ItemNameDescriptionLog = itemNameDetailDecription,
                            CatalogStatusID = 0,
                            InventoryStatusID = 0,
                            IsActive = true,
                            QuantityStock = 0,
                            QuantiryStockInTraffic = 0,
                            QuantityStockUnaswared = 0,
                            RemaingStock = 0,
                            ExpirationDate = null,
                            InventoryWarehouseSourceID = objTmNew.SourceWarehouseID,
                            InventoryWarehouseTargetID = objTmNew.TargetWarehouseID,
                            SkuCatalogItemID = skuCatalogItemId,
                            SkuFormatoDescription = skuFormatoDescription,
                            AmountCommision = price * comisionPorcentage * quantity // impuesto de lista
                        };
                        objTmd.Cost = objTmd.Quantity * unitaryCost; // costo por unIdad
                        objTmd.Amount = objTmd.Quantity * unitaryAmount; // precio de lista con impuesto por cantIdad

                        tax1Total = tax1Total + (tax1.Value * quantity);
                        tax2Total = tax2Total + (tax2.Value * quantity);
                        subAmountTotal = subAmountTotal + (quantity * price);
                        amountTotal = amountTotal + objTmd.Amount.Value;
                        transactionMasterDetailId = _objInterfazTransactionMasterDetailModel.InsertAppPosme(objTmd);

                        var objTmdc = new TbTransactionMasterDetailCredit();
                        objTmdc.TransactionMasterID = TransactionMasterId.Value;
                        objTmdc.TransactionMasterDetailID = transactionMasterDetailId;
                        objTmdc.Reference1 = string.IsNullOrEmpty(txtFixedExpenses.Text) ? "0" : txtFixedExpenses.Text;
                        objTmdc.Reference2 = txtReportSinRiesgo.IsOn ? "1" : "0";
                        objTmdc.Reference3 = "0"; //no existe el campo
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
                            var dataUpdatePrice = _objInterfazPriceModel.GetRowByPk(user.CompanyID, Convert.ToInt32(listPriceId), itemId, (int)typePriceId);
                            if (dataUpdatePrice is not null)
                            {
                                dataUpdatePrice.Price = price;
                                dataUpdatePrice.Percentage = percentage;

                                // Llamar al método de actualización de precio en el modelo de precio
                                _objInterfazPriceModel.UpdateAppPosme(user.CompanyID, Convert.ToInt32(listPriceId), itemId, (int)typePriceId, dataUpdatePrice);
                            }
                        }

                        //Ingresar la lista de productos de RESTAURANTE
                        if (objParameterINVOICE_BILLING_TRAKING_BAR.Equals("true", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var objTMDRNew = new TbTransactionMasterDetailReference
                            {
                                IsActive = 1,
                                CreatedOn = DateTime.Now,
                                Quantity = objTmd.Quantity.Value.ToString("F2"),
                                ComponentID = objTmd.ComponentID,
                                ComponentItemID = objTmd.ComponentItemID,
                                TransactionMasterDetailID = transactionMasterDetailId
                            };
                            _objInterfazTransactionMasterDetailReferencesModel.InsertAppPosme(objTMDRNew);
                        }
                    }
                    else
                    {
                        var objTmdc = _objInterfazTransactionMasterDetailCreditModel.GetRowByPk(transactionMasterDetailId);
                        var objTmdNew = _objInterfazTransactionMasterDetailModel.GetRowByPKK(transactionMasterDetailId);
                        if (objTmdNew is null) throw new Exception("No existe el objeto objTmdNew");
                        objTmdNew.Quantity = quantity * objItemSku.Value; // cantidad
                        objTmdNew.SkuQuantity = quantity; // cantidad
                        objTmdNew.SkuQuantityBySku = objItemSku.Value; // cantidad
                        objTmdNew.UnitaryCost = unitaryCost; // costo
                        objTmdNew.UnitaryPrice = price; // precio de lista
                        objTmdNew.UnitaryAmount = unitaryAmount; // precio de lista con impuesto
                        objTmdNew.Tax1 = tax1; // impuesto de lista
                        objTmdNew.Tax2 = tax2; //impuesto de servicio
                        objTmdNew.Amount = objTmdNew.Quantity * unitaryAmount; // precio de lista con impuesto por cantidad
                        objTmdNew.Reference1 = lote;
                        objTmdNew.Reference2 = vencimiento;
                        objTmdNew.Reference3 = "0";
                        objTmdNew.ItemNameLog = itemNameDetail;
                        objTmdNew.ItemNameDescriptionLog = itemNameDetailDecription;
                        objTmdNew.InventoryWarehouseSourceID = objTmNew.SourceWarehouseID;
                        objTmdNew.SkuCatalogItemID = skuCatalogItemId;
                        objTmdNew.SkuFormatoDescription = skuFormatoDescription;
                        objTmdNew.AmountCommision = price * comisionPorcentage * quantity;
                        objTmdNew.Cost = objTmdNew.Quantity * unitaryCost; // costo por cantidad


                        tax1Total = tax1Total + (tax1.Value * quantity);
                        tax2Total = tax2Total + (tax2.Value * quantity);
                        subAmountTotal = subAmountTotal + (quantity * price);
                        amountTotal = amountTotal + objTmdNew.Amount.Value;
                        var objTMDOld = _objInterfazTransactionMasterDetailModel.GetRowByPk(user.CompanyID, TransactionId.Value, TransactionMasterId.Value, transactionMasterDetailId, objComponentItem.ComponentID);
                        _objInterfazTransactionMasterDetailModel.UpdateAppPosme(user.CompanyID, TransactionId.Value, TransactionMasterId.Value, transactionMasterDetailId, objTmdNew);

                        objTmdc.Reference1 = string.IsNullOrEmpty(txtFixedExpenses.Text) ? "0" : txtFixedExpenses.Text;
                        objTmdc.Reference2 = txtReportSinRiesgo.IsOn ? "1" : "0";
                        objTmdc.Reference3 = ""; //no existe el campo
                        objTmdc.Reference4 = "";
                        objTmdc.Reference5 = "";
                        objTmdc.Reference9 = "reference1: Porcentaje de Gastos Fijos para las Facturas de Credito,reference2: Escritura Publica,reference3: Primer Linea del Protocolo";
                        _objInterfazTransactionMasterDetailCreditModel.UpdateAppPosme(transactionMasterDetailId, objTmdc);

                        //Actualizar el Precio
                        if (objUpdatePrice == "true")
                        {
                            var dataUpdatePrice = _objInterfazPriceModel.GetRowByPk(user.CompanyID, Convert.ToInt32(listPriceId), itemId, (int)typePriceId);
                            if (dataUpdatePrice is not null)
                            {
                                dataUpdatePrice.Price = price;
                                dataUpdatePrice.Percentage = unitaryCost == 0 ? (price / 100) : (((100 * price) / unitaryCost) - 100);
                                _objInterfazPriceModel.UpdateAppPosme(user.CompanyID, Convert.ToInt32(listPriceId), itemId, (int)typePriceId, dataUpdatePrice);
                            }
                        }

                        //Ingresar la lista de productos de RESTAURANTE
                        if (objParameterINVOICE_BILLING_TRAKING_BAR.Equals("true", StringComparison.InvariantCultureIgnoreCase) && objTmdNew.Quantity.Value > objTMDOld.Quantity.Value)
                        {
                            //$quantityRestaranteTraking 					= $objTMDNew["quantity"] - $objTMDOld->quantity;
                            var quantityRestaranteTraking = objTmdNew.Quantity.Value - objTMDOld.Quantity.Value;
                            var objTMDRNew = new TbTransactionMasterDetailReference
                            {
                                IsActive = 1,
                                CreatedOn = DateTime.Now,
                                Quantity = quantityRestaranteTraking.ToString("F2"),
                                ComponentID = objComponentItem.ComponentID,
                                ComponentItemID = itemId,
                                TransactionMasterDetailID = transactionMasterDetailId
                            };
                            _objInterfazTransactionMasterDetailReferencesModel.InsertAppPosme(objTMDRNew);
                        }
                    }
                }
            }

            //Actualizar Transaccion
            amountTotal = amountTotal - varDescuento;
            objTmNew.SubAmount = subAmountTotal;
            objTmNew.Tax1 = tax1Total;
            objTmNew.Tax2 = tax2Total;
            objTmNew.Tax4 = varPorcentajeDescuento;
            objTmNew.Discount = varDescuento;
            objTmNew.Amount = amountTotal;
            _objInterfazTransactionMasterModel.UpdateAppPosme(user.CompanyID, TransactionId.Value, TransactionMasterId.Value, objTmNew);

            //Aplicar el Documento?
            var COMMAND_APLICABLE = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_APLICABLE"]);
            if (
                _objInterfazCoreWebWorkflow.ValidateWorkflowStage
                (
                    "tb_transaction_master_billing",
                    "statusID",
                    objTmNew.StatusID!.Value,
                    COMMAND_APLICABLE,
                    user.CompanyID,
                    user.BranchID,
                    role.RoleID
                )!.Value && oldStatusId != objTmNew.StatusID
            )
            {
                //Actualizar el numero de factura
                var objTmNew003 = _objInterfazTransactionMasterModel.GetRowByTransactionMasterId(user.CompanyID, TransactionMasterId.Value);
                objTmNew003.TransactionNumber = _objInterfazCoreWebCounter.GoNextNumber(user.CompanyID, user.BranchID, "tb_transaction_master_billing", 0);
                _objInterfazTransactionMasterModel.UpdateAppPosme(user.CompanyID, TransactionId.Value, TransactionMasterId.Value, objTmNew003);


                //Acumular punto del cliente.
                if (objTmInfoNew.ReceiptAmountPoint <= 0 && objTmNew.CurrencyID == ObjCurrencyCordoba.CurrencyID)
                {
                    var objCustomer = _objInterfazCustomerModel.GetRowByPKK(objTmNew.EntityID.Value);
                    objCustomer.BalancePoint = objCustomer.BalancePoint + amountTotal;
                    _objInterfazCustomerModel.UpdateAppPosme(objCustomer.CompanyID, objCustomer.BranchID, objCustomer.EntityID, objCustomer);
                }

                //Es pago con punto restar puntos
                if (objTmInfoNew.ReceiptAmountPoint > 0 && objTmNew.CurrencyID == ObjCurrencyCordoba.CurrencyID)
                {
                    var objCustomer = _objInterfazCustomerModel.GetRowByPKK(objTmNew.EntityID.Value);
                    objCustomer.BalancePoint = objCustomer.BalancePoint - objTmInfoNew.ReceiptAmountPoint;
                    _objInterfazCustomerModel.UpdateAppPosme(objCustomer.CompanyID, objCustomer.BranchID, objCustomer.EntityID, objCustomer);
                }

                //Ingresar en Kardex.
                VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebInventory>().CalculateKardexNewOutput(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);

                //Crear Conceptos.
                VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebConcept>().Billing(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);

                //Si es al credito crear tabla de amortizacion
                string[] causalIDTypeCredit = ParameterCausalTypeCredit!.Value!.Split(',');
                var exisCausalInCredit = causalIDTypeCredit.Any(c => c == objTmNew.TransactionCausalID.ToString());

                //si la factura es de credito
                if (exisCausalInCredit)
                {
                    //Crear documento del modulo
                    var objCustomerCreditLine = _objInterfazCustomerCreditLineModel.GetRowByPk(Convert.ToInt32(objTmNew.Reference4));
                    var objCustomerCreditDocument = new TbCustomerCreditDocument
                    {
                        CompanyID = user.CompanyID,
                        EntityID = objCustomerCreditLine.EntityID,
                        CustomerCreditLineID = objCustomerCreditLine.CustomerCreditLineID,
                        DocumentNumber = objTmNew003.TransactionNumber,
                        DateOn = objTmNew.TransactionOn.Value,
                        ExchangeRate = objTmNew.ExchangeRate.Value,
                        Interes = objCustomerCreditLine.InterestYear,
                        Term = objCustomerCreditLine!.Term!.Value,
                        Amount = amountTotal,
                        Balance = amountTotal
                    };

                    var objCatalogItemDayExclude = _objInterfazCatalogItemModel.GetRowByCatalogItemId(objCustomerCreditLine.DayExcluded.Value);

                    if (objParameterAmortizationDuranteFactura == "true" && objTmNew.CurrencyID == 1 /*cordoba*/)
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


                        objCustomerCreditDocument.Amount = amount;
                        objCustomerCreditDocument.Balance = amount;
                    }

                    if (objParameterAmortizationDuranteFactura == "true" && objTmNew.CurrencyID == 2 /*dolares*/)
                    {
                        objCustomerCreditDocument.Term = Convert.ToInt32(objTmNew.Reference2);
                        objCustomerCreditDocument.Amount = amountTotal -
                                                           objTmInfoNew.ReceiptAmountPoint.Value -
                                                           objTmInfoNew.ReceiptAmount.Value -
                                                           objTmInfoNew.ReceiptAmountBank -
                                                           objTmInfoNew.ReceiptAmountCard -
                                                           Math.Round(objTmInfoNew.ReceiptAmountBankDol / objTmNew.ExchangeRate.Value, 2) -
                                                           Math.Round(objTmInfoNew.ReceiptAmountCardDol / objTmNew.ExchangeRate.Value, 2) -
                                                           Math.Round(objTmInfoNew.ReceiptAmountDol / objTmNew.ExchangeRate.Value, 2);
                        objCustomerCreditDocument.Balance = objCustomerCreditDocument.Amount;
                    }

                    objCustomerCreditDocument.CurrencyID = objTmNew.CurrencyID.Value;
                    objCustomerCreditDocument.StatusID = _objInterfazCoreWebWorkflow.GetWorkflowInitStage("tb_customer_credit_document", "statusID", user.CompanyID, user.BranchID, role.RoleID)![0].WorkflowStageID;
                    objCustomerCreditDocument.Reference1 = objTmNew.Note;
                    objCustomerCreditDocument.Reference2 = "";
                    objCustomerCreditDocument.Reference3 = "";
                    objCustomerCreditDocument.IsActive = true;
                    objCustomerCreditDocument.ProviderIDCredit = Convert.ToInt32(objTmNew.Reference1);
                    objCustomerCreditDocument.PeriodPay = objCustomerCreditLine.PeriodPay;

                    if (objParameterAmortizationDuranteFactura == "true")
                    {
                        objCustomerCreditDocument.PeriodPay = objTmNew.PeriodPay.Value;
                        objCatalogItemDayExclude = _objInterfazCatalogItemModel.GetRowByCatalogItemId(objTmNew.DayExcluded.Value);
                    }

                    objCustomerCreditDocument.TypeAmortization = objCustomerCreditLine.TypeAmortization;
                    objCustomerCreditDocument.ReportSinRiesgo = txtReportSinRiesgo.IsOn ? 1 : 0;
                    var customerCreditDocumentID = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerCreditDocumentModel>().InsertAppPosme(objCustomerCreditDocument);
                    var periodPay = VariablesGlobales.Instance.UnityContainer.Resolve<ICatalogItemModel>().GetRowByCatalogItemId(objCustomerCreditLine.PeriodPay);

                    if (objParameterAmortizationDuranteFactura == "true")
                    {
                        periodPay = VariablesGlobales.Instance.UnityContainer.Resolve<ICatalogItemModel>().GetRowByCatalogItemId(objTmNew.PeriodPay.Value);
                    }

                    var objCatalogItem_DiasNoCobrables = _objInterfazCoreWebCatalog.GetCatalogAllItemByNameCatalogo("CXC_NO_COBRABLES", user.CompanyID);
                    var objCatalogItem_DiasFeriados365 = _objInterfazCoreWebCatalog.GetCatalogAllItemByNameCatalogo("CXC_NO_COBRABLES_FERIADOS_365", user.CompanyID);
                    var objCatalogItem_DiasFeriados366 = _objInterfazCoreWebCatalog.GetCatalogAllItemByNameCatalogo("CXC_NO_COBRABLES_FERIADOS_366", user.CompanyID);

                    //Crear tabla de amortizacion
                    var objInterfazFinaicialAmortization = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebFinancialAmort>();
                    objInterfazFinaicialAmortization.Amort(
                        objCustomerCreditDocument.Amount, /*monto*/
                        objCustomerCreditDocument.Interes, /*interes anual*/
                        objCustomerCreditDocument.Term, /*numero de pagos*/
                        periodPay.Sequence!.Value, /*frecuencia de pago en dia*/
                        objTmNew.TransactionOn2, /*fecha del credito*/
                        objCustomerCreditLine.TypeAmortization /*tipo de amortizacion*/,
                        objCatalogItem_DiasNoCobrables,
                        objCatalogItem_DiasFeriados365,
                        objCatalogItem_DiasFeriados366,
                        objCatalogItemDayExclude!,
                        VariablesGlobales.Instance.Company.FlavorID
                    );

                    var tableAmortization = objInterfazFinaicialAmortization.GetTable();
                    if (tableAmortization.ListDetailDto is not null && tableAmortization.ListDetailDto.Count > 0)
                    {
                        foreach (var itemAmortization in tableAmortization.ListDetailDto)
                        {
                            var objCustomerAmoritizacion = new TbCustomerCreditAmoritization
                            {
                                CustomerCreditDocumentID = customerCreditDocumentID,
                                BalanceStart = itemAmortization!.SaldoInicial!.Value,
                                DateApply = itemAmortization.Date!.Value,
                                Interest = itemAmortization.Interes!.Value,
                                Capital = itemAmortization.Principal!.Value,
                                Share = itemAmortization.Cuota!.Value,
                                BalanceEnd = itemAmortization.Saldo!.Value,
                                Remaining = itemAmortization.Cuota.Value,
                                DayDelay = 0,
                                Note = "",
                                StatusID = _objInterfazCoreWebWorkflow.GetWorkflowInitStage("tb_customer_credit_amoritization", "statusID", user.CompanyID, user.BranchID, role.RoleID)![0].WorkflowStageID,
                                IsActive = true
                            };
                            _objInterfazCustomerCreditAmortizationModel.InsertAppPosme(objCustomerAmoritizacion);
                        }
                    }

                    //Crear las personas relacionadas a la factura
                    var objEntityRelated = new TbCustomerCreditDocumentEntityRelated();
                    objEntityRelated.CustomerCreditDocumentID = customerCreditDocumentID;
                    objEntityRelated.EntityID = objCustomerCreditLine.EntityID;
                    objEntityRelated.Type = Convert.ToInt32(_objInterfazCoreWebParameter.GetParameter("CXC_PROPIETARIO_DEL_CREDITO", user.CompanyID)!.Value);
                    objEntityRelated.TypeCredit = 401; // Comercial
                    objEntityRelated.StatusCredit = 429; // Activo
                    objEntityRelated.TypeGarantia = 444; // Pagare
                    objEntityRelated.TypeRecuperation = 450; // Recuperación normal
                    objEntityRelated.RatioDesembolso = 1;
                    objEntityRelated.RatioBalance = 1;
                    objEntityRelated.RatioBalanceExpired = 1;
                    objEntityRelated.RatioShare = 1;
                    objEntityRelated.IsActive = true;
                    VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebAuditoria>().SetAuditCreated(objEntityRelated, user, "");
                    VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerCreditDocumentEntityRelatedModel>().InsertAppPosme(objEntityRelated);

                    var montoTotalCordobaCredit = objTmNew.CurrencyID == 1 ? objCustomerCreditDocument.Amount : Math.Round((objCustomerCreditDocument.Amount * objTmNew.ExchangeRate.Value), 2);
                    var montoTotalDolaresCredit = objTmNew.CurrencyID == 2 ? objCustomerCreditDocument.Amount : Math.Round((objCustomerCreditDocument.Amount / objTmNew.ExchangeRate.Value), 2);


                    //disminuir el balance de general	
                    var objCustomerCredit = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerCreditModel>().GetRowByPk(objCustomerCreditLine.CompanyID, objCustomerCreditLine.BranchID, objCustomerCreditLine.EntityID);
                    objCustomerCredit.BalanceDol = objCustomerCredit.BalanceDol - montoTotalDolaresCredit;
                    VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerCreditModel>().UpdateAppPosme(objCustomerCreditLine.CompanyID, objCustomerCreditLine.BranchID, objCustomerCreditLine.EntityID, objCustomerCredit);

                    //disminuir el balance de linea
                    decimal balance;
                    if (objCustomerCreditLine.CurrencyID == ObjCurrencyCordoba.CurrencyID)
                        balance = objCustomerCreditLine.Balance - montoTotalCordobaCredit;
                    else
                        balance = objCustomerCreditLine.Balance - montoTotalDolaresCredit;

                    var objCustomerCreditLineNew = _objInterfazCustomerCreditLineModel.GetRowByPk(objCustomerCreditLine.CustomerCreditLineID);
                    objCustomerCreditLineNew.Balance = balance;
                    _objInterfazCustomerCreditLineModel.UpdateAppPosme(objCustomerCreditLine.CustomerCreditLineID, objCustomerCreditLineNew);
                }
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
                var objParameterPrinterName = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_PRINTER_DIRECT_NAME_DEFAULT", user.CompanyID)!.Value;
                var coreWebPrinter = new CoreWebPrinterDirect();
                var pd = coreWebPrinter.ConfigurationPrinter(objParameterPrinterName);
                var printer = new Printer(objParameterPrinterName);
                printer.OpenDrawer();
                printer.PrintDocument();
            }
            catch (Exception ex)
            {
                new CoreWebRenderInView().GetMessageAlert(TypeError.Error, "Error", $"Error al imprimir: {ex.Message}", this);
            }
        }

        public void PreRender()
        {
            var objWebToolsCustomizationViewHelper = new WebToolsCustomizationViewHelper();
            var objCoreWebRenderInView = new CoreWebRenderInView();
            var objCompany = VariablesGlobales.Instance.Company;
            HelperMethods.OnlyNumberDecimals(FormInvoiceBillingEditPayment.txtDescuento);
            HelperMethods.OnlyNumberDecimals(FormInvoiceBillingEditPayment.txtReceiptAmount);
            HelperMethods.OnlyNumberDecimals(FormInvoiceBillingEditPayment.txtReceiptAmountDol);
            HelperMethods.OnlyNumberDecimals(FormInvoiceBillingEditPayment.txtReceiptAmountBank);
            HelperMethods.OnlyNumberDecimals(FormInvoiceBillingEditPayment.txtReceiptAmountBankDol);
            HelperMethods.OnlyNumberDecimals(FormInvoiceBillingEditPayment.txtReceiptAmountTarjeta);
            HelperMethods.OnlyNumberDecimals(FormInvoiceBillingEditPayment.txtReceiptAmountTarjetaDol);
            HelperMethods.OnlyNumberDecimals(FormInvoiceBillingEditPayment.txtReceiptAmountPoint);

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

            colTransactionDetailName.OptionsColumn.AllowEdit = varPermisosEsPermitidoModificarNombre;
            colPrice.OptionsColumn.AllowEdit = varPermisosEsPermitidoModificarPrecio;
            colAccionMas.Caption = "Mas";
            colAccionMenos.Caption = "Menos";
            colAccionPrecios.Caption = "Precios";
            ObjSELECCIONAR_ITEM_BILLING_BACKGROUND = VariablesGlobales.Instance.ObjSELECCIONAR_ITEM_BILLING_BACKGROUND;


            //Personalizar Label ZONAS
            string? targetControlLabelZone = objWebToolsCustomizationViewHelper.GetBehavior(objCompany.Type, "app_invoice_billing", "divLabelZoneControlPosition", "");
            labelControl12.Text = objWebToolsCustomizationViewHelper.GetBehavior(objCompany.Type, "app_invoice_billing", "divLabelZone", "Zona");
            objCoreWebRenderInView.MoveControlToSource(targetControlLabelZone, "labelControl12", this);
            objCoreWebRenderInView.ChangeSizeAutoSizeMode(labelControl12, this);
            objCoreWebRenderInView.ChangePositionX("labelControl12", objWebToolsCustomizationViewHelper.GetBehavior(objCompany.Type, "app_invoice_billing", "divLabelZoneX", "0"), this);

            //Personalizar Control ZONAS
            string? targetControlSourceName = objWebToolsCustomizationViewHelper.GetBehavior(objCompany.Type, "app_invoice_billing", "divControlZoneControlPosition", "");
            objCoreWebRenderInView.MoveControlToSource(targetControlSourceName, "txtZoneID", this);
            objCoreWebRenderInView.ChangeWidth("txtZoneID", objWebToolsCustomizationViewHelper.GetBehavior(objCompany.Type, "app_invoice_billing", "divControlZoneWidth", "0"), this);


            //Personalizar Label Identificacion del cliente
            //labelControl6.Visible                   = Convert.ToBoolean(objWebToolsCustomizationViewHelper.GetBehavior(objCompany.Type, "app_invoice_billing", "divLabelShowWindowIdentificationCustomer", "true"));
            //txtReferenceClientIdentifier.Visible    = Convert.ToBoolean(objWebToolsCustomizationViewHelper.GetBehavior(objCompany.Type, "app_invoice_billing", "divControlShowWindowIdentificationCustomer", "true"));
            objCoreWebRenderInView.ChangeHigth("txtReferenceClientIdentifier", objWebToolsCustomizationViewHelper.GetBehavior(objCompany.Type, "app_invoice_billing", "divControlIdentificationCustomerHigth", "0"), this);
            objCoreWebRenderInView.ChangeWidth("txtReferenceClientIdentifier", objWebToolsCustomizationViewHelper.GetBehavior(objCompany.Type, "app_invoice_billing", "divControlIdentificationCustomerWidth", "0"), this);
        }


        public void LoadRender(TypeRender typeRender)
        {
            switch (typeRender)
            {
                case TypeRender.New:
                    _bindingListTransactionMasterDetail.Clear();
                    var employerDefault = ObjParameterInvoiceBillingEmployeeDefault;
                    if (employerDefault == "true")
                        CoreWebRenderInView.LlenarComboBox(ObjListEmployee, txtEmployeeID, "EntityId", "FirstName", ObjListEmployee.ElementAt(0).EntityId);
                    else
                        CoreWebRenderInView.LlenarComboBox(ObjListEmployee, txtEmployeeID, "EntityId", "FirstName", null);

                    CoreWebRenderInView.LlenarComboBox(ObjListCurrency, txtCurrencyID, "CurrencyId", "Name", ObjListCurrency.ElementAt(0).CurrencyId);
                    CoreWebRenderInView.LlenarComboBox(ObjListZone, txtZoneID, "CatalogItemID", "Name", ObjListZone.ElementAt(0).CatalogItemID);
                    CoreWebRenderInView.LlenarComboBox(ObjListWarehouse, txtWarehouseID, "WarehouseId", "Name", ObjListWarehouse.ElementAt(0).WarehouseId);
                    CoreWebRenderInView.LlenarComboBox(ObjListMesa, txtMesaID, "CatalogItemID", "Name", ObjListMesa.ElementAt(0).CatalogItemID);
                    CoreWebRenderInView.LlenarComboBox(ListProvider, txtReference1, "EntityId", "FirstName", ListProvider.ElementAt(0).EntityId);
                    CoreWebRenderInView.LlenarComboBox(ObjListPay, txtPeriodPay, "CatalogItemID", "Name", ObjParameterCxcFrecuenciaPayDefault);
                    CoreWebRenderInView.LlenarComboBox(ObjListDayExcluded, txtDayExcluded, "CatalogItemID", "Name", objParameterCXC_DAY_EXCLUDED_IN_CREDIT);
                    CoreWebRenderInView.LlenarComboBox(ObjCausal, txtCausalID, "TransactionCausalID", "Name", ObjCausal.ElementAt(0).TransactionCausalID);
                    CoreWebRenderInView.LlenarComboBox(ObjListTypePrice, txtTypePriceID, "CatalogItemID", "Name", ObjListTypePrice.ElementAt(0).CatalogItemID);
                    CoreWebRenderInView.LlenarComboBox(ObjListBank, FormInvoiceBillingEditPayment.txtReceiptAmountTarjeta_BankID, "BankID", "Name", ObjListBank.ElementAt(0).BankID);
                    CoreWebRenderInView.LlenarComboBox(ObjListBank, FormInvoiceBillingEditPayment.txtReceiptAmountTarjetaDol_BankID, "BankID", "Name", ObjListBank.ElementAt(0).BankID);
                    CoreWebRenderInView.LlenarComboBox(ObjListBank, FormInvoiceBillingEditPayment.txtReceiptAmountBank_BankID, "BankID", "Name", ObjListBank.ElementAt(0).BankID);
                    CoreWebRenderInView.LlenarComboBox(ObjListBank, FormInvoiceBillingEditPayment.txtReceiptAmountBankDol_BankID, "BankID", "Name", ObjListBank.ElementAt(0).BankID);
                    lblTitulo.Text = @"Factura: #00000000";
                    txtExchangeRate.Text = ExchangeRate.ToString(CultureInfo.InvariantCulture);
                    txtCustomerDescription.Text = ObjNaturalDefault is not null ? ($"{ObjCustomerDefault.CustomerNumber} {ObjNaturalDefault!.FirstName!.ToUpper()} {ObjNaturalDefault!.LastName!.ToUpper()}") : ($"{ObjCustomerDefault.CustomerNumber} {ObjLegalDefault!.ComercialName!.ToUpper()}");
                    txtDate.DateTime = DateTime.Today;
                    txtNote.Text = string.Empty;
                    TxtCustomerId = ObjCustomerDefault.EntityID;
                    txtReferenceClientName.Text = string.Empty;
                    txtReferenceClientIdentifier.Text = string.Empty;
                    txtNumberPhone.Text = string.Empty;
                    txtNextVisit.EditValue = DateTime.MinValue;
                    txtFixedExpenses.Text = string.Empty;
                    txtReference2.Text = ObjParameterCxcPlazoDefault;
                    txtReference3.Text = ObjEmployeeNatural is null ? "N/D" : ObjEmployeeNatural.FirstName;
                    txtIsApplied.Checked = false;
                    txtDateFirst.DateTime = DateTime.Today;
                    txtDesembolsoEfectivo.IsOn = false;
                    txtReportSinRiesgo.IsOn = false;
                    TxtStatusOldId = ObjListWorkflowStage!.ElementAt(0).WorkflowStageID;
                    TxtStatusId = ObjListWorkflowStage!.ElementAt(0).WorkflowStageID;
                    lblTotal.Text = @"0.0";
                    txtLayFirstLineProtocolo.Text = "";
                    txtCheckApplyExoneracion.IsOn = false;
                    FormInvoiceBillingEditPayment.txtSubTotal.Text = @"0.0";
                    FormInvoiceBillingEditPayment.txtIva.Text = @"0.0";
                    FormInvoiceBillingEditPayment.txtPorcentajeDescuento.Text = @"0.0";
                    FormInvoiceBillingEditPayment.txtDescuento.Text = @"0.0";
                    FormInvoiceBillingEditPayment.txtServices.Text = @"0.0";
                    FormInvoiceBillingEditPayment.txtTotal.Text = @"0.0";
                    FormInvoiceBillingEditPayment.txtSubTotal.Text = @"0.0";
                    FormInvoiceBillingEditPayment.txtIva.Text = @"0.0";
                    FormInvoiceBillingEditPayment.txtTotal.Text = @"0.0";
                    FormInvoiceBillingEditPayment.txtChangeAmount.Text = @"0.0";
                    FormInvoiceBillingEditPayment.txtReceiptAmount.Text = @"0.0";
                    FormInvoiceBillingEditPayment.txtReceiptAmountDol.Text = @"0.0";
                    FormInvoiceBillingEditPayment.txtReceiptAmountTarjeta.Text = @"0.0";
                    FormInvoiceBillingEditPayment.txtReceiptAmountTarjeta_Reference.Text = string.Empty;
                    FormInvoiceBillingEditPayment.txtReceiptAmountTarjetaDol.Text = @"0.0";
                    FormInvoiceBillingEditPayment.txtReceiptAmountTarjetaDol_Reference.Text = string.Empty;
                    FormInvoiceBillingEditPayment.txtReceiptAmountBank.Text = @"0.0";
                    FormInvoiceBillingEditPayment.txtReceiptAmountBank_Reference.Text = string.Empty;
                    FormInvoiceBillingEditPayment.txtReceiptAmountBankDol.Text = @"0.0";
                    FormInvoiceBillingEditPayment.txtReceiptAmountBankDol_Reference.Text = string.Empty;
                    FormInvoiceBillingEditPayment.txtReceiptAmountPoint.Text = @"0.0";
                    txtNote.Text = "sin comentario...";

                    //Llenar Linea de Credito
                    FnRenderLineaCredit(ObjListCustomerCreditLine, ParameterCausalTypeCredit!);
                    FormInvoiceBillingEditPayment.btnAplicar.Visible = false;

                    btnPrinterFooter.Visibility = ObjParameterShowComandoDeCocina!.ToUpper() == "false".ToUpper() ? BarItemVisibility.Never : BarItemVisibility.Always;
                    btnPrinterBar.Visibility = ObjParameterInvoiceBillingShowCommandBar!.ToUpper() == "false".ToUpper() ? BarItemVisibility.Never : BarItemVisibility.Always;

                    break;
                case TypeRender.Edit:
                    _bindingListTransactionMasterDetail.Clear();

                    CoreWebRenderInView.LlenarComboBox(ObjListCurrency, txtCurrencyID, "CurrencyId", "Name", ObjTransactionMaster!.CurrencyId);
                    CoreWebRenderInView.LlenarComboBox(ObjListZone, txtZoneID, "CatalogItemID", "Name", ObjTransactionMasterInfo!.ZoneId);
                    CoreWebRenderInView.LlenarComboBox(ObjListWarehouse, txtWarehouseID, "WarehouseId", "Name", ObjTransactionMaster.SourceWarehouseId);
                    CoreWebRenderInView.LlenarComboBox(ObjListEmployee, txtEmployeeID, "EntityId", "FirstName", ObjTransactionMaster.EntityIdsecondary);
                    CoreWebRenderInView.LlenarComboBox(ObjListMesa, txtMesaID, "CatalogItemID", "Name", ObjTransactionMasterInfo.MesaId);
                    CoreWebRenderInView.LlenarComboBox(ListProvider, txtReference1, "EntityId", "FirstName", ObjTransactionMaster.Reference1);
                    CoreWebRenderInView.LlenarComboBox(ObjListPay, txtPeriodPay, "CatalogItemID", "Name", ObjTransactionMaster.PeriodPay);
                    CoreWebRenderInView.LlenarComboBox(ObjListDayExcluded, txtDayExcluded, "CatalogItemID", "Name", ObjTransactionMaster.DayExcluded);
                    CoreWebRenderInView.LlenarComboBox(ObjCausal, txtCausalID, "TransactionCausalID", "Name", ObjTransactionMaster.TransactionCausalId);
                    CoreWebRenderInView.LlenarComboBox(ObjListTypePrice, txtTypePriceID, "CatalogItemID", "Name", ObjListTypePrice.ElementAt(0).CatalogItemID);
                    CoreWebRenderInView.LlenarComboBox(ObjListBank, FormInvoiceBillingEditPayment.txtReceiptAmountTarjeta_BankID, "BankID", "Name", ObjTransactionMasterInfo.ReceiptAmountCardBankId);
                    CoreWebRenderInView.LlenarComboBox(ObjListBank, FormInvoiceBillingEditPayment.txtReceiptAmountTarjetaDol_BankID, "BankID", "Name", ObjTransactionMasterInfo.ReceiptAmountCardBankDolId);
                    CoreWebRenderInView.LlenarComboBox(ObjListBank, FormInvoiceBillingEditPayment.txtReceiptAmountBank_BankID, "BankID", "Name", ObjTransactionMasterInfo.ReceiptAmountBankId);
                    CoreWebRenderInView.LlenarComboBox(ObjListBank, FormInvoiceBillingEditPayment.txtReceiptAmountBankDol_BankID, "BankID", "Name", ObjTransactionMasterInfo.ReceiptAmountBankDolId);

                    lblTitulo.Text = $@"Factura: #{ObjTransactionMaster.TransactionNumber}";
                    txtExchangeRate.Text = ExchangeRate.ToString(CultureInfo.InvariantCulture);
                    txtCustomerDescription.Text = ObjNaturalDefault is not null ? ($"{ObjCustomerDefault.CustomerNumber} {ObjNaturalDefault!.FirstName!.ToUpper()} {ObjNaturalDefault!.LastName!.ToUpper()}") : ($"{ObjCustomerDefault.CustomerNumber} {ObjLegalDefault!.ComercialName!.ToUpper()}");
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
                    txtLayFirstLineProtocolo.Text = ObjTransactionMasterReferences.Reference1;
                    txtCheckApplyExoneracion.IsOn = !string.IsNullOrWhiteSpace(ObjTransactionMasterReferences.Reference2) && ObjTransactionMasterReferences.Reference2.Equals("1");
                    if (ObjTransactionMasterDetailCredit is not null)
                    {
                        txtFixedExpenses.Text = ObjTransactionMasterDetailCredit.Reference1;
                        txtReportSinRiesgo.IsOn = !string.IsNullOrWhiteSpace(ObjTransactionMasterDetailCredit.Reference2) && ObjTransactionMasterDetailCredit.Reference2.Equals("1", StringComparison.InvariantCultureIgnoreCase);
                    }
                    else
                    {
                        txtFixedExpenses.Text = string.Empty;
                    }

                    txtIsApplied.Checked = ObjTransactionMaster.IsApplied!.Value;
                    txtDateFirst.DateTime = ObjTransactionMaster.TransactionOn2!.Value;
                    txtReference2.Text = ObjTransactionMaster.Reference2;
                    txtDesembolsoEfectivo.IsOn = true; //txtCheckDeEfectivo
                    TxtStatusOldId = ObjTransactionMaster.StatusId;
                    TxtStatusId = ObjTransactionMaster.StatusId;
                    FormInvoiceBillingEditPayment.txtSubTotal.Text = @"0.0";
                    FormInvoiceBillingEditPayment.txtIva.Text = @"0.0";
                    FormInvoiceBillingEditPayment.txtPorcentajeDescuento.Text = @"0.0";
                    FormInvoiceBillingEditPayment.txtDescuento.Text = @"0.0";
                    FormInvoiceBillingEditPayment.txtServices.Text = @"0.0";
                    FormInvoiceBillingEditPayment.txtTotal.Text = @"0.0";

                    //Llenar Linea de Credito
                    FnRenderLineaCredit(ObjListCustomerCreditLine, ParameterCausalTypeCredit!);
                    FormInvoiceBillingEditPayment.txtDescuento.Text = ObjTransactionMaster.Discount.Value.ToString(FormatDecimal);
                    FormInvoiceBillingEditPayment.txtPorcentajeDescuento.Text = ObjTransactionMaster.Tax4.Value.ToString(FormatDecimal);

                    if (ObjTransactionMasterDetail.Count > 0)
                    {
                        var aux = 0;
                        foreach (var itemDto in ObjTransactionMasterDetail)
                        {
                            var precio2 = ObjTransactionMasterItemPrice.First(c => c.ItemID == itemDto.ComponentItemId && c.TypePriceID == (int)TypePrice.PorMayor).Price;
                            var precio3 = ObjTransactionMasterItemPrice.First(c => c.ItemID == itemDto.ComponentItemId && c.TypePriceID == (int)TypePrice.Credito).Price;
                            var objConcept = ObjTransactionMasterDetailConcept.Where(c => c.ComponentItemID == itemDto.ComponentItemId && c.Name == "IVA").ToList();
                            var objConceptTaxService = ObjTransactionMasterDetailConcept.Where(c => c.ComponentItemID == itemDto.ComponentItemId && c.Name == "TAX_SERVICES").ToList();
                            var Iva = objConcept.Count == 0 ? decimal.Zero : objConcept.ElementAt(0).ValueOut;
                            var taxServices = objConceptTaxService.Count == 0 ? decimal.Zero : objConceptTaxService.ElementAt(0).ValueOut;
                            var billingEdit = new FormInvoiceBillingEditDetailDTO
                            {
                                TransactionMasterDetailId = itemDto.TransactionMasterDetailId,
                                ItemId = itemDto.ComponentItemId!.Value,
                                ItemNumber = itemDto.ItemNumber!,
                                TransactionDetailName = itemDto.ItemNameLog!,
                                Sku = itemDto.SkuCatalogItemId,
                                Quantity = itemDto.SkuQuantity,
                                Price = decimal.Round(itemDto.UnitaryPrice!.Value * itemDto.SkuQuantityBySku, 2, MidpointRounding.AwayFromZero),
                                SubTotal = decimal.Round(itemDto.UnitaryPrice.Value * itemDto.SkuQuantityBySku * itemDto.SkuQuantity, 2, MidpointRounding.AwayFromZero),
                                Iva = Iva ?? 0m,
                                TaxServices = taxServices ?? 0m,
                                SkuQuantityBySku = itemDto.SkuQuantityBySku,
                                UnitaryPriceIndividual = itemDto.UnitaryPrice!.Value,
                                SkuFormatoDescription = itemDto.SkuFormatoDescription,
                                ItemPrecio2 = precio2,
                                ItemPrecio3 = precio3,
                                AccionMas = "",
                                AccionMenos = "",
                                AccionPrecios = decimal.Zero
                            };

                            _bindingListTransactionMasterDetail.Add(billingEdit);
                        }
                    }

                    //Refrescar
                    FnRefrechDetail();
                    FnRecalculateDetail(false, "");

                    //Renderizar Pagos                    
                    FormInvoiceBillingEditPayment.txtReceiptAmount.Text = ObjTransactionMasterInfo.ReceiptAmount!.Value.ToString(FormatDecimal);
                    FormInvoiceBillingEditPayment.txtReceiptAmountDol.Text = ObjTransactionMasterInfo.ReceiptAmountDol.ToString(FormatDecimal);
                    FormInvoiceBillingEditPayment.txtReceiptAmountTarjeta.Text = ObjTransactionMasterInfo.ReceiptAmountCard.ToString(FormatDecimal);
                    FormInvoiceBillingEditPayment.txtReceiptAmountTarjeta_Reference.Text = ObjTransactionMasterInfo.ReceiptAmountCardBankReference;
                    FormInvoiceBillingEditPayment.txtReceiptAmountTarjetaDol.Text = ObjTransactionMasterInfo.ReceiptAmountCardDol.ToString(FormatDecimal);
                    FormInvoiceBillingEditPayment.txtReceiptAmountTarjetaDol_Reference.Text = ObjTransactionMasterInfo.ReceiptAmountBankDolReference;
                    FormInvoiceBillingEditPayment.txtReceiptAmountBank.Text = ObjTransactionMasterInfo.ReceiptAmountBank.ToString(FormatDecimal);
                    FormInvoiceBillingEditPayment.txtReceiptAmountBank_Reference.Text = ObjTransactionMasterInfo.ReceiptAmountBankReference;
                    FormInvoiceBillingEditPayment.txtReceiptAmountBankDol.Text = ObjTransactionMasterInfo.ReceiptAmountBankDol.ToString(FormatDecimal);
                    FormInvoiceBillingEditPayment.txtReceiptAmountBankDol_Reference.Text = ObjTransactionMasterInfo.ReceiptAmountBankDolReference;
                    FormInvoiceBillingEditPayment.txtReceiptAmountPoint.Text = ObjTransactionMasterInfo.ReceiptAmountPoint!.Value.ToString(FormatDecimal);
                    FormInvoiceBillingEditPayment.txtChangeAmount.Text = ObjTransactionMasterInfo.ChangeAmount.ToString(FormatDecimal);
                    FormInvoiceBillingEditPayment.btnAplicar.Visible = true;

                    btnPrinterFooter.Visibility = ObjParameterShowComandoDeCocina!.ToUpper() == "false".ToUpper() ? BarItemVisibility.Never : BarItemVisibility.Always;
                    btnPrinterBar.Visibility = ObjParameterInvoiceBillingShowCommandBar!.ToUpper() == "false".ToUpper() ? BarItemVisibility.Never : BarItemVisibility.Always;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(typeRender), typeRender, null);
            }


            //Incializar Focos
            if (ObjParameterScanerProducto == "true")
            {
                txtScanerCodigo.Focus();
            }
        }

        public void InitializeControl()
        {
        }

        public void FnEnviarFactura()
        {
            FnValidateFormAndSubmit();
        }

        public bool FnValidateFormAndSubmit()
        {
            //Validar desembolso
            var switchDesembolso = txtDesembolsoEfectivo.IsOn;


            //Validar Bodega
            if (txtWarehouseID.SelectedIndex == -1)
            {
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Datos incorrectos",
                    "Debe seleccionar una bodega.", this);
                return false;
            }

            if (txtDate.EditValue is null)
            {
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Datos incorrectos",
                    "Debe seleccionar una fecha.", this);
                return false;
            }

            if (TxtCustomerId <= 0)
            {
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Datos incorrectos",
                    "Debe seleccionar un cliente.", this);
                return false;
            }

            if (txtReference1.SelectedIndex == -1 && switchDesembolso)
            {
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Datos incorrectos",
                    "Debe seleccionar un proveedor de credito.", this);
                return false;
            }

            if (txtZoneID.SelectedIndex == -1 && switchDesembolso)
            {
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Datos incorrectos",
                    "Debe seleccionar una zona de factura.", this);
                return false;
            }

            if (txtEmployeeID.SelectedIndex == -1 && switchDesembolso)
            {
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Datos incorrectos",
                    "Debe seleccionar una colaborador.", this);
                return false;
            }


            if (TxtStatusOldId == (int)WorkflowStatus.FacturaStatusAplicado && TransactionMasterId > 0)
            {
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Datos incorrectos",
                    "Debe crear una factura nueva la actual ya esta aplicada.", this);
                return false;
            }

            if (TxtStatusId == (int)WorkflowStatus.FacturaStatusAnulado)
            {
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Datos incorrectos",
                    "No puede pasar a estado anulado.", this);
                return false;
            }

            //Validar cantidades en el detalle
            if (_bindingListTransactionMasterDetail.Count <= 0)
            {
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Guardar", "No hay datos a guardar en el detalle", this);
                return false;
            }

            for (var i = 0; i < gridViewValues.RowCount; i++)
            {
                var total = WebToolsHelper.ConvertToNumber<decimal>(gridViewValues.GetRowCellValue(i, colSubTotal).ToString());
                if (total == 0)
                {
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Datos incorrectos",
                        "No puede haber totales en  0.", this);
                    return false;
                }
            }


            for (var i = 0; i < gridViewValues.RowCount; i++)
            {
                var total = WebToolsHelper.ConvertToNumber<decimal>(gridViewValues.GetRowCellValue(i, colQuantity).ToString());
                if (total == 0)
                {
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Datos incorrectos",
                        "No puede haber cantidades en  0.", this);
                    return false;
                }
            }


            //Validar Linea de Credito
            var lineaCreditoId = 0;
            var causalId = Convert.ToInt32(((ComboBoxItem)txtCausalID.SelectedItem).Key);
            if (txtCustomerCreditLineID.SelectedIndex != -1 && txtCustomerCreditLineID.SelectedItem is not null)
            {
                lineaCreditoId = Convert.ToInt32(((ComboBoxItem)txtCustomerCreditLineID.SelectedItem).Key);
            }

            var causalCredit = objCompanyParameter_Key_INVOICE_BILLING_CREDIT!.Split(",");
            var objCustomerCreditLine = new TbCustomerCreditLineDto();
            var invoiceTypeCredit = false;

            if (ObjCompanyParameter_Key_INVOICE_VALIDATE_BALANCE == "true")
            {
                objCustomerCreditLine = ObjListCustomerCreditLine.Where(c => c.CustomerCreditLineId == lineaCreditoId).FirstOrDefault();
            }
            else
            {
                objCustomerCreditLine = null;
            }

            invoiceTypeCredit = causalCredit.Any(c => Convert.ToInt32(c) == causalId);

            //Validar la amortizacion durante la factura
            if (ObjParameterAmortizationDuranteFactura!.ToUpper() == "true".ToUpper() && txtReference2.EditValue.ToString() == "" && invoiceTypeCredit)
            {
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Datos incorrectos",
                    "Seleccionar el plazo.", this);
                return false;
            }

            if (WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtChangeAmount.EditValue.ToString()) > 0 && invoiceTypeCredit)
            {
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Datos incorrectos",
                    "No puede haber cambios si la factura es de credito.", this);

                return false;
            }


            if (invoiceTypeCredit)
            {
                if (txtNote.EditValue.ToString() == "" && switchDesembolso)
                {
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Datos incorrectos",
                        "Asignarle una nota al documento.", this);

                    return false;
                }

                if (txtFixedExpenses.EditValue.ToString() == "" && switchDesembolso)
                {
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Datos incorrectos",
                        "Ingresar el porcentaje de gastos fijos por desembolso.", this);

                    return false;
                }

                var montoTotalInvoice = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtTotal.EditValue.ToString());
                decimal? balanceCredit = 0.0m;

                if (ObjCompanyParameter_Key_INVOICE_VALIDATE_BALANCE!.ToUpper() == "true".ToUpper())
                {
                    if (ObjCurrencyCordoba!.CurrencyID == objCustomerCreditLine!.CurrencyId)
                    {
                        balanceCredit = ObjListCustomerCreditLine.ElementAt(0).Balance;
                    }
                    else
                    {
                        balanceCredit = ObjListCustomerCreditLine.ElementAt(0).Balance * ExchangeRate;
                    }

                    if (balanceCredit < montoTotalInvoice && balanceCredit != 0)
                    {
                        _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Datos incorrectos",
                            "Cliente sin balance suficiente.", this);

                        return false;
                    }
                }
            }
            else
            {
                if (WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtChangeAmount.EditValue.ToString()) < 0)
                {
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Datos incorrectos",
                        "El cambio de la factura no puede ser menor que 0.", this);

                    return false;
                }
            }


            if (ObjParameterInvoiceBillingQuantityZero!.ToUpper() == "true".ToUpper())
            {
                return true;
            }


            //Validar Cantidades
            var objFormInvoiceApi = new FormInvoiceApi();
            for (var i = 0; i < gridViewValues.RowCount; i++)
            {
                var itemID = WebToolsHelper.ConvertToNumber<int>(gridViewValues.GetRowCellValue(i, colItemId).ToString());
                var quantity = WebToolsHelper.ConvertToNumber<decimal>(gridViewValues.GetRowCellValue(i, colQuantity).ToString());
                var resultValid = objFormInvoiceApi.GetValidExistencia(
                    Convert.ToInt32(((ComboBoxItem)txtWarehouseID.SelectedItem).Key),
                    itemID,
                    quantity
                );

                if (resultValid.Mensaje == "Existencia agotada")
                {
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Datos incorrectos",
                        "No existe suficiente cantidad en bodega, del producto " + resultValid.Nombre, this);

                    return false;
                }
            }

            return false;
        }

        public void FnRenderLineaCreditoDiv()
        {
            var causalID = ((ComboBoxItem)txtCausalID.SelectedItem).Key;
            var causalTypeCredit = ParameterCausalTypeCredit!.Value!.Split(",");
            var isCredit = false;

            for (var i = 0; i < causalTypeCredit.Length; i++)
            {
                if (causalTypeCredit[i] == causalID)
                    isCredit = true;
            }

            if (isCredit)
            {
                txtCustomerCreditLineID.Visible = true;
                labelCustomerCreditLineID.Visible = true;
            }
            else
            {
                txtCustomerCreditLineID.Visible = false;
                labelCustomerCreditLineID.Visible = false;
            }
        }

        public void FnRenderLineaCredit(List<TbCustomerCreditLineDto> objCustomerCreditLine, TbCompanyParameter causalTypeCredit)
        {
            //Llenar el combobox de Linea de Credito
            txtCustomerCreditLineID.Properties.Items.Clear();
            if (objCustomerCreditLine.Count() > 0)
            {
                for (var i = 0; i < objCustomerCreditLine.Count; i++)
                {
                    CoreWebRenderInView.LlenarComboBoxAddItem(objCustomerCreditLine[i], txtCustomerCreditLineID, "CustomerCreditLineId", "AccountNumber");
                    if (i == 0 && ObjTransactionMaster is null)
                    {
                        //CoreWebRenderInView.LlenarComboBoxSetIndex(txtCustomerCreditLineID, 0);
                        txtCustomerCreditLineID.SelectedIndex = 0;
                    }
                    else if (ObjTransactionMaster is not null && ObjTransactionMaster.Reference4 == objCustomerCreditLine[i].CustomerCreditLineId.ToString())
                    {
                        CoreWebRenderInView.LlenarComboBoxSetItem(txtCustomerCreditLineID, objCustomerCreditLine[i].CustomerCreditLineId.ToString());
                    }
                    else
                    {
                        txtCustomerCreditLineID.SelectedIndex = 0;
                    }
                }
            }


            //Agregar los causales de la transaccione
            txtCausalID.Properties.Items.Clear();
            for (var i = 0; i < ObjCausal.Count; i++)
            {
                CoreWebRenderInView.LlenarComboBoxAddItem(ObjCausal[i], txtCausalID, "TransactionCausalID", "Name");
            }

            //Buscar los causales a eliminar
            var causalesTipoCredito = causalTypeCredit!.Value!.Split(",");
            List<string> indexEliminate = new List<string>();

            for (var i = 0; i < causalesTipoCredito.Length; i++)
            {
                for (var ii = 0; ii < txtCausalID.Properties.Items.Count; ii++)
                {
                    var causalIDCredit = causalesTipoCredito[i];
                    var comboboxIem = (ComboBoxItem)txtCausalID.Properties.Items[ii];
                    if (comboboxIem.Key == causalIDCredit && objCustomerCreditLine!.Count == 0)
                    {
                        indexEliminate.Add(comboboxIem!.Key!.ToString());
                    }
                }
            }

            //Eliminar los causales
            for (var i = 0; i < indexEliminate.Count; i++)
            {
                CoreWebRenderInView.LlenarComboBoxRemoveItem(txtCausalID, indexEliminate[i]);
            }

            //Establecer el causal
            if (ObjTransactionMaster is not null)
            {
                if (ObjTransactionMaster.TransactionCausalId is not null)
                {
                    CoreWebRenderInView.LlenarComboBoxSetItem(txtCausalID, ObjTransactionMaster!.TransactionCausalId!.Value.ToString());
                }
            }
            else
            {
                CoreWebRenderInView.LlenarComboBoxSetIndex(txtCausalID, 0);
            }


            FnRenderLineaCreditoDiv();
        }

        public string FnDeleteCerosIzquierdos(string input)
        {
            int indice = 0;
            // Buscar el índice del primer carácter que no sea '0'
            while (indice < input.Length && input[indice] == '0')
            {
                indice++;
            }

            // Retornar la cadena a partir de ese índice
            return input.Substring(indice);
        }

        private void FnActualizarPrecio()
        {
            if (txtTypePriceID.SelectedItem is null)
            {
                return;
            }

            var rowCount = gridViewValues.RowCount;
            if (rowCount <= 0)
            {
                return;
            }

            var typePriceId = Convert.ToInt32(((ComboBoxItem)txtTypePriceID.SelectedItem).Key);
            //Actualizar Precio
            for (var i = 0; i < rowCount - 1; i++)
            {
                var typePriceValue = typePriceId switch
                {
                    (int)TypePrice.Publico =>
                        //precio 1 ---> 154 --> precio publico
                        (decimal)gridViewValues.GetRowCellValue(i, colPrice),
                    (int)TypePrice.PorMayor =>
                        //precio 2 ---> 155 --> precio mayorista
                        (decimal)gridViewValues.GetRowCellValue(i, colItemPrecio2),
                    (int)TypePrice.Credito =>
                        //precio 3 ---> 156 --> precio credito
                        (decimal)gridViewValues.GetRowCellValue(i, colItemPrecio3),
                    _ => decimal.Zero
                };
                gridViewValues.SetRowCellValue(i, colPrice, typePriceValue);
            }

            FnRecalculateDetail(true, "");
        }

        public void FnCalculateAmountPay()
        {
            var currencyId = ((ComboBoxItem)txtCurrencyID.SelectedItem).Key;
            if (Convert.ToInt32(currencyId) == 1 /*cordoba*/)
            {
                var ingresoCordoba = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmount.Text);
                var bancoCordoba = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmountBank.Text);
                var puntoCordoba = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmountPoint.Text);

                var tarjetaCordoba = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmountTarjeta.Text);
                var tarejtaDolares = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmountTarjetaDol.Text);
                var bancoDolares = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmountBankDol.Text);

                var ingresoDol = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmountDol.Text);
                var tipoCambio = WebToolsHelper.ConvertToNumber<decimal>(txtExchangeRate.Text);
                var total = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtTotal.Text);

                if (tipoCambio == 0)
                {
                    var resultTotal = 0;
                    FormInvoiceBillingEditPayment.txtChangeAmount.Text = resultTotal.ToString(FormatDecimal);
                }
                else
                {
                    var resultTotal = (ingresoCordoba + bancoCordoba + puntoCordoba + tarjetaCordoba + (bancoDolares / tipoCambio) + (tarejtaDolares / tipoCambio) + (ingresoDol / tipoCambio)) - total;
                    FormInvoiceBillingEditPayment.txtChangeAmount.Text = resultTotal.ToString(FormatDecimal);
                }
            }

            if (Convert.ToInt32(currencyId) == 2 /*dolar*/)
            {
                var ingresoCordoba = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmount.Text);
                var bancoCordoba = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmountBank.Text);
                var puntoCordoba = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmountPoint.Text);

                var tarjetaCordoba = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmountTarjeta.Text);
                var tarejtaDolares = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmountTarjetaDol.Text);
                var bancoDolares = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmountBankDol.Text);

                var ingresoDol = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtReceiptAmountDol.Text);
                var tipoCambio = WebToolsHelper.ConvertToNumber<decimal>(txtExchangeRate.Text);
                var total = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtTotal.Text);

                var resultTotal = (ingresoCordoba + bancoCordoba + puntoCordoba + tarjetaCordoba + (bancoDolares * tipoCambio) + (tarejtaDolares * tipoCambio) + (ingresoDol * tipoCambio)) - total;
                FormInvoiceBillingEditPayment.txtChangeAmount.Text = resultTotal.ToString(FormatDecimal);
            }
        }

        public void FnCreateTableSearchProductos()
        {
            var warehouse = (ComboBoxItem)txtWarehouseID.SelectedItem;
            var typePrice = (ComboBoxItem)txtTypePriceID.SelectedItem;
            var currency = (ComboBoxItem)txtCurrencyID.SelectedItem;
            var listPrice_ = ObjListPrice!.ListPriceID;
            var warehouseID_ = Convert.ToInt32(warehouse.Key);
            var typePrice_ = Convert.ToInt32(typePrice.Key);
            var currencyID_ = Convert.ToInt32(currency.Key);


            var formTypeListSearch = new FormTypeListSearch("Lista de Productos", ObjComponentItem!.ComponentID,
                "SELECCIONAR_ITEM_BILLING_POPUP_INVOICE", true,
                @"{warehouseID:" + warehouseID_ + ", listPriceID:" + listPrice_ + ",typePriceID:" + typePrice_ + ",currencyID:" + currencyID_ + "}",
                false, "", 0, Convert.ToInt32(ObjParameterCantidadItemPoup!), "", false);
            formTypeListSearch.EventoCallBackAceptarEvent += EventoCallBackAceptarItem;
            formTypeListSearch.ShowDialog(this);
        }

        public void FnRefrechDetail()
        {
            gridViewTbTransactionMasterDetail.DataSource = _bindingListTransactionMasterDetail;
            gridViewValues.RefreshData();
            gridViewTbTransactionMasterDetail.RefreshDataSource();
        }

        public void FnClearData()
        {
            //gridViewTbTransactionMasterDetail.DataSource = null;
            _bindingListTransactionMasterDetail.Clear();
            gridViewValues.RefreshData();
            gridViewTbTransactionMasterDetail.RefreshDataSource();

            FormInvoiceBillingEditPayment.txtReceiptAmount.Text = "0";
            FormInvoiceBillingEditPayment.txtReceiptAmountDol.Text = "0";
            FormInvoiceBillingEditPayment.txtReceiptAmountBank.Text = "0";
            FormInvoiceBillingEditPayment.txtReceiptAmountPoint.Text = "0";
            FormInvoiceBillingEditPayment.txtChangeAmount.Text = "0";
            FormInvoiceBillingEditPayment.txtSubTotal.Text = "0";
            FormInvoiceBillingEditPayment.txtIva.Text = "0";
            FormInvoiceBillingEditPayment.txtTotal.Text = "0";
            FormInvoiceBillingEditPayment.txtReceiptAmountTarjeta.Text = "0";
            FormInvoiceBillingEditPayment.txtReceiptAmountTarjetaDol.Text = "0";
            FormInvoiceBillingEditPayment.txtReceiptAmountBankDol.Text = "0";
        }


        public void FnOnCompleteNewItemPopPub(dynamic mensaje)
        {
            Dictionary<string, string> diccionario = new Dictionary<string, string>();
            WebToolsHelper objWebToolsHelper = new WebToolsHelper();
            string itemIDTemporal = objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "itemID", "0");

            if (itemIDTemporal == "0")
                return;


            diccionario.Add("itemID", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "itemID", "0"));
            diccionario.Add("Codigo", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Codigo", "0"));
            diccionario.Add("Nombre", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Nombre", "0"));
            diccionario.Add("MedidaID", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "MedidaID", "0"));
            diccionario.Add("Medida", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Medida", "0"));

            diccionario.Add("Cantidad", "1");
            diccionario.Add("BCantiad", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Cantidad", "0")); //Cantidad en bodega
            diccionario.Add("Precio", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Precio", "0"));
            diccionario.Add("Total", Convert.ToString(1 * Convert.ToDecimal(objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Precio", "0"))));

            diccionario.Add("Iva", "0");
            diccionario.Add("Lote", "");
            diccionario.Add("Vencimiento", "");
            diccionario.Add("Precio2", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Precio2", "0"));
            diccionario.Add("Precio3", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Precio3", "0"));
            diccionario.Add("Descripcion", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Descripcion", ""));
            FnOnCompleteNewItem(diccionario, true);
        }

        public void FnOnCompleteNewItem(Dictionary<string, string> diccionario, bool sumar)
        {
            Invoke(() =>
            {
                txtScanerCodigo.Focus();
                var index = 0;
                var indexEncontrado = 0;
                var encontrado = false;
                var itemID = Convert.ToInt32(diccionario["itemID"]);

                //Buscar Item
                if (_bindingListTransactionMasterDetail.Count > 0)
                {
                    foreach (FormInvoiceBillingEditDetailDTO detailDto in _bindingListTransactionMasterDetail)
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
                    FnRefrechDetail();
                    FnGetConcept(itemID, "IVA");
                    return;
                }

                var billingEdit = new FormInvoiceBillingEditDetailDTO
                {
                    ItemId = Convert.ToInt32(diccionario["itemID"]),
                    ItemNumber = diccionario["Codigo"],
                    TransactionDetailName = diccionario["Nombre"],
                    TransactionDetailNameDescription = diccionario["Descripcion"],
                    Sku = Convert.ToInt32(diccionario["MedidaID"]),
                    Quantity = 1,
                    Price = WebToolsHelper.ConvertToNumber<decimal>(diccionario["Precio"]),
                    SubTotal = 1 * WebToolsHelper.ConvertToNumber<decimal>(diccionario["Precio"]),
                    TaxServices = decimal.Zero,
                    Iva = WebToolsHelper.ConvertToNumber<decimal>(diccionario["Iva"]),
                    SkuQuantityBySku = 0,
                    UnitaryPriceIndividual = 0,
                    SkuFormatoDescription = diccionario["Medida"],
                    ItemPrecio2 = WebToolsHelper.ConvertToNumber<decimal>(diccionario["Precio2"]),
                    ItemPrecio3 = WebToolsHelper.ConvertToNumber<decimal>(diccionario["Precio3"]),
                    AccionMas = "",
                    AccionMenos = "",
                    AccionPrecios = decimal.Zero
                };
                _bindingListTransactionMasterDetail.Add(billingEdit);
                FnRefrechDetail();
                FnGetConcept(itemID, "IVA");
            });
        }

        public void FnGetConcept(int itemID, string concepName)
        {
            //Recalculoa el concepto via AJAX 2023-12-05 Inicio		
            if (_bindingListTransactionMasterDetail.Count <= 0) return;
            var user = VariablesGlobales.Instance.User;
            var findValue = _bindingListTransactionMasterDetail.SingleOrDefault(dto => dto.ItemId==itemID);
            if (!txtCheckApplyExoneracion.IsOn)
            {
                var objConceptIva = _objInterfazCompanyComponentConceptModel.GetRowByPk(user!.CompanyID, ObjComponentItem!.ComponentID, itemID, "IVA");
                var objConceptTaxServices = _objInterfazCompanyComponentConceptModel.GetRowByPk(user!.CompanyID, ObjComponentItem!.ComponentID, itemID, "TAX_SERVICES");
                if (findValue != null)
                {
                    if (objConceptTaxServices is not null)
                    {
                        findValue.TaxServices = objConceptTaxServices.ValueOut!.Value;
                    }
                    if (objConceptIva is not null)
                    {
                        findValue.Iva = objConceptIva.ValueOut!.Value;
                    }
                }
            }
            else
            {
                if (findValue != null)
                {
                    findValue.TaxServices = decimal.Zero;
                    findValue.Iva = decimal.Zero;
                }
            }

            FnRecalculateDetail(true, "");
        }


        public void FnRecalculateDetail(bool clearRecibo, string sourceEvent)
        {
            var typePriceID = Convert.ToInt32(((ComboBoxItem)txtTypePriceID.SelectedItem).Key);
            var cantidad = 0.0m;
            var iva = 0.0m;
            var precio = 0.0m;
            var taxServices = 0.0m;
            var subtotal = 0.0m;
            var total = 0.0m;
            var porcentajeDescuento = WebToolsHelper.ConvertToNumber<decimal>(FormInvoiceBillingEditPayment.txtPorcentajeDescuento.Text);
            var cantidadGeneral = 0.0m;
            var ivaGeneral = 0.0m;
            var precioGeneral = 0.0m;
            var subtotalGeneral = 0.0m;
            var totalGeneral = 0.0m;
            var serviceGeneral = 0m;
            var priceTemporal = 0.0m;
            var cantidadTemporal = 0.0m;


            var NSSystemDetailInvoice = gridViewValues;
            for (var i = 0; i < (NSSystemDetailInvoice.RowCount); i++)
            {
                var skuSelecte = Convert.ToInt32(NSSystemDetailInvoice.GetRowCellValue(i, colSku));
                var skuCatalogItemID = skuSelecte;
                var skuValue = 1; /*configuracion del sku a cuantas unidades contiene por ejemplo sku 1 quintal = 100 unidades, pero en nuestro caso siempre va ha ser 1*/
                var skuValuePrimceUnit = WebToolsHelper.ConvertToNumber<decimal>(NSSystemDetailInvoice.GetRowCellValue(i, colPrice).ToString()); //precio por unidad de sku
                var skuValueDescription = NSSystemDetailInvoice.GetRowCellValue(i, colSkuFormatoDescripton).ToString();


                cantidadTemporal = Convert.ToInt32(NSSystemDetailInvoice.GetRowCellValue(i, colQuantity));
                priceTemporal = Convert.ToDecimal(NSSystemDetailInvoice.GetRowCellValue(i, colPrice));


                NSSystemDetailInvoice.SetRowCellValue(i, colQuantity, cantidadTemporal);
                NSSystemDetailInvoice.SetRowCellValue(i, colPrice, priceTemporal);
                NSSystemDetailInvoice.SetRowCellValue(i, colSkuFormatoDescripton, skuValueDescription);

                cantidad = Convert.ToInt32(NSSystemDetailInvoice.GetRowCellValue(i, colQuantity));
                precio = Convert.ToDecimal(NSSystemDetailInvoice.GetRowCellValue(i, colPrice));
                taxServices = Convert.ToDecimal(NSSystemDetailInvoice.GetRowCellValue(i, colTaxServices));
                iva = Convert.ToDecimal(NSSystemDetailInvoice.GetRowCellValue(i, colIva));


                subtotal = precio * cantidad;
                iva = (precio * cantidad) * iva;
                taxServices = (precio * cantidad) * taxServices;
                total = iva + taxServices + subtotal;


                cantidadGeneral = cantidadGeneral + cantidad;
                precioGeneral = precioGeneral + precio;
                ivaGeneral = ivaGeneral + iva;
                serviceGeneral = serviceGeneral + taxServices;
                subtotalGeneral = subtotalGeneral + subtotal;
                totalGeneral = totalGeneral + total;
                NSSystemDetailInvoice.SetRowCellValue(i, colSubTotal, subtotal);
            }
            gridViewTbTransactionMasterDetail.RefreshDataSource();
            var descuento = subtotalGeneral * (porcentajeDescuento / 100);
            totalGeneral = subtotalGeneral + serviceGeneral + ivaGeneral - descuento;
            FormInvoiceBillingEditPayment.txtSubTotal.Text = subtotalGeneral.ToString(FormatDecimal);
            FormInvoiceBillingEditPayment.txtDescuento.Text = descuento.ToString(FormatDecimal);
            FormInvoiceBillingEditPayment.txtIva.Text = ivaGeneral.ToString(FormatDecimal);
            FormInvoiceBillingEditPayment.txtServices.Text = serviceGeneral.ToString(FormatDecimal);
            FormInvoiceBillingEditPayment.txtTotal.Text = totalGeneral.ToString(FormatDecimal);
            FormInvoiceBillingEditPayment.lblTotal.Text = totalGeneral.ToString(FormatDecimal);
            lblTotal.Text = totalGeneral.ToString(FormatDecimal);
            FormInvoiceBillingEditPayment.txtChangeAmount.Text = @"0";
            //Si es de credito que la factura no supere la linea de credito
            if (TransactionMasterId > 0)
            {
                FormInvoiceBillingEditPayment.txtReceiptAmountDol.Text = @"0";
                FormInvoiceBillingEditPayment.txtChangeAmount.Text = @"0";
                FormInvoiceBillingEditPayment.txtReceiptAmountBank.Text = @"0";
                FormInvoiceBillingEditPayment.txtReceiptAmountPoint.Text = @"0";
                FormInvoiceBillingEditPayment.txtReceiptAmountTarjeta.Text = @"0";
                FormInvoiceBillingEditPayment.txtReceiptAmountTarjetaDol.Text = @"0";
                FormInvoiceBillingEditPayment.txtReceiptAmountBankDol.Text = @"0";
                FormInvoiceBillingEditPayment.txtReceiptAmount.Text = @"0";
            }
            else
            {
                var causalSelect = ((ComboBoxItem)txtCausalID.SelectedItem).Key;
                var customerCreditLineSelectedValue = txtCustomerCreditLineID.SelectedItem;
                if (customerCreditLineSelectedValue is not null)
                {
                    var customerCreditLineID = ((ComboBoxItem)customerCreditLineSelectedValue).Key;
                }

                var causalCredit = ParameterCausalTypeCredit.Value.Split(",");
                var invoiceTypeCredit = false;

                //Obtener si la factura es al credito						
                foreach (var t in causalCredit)
                {
                    if (t == causalSelect)
                    {
                        invoiceTypeCredit = true;
                    }
                }


                if (invoiceTypeCredit)
                {
                    FormInvoiceBillingEditPayment.txtReceiptAmountDol.Text = @"0";
                    FormInvoiceBillingEditPayment.txtChangeAmount.Text = @"0";
                    FormInvoiceBillingEditPayment.txtReceiptAmountBank.Text = @"0";
                    FormInvoiceBillingEditPayment.txtReceiptAmountPoint.Text = @"0";
                    FormInvoiceBillingEditPayment.txtReceiptAmountTarjeta.Text = @"0";
                    FormInvoiceBillingEditPayment.txtReceiptAmountTarjetaDol.Text = @"0";
                    FormInvoiceBillingEditPayment.txtReceiptAmountBankDol.Text = @"0";
                    FormInvoiceBillingEditPayment.txtReceiptAmount.Text = @"0";
                }
                else
                {
                    FormInvoiceBillingEditPayment.txtReceiptAmount.Text = decimal.Round(totalGeneral, 2).ToString("N2");
                    FormInvoiceBillingEditPayment.txtReceiptAmountDol.Text = @"0";
                    FormInvoiceBillingEditPayment.txtReceiptAmountBank.Text = @"0";
                    FormInvoiceBillingEditPayment.txtReceiptAmountPoint.Text = @"0";
                    FormInvoiceBillingEditPayment.txtReceiptAmountTarjeta.Text = @"0";
                    FormInvoiceBillingEditPayment.txtReceiptAmountTarjetaDol.Text = @"0";
                    FormInvoiceBillingEditPayment.txtReceiptAmountBankDol.Text = @"0";
                    FormInvoiceBillingEditPayment.txtChangeAmount.Text = @"0";
                }
            }
        }

        public void FnGetCustomerClient(int entityID)
        {
            var objForm = new FormInvoiceApi();
            var dataForm = objForm.GetLineByCustomer(entityID);
            FnRenderLineaCredit(dataForm!.ObjListCustomerCreditLine!, dataForm.ParameterCausalTypeCredit!);
        }

        private bool IsDialogPaymentOpen()
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form is FormInvoiceBillingEditPaymentDialog)
                {
                    return true;
                }
            }

            return false;
        }

        private void UpdateConcept()
        {
            var length = _bindingListTransactionMasterDetail.Count;
            var i = 0;
            while (i < length)
            {
                FnGetConcept(_bindingListTransactionMasterDetail[i].ItemId ?? 0, "IVA");
                i++;
            }
        }

        #endregion

        #region Eventos Formulario

        private void txtCheckApplyExoneracion_Toggled(object sender, EventArgs e)
        {
            UpdateConcept();
        }

        private void txtLayFirstLineProtocolo_EditValueChanged(object sender, EventArgs e)
        {
            if (ObjParameterINVOICE_BILLING_VALIDATE_EXONERATION == "true")
            {
                var api = new FormInvoiceApi();
                var data = api.GetNumberExoneration(txtLayFirstLineProtocolo.Text);
                if (data.Count > 0)
                {
                    txtCheckApplyExoneracion.IsOn = false;
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Exoneracion", "El numero de exoneracion ya existe!!", this);
                }
                else
                {
                    txtCheckApplyExoneracion.IsOn = true;
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Informacion, "Exoneracion", "Exoneracion aplicada!!", this);
                }

                UpdateConcept();
            }
        }

        private void txtScanerCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.M:
                    // La tecla presionada es la letra "M"
                    // Aquí puedes agregar la lógica que deseas ejecutar
                    ComandPrinter();
                    break;
                case Keys.K:
                    // La tecla presionada es la letra "M"
                    // Aquí puedes agregar la lógica que deseas ejecutar
                    LoadNew();
                    LoadRender(TypeRender.New);
                    break;
                case Keys.I:
                    OpenCashbox();
                    break;
                case Keys.Down:
                {
                    if (gridViewValues.RowCount > 0)
                    {
                        gridViewValues.Focus();
                        gridViewValues.FocusedRowHandle = 0;
                    }

                    break;
                }
            }
        }

        public void txtReceiptAmountDol_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Tab:
                case (char)Keys.Enter:
                    FormInvoiceBillingEditPayment.btnRegistrar.Focus();
                    break;
            }
        }

        private void txtScanerCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter)
            {
                return;
            }

            var currencyID_ = Convert.ToInt32(((ComboBoxItem)txtCurrencyID.SelectedItem).Key);
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
                FormInvoiceBillingEditPayment.txtReceiptAmount.Focus();
            }


            if (ObjSELECCIONAR_ITEM_BILLING_BACKGROUND is null)
                return;


            bool encontrado = false;
            int i = 0;
            for (i = 0; i < ObjSELECCIONAR_ITEM_BILLING_BACKGROUND.Rows.Count; i++)
            {
                if (encontrado == true)
                {
                    i--;
                    break;
                }

                //buscar por codigo de sistema					
                var currencyTemp = Convert.ToInt32(ObjSELECCIONAR_ITEM_BILLING_BACKGROUND.Rows[i]["currencyID"]);
                var currencyID = Convert.ToInt32(((ComboBoxItem)txtCurrencyID.SelectedItem).Key);

                var warehouseIDTemp = Convert.ToInt32(ObjSELECCIONAR_ITEM_BILLING_BACKGROUND.Rows[i]["warehouseID"]);
                var warehouseID = Convert.ToInt32(((ComboBoxItem)txtWarehouseID.SelectedItem).Key);
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
                var listCodigoTmpArray = listCodigTmp!.Split(",");
                encontrado = false;

                if (!encontrado)
                {
                    for (var ii = 0; ii < listCodigoTmpArray.Length; ii++)
                    {
                        if (
                            FnDeleteCerosIzquierdos(listCodigoTmpArray[ii].ToString().ToUpper()) == FnDeleteCerosIzquierdos(codigoABuscar) &&
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

            if (encontrado)
            {
                var sumar = true;
                var filterResult = ObjSELECCIONAR_ITEM_BILLING_BACKGROUND.Rows[i];
                Dictionary<string, string> diccionario = new Dictionary<string, string>();

                diccionario.Add("itemID", filterResult["itemID"].ToString()!);
                diccionario.Add("Codigo", filterResult["Codigo"].ToString()!);
                diccionario.Add("Nombre", filterResult["Nombre"].ToString()!);
                diccionario.Add("MedidaID", filterResult["unitMeasureID"].ToString()!);
                diccionario.Add("Medida", filterResult["Medida"].ToString()!);
                diccionario.Add("Cantidad", "1");
                diccionario.Add("BMedida", filterResult["Cantidad"].ToString()!);
                diccionario.Add("Precio", filterResult["Precio"].ToString()!);
                diccionario.Add("Total", filterResult["Precio"].ToString()!);
                diccionario.Add("Iva", "0");
                diccionario.Add("Lote", "");
                diccionario.Add("Vencimiento", "");
                diccionario.Add("Precio2", filterResult["Precio2"].ToString()!);
                diccionario.Add("Precio3", filterResult["Precio3"].ToString()!);

                //Agregar el Item a la Fila
                FnOnCompleteNewItem(diccionario, sumar);
            }
        }

        private void downButtonProducto_Click(object sender, EventArgs e)
        {
        }


        private void btnClearCustomer_Click(object sender, EventArgs e)
        {
            TxtCustomerId = 0;
            txtCustomerDescription.Text = "";
        }


        private void txtTypePriceID_SelectedIndexChanged(object sender, EventArgs e)
        {
            FnActualizarPrecio();
        }

        private void txtCausalID_SelectedValueChanged(object sender, EventArgs e)
        {
            FnClearData();
            FnRenderLineaCreditoDiv();
        }

        private void txtCustomerCreditLineID_SelectedIndexChanged(object sender, EventArgs e)
        {
            FnClearData();
        }

        private void txtCurrencyID_SelectedIndexChanged(object sender, EventArgs e)
        {
            FnClearData();
        }

        private void txtWarehouseID_SelectedIndexChanged(object sender, EventArgs e)
        {
            FnClearData();
        }

        public void txtPorcentajeDescuento_EditValueChanged(object sender, EventArgs e)
        {
            FnRecalculateDetail(true, "");
        }

        public void txtReceiptAmount_EditValueChanged(object sender, EventArgs e)
        {
            FnCalculateAmountPay();
        }

        private void txtReceiptAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        public void txtReceiptAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            FormInvoiceBillingEditPayment.txtReceiptAmountDol.Focus();
        }

        public void txtReceiptAmountDol_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                case Keys.Tab:
                    FormInvoiceBillingEditPayment.btnRegistrar.Focus();
                    break;
            }
        }

        public void txtReceiptAmountDol_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                var workflowStage = ObjListWorkflowStage!.Where(c => c.Aplicable!.Value).FirstOrDefault();
                TxtStatusId = workflowStage!.WorkflowStageID;
            }

            if (e.Control && e.KeyCode == Keys.B)
            {
                // Realizar la acción deseada al presionar Control + A
                txtScanerCodigo.Focus();
            }
        }

        public void txtReceiptAmountDol_EditValueChanged(object sender, EventArgs e)
        {
            FnCalculateAmountPay();
        }

        public void txtReceiptAmountTarjeta_EditValueChanged(object sender, EventArgs e)
        {
            FnCalculateAmountPay();
        }

        public void txtReceiptAmountTarjetaDol_EditValueChanged(object sender, EventArgs e)
        {
            FnCalculateAmountPay();
        }

        public void txtReceiptAmountPoint_EditValueChanged(object sender, EventArgs e)
        {
            FnCalculateAmountPay();
        }

        public void txtReceiptAmountBank_EditValueChanged(object sender, EventArgs e)
        {
            FnCalculateAmountPay();
        }

        public void txtReceiptAmountBankDol_EditValueChanged(object sender, EventArgs e)
        {
            FnCalculateAmountPay();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (gridViewValues.SelectedRowsCount > 0)
            {
                var rowIndex = gridViewValues.GetSelectedRows().ToList();
                foreach (var indexRow in rowIndex)
                {
                    gridViewValues.DeleteRow(indexRow);
                }

                //FnRefrechDetail();
                FnRecalculateDetail(true, "");
            }
        }

        private void txtCurrencyID_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Enter:
                case (char)Keys.Tab:
                    txtScanerCodigo.Focus();
                    break;
            }
        }

        private void txtCurrencyID_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                case Keys.Tab:
                    txtScanerCodigo.Focus();
                    break;
            }
        }

        private void gridViewValues_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            try
            {
                if (e.Value == null) return;

                if (e.Column.Name == colQuantity.Name)
                {
                    if (e.Value.ToString() == e.OldValue.ToString()) return;
                    FnRecalculateDetail(true, "");
                }

                if (e.Column.Name == colPrice.Name)
                {
                    var selectedValue = e.Value.ToString();
                    if (selectedValue == e.OldValue.ToString()) return;
                    FnRecalculateDetail(true, "txtPrice");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                XtraMessageBox.Show(ex.Message);
            }
        }


        private void ButtonPlusQuantity_Click(object sender, EventArgs e)
        {
            var quantity = gridViewValues.GetRowCellValue(gridViewValues.FocusedRowHandle, colQuantity).ToString();
            var quantityDecimal = WebToolsHelper.ConvertToNumber<decimal>(quantity);
            quantityDecimal += 1;
            gridViewValues.SetRowCellValue(gridViewValues.FocusedRowHandle, colQuantity, quantityDecimal);
            FnRecalculateDetail(true, "");
        }

        private void ButtonMinusQuantity_Click(object sender, EventArgs e)
        {
            var quantity = gridViewValues.GetRowCellValue(gridViewValues.FocusedRowHandle, colQuantity).ToString();
            var quantityDecimal = WebToolsHelper.ConvertToNumber<decimal>(quantity);
            if (decimal.Compare(quantityDecimal, decimal.Zero) == 0)
            {
                gridViewValues.DeleteRow(gridViewValues.FocusedRowHandle);
                return;
            }

            quantityDecimal -= 1;
            gridViewValues.SetRowCellValue(gridViewValues.FocusedRowHandle, colQuantity, quantityDecimal);
            FnRecalculateDetail(true, "");
        }

        private void gridViewValues_CustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.Name == colAccionPrecios.Name)
            {
                if (e.RepositoryItem is not RepositoryItemComboBox buttonEdit) return;
                if (gridViewValues.RowCount <= 0) return;
                buttonEdit.Items.Clear();
                var precio1 = Convert.ToDecimal(gridViewValues.GetRowCellValue(e.RowHandle, colPrice).ToString());
                var precio2 = Convert.ToDecimal(gridViewValues.GetRowCellValue(e.RowHandle, colItemPrecio2).ToString());
                var precio3 = Convert.ToDecimal(gridViewValues.GetRowCellValue(e.RowHandle, colItemPrecio3).ToString());
                var comboBoxItem1 = new ComboBoxItem("1", $"{precio1:N2}");
                var comboBoxItem2 = new ComboBoxItem("2", $"{precio2:N2}");
                var comboBoxItem3 = new ComboBoxItem("3", $"{precio3:N2}");
                buttonEdit.Items.AddRange([comboBoxItem1, comboBoxItem2, comboBoxItem3]);
                repositoryItemComboBox2 = buttonEdit;
            }
        }

        private void repositoryItemComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtiene el índice de la fila actualmente enfocada
            int fila = gridViewValues.FocusedRowHandle;
            var comboBoxEdit = sender as ComboBoxEdit;
            // Obtiene el valor seleccionado del RepositoryItemComboBox
            var itemSeleccionado = ((ComboBoxItem)comboBoxEdit.SelectedItem).Value.ToString();
            var priceSeleccionado = WebToolsHelper.ConvertToNumber<decimal>(itemSeleccionado);
            gridViewValues.SetRowCellValue(fila, colPrice, itemSeleccionado);
            gridViewValues.SetRowCellValue(fila, colAccionPrecios, itemSeleccionado);
            FnRecalculateDetail(true, "txtPrice");
        }

        public void btnNew_Click(object sender, EventArgs e)
        {
            if (IsDialogPaymentOpen())
            {
                FormInvoiceBillingEditPayment.Close();
            }

            if (!progressPanel.Visible)
            {
                progressPanel.Visible = true;
            }

            TypeOpen = TypeOpenForm.Init;
            TransactionMasterId = 0;
            FormInvoiceBillingEdit_Load(sender, e);
        }

        private void barManager1_ItemClick(object sender, ItemClickEventArgs e)
        {
            _backgroundWorker = new BackgroundWorker();
            if (e.Item.Name == btnActualizarCatalogo.Name)
            {
                var formInvoiceApi = new FormInvoiceApi();
                var warehouseComboBoxItem = (ComboBoxItem)txtWarehouseID.SelectedItem;
                var typePriceComboBoxItem = (ComboBoxItem)txtWarehouseID.SelectedItem;
                var currencyIdComboBoxItem = (ComboBoxItem)txtWarehouseID.SelectedItem;
                var listPrice_ = ObjListPrice!.ListPriceID;
                var warehouseID_ = Convert.ToInt32(warehouseComboBoxItem.Key);
                var typePrice_ = Convert.ToInt32(typePriceComboBoxItem.Key);
                var currencyID_ = Convert.ToInt32(currencyIdComboBoxItem.Key);
                if (!progressPanel.Visible)
                {
                    progressPanel.Visible = true;
                }

                _backgroundWorker.DoWork += (ob, ev) =>
                {
                    VariablesGlobales.Instance.ObjSELECCIONAR_ITEM_BILLING_BACKGROUND =
                        formInvoiceApi.GetViewApi(
                            ObjComponentItem!.ComponentID,
                            @"SELECCIONAR_ITEM_BILLING_BACKGROUND",
                            @"{warehouseID:" + warehouseID_ + ", listPriceID:" + listPrice_ + ",typePriceID:" + typePrice_ + ",currencyID:" + currencyID_ + "}"
                        );

                    ObjSELECCIONAR_ITEM_BILLING_BACKGROUND = VariablesGlobales.Instance.ObjSELECCIONAR_ITEM_BILLING_BACKGROUND;
                };
                _backgroundWorker.RunWorkerCompleted += (ob, ev) =>
                {
                    if (ev.Error is not null)
                    {
                        CustomException.LogException(ev.Error);
                        _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", $"Se ha cancelado la operación actual. Error: {ev.Error.Message}", this);
                    }
                    else if (ev.Cancelled)
                    {
                        _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Warning, "Cancelado", "Se ha cancelado la operación actual", this);
                    }
                    else
                    {
                        _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Informacion, "Actualizar Catálogo", "Se ha actualizado el catálogo de forma correcta", this);
                        txtScanerCodigo.Focus();
                        progressPanel.Visible = false;
                    }
                };
                if (!_backgroundWorker.IsBusy)
                {
                    _backgroundWorker.RunWorkerAsync();
                }
            }

            if (e.Item.Name == btnPrinterInvoice.Name)
            {
                if (!progressPanel.Visible)
                {
                    progressPanel.Visible = true;
                }

                _backgroundWorker.DoWork += (ob, ev) => { ComandPrinter(); };
                _backgroundWorker.RunWorkerCompleted += (ob, ev) =>
                {
                    if (ev.Error is not null)
                    {
                        CustomException.LogException(ev.Error);
                        _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", $"Se ha cancelado la operación actual. Error: {ev.Error.Message}", this);
                    }
                    else if (ev.Cancelled)
                    {
                        _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Warning, "Cancelado", "Se ha cancelado la operación actual", this);
                    }
                    else
                    {
                        _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Informacion, "Imprimir", "Se ha enviado a la impresora de forma correcta", this);
                        progressPanel.Visible = false;
                    }
                };
                if (!_backgroundWorker.IsBusy)
                {
                    _backgroundWorker.RunWorkerAsync();
                }
            }

            if (e.Item.Name == btnInvoiceDelete.Name)
            {
                var message = XtraMessageBox.Show("¿Seguro desea eliminar la factua actual? Esta acción no se puede revertir", "Eliminar", MessageBoxButtons.YesNo);
                if (message == DialogResult.No)
                {
                    return;
                }

                if (TransactionMasterId > 0)
                {
                    _backgroundWorker = new BackgroundWorker();
                    if (!progressPanel.Visible)
                    {
                        progressPanel.Visible = true;
                    }

                    _backgroundWorker.DoWork += (o, args) => { ComandDelete(); };
                    _backgroundWorker.RunWorkerCompleted += (o, args) =>
                    {
                        if (args.Error is not null)
                        {
                            CustomException.LogException(args.Error);
                            _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", args.Error.Message, this);
                        }
                        else if (args.Cancelled)
                        {
                            _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", "Se ha cancelado la operación actual", this);
                        }
                        else
                        {
                            _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Informacion, "Eliminar", "Se ha eliminado la factura de forma correcta", this);
                            LoadNew();
                            LoadRender(TypeRender.New);
                            if (progressPanel.Visible)
                            {
                                progressPanel.Visible = false;
                            }
                        }
                    };
                    if (!_backgroundWorker.IsBusy)
                    {
                        _backgroundWorker.RunWorkerAsync();
                    }
                }
                else
                {
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Informacion, "Eliminar", "No hay factura a eliminar", this);
                }
            }

            if (e.Item.Name == btnSeleccionarFactura.Name)
            {
                var formTypeListSearch = new FormTypeListSearch("Lista de Facturas", ObjComponentTransactionBilling!.ComponentID,
                    "SELECCIONAR_BILLING_REGISTER", true,
                    "{warehouseID:4,listPriceID:12,typePriceID:154,currencyID:1}", false, "", 0, 5, "", true);
                formTypeListSearch.EventoCallBackAceptarEvent += EventoCallBackAceptarSeleccoinDeFactura;
                formTypeListSearch.ShowDialog(this);
            }

            if (e.Item.Name == btnRegresar.Name)
            {
                Close();
                var formulario = CoreFormList.GetFormulario("core_dashboards");
                if (!CoreFormList.Principal().IsFormOpen(formulario.Name))
                {
                    formulario.MdiParent = CoreFormList.Principal();
                    formulario.Show();
                }
                else
                {
                    formulario.BringToFront();
                    formulario.Focus();
                }
            }

            if (e.Item.Name == btnOpenCashdrawer.Name)
            {
                OpenCashbox();
            }
        }

        private void downButtonSeleccion_Click(object sender, EventArgs e)
        {
        }

        public void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                _backgroundWorker = new BackgroundWorker();
                if (FnValidateFormAndSubmit())
                {
                    if (!progressPanel.Visible)
                    {
                        progressPanel.Visible = true;
                    }

                    _backgroundWorker.DoWork += (ob, ev) =>
                    {
                        if (TransactionMasterId == 0)
                            SaveInsert();
                        else
                            SaveUpdate();
                    };
                    _backgroundWorker.RunWorkerCompleted += (ob, ev) =>
                    {
                        if (ev.Error is not null)
                        {
                            CustomException.LogException(ev.Error);
                            _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Registrar", $"No se registraron los valores. {ev.Error.Message}", this);
                        }
                        else if (ev.Cancelled)
                        {
                            //cancelado por el usuario   
                            _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Informacion, "Registrar", "Se ha cancelado la operación actual. Linea 3424", this);
                        }
                        else
                        {
                            if (IsDialogPaymentOpen())
                            {
                                FormInvoiceBillingEditPayment.Close();
                            }

                            _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Informacion, "Registrar", "Se han registrdo los datos de forma correcta", this);
                            if (TransactionMasterId > 0 && ObjParameterInvoiceAutoApply == "false")
                            {
                                LoadEdit();
                                LoadRender(TypeRender.Edit);
                            }

                            if (TransactionMasterId > 0 && ObjParameterInvoiceAutoApply!.ToUpper() == "true".ToUpper())
                            {
                                LoadNew();
                                LoadRender(TypeRender.New);
                            }

                            if (progressPanel.Visible)
                            {
                                progressPanel.Visible = false;
                            }
                        }
                    };

                    if (!_backgroundWorker.IsBusy)
                    {
                        _backgroundWorker.RunWorkerAsync();
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.StackTrace);
                _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error registrar", $"{exception.Message} {exception.Source}", this);
            }
        }

        public void btnAplicar_Click(object sender, EventArgs e)
        {
            _backgroundWorker = new BackgroundWorker();
            if (!FnValidateFormAndSubmit()) return;
            if (!progressPanel.Visible)
            {
                progressPanel.Visible = true;
            }

            _backgroundWorker.DoWork += (ob, ev) =>
            {
                if (TransactionMasterId == 0)
                    SaveInsert();
                else
                {
                    TxtStatusId = (int)WorkflowStatus.FacturaStatusAplicado;
                    SaveUpdate();
                }
            };
            _backgroundWorker.RunWorkerCompleted += (ob, ev) =>
            {
                if (ev.Error is not null)
                {
                    CustomException.LogException(ev.Error);
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Registrar", $"No se registraron los valores. {ev.Error.Message}", this);
                }
                else if (ev.Cancelled)
                {
                    //cancelado por el usuario   
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Informacion, "Registrar", "Se ha cancelado la operación actual. Linea 3424", this);
                }
                else
                {
                    _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Informacion, "Registrar", "Se han registrdo los datos de forma correcta", this);
                    if (TransactionMasterId > 0)
                    {
                        LoadEdit();
                        LoadRender(TypeRender.Edit);
                    }

                    if (progressPanel.Visible)
                    {
                        progressPanel.Visible = false;
                    }
                }
            };

            if (!_backgroundWorker.IsBusy)
            {
                _backgroundWorker.RunWorkerAsync();
            }
        }

        private void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            if (ObjComponentItem is null)
            {
                return;
            }

            var formTypeListSearch = new FormTypeListSearch("Lista de Cliente", ObjComponentCustomer!.ComponentID, "SELECCIONAR_CLIENTES_BILLING", true,
                "{warehouseID:4,listPriceID:12,typePriceID:154,currencyID:1}", false, "", 0, 15, "", true);
            formTypeListSearch.EventoCallBackAceptarEvent += EventoCallBackAceptarCusomter;
            formTypeListSearch.ShowDialog(this);
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            var warehouse = (ComboBoxItem)txtWarehouseID.SelectedItem;
            var typePrice = (ComboBoxItem)txtTypePriceID.SelectedItem;
            var currency = (ComboBoxItem)txtCurrencyID.SelectedItem;
            var listPrice = ObjListPrice!.ListPriceID;
            var warehouseId = Convert.ToInt32(warehouse.Key);
            var typePriceId = Convert.ToInt32(typePrice.Key);
            var currencyIdKey = Convert.ToInt32(currency.Key);


            var formTypeListSearch = new FormTypeListSearch("Lista de Productos", ObjComponentItem!.ComponentID,
                "SELECCIONAR_ITEM_BILLING_POPUP_INVOICE", true,
                @"{warehouseID:" + warehouseId + ", listPriceID:" + listPrice + ",typePriceID:" + typePriceId + ",currencyID:" + currencyIdKey + "}",
                false, "", 0, 5, "", false);
            formTypeListSearch.EventoCallBackAceptarEvent += EventoCallBackAceptarItem;
            formTypeListSearch.ShowDialog(this);
        }

        private void EventoCallBackAceptarCusomter(dynamic mensaje)
        {
            // Realizar la lógica que desees en el formulario padre
            Invoke(() =>
            {
                WebToolsHelper objWebToolsHelper = new WebToolsHelper();
                TxtCustomerId = Convert.ToInt32(objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "entityID", "0"));
                txtCustomerDescription.Text = objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Codigo", "0");
                txtCustomerDescription.Text = txtCustomerDescription.Text + "/" + objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Nombre", "N/D");

                //FnClearData();
                FnGetCustomerClient(TxtCustomerId);
            });
        }

        private void EventoCallBackAceptarSeleccoinDeFactura(dynamic mensaje)
        {
            WebToolsHelper objWebToolsHelper = new WebToolsHelper();
            CompanyId = Convert.ToInt32(objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "companyID", "0"));
            TransactionId = Convert.ToInt32(objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "transactionID", "0"));
            TransactionMasterId = Convert.ToInt32(objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "transactionMasterID", "0"));

            Invoke(() =>
            {
                if (!_backgroundWorker.IsBusy)
                {
                    if (!progressPanel.Visible)
                    {
                        progressPanel.Size = Size;
                        progressPanel.Visible = true;
                    }

                    _backgroundWorker.RunWorkerAsync();
                }
            });
        }

        private void gridViewValues_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Tab:
                    e.SuppressKeyPress = true;
                    txtDate.Focus();
                    break;
                case Keys.Up when gridViewValues.FocusedRowHandle == 0:
                    txtScanerCodigo.Focus();
                    break;
                case Keys.Add:
                case Keys.Oemplus:
                    ButtonPlusQuantity_Click(sender, e);
                    break;
                case Keys.Subtract:
                case Keys.OemMinus:
                    ButtonMinusQuantity_Click(sender, e);
                    break;
                case Keys.Enter:
                {
                    var focusedColumn = gridViewValues.FocusedColumn;

                    if (focusedColumn.FieldName == colAccionMas.FieldName)
                    {
                        ButtonPlusQuantity_Click(sender, e);
                    }

                    if (focusedColumn.FieldName == colAccionMenos.FieldName)
                    {
                        ButtonMinusQuantity_Click(sender, e);
                    }

                    if (focusedColumn.FieldName == colAccionPrecios.FieldName)
                    {
                        var editor = (sender as GridView);
                        repositoryItemComboBox2.OwnerEdit.ShowPopup();
                    }

                    break;
                }
            }
        }

        private void EventoCallBackAceptarItem(dynamic mensaje)
        {
            // Realizar la lógica que desees en el formulario padre
            var objWebToolsHelper = new WebToolsHelper();
            FnOnCompleteNewItemPopPub(mensaje);
        }

        private void FormInvoiceBillingEdit_KeyDown(object sender, KeyEventArgs e)
        {
            //agregar una tecla para q el scaner tenga el focus
            switch (e.KeyData)
            {
                case Keys.F1:
                    //Ayuda
                    var panelAyuda = new FormInvoiceBillingEditHelpDialog();
                    panelAyuda.ShowDialog(this);
                    break;
                case Keys.F2:
                    //Imprimir
                    ComandPrinter();
                    break;
                case Keys.F3:
                    //Eliminar Factura
                    barManager1_ItemClick(sender, new ItemClickEventArgs(barManager1.Items["btnInvoiceDelete"], null));
                    break;
                case Keys.F4:
                    //Nueva Factura
                    btnNew_Click(sender, EventArgs.Empty);
                    break;
                case Keys.F5:
                    //Registrar Factura
                    btnRegistrar_Click(sender, EventArgs.Empty);
                    break;
                case Keys.F6:
                    //Aplicar Facutra
                    btnAplicar_Click(sender, EventArgs.Empty);
                    break;
                case Keys.F7:
                    //Buscar producto
                    btnAddProduct_Click(sender, EventArgs.Empty);
                    break;
                case Keys.F8:
                    //Metodo de pago
                    _backgroundWorker = new BackgroundWorker();
                    if (!progressPanel.Visible)
                    {
                        progressPanel.Visible = true;
                    }

                    _backgroundWorker.DoWork += (ob, ev) => { Invoke(() => FormInvoiceBillingEditPayment.ShowDialog(this)); };
                    _backgroundWorker.RunWorkerCompleted += (ob, ev) =>
                    {
                        if (ev.Error is not null)
                        {
                            CustomException.LogException(ev.Error);
                            _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Registrar", $"No se registraron los valores. {ev.Error.Message}", this);
                        }
                        else if (ev.Cancelled)
                        {
                            //cancelado por el usuario   
                            _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Informacion, "Registrar", "Se ha cancelado la operación actual. Linea 3424", this);
                        }
                        else
                        {
                            if (progressPanel.Visible)
                            {
                                progressPanel.Visible = false;
                            }
                        }
                    };

                    if (!_backgroundWorker.IsBusy)
                    {
                        _backgroundWorker.RunWorkerAsync();
                    }

                    break;
                case Keys.F9:
                    txtScanerCodigo.Focus();
                    break;
            }
        }

        private void btnSearchCustomer_Enter(object sender, EventArgs e)
        {
            btnSearchCustomer.Appearance.BackColor = Color.DarkBlue;
        }

        private void btnSearchCustomer_Leave(object sender, EventArgs e)
        {
            btnSearchCustomer.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
        }

        #endregion
    }
}