using System.ComponentModel;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Libraries.CustomModels;
using v4posme_window.Api;
using v4posme_window.Interfaz;
using v4posme_window.Libraries;
using v4posme_window.Template;
using Unity;
using v4posme_library.Libraries.CustomHelper;
using v4posme_library.Models;
using v4posme_window.Dto;
using System.IO;
using DevExpress.XtraEditors;
using ESC_POS_USB_NET.Printer;
using v4posme_library.ModelsDto;

namespace v4posme_window.Views.Box.ShareCapital;

public partial class FormShareCapitalEdit : FormTypeHeadEdit, IFormTypeEdit
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

    public TbLegal? ObjLegalDefault { get; set; }

    public TbNaturale? ObjNaturalDefault { get; set; }

    public TbCustomerDto? ObjCustomerDefault { get; set; }

    public TbComponent? ObjComponentTransactionShareCapital { get; set; }

    public List<TbTransactionMasterDetail> ObjTransactionMasterDetail { get; set; }

    public TbTransactionMasterInfoDto? ObjTransactionMasterInfo { get; set; }

    public TbTransactionMasterDto? ObjTransactionMaster { get; set; }

    public string? UrlPrinterDocument { get; set; }

    public List<TbWorkflowStage>? ObjListWorkflowStage { get; set; }

    public List<TbTransactionCausal>? ObjCaudal { get; set; }

    public decimal ExchangeRate { get; set; }

    public TbComponent? ObjComponentCustomerCreditDocument { get; set; }

    public TbComponent? ObjComponentCustomer { get; set; }

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

    public FormShareCapitalEdit()
    {
        InitializeComponent();
    }

    public FormShareCapitalEdit(TypeOpenForm typeOpen, int companyId, int transactionMasterId, int transactionId)
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

    private void FormShareCapitalEdit_Load(object sender, EventArgs e)
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
            var permited = objInterfazCoreWebPermission.UrlPermited("app_box_sharecapital", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (!permited)
            {
                throw new Exception(notAccessControl);
            }

            resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd("app_box_sharecapital", "delete", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
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
        var validateWorkflowStage = objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_share_capital", "statusID", objTm.StatusId.Value, commandEliminable, user.CompanyID, user.BranchID, role.RoleID);
        if (validateWorkflowStage.HasValue && validateWorkflowStage.Value)
        {
            throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_WORKFLOW_DELETE"]);
        }

        transactionMasterModel.DeleteAppPosme(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
        transactionMasterDetailModel.DeleteWhereTm(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
    }

    public void ComandPrinter()
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
        var company = VariablesGlobales.Instance.Company;
        var resultPermission = 0;
        if (appNeedAuthentication == "true")
        {
            var permited = objInterfazCoreWebPermission.UrlPermited("app_box_sharecapital", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (!permited)
            {
                throw new Exception(notAccessControl);
            }

            resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd("app_box_sharecapital", "delete", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (resultPermission == permissionNone)
            {
                throw new Exception(notAllEdit);
            }
        }

        //Get Component
        var objComponent = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_company");
        //Get Logo
        var objParameterLogo = objInterfazCoreWebParameter.GetParameterValue("CORE_COMPANY_LOGO", user.CompanyID);
        //Get Company
        var objCompany = companyModel.GetRowByPk(user.CompanyID);
        var objParameterTelefono = objInterfazCoreWebParameter.GetParameterValue("CORE_PHONE", user.CompanyID);
        var accountingExchangeSale = Convert.ToDecimal(objInterfazCoreWebParameter.GetParameterValue("ACCOUNTING_EXCHANGE_SALE", user.CompanyID));

        //Get Documento	
        var objTM = transactionMasterModel.GetRowByPk(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
        var objTMD = transactionMasterDetailModel.GetRowByTransactionToShare(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
        var objTMI = transactionMasterInfoModel.GetRowByPk(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
        var objUser = userModel.GetRowByPk(objTM.CompanyId, objTM.CreatedAt.Value, objTM.CreatedBy.Value);
        var objBranch = branchModel.GetRowByPk(objTM.CompanyId,objTM.BranchId.Value);
        var objCustumer = customerModel.GetRowByEntity(user.CompanyID, objTM.EntityId.Value);
        var objNatural = naturalModel.GetRowByPk(user.CompanyID, objCustumer.BranchId, objCustumer.EntityId);
        var tipoCambio = Math.Round(objTM.ExchangeRate.Value + accountingExchangeSale, 2);
        var objCurrency = currencyModel.GetRowByPk(objTM.CurrencyId.Value);
        var objStage = objInterfazCoreWebWorkflow.GetWorkflowStage("tb_transaction_master_share_capital", "statusID", objTM.StatusId.Value, user.CompanyID, user.BranchID, role.RoleID);

        // Formatear la fecha de la transacción
        var transactionDate = objTM.TransactionOn;
        var formattedTransactionDate = transactionDate!.Value.ToString("yyyy-M-d HH:mm:ss");

        try
        {
            // Imprimir el documento               
            var printerName = objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_PRINTER_DIRECT_NAME_DEFAULT", user.CompanyID);
            var pathOfLogo = VariablesGlobales.ConfigurationBuilder["PATH_FILE_OF_APP_ROOT"];
            var printer = new Printer(printerName!.Value);

            printer.AlignCenter();
            if (objParameterLogo is not null)
            {
                objParameterLogo = "direct-ticket-" + objParameterLogo;
                var imagePath = $"{pathOfLogo}/img/logos/{objParameterLogo!}";
                if (File.Exists(imagePath))
                {
                    var logoCompany = new Bitmap(Image.FromFile(imagePath));
                    //el logo que se esta mostrando no se redimensiona 
                    //printer.Image(logoCompany);
                }
            }

            printer.AlignCenter();
            printer.Append(objCompany.Name);
            printer.BoldMode("ABONO AL CAPITAL");
            printer.Append($"# {objTM.TransactionNumber}");
            printer.Append($"FECHA: {formattedTransactionDate} ");
            printer.Separator();
            printer.AlignLeft();
            var datos = $"""
                         ESTADO:   {objStage.First().Name}
                         MONEDA:   {objCurrency.Simbol}
                         """;
            printer.Append(datos);
            printer.Append($"TOTAL                                 {objTM.Amount}");
            printer.AlignCenter();
            printer.Append(objCompany.Address);
            printer.Append($"Tel.: {objParameterTelefono}");
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
        var company = VariablesGlobales.Instance.Company;

        if (appNeedAuthentication == "true")
        {
            var permited = objInterfazCoreWebPermission.UrlPermited("app_box_sharecapital", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (!permited)
            {
                throw new Exception(notAccessControl);
            }

            var resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd("app_box_sharecapital", "edit", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (resultPermission == permissionNone)
            {
                throw new Exception(notAllEdit);
            }
        }

        if (TransactionId is 0 && TransactionMasterId is 0)
        {
            Close();
            var frm = new FormShareCapitalEdit(TypeOpenForm.Init, 0, 0, 0)
            {
                MdiParent = CoreFormList.Principal()
            };
            frm.Show();
            return;
        }

        ObjComponentCustomer = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_customer");
        if (ObjComponentCustomer is null)
        {
            throw new Exception("EL COMPONENTE 'tb_customer' NO EXISTE...");
        }

        ObjComponentTransactionShareCapital = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_transaction_master_share_capital");
        if (ObjComponentTransactionShareCapital is null)
        {
            throw new Exception("EL COMPONENTE 'tb_transaction_master_share_capital' NO EXISTE...");
        }

        ObjComponentCustomerCreditDocument = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_customer_credit_document");
        if (ObjComponentCustomerCreditDocument is null)
        {
            throw new Exception("EL COMPONENTE 'tb_customer_credit_document' NO EXISTE...");
        }

        var objCurrency = objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyID);
        var targetCurrency = objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyID);
        UrlPrinterDocument = objInterfazCoreWebParameter.GetParameterValue("BOX_SHARECAPITAL_URL_PRINTER", user.CompanyID);
        ObjTransactionMaster = transactionMasterModel.GetRowByPk(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
        ObjTransactionMasterInfo = transactionMasterInfoModel.GetRowByPk(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
        ObjTransactionMasterDetail = transactionMasterDetailModel.GetRowByTransactionToShare(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
        ObjListWorkflowStage = objInterfazCoreWebWorkflow.GetWorkflowStageByStageInit("tb_transaction_master_share_capital", "statusID", ObjTransactionMaster.StatusId.Value, user.CompanyID, user.BranchID, role.RoleID);
        ObjCustomerDefault = customerModel.GetRowByEntity(user.CompanyID, ObjTransactionMaster.EntityId.Value);
        ObjNaturalDefault = naturalModel.GetRowByPk(user.CompanyID, role.RoleID, ObjCustomerDefault.EntityId);
        ObjLegalDefault = legalModel.GetRowByPk(user.CompanyID, user.BranchID, ObjCustomerDefault.EntityId);
        ExchangeRate = objInterfazCoreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, decimal.One, targetCurrency.CurrencyID, objCurrency.CurrencyID);
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
            var permited = objInterfazCoreWebPermission.UrlPermited("app_box_sharecapital", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (!permited)
            {
                throw new Exception(notAccessControl);
            }

            var resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd("app_box_sharecapital", "add", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
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
        if (ObjComponentCustomer is null)
        {
            throw new Exception("EL COMPONENTE 'tb_customer_credit_document' NO EXISTE...");
        }

        var transactionID = objInterfazCoreWebTransaction.GetTransactionId(user.CompanyID, "tb_transaction_master_share_capital", 0);
        var objCurrency = objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyID);
        var targetCurrency = objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyID);

        ExchangeRate = objInterfazCoreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, decimal.One, targetCurrency.CurrencyID, objCurrency.CurrencyID);
        ObjCaudal = transactionCausalModel.GetCausalByBranch(user.CompanyID, transactionID.Value, user.BranchID);
        ObjListWorkflowStage = objInterfazCoreWebWorkflow.GetWorkflowInitStage("tb_transaction_master_share_capital", "statusID", user.CompanyID, user.BranchID, role.RoleID);
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
            var permited = objInterfazCoreWebPermission.UrlPermited("app_box_sharecapital", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (!permited)
            {
                throw new Exception(notAccessControl);
            }

            var resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd("app_box_sharecapital", "add", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (resultPermission == permissionNone)
            {
                throw new Exception(notAllInsert);
            }
        }

        objInterfazCoreWebPermission.GetValueLicense(user.CompanyID, "app_box_sharecapital/index");

        //Obtener el Componente de Transacciones Facturacion
        var objComponentShareCapital = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_transaction_master_share_capital");
        if (objComponentShareCapital is null)
        {
            throw new Exception("EL COMPONENTE 'tb_transaction_master_share_capital' NO EXISTE...");
        }

        var objComponentCreditDocument = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_customer_credit_document");
        if (objComponentCreditDocument is null)
        {
            throw new Exception("EL COMPONENTE 'tb_customer_credit_document' NO EXISTE...");
        }

        var objComponentCustomer = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_customer");
        if (objComponentCustomer is null)
        {
            throw new Exception("EL COMPONENTE 'tb_customer' NO EXISTE...");
        }

        if (objInterfazCoreWebAccounting.CycleIsCloseByDate(user.CompanyID, txtDate.DateTime.Date))
        {
            throw new Exception("EL DOCUMENTO NO PUEDE INGRESAR, EL CICLO CONTABLE ESTA CERRADO");
        }

        var currencyId = objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyID).CurrencyID;
        var currencyId2 = objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyID).CurrencyID;
        TransactionId = objInterfazCoreWebTransaction.GetTransactionId(user.CompanyID, "tb_transaction_master_share_capital", 0) ?? 0;
        var objT = transactionModel.GetByCompanyAndTransaction(user.CompanyID, TransactionId.Value);
        var objTM = new TbTransactionMaster
        {
            CompanyID = user.CompanyID,
            TransactionID = TransactionId.Value,
            BranchID = user.BranchID,
            TransactionNumber = objInterfazCoreWebCounter.GoNextNumber(user.CompanyID, user.BranchID, "tb_transaction_master_share_capital", 0),
            TransactionCausalID = objInterfazCoreWebTransaction.GetDefaultCausalId(user.CompanyID, TransactionId.Value),
            EntityID = txtCustomerID,
            TransactionOn = txtDate.DateTime,
            StatusIDChangeOn = DateTime.Now,
            ComponentID = objComponentShareCapital.ComponentID,
            Note = txtNote.Text,
            Sign = (short?)objT.SignInventory.Value,
            CurrencyID = currencyId,
            CurrencyID2 = currencyId2,
            ExchangeRate = objInterfazCoreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, decimal.One, currencyId2, currencyId),
            Reference1 = txtReference1.Text,
            Reference2 = txtReference2.Text,
            Reference3 = txtReference3.Text,
            Reference4 = string.Empty,
            StatusID = Convert.ToInt32((txtStatusID.EditValue as ComboBoxItem).Key),
            Amount = Convert.ToDecimal(txtTotal.EditValue),
            IsApplied = false,
            JournalEntryID = 0,
            ClassID = null,
            AreaID = null,
            SourceWarehouseID = null,
            TargetWarehouseID = null,
            IsActive = true
        };
        objInterfazCoreWebAuditoria.SetAuditCreated(objTM, user, "");

        TransactionMasterId = transactionMasterModel.InsertAppPosme(objTM);

        var pathFileOfApp = VariablesGlobales.ConfigurationBuilder["objComponentShareCapital"];
        var path = $"{pathFileOfApp}/company_{user.CompanyID}/component_{objComponentShareCapital.ComponentID}/component_item_{TransactionMasterId}";
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
            ReceiptAmount = Convert.ToDecimal(txtReceiptAmount.EditValue)
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
                    ComponentID = objComponentCreditDocument.ComponentID,
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
                    Reference2 = null,
                    Reference3 = null,
                    CatalogStatusID = 0,
                    InventoryStatusID = 0,
                    IsActive = true,
                    QuantityStock = decimal.Zero,
                    QuantiryStockInTraffic = decimal.Zero,
                    QuantityStockUnaswared = decimal.Zero,
                    RemaingStock = decimal.Zero,
                    ExpirationDate = null,
                    InventoryWarehouseSourceID = objTM.SourceWarehouseID,
                    InventoryWarehouseTargetID = objTM.TargetWarehouseID,
                };
                amount = decimal.Add(amount, objTmd.Amount.Value);
                transactionMasterDetailModel.InsertAppPosme(objTmd);
            }
        }

        transactionMasterModel.UpdateAppPosme(user.CompanyID, TransactionId.Value, TransactionMasterId.Value, objTM);
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
            var permited = objInterfazCoreWebPermission.UrlPermited("app_box_sharecapital", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (!permited)
            {
                throw new Exception(notAccessControl);
            }

            resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd("app_box_sharecapital", "edit", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (resultPermission == permissionNone)
            {
                throw new Exception(notAllEdit);
            }
        }

        //Obtener el Componente de Transacciones Facturacion
        var objComponentShareCapital = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_transaction_master_share_capital");
        if (objComponentShareCapital is null)
        {
            throw new Exception("EL COMPONENTE 'tb_transaction_master_share_capital' NO EXISTE...");
        }

        var objComponentCreditDocument = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_customer_credit_document");
        if (objComponentCreditDocument is null)
        {
            throw new Exception("EL COMPONENTE 'tb_customer_credit_document' NO EXISTE...");
        }

        var objComponentCustomer = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_customer");
        if (objComponentCustomer is null)
        {
            throw new Exception("EL COMPONENTE 'tb_customer' NO EXISTE...");
        }

        var typeAmortizationAmericanoID = objInterfazCoreWebParameter.GetParameterValue("CXC_AMERICANO", user.CompanyID);
        var objTm = transactionMasterModel.GetRowByPk(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
        var oldStatusID = objTm.StatusId;

        var objCurrencyDolares = objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyID);
        var objCurrencyCordoba = objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyID);
        ExchangeRate = objInterfazCoreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, decimal.One, objCurrencyDolares.CurrencyID, objCurrencyCordoba.CurrencyID);

        var permissionMe = VariablesGlobales.ConfigurationBuilder["PERMISSION_ME"];
        if (resultPermission == Convert.ToInt32(permissionMe) && objTm.CreatedBy != user.UserID)
        {
            throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_EDIT"]);
        }

        var commandEditableTotal = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_EDITABLE_TOTAL"]);
        var validateWorkflow = objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_share_capital", "statusID", objTm.StatusId!.Value, commandEditableTotal, user.CompanyID, user.BranchID, role.RoleID);
        if (validateWorkflow.HasValue && !validateWorkflow.Value)
        {
            throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_WORKFLOW_EDIT"]);
        }

        if (objInterfazCoreWebAccounting.CycleIsCloseByDate(user.CompanyID, txtDate.DateTime.Date))
        {
            throw new Exception("EL DOCUMENTO NO PUEDE INGRESAR, EL CICLO CONTABLE ESTA CERRADO");
        }

        var selectedStatus = txtStatusID.SelectedItem as ComboBoxItem;
        //Actualizar Maestro
        var objTMNew = transactionMasterModel.GetRowByPKK(objTm.TransactionMasterId);
        objTMNew.EntityID = txtCustomerID;
        objTMNew.TransactionOn = txtDate.DateTime;
        objTMNew.StatusIDChangeOn = DateTime.Now;
        objTMNew.Note = txtNote.Text;
        objTMNew.ExchangeRate = objInterfazCoreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, decimal.One, objTm.CurrencyId2.Value, objTm.CurrencyId.Value);
        ;
        objTMNew.Reference1 = txtReference1.Text;
        objTMNew.Reference2 = txtReference2.Text;
        objTMNew.Reference3 = txtReference3.Text;
        objTMNew.Reference4 = "";
        objTMNew.StatusID = Convert.ToInt32(selectedStatus.Key);
        objTMNew.Amount = decimal.Zero;

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

        var commandEditable = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_EDITABLE"]);
        validateWorkflow = objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_share_capital", "statusID", objTm.StatusId!.Value, commandEditable, user.CompanyID, user.BranchID, role.RoleID);
        if (validateWorkflow.HasValue && validateWorkflow.Value)
        {
            var tbTransactionMaster = transactionMasterModel.GetRowByPKK(TransactionMasterId.Value);
            tbTransactionMaster.StatusID = Convert.ToInt32(selectedStatus.Key);
            transactionMasterModel.UpdateAppPosme(user.CompanyID, objTm.TransactionId, objTm.TransactionMasterId, tbTransactionMaster);
        }
        else
        {
            transactionMasterModel.UpdateAppPosme(user.CompanyID, objTm.TransactionId, objTm.TransactionMasterId, objTMNew);
            transactionMasterInfoModel.UpdateAppPosme(user.CompanyID, objTm.TransactionId, objTm.TransactionMasterId, objTMInfoNew);
        }

        //Actualizar Detalle
        var formShareEditDetailDtos = bindingSourceDetailDto.List as IList<FormShareEditDetailDTO>;
        var arrayListTransactionDetailId = formShareEditDetailDtos.Select(dto => dto.DetailTransactionDetailId).ToList();
        var amount = decimal.Zero;

        transactionMasterDetailModel.DeleteWhereIdNotIn(user.CompanyID, objTm.TransactionId, objTm.TransactionMasterId, arrayListTransactionDetailId);

        if (formShareEditDetailDtos.Count > 0)
        {
            foreach (var detailDto in formShareEditDetailDtos)
            {
                var customerCreditDocumentID = detailDto.DetailCustomerCreditDocumentId;
                var share = detailDto.DetailShare;
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

                if (detailDto.DetailTransactionDetailId == 0)
                {
                    var objTmd = new TbTransactionMasterDetail
                    {
                        CompanyID = objTm.CompanyId,
                        TransactionID = objTm.TransactionId,
                        TransactionMasterID = TransactionMasterId.Value,
                        ComponentID = objComponentCreditDocument.ComponentID,
                        ComponentItemID = customerCreditDocumentID,
                        Quantity = decimal.Zero,
                        UnitaryCost = decimal.Zero,
                        Cost = decimal.Zero,
                        UnitaryPrice = decimal.Zero,
                        UnitaryAmount = decimal.Zero,
                        Amount = share,
                        Discount = decimal.Zero,
                        PromotionID = 0,
                        Reference1 = objCustomerCreditDocument.DocumentNumber,
                        Reference2 = Convert.ToInt32(typeAmortizationAmericanoID) == objCustomerCreditLine.TypeAmortization ? objCustomerCreditDocument.Balance!.Value.ToString("F") : objCustomerCreditDocument.BalanceNew!.Value.ToString("F"),
                        Reference3 = null,
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
                    var objTMDNew = transactionMasterDetailModel.GetRowByPKK(detailDto.DetailTransactionDetailId);
                    objTMDNew.Amount = share;
                    objTMDNew.Reference1 = objCustomerCreditDocument.DocumentNumber;
                    objTMDNew.Reference2 = Convert.ToInt32(typeAmortizationAmericanoID) == objCustomerCreditLine.TypeAmortization ? objCustomerCreditDocument.Balance!.Value.ToString("F") : objCustomerCreditDocument.BalanceNew!.Value.ToString("F");
                    objTMDNew.Reference3 = null;
                    objTMDNew.ExchangeRateReference = objCustomerCreditDocument.ExchangeRate;
                    objTMDNew.DescriptionReference = "{componentID:Componente de los documentos de credito,componentItemID:Id del documento de credito,reference1:Numero del desembolso,refernece2:balance anterior,reference4:balance nuevo,exchangeRateReference:Tasa de cambio del desembolso}";
                    amount += share;
                    transactionMasterDetailModel.UpdateAppPosme(user.CompanyID, TransactionId.Value, TransactionMasterId.Value, detailDto.DetailTransactionDetailId, objTMDNew);
                }
            }
        }

        //Actualizar Transaccion
        objTMNew.Amount = amount;
        transactionMasterModel.UpdateAppPosme(user.CompanyID, TransactionId.Value, TransactionMasterId.Value, objTMNew);

        //Aplicar el Documento?
        var commandAplicable = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_APLICABLE"]);
        validateWorkflow = objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_share_capital", "statusID", objTMNew.StatusID.Value, commandAplicable, user.CompanyID, user.BranchID, role.RoleID);
        if (validateWorkflow.HasValue && validateWorkflow.Value && objTm.StatusId.Value != objTMNew.StatusID)
        {
            //Recorrer Facturas
            var objListTmd = transactionMasterDetailModel.GetRowByTransactionToShare(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
            if (objListTmd.Count > 0)
            {
                foreach (var objTmd in objListTmd)
                {
                    //documento inicial
                    var objCustomerCreditDocumentInicial = customerCreditDocumentModel.GetRowByPk(objTmd.ComponentItemID.Value);
                    objInterfazCoreWebAmortization.ShareCapital(user.CompanyID, objTmd.ComponentItemID.Value, objTmd.Amount.Value);

                    //documento final
                    var objCustomerCreditDocument = customerCreditDocumentModel.GetRowByPk(objTmd.ComponentItemID.Value);

                    //Actualizar detlle de credito
                    var capital = objCustomerCreditDocumentInicial.Balance.Value - objCustomerCreditDocument.Balance.Value;
                    var objTMDC = new TbTransactionMasterDetailCredit
                    {
                        TransactionMasterID = objTmd.TransactionMasterID,
                        TransactionMasterDetailID = objTmd.TransactionMasterDetailID,
                        Capital = capital,
                        Interest = objTmd.Amount.Value - capital,
                        DayDalay = 0,
                        InterestMora = 0,
                        CurrencyID = objTm.CurrencyId.Value,
                        ExchangeRate = objTMNew.ExchangeRate.Value,
                        Reference1 = null,
                        Reference2 = null,
                        Reference3 = null,
                        Reference4 = null,
                    };
                    transactionMasterDetailCreditModel.InsertAppPosme(objTMDC);

                    var objCustomer = customerModel.GetRowByEntity(user.CompanyID, objTMNew.EntityID.Value);
                    var objTMFactura = transactionMasterModel.GetRowByTransactionNumber(user.CompanyID, objCustomerCreditDocument.DocumentNumber);
                    var objCustomerCreditLine = customerCreditLineModel.GetRowByPk(objCustomerCreditDocument.CustomerCreditLineId);
                    var objCustomerCredit = customerCreditModel.GetRowByPk(user.CompanyID, user.BranchID, objCustomer.EntityId);
                    var montoAbono = objTMDC.Capital;
                    var montoAbonoDolares = objTMFactura.CurrencyID == 2 ? /*cordoba a dolares*/ objTMDC.Capital : (objTMDC.Capital * Math.Round(objTMFactura.ExchangeRate!.Value, 4));
                    var montoAbonoCordobas = objTMFactura.CurrencyID == 1 ? /*dolares a cordoba*/ objTMDC.Capital : (objTMDC.Capital * Math.Round(objTMFactura.ExchangeRate!.Value, 4));

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
                    if (Convert.ToInt32(typeAmortizationAmericanoID) == objCustomerCreditLine.TypeAmortization)
                        objTMDNew.Reference4 = objCustomerCreditDocument.Balance.Value.ToString("F");
                    else
                        objTMDNew.Reference4 = objCustomerCreditDocument.BalanceNew.Value.ToString("F");

                    // Actualizar saldo del recibo
                    transactionMasterDetailModel.UpdateAppPosme(user.CompanyID, objTmd.TransactionID, objTmd.TransactionMasterID, objTmd.TransactionMasterDetailID, objTMDNew);
                }
            }

            //Crear Conceptos.
            objInterfazCoreWebConcept.Share(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
        }
    }

    public void CommandNew(object? sender, EventArgs e)
    {
        Close();
        var frm = new FormShareCapitalEdit(TypeOpenForm.Init, 0, 0, 0)
        {
            MdiParent = CoreFormList.Principal()
        };
        frm.Show();
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
                tabPageArchivos.PageVisible = false;
                lblTitulo.Text = @"ABONO AL CAPITAL #:00000000";
                txtCustomerID = 0;
                txtCustomerDescription.Text = string.Empty;
                txtDate.DateTime = DateTime.Now;
                txtIsApplied.Checked = false;
                txtExchangeRate.EditValue = ExchangeRate;
                CoreWebRenderInView.LlenarComboBox(ObjListWorkflowStage, txtStatusID, "WorkflowStageID", "Name", ObjListWorkflowStage.ElementAt(0).WorkflowStageID);
                break;
            case TypeRender.Edit:
                btnEliminar.Visible = true;
                btnImprmir.Visible = true;
                btnNuevo.Visible = true;
                tabPageArchivos.PageVisible = true;
                lblTitulo.Text = @$"ABONO AL CAPITAL #:{ObjTransactionMaster.TransactionNumber}";
                txtCustomerID = ObjTransactionMaster.EntityId.Value;
                txtCustomerDescription.Text = ObjNaturalDefault != null ? $"{ObjCustomerDefault.CustomerNumber.ToUpper()} {ObjNaturalDefault.FirstName} {ObjNaturalDefault.LastName}" : $"{ObjCustomerDefault.CustomerNumber} {ObjLegalDefault.LegalName}";
                txtDate.DateTime = ObjTransactionMaster.TransactionOn ?? DateTime.Now;
                txtIsApplied.Checked = ObjTransactionMaster.IsApplied ?? false;
                txtExchangeRate.EditValue = ExchangeRate;
                txtReferenceClientName.Text = ObjTransactionMasterInfo.ReferenceClientName;
                txtReferenceClientIdentifier.Text = ObjTransactionMasterInfo.ReferenceClientIdentifier;
                txtReference1.Text = ObjTransactionMaster.Reference1;
                txtReference2.Text = ObjTransactionMaster.Reference2;
                txtReference3.Text = ObjTransactionMaster.Reference3;
                txtNote.Text = ObjTransactionMaster.Note;
                if (ObjTransactionMasterDetail.Count>0)
                {
                    bindingSourceDetailDto.Clear();
                    foreach (var detail in ObjTransactionMasterDetail)
                    {
                        bindingSourceDetailDto.Add(new FormShareEditDetailDTO
                        {
                            DetailCustomerCreditDocumentId = detail.ComponentItemID.Value,
                            DetailTransactionDetailId = detail.TransactionMasterDetailID,
                            DetailTransactionDetailDocument = detail.Reference1,
                            DetailTransactionDetailFecha = null,
                            DetailAmortizationId = 0,
                            DetailBalanceStart = 0,
                            DetailBalanceFinish = 0,
                            DetailShare = detail.Amount ?? decimal.Zero,
                            BalanceStartShare = 0,
                            BalanceFinishShare = 0
                        });
                    }
                }

                txtReceiptAmount.EditValue = ObjTransactionMasterInfo.ReceiptAmount ?? decimal.Zero;
                txtChangeAmount.EditValue = ObjTransactionMasterInfo.ChangeAmount;
                txtTotal.EditValue = ObjTransactionMaster.Amount ?? decimal.Zero;
                CoreWebRenderInView.LlenarComboBox(ObjListWorkflowStage, txtStatusID, "WorkflowStageID", "Name", ObjTransactionMaster.StatusId);
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
            txtCustomerID = Convert.ToInt32(diccionario["entityID"]);
            txtCustomerDescription.Text = $@"{diccionario["Codigo"]} {diccionario["Nombre"]} / {diccionario["Comercial"]}";
        });
    }

    private void FnOnCompleteNewSharePopPub(dynamic mensaje)
    {
        var diccionario = new Dictionary<string, string>();
        var objWebToolsHelper = new WebToolsHelper();
        diccionario.Add("customerCreditDocumentID", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "customerCreditDocumentID", "0"));
        diccionario.Add("Documento", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Documento", "0"));
        diccionario.Add("Fecha", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Fecha", "0"));
        diccionario.Add("Saldo", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Saldo", "0"));
        diccionario.Add("Moneda", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Moneda", ""));
        FnOnCompleteNewShare(diccionario);
    }

    private void FnOnCompleteNewShare(Dictionary<string, string> diccionario)
    {
        Invoke(() =>
        {
            var detailTransactionDetailDocument = diccionario["Documento"];
            var detailCustomerCreditDocumentId = Convert.ToInt32(diccionario["customerCreditDocumentID"]);
            var formShareEditDetailDtos = bindingSourceDetailDto.Cast<FormShareEditDetailDTO>().ToList();
            if (formShareEditDetailDtos.Count > 0)
            {
                var exist = formShareEditDetailDtos.Any(detailDto => detailDto.DetailCustomerCreditDocumentId.Equals(detailCustomerCreditDocumentId));
                if (exist)
                {
                    objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", $"El Documento ya esta agregado", this);
                    return;
                }
            }

            var dto = new FormShareEditDetailDTO
            {
                DetailCustomerCreditDocumentId = detailCustomerCreditDocumentId,
                DetailTransactionDetailId = 0,
                DetailTransactionDetailDocument = detailTransactionDetailDocument,
                DetailTransactionDetailFecha = DateTime.Parse(diccionario["Fecha"]),
                DetailAmortizationId = 0,
                DetailBalanceStart = decimal.Zero,
                DetailBalanceFinish = 0,
                DetailShare = Convert.ToDecimal(diccionario["Saldo"]),
                BalanceStartShare = decimal.Zero,
                BalanceFinishShare = decimal.Zero
            };
            bindingSourceDetailDto.Add(dto);
            UpdateSummary();
            UpdateCalculateChange();
        });
    }

    private void UpdateCalculateChange()
    {
        var total = (decimal)txtTotal.EditValue;
        var amount = (decimal)txtReceiptAmount.EditValue;
        txtChangeAmount.EditValue = decimal.Subtract(total, amount);
    }

    private void UpdateSummary()
    {
        var total = bindingSourceDetailDto.Cast<FormShareEditDetailDTO>().ToList().Sum(dto => dto.DetailShare);
        txtTotal.EditValue = total;
    }

    #endregion

    #region Eventos

    private void BtnImprmir_Click(object? sender, EventArgs e)
    {
        ComandPrinter();
    }

    private void BtnEliminarOnClick(object? sender, EventArgs e)
    {
        if (XtraMessageBox.Show("Eliminar", "¿Seguro desea eliminar el abono seleccionado? Esta acción no se puede revertir.", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
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

    private void FormShareCapitalEdit_Resize(object sender, EventArgs e)
    {
        var thirdWidth = ClientSize.Width / 3;
        layoutControl5.Width = thirdWidth;
        layoutControl6.Width = thirdWidth;
        layoutControl7.Width = thirdWidth;
    }

    private void btnClearCustomer_Click(object sender, EventArgs e)
    {
        txtCustomerID = 0;
        txtCustomerDescription.Clear();
    }

    private void btnSearchCustomer_Click(object sender, EventArgs e)
    {
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


        var formTypeListSearch = new FormTypeListSearch("Lista de Clientes", ObjComponentCustomerCreditDocument.ComponentID,
            "SELECCIONAR_DOCUMENTOS_DE_CREDITO_SUMMARY", true, @"{entityID:" + txtCustomerID + "}", false, "", 0, 5, "", true);
        formTypeListSearch.EventoCallBackAceptarEvent += EventoCallBackAceptarNewShare;
        formTypeListSearch.ShowDialog(this);
    }

    private void EventoCallBackAceptarNewShare(dynamic mensaje)
    {
        FnOnCompleteNewSharePopPub(mensaje);
    }

    private void btnDeleteShare_Click(object sender, EventArgs e)
    {
        var rowHandle = gridViewMasterDetail.FocusedRowHandle;
        if (rowHandle >= 0)
        {
            gridViewMasterDetail.DeleteRow(rowHandle);
            UpdateSummary();
            UpdateCalculateChange();
        }
    }

    private void gridViewMasterDetail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
    {
        if (e.Column.FieldName == colDetailShare.FieldName)
        {
            UpdateSummary();
            UpdateCalculateChange();
        }
    }

    private void txtReceiptAmount_EditValueChanged(object sender, EventArgs e)
    {
        UpdateCalculateChange();
    }

    #endregion
}