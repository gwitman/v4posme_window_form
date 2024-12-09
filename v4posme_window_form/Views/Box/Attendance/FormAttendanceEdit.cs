using System.ComponentModel;
using System.IO;
using DevExpress.XtraEditors;
using ESC_POS_USB_NET.Printer;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_window.Interfaz;
using v4posme_window.Libraries;
using v4posme_window.Template;
using Unity;
using v4posme_library.Libraries.CustomHelper;
using v4posme_library.ModelsDto;
using v4posme_library.Models;
using v4posme_window.Api;

namespace v4posme_window.Views.Box.Attendance
{
    public partial class FormAttendanceEdit : FormTypeHeadEdit, IFormTypeEdit
    {
        #region Campos

        private TypeOpenForm TypeOpen { get; set; }
        private int? TransactionMasterId = 0;
        private int? TransactionId = 0;
        private int txtCustomerID = 0;
        private RenderFileGridControl renderGridFiles;
        private System.Windows.Forms.Timer timerHuella;

        #endregion

        #region Modelos

        public TbTransactionMasterDto? ObjTransactionMaster { get; set; }

        public TbComponent? ObjComponentShare { get; set; }

        public List<TbCatalogItem>? ObjListPrioridad { get; set; }

        public TbLegal? ObjLegalDefault { get; set; }

        public TbNaturale? ObjNaturalDefault { get; set; }

        public TbCustomer? ObjCustomerDefaultNew { get; set; }

        public TbCustomerDto? ObjCustomerDefaultEdit { get; set; }

        public TbComponent? ObjComponentCustomer { get; set; }

        public List<TbWorkflowStage>? ObjListWorkflowStage { get; set; }

        public List<TbTransactionCausal>? ObjCaudal { get; set; }

        public decimal ExchangeRateSale { get; set; }

        public decimal ExchangeRatePurchase { get; set; }

        public string? ObjParameterAttendanceAutoPrinter { get; set; }

        public decimal ExchangeRate { get; set; }

        public List<TbCompanyCurrencyDto>? ObjListCurrency { get; set; }

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
        private readonly ICoreWebCatalog objInterfazCoreWebCatalog = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCatalog>();
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
        private readonly FormInvoiceApi formInvoiceApi = new();
        private readonly FormFingerprintApi formFingerprintApi = new();

        #endregion

        #region Init

        public FormAttendanceEdit()
        {
            InitializeComponent();
        }

        public FormAttendanceEdit(TypeOpenForm typeOpen, int companyId, int transactionMasterId, int transactionId)
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
            InicializarTimer();
        }

        public void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            CustomException.LogException(e.Exception);
        }

        public void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            CustomException.LogException((Exception)e.ExceptionObject);
        }

        private void FormAttendanceEdit_Load(object sender, EventArgs e)
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
                var permited = objInterfazCoreWebPermission.UrlPermited("app_box_attendance", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(notAccessControl);
                }

                resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd("app_box_attendance", "delete", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    throw new Exception(notAllEdit);
                }
            }

            if (TransactionId.Value == 0 && TransactionMasterId.Value == 0)
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
            var validateWorkflowStage = objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_attendance", "statusID", objTm.StatusId.Value, commandEliminable, user.CompanyID, user.BranchID, role.RoleID);
            if (validateWorkflowStage.HasValue && validateWorkflowStage.Value)
            {
                throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_WORKFLOW_DELETE"]);
            }

            transactionMasterModel.DeleteAppPosme(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
            transactionMasterDetailModel.DeleteWhereTm(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
        }

        public void ComandPrinter()
        {
            var roleId = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["APP_ROL_SUPERADMIN"]);
            var companyId = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["APP_COMPANY"]);
            var ACCOUNTING_EXCHANGE_SALE = Convert.ToDecimal(objInterfazCoreWebParameter.GetParameterValue("ACCOUNTING_EXCHANGE_SALE", companyId));
            var objComponent = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_company");
            var objParameterCompanyLogo = objInterfazCoreWebParameter.GetParameterValue("CORE_COMPANY_LOGO", companyId);
            var objParameterTelefono = objInterfazCoreWebParameter.GetParameterValue("CORE_PHONE", companyId);
            var objParameterRuc = objInterfazCoreWebParameter.GetParameterValue("CORE_COMPANY_IDENTIFIER", companyId);
            var objCompany = companyModel.GetRowByPk(companyId);
            var objTM = transactionMasterModel.GetRowByPk(companyId, TransactionId.Value, TransactionMasterId.Value);
            var objTC = transactionCausalModel.GetByCompanyAndTransactionAndCausal(companyId, TransactionId.Value, objTM.TransactionCausalId.Value);
            var objUser = userModel.GetRowByPk(objTM.CompanyId, objTM.BranchId.Value, objTM.CreatedBy.Value);
            var Identifier = objInterfazCoreWebParameter.GetParameterValue("CORE_COMPANY_IDENTIFIER", companyId);
            var objBranch = branchModel.GetRowByPk(objTM.CompanyId, objTM.BranchId.Value);
            var objStage = objInterfazCoreWebWorkflow.GetWorkflowStage("tb_transaction_master_attendance", "statusID", objTM.StatusId.Value, companyId, objTM.BranchId.Value, roleId);
            var objTipo = transactionCausalModel.GetByCompanyAndTransactionAndCausal(companyId, TransactionId.Value, objTM.TransactionCausalId.Value);
            var objCustumer = customerModel.GetRowByEntity(companyId, objTM.EntityId.Value);
            var objCurrency = currencyModel.GetRowByPk(objTM.CurrencyId.Value);
            var objNatural = naturalModel.GetRowByPk(companyId, objCustumer.BranchId, objCustumer.EntityId);
            var tipoCambio = Math.Round(objTM.ExchangeRate.Value + ACCOUNTING_EXCHANGE_SALE, 2);

            // Formatear la fecha de la transacción
            var transactionDate = objTM.TransactionOn;
            var formattedTransactionDate = transactionDate!.Value.ToString("yyyy-M-d HH:mm:ss");

            try
            {
                // Imprimir el documento               
                var printerName = objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_PRINTER_DIRECT_NAME_DEFAULT", companyId);
                var pathOfLogo = VariablesGlobales.ConfigurationBuilder["PATH_FILE_OF_APP_ROOT"];
                var printer = new Printer(printerName!.Value);
                var userNickName = objUser.Nickname;
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
                printer.Append(objCompany.Name);
                if (userNickName is not null)
                {
                    printer.Append($"{objParameterRuc}");
                }

                var nickname = userNickName.Contains("@") ? userNickName.Substring(0, userNickName.IndexOf("@")) : userNickName;
                printer.BoldMode("ASISTENCIA");
                printer.Append($"# {objTM.TransactionNumber}");
                printer.Append($"FECHA: {formattedTransactionDate} ");
                printer.Separator();
                printer.AlignLeft();
                if (objUser.Nickname is not null)
                {
                    printer.Append($"Vendedor:        {nickname}");
                }

                printer.Append($"Codigo:          {objCustumer.CustomerNumber}");
                printer.Append($"Cliente:         {objNatural.FirstName}");
                printer.Append($"Solvencia:       {objTM.Reference1}");
                printer.Append($"Proximo Pago:    {objTM.Reference2}");
                printer.Append($"Dias Pro. Pago:  {objTM.Reference4}");
                printer.Append($"Vencimiento:     {objTM.Reference3}");
                var estado = objStage.ElementAt(0).Display == "CANCELADA" ? "APLICADA" : objStage.ElementAt(0).Display;
                printer.Append($"Estado:          {estado}");
                printer.NewLine();
                printer.AlignCenter();
                printer.Append(objCompany.Address);
                printer.Append($"Tel.: {objParameterTelefono}");
                printer.Append("Sistema 505-8712-5827");
                printer.FullPaperCut();
                printer.PrintDocument();
            }
            catch (Exception ex)
            {
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
                var permited = objInterfazCoreWebPermission.UrlPermited("app_box_attendance", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(notAccessControl);
                }

                var resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd("app_box_attendance", "add", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    throw new Exception(notAllEdit);
                }
            }

            if (TransactionId is 0 && TransactionMasterId is 0)
            {
                Close();
                var frm = new FormAttendanceEdit(TypeOpenForm.Init, 0, 0, 0)
                {
                    MdiParent = CoreFormList.Principal()
                };
                frm.Show();
                return;
            }

            //Componente de facturacion
            ObjComponentShare = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_transaction_master_attendance");
            if (ObjComponentShare is null)
            {
                throw new Exception("EL COMPONENTE 'tb_transaction_master_attendance' NO EXISTE...");
            }

            var objCurrency = objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyID);
            var targetCurrency = objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyID);
            ObjListCurrency = companyCurrencyModel.GetByCompany(user.CompanyID);

            ObjComponentCustomer = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_customer");
            if (ObjComponentCustomer is null)
            {
                throw new Exception("EL COMPONENTE 'tb_customer' NO EXISTE...");
            }

            //Tipo de Factura
            var customerDefault = objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_CLIENTDEFAULT", user.CompanyID);
            ObjTransactionMaster = transactionMasterModel.GetRowByPk(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
            ObjParameterAttendanceAutoPrinter = objInterfazCoreWebParameter.GetParameterValue("ATTENDANCE_AUTO_PRINTER", user.CompanyID);
            ExchangeRate = objInterfazCoreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, decimal.One, targetCurrency.CurrencyID, objCurrency.CurrencyID);
            ObjListWorkflowStage = objInterfazCoreWebWorkflow.GetWorkflowStageByStageInit("tb_transaction_master_attendance", "statusID", ObjTransactionMaster.StatusId.Value, user.CompanyID, user.BranchID, role.RoleID);
            ObjCustomerDefaultEdit = customerModel.GetRowByEntity(user.CompanyID, ObjTransactionMaster.EntityId.Value);
            ObjNaturalDefault = naturalModel.GetRowByPk(user.CompanyID, ObjCustomerDefaultEdit.BranchId, ObjCustomerDefaultEdit.EntityId);
            ObjLegalDefault = legalModel.GetRowByPk(user.CompanyID, ObjCustomerDefaultEdit.BranchId, ObjCustomerDefaultEdit.EntityId);
            ObjListPrioridad = objInterfazCoreWebCatalog.GetCatalogAllItem("tb_transaction_master_attendance", "priorityID", user.CompanyID);
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
                var permited = objInterfazCoreWebPermission.UrlPermited("app_box_attendance", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(notAccessControl);
                }

                var resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd("app_box_attendance", "add", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
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

            var transactionID = objInterfazCoreWebTransaction.GetTransactionId(user.CompanyID, "tb_transaction_master_attendance", 0) ?? 0;
            var objCurrency = objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyID);
            var targetCurrency = objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyID);
            var customerDefault = objInterfazCoreWebParameter.GetParameterValue("INVOICE_BILLING_CLIENTDEFAULT", user.CompanyID);
            ObjListCurrency = companyCurrencyModel.GetByCompany(user.CompanyID);
            ExchangeRate = objInterfazCoreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, decimal.One, targetCurrency.CurrencyID, objCurrency.CurrencyID);
            ObjParameterAttendanceAutoPrinter = objInterfazCoreWebParameter.GetParameterValue("ATTENDANCE_AUTO_PRINTER", user.CompanyID);
            var objParameterExchangePurchase = objInterfazCoreWebParameter.GetParameterValue("ACCOUNTING_EXCHANGE_PURCHASE", user.CompanyID);
            ExchangeRatePurchase = ExchangeRate - Convert.ToDecimal(objParameterExchangePurchase);
            var objParameterExchangeSales = objInterfazCoreWebParameter.GetParameterValue("ACCOUNTING_EXCHANGE_PURCHASE", user.CompanyID);
            ExchangeRateSale = ExchangeRate + Convert.ToDecimal(objParameterExchangeSales);
            ObjCaudal = transactionCausalModel.GetCausalByBranch(user.CompanyID, transactionID, user.BranchID);
            ObjListWorkflowStage = objInterfazCoreWebWorkflow.GetWorkflowInitStage("tb_transaction_master_attendance", "statusID", user.CompanyID, user.BranchID, role.RoleID);
            ObjCustomerDefaultNew = customerModel.GetRowByCode(user.CompanyID, customerDefault);
            ObjNaturalDefault = naturalModel.GetRowByPk(user.CompanyID, ObjCustomerDefaultNew.BranchID, ObjCustomerDefaultNew.EntityID);
            ObjLegalDefault = legalModel.GetRowByPk(user.CompanyID, ObjCustomerDefaultNew.BranchID, ObjCustomerDefaultNew.EntityID);
            ObjListPrioridad = objInterfazCoreWebCatalog.GetCatalogAllItem("tb_transaction_master_attendance", "priorityID", user.CompanyID);
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
                var permited = objInterfazCoreWebPermission.UrlPermited("app_box_attendance", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(notAccessControl);
                }

                var resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd("app_box_attendance", "add", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    throw new Exception(notAllInsert);
                }
            }

            objInterfazCoreWebPermission.GetValueLicense(user.CompanyID, "app_box_attendance/index");

            //Obtener el Componente de Transacciones Facturacion
            ObjComponentShare = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_transaction_master_attendance");
            if (ObjComponentShare is null)
            {
                throw new Exception("EL COMPONENTE 'tb_transaction_master_attendance' NO EXISTE...");
            }

            if (objInterfazCoreWebAccounting.CycleIsCloseByDate(user.CompanyID, txtDate.DateTime.Date))
            {
                throw new Exception("EL DOCUMENTO NO PUEDE INGRESAR, EL CICLO CONTABLE ESTA CERRADO");
            }

            ObjParameterAttendanceAutoPrinter = objInterfazCoreWebParameter.GetParameterValue("ATTENDANCE_AUTO_PRINTER", user.CompanyID);
            TransactionId = objInterfazCoreWebTransaction.GetTransactionId(user.CompanyID, "tb_transaction_master_attendance", 0);
            var objT = transactionModel.GetByCompanyAndTransaction(user.CompanyID, TransactionId.Value);
            var currencyId = objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyID).CurrencyID;
            var currencyId2 = objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyID).CurrencyID;
            var objTM = new TbTransactionMaster
            {
                CompanyID = user.CompanyID,
                TransactionID = TransactionId.Value,
                BranchID = user.BranchID,
                TransactionNumber = objInterfazCoreWebCounter.GoNextNumber(user.CompanyID, user.BranchID, "tb_transaction_master_attendance", 0),
                TransactionCausalID = objInterfazCoreWebTransaction.GetDefaultCausalId(user.CompanyID, TransactionId.Value),
                EntityID = txtCustomerID,
                TransactionOn = txtDate.DateTime,
                StatusIDChangeOn = DateTime.Now,
                ComponentID = ObjComponentShare.ComponentID,
                Note = string.Empty,
                Sign = 0,
                CurrencyID = currencyId,
                CurrencyID2 = currencyId2,
                ExchangeRate = objInterfazCoreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, decimal.One, currencyId2, currencyId),
                Reference1 = txtDetailReference1.Text,
                Reference2 = txtDetailReference2.DateTime.ToString("yyyy-MM-dd"),
                Reference3 = txtDetailReference3.DateTime.ToString("yyyy-MM-dd"),
                Reference4 = txtDetailReference4.Text,
                StatusID = Convert.ToInt32((txtStatusID.SelectedItem as ComboBoxItem).Key),
                PriorityID = Convert.ToInt32((txtPriorityID.SelectedItem as ComboBoxItem).Key),
                Amount = decimal.Zero,
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

            //Crear la Carpeta para almacenar los Archivos del Documento
            var pathFileOfApp = VariablesGlobales.ConfigurationBuilder["PATH_FILE_OF_APP"];
            var path = $"{pathFileOfApp}/company_{user.CompanyID}/component_{ObjComponentShare.ComponentID}/component_item_{TransactionMasterId}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (ObjParameterAttendanceAutoPrinter.Equals("true", StringComparison.InvariantCultureIgnoreCase))
            {
                ComandPrinter();
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
                var permited = objInterfazCoreWebPermission.UrlPermited("app_box_attendance", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(notAccessControl);
                }

                resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd("app_box_attendance", "edit", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    throw new Exception(notAllEdit);
                }
            }

            ObjComponentShare = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_transaction_master_attendance");
            if (ObjComponentShare is null)
            {
                throw new Exception("EL COMPONENTE 'tb_transaction_master_attendance' NO EXISTE...");
            }

            var objTM = transactionMasterModel.GetRowByPk(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
            var oldStatusID = objTM.StatusId;
            var objCurrencyDolares = objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyID);
            var objCurrencyCordoba = objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyID);
            ExchangeRate = objInterfazCoreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, decimal.One, objCurrencyDolares.CurrencyID, objCurrencyCordoba.CurrencyID);
            var PERMISSION_ME = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_ME"]);
            if (resultPermission == PERMISSION_ME && (objTM.CreatedBy != user.UserID))
            {
                throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_EDIT"]);
            }

            var COMMAND_EDITABLE_TOTAL = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_EDITABLE_TOTAL"]);
            if (!objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_attendance", "statusID", objTM.StatusId.Value, COMMAND_EDITABLE_TOTAL, user.CompanyID, user.BranchID, role.RoleID).Value)
            {
                throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_WORKFLOW_EDIT"]);
            }

            if (objInterfazCoreWebAccounting.CycleIsCloseByDate(user.CompanyID, objTM.TransactionOn.Value))
            {
                throw new Exception("EL DOCUMENTO NO PUEDE ACTUALIZARCE, EL CICLO CONTABLE ESTA CERRADO");
            }

            var objTMNew = transactionMasterModel.GetRowByPKK(TransactionMasterId.Value);
            objTMNew.TransactionOn = txtDate.DateTime;
            objTMNew.StatusIDChangeOn = DateTime.Now;
            objTMNew.EntityID = txtCustomerID;
            objTMNew.Reference1 = txtDetailReference1.Text;
            objTMNew.Reference2 = txtDetailReference2.DateTime.ToString("yyyy-MM-dd");
            objTMNew.Reference3 = txtDetailReference3.DateTime.ToString("yyyy-MM-dd");
            objTMNew.Reference4 = txtDetailReference4.Text;
            objTMNew.StatusID = Convert.ToInt32((txtStatusID.SelectedItem as ComboBoxItem).Key);
            objTMNew.PriorityID = Convert.ToInt32((txtPriorityID.SelectedItem as ComboBoxItem).Key);

            var commandEditable = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_EDITABLE"]);
            var validateWorkflow = objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_attendance", "statusID", objTMNew.StatusID.Value, commandEditable, user.CompanyID, user.BranchID, role.RoleID);
            if (validateWorkflow.HasValue && validateWorkflow.Value)
            {
                var tbTransactionMaster = transactionMasterModel.GetRowByPKK(TransactionMasterId.Value);
                tbTransactionMaster.StatusID = Convert.ToInt32((txtPriorityID.SelectedItem as ComboBoxItem).Key);
                transactionMasterModel.UpdateAppPosme(user.CompanyID, objTM.TransactionId, objTM.TransactionMasterId, tbTransactionMaster);
            }
            else
            {
                transactionMasterModel.UpdateAppPosme(user.CompanyID, TransactionId.Value, TransactionMasterId.Value, objTMNew);
            }
        }

        public void CommandNew(object? sender, EventArgs e)
        {
            Close();
            var frm = new FormAttendanceEdit(TypeOpenForm.Init, 0, 0, 0)
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
                    lblTitulo.Text = @"ASISTENCIA: 00000000";
                    txtDate.DateTime = DateTime.Now;
                    txtCustomerID = ObjCustomerDefaultNew.EntityID;
                    txtCustomerDescription.Text = ObjNaturalDefault is not null ? $"{ObjCustomerDefaultNew.CustomerNumber} {ObjNaturalDefault.FirstName.ToUpper()} {ObjNaturalDefault.LastName.ToUpper()}" : $"{ObjCustomerDefaultNew.CustomerNumber} {ObjLegalDefault.ComercialName.ToUpper()}";
                    CoreWebRenderInView.LlenarComboBox(ObjListPrioridad, txtPriorityID, "CatalogItemID", "Display", ObjListPrioridad.ElementAt(0).CatalogItemID);
                    CoreWebRenderInView.LlenarComboBox(ObjListWorkflowStage, txtStatusID, "WorkflowStageID", "Name", ObjListWorkflowStage.ElementAt(0).WorkflowStageID);
                    FnActiveSensorRead();
                    FnHuellaLeida();
                    break;
                case TypeRender.Edit:
                    btnEliminar.Visible = true;
                    btnImprmir.Visible = true;
                    btnNuevo.Visible = true;
                    layoutControlItemHuella.ContentVisible = false;
                    lblTitulo.Text = @$"ASISTENCIA: {ObjTransactionMaster.TransactionNumber}";
                    txtDate.DateTime = ObjTransactionMaster.TransactionOn ?? DateTime.Now;
                    txtCustomerID = ObjCustomerDefaultEdit.EntityId;
                    txtCustomerDescription.Text = ObjNaturalDefault is not null ? $"{ObjCustomerDefaultEdit.CustomerNumber} {ObjNaturalDefault.FirstName.ToUpper()} {ObjNaturalDefault.LastName.ToUpper()}" : $"{ObjCustomerDefaultEdit.CustomerNumber} {ObjLegalDefault.ComercialName.ToUpper()}";
                    CoreWebRenderInView.LlenarComboBox(ObjListPrioridad, txtPriorityID, "CatalogItemID", "Display", ObjTransactionMaster.PriorityId);
                    CoreWebRenderInView.LlenarComboBox(ObjListWorkflowStage, txtStatusID, "WorkflowStageID", "Name", ObjTransactionMaster.StatusId);
                    txtDetailReference1.Text = ObjTransactionMaster.Reference1;
                    txtDetailReference2.DateTime = Convert.ToDateTime(ObjTransactionMaster.Reference2);
                    txtDetailReference4.Text = ObjTransactionMaster.Reference4;
                    txtDetailReference3.DateTime = Convert.ToDateTime(ObjTransactionMaster.Reference3);
                    break;
            }
        }


        public void InitializeControl()
        {
        }

        #endregion

        #region Funciones

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
                FnCompleteGetCustomerCreditLine();
            });
        }

        private void FnCompleteGetCustomerCreditLine()
        {
            if (TransactionId.Value > 0 && TransactionMasterId.Value > 0)
            {
                FnCompleteGetCustomerCreditLineEdit();
            }
            else
            {
                FnCompleteGetCustomerCreditLineNew();
            }
        }

        private void FnCompleteGetCustomerCreditLineNew()
        {
            var getData = formInvoiceApi.GetLineByCustomer(txtCustomerID);
            if (getData is null)
            {
                txtCustomerID = 0;
                txtCustomerDescription.Text = @"Cliente no tiene membresía";
                objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", "Cliente sin membresia", this);
                return;
            }

            var data = getData.ObjCustomerCreditAmoritizationAll;
            if (data is null || data.Count <= 0)
            {
                txtCustomerID = 0;
                txtCustomerDescription.Text = @"Cliente no tiene membresía";
                objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", "Cliente sin membresia", this);
                return;
            }

            var cantidadMoraList = data.Where(dto => dto.StageDocumento != "CANCELADO").ToList();
            cantidadMoraList = cantidadMoraList.Where(dto => dto.Mora > 0).ToList();
            int cantidadMora;
            if (cantidadMoraList.Count == 0)
            {
                cantidadMora = 0;
            }
            else
            {
                cantidadMora = cantidadMoraList.Max(dto => dto.Mora) ?? 0;
            }

            switch (cantidadMora)
            {
                case > 0:
                    objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", "Cliente con mora", this);
                    txtDetailReference1.Text = @"NO";
                    txtDetailReference4.Text = @"0";
                    break;
                case <= 0:
                    txtDetailReference1.Text = @"SI";
                    break;
            }

            //Fecha de Vencimiento
            var fechaVencimiento = data.Where(dto => dto.StageDocumento != "CANCELADO").ToList();
            var fechaVencimientoMora = fechaVencimiento.Min(dto => dto.Mora);
            fechaVencimiento = fechaVencimiento.Where(dto => dto.StageDocumento != "CANCELADO" && dto.Mora == fechaVencimientoMora).ToList();
            var fechaVencimientoValue = fechaVencimiento.ElementAt(0);
            txtDetailReference3.DateTime = fechaVencimientoValue.DateApply;

            //Fecha del proximo pago	
            var fechaProximoPago = data.Where(dto => dto.StageDocumento != "CANCELADO").ToList();
            var fechaProximoPagoMora = fechaProximoPago.Max(dto => dto.Mora);
            fechaProximoPago = fechaProximoPago.Where(dto => dto.StageDocumento != "CANCELADO" && dto.Mora == fechaProximoPagoMora).ToList();
            var fechaProximoPagoValue = fechaProximoPago.ElementAt(0);
            txtDetailReference2.DateTime = fechaProximoPagoValue.DateApply;

            //Dias del Proximo Pago
            if (fechaProximoPagoMora > 0)
            {
                txtDetailReference4.Text = @"0";
            }
            else
            {
                txtDetailReference4.EditValue = fechaProximoPagoMora * -1;
            }

            if (TransactionMasterId == 0 && TransactionId == 0)
            {
                if (ObjParameterAttendanceAutoPrinter.Equals("true", StringComparison.InvariantCultureIgnoreCase))
                {
                    CommandSave(null, new EventArgs());
                }
            }
        }

        private void FnCompleteGetCustomerCreditLineEdit()
        {
            var getData = formInvoiceApi.GetLineByCustomer(txtCustomerID);
            if (getData is null)
            {
                txtCustomerID = 0;
                txtCustomerDescription.Text = @"Cliente no tiene membresía";
                objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", "Cliente sin membresia", this);
                return;
            }

            var data = getData.ObjCustomerCreditAmoritizationAll;
            if (data is null || data.Count <= 0)
            {
                txtCustomerID = 0;
                txtCustomerDescription.Text = @"Cliente no tiene membresía";
                objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", "Cliente sin membresia", this);
                return;
            }

            var cantidadMora = data.Where(dto => dto.Mora > 0).Select(dto => dto.Mora).Sum() ?? 0;
            switch (cantidadMora)
            {
                case > 0:
                    objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", "Cliente con mora", this);
                    txtDetailReference1.Text = @"NO";
                    break;
                case <= 0:
                    txtDetailReference1.Text = @"SI";
                    break;
            }

            //Fecha del proximo pago
            var fechaProximoPagoMax = data.Max(dto => dto.Mora) ?? 0;
            var FechaProximoPagoFilter = data.Where(dto => dto.Mora == fechaProximoPagoMax).ToList();
            var fechaProximaPago = FechaProximoPagoFilter.ElementAt(0).DateApply;
            txtDetailReference2.DateTime = fechaProximaPago;

            //Fecha de Vencimiento
            var fechaVencimientoMin = data.Min(dto => dto.Mora) ?? 0;
            var fechaVencimientoFilter = data.Where(dto => dto.Mora == fechaVencimientoMin).ToList();
            var fechaVencimiento = fechaVencimientoFilter.ElementAt(0).DateApply;
            txtDetailReference3.DateTime = fechaVencimiento;
        }

        private bool FnValidateForm()
        {
            if (txtDate.EditValue is null)
            {
                objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", "Establecer Fecha al Documento", this);
                return false;
            }

            if (!DateTime.TryParse(txtDate.Text, out _))
            {
                objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Fecha", "Por favor, ingrese una fecha válida", this);
                return false;
            }

            if (txtCustomerID is <= 0 or 13)
            {
                objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Error", "Seleccionar el Cliente", this);
                return false;
            }

            return true;
        }

        //Preparar el lector
        private void FnActiveSensorRead()
        {
            if (formFingerprintApi.WebActiveSensorRead())
            {
                objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Informacion, "Sensor", "Lectura de forma correcta", this);
            }
            else
            {
                objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Informacion, "Sensor", "Error en Lectura de Sensor", this);
            }
        }

        //Evento que verifica si ya se lello la huella
        private void FnHuellaLeida()
        {
            var data = formFingerprintApi.WebSsejs();
            if (data is not null)
            {
                if (data.Image is not null)
                {
                    if (data.UserId is not null)
                    {
                        txtCustomerID = (int)data.UserId;
                        txtCustomerDescription.Text = data.Name;
                        FnCompleteGetCustomerCreditLine();
                    }
                    else
                    {
                        objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Informacion, "Huella", "El usuairo no existe", this);
                    }
                }
            }
            else
            {
                objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Informacion, "Huella", "Error al leer la huella", this);
            }
        }

        private void InicializarTimer()
        {
            var tick = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["TIMER_TICK_HUELLA_ASISTENCIA"]);
            timerHuella = new System.Windows.Forms.Timer();
            timerHuella.Interval = tick;
            timerHuella.Tick += Timer_Tick;
            timerHuella.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (TransactionId.Value == 0 && TransactionMasterId.Value == 0)
            {
                FnHuellaLeida();
            }
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

        private void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            var formTypeListSearch = new FormTypeListSearch("Lista de Clientes", ObjComponentCustomer.ComponentID,
                "SELECCIONAR_CLIENTES_BILLING", true, @"", false, "", 0, 5, "", true);
            formTypeListSearch.EventoCallBackAceptarEvent += EventoCallBackAceptarCustomer;
            formTypeListSearch.ShowDialog(this);
        }

        private void btnClearCustomer_Click(object sender, EventArgs e)
        {
            txtCustomerID = 0;
            txtCustomerDescription.Clear();
        }

        private void EventoCallBackAceptarCustomer(dynamic mensaje)
        {
            FnOnCompleteNewCustomerPopPub(mensaje);
        }

        private void FormAttendanceEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerHuella.Stop();
            timerHuella.Dispose();
        }

        private void btnScanerHuella_Click(object sender, EventArgs e)
        {
            FnActiveSensorRead();
        }

        #endregion
    }
}