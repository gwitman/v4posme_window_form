using DevExpress.XtraEditors;
using System.ComponentModel;
using System.Globalization;
using System.IO;
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

namespace v4posme_window.Views.Box.Share;

public partial class FormShareEdit : FormTypeHeadEdit, IFormTypeEdit
{
    #region Campos

    private TypeOpenForm TypeOpen { get; set; }
    private int? TransactionMasterId = 0;
    private int? TransactionId = 0;
    private int? EntityId;
    private int? txtEmployeeID;
    private int txtCustomerID = 0;
    private RenderFileGridControl renderGridFiles;

    #endregion

    #region Modelos

    public TbCurrency? ObjListCurrencyDefault { get; set; }

    public List<TbCompanyCurrencyDto>? ObjListCurrency { get; set; }

    public List<TbCustomerDto> ObjListCustomer { get; set; }

    public List<TbWorkflowStage>? ObjListWorkflowStage { get; set; }

    public List<TbTransactionCausal>? ObjCaudal { get; set; }

    public decimal ExchangeRateSale { get; set; }

    public decimal ExchangeRatePurchase { get; set; }

    public decimal ExchangeRate { get; set; }

    public TbCurrency? ObjCurrency { get; set; }

    public TbNaturale? ObjNatural { get; set; }

    public TbCustomerDto? ObjCustomer { get; set; }

    public TbComponent? ObjComponentCustomerCreditDocument { get; set; }

    public TbComponent? ObjComponentCustomer { get; set; }

    public List<TbCustomerCreditDocumentDto> ObjListCustomerCreditDocument { get; set; }

    public string? UrlPrinterDocument { get; set; }

    public TbComponent? ObjComponentTransactionShare { get; set; }

    public TbComponent? ObjComponentEmployee { get; set; }

    public TbNaturale? ObjEmployeeNaturalDefault { get; set; }

    public TbEmployeeDto? ObjEmployeeDefault { get; set; }

    public string? ObjParameterShareInvoiceByInvoice { get; set; }

    public TbLegal? ObjLegalDefault { get; set; }

    public TbNaturale? ObjNaturalDefault { get; set; }

    public TbCustomerDto? ObjCustomerDefault { get; set; }

    public List<TbWorkflowStage> ObjWorkflowStage { get; set; }

    public List<TbTransactionMasterDetail> ObjTransactionMasterDetail { get; set; }

    public TbTransactionMasterInfoDto? ObjTransactionMasterInfo { get; set; }

    public TbTransactionMasterDto? ObjTransactionMaster { get; set; }

    #endregion

    #region Librerias

    private readonly ICoreWebAmortization objInterfazCoreWebAmortization = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebAmortization>();
    private readonly ICoreWebAuditoria objInterfazCoreWebAuditoria = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebAuditoria>();
    private readonly ICoreWebAccounting objInterfazCoreWebAccounting = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebAccounting>();
    private readonly ICoreWebCounter objInterfazCoreWebCounter = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCounter>();
    private readonly ICoreWebConcept objInterfazCoreWebConcept = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebConcept>();
    private readonly ICoreWebParameter objInterfazCoreWebParameter = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebParameter>();
    private readonly ICoreWebTransaction objInterfazCoreWebTransaction = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTransaction>();
    private readonly ICoreWebCurrency objInterfazCoreWebCurrency = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCurrency>();
    private readonly ICoreWebTools objInterfazCoreWebTools = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();
    private readonly ICoreWebWorkflow objInterfazCoreWebWorkflow = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebWorkflow>();
    private readonly CoreWebRenderInView objInterfazCoreWebRenderInView = new();
    private readonly ICoreWebPermission objInterfazCoreWebPermission = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebPermission>();
    private readonly ICustomerModel customerModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerModel>();
    private readonly ICustomerCreditDocumentModel customerCreditDocumentModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerCreditDocumentModel>();
    private readonly ICustomerCreditModel customerCreditModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerCreditModel>();
    private readonly ICustomerCreditLineModel customerCreditLineModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerCreditLineModel>();
    private readonly ICustomerCreditAmortizationModel customerCreditAmortizationModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerCreditAmortizationModel>();
    private readonly INaturalModel naturalModel = VariablesGlobales.Instance.UnityContainer.Resolve<INaturalModel>();
    private readonly ICurrencyModel currencyModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICurrencyModel>();
    private readonly ILegalModel legalModel = VariablesGlobales.Instance.UnityContainer.Resolve<ILegalModel>();
    private readonly ICompanyCurrencyModel companyCurrencyModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyCurrencyModel>();
    private readonly ITransactionModel transactionModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionModel>();
    private readonly ITransactionMasterModel transactionMasterModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterModel>();
    private readonly ITransactionMasterDetailModel transactionMasterDetailModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterDetailModel>();
    private readonly ITransactionCausalModel transactionCausalModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionCausalModel>();
    private readonly ITransactionMasterInfoModel transactionMasterInfoModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterInfoModel>();
    private readonly ITransactionMasterDetailCreditModel transactionMasterDetailCreditModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterDetailCreditModel>();
    private readonly ITransactionMasterDetailReferencesModel transactionMasterDetailReferencesModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterDetailReferencesModel>();
    private readonly IWorkflowStageModel workflowStageModel = VariablesGlobales.Instance.UnityContainer.Resolve<IWorkflowStageModel>();
    private readonly IEmployeeModel employeeModel = VariablesGlobales.Instance.UnityContainer.Resolve<IEmployeeModel>();
    private readonly ICompanyModel companyModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyModel>();
    private readonly IUserModel userModel = VariablesGlobales.Instance.UnityContainer.Resolve<IUserModel>();
    private readonly IBranchModel branchModel = VariablesGlobales.Instance.UnityContainer.Resolve<IBranchModel>();
    private readonly FormCxcApi formCxcApi = new FormCxcApi();

    #endregion

    #region Init

    public FormShareEdit()
    {
        InitializeComponent();
    }

    public FormShareEdit(TypeOpenForm typeOpen, int companyId, int transactionMasterId, int transactionId)
    {
        InitializeComponent();
        Application.ThreadException += Application_ThreadException;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        TypeOpen = typeOpen;
        TransactionMasterId = transactionMasterId;
        TransactionId = transactionId;
        btnRegresar.Click += CommandRegresar;
        btnGuardar.Click += CommandSave;
        btnEliminar.Click += BtnEliminarOnClick;
        btnNuevo.Click += CommandNew;
        btnImprmir.Click += BtnImprmir_Click;
    }

    public void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
    {
        CustomException.LogException(e.Exception);
    }

    public void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        CustomException.LogException((Exception)e.ExceptionObject);
    }

    private void FormShareEdit_Load(object sender, EventArgs e)
    {
        backgroundWorker = new BackgroundWorker();
        if (!progressPanel.Visible)
        {
            progressPanel.Width = Width;
            progressPanel.Height = Height;
            progressPanel.Visible = true;
        }

        backgroundWorker.DoWork += (ob, ev) =>
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
        backgroundWorker.RunWorkerCompleted += (ob, ev) =>
        {
            if (ev.Error is not null)
            {
                objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", ev.Error.Message, this);
            }
            else if (ev.Cancelled)
            {
                //se canceló por el usuario
                objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Warning, "Error", "Operación cancelada por el usuario", this);
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

                if (TypeOpen == TypeOpenForm.Init && TransactionMasterId == 0 && TransactionId > 0)
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

        if (!backgroundWorker.IsBusy)
        {
            backgroundWorker.RunWorkerAsync();
        }
    }

    #endregion

    #region Metodos

    public void ComandDelete()
    {
        var userNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];
        var notAccessControl = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_CONTROL"];
        var notAllDelete = VariablesGlobales.ConfigurationBuilder["NOT_ALL_DELETE"];
        var permissionNone = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_NONE"]);
        var appNeedAuthentication = VariablesGlobales.ConfigurationBuilder["APP_NEED_AUTHENTICATION"];
        var urlSuffix = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX"];
        var user = VariablesGlobales.Instance.User;
        if (user is null)
        {
            throw new Exception(userNotAutenticated);
        }

        var role = VariablesGlobales.Instance.Role;
        var company = VariablesGlobales.Instance.Company;
        var resultPermission = 0;
        if (appNeedAuthentication == "true")
        {
            var permited = objInterfazCoreWebPermission.UrlPermited("app_box_share", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (!permited)
            {
                throw new Exception(notAccessControl);
            }

            resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd("app_box_share", "delete", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (resultPermission == permissionNone)
            {
                throw new Exception(notAllDelete);
            }
        }

        var workflowStageAmortizationRegister = objInterfazCoreWebParameter.GetParameterValue("SHARE_AMORTIZATION_STATUS_REGISTER", user.CompanyID);
        var workflowStageDocumentRegister = objInterfazCoreWebParameter.GetParameterValue("SHARE_DOCUMENT_CREDIT_STATUS_REGISTER", user.CompanyID);
        var objCurrencyCordoba = objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyID);

        if (TransactionId is <= 0 && TransactionMasterId is <= 0)
        {
            throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_PARAMETER"]);
        }

        var objTm = transactionMasterModel.GetRowByPk(user.CompanyID, TransactionId!.Value, TransactionMasterId!.Value);

        var permissionMe = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_ME"]);
        if (resultPermission == permissionMe && objTm!.CreatedBy != user.UserID)
        {
            throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_DELETE"]);
        }

        if (objInterfazCoreWebAccounting.CycleIsCloseByDate(user.CompanyID, objTm.TransactionOn.Value))
        {
            throw new Exception("EL DOCUMENTO NO PUEDE ELIMINARSE, EL CICLO CONTABLE ESTA CERRADO");
        }

        var commandEliminable = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_ELIMINABLE"]);
        var validateWorkflowStage = objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_share", "statusID", objTm.StatusId.Value, commandEliminable, user.CompanyID, user.BranchID, role.RoleID);
        if (validateWorkflowStage.HasValue && validateWorkflowStage.Value)
        {
            //Eliminar el Registro	
            transactionMasterModel.DeleteAppPosme(user.CompanyID, objTm.TransactionId, objTm.TransactionMasterId);
            transactionMasterDetailModel.DeleteWhereTm(user.CompanyID, objTm.TransactionId, objTm.TransactionMasterId);
        } //Si el documento esta aplicado crear el contra documento
        else
        {
            if (role.IsAdmin.HasValue && !role.IsAdmin.Value)
            {
                throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_WORKFLOW_DELETE"]);
            }

            var objTmd = transactionMasterDetailModel.GetRowByTransactionToShare(user.CompanyID, objTm.TransactionId, objTm.TransactionMasterId);
            var objTMDR = transactionMasterDetailReferencesModel.GetRowByTransactionMasterId(objTm.TransactionMasterId);
            var objTMSOld = transactionMasterModel.getRowByTransactionIDAndEntityID(objTm.CompanyId, objTm.TransactionId, objTm.EntityId!.Value);
            if (objTMSOld.Count > 0)
            {
                if (objTMSOld.ElementAt(0).TransactionMasterID != TransactionMasterId.Value)
                {
                    //no existe el parametro para este mensaje
                    throw new Exception("SOLO PUEDE ELIMINAR EL ULTIMO ABONO DEL CLIENTE...");
                }
            }

            //Recorrare las cuotas a regresar
            foreach (var tbTransactionMasterDetailReference in objTMDR)
            {
                var totalCordobaCredit = Convert.ToDecimal(tbTransactionMasterDetailReference.Quantity);
                var objCCA = customerCreditAmortizationModel.GetRowByPk(tbTransactionMasterDetailReference.ComponentItemID ?? 0);
                if (objCCA is null)
                {
                    //no existe el parametro para este mensaje
                    throw new Exception("NO EXISTE EL OBJETO CUSTOMER CREDIT AMORTIZACION...");
                }

                objCCA.Remaining += totalCordobaCredit;
                objCCA.StatusID = Convert.ToInt32(workflowStageAmortizationRegister);
                objCCA.DayDelay = 0;
                customerCreditAmortizationModel.UpdateAppPosme(tbTransactionMasterDetailReference.ComponentItemID!.Value, objCCA);

                //Actualizar Documento
                var objCC = customerCreditDocumentModel.GetRowByPkk(objCCA.CustomerCreditDocumentID);
                if (objCC is null)
                {
                    //no existe el parametro para este mensaje
                    throw new Exception("NO EXISTE EL OBJETO CUSTOMER CREDIT DOCUMENT...");
                }

                objCC.StatusID = Convert.ToInt32(workflowStageDocumentRegister);
                objCC.Balance += totalCordobaCredit;
                customerCreditDocumentModel.UpdateAppPosme(objCCA.CustomerCreditDocumentID, objCC);

                //Obtener Linea de Credito
                var objCustomerCreditLine = customerCreditLineModel.GetRowByPk(objCC.CustomerCreditLineID);

                //Actualizar Linea de Credito
                if (objCustomerCreditLine is null)
                {
                    //no existe el parametro para este mensaje
                    throw new Exception("NO EXISTE EL OBJETO CUSTOMER CREDIT LINE...");
                }

                var montoTotalCordobaCredit = objTm.CurrencyId.Value == 1 ? totalCordobaCredit : Math.Round(totalCordobaCredit * objTm.ExchangeRate!.Value);
                var montoTotalDolaresCredit = objTm.CurrencyId.Value == 2 ? totalCordobaCredit : Math.Round(totalCordobaCredit / objTm.ExchangeRate!.Value);

                //aumentar el balance de general
                var objCustomerCredit = customerCreditModel.GetRowByPk(objCustomerCreditLine.CompanyID, objCustomerCreditLine.BranchID, objCustomerCreditLine.EntityID);
                if (objCustomerCredit is null)
                {
                    throw new Exception("NO EXISTE EL OBJETO CUSTOMER CREDIT...");
                }

                objCustomerCredit.BalanceDol += montoTotalDolaresCredit;
                customerCreditModel.UpdateAppPosme(objCustomerCreditLine.CompanyID, objCustomerCreditLine.BranchID, objCustomerCreditLine.EntityID, objCustomerCredit);
                //aumentar el balance de linea
                if (objCustomerCreditLine.CurrencyID == objCurrencyCordoba.CurrencyID)
                {
                    objCustomerCreditLine.Balance += montoTotalCordobaCredit;
                }
                else
                {
                    objCustomerCreditLine.Balance += montoTotalDolaresCredit;
                }

                customerCreditLineModel.UpdateAppPosme(objCustomerCreditLine.CustomerCreditLineID, objCustomerCreditLine);
            }

            transactionMasterModel.DeleteAppPosme(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
            transactionMasterDetailModel.DeleteWhereTm(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
        }
    }

    public void ComandPrinter()
    {
        var frmSelectPrint = new FormShareEditOpcionPrint();
        if (frmSelectPrint.ShowDialog() == DialogResult.Cancel)
        {
            return;
        }

        try
        {
            var selectedResult = frmSelectPrint.Result;
            if (selectedResult is null)
            {
                throw new Exception("Seleccione una opcion para continuar");
            }

            var user = VariablesGlobales.Instance.User;
            if (user is null)
            {
                throw new Exception("Usuario no logeado");
            }

            if (Convert.ToInt32(selectedResult.Key) == 4)
            {
                ViewRegisterFormatoPaginaTicketInvoiceCancel();
                return;
            }

            var role = VariablesGlobales.Instance.Role;
            var transactionID = TransactionId.Value;
            var transactionMasterID = TransactionMasterId.Value;
            var companyID = user.CompanyID;
            var accountingExchangeSale = objInterfazCoreWebParameter.GetParameterValue("ACCOUNTING_EXCHANGE_SALE", companyID);
            //Get Logo
            var objParameterCompanyLogo = objInterfazCoreWebParameter.GetParameterValue("CORE_COMPANY_LOGO", companyID);
            var objParameterTelefono = objInterfazCoreWebParameter.GetParameterValue("CORE_PHONE", companyID);

            //get company
            var objCompany = companyModel.GetRowByPk(companyID);

            //Get Documento
            //Obtener Datos
            var objTm = transactionMasterModel.GetRowByPk(companyID, transactionID, transactionMasterID);
            var objTMD = transactionMasterDetailModel.GetRowByTransactionToShare(companyID, transactionID, transactionMasterID);
            var objTMI = transactionMasterInfoModel.GetRowByPk(companyID, transactionID, transactionMasterID);
            var objUser = userModel.GetRowByPk(objTm.CompanyId, objTm.BranchId.Value, objTm.CreatedBy.Value);
            var objBranch = branchModel.GetRowByPk(objTm.CompanyId, objTm.BranchId.Value);
            var objCustumer = customerModel.GetRowByEntity(objTm.CompanyId, objTm.EntityId.Value);
            var objNatural = naturalModel.GetRowByPk(objTm.CompanyId, objTm.BranchId.Value, objTm.EntityId.Value);
            var tipoCambio = Math.Round(objTm.ExchangeRate.Value + Decimal.Parse(accountingExchangeSale));
            var objCurrency = currencyModel.GetRowByPk(objTm.CurrencyId.Value);
            var objStage = objInterfazCoreWebWorkflow.GetWorkflowStage("tb_transaction_master_share", "statusID", objTm.StatusId.Value, objTm.CompanyId, objTm.BranchId.Value, role.RoleID);

            // Formatear la fecha de la transacción
            var transactionDate = objTm.TransactionOn;
            var formattedTransactionDate = transactionDate!.Value.ToString("yyyy-M-d HH:mm:ss");

            // Imprimir el documento               
            var printerName = objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_PRINTER_DIRECT_NAME_DEFAULT", companyID);
            var pathOfLogo = VariablesGlobales.ConfigurationBuilder["PATH_FILE_OF_APP_ROOT"];
            var printer = new Printer(printerName!.Value);

            printer.AlignCenter();
            if (objParameterCompanyLogo is not null)
            {
                objParameterCompanyLogo = "direct-ticket-" + objParameterCompanyLogo;
                var imagePath = $"{pathOfLogo}/img/logos/{objParameterCompanyLogo!}";
                if (File.Exists(imagePath))
                {
                    var logoCompany = new Bitmap(Image.FromFile(imagePath));
                    //el logo que se esta mostrando no se redimensiona 
                    //printer.Image(logoCompany);
                }
            }

            printer.AlignCenter();
            printer.Append(objCompany.Name);
            printer.BoldMode("ABONO");
            printer.Append($"# {objTm.TransactionNumber}");
            printer.Append($"FECHA: {formattedTransactionDate} ");
            printer.Separator();
            printer.AlignLeft();
            var datos = $"""
                         CODIGO:   {objCustumer.CustomerNumber}
                         ESTADO:   {objStage.First().Name}
                         MONEDA:   {objCurrency.Name}
                         """;
            printer.Append(datos);
            printer.Append("CLIENTE");
            printer.Append($"{objCustumer.FirstName} {objCustumer.LastName}");
            printer.NewLine();
            var saldoInicial = objTMD.Sum(detail => Convert.ToDecimal(detail.Reference2));
            var saldoFinal = objTMD.Sum(detail => Convert.ToDecimal(detail.Reference4));
            var saldoAbonado = objTMD.Sum(detail => detail.Amount);

            /*Calculo de saldos generales*/
            var saldoInicialGeneral = decimal.Zero;
            if (!string.IsNullOrWhiteSpace(objTMI.Reference1))
            {
                saldoInicialGeneral = Math.Round(Convert.ToDecimal(objTMI.Reference1));
            }

            var saldoFinalGeneral = decimal.Zero;
            if (!string.IsNullOrWhiteSpace(objTMI.Reference2))
            {
                saldoFinalGeneral = Math.Round(Convert.ToDecimal(objTMI.Reference2));
            }

            saldoInicial = selectedResult.Value.ToString() == "Individual" ? saldoInicial : saldoInicialGeneral;
            saldoFinal = selectedResult.Value.ToString() == "Individual" ? saldoFinal : saldoFinalGeneral;

            var detalle = new Dictionary<string, string>(); // Lista de listas para almacenar los datos

            if (selectedResult.Value.ToString() != "Basico")
            {
                // SALDO INICIAL
                detalle.Add("SALDO INICIAL", $"{saldoInicial:N2}");
                // ABONOS
                var aux = 1;
                foreach (var detail in objTMD)
                {
                    detalle.Add($"ABONO {aux}", $"{Math.Round(detail.Amount!.Value, 2):N2}");
                    aux++;
                }

                // SALDO FINAL
                detalle.Add("SALDO FINAL", $"{saldoFinal:N2}");
            }

            if (detalle.Count > 0)
            {
                foreach (var deta in detalle)
                {
                    printer.Append($"{deta.Key}               {objCurrency.Simbol} {deta.Value}");
                }
            }
            else
            {
                printer.Append($"SALDO INIIAL               {objCurrency.Simbol} {saldoInicial:N2}");
                printer.Append($"ABONO                      {objCurrency.Simbol} {saldoAbonado:N2}");
                printer.Append($"SALDO FINAL                {objCurrency.Simbol} {saldoFinal:N2}");
            }

            printer.NewLine();
            printer.Append($"SUB-TOTAL               {objCurrency.Simbol} {objTm.SubAmount:N2 ?? decimal.Zero:N2}");
            printer.Append($"IVA                     {objCurrency.Simbol} {objTm.Tax1:N2 ?? decimal.Zero:N2}");
            printer.Append($"DESC                    {objCurrency.Simbol} {objTm.Discount:N2 ?? decimal.Zero:N2}");
            printer.Append($"TOTAL                   {objCurrency.Simbol} {objTm.Amount:N2}");
            printer.Append($"RECIBIDO                {objCurrency.Simbol} {objTm.Amount - objTMI.ChangeAmount:N2}");
            printer.NewLine();
            printer.AlignCenter();
            printer.Append(objCompany.Address);
            printer.Append($"Tel.: {objParameterTelefono!}");
            printer.FullPaperCut();
            printer.PrintDocument();
        }
        catch (Exception e)
        {
            objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Imprimir", $"Se produjo un error al imprimir, revisar los datos. Error: {e.Message}", this);
        }
    }

    public void LoadEdit()
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
            var permited = objInterfazCoreWebPermission.UrlPermited("app_box_share", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (!permited)
            {
                throw new Exception(notAccessControl);
            }

            var resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd("app_box_share", "edit", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (resultPermission == permissionNone)
            {
                throw new Exception(notAllInsert);
            }
        }

        if (TransactionId is <= 0 && TransactionMasterId is <= 0)
        {
            PreRender();
            LoadNew();
            LoadRender(TypeRender.New);
            return;
        }

        //Obtener el componente de Item
        ObjComponentCustomer = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_customer");
        if (ObjComponentCustomer is null)
        {
            throw new Exception("EL COMPONENTE 'tb_customer' NO EXISTE...");
        }

        //Obtener el componente del recolector de cobro
        ObjComponentEmployee = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_employee");
        if (ObjComponentCustomer is null)
        {
            throw new Exception("EL COMPONENTE 'tb_employee' NO EXISTE...");
        }

        //Componente de facturacion
        ObjComponentTransactionShare = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_transaction_master_share");
        if (ObjComponentTransactionShare is null)
        {
            throw new Exception("EL COMPONENTE 'tb_transaction_master_share' NO EXISTE...");
        }

        //Componente de facturacion
        ObjComponentCustomerCreditDocument = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_customer_credit_document");
        if (ObjComponentCustomerCreditDocument is null)
        {
            throw new Exception("EL COMPONENTE 'tb_customer_credit_document' NO EXISTE...");
        }

        //Obtener el Componente de Transacciones Facturacion
        var objComponentAmortization = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_customer_credit_amoritization");
        if (objComponentAmortization is null)
        {
            throw new Exception("EL COMPONENTE 'tb_customer_credit_amoritization' NO EXISTE...");
        }

        var objCurrency = objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyID);
        var targetCurrency = objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyID);
        UrlPrinterDocument = objInterfazCoreWebParameter.GetParameterValue("BOX_SHARE_URL_PRINTER", user.CompanyID);

        //Tipo de Factura
        ObjTransactionMaster = transactionMasterModel.GetRowByPk(user.CompanyID, TransactionId!.Value, TransactionMasterId!.Value);
        ObjTransactionMasterInfo = transactionMasterInfoModel.GetRowByPk(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
        ObjTransactionMasterDetail = transactionMasterDetailModel.GetRowByTransactionToShare(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
        ExchangeRate = objInterfazCoreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, decimal.One, targetCurrency.CurrencyID, objCurrency.CurrencyID);
        ObjListWorkflowStage = objInterfazCoreWebWorkflow.GetWorkflowStageByStageInit("tb_transaction_master_share", "statusID", ObjTransactionMaster.StatusId.Value, user.CompanyID, user.BranchID, role.RoleID);
        ObjWorkflowStage = workflowStageModel.GetRowByWorkflowStageIdOnly(ObjTransactionMaster.StatusId.Value);
        ObjCustomerDefault = customerModel.GetRowByEntity(user.CompanyID, ObjTransactionMaster.EntityId.Value);
        ObjNaturalDefault = naturalModel.GetRowByPk(user.CompanyID, user.BranchID, ObjCustomerDefault.EntityId);
        ObjLegalDefault = legalModel.GetRowByPk(user.CompanyID, user.BranchID, ObjCustomerDefault.EntityId);
        ObjParameterShareInvoiceByInvoice = objInterfazCoreWebParameter.GetParameterValue("SHARE_INVOICE_BY_INVOICE", user.CompanyID);
        ObjListCustomer = customerModel.GetRowByCompany(user.CompanyID);
        ObjListCurrency = companyCurrencyModel.GetByCompany(user.CompanyID);
        ObjListCurrencyDefault = objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyID);
        if (!string.IsNullOrWhiteSpace(ObjTransactionMaster.Reference3))
        {
            ObjEmployeeDefault = employeeModel.GetRowByEntityId(user.CompanyID, Convert.ToInt32(ObjTransactionMaster.Reference3));
            if (ObjEmployeeDefault is not null)
            {
                ObjEmployeeNaturalDefault = naturalModel.GetRowByPk(user.CompanyID, user.BranchID, ObjEmployeeDefault.EntityId);
            }
        }
    }

    public void LoadNew()
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
            var permited = objInterfazCoreWebPermission.UrlPermited("app_box_share", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (!permited)
            {
                throw new Exception(notAccessControl);
            }

            var resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd("app_box_share", "add", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (resultPermission == permissionNone)
            {
                throw new Exception(notAllInsert);
            }
        }

        ObjComponentCustomer = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_customer");
        if (ObjComponentCustomer is null)
        {
            throw new Exception("EL COMPONENTE 'tb_customer' NO EXISTE...");
        }

        ObjComponentCustomerCreditDocument = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_customer_credit_document");
        if (ObjComponentCustomerCreditDocument is null)
        {
            throw new Exception("EL COMPONENTE 'tb_customer_credit_document' NO EXISTE...");
        }

        if (EntityId is not null)
        {
            ObjCustomer = customerModel.GetRowByEntity(user.CompanyID, EntityId.Value);
            if (ObjCustomer is not null)
            {
                ObjNatural = naturalModel.GetRowByPk(user.CompanyID, ObjCustomer.BranchId, EntityId.Value);
            }
        }

        TransactionId = objInterfazCoreWebTransaction.GetTransactionId(user.CompanyID, "tb_transaction_master_share", 0) ?? 0;
        ObjCurrency = objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyID);
        if (ObjCurrency is null)
        {
            throw new Exception("No existe el 'currency local'");
        }

        var targetCurrency = objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyID);
        if (targetCurrency is null)
        {
            throw new Exception("No existe el 'currency external'");
        }

        ExchangeRate = objInterfazCoreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, decimal.One, targetCurrency.CurrencyID, ObjCurrency.CurrencyID);
        var objParameterExchangePurchase = objInterfazCoreWebParameter.GetParameterValue("ACCOUNTING_EXCHANGE_PURCHASE", user.CompanyID);
        var parameterExchangePurchase = decimal.Zero;
        if (string.IsNullOrWhiteSpace(objParameterExchangePurchase))
        {
            throw new Exception("No existe el parametro ACCOUNTING_EXCHANGE_PURCHASE");
        }

        if (decimal.TryParse(objParameterExchangePurchase, out parameterExchangePurchase))
        {
            ExchangeRatePurchase = ExchangeRate - parameterExchangePurchase;
        }

        ExchangeRatePurchase = ExchangeRate - parameterExchangePurchase;
        var objParameterExchangeSales = objInterfazCoreWebParameter.GetParameterValue("ACCOUNTING_EXCHANGE_SALE", user.CompanyID);
        var parameterExchangeSales = decimal.Zero;
        if (string.IsNullOrWhiteSpace(objParameterExchangeSales))
        {
            throw new Exception("No existe el parametro ACCOUNTING_EXCHANGE_SALE");
        }

        if (decimal.TryParse(objParameterExchangeSales, out parameterExchangeSales))
        {
            ExchangeRateSale = ExchangeRate + parameterExchangeSales;
        }

        ExchangeRateSale = ExchangeRate + parameterExchangeSales;

        ObjCaudal = transactionCausalModel.GetCausalByBranch(user.CompanyID, TransactionId.Value, user.BranchID);
        ObjListWorkflowStage = objInterfazCoreWebWorkflow.GetWorkflowInitStage("tb_transaction_master_share", "statusID", user.CompanyID, user.BranchID, role.RoleID);
        ObjListCustomer = customerModel.GetRowByCompany(user.CompanyID);
        ObjListCurrency = companyCurrencyModel.GetByCompany(user.CompanyID);
        ObjListCurrencyDefault = objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyID);
    }


    public void SaveInsert()
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
            var permited = objInterfazCoreWebPermission.UrlPermited("app_box_share", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (!permited)
            {
                throw new Exception(notAccessControl);
            }

            var resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd("app_box_share", "add", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (resultPermission == permissionNone)
            {
                throw new Exception(notAllInsert);
            }
        }

        objInterfazCoreWebPermission.GetValueLicense(user.CompanyID, "app_box_share/index");
        var objcomponentShare = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_transaction_master_share");
        if (objcomponentShare is null)
        {
            throw new Exception("EL COMPONENTE 'tb_transaction_master_share' NO EXISTE...");
        }

        var objComponentAmortization = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_customer_credit_amoritization");
        if (objComponentAmortization is null)
        {
            throw new Exception("EL COMPONENTE 'tb_customer_credit_amoritization' NO EXISTE...");
        }

        ObjComponentCustomer = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_customer");
        if (ObjComponentCustomer is null)
        {
            throw new Exception("EL COMPONENTE 'tb_customer' NO EXISTE...");
        }

        if (objInterfazCoreWebAccounting.CycleIsCloseByDate(user.CompanyID, txtDate.DateTime))
        {
            throw new Exception("EL DOCUMENTO NO PUEDE INGRESAR, EL CICLO CONTABLE ESTA CERRADO");
        }

        TransactionId = objInterfazCoreWebTransaction.GetTransactionId(user.CompanyID, "tb_transaction_master_share", 0);
        if (!TransactionId.HasValue)
        {
            throw new Exception("No se pudo recuperar el TransactionID");
        }

        var objT = transactionModel.GetByCompanyAndTransaction(user.CompanyID, TransactionId.Value);
        if (objT is null)
        {
            throw new Exception($"No hay una trasaction con el Trasaction ID: {TransactionId.Value}");
        }

        var selectedCurrency = (ComboBoxItem)txtCurrencyID.SelectedItem;
        var selectedStatus = (ComboBoxItem)txtStatusID.SelectedItem;


        var objTm = new TbTransactionMaster()
        {
            CompanyID = user.CompanyID,
            TransactionID = TransactionId.Value,
            BranchID = user.BranchID,
            TransactionNumber = objInterfazCoreWebCounter.GoNextNumber(user.CompanyID, user.BranchID, "tb_transaction_master_share", 0) ?? string.Empty,
            TransactionCausalID = objInterfazCoreWebTransaction.GetDefaultCausalId(user.CompanyID, TransactionId.Value),
            EntityID = txtCustomerID,
            TransactionOn = txtDate.DateTime,
            StatusIDChangeOn = DateTime.Now,
            ComponentID = objcomponentShare.ComponentID,
            Note = txtNote.Text,
            Sign = (short?)(objT.SignInventory ?? 0),
            CurrencyID = Convert.ToInt32(selectedCurrency.Key),
            CurrencyID2 = objInterfazCoreWebCurrency.GetTarget(user.CompanyID, Convert.ToInt32(selectedCurrency.Key)),
            ExchangeRate = ExchangeRate,
            Reference1 = txtReference1.Text,
            Reference2 = txtReference2.Text,
            Reference3 = string.Empty,
            Reference4 = string.Empty,
            StatusID = Convert.ToInt32(selectedStatus.Key),
            Amount = Convert.ToDecimal(txtTotal.EditValue),
            IsApplied = false,
            JournalEntryID = 0,
            ClassID = null,
            AreaID = null,
            SourceWarehouseID = null,
            TargetWarehouseID = null,
            IsActive = true
        };
        objInterfazCoreWebAuditoria.SetAuditCreated(objTm, user, "");

        TransactionMasterId = transactionMasterModel.InsertAppPosme(objTm);

        var pathFileOfApp = VariablesGlobales.ConfigurationBuilder["PATH_FILE_OF_APP"];
        var path = $"{pathFileOfApp}/company_{user.CompanyID}/component_{objcomponentShare.ComponentID}/component_item_{TransactionMasterId}";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var objTmInfo = new TbTransactionMasterInfo()
        {
            CompanyID = user.CompanyID,
            TransactionID = TransactionId.Value,
            TransactionMasterID = TransactionMasterId.Value,
            ZoneID = 0,
            RouteID = 0,
            ReferenceClientName = txtReferenceClientName.Text,
            ReferenceClientIdentifier = txtReferenceClientIdentifier.Text,
            ReceiptAmount = Convert.ToDecimal(txtReceiptAmount.EditValue),
            Reference1 = $"{txtBalanceStart.EditValue}",
            Reference2 = $"{txtBalanceFinish.EditValue}"
        };
        transactionMasterInfoModel.InsertAppPosme(objTmInfo);
        var amount = decimal.Zero;
        if (bindingSourceDetailDto.List is IList<FormShareEditDetailDTO> formShareEditDetailDtos && formShareEditDetailDtos.Count > 0)
        {
            foreach (var detail in formShareEditDetailDtos)
            {
                var objTmd = new TbTransactionMasterDetail
                {
                    CompanyID = user.CompanyID,
                    TransactionID = TransactionId.Value,
                    TransactionMasterID = TransactionMasterId.Value,
                    ComponentID = objcomponentShare.ComponentID,
                    ComponentItemID = detail.DetailCustomerCreditDocumentId,
                    Quantity = decimal.Zero,
                    UnitaryCost = decimal.Zero,
                    Cost = decimal.Zero,
                    UnitaryPrice = decimal.Zero,
                    UnitaryAmount = decimal.Zero,
                    Amount = detail.DetailShare,
                    Discount = decimal.Zero,
                    PromotionID = 0,
                    Reference1 = detail.DetailTransactionDetailDocument,
                    Reference2 = $"{detail.DetailBalanceStart}",
                    Reference3 = $"{detail.DetailAmortizationId}",
                    Reference4 = "0",
                    CatalogStatusID = 0,
                    InventoryStatusID = 0,
                    IsActive = true,
                    QuantityStock = decimal.Zero,
                    QuantiryStockInTraffic = decimal.Zero,
                    QuantityStockUnaswared = decimal.Zero,
                    ExpirationDate = null,
                    InventoryWarehouseSourceID = objTm.SourceWarehouseID,
                    InventoryWarehouseTargetID = objTm.TargetWarehouseID,
                };
                amount = decimal.Add(amount, objTmd.Amount.Value);
                transactionMasterDetailModel.InsertAppPosme(objTmd);
            }
        }

        transactionMasterModel.UpdateAppPosme(user.CompanyID, TransactionId.Value, TransactionMasterId.Value, objTm);
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
            var permited = objInterfazCoreWebPermission.UrlPermited("app_box_share", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (!permited)
            {
                throw new Exception(notAccessControl);
            }

            resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd("app_box_share", "edit", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (resultPermission == permissionNone)
            {
                throw new Exception(notAllEdit);
            }
        }

        var objcomponentShare = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_transaction_master_share");
        if (objcomponentShare is null)
        {
            throw new Exception("EL COMPONENTE 'tb_transaction_master_share' NO EXISTE...");
        }

        var objComponentAmortization = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_customer_credit_amoritization");
        if (objComponentAmortization is null)
        {
            throw new Exception("EL COMPONENTE 'tb_customer_credit_amoritization' NO EXISTE...");
        }

        ObjComponentCustomer = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_customer");
        if (ObjComponentCustomer is null)
        {
            throw new Exception("EL COMPONENTE 'tb_customer' NO EXISTE...");
        }

        var typeAmortizationAmericanoId = objInterfazCoreWebParameter.GetParameterValue("CXC_AMERICANO", user.CompanyID);
        if (!TransactionId.HasValue)
        {
            throw new Exception("No hay un valor para Transaciton Id");
        }

        if (!TransactionMasterId.HasValue)
        {
            throw new Exception("No hay un valor para Transaciton Master Id");
        }

        var objTm = transactionMasterModel.GetRowByPk(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
        if (objTm is null)
        {
            throw new Exception("No se encontró el Transaction Master de este documento");
        }

        var objCurrencyDolares = objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyID);
        var objCurrencyCordoba = objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyID);

        var permissionMe = VariablesGlobales.ConfigurationBuilder["PERMISSION_ME"];
        if (resultPermission == Convert.ToInt32(permissionMe) && objTm.CreatedBy != user.UserID)
        {
            throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_EDIT"]);
        }

        var commandEditableTotal = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_EDITABLE_TOTAL"]);
        var validateWorkflow = objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_share", "statusID", objTm.StatusId!.Value, commandEditableTotal, user.CompanyID, user.BranchID, role.RoleID);
        if (validateWorkflow.HasValue && !validateWorkflow.Value)
        {
            throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_WORKFLOW_EDIT"]);
        }

        if (objInterfazCoreWebAccounting.CycleIsCloseByDate(user.CompanyID, objTm.TransactionOn!.Value))
        {
            throw new Exception("EL DOCUMENTO NO PUEDE ACTUALIZARCE, EL CICLO CONTABLE ESTA CERRADO");
        }

        var objT = transactionModel.GetByCompanyAndTransaction(user.CompanyID, TransactionId.Value);
        if (objT is null)
        {
            throw new Exception($"No hay una trasaction con el Trasaction ID: {TransactionId.Value}");
        }

        var selectedCurrency = txtCurrencyID.SelectedItem as ComboBoxItem;
        var selectedStatus = (ComboBoxItem)txtStatusID.SelectedItem;

        using var dbTransaction = VariablesGlobales.Instance.DataContext.Database.BeginTransaction();
        try
        {
            var objTmNew = transactionMasterModel.GetRowByPKK(TransactionMasterId.Value)!;
            objTmNew.EntityID = txtCustomerID;
            objTmNew.TransactionOn = txtDate.DateTime;
            objTmNew.StatusIDChangeOn = DateTime.Now;
            objTmNew.Note = txtNote.Text;
            objTmNew.Reference1 = txtReference1.Text;
            objTmNew.Reference2 = txtReference2.Text;
            objTmNew.Reference3 = $"{txtEmployeeID}";
            objTmNew.Reference4 = "txtCustomerCreditLineID"; //no encontré el campo
            objTmNew.DescriptionReference = "reference1:input,reference2:input,reference3:Gestor de Cobro,reference4:Linea de credito del Cliente";
            objTmNew.StatusID = Convert.ToInt32(selectedStatus.Key);
            objTmNew.Amount = Convert.ToDecimal(txtTotal.EditValue);
            objTmNew.CurrencyID = Convert.ToInt32(selectedCurrency.Key);
            objTmNew.CurrencyID2 = objInterfazCoreWebCurrency.GetTarget(user.CompanyID, Convert.ToInt32(selectedCurrency.Key));
            objTmNew.ExchangeRate = objInterfazCoreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, decimal.One, objTmNew.CurrencyID2.Value, objTmNew.CurrencyID.Value);

            //Ingresar Informacion Adicional
            var objTMInfoNew = transactionMasterInfoModel.GetRowByPkPk(TransactionMasterId.Value)!;
            objTMInfoNew.CompanyID = objTm.CompanyId;
            objTMInfoNew.TransactionID = objTm.TransactionId;
            objTMInfoNew.TransactionMasterID = objTm.TransactionMasterId;
            objTMInfoNew.ZoneID = 0;
            objTMInfoNew.RouteID = 0;
            objTMInfoNew.ReferenceClientName = txtReferenceClientName.Text;
            objTMInfoNew.ReferenceClientIdentifier = txtReferenceClientIdentifier.Text;
            objTMInfoNew.ReceiptAmount = Convert.ToDecimal(txtReceiptAmount.EditValue);
            objTMInfoNew.ChangeAmount = Convert.ToDecimal(txtChangeAmount.EditValue);
            objTMInfoNew.Reference1 = $"{txtBalanceStart.EditValue}";
            objTMInfoNew.Reference2 = $"{txtBalanceFinish.EditValue}";

            var commandEditable = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_EDITABLE"]);
            validateWorkflow = objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_share", "statusID", objTm.StatusId!.Value, commandEditable, user.CompanyID, user.BranchID, role.RoleID);
            if (validateWorkflow.HasValue && validateWorkflow.Value)
            {
                var tbTransactionMaster = transactionMasterModel.GetRowByPKK(TransactionMasterId.Value);
                tbTransactionMaster.StatusID = Convert.ToInt32(selectedStatus.Key);
                transactionMasterModel.UpdateAppPosme(user.CompanyID, objTm.TransactionId, objTm.TransactionMasterId, tbTransactionMaster);
            }
            else
            {
                transactionMasterModel.UpdateAppPosme(user.CompanyID, objTm.TransactionId, objTm.TransactionMasterId, objTmNew);
                transactionMasterInfoModel.UpdateAppPosme(user.CompanyID, objTm.TransactionId, objTm.TransactionMasterId, objTMInfoNew);
            }

            var formShareEditDetailDtos = bindingSourceDetailDto.List as IList<FormShareEditDetailDTO>;
            //Actualizar Detalle
            var shareInvoiceByInvoice = objInterfazCoreWebParameter.GetParameterValue("SHARE_INVOICE_BY_INVOICE", user.CompanyID);
            var arrayListCustomerCreditDocumentId = formShareEditDetailDtos!.Select(dto => dto.DetailCustomerCreditDocumentId).ToList();
            var arrayListTransactionDetailId = formShareEditDetailDtos.Select(dto => dto.DetailTransactionDetailId).ToList();
            var arrayListTransactionDetailDocument = formShareEditDetailDtos.Select(dto => dto.DetailTransactionDetailDocument).ToList();
            var arrayListTransactionDetailFecha = formShareEditDetailDtos.Select(dto => dto.DetailTransactionDetailFecha).ToList();
            var arrayListCustomerCreditAmortizationId = formShareEditDetailDtos.Select(dto => dto.DetailAmortizationId).ToList();
            var arrayListShare = formShareEditDetailDtos.Select(dto => dto.DetailShare).ToList();
            decimal abonoTotal;
            var amount = decimal.Zero;

            transactionMasterDetailModel.DeleteWhereIdNotIn(user.CompanyID, objTm.TransactionId, objTm.TransactionMasterId, arrayListTransactionDetailId);

            //Si este valor esta en true, se tiene que seleccionar una factura a la ves, par ir abonando una a una
            //Si este valor es false, se puede seleccionar una sola factura, y el sistema va aplicado automaticamente, segun el monto.
            //Si este valor es false, ingreas a la funcion 
            if (string.Compare(shareInvoiceByInvoice, "false", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                //Sumar abono total.
                abonoTotal = arrayListShare.Sum();

                transactionMasterDetailModel.DeleteWhereTm(user.CompanyID, objTm.TransactionId, objTm.TransactionMasterId);

                if (arrayListTransactionDetailId.Count > 0)
                {
                    arrayListCustomerCreditDocumentId = new List<int>();
                    arrayListTransactionDetailId = new List<int>();
                    arrayListTransactionDetailDocument = new List<string>();
                    arrayListTransactionDetailFecha = new List<DateTime?>();
                    arrayListCustomerCreditAmortizationId = new List<int>();
                    arrayListShare = new List<decimal>();
                    var customerCreditDocumentIDMin = formShareEditDetailDtos.ElementAt(0).DetailCustomerCreditDocumentId;
                    var objListDocumentoAmortization = formCxcApi.GetCustomerBalance(txtCustomerID, Convert.ToInt32(selectedCurrency));
                    //Obtener el banace total pendietne
                    var objListDocumentoAmortizationBalanceTotal = objListDocumentoAmortization.Sum(dto => dto.Remaining) ?? decimal.Zero;

                    if (decimal.Compare(objListDocumentoAmortizationBalanceTotal, abonoTotal) < 0)
                    {
                        throw new Exception("ABONO, no puede ser aplicado por que supera el monto del saldo");
                    }

                    foreach (var it in objListDocumentoAmortization)
                    {
                        if (abonoTotal > it.Remaining)
                        {
                            // Agregar y disminuir
                            abonoTotal -= it.Remaining!.Value;
                            arrayListCustomerCreditDocumentId.Add(it.CustomerCreditDocumentId!.Value);
                            arrayListTransactionDetailId.Add(0);
                            arrayListTransactionDetailDocument.Add(it.DocumentNumber ?? string.Empty);
                            arrayListTransactionDetailFecha.Add(it.DateApply);
                            arrayListCustomerCreditAmortizationId.Add(it.CreditAmortizationId);
                            arrayListShare.Add(it.Remaining.Value);
                        }
                        else
                        {
                            arrayListCustomerCreditDocumentId.Add(it.CustomerCreditDocumentId!.Value);
                            arrayListTransactionDetailId.Add(0);
                            arrayListTransactionDetailDocument.Add(it.DocumentNumber ?? string.Empty);
                            arrayListTransactionDetailFecha.Add(it.DateApply);
                            arrayListCustomerCreditAmortizationId.Add(it.CreditAmortizationId);
                            arrayListShare.Add(abonoTotal);
                            abonoTotal = decimal.Zero;
                            break;
                        }
                    }
                }
            }

            //Obtener cuanto abona totalmente
            if (arrayListTransactionDetailId.Count > 0)
            {
                for (var key = 0; key < arrayListTransactionDetailId.Count; key++)
                {
                    var customerCreditDocumentID = arrayListCustomerCreditDocumentId[key];
                    var share = arrayListShare[key];
                    var transactionDetailID = arrayListTransactionDetailId[key];

                    var reference1Documento = arrayListTransactionDetailDocument[key];
                    var reference2Fecha = arrayListTransactionDetailFecha[key];
                    var reference3AmortizationID = arrayListCustomerCreditAmortizationId[key];

                    //Nuevo Detalle
                    if (transactionDetailID == 0)
                    {
                        var objCustomerCreditDocument = customerCreditDocumentModel.GetRowByPk(customerCreditDocumentID);
                        if (objCustomerCreditDocument is null)
                        {
                            throw new Exception("No eixste el objeto Customer Credit Document");
                        }

                        var objCustomerCreditLine = customerCreditLineModel.GetRowByPk(objCustomerCreditDocument.CustomerCreditLineId);
                        if (objCustomerCreditLine is null)
                        {
                            throw new Exception("No eixste el objeto Customer Credit Line");
                        }

                        var objCustomerAmortization = customerCreditAmortizationModel.GetRowByPk(reference3AmortizationID);
                        if (objCustomerAmortization is null)
                        {
                            throw new Exception("No eixste el objeto Customer Amortization");
                        }

                        //Verificar los dias de atraso
                        var hoy = DateTime.Today;
                        var cuotaDate = objCustomerAmortization.DateApply.Date;
                        var cuotaDif = hoy - cuotaDate;
                        var diferenciaDias = Math.Abs(cuotaDif.Days);

                        var objTmd = new TbTransactionMasterDetail
                        {
                            CompanyID = objTm.CompanyId,
                            TransactionID = objTm.TransactionId,
                            TransactionMasterID = TransactionMasterId.Value,
                            ComponentID = objcomponentShare.ComponentID,
                            ComponentItemID = customerCreditDocumentID,
                            Quantity = decimal.Zero,
                            UnitaryCost = decimal.Zero,
                            Cost = decimal.Zero,
                            UnitaryAmount = decimal.Zero,
                            UnitaryPrice = decimal.Zero,
                            Amount = share,
                            Discount = decimal.Zero,
                            PromotionID = 0,
                            Reference1 = reference1Documento,
                            Reference2 = Convert.ToInt32(typeAmortizationAmericanoId) == objCustomerCreditLine.TypeAmortization ? objCustomerCreditDocument.Balance!.Value.ToString("N2").Replace(",", "") : objCustomerCreditDocument.BalanceNew!.Value.ToString("N2").Replace(",", ""),
                            Reference3 = $"{reference3AmortizationID}",
                            Reference5 = objCustomerCreditDocument.DateOn.ToString("yyyy-MM-dd"),
                            Reference6 = objCustomerCreditDocument.DateFinish!.Value.ToString("yyyy-MM-dd"),
                            Reference7 = objCustomerAmortization.DateApply.ToString("yyyy-MM-dd"),
                            Lote = $"{diferenciaDias}",
                            CatalogStatusID = 0,
                            InventoryStatusID = 0,
                            IsActive = true,
                            QuantityStock = decimal.Zero,
                            QuantiryStockInTraffic = decimal.Zero,
                            QuantityStockUnaswared = decimal.Zero,
                            RemaingStock = decimal.Zero,
                            ExpirationDate = null,
                            InventoryWarehouseSourceID = objTm.SourceWarehouseId,
                            InventoryWarehouseTargetID = objTm.TargetWarehouseId
                        };
                        amount += share;
                        transactionMasterDetailModel.InsertAppPosme(objTmd);
                    }
                    else
                    {
                        //Editar Detalle
                        var objTmd = transactionMasterDetailModel.GetRowByPKK(transactionDetailID);
                        if (objTmd is null)
                        {
                            throw new Exception($"NO se encontró el detalle de la trasacción con el ID: {transactionDetailID}");
                        }

                        var objCustomerCreditDocument = customerCreditDocumentModel.GetRowByPk(objTmd.ComponentItemID!.Value);
                        if (objCustomerCreditDocument is null)
                        {
                            throw new Exception("No eixste el objeto Customer Credit Document");
                        }

                        var objCustomerCreditLine = customerCreditLineModel.GetRowByPk(objCustomerCreditDocument.CustomerCreditLineId);
                        if (objCustomerCreditLine is null)
                        {
                            throw new Exception("No eixste el objeto Customer Credit Line");
                        }

                        var objCustomerAmortization = customerCreditAmortizationModel.GetRowByPk(reference3AmortizationID);
                        if (objCustomerAmortization is null)
                        {
                            throw new Exception("No eixste el objeto Customer Amortization");
                        }

                        //Verificar los dias de atraso
                        var hoy = DateTime.Today;
                        var cuotaDate = objCustomerAmortization.DateApply.Date;
                        var cuotaDif = hoy - cuotaDate;
                        var diferenciaDias = Math.Abs(cuotaDif.Days);
                        var objTmdNew = objTmd;
                        objTmdNew.Amount = share;
                        objTmdNew.IsActive = true;
                        objTmdNew.Reference1 = reference1Documento;
                        objTmdNew.Reference2 = Convert.ToInt32(typeAmortizationAmericanoId) == objCustomerCreditLine.TypeAmortization ? objCustomerCreditDocument.Balance!.Value.ToString("N2") : objCustomerCreditDocument.BalanceNew!.Value.ToString("N2");
                        objTmdNew.Reference3 = $"{reference3AmortizationID}";
                        objTmdNew.Reference5 = objCustomerCreditDocument.DateOn.ToString("d");
                        objTmdNew.Reference6 = objCustomerCreditDocument.DateFinish!.Value.ToString("d");
                        objTmdNew.Reference7 = objCustomerCreditDocument.DateApply.ToString("d");
                        objTmdNew.Lote = $"{diferenciaDias}";
                        objTmdNew.ExchangeRateReference = objCustomerCreditDocument.ExchangeRate;
                        objTmdNew.DescriptionReference = "{componentID:\"Componente de transacciones de cuotas\",componentItemID:\"Id del documento de credito\",reference1:\"Numero del desembolso\",refernece2:\"balance anterior\",refernece3:\"Id de la amortizacion\",reference4:\"balance nuevo\",exchangeRateReference:\"Tasa de cambio del desembolso\",referece5:\"Fecha Inical de la deuda\",reference6:\"Fecha Final de la deuda\",reference7:\"dia que tocaba la cuota\",lote:\"Dias de atraso de la cuota\"}";
                        amount += share;
                        transactionMasterDetailModel.UpdateAppPosme(user.CompanyID, TransactionId.Value, TransactionMasterId.Value, transactionDetailID, objTmdNew);
                    }
                }
            }

            //Actualizar Transaccion
            objTmNew.Amount = amount;
            transactionMasterModel.UpdateAppPosme(user.CompanyID, TransactionId.Value, TransactionMasterId.Value, objTmNew);

            //Aplicar el Documento?
            var commandAplicable = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_APLICABLE"]);
            validateWorkflow = objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_share", "statusID", objTmNew.StatusID.Value, commandAplicable, user.CompanyID, user.BranchID, role.RoleID);
            if (validateWorkflow.HasValue && validateWorkflow.Value && objTm.StatusId.Value != objTmNew.StatusID)
            {
                //Recorrer Facturas para Actualizar Balances
                var objListTmd = transactionMasterDetailModel.GetRowByTransactionToShare(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
                if (objListTmd.Count > 0)
                {
                    foreach (var objTmd in objListTmd)
                    {
                        var objCustomerCreditDocumentInicial = customerCreditDocumentModel.GetRowByPk(objTmd.ComponentItemID!.Value);
                        objInterfazCoreWebAmortization.ApplyCuote(user.CompanyID, objTmd.ComponentItemID!.Value, Convert.ToDecimal(objTmd.Reference3), objTmd.TransactionMasterDetailID);
                        //documento final
                        var objCustomerCreditDocument = customerCreditDocumentModel.GetRowByPk(objTmd.ComponentItemID.Value);
                        //capital
                        var capital = (objCustomerCreditDocumentInicial!.Balance - objCustomerCreditDocument!.Balance) ?? decimal.Zero;
                        var objTMDC = new TbTransactionMasterDetailCredit
                        {
                            TransactionMasterID = objTmd.TransactionMasterID,
                            TransactionMasterDetailID = objTmd.TransactionMasterDetailID,
                            Capital = capital,
                            Interest = objTmd.Amount!.Value - capital,
                            DayDalay = 0,
                            InterestMora = 0,
                            CurrencyID = objTm.CurrencyId!.Value,
                            ExchangeRate = objTmNew.ExchangeRate!.Value,
                            Reference1 = null,
                            Reference2 = null,
                            Reference3 = null,
                            Reference4 = null,
                        };
                        transactionMasterDetailCreditModel.InsertAppPosme(objTMDC);

                        var objCustomer = customerModel.GetRowByEntity(user.CompanyID, objTmNew.EntityID!.Value);
                        var objTMFactura = transactionMasterModel.GetRowByTransactionNumber(user.CompanyID, objTmd.Reference1); /*invoiceNumber*/
                        var objCustomerCreditLine = customerCreditLineModel.GetRowByPk(Convert.ToInt32(objTMFactura.Reference4));
                        var objCustomerCredit = customerCreditModel.GetRowByPk(user.CompanyID, user.BranchID, objCustomer.EntityId);
                        var montoAbono = objTMDC.Capital;
                        var montoAbonoDolares = objTMFactura.ExchangeRate > 1 ? /*cordoba a dolares*/(objTMDC.Capital * Math.Round(1 / Math.Round(objTMFactura.ExchangeRate!.Value, 4), 4)) : objTMDC.Capital;
                        var montoAbonoCordobas = objTMFactura.ExchangeRate < 1 ? /*dolares a cordoba*/ (objTMDC.Capital / Math.Round(objTMFactura.ExchangeRate!.Value, 4)) : objTMDC.Capital;

                        // Actualizar saldo general del cliente
                        var objCustomerCreditNew = objCustomerCredit;
                        objCustomerCreditNew.BalanceDol = objCustomerCredit.BalanceDol + montoAbonoDolares;
                        customerCreditModel.UpdateAppPosme(user.CompanyID, objCustomer.BranchId, objCustomer.EntityId, objCustomerCreditNew);

                        // Actualizar saldo de la línea
                        // Línea dólares y factura dólares
                        // Línea córdoba y factura córdoba
                        var objCustomerCreditLineNew = objCustomerCreditLine;
                        if (objCustomerCreditLine.CurrencyID == objTMFactura.CurrencyID)
                        {
                            objCustomerCreditLineNew.Balance = objCustomerCreditLine.Balance + montoAbono;
                        }

                        // Línea en dólares, factura en córdoba
                        if (objCustomerCreditLine.CurrencyID == objCurrencyDolares.CurrencyID && objTMFactura.CurrencyID != objCurrencyDolares.CurrencyID)
                            objCustomerCreditLineNew.Balance = objCustomerCreditLine.Balance + montoAbonoDolares;

                        // Línea en córdoba, factura en dólares
                        if (objCustomerCreditLine.CurrencyID != objCurrencyDolares.CurrencyID && objTMFactura.CurrencyID == objCurrencyDolares.CurrencyID)
                            objCustomerCreditLineNew.Balance = objCustomerCreditLine.Balance + montoAbonoCordobas;

                        // Actualizar línea
                        customerCreditLineModel.UpdateAppPosme(objCustomerCreditLine.CustomerCreditLineID, objCustomerCreditLineNew);

                        // Actualizar saldo del recibo
                        var objTMDNew = transactionMasterDetailModel.GetRowByPKK(objTmd.TransactionMasterDetailID);
                        if (Convert.ToInt32(typeAmortizationAmericanoId) == objCustomerCreditLine.TypeAmortization)
                            objTMDNew.Reference4 = objCustomerCreditDocument.Balance.Value.ToString("N2");
                        else
                            objTMDNew.Reference4 = objCustomerCreditDocument.BalanceNew.Value.ToString("N2");

                        // Actualizar saldo del recibo
                        transactionMasterDetailModel.UpdateAppPosme(user.CompanyID, objTmd.TransactionID, objTmd.TransactionMasterID, objTmd.TransactionMasterDetailID, objTMDNew);
                    }
                }

                //Crear Conceptos.
                objInterfazCoreWebConcept.Share(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
            }

            dbTransaction.Commit();
        }
        catch (Exception ex)
        {
            dbTransaction.Rollback();
            throw ex;
        }
    }

    public void CommandNew(object? sender, EventArgs e)
    {
        Close();
        var frmShare = new FormShareEdit(TypeOpenForm.Init, 0, 0, 0)
        {
            MdiParent = CoreFormList.Principal()
        };
        frmShare.Show();
    }

    public void CommandSave(object? sender, EventArgs e)
    {
        if (FnValidateForm())
        {
            backgroundWorker = new BackgroundWorker();
            if (!progressPanel.Visible)
            {
                progressPanel.Width = Width;
                progressPanel.Height = Height;
                progressPanel.Visible = true;
            }

            backgroundWorker.DoWork += (ob, ev) =>
            {
                if (TransactionMasterId == 0)
                {
                    SaveInsert();
                }
                else
                {
                    SaveUpdate();
                }
            };
            backgroundWorker.RunWorkerCompleted += (ob, ev) =>
            {
                if (ev.Error is not null)
                {
                    objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Registrar", $"No se registraron los valores. {ev.Error.Message}", this);
                }
                else if (ev.Cancelled)
                {
                    //cancelado por el usuario   
                    objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Warning, "Registrar", "Se ha cancelado la operación actual. Linea 3424", this);
                }
                else
                {
                    objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Informacion, "Registrar", "Se han registrdo los datos de forma correcta", this);
                    if (TransactionMasterId > 0)
                    {
                        LoadEdit();
                        LoadRender(TypeRender.Edit);
                    }
                }

                if (progressPanel.Visible)
                {
                    progressPanel.Visible = false;
                }
            };

            if (!backgroundWorker.IsBusy)
            {
                backgroundWorker.RunWorkerAsync();
            }
        }
    }

    public void CommandRegresar(object? sender, EventArgs e)
    {
        Close();
    }

    public void PreRender()
    {
        HelperMethods.OnlyNumberDecimals(txtReceiptAmount);
    }


    public void LoadRender(TypeRender typeRedner)
    {
        var user = VariablesGlobales.Instance.User;
        switch (typeRedner)
        {
            case TypeRender.New:
                btnEliminar.Visible = false;
                btnImprmir.Visible = false;
                btnNuevo.Visible = false;
                layoutControlItemCobrador.ContentVisible = false;
                tabPageArchivos.PageVisible = false;
                lblTitulo.Text = @"ABONO #:00000000";
                txtCustomerID = 0;
                txtCustomerDescription.Text = string.Empty;
                txtDate.DateTime = DateTime.Now;
                txtIsApplied.Checked = false;
                txtExchangeRate.EditValue = ExchangeRate;
                CoreWebRenderInView.LlenarComboBox(ObjListWorkflowStage, txtStatusID, "WorkflowStageID", "Name", ObjListWorkflowStage.ElementAt(0).WorkflowStageID);
                CoreWebRenderInView.LlenarComboBox(ObjListCurrency, txtCurrencyID, "CurrencyId", "Name", ObjListCurrencyDefault.CurrencyID);
                break;
            case TypeRender.Edit:
                btnEliminar.Visible = true;
                btnImprmir.Visible = true;
                btnNuevo.Visible = true;
                layoutControlItemCobrador.ContentVisible = true;
                lblTitulo.Text = $"ABONO #: {ObjTransactionMaster.TransactionNumber}";
                txtDate.DateTime = ObjTransactionMaster.TransactionOn ?? DateTime.Now;
                txtIsApplied.Checked = ObjTransactionMaster.IsApplied.Value;
                txtExchangeRate.EditValue = ExchangeRate;
                txtCustomerID = ObjTransactionMaster.EntityId ?? 0;
                txtNote.Text = ObjTransactionMaster.Note;
                txtBalanceStart.EditValue = ObjTransactionMasterInfo!.Reference1 ?? string.Empty;
                txtBalanceFinish.EditValue = ObjTransactionMasterInfo.Reference2 ?? string.Empty;
                txtReferenceClientName.Text = ObjTransactionMasterInfo.ReferenceClientName;
                txtReferenceClientIdentifier.Text = ObjTransactionMasterInfo.ReferenceClientIdentifier;
                txtReference1.Text = ObjTransactionMaster.Reference1;
                txtReference2.Text = ObjTransactionMaster.Reference2;
                txtReceiptAmount.EditValue = ObjTransactionMasterInfo.ReceiptAmount;
                txtChangeAmount.EditValue = ObjTransactionMasterInfo.ChangeAmount;
                txtTotal.EditValue = ObjTransactionMaster.Amount;
                txtEmployeeID = string.IsNullOrWhiteSpace(ObjTransactionMaster.Reference3) ? 0 : Convert.ToInt32(ObjTransactionMaster.Reference3);
                txtCustomerDescription.Text = ObjNaturalDefault is not null ? $"{ObjCustomerDefault.CustomerNumber.ToUpper()} / {ObjNaturalDefault.FirstName} {ObjNaturalDefault.LastName}" : $"{ObjCustomerDefault.CustomerNumber}/{ObjLegalDefault.LegalName}";
                txtEmployeeDescription.Text = ObjEmployeeDefault is not null ? $"{ObjEmployeeDefault.EmployeNumber} / {ObjEmployeeNaturalDefault.FirstName}" : string.Empty;
                CoreWebRenderInView.LlenarComboBox(ObjListWorkflowStage, txtStatusID, "WorkflowStageID", "Name", ObjTransactionMaster.StatusId.Value);
                CoreWebRenderInView.LlenarComboBox(ObjListCurrency, txtCurrencyID, "CurrencyId", "Name", ObjTransactionMaster.CurrencyId.Value);
                bindingSourceDetailDto.Clear();
                if (ObjTransactionMasterDetail.Count > 0)
                {
                    foreach (var detailDto in ObjTransactionMasterDetail.Select(detail => new FormShareEditDetailDTO
                             {
                                 DetailCustomerCreditDocumentId = detail.ComponentItemID ?? 0,
                                 DetailTransactionDetailId = detail.TransactionMasterID,
                                 DetailTransactionDetailDocument = detail.Reference1 ?? string.Empty,
                                 DetailTransactionDetailFecha = null,
                                 DetailAmortizationId = Convert.ToInt32(detail.Reference3),
                                 DetailBalanceStart = Convert.ToDecimal(detail.Reference2),
                                 DetailBalanceFinish = Convert.ToDecimal(detail.Reference4),
                                 DetailShare = detail.Amount ?? decimal.Zero,
                                 DetailBalanceStartShare = decimal.Parse(detail.Reference2 ?? "0", CultureInfo.CurrentCulture),
                                 DetailBalanceFinishShare = decimal.Parse(detail.Reference4 ?? "0", CultureInfo.CurrentCulture)
                             }))
                    {
                        bindingSourceDetailDto.Add(detailDto);
                    }
                }

                gridControlArchivos.DataSource = null;
                gridControlArchivos.MainView = null;
                renderGridFiles = new RenderFileGridControl(user.CompanyID, ObjComponentTransactionShare!.ComponentID, TransactionMasterId.Value);
                renderGridFiles.RenderGridControl(gridControlArchivos);
                renderGridFiles.LoadFiles();
                ObjListCustomerCreditDocument = customerCreditDocumentModel.GetRowByEntityApplied(ObjTransactionMaster.CompanyId, txtCustomerID, ObjTransactionMaster.CurrencyId ?? 0);
                break;
        }
    }


    public void InitializeControl()
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Funciones

    private bool FnValidateForm()
    {
        if (txtDate.EditValue is null)
        {
            objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", "Establecer Fecha al Documento", this);
            return false;
        }

        if (txtCustomerID <= 0)
        {
            objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", "Seleccionar el Cliente", this);
            return false;
        }

        if (TransactionMasterId.HasValue && TransactionMasterId.Value > 0)
        {
            if (!string.IsNullOrWhiteSpace(ObjParameterShareInvoiceByInvoice))
            {
                if (ObjParameterShareInvoiceByInvoice.Equals("true", StringComparison.InvariantCultureIgnoreCase))
                {
                    var datos = bindingSourceDetailDto.List as IList<FormShareEditDetailDTO>;
                    foreach (var dato in datos)
                    {
                        var saldoInicial = dato.DetailBalanceStartShare;
                        var amountShare = dato.DetailShare;
                        if (amountShare > (saldoInicial + 2))
                        {
                            objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", "El monto del abono en la factura es mayor quel saldo", this);
                            return false;
                        }
                    }
                }
            }
        }


        return true;
    }

    private void FnOnCompleteNewCustomerPopPub(dynamic mensaje)
    {
        var diccionario = new Dictionary<string, string>();
        var objWebToolsHelper = new WebToolsHelper();
        diccionario.Add("companyID", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "companyID", "0"));
        diccionario.Add("entityID", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "entityID", "0"));
        diccionario.Add("Codigo", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Codigo", "0"));
        diccionario.Add("Nombre", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Nombre", "0"));
        diccionario.Add("Apellidos", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Apellidos", "0"));
        diccionario.Add("Comercial", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Comercial", "0"));
        FnOnCompleteNewCustomer(diccionario, true);
    }

    private void FnOnCompleteNewCustomer(Dictionary<string, string> diccionario, bool b)
    {
        Invoke(() =>
        {
            var companyId = Convert.ToInt32(diccionario["companyID"]);
            txtCustomerID = Convert.ToInt32(diccionario["entityID"]);
            txtCustomerDescription.Text = $@"{diccionario["Codigo"]} {diccionario["Nombre"]} / {diccionario["Comercial"]}";
            var selectedCurrency = txtCurrencyID.SelectedItem as ComboBoxItem;
            ObjListCustomerCreditDocument = selectedCurrency is not null ? formCxcApi.GetCustomerBalance(txtCustomerID, Convert.ToInt32(selectedCurrency)) : new();
            var saldoTotal = decimal.Zero;
            if (ObjListCustomerCreditDocument.Count > 0)
            {
                ObjListCustomerCreditDocument.ForEach(dto => saldoTotal = decimal.Add(saldoTotal, dto.Remaining ?? decimal.Zero));
            }

            txtBalanceStart.EditValue = saldoTotal;
        });
    }

    private void FnOnCompleteNewEmployeePopPub(dynamic mensaje)
    {
        var diccionario = new Dictionary<string, string>();
        var objWebToolsHelper = new WebToolsHelper();
        diccionario.Add("companyID", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "companyID", "0"));
        diccionario.Add("entityID", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "entityID", "0"));
        diccionario.Add("Codigo", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Codigo", "0"));
        diccionario.Add("Nombre", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Nombre", "0"));
        FnOnCompleteNewEmployee(diccionario, true);
    }

    private void FnOnCompleteNewEmployee(Dictionary<string, string> diccionario, bool b)
    {
        Invoke(() =>
        {
            txtEmployeeID = Convert.ToInt32(diccionario["entityID"]);
            txtEmployeeDescription.Text = $@"{diccionario["Codigo"]} / {diccionario["Nombre"]}";
        });
    }

    private void FnOnCompleteNewSharePopPub(dynamic mensaje)
    {
        var diccionario = new Dictionary<string, string>();
        var objWebToolsHelper = new WebToolsHelper();
        diccionario.Add("companyID", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "companyID", "0"));
        diccionario.Add("entityID", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "entityID", "0"));
        diccionario.Add("customerCreditDocumentID", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "customerCreditDocumentID", "0"));
        diccionario.Add("creditAmortizationID", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "creditAmortizationID", "0"));
        diccionario.Add("Documento", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Documento", "0"));
        diccionario.Add("Fecha", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Fecha", "0"));
        diccionario.Add("Cuota", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Cuota", "0"));
        diccionario.Add("Faltante", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Faltante", "0"));
        FnOnCompleteNewShare(diccionario);
    }

    private void FnOnCompleteNewShare(Dictionary<string, string> diccionario)
    {
        Invoke(() =>
        {
            var detailTransactionDetailDocument = diccionario["Documento"];
            var objBalancesDocument = ObjListCustomerCreditDocument
                .SingleOrDefault(obj => obj.DocumentNumber == detailTransactionDetailDocument);
            if (objBalancesDocument is null)
            {
                objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", $"No hay un documento con el numero de documento {detailTransactionDetailDocument}", this);
                return;
            }

            var dto = new FormShareEditDetailDTO
            {
                DetailCustomerCreditDocumentId = Convert.ToInt32(diccionario["customerCreditDocumentID"]),
                DetailTransactionDetailId = 0,
                DetailTransactionDetailDocument = detailTransactionDetailDocument,
                DetailTransactionDetailFecha = null,
                DetailAmortizationId = Convert.ToInt32(diccionario["creditAmortizationID"]),
                DetailBalanceStart = objBalancesDocument.Balance ?? decimal.Zero,
                DetailBalanceFinish = 0,
                DetailShare = Convert.ToDecimal(diccionario["Faltante"]),
                DetailBalanceStartShare = objBalancesDocument.Balance ?? decimal.Zero,
                DetailBalanceFinishShare = 0
            };
            bindingSourceDetailDto.Add(dto);
            UpdateSummary();
            UpdateCalculateChange();
        });
    }

    private void UpdateSummary()
    {
        var total = bindingSourceDetailDto.List.Cast<FormShareEditDetailDTO>().Sum(dto => dto.DetailShare);
        txtTotal.EditValue = total;
        var saldoFinal = decimal.Parse(txtBalanceStart.Text) - total;
        txtBalanceFinish.EditValue = saldoFinal;
    }

    private void UpdateCalculateChange()
    {
        if (!string.IsNullOrWhiteSpace(txtReceiptAmount.Text))
        {
            var total = string.IsNullOrWhiteSpace(txtTotal.Text) ? decimal.Zero : Convert.ToDecimal(txtTotal.EditValue);
            txtChangeAmount.EditValue = decimal.Subtract(total, Convert.ToDecimal(txtReceiptAmount.EditValue));
        }
    }

    private void ViewRegisterFormatoPaginaTicketInvoiceCancel()
    {
        var user = VariablesGlobales.Instance.User!;
        var role = VariablesGlobales.Instance.Role!;
        var transactionID = TransactionId!.Value;
        var transactionMasterID = TransactionMasterId!.Value;
        var companyID = user.CompanyID;

        //Get Logo
        var objParameterCompanyLogo = objInterfazCoreWebParameter.GetParameterValue("CORE_COMPANY_LOGO", companyID);
        var objParameterTelefono = objInterfazCoreWebParameter.GetParameterValue("CORE_PHONE", companyID);

        //Get Company
        var objCompany = companyModel.GetRowByPk(companyID);

        //Get Documento
        var objDetail = transactionMasterDetailModel.GetRowByShareId(companyID, transactionMasterID);
        var Identifier = objInterfazCoreWebParameter.GetParameterValue("CORE_COMPANY_IDENTIFIER", companyID);
        if (objDetail.Count < 0)
        {
            objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Informacion, "Imprimir", "No hay datos a imprimir", this);
            return;
        }

        // Imprimir el documento               
        var printerName = objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_PRINTER_DIRECT_NAME_DEFAULT", companyID);
        var pathOfLogo = VariablesGlobales.ConfigurationBuilder["PATH_FILE_OF_APP_ROOT"];
        var printer = new Printer(printerName!.Value);

        printer.AlignCenter();
        if (objParameterCompanyLogo is not null)
        {
            objParameterCompanyLogo = "direct-ticket-" + objParameterCompanyLogo;
            var imagePath = $"{pathOfLogo}/img/logos/{objParameterCompanyLogo!}";
            if (File.Exists(imagePath))
            {
                var logoCompany = new Bitmap(Image.FromFile(imagePath));
                //el logo que se esta mostrando no se redimensiona 
                printer.Image(logoCompany);
            }
        }

        printer.AlignCenter();
        printer.Append(objCompany.Name.ToUpper());
        printer.Append("DETALLE");
        printer.AlignLeft();
        printer.Append($"Código       {objDetail.ElementAt(0).CustomerNumber}");
        printer.Append($"Cliente      {objDetail.ElementAt(0).FirstName}");
        printer.NewLine();
        foreach (var detail in objDetail)
        {
            printer.Append($"{detail.CreatedOn}");
            printer.Append($"{detail.ItemName}");
            printer.Append($"{detail.Quantity}");
            printer.Append($"{detail.UnitaryPrice}");
            printer.Append($"{detail.Amount}");
            printer.Append($"{detail.Quantity * detail.UnitaryPrice}");
        }

        printer.NewLine();
        printer.AlignCenter();
        printer.Append(objCompany.Address);
        printer.Append($"Tel.: {objParameterTelefono!}");
        printer.FullPaperCut();
        printer.PrintDocument();
    }

    #endregion

    #region Eventos

    private void BtnImprmir_Click(object? sender, EventArgs e)
    {
        ComandPrinter();
    }

    private void BtnEliminarOnClick(object? sender, EventArgs e)
    {
        if (XtraMessageBox.Show("", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
        {
            return;
        }

        backgroundWorker = new BackgroundWorker();
        if (!progressPanel.Visible)
        {
            progressPanel.Width = Width;
            progressPanel.Height = Height;
            progressPanel.Visible = true;
        }

        backgroundWorker.DoWork += (ob, ev) => { ComandDelete(); };
        backgroundWorker.RunWorkerCompleted += (ob, ev) =>
        {
            if (ev.Error is not null)
            {
                objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", ev.Error.Message, this);
            }
            else if (ev.Cancelled)
            {
                //se canceló por el usuario
                objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Warning, "Error", "Operación cancelada por el usuario", this);
            }
            else
            {
                objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Informacion, "Eliminar", "Se ha eliminado el registro de forma correcta", this);
                Close();

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

        if (!backgroundWorker.IsBusy)
        {
            backgroundWorker.RunWorkerAsync();
        }
    }

    private void FormShareEdit_Resize(object sender, EventArgs e)
    {
        var thirdWidth = ClientSize.Width / 3;
        layoutControl5.Width = thirdWidth;
        layoutControl6.Width = thirdWidth;
        layoutControl7.Width = thirdWidth;
    }

    private void btnSearchCustomer_Click(object sender, EventArgs e)
    {
        var selectedCurrency = txtCurrencyID.SelectedItem as ComboBoxItem;
        if (selectedCurrency is null)
        {
            objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", "Seleccione la moneda", this);
            return;
        }

        var formTypeListSearch = new FormTypeListSearch("Lista de Clientes", ObjComponentCustomer.ComponentID,
            "SELECCIONAR_CLIENTES_BILLING", true, @"", false, "", 0, 5, "", true);
        formTypeListSearch.EventoCallBackAceptarEvent += EventoCallBackAceptarCustomer;
        formTypeListSearch.ShowDialog(this);
    }

    private void EventoCallBackAceptarCustomer(dynamic mensaje)
    {
        FnOnCompleteNewCustomerPopPub(mensaje);
    }

    private void btnNewShare_Click(object sender, EventArgs e)
    {
        if (txtCustomerID <= 0)
        {
            objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Cliente", "Seleccione el cliente para continuar", this);
            return;
        }

        var selectedCurrency = txtCurrencyID.SelectedItem as ComboBoxItem;
        if (selectedCurrency is null)
        {
            objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Moneda", "Seleccione la moneda para continuar", this);
            return;
        }

        var formTypeListSearch = new FormTypeListSearch("Lista de Clientes", ObjComponentCustomerCreditDocument.ComponentID,
            "SELECCIONAR_DOCUMENTOS_DE_CREDITO", true, @"{entityID:" + txtCustomerID + ",currencyID:" + selectedCurrency.Key + "}", false, "", 0, 5, "", true);
        formTypeListSearch.EventoCallBackAceptarEvent += EventoCallBackAceptarNewShare;
        formTypeListSearch.ShowDialog(this);
    }

    private void EventoCallBackAceptarNewShare(dynamic mensaje)
    {
        FnOnCompleteNewSharePopPub(mensaje);
    }

    private void txtReceiptAmount_EditValueChanged(object sender, EventArgs e)
    {
        UpdateCalculateChange();
    }

    private void btnVerMovimientos_Click(object sender, EventArgs e)
    {
        if (txtCustomerID <= 0)
        {
            objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Seleccione el cliente para ver los movimientos", "Cliente", this);
            return;
        }

        var dialogMovimientos = new FormShareEditVerMovimientos("Ver Movimientos");
        var pathUrl = VariablesGlobales.ConfigurationBuilder["APP_URL_RESOURCE_CSS_JS"];
        dialogMovimientos.webView.Source = new Uri($"{pathUrl}/app_cxc_report/movement_customer/viewReport/true/customerNumber/{txtCustomerID}");
        dialogMovimientos.ShowDialog();
    }

    private void btnDeleteShare_Click(object sender, EventArgs e)
    {
        if (gridViewTransactionMasterDetail.RowCount < 0)
        {
            objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "No hay datos en la tabla", "Cliente", this);
            return;
        }

        if (gridViewTransactionMasterDetail.FocusedRowHandle < 0)
        {
            objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "No ha seleccionado un valor a eliminar", "Cliente", this);
            return;
        }

        bindingSourceDetailDto.RemoveAt(gridViewTransactionMasterDetail.FocusedRowHandle);
    }

    private void gridViewTransactionMasterDetail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
    {
        if (e.Column.FieldName == colDetailShare.FieldName)
        {
            UpdateSummary();
            UpdateCalculateChange();
        }
    }

    private void gridViewTransactionMasterDetail_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
    {
        if (e.Column.FieldName == colDetailShare.FieldName)
        {
            UpdateSummary();
            UpdateCalculateChange();
        }
    }

    private void btnClearCustomer_Click(object sender, EventArgs e)
    {
        txtCustomerID = 0;
        txtCustomerDescription.Clear();
        txtBalanceStart.Clear();
        txtBalanceFinish.Clear();
    }

    private void btnClearEmployee_Click(object sender, EventArgs e)
    {
        txtEmployeeID = 0;
        txtEmployeeDescription.Clear();
    }

    private void btnSearchEmployee_Click(object sender, EventArgs e)
    {
        if (ObjComponentEmployee is null)
        {
            objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Empleado", "No se ha configurado el comonente employee", this);
            return;
        }

        var formTypeListSearch = new FormTypeListSearch("Lista de Empleados", ObjComponentEmployee.ComponentID,
            "SELECCIONAR_EMPLOYEE_COLLECTOR", true, @"", false, "", 0, 5, "", true);
        formTypeListSearch.EventoCallBackAceptarEvent += EventoCallBackAceptarEmployee;
        formTypeListSearch.ShowDialog(this);
    }

    private void EventoCallBackAceptarEmployee(dynamic mensaje)
    {
        FnOnCompleteNewEmployeePopPub(mensaje);
    }

    private void btnAgregarArchivo_Click(object sender, EventArgs e)
    {
        var openFileDialog = new XtraOpenFileDialog();
        openFileDialog.Title = @"Seleccionar archivo";
        var dialogResult = openFileDialog.ShowDialog(this);
        if (dialogResult == DialogResult.OK)
        {
            var file = openFileDialog.SafeFileName;
            renderGridFiles.AddRow(file);
        }
    }

    #endregion
}