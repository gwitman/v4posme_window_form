using System.ComponentModel;
using System.Diagnostics;
using DevExpress.Data.Svg;
using DevExpress.XtraEditors;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Libraries.CustomModels;
using v4posme_window.Dto;
using v4posme_window.Interfaz;
using v4posme_window.Libraries;
using v4posme_window.Template;
using Unity;
using v4posme_library.Libraries.CustomHelper;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_window.Views.Box.Share;

public partial class FormShareEdit : FormTypeHeadEdit, IFormTypeEdit
{
    #region Campos

    private TypeOpenForm TypeOpen { get; set; }
    private int? TransactionMasterId = 0;
    private int? TransactionId = 0;
    private int? EntityId;
    private int txtCustomerID = 0;

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

    #endregion

    #region Librerias

    private readonly ICoreWebAccounting objInterfazCoreWebAccounting = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebAccounting>();
    private readonly ICoreWebAuditoria objInterfazCoreWebAuditoria = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebAuditoria>();
    private readonly ICoreWebCatalog objInterfazCoreWebCatalog = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCatalog>();
    private readonly ICoreWebConcept objInterfazCoreWebConcept = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebConcept>();
    private readonly ICoreWebParameter objInterfazCoreWebParameter = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebParameter>();
    private readonly ICoreWebTransaction objInterfazCoreWebTransaction = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTransaction>();
    private readonly ICoreWebCurrency objInterfazCoreWebCurrency = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCurrency>();
    private readonly ICoreWebTools objInterfazCoreWebTools = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();
    private readonly ICoreWebCounter objInterfazCoreWebCounter = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCounter>();
    private readonly ICoreWebWorkflow objInterfazCoreWebWorkflow = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebWorkflow>();
    private readonly ICoreWebInventory objInterfazCoreWebInventory = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebInventory>();
    private readonly CoreWebRenderInView objInterfazCoreWebRenderInView = new();
    private readonly ICoreWebPermission objInterfazCoreWebPermission = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebPermission>();
    private readonly ICustomerModel customerModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerModel>();
    private readonly ICustomerCreditDocumentModel customerCreditDocumentModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerCreditDocumentModel>();
    private readonly IProviderModel providerModel = VariablesGlobales.Instance.UnityContainer.Resolve<IProviderModel>();
    private readonly IProviderItemModel providerItemModel = VariablesGlobales.Instance.UnityContainer.Resolve<IProviderItemModel>();
    private readonly IPriceModel priceModel = VariablesGlobales.Instance.UnityContainer.Resolve<IPriceModel>();
    private readonly INaturalModel naturalModel = VariablesGlobales.Instance.UnityContainer.Resolve<INaturalModel>();
    private readonly IListPriceModel listPriceModel = VariablesGlobales.Instance.UnityContainer.Resolve<IListPriceModel>();
    private readonly ICompanyCurrencyModel companyCurrencyModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyCurrencyModel>();
    private readonly IUserWarehouseModel userWarehouseModel = VariablesGlobales.Instance.UnityContainer.Resolve<IUserWarehouseModel>();
    private readonly ITransactionMasterModel transactionMasterModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterModel>();
    private readonly ITransactionModel transactionModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionModel>();
    private readonly ITransactionMasterDetailModel transactionMasterDetailModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterDetailModel>();
    private readonly ITransactionCausalModel transactionCausalModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionCausalModel>();
    private readonly ILegalModel legalModel = VariablesGlobales.Instance.UnityContainer.Resolve<ILegalModel>();

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
        throw new NotImplementedException();
    }

    public void ComandPrinter()
    {
        throw new NotImplementedException();
    }

    public void LoadEdit()
    {
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
        throw new NotImplementedException();
    }

    public void SaveUpdate()
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
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
        switch (typeRedner)
        {
            case TypeRender.New:
                txtCustomerID = 0;
                txtCustomerDescription.Text = string.Empty;
                txtDate.DateTime = DateTime.Now;
                txtIsApplied.Checked = false;
                txtExchangeRate.EditValue = ExchangeRate;
                lblExchangeRatePurchase.Text = $@"{ExchangeRatePurchase:N2}";
                lblExchangeRateSale.Text = $@"{ExchangeRateSale:N2}";
                lblExchangeRate.Text = $@"{ExchangeRate:N2}";
                CoreWebRenderInView.LlenarComboBox(ObjListWorkflowStage, txtStatusID, "WorkflowStageID", "Name", ObjListWorkflowStage.ElementAt(0).WorkflowStageID);
                CoreWebRenderInView.LlenarComboBox(ObjListCurrency, txtCurrencyID, "CurrencyId", "Name", ObjListCurrencyDefault.CurrencyID);
                break;
            case TypeRender.Edit:
                break;
        }
    }


    public void InitializeControl()
    {
        throw new NotImplementedException();
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
            var companyId = Convert.ToInt32(diccionario["companyID"]);
            txtCustomerID = Convert.ToInt32(diccionario["entityID"]);
            txtCustomerDescription.Text = $@"{diccionario["Codigo"]} {diccionario["Nombre"]} / {diccionario["Comercial"]}";
            var selectedCurrency = txtCurrencyID.SelectedItem as ComboBoxItem;
            ObjListCustomerCreditDocument = selectedCurrency is not null ? customerCreditDocumentModel.GetRowByEntityApplied(companyId, txtCustomerID, Convert.ToInt32(selectedCurrency.Key)) : new();
            if (ObjListCustomerCreditDocument.Count > 0)
            {
                var saldoTotal = decimal.Zero;
                ObjListCustomerCreditDocument.ForEach(dto => saldoTotal = decimal.Add(saldoTotal, dto.Remaining ?? decimal.Zero));
                txtBalanceStart.EditValue = saldoTotal;
            }
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
            // Suponiendo que 'objListaCustomerCredit' es una lista de objetos en C#
            var objBalancesDocument = ObjListCustomerCreditDocument
                .Where(obj => obj.DocumentNumber == detailTransactionDetailDocument)
                .Select(obj => obj)
                .SingleOrDefault();
            if (objBalancesDocument is null)
            {
                objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error,"Error",$"No hay un documento con el numero de documento {detailTransactionDetailDocument}",this);
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
        });
    }

    #endregion

    #region Eventos

    private void BtnImprmir_Click(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void BtnEliminarOnClick(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
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
        if (ObjComponentCustomer is null)
        {
            XtraMessageBox.Show("No se ha definido el componente ObjComponentCustomer");
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
            objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Seleccione el cliente para continuar", "Cliente", this);
            return;
        }

        var selectedCurrency = txtCurrencyID.SelectedItem as ComboBoxItem;
        if (selectedCurrency is null)
        {
            objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Seleccione la moneda para continuar", "Moneda", this);
            return;
        }

        if (ObjComponentCustomerCreditDocument is null)
        {
            objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "No existe el componente ComponentCustomerCreditDocument", "Moneda", this);
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

    #endregion

}