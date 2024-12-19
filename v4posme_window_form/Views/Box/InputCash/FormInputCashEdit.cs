using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Libraries.CustomModels;
using v4posme_window.Api;
using v4posme_window.Interfaz;
using v4posme_window.Libraries;
using v4posme_window.Template;
using Unity;
using v4posme_library.ModelsDto;
using v4posme_library.Models;
using v4posme_window.Dto;
using v4posme_window.Views.Box.CancelDocument;
using ESC_POS_USB_NET.Printer;

namespace v4posme_window.Views.Box.InputCash
{
    public partial class FormInputCashEdit : FormTypeHeadEdit, IFormTypeEdit
    {
        #region Campos

        private TypeOpenForm TypeOpen { get; set; }
        private int? TransactionMasterId = 0;
        private int? TransactionId = 0;
        private int? txtDetailTransactionDetailID;
        private RenderFileGridControl renderGridFiles;

        #endregion

        #region Modelos

        public List<TbBranch> ObjListBranch { get; set; }

        public List<TbCatalogItem>? ObjListDenomination { get; set; }

        public List<TbCatalogItem>? ObjSubTipoMovement { get; set; }

        public List<TbCatalogItem>? ObjTipoMovement { get; set; }

        public List<TbWorkflowStage>? ObjListWorkflowStage { get; set; }

        public List<TbTransactionCausal>? ObjCaudal { get; set; }

        public decimal ExchangeRateSale { get; set; }

        public decimal ExchangeRatePurchase { get; set; }

        public decimal ExchangeRate { get; set; }

        public List<TbCompanyCurrencyDto>? ObjListCurrency { get; set; }

        public string? ObjParameterUrlPrinter { get; set; }

        public TbComponent? ObjComponentShare { get; set; }

        public List<TbTransactionMasterDenominationDto> ObjTransactionMasterDenomination { get; set; }

        public List<TbTransactionMasterDetail> ObjTransactionMasterDetail { get; set; }

        public TbTransactionMasterDto? ObjTransactionMaster { get; set; }

        public ObservableCollection<FormInputCashDetailDto> FormInputCashDetaiList = new ObservableCollection<FormInputCashDetailDto>();

        #endregion

        #region Librerias

        private readonly ICoreWebAmortization objInterfazCoreWebAmortization = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebAmortization>();
        private readonly ICoreWebAuditoria objInterfazCoreWebAuditoria = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebAuditoria>();
        private readonly ICoreWebAccounting objInterfazCoreWebAccounting = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebAccounting>();
        private readonly ICoreWebCounter objInterfazCoreWebCounter = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCounter>();
        private readonly ICoreWebConcept objInterfazCoreWebConcept = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebConcept>();
        private readonly ICoreWebParameter objInterfazCoreWebParameter = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebParameter>();
        private readonly ICoreWebCatalog objInterfazCoreWebCatalog = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCatalog>();
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
        private readonly ITransactionMasterDenominationModel transactionMasterDenominationModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterDenominationModel>();
        private readonly ITransactionCausalModel transactionCausalModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionCausalModel>();
        private readonly ITransactionMasterInfoModel transactionMasterInfoModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterInfoModel>();
        private readonly ITransactionMasterDetailCreditModel transactionMasterDetailCreditModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterDetailCreditModel>();
        private readonly ITransactionMasterDetailReferencesModel transactionMasterDetailReferencesModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterDetailReferencesModel>();
        private readonly IWorkflowStageModel workflowStageModel = VariablesGlobales.Instance.UnityContainer.Resolve<IWorkflowStageModel>();
        private readonly IEmployeeModel employeeModel = VariablesGlobales.Instance.UnityContainer.Resolve<IEmployeeModel>();
        private readonly ICompanyModel companyModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyModel>();
        private readonly IUserModel userModel = VariablesGlobales.Instance.UnityContainer.Resolve<IUserModel>();
        private readonly IBranchModel branchModel = VariablesGlobales.Instance.UnityContainer.Resolve<IBranchModel>();
        private readonly AppCatalogApi appCatalogApi = new();

        private readonly ICashBoxSessionModel cashBoxSessionModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICashBoxSessionModel>();
        private readonly ICashBoxUserModel cashBoxUserModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICashBoxUserModel>();
        private readonly ICatalogItemModel catalogItemModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICatalogItemModel>();

        #endregion

        #region Init

        public FormInputCashEdit()
        {
            InitializeComponent();
        }

        public FormInputCashEdit(TypeOpenForm typeOpenForm, int companyId, int transactionMasterId, int transactionId)
        {
            InitializeComponent();
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TypeOpen = TypeOpen;
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

        private void FormInputCashEdit_Load(object sender, EventArgs e)
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
                var permited = objInterfazCoreWebPermission.UrlPermited("app_box_inputcash", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(notAccessControl);
                }

                resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd("app_box_inputcash", "delete", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    throw new Exception(notAllDelete);
                }
            }

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
            var validateWorkflowStage = objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_inputcash", "statusID", objTm.StatusId.Value, commandEliminable, user.CompanyID, user.BranchID, role.RoleID);
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
                var permited = objInterfazCoreWebPermission.UrlPermited("app_box_inputcash", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(notAccessControl);
                }

                resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd("app_box_inputcash", "edit", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
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

            //Get Documento	
            var objTM = transactionMasterModel.GetRowByPk(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
            var objTmd = transactionMasterDetailModel.GetRowByTransactionToShare(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
            var objTmi = transactionMasterInfoModel.GetRowByPk(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
            var objUser = userModel.GetRowByPk(objTM.CompanyId, objTM.CreatedAt.Value, objTM.CreatedBy.Value);
            var objBranch = branchModel.GetRowByPk(objTM.CompanyId, objTM.BranchId.Value);
            //var objCustumer = customerModel.GetRowByEntity(user.CompanyID, objTM.EntityId.Value);
            //var objNatural = naturalModel.GetRowByPk(user.CompanyID, objCustumer.BranchId, objCustumer.EntityId);
            var objCurrency = currencyModel.GetRowByPk(objTM.CurrencyId.Value);
            var objStage = objInterfazCoreWebWorkflow.GetWorkflowStage("tb_transaction_master_inputcash", "statusID", objTM.StatusId.Value, user.CompanyID, user.BranchID, role.RoleID);

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
                printer.BoldMode("INGRESO DE CAJA");
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
                printer.Append("sistema 505-8712-5827");
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
                var permited = objInterfazCoreWebPermission.UrlPermited("app_box_inputcash", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(notAccessControl);
                }

                var resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd("app_box_inputcash", "edit", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    throw new Exception(notAllEdit);
                }
            }

            if (TransactionId is 0 && TransactionMasterId is 0)
            {
                Close();
                var frm = new FormInputCashEdit(TypeOpenForm.Init, 0, 0, 0)
                {
                    MdiParent = CoreFormList.Principal()
                };
                frm.Show();
                return;
            }

            ObjComponentShare = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_transaction_master_inputcash");
            if (ObjComponentShare is null)
            {
                throw new Exception("EL COMPONENTE 'tb_transaction_master_inputcash' NO EXISTE...");
            }

            var objCurrency = objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyID);
            var targetCurrency = objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyID);
            ObjListCurrency = companyCurrencyModel.GetByCompany(user.CompanyID);
            ObjTransactionMaster = transactionMasterModel.GetRowByPk(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
            ObjTransactionMasterDetail = transactionMasterDetailModel.GetRowByTransactionToShare(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
            ObjTransactionMasterDenomination = transactionMasterDenominationModel.GetRowByTransactionMaster(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
            ExchangeRate = objInterfazCoreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, decimal.One, targetCurrency.CurrencyID, objCurrency.CurrencyID);
            ObjListWorkflowStage = objInterfazCoreWebWorkflow.GetWorkflowStageByStageInit("tb_transaction_master_inputcash", "statusID", ObjTransactionMaster.StatusId.Value, user.CompanyID, user.BranchID, role.RoleID);
            ObjTipoMovement = objInterfazCoreWebCatalog.GetCatalogAllItem("tb_transaction_master_inputcash", "areaID", user.CompanyID);
            if (ObjTransactionMaster.PriorityId is null)
            {
                ObjSubTipoMovement = new();
            }
            else
            {
                ObjSubTipoMovement = objInterfazCoreWebCatalog.GetCatalogAllItemParent("tb_transaction_master_inputcash", "priorityID", user.CompanyID, ObjTransactionMaster.PriorityId.Value);
            }

            ObjListDenomination = objInterfazCoreWebCatalog.GetCatalogAllItem("tb_transaction_master_denomination", "catalogItemID", user.CompanyID);
            ObjListBranch = branchModel.GetByCompany(user.CompanyID);
            ObjParameterUrlPrinter = objInterfazCoreWebParameter.GetParameterValue("BOX_INPUTCASH_URL_PRINTER", user.CompanyID);
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
                var permited = objInterfazCoreWebPermission.UrlPermited("app_box_inputcash", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(notAccessControl);
                }

                var resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd("app_box_inputcash", "add", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    throw new Exception(notAllInsert);
                }
            }

            var transactionID = objInterfazCoreWebTransaction.GetTransactionId(user.CompanyID, "tb_transaction_master_inputcash", 0);
            var objCurrency = objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyID);
            var targetCurrency = objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyID);
            ObjListCurrency = companyCurrencyModel.GetByCompany(user.CompanyID);
            ExchangeRate = objInterfazCoreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, decimal.One, targetCurrency.CurrencyID, objCurrency.CurrencyID);
            var objParameterExchangePurchase = objInterfazCoreWebParameter.GetParameterValue("ACCOUNTING_EXCHANGE_PURCHASE", user.CompanyID);
            ExchangeRatePurchase = ExchangeRate - Convert.ToDecimal(objParameterExchangePurchase);
            var objParameterExchangeSales = objInterfazCoreWebParameter.GetParameterValue("ACCOUNTING_EXCHANGE_SALE", user.CompanyID);
            ExchangeRateSale = ExchangeRate + Convert.ToDecimal(objParameterExchangeSales);
            ObjCaudal = transactionCausalModel.GetCausalByBranch(user.CompanyID, transactionID.Value, user.BranchID);
            ObjListWorkflowStage = objInterfazCoreWebWorkflow.GetWorkflowInitStage("tb_transaction_master_inputcash", "statusID", user.CompanyID, user.BranchID, role.RoleID);
            ObjTipoMovement = objInterfazCoreWebCatalog.GetCatalogAllItem("tb_transaction_master_inputcash", "areaID", user.CompanyID);
            ObjSubTipoMovement = objInterfazCoreWebCatalog.GetCatalogAllItem("tb_transaction_master_inputcash", "priorityID", user.CompanyID);
            ObjListDenomination = objInterfazCoreWebCatalog.GetCatalogAllItem("tb_transaction_master_denomination", "catalogItemID", user.CompanyID);
            ObjListBranch = branchModel.GetByCompany(user.CompanyID);
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
                var permited = objInterfazCoreWebPermission.UrlPermited("app_box_inputcash", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(notAccessControl);
                }

                var resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd("app_box_inputcash", "add", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    throw new Exception(notAllInsert);
                }
            }

            objInterfazCoreWebPermission.GetValueLicense(user.CompanyID, "app_box_inputcash/index");

            ObjComponentShare = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_transaction_master_inputcash");
            if (ObjComponentShare is null)
            {
                throw new Exception("EL COMPONENTE 'tb_transaction_master_inputcash' NO EXISTE...");
            }

            var objComponentDenomination = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_transaction_master_denomination");
            if (objComponentDenomination is null)
            {
                throw new Exception("EL COMPONENTE 'tb_transaction_master_denomination' NO EXISTE...");
            }

            if (objInterfazCoreWebAccounting.CycleIsCloseByDate(user.CompanyID, txtDate.DateTime.Date))
            {
                throw new Exception("EL DOCUMENTO NO PUEDE INGRESAR, EL CICLO CONTABLE ESTA CERRADO");
            }

            TransactionId = objInterfazCoreWebTransaction.GetTransactionId(user.CompanyID, "tb_transaction_master_inputcash", 0);
            var objT = transactionModel.GetByCompanyAndTransaction(user.CompanyID, TransactionId.Value);
            var selectedBranch = txtBranchID.SelectedItem as ComboBoxItem;
            var selectedCurrency = txtCurrencyID.SelectedItem as ComboBoxItem;
            var currencyId2 = objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyID).CurrencyID;
            var selectedStatus = txtStatusID.SelectedItem as ComboBoxItem;
            var selectedArea = txtAreaID.SelectedItem as ComboBoxItem;
            var selectedPriority = txtPriorityID.SelectedItem as ComboBoxItem;
            var detailAmount = Convert.ToDecimal(txtDetailAmount.EditValue);
            var objTM = new TbTransactionMaster
            {
                CompanyID = user.CompanyID,
                TransactionID = TransactionId.Value,
                BranchID = Convert.ToInt32(selectedBranch.Key),
                TransactionNumber = objInterfazCoreWebCounter.GoNextNumber(user.CompanyID, user.BranchID, "tb_transaction_master_inputcash", 0),
                TransactionCausalID = objInterfazCoreWebTransaction.GetDefaultCausalId(user.CompanyID, TransactionId.Value),
                TransactionOn = txtDate.DateTime,
                StatusIDChangeOn = DateTime.Now,
                ComponentID = ObjComponentShare.ComponentID,
                Note = txtNote.Text,
                Sign = (short?)objT.SignInventory.Value,
                CurrencyID = Convert.ToInt32(selectedCurrency.Key),
                CurrencyID2 = currencyId2,
                ExchangeRate = objInterfazCoreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, decimal.One, currencyId2, Convert.ToInt32(selectedCurrency.Key)),
                Reference1 = txtDetailReference1.Text,
                Reference2 = txtDetailReference2.Text,
                Reference3 = txtDetailReference3.Text,
                Reference4 = string.Empty,
                StatusID = Convert.ToInt32(selectedStatus.Key),
                Amount = decimal.Zero, //no existe txtTotal
                IsApplied = false,
                JournalEntryID = 0,
                ClassID = null,
                AreaID = Convert.ToInt32(selectedArea.Key),
                PriorityID = selectedPriority is null ? null : Convert.ToInt32(selectedPriority.Key),
                SourceWarehouseID = null,
                TargetWarehouseID = null,
                IsActive = true
            };

            objInterfazCoreWebAuditoria.SetAuditCreated(objTM, user, "");

            TransactionMasterId = transactionMasterModel.InsertAppPosme(objTM);
            var pathFileOfApp = VariablesGlobales.ConfigurationBuilder["PATH_FILE_OF_APP"];
            var path = $"{pathFileOfApp}/company_{user.CompanyID}/component_{ObjComponentShare.ComponentID}/component_item_{TransactionMasterId}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Ingresar el detalle de moneda
            if (FormInputCashDetaiList.Count > 0)
            {
                foreach (var detail in FormInputCashDetaiList)
                {
                    var objTMDeno = new TbTransactionMasterDenomination
                    {
                        CompanyID = user.CompanyID,
                        TransactionID = objTM.TransactionID,
                        TransactionMasterID = objTM.TransactionMasterID,
                        IsActive = 1,
                        ComponentID = objComponentDenomination.ComponentID,
                        CatalogItemID = detail.TransactionMasterDenominationCatalogItemId,
                        CurrencyID = detail.TransactionMasterDenominationCurrencyId,
                        ExchangeRate = detail.TransactionMasterDenominationExchangeRate,
                        Quantity = (int)detail.TransactionMasterDenominationQuantity,
                        Reference1 = detail.TransactionMasterDenominationReference,
                    };
                    transactionMasterDenominationModel.InsertAppPosme(objTMDeno);
                }
            }

            var amount = decimal.Zero;
            var arrayListShare = detailAmount;
            if (decimal.Compare(arrayListShare, decimal.Zero) > 0)
            {
                amount = arrayListShare;
                var objTMD = new TbTransactionMasterDetail
                {
                    CompanyID = objTM.CompanyID,
                    TransactionID = objTM.TransactionID,
                    TransactionMasterID = objTM.TransactionMasterID,
                    ComponentID = 0,
                    ComponentItemID = 0,
                    Quantity = decimal.Zero,
                    UnitaryCost = decimal.Zero,
                    Cost = decimal.Zero,
                    UnitaryPrice = decimal.Zero,
                    UnitaryAmount = decimal.Zero,
                    Amount = amount,
                    Discount = 0,
                    PromotionID = 0,
                    Reference1 = $"{0}",
                    Reference2 = $"{0}",
                    Reference3 = $"{0}",
                    CatalogStatusID = 0,
                    InventoryStatusID = 0,
                    IsActive = true,
                    QuantityStock = decimal.Zero,
                    QuantiryStockInTraffic = decimal.Zero,
                    QuantityStockUnaswared = decimal.Zero,
                    RemaingStock = decimal.Zero,
                    ExpirationDate = null,
                    InventoryWarehouseSourceID = null,
                    InventoryWarehouseTargetID = null
                };

                transactionMasterDetailModel.InsertAppPosme(objTMD);
            }

            objTM.Amount = amount;
            transactionMasterModel.UpdateAppPosme(user.CompanyID, objTM.TransactionID, objTM.TransactionMasterID, objTM);
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
                var permited = objInterfazCoreWebPermission.UrlPermited("app_box_inputcash", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(notAccessControl);
                }

                resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd("app_box_inputcash", "edit", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    throw new Exception(notAllEdit);
                }
            }

            ObjComponentShare = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_transaction_master_inputcash");
            if (ObjComponentShare is null)
            {
                throw new Exception("EL COMPONENTE 'tb_transaction_master_inputcash' NO EXISTE...");
            }

            var objComponentDenomination = objInterfazCoreWebTools.GetComponentIdByComponentName("tb_transaction_master_denomination");
            if (objComponentDenomination is null)
            {
                throw new Exception("EL COMPONENTE 'tb_transaction_master_denomination' NO EXISTE...");
            }

            var objTM = transactionMasterModel.GetRowByPk(user.CompanyID, TransactionId.Value, TransactionMasterId.Value);
            var oldStatusID = objTM.StatusId;
            var objCurrencyDolares = objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyID);
            var objCurrencyCordoba = objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyID);
            ExchangeRate = objInterfazCoreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, decimal.One, objCurrencyDolares.CurrencyID, objCurrencyCordoba.CurrencyID);

            var permissionMe = VariablesGlobales.ConfigurationBuilder["PERMISSION_ME"];
            if (resultPermission == Convert.ToInt32(permissionMe) && objTM.CreatedBy != user.UserID)
            {
                throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_EDIT"]);
            }

            var commandEditableTotal = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_EDITABLE_TOTAL"]);
            var validateWorkflow = objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_inputcash", "statusID", objTM.StatusId!.Value, commandEditableTotal, user.CompanyID, user.BranchID, role.RoleID);
            if (validateWorkflow.HasValue && !validateWorkflow.Value)
            {
                throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_WORKFLOW_EDIT"]);
            }

            if (objInterfazCoreWebAccounting.CycleIsCloseByDate(user.CompanyID, objTM.TransactionOn.Value))
            {
                throw new Exception("EL DOCUMENTO NO PUEDE INGRESAR, EL CICLO CONTABLE ESTA CERRADO");
            }

            var selectedBranch = txtBranchID.SelectedItem as ComboBoxItem;
            var selectedCurrency = txtCurrencyID.SelectedItem as ComboBoxItem;
            var currencyId2 = objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyID).CurrencyID;
            var selectedStatus = txtStatusID.SelectedItem as ComboBoxItem;
            var selectedArea = txtAreaID.SelectedItem as ComboBoxItem;
            var selectedPriority = txtPriorityID.SelectedItem as ComboBoxItem;
            var detailAmount = Convert.ToDecimal(txtDetailAmount.EditValue);

            var objTMNew = transactionMasterModel.GetRowByPKK(objTM.TransactionMasterId);
            objTMNew.TransactionOn = txtDate.DateTime;
            objTMNew.StatusIDChangeOn = DateTime.Now;
            objTMNew.BranchID = Convert.ToInt32(selectedBranch.Key);
            objTMNew.Note = txtNote.Text;
            objTMNew.CurrencyID = Convert.ToInt32(selectedCurrency.Key);
            objTMNew.ExchangeRate = objInterfazCoreWebCurrency.GetRatio(user.CompanyID, DateTime.Now.Date, decimal.One, objTM.CurrencyId2.Value, objTMNew.CurrencyID.Value);
            objTMNew.AreaID = Convert.ToInt32(selectedArea.Key);
            objTMNew.PriorityID = selectedPriority is null ? 0 : Convert.ToInt32(selectedPriority.Key);
            objTMNew.Reference1 = txtDetailReference1.Text;
            objTMNew.Reference2 = txtDetailReference2.Text;
            objTMNew.Reference3 = txtDetailReference3.Text;
            objTMNew.StatusID = Convert.ToInt32(selectedStatus.Key);
            objTMNew.Amount = decimal.Zero;
            var COMMAND_EDITABLE = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_EDITABLE"]);
            if (objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_transaction_master_inputcash", "statusID", objTM.StatusId.Value, COMMAND_EDITABLE, user.CompanyID, user.BranchID, role.RoleID).Value)
            {
                var objTmMaster = transactionMasterModel.GetRowByPKK(objTM.TransactionMasterId);
                objTmMaster.StatusID = Convert.ToInt32(selectedStatus.Key);
                transactionMasterModel.UpdateAppPosme(user.CompanyID, TransactionId.Value, TransactionMasterId.Value, objTmMaster);
            }
            else
            {
                transactionMasterModel.UpdateAppPosme(user.CompanyID, TransactionId.Value, TransactionMasterId.Value, objTMNew);
            }

            //Ingresar el detalle de moneda
            transactionMasterDenominationModel.DeleteAppPosme(TransactionMasterId.Value);
            if (FormInputCashDetaiList.Count>0)
            {
                foreach (var detail in FormInputCashDetaiList)
                {
                    var objTMDeno = new TbTransactionMasterDenomination
                    {
                        CompanyID = user.CompanyID,
                        TransactionID = TransactionId.Value,
                        TransactionMasterID = TransactionMasterId.Value,
                        IsActive = 1,
                        ComponentID = objComponentDenomination.ComponentID,
                        CatalogItemID = detail.TransactionMasterDenominationCatalogItemId,
                        CurrencyID = detail.TransactionMasterDenominationCurrencyId,
                        ExchangeRate = detail.TransactionMasterDenominationExchangeRate,
                        Quantity = (int)detail.TransactionMasterDenominationQuantity,
                        Reference1 = detail.TransactionMasterDenominationReference,
                    };
                    transactionMasterDenominationModel.InsertAppPosme(objTMDeno);
                }
            }

            //Actualizar Detalle
            var arrayListTransactionDetailID = txtDetailTransactionDetailID ?? 0;
            var arrayListTransactionDetailID_ = new List<int> { arrayListTransactionDetailID };
            var arrayListShare = Convert.ToDecimal(txtDetailAmount.EditValue);
            var amount = decimal.Zero;
            transactionMasterDetailModel.DeleteWhereIdNotIn(user.CompanyID, TransactionId.Value, TransactionMasterId.Value, arrayListTransactionDetailID_);

            if (arrayListTransactionDetailID > 0)
            {
                amount += arrayListShare;
                var transactionDetailID = arrayListTransactionDetailID;

                //Nuevo Detalle
                if (transactionDetailID == 0)
                {
                    var objTMD = new TbTransactionMasterDetail
                    {
                        CompanyID = objTM.CompanyId,
                        TransactionID = objTM.TransactionId,
                        TransactionMasterID = objTM.TransactionMasterId,
                        ComponentID = ObjComponentShare.ComponentID,
                        ComponentItemID = 0,
                        Quantity = decimal.Zero,
                        UnitaryCost = decimal.Zero,
                        Cost = decimal.Zero,
                        UnitaryPrice = decimal.Zero,
                        UnitaryAmount = decimal.Zero,
                        Amount = amount,
                        Discount = 0,
                        PromotionID = 0,
                        Reference1 = string.Empty,
                        Reference2 = string.Empty,
                        Reference3 = string.Empty,
                        CatalogStatusID = 0,
                        InventoryStatusID = 0,
                        IsActive = true,
                        QuantityStock = decimal.Zero,
                        QuantiryStockInTraffic = decimal.Zero,
                        QuantityStockUnaswared = decimal.Zero,
                        RemaingStock = decimal.Zero,
                        ExpirationDate = null,
                        InventoryWarehouseSourceID = null,
                        InventoryWarehouseTargetID = null
                    };
                    //amount += objTMD.Amount.Value; creo q se va a duplicar el monto con esta lineas
                    transactionMasterDetailModel.InsertAppPosme(objTMD);
                }
                //Editar Detalle
                else
                {
                    var objTMDNew = transactionMasterDetailModel.GetRowByPKK(transactionDetailID);
                    objTMDNew.Amount = amount;
                    objTMDNew.Reference1 = string.Empty;
                    objTMDNew.Reference2 = string.Empty;
                    objTMDNew.Reference3 = string.Empty;
                    objTMDNew.ExchangeRateReference = decimal.Zero;
                    objTMDNew.DescriptionReference = "";
                    transactionMasterDetailModel.UpdateAppPosme(user.CompanyID, TransactionId.Value, TransactionMasterId.Value, transactionDetailID, objTMDNew);
                }
            }

            //Actualizar Transaccion
            objTMNew.Amount = amount;
            transactionMasterModel.UpdateAppPosme(user.CompanyID, objTMNew.TransactionID, objTMNew.TransactionMasterID, objTMNew);

            //Abrir caja si el tipo es Apertura
            var typeInputCash = objTMNew.AreaID;
            var objCatalogItem = catalogItemModel.GetRowByCatalogItemId(typeInputCash.Value);
            var objWorkflowStageApply = objInterfazCoreWebWorkflow.GetWorkflowStageApplyFirst("tb_cash_box_session", "statusID", user.CompanyID, user.BranchID, role.RoleID);
            var objWorkflowStageInit = objInterfazCoreWebWorkflow.GetWorkflowInitStage("tb_cash_box_session", "statusID", user.CompanyID, user.BranchID, role.RoleID);
            var objListCashUser = cashBoxUserModel.GetRowByCompanyIdAndUserId(user.CompanyID, user.UserID);
            var cashBoxID = objListCashUser.Count > 0 ? objListCashUser.ElementAt(0).CashBoxID : 0;
            if (objCatalogItem.Display.Equals("Apertura", StringComparison.InvariantCultureIgnoreCase))
            {
                var objCashBoxSession = cashBoxSessionModel.GetRowByUserIdAndStatusId(user.UserID, objWorkflowStageInit.ElementAt(0).WorkflowStageID);
                if (objCashBoxSession.Count == 0)
                {
                    var cashBoxSession = new TbCashBoxSession
                    {
                        CompanyID = user.CompanyID,
                        BranchID = user.BranchID,
                        CashBoxID = cashBoxID,
                        UserID = user.UserID,
                        IsActive = true,
                        StatusID = objWorkflowStageInit.ElementAt(0).WorkflowStageID,
                        StartOn = DateTime.Now,
                        EndOn = DateTime.MinValue
                    };
                    cashBoxSessionModel.InsertAppPosme(cashBoxSession);
                }
            }
        }

        public void CommandNew(object? sender, EventArgs e)
        {
            Close();
            var frm = new FormInputCashEdit(TypeOpenForm.Init, 0, 0, 0)
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
                    xtraTabPageArchivos.PageVisible = false;
                    lblTitulo.Text = @"INGRESO:#00000000";
                    txtExchangeRate.EditValue = ExchangeRate;
                    txtDetailAmount.EditValue = decimal.Zero;
                    txtDetailTransactionDetailID = 0;
                    txtDate.DateTime = DateTime.Now;
                    CoreWebRenderInView.LlenarComboBox(ObjListWorkflowStage, txtStatusID, "WorkflowStageID", "Name", ObjListWorkflowStage.ElementAt(0).WorkflowStageID);
                    CoreWebRenderInView.LlenarComboBox(ObjListBranch, txtBranchID, "BranchID", "Name", ObjListBranch.ElementAt(0).BranchID);
                    CoreWebRenderInView.LlenarComboBox(ObjListCurrency, txtCurrencyID, "CurrencyId", "Simb", ObjListCurrency.ElementAt(0).CurrencyId);
                    CoreWebRenderInView.LlenarComboBox(ObjTipoMovement, txtAreaID, "CatalogItemID", "Name", ObjTipoMovement.ElementAt(0).CatalogItemID);
                    CoreWebRenderInView.LlenarComboBox(ObjSubTipoMovement, txtPriorityID, "CatalogItemID", "Name", ObjTipoMovement.ElementAt(0).CatalogItemID);
                    if (ObjListDenomination.Count > 0)
                    {
                        foreach (var tbCatalogItem in ObjListDenomination)
                        {
                            var dto = new FormInputCashDetailDto
                            {
                                TransactionMasterDenominationName = tbCatalogItem.Name,
                                TransactionMasterDenominationId = 0,
                                TransactionMasterDenominationCatalogItemId = tbCatalogItem.CatalogItemID,
                                TransactionMasterDenominationCurrencyId = (int)tbCatalogItem.Ratio,
                                TransactionMasterDenominationExchangeRate = ExchangeRate,
                                TransactionMasterDenominationReference = tbCatalogItem.Reference1,
                                TransactionMasterDenominationQuantity = 0
                            };
                            FormInputCashDetaiList.Add(dto);
                        }
                    }

                    UpdatePantalla();
                    break;
                case TypeRender.Edit:
                    btnEliminar.Visible = true;
                    btnImprmir.Visible = true;
                    btnNuevo.Visible = true;
                    xtraTabPageArchivos.PageVisible = true;
                    lblTitulo.Text = @$"INGRESO:#{ObjTransactionMaster.TransactionNumber}";
                    txtExchangeRate.EditValue = ExchangeRate;
                    txtDetailTransactionDetailID = ObjTransactionMasterDetail.ElementAt(0).TransactionMasterDetailID;
                    txtDate.DateTime = ObjTransactionMaster.TransactionOn.Value;
                    txtIsApplied.Checked = ObjTransactionMaster.IsApplied.Value;
                    txtDetailReference1.Text = ObjTransactionMaster.Reference1;
                    txtDetailReference2.Text = ObjTransactionMaster.Reference2;
                    txtDetailReference3.Text = ObjTransactionMaster.Reference3;
                    txtNote.Text = ObjTransactionMaster.Note;
                    CoreWebRenderInView.LlenarComboBox(ObjListWorkflowStage, txtStatusID, "WorkflowStageID", "Name", ObjTransactionMaster.StatusId.Value);
                    CoreWebRenderInView.LlenarComboBox(ObjListBranch, txtBranchID, "BranchID", "Name", ObjTransactionMaster.BranchId.Value);
                    CoreWebRenderInView.LlenarComboBox(ObjListCurrency, txtCurrencyID, "CurrencyId", "Simb", ObjTransactionMaster.CurrencyId.Value);
                    CoreWebRenderInView.LlenarComboBox(ObjTipoMovement, txtAreaID, "CatalogItemID", "Name", ObjTransactionMaster.AreaId.Value);
                    if ( ObjTransactionMaster.PriorityId is not null)
                    {
                        CoreWebRenderInView.LlenarComboBox(ObjSubTipoMovement, txtPriorityID, "CatalogItemID", "Name", ObjTransactionMaster.PriorityId.Value);
                    }
                    if (ObjTransactionMasterDenomination.Count > 0)
                    {
                        FormInputCashDetaiList.Clear();
                        foreach (var deno in ObjTransactionMasterDenomination)
                        {
                            var dto = new FormInputCashDetailDto
                            {
                                TransactionMasterDenominationName = deno.DenominationName,
                                TransactionMasterDenominationId = 0,
                                TransactionMasterDenominationCatalogItemId = deno.CatalogItemId,
                                TransactionMasterDenominationCurrencyId = deno.CurrencyId,
                                TransactionMasterDenominationExchangeRate = ExchangeRate,
                                TransactionMasterDenominationReference = deno.Reference1,
                                TransactionMasterDenominationQuantity = deno.Quantity
                            };
                            FormInputCashDetaiList.Add(dto);
                        }
                    }
                    UpdatePantalla();
                    txtDetailAmount.Text = ObjTransactionMasterDetail.ElementAt(0).Amount.Value.ToString("F2");
                    gridControlArchivos.DataSource = null;
                    gridControlArchivos.MainView = null;
                    renderGridFiles = new RenderFileGridControl(user.CompanyID, ObjComponentShare!.ComponentID, TransactionMasterId.Value);
                    renderGridFiles.RenderGridControl(gridControlArchivos);
                    renderGridFiles.LoadFiles();
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

            if (!DateTime.TryParse(txtDate.Text, out _))
            {
                objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Fecha", "Por favor, ingrese una fecha válida", this);
                return false;
            }

            var monto = Convert.ToDecimal(txtDetailAmount.EditValue);
            if (decimal.Compare(monto, decimal.Zero) <= 0)
            {
                objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Monto", "El monto no puede ser 0", this);
                return false;
            }

            return true;
        }

        private void FnCompletCatalogDetail()
        {
            var selectedArea = txtAreaID.SelectedItem as ComboBoxItem;
            txtPriorityID.Text = "";
            ObjSubTipoMovement = appCatalogApi.GetCatalogItemByParentCatalogItemId("tb_transaction_master_inputcash", "priorityID", Convert.ToInt32(selectedArea.Key));
            var catalogItemId = ObjSubTipoMovement.Count > 0 ? ObjSubTipoMovement.ElementAt(0).CatalogItemID : 0;
            CoreWebRenderInView.LlenarComboBox(ObjSubTipoMovement, txtPriorityID, "CatalogItemID", "Name", catalogItemId);
        }

        private void UpdateAmount()
        {
            var totalTemp = 0m;
            txtDetailAmount.Text = @"0.0";
            if (FormInputCashDetaiList.Count>0)
            {
                totalTemp += FormInputCashDetaiList.Sum(data => (data.TransactionMasterDenominationQuantity * Convert.ToDecimal(data.TransactionMasterDenominationReference)));

                txtDetailAmount.Text = totalTemp.ToString("F2");
            }
        }

        public void UpdatePantalla()
        {
            var selectedArea = txtAreaID.SelectedItem as ComboBoxItem;
            var selectedMoneda = txtCurrencyID.SelectedItem as ComboBoxItem;
            if (selectedArea is null || selectedMoneda is null)
            {
                return;
            }

            if (selectedArea.Value.ToString().Equals("Apertura", StringComparison.InvariantCultureIgnoreCase))
            {
                txtDetailAmount.Properties.ReadOnly = true;
                if (FormInputCashDetaiList.Count > 0)
                {
                    bindingSourceInputCashDetailDto.DataSource = null;
                    foreach (var detailDto in FormInputCashDetaiList)
                    {
                        if (detailDto.TransactionMasterDenominationCurrencyId != Convert.ToInt32(selectedMoneda.Key))
                        {
                            detailDto.TransactionMasterDenominationQuantity = decimal.Zero;
                        }
                    }
                    var cashDetailDtos = FormInputCashDetaiList.Where(item => item.TransactionMasterDenominationCurrencyId == Convert.ToInt32(selectedMoneda.Key)).ToList();
                    bindingSourceInputCashDetailDto.DataSource = cashDetailDtos;
                }
            }
            else
            {
                txtDetailAmount.Properties.ReadOnly = false;
                bindingSourceInputCashDetailDto.DataSource = null;
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

        private void txtCurrencyID_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePantalla();
            UpdateAmount();
        }

        private void txtAreaID_SelectedIndexChanged(object sender, EventArgs e)
        {
            FnCompletCatalogDetail();
            UpdatePantalla();
            UpdateAmount();
        }

        private void gridViewTransactionMaster_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            UpdateAmount();
        }

        #endregion
    }
}