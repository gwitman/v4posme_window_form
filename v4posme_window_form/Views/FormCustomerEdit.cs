using System.ComponentModel;
using System.IO;
using System.Text;
using DevExpress.UITemplates.Collection.Utilities;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout.Utils;
using Unity;
using v4posme_library_biometric.Libraries.CustomModels;
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
using v4posme_window.Template;
using ComboBoxItem = v4posme_window.Libraries.ComboBoxItem;
using v4posme_window.Api;
using Exception = System.Exception;

namespace v4posme_window.Views;

public partial class FormCustomerEdit : FormTypeHeadEdit, IFormTypeEdit
{
    #region Campos

    private TypeRender _typeRender;
    private int _entityId = 0;
    private int _txtEmployerId;
    private int _txtAccountId = 0;
    private BackgroundWorker? _backgroundWorker;
    private RenderFileGridControl? _renderGridFiles;

    #endregion

    #region Modelos

    public TbCustomer? ObjCustomer { get; set; }

    public List<TbPublicCatalogDetail>? ObjPCItemCategoryLeads { get; set; }

    public List<TbPublicCatalogDetail>? ObjPCItemSubTypeLeads { get; set; }

    public List<TbPublicCatalogDetail>? ObjPCItemTypeLeads { get; set; }

    public List<TbCatalogItem>? ObjListFrecuencyContactId { get; set; }

    public List<TbCatalogItem>? ObjListSituationId { get; set; }

    public List<TbCatalogItem>? ObjListTypeId { get; set; }

    public TbCurrency? ObjCurrency { get; set; }

    public List<TbCompanyCurrencyDto>? ObjListCurrency { get; set; }

    public List<TbCatalogItem>? ObjListTypeFirmId { get; set; }

    public List<TbCatalogItem>? ObjListProfesionId { get; set; }

    public List<TbCatalogItem>? ObjListEstadoCivilId { get; set; }

    public List<TbCatalogItem>? ObjListFormContactId { get; set; }

    public List<TbCatalogItem>? ObjListSexoId { get; set; }

    public List<TbCatalogItem>? ObjListPayConditionId { get; set; }

    public List<TbCatalogItem>? ObjListTypePay { get; set; }

    public List<TbCatalogItem>? ObjListSubCategoryId { get; set; }

    public List<TbCatalogItem>? ObjListCategoryId { get; set; }

    public List<TbCatalogItem>? ObjListCustomerTypeId { get; set; }

    public List<TbCatalogItem>? ObjListClasificationId { get; set; }

    public List<TbCatalogItem>? ObjListCountry { get; set; }

    public List<TbCatalogItem>? ObjListIdentificationType { get; set; }

    public List<TbWorkflowStage>? ObjListWorkflowStage { get; set; }

    public BindingList<TbCustomerCreditLineDto> ObjListCustomerCreditLine { get; set; }

    public BindingList<TbEntityPhoneDto> ObjListPhone { get; set; }

    public BindingList<TbEntityEmail> ObjListEmail { get; set; }

    public BindingList<FormCustomerEditCustomerFrecuencyActuationDTO> ObjListCustomerFrecuencyActuations { get; set; }

    public string? ObjParameterMunicipio { get; set; }

    public string? ObjParameterDepartamento { get; set; }

    public string? ObjParameterPais { get; set; }

    public TbComponent? ObjComponentEmployer { get; set; }

    public TbComponent? ObjComponentAccount { get; set; }

    public TbComponent? ObjComponent { get; set; }

    public TbCustomerPaymentMethod? ObjPaymentMethod { get; set; }

    public List<TbCustomerConsultasSinRiesgo> ObjCustomerSinRiesgo { get; set; }

    public TbCustomerCredit? ObjCustomerCredit { get; set; }

    public TbLegal? ObjLegal { get; set; }

    public TbNaturale? ObjNatural { get; set; }

    public TbEntity? ObjEntity { get; set; }

    public TbLegal? ObjEmployerLegal { get; set; }

    public TbNaturale? ObjEmployerNatural { get; set; }

    public TbEmployeeDto? ObjEmployer { get; set; }

    public TbAccount? ObjAccount { get; set; }

    public TbEntityAccount ObjEntityAccount { get; set; }

    public List<TbCatalogItem>? ObjListCity { get; set; }

    public List<TbCatalogItem>? ObjListState { get; set; }

    #endregion

    #region Librerias

    private readonly ICoreWebAuditoria _objInterfazCoreWebAuditoria = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebAuditoria>();
    private readonly ICoreWebCatalog _objInterfazCoreWebCatalog = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCatalog>();
    private readonly ICoreWebParameter _objInterfazCoreWebParameter = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebParameter>();
    private readonly ICoreWebTransaction _objInterfazCoreWebTransaction = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTransaction>();
    private readonly ICoreWebCurrency _objInterfazCoreWebCurrency = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCurrency>();
    private readonly ICoreWebTools _objInterfazCoreWebTools = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();
    private readonly ICoreWebCounter _objInterfazCoreWebCounter = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCounter>();
    private readonly ICoreWebWorkflow _objInterfazCoreWebWorkflow = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebWorkflow>();
    private readonly CoreWebRenderInView _objInterfazCoreWebRenderInView = new CoreWebRenderInView();
    private readonly ICoreWebPermission _objInterfazCoreWebPermission = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebPermission>();
    private readonly ICompanyCurrencyModel _companyCurrencyModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyCurrencyModel>();
    private readonly IPublicCatalogModel _publicCatalogModel = VariablesGlobales.Instance.UnityContainer.Resolve<IPublicCatalogModel>();
    private readonly IPublicCatalogDetailModel _publicCatalogDetailModel = VariablesGlobales.Instance.UnityContainer.Resolve<IPublicCatalogDetailModel>();
    private readonly IEntityModel _entityModel = VariablesGlobales.Instance.UnityContainer.Resolve<IEntityModel>();
    private readonly IEmployeeModel _employeeModel = VariablesGlobales.Instance.UnityContainer.Resolve<IEmployeeModel>();
    private readonly IAccountModel _accountModel = VariablesGlobales.Instance.UnityContainer.Resolve<IAccountModel>();
    private readonly IEntityAccountModel _entityAccountModel = VariablesGlobales.Instance.UnityContainer.Resolve<IEntityAccountModel>();
    private readonly IEntityEmailModel _entityEmailModel = VariablesGlobales.Instance.UnityContainer.Resolve<IEntityEmailModel>();
    private readonly IEntityPhoneModel _entityPhoneModel = VariablesGlobales.Instance.UnityContainer.Resolve<IEntityPhoneModel>();
    private readonly INaturalModel _naturalModel = VariablesGlobales.Instance.UnityContainer.Resolve<INaturalModel>();
    private readonly ILegalModel _legalModel = VariablesGlobales.Instance.UnityContainer.Resolve<ILegalModel>();
    private readonly ICustomerCreditLineModel _customerCreditLineModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerCreditLineModel>();
    private readonly ICustomerConsultasSinRiesgoModel _customerConsultasSinRiesgoModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerConsultasSinRiesgoModel>();
    private readonly ICustomerPaymentMethodModel _customerPaymentMethod = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerPaymentMethodModel>();
    private readonly ICustomerFrecuencyActuationsModel _customerFrecuencyActuationsModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerFrecuencyActuationsModel>();
    private readonly ICustomerModel _customerModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerModel>();
    private readonly ICustomerCreditModel _customerCreditModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerCreditModel>();
    private readonly IBiometricUserModel _biometricUserModel = v4posme_library_biometric.Libraries.VariablesGlobales.Instance.UnityContainer.Resolve<IBiometricUserModel>();

    #endregion

    #region Init

    public FormCustomerEdit()
    {
        InitializeComponent();
    }

    public FormCustomerEdit(TypeRender typeRender, int entityId)
    {
        InitializeComponent();
        _typeRender = typeRender;
        _entityId = entityId;
        btnRegresar.Click += CommandRegresar;
        btnGuardar.Click += CommandSave;
        btnEliminar.Click += BtnEliminarOnClick;
        btnNuevo.Click += CommandNew;
    }

    public void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
    {
    }

    public void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
    }

    private void FormCustomerEdit_Load(object sender, EventArgs e)
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
                case TypeRender.Edit:
                    LoadEdit();
                    break;
                case TypeRender.New:
                    LoadNew();
                    break;
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
                PreRender();
                LoadRender(_typeRender);
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

        ObjComponent = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_customer");
        if (ObjComponent is null)
        {
            throw new Exception("EL COMPONENTE 'tb_customer' NO EXISTE...");
        }

        if (_entityId == 0)
        {
            throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_PARAMETER"]);
        }

        var appCustomer01 = VariablesGlobales.ConfigurationBuilder["APP_CUSTOMER01"];
        var appCustomer02 = VariablesGlobales.ConfigurationBuilder["APP_CUSTOMER02"];
        if (_entityId == Convert.ToInt32(appCustomer01))
        {
            throw new Exception("No es posible eliminar el cliente, edite el nombre");
        }

        if (_entityId == Convert.ToInt32(appCustomer02))
        {
            throw new Exception("No es posible eliminar el cliente, edite el nombre");
        }

        ObjCustomer = _customerModel.GetRowByPk(user.CompanyID, user.BranchID, _entityId);
        //PERMISO SOBRE EL REGISTRO
        var permissionMe = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_ME"]);
        if (resultPermission == permissionMe && ObjCustomer.CreatedBy!.Value != user.UserID)
        {
            throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_DELETE"]);
        }

        //PERMISO PUEDE ELIMINAR EL REGISTRO SEGUN EL WORKFLOW
        var commandEliminable = VariablesGlobales.ConfigurationBuilder["COMMAND_ELIMINABLE"];
        if (!_objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_customer", "statusID", ObjCustomer.StatusID!.Value, Convert.ToInt32(commandEliminable), user.CompanyID, user.BranchID, role.RoleID)!.Value)
        {
            throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_WORKFLOW_DELETE"]);
        }

        _customerModel.DeleteAppPosme(user.CompanyID, user.BranchID, _entityId);
    }

    public void ComandPrinter()
    {
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
            var permited = _objInterfazCoreWebPermission.UrlPermited("app_cxc_customer", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (!permited)
            {
                throw new Exception(notAccessControl);
            }

            var resultPermission = _objInterfazCoreWebPermission.UrlPermissionCmd("app_cxc_customer", "add", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (resultPermission == permissionNone)
            {
                throw new Exception(notAllEdit);
            }
        }

        ObjComponent = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_customer");
        if (ObjComponent is null)
        {
            throw new Exception("EL COMPONENTE 'tb_customer' NO EXISTE...");
        }

        ObjComponentAccount = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_account");
        if (ObjComponentAccount is null)
        {
            throw new Exception("EL COMPONENTE 'tb_account' NO EXISTE...");
        }

        ObjComponentEmployer = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_employee");
        if (ObjComponentEmployer is null)
        {
            throw new Exception("EL COMPONENTE 'tb_employee' NO EXISTE...");
        }

        //Obtener el Registro
        ObjCustomer = _customerModel.GetRowByPk(user.CompanyID, user.BranchID, _entityId);
        if (ObjCustomer is null)
        {
            throw new Exception("No existe el cliente con el Id indicado");
        }

        _txtEmployerId = ObjCustomer.EntityContactID ?? 0;
        ObjEntity = _entityModel.GetRowByPk(user.CompanyID, user.BranchID, _entityId);
        ObjNatural = _naturalModel.GetRowByPk(user.CompanyID, user.BranchID, _entityId);
        ObjLegal = _legalModel.GetRowByPk(user.CompanyID, user.BranchID, _entityId);
        var objEntitylistEmail = _entityEmailModel.GetRowByEntity(user.CompanyID, user.BranchID, _entityId);
        var objEntityListPhone = _entityPhoneModel.GetRowByEntity(user.CompanyID, user.BranchID, _entityId);
        ObjCustomerCredit = _customerCreditModel.GetRowByPk(user.CompanyID, user.BranchID, _entityId);
        var objCustomerCreditLine = _customerCreditLineModel.GetRowByEntity(user.CompanyID, user.BranchID, _entityId);
        ObjCustomerSinRiesgo = _customerConsultasSinRiesgoModel.GetRowByCedulaFileName(user.CompanyID, ObjCustomer.Identification.Replace("-", ""));
        ObjPaymentMethod = _customerPaymentMethod.GetRowByEntity(user.CompanyID, _entityId);
        var objCustomerFrecuency = _customerFrecuencyActuationsModel.GetrowByEntityId(_entityId);

        var parametroPaisDefault = _objInterfazCoreWebParameter.GetParameter("CXC_PAIS_DEFAULT", user.CompanyID);
        if (parametroPaisDefault is null)
        {
            throw new Exception("Configure el parametro del pais por defualt...");
        }

        ObjParameterPais = parametroPaisDefault.Value;

        var parametroDepartamentoDefault = _objInterfazCoreWebParameter.GetParameter("CXC_DEPARTAMENTO_DEFAULT", user.CompanyID);
        if (parametroDepartamentoDefault is null)
        {
            throw new Exception("Configure el parametro del departamento por defualt...");
        }

        ObjParameterDepartamento = parametroDepartamentoDefault.Value;
        var parametroMunicipioDefault = _objInterfazCoreWebParameter.GetParameter("CXC_MUNICIPIO_DEFAULT", user.CompanyID);
        if (parametroMunicipioDefault is null)
        {
            throw new Exception("Configure el parametro del municipio por defualt...");
        }

        var objFirstEntityAccount = _entityAccountModel.GetRowByEntity(user.CompanyID, ObjComponent.ComponentID, _entityId).First();
        ObjAccount = _accountModel.GetRowByPk(user.CompanyID, objFirstEntityAccount.AccountID!.Value);

        ObjParameterMunicipio = parametroMunicipioDefault.Value;
        ObjListWorkflowStage = _objInterfazCoreWebWorkflow.GetWorkflowStageByStageInit("tb_customer", "statusID", ObjCustomer.StatusID!.Value, user.CompanyID, user.BranchID, role.RoleID);
        ObjListCurrency = _companyCurrencyModel.GetByCompany(user.CompanyID);
        ObjCurrency = _objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyID);
        ObjListIdentificationType = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer", "identificationType", user.CompanyID);
        ObjListCountry = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer", "countryID", user.CompanyID);
        ObjListState = _objInterfazCoreWebCatalog.GetCatalogAllItemParent("tb_customer", "stateID", user.CompanyID, ObjCustomer.CountryID!.Value);
        ObjListCity = _objInterfazCoreWebCatalog.GetCatalogAllItemParent("tb_customer", "cityID", user.CompanyID, ObjCustomer.CityID!.Value);
        ObjListClasificationId = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer", "clasificationID", user.CompanyID);
        ObjListCustomerTypeId = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer", "customerTypeID", user.CompanyID);
        ObjListCategoryId = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer", "categoryID", user.CompanyID);
        ObjListSubCategoryId = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer", "subCategoryID", user.CompanyID);
        ObjListTypePay = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer", "typePay", user.CompanyID);
        ObjListPayConditionId = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer", "payConditionID", user.CompanyID);
        ObjListSexoId = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer", "sexoID", user.CompanyID);
        ObjListFormContactId = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer", "formContactID", user.CompanyID);
        ObjListEstadoCivilId = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_naturales", "statusID", user.CompanyID);
        ObjListProfesionId = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_naturales", "profesionID", user.CompanyID);
        ObjListTypeFirmId = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer", "typeFirm", user.CompanyID);
        ObjListTypeId = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer_payment_method", "typeID", user.CompanyID);
        ObjListSituationId = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer_frecuency_actuations", "situationID", user.CompanyID);
        ObjListFrecuencyContactId = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer_frecuency_actuations", "frecuencyContactID", user.CompanyID);

        ObjEntityAccount = objFirstEntityAccount;
        ObjEmployer = _employeeModel.GetRowByEntityId(user.CompanyID, ObjCustomer.EntityContactID!.Value);
        var entityEmployeerId = ObjEmployer?.EntityId ?? 0;
        ObjEmployerNatural = _naturalModel.GetRowByPk(user.CompanyID, user.BranchID, entityEmployeerId);
        ObjEmployerLegal = _legalModel.GetRowByPk(user.CompanyID, user.BranchID, entityEmployeerId);

        var objPCatalogTypeLeads = _publicCatalogModel.GetBySystemNameAndFlavorID("tb_customer.typeLeads", company.FlavorID).FirstOrDefault(catalog => catalog.IsActive!.Value);
        ObjPCItemTypeLeads = _publicCatalogDetailModel.GetView(objPCatalogTypeLeads?.PublicCatalogID ?? 0);
        var objPCatalogSubTypeLeads = _publicCatalogModel.GetBySystemNameAndFlavorID("tb_customer.subTypeLeads", company.FlavorID).FirstOrDefault(catalog => catalog.IsActive!.Value);
        ObjPCItemSubTypeLeads = _publicCatalogDetailModel.GetView(objPCatalogSubTypeLeads?.PublicCatalogID ?? 0);
        var objPCatalogCategoryLeads = _publicCatalogModel.GetBySystemNameAndFlavorID("tb_customer.categoryLeads", company.FlavorID).FirstOrDefault(catalog => catalog.IsActive!.Value);
        ObjPCItemCategoryLeads = _publicCatalogDetailModel.GetView(objPCatalogCategoryLeads?.PublicCatalogID ?? 0);

        FnObjListCustomerCreditLine(objCustomerCreditLine);
        FnObjListPhone(objEntityListPhone);
        FnObjListEmail(objEntitylistEmail);
        FonObjListCustomerFrecuencyActuations(objCustomerFrecuency);
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
            var permited = _objInterfazCoreWebPermission.UrlPermited("app_cxc_customer", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (!permited)
            {
                throw new Exception(notAccessControl);
            }

            var resultPermission = _objInterfazCoreWebPermission.UrlPermissionCmd("app_cxc_customer", "add", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (resultPermission == permissionNone)
            {
                throw new Exception(notAllEdit);
            }
        }

        ObjComponent = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_customer");
        if (ObjComponent is null)
        {
            throw new Exception("EL COMPONENTE 'tb_customer' NO EXISTE...");
        }

        ObjComponentAccount = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_account");
        if (ObjComponentAccount is null)
        {
            throw new Exception("EL COMPONENTE 'tb_account' NO EXISTE...");
        }

        ObjComponentEmployer = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_employee");
        if (ObjComponentEmployer is null)
        {
            throw new Exception("EL COMPONENTE 'tb_employee' NO EXISTE...");
        }

        var parametroPaisDefault = _objInterfazCoreWebParameter.GetParameter("CXC_PAIS_DEFAULT", user.CompanyID);
        if (parametroPaisDefault is null)
        {
            throw new Exception("Configure el parametro del pais por defualt...");
        }

        ObjParameterPais = parametroPaisDefault.Value;

        var parametroDepartamentoDefault = _objInterfazCoreWebParameter.GetParameter("CXC_DEPARTAMENTO_DEFAULT", user.CompanyID);
        if (parametroDepartamentoDefault is null)
        {
            throw new Exception("Configure el parametro del departamento por defualt...");
        }

        ObjParameterDepartamento = parametroDepartamentoDefault.Value;
        var parametroMunicipioDefault = _objInterfazCoreWebParameter.GetParameter("CXC_MUNICIPIO_DEFAULT", user.CompanyID);
        if (parametroMunicipioDefault is null)
        {
            throw new Exception("Configure el parametro del municipio por defualt...");
        }

        ObjParameterMunicipio = parametroMunicipioDefault.Value;

        ObjListWorkflowStage = _objInterfazCoreWebWorkflow.GetWorkflowInitStage("tb_customer", "statusID", user.CompanyID, user.BranchID, role.RoleID);
        ObjListIdentificationType = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer", "identificationType", user.CompanyID);
        ObjListCountry = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer", "countryID", user.CompanyID);
        ObjListClasificationId = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer", "clasificationID", user.CompanyID);
        ObjListCustomerTypeId = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer", "customerTypeID", user.CompanyID);
        ObjListCategoryId = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer", "categoryID", user.CompanyID);
        ObjListSubCategoryId = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer", "subCategoryID", user.CompanyID);
        ObjListTypePay = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer", "typePay", user.CompanyID);
        ObjListPayConditionId = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer", "payConditionID", user.CompanyID);
        ObjListSexoId = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer", "sexoID", user.CompanyID);
        ObjListFormContactId = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer", "formContactID", user.CompanyID);
        ObjListEstadoCivilId = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_naturales", "statusID", user.CompanyID);
        ObjListProfesionId = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_naturales", "profesionID", user.CompanyID);
        ObjListTypeFirmId = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer", "typeFirm", user.CompanyID);
        ObjListTypeId = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer_payment_method", "typeID", user.CompanyID);
        ObjListSituationId = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer_frecuency_actuations", "situationID", user.CompanyID);
        ObjListFrecuencyContactId = _objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer_frecuency_actuations", "frecuencyContactID", user.CompanyID);
        ObjListCurrency = _companyCurrencyModel.GetByCompany(user.CompanyID);
        ObjCurrency = _objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyID);

        var objPCatalogTypeLeads = _publicCatalogModel.GetBySystemNameAndFlavorID("tb_customer.typeLeads", company.FlavorID).FirstOrDefault(catalog => catalog.IsActive!.Value);
        ObjPCItemTypeLeads = _publicCatalogDetailModel.GetView(objPCatalogTypeLeads?.PublicCatalogID ?? 0);
        var objPCatalogSubTypeLeads = _publicCatalogModel.GetBySystemNameAndFlavorID("tb_customer.subTypeLeads", company.FlavorID).FirstOrDefault(catalog => catalog.IsActive!.Value);
        ObjPCItemSubTypeLeads = _publicCatalogDetailModel.GetView(objPCatalogSubTypeLeads?.PublicCatalogID ?? 0);
        var objPCatalogCategoryLeads = _publicCatalogModel.GetBySystemNameAndFlavorID("tb_customer.categoryLeads", company.FlavorID).FirstOrDefault(catalog => catalog.IsActive!.Value);
        ObjPCItemCategoryLeads = _publicCatalogDetailModel.GetView(objPCatalogCategoryLeads?.PublicCatalogID ?? 0);

        FnObjListCustomerCreditLine(new List<TbCustomerCreditLineDto>());
        FnObjListPhone(new List<TbEntityPhoneDto>());
        FnObjListEmail(new List<TbEntityEmail>());
        FonObjListCustomerFrecuencyActuations(new List<TbCustomerFrecuencyActuation>());
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
            var permited = _objInterfazCoreWebPermission.UrlPermited("app_cxc_customer", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (!permited)
            {
                throw new Exception(notAccessControl);
            }

            var resultPermission = _objInterfazCoreWebPermission.UrlPermissionCmd("app_cxc_customer", "add", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (resultPermission == permissionNone)
            {
                throw new Exception(notAllEdit);
            }
        }

        using var dbContextTransaction = VariablesGlobales.Instance.DataContext.Database.BeginTransaction();

        ObjComponent = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_customer");
        if (ObjComponent is null)
        {
            throw new Exception("EL COMPONENTE 'tb_account' NO EXISTE...");
        }

        var objEntity = new TbEntity()
        {
            CompanyID = user.CompanyID,
            BranchID = user.BranchID
        };


        _objInterfazCoreWebPermission.GetValueLicense(user.CompanyID, "app_cxc_customer/index");

        var objCurrencyDolares = _objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyID);
        var dateOn = DateTime.Now.Date;
        var exchangeRate = decimal.Zero;
        var exchangeRateTotal = decimal.Zero;
        var exchangeRateAmount = decimal.Zero;

        _objInterfazCoreWebAuditoria.SetAuditCreated(objEntity, user, "");
        var entityId = _entityModel.InsertAppPosme(objEntity);

        var status = txtCivilStatusID.SelectedItem as ComboBoxItem;
        var profesion = txtProfesionID.SelectedItem as ComboBoxItem;
        var objNatural = new TbNaturale
        {
            EntityID = entityId,
            CompanyID = objEntity.CompanyID,
            BranchID = objEntity.BranchID,
            IsActive = true,
            FirstName = txtFirstName.Text,
            LastName = txtLastName.Text,
            Address = txtAddress.Text,
            StatusID = Convert.ToInt32(status.Key),
            ProfesionID = Convert.ToInt32(profesion.Key)
        };
        var naturalId = _naturalModel.InsertAppPosme(objNatural);

        var objLegal = new TbLegal
        {
            CompanyID = objEntity.CompanyID,
            BranchID = objEntity.BranchID,
            EntityID = entityId,
            IsActive = true,
            ComercialName = txtCommercialName.Text,
            LegalName = txtLegalName.Text,
            Address = txtAddress.Text
        };
        var legalId = _legalModel.InsertAppPosme(objLegal);

        var tipoTarjeta = txtTipoTarjeta.SelectedItem as ComboBoxItem;
        if (txtDatosTarjeta.Card is not null)
        {
            var objPaymentMethod = new TbCustomerPaymentMethod
            {
                EntityID = entityId,
                StatusID = 1,
                IsActive = true,
                Name = string.IsNullOrWhiteSpace(txtNombreTarjeta.Text) ? "" : txtNombreTarjeta.Text,
                Number = txtDatosTarjeta.Card.Number,
                Email = string.IsNullOrWhiteSpace(txtEmailTarjeta.Text) ? "" : txtEmailTarjeta.Text,
                ExpirationDate = txtDatosTarjeta.Card.ExpirationDate.ToShortDateString(),
                Cvc = txtDatosTarjeta.Card.CVC,
                TypeId = Convert.ToInt32(tipoTarjeta!.Key),
            };
            _customerPaymentMethod.InsertAppPosme(objPaymentMethod);
        }
        else
        {
            var objPaymentMethod = new TbCustomerPaymentMethod
            {
                EntityID = entityId,
                StatusID = 1,
                IsActive = true,
                Name = "",
                Number = "",
                Email = "",
                ExpirationDate = "",
                Cvc = "",
                TypeId = Convert.ToInt32(tipoTarjeta!.Key),
            };
            _customerPaymentMethod.InsertAppPosme(objPaymentMethod);
        }


        if (ObjListCustomerFrecuencyActuations.Count > 0)
        {
            foreach (var actuationDto in ObjListCustomerFrecuencyActuations)
            {
                var objFrecuencyActuations = new TbCustomerFrecuencyActuation
                {
                    EntityID = entityId,
                    CreatedOn = DateTime.Now,
                    Name = actuationDto.Name,
                    SituationID = actuationDto.SituationID,
                    FrecuencyContactID = actuationDto.FrecuencyContactID,
                    IsActive = 1,
                    IsApply = 0
                };
                _customerFrecuencyActuationsModel.InsertAppPosme(objFrecuencyActuations);
            }
        }

        var paisDefault = _objInterfazCoreWebParameter.GetParameterValue("CXC_PAIS_DEFAULT", user.CompanyID);
        var departamentoDefault = _objInterfazCoreWebParameter.GetParameterValue("CXC_DEPARTAMENTO_DEFAULT", user.CompanyID);
        var municipioDefault = _objInterfazCoreWebParameter.GetParameterValue("CXC_MUNICIPIO_DEFAULT", user.CompanyID);
        var plazoDefault = _objInterfazCoreWebParameter.GetParameterValue("CXC_PLAZO_DEFAULT", user.CompanyID);
        var typeAmortizationDefault = _objInterfazCoreWebParameter.GetParameterValue("CXC_TYPE_AMORTIZATION", user.CompanyID);
        var dayExcludedDefault = _objInterfazCoreWebParameter.GetParameterValue("CXC_DAY_EXCLUDED_IN_CREDIT", user.CompanyID);
        var frecuencyDefault = _objInterfazCoreWebParameter.GetParameterValue("CXC_FRECUENCIA_PAY_DEFAULT", user.CompanyID);
        var creditLineDefault = _objInterfazCoreWebParameter.GetParameterValue("CXC_CREDIT_LINE_DEFAULT", user.CompanyID);
        var validarCedula = _objInterfazCoreWebParameter.GetParameterValue("CXC_VALIDAR_CEDULA_REPETIDA", user.CompanyID);
        var interesDefault = _objInterfazCoreWebParameter.GetParameterValue("CXC_INTERES_DEFAULT", user.CompanyID);

        var selectedCountry = txtCountryID.SelectedItem as ComboBoxItem;
        var paisId = Convert.ToInt32(selectedCountry is null ? paisDefault : selectedCountry.Key);

        var selectedSate = txtStateID.SelectedItem as ComboBoxItem;
        var departamentoId = Convert.ToInt32(selectedSate is null ? departamentoDefault : selectedSate.Key);

        var selectedMunicipio = txtCityID.SelectedItem as ComboBoxItem;
        var municipioId = Convert.ToInt32(selectedMunicipio is null ? municipioDefault : selectedMunicipio.Key);

        //validar que se permita la omision de la cedula
        if (string.Compare(validarCedula, "true", StringComparison.CurrentCultureIgnoreCase) == 0)
        {
            //Validar que ya existe el cliente
            var objCustomerOld = _customerModel.GetRowByIdentification(user.CompanyID, txtIdentification.Text);
            if (objCustomerOld is not null)
            {
                throw new Exception("Error identificacion del cliente ya existe.");
            }
        }

        var selectedIdentificationTypeId = txtIdentificationTypeID.SelectedItem as ComboBoxItem;
        var selectedCurrencyId = txtCurrencyID.SelectedItem as ComboBoxItem;
        var selectedClasificacionId = txtClasificationID.SelectedItem as ComboBoxItem;
        var selectedCateogoryId = txtCategoryID.SelectedItem as ComboBoxItem;
        var selectedSubCateogoryId = txtSubCategoryID.SelectedItem as ComboBoxItem;
        var selectedCustomerTypeId = txtCustomerTypeID.SelectedItem as ComboBoxItem;
        var selectedStatusId = txtStatusID.SelectedItem as ComboBoxItem;
        var selectedPayConditionId = txtPayConditionID.SelectedItem as ComboBoxItem;
        var selectedSexoId = txtSexoID.SelectedItem as ComboBoxItem;
        var selectedTypeFirmId = txtTypeFirmID.SelectedItem as ComboBoxItem;
        var selectedTypeId = txtTypePayID.SelectedItem as ComboBoxItem;
        var budget = decimal.Zero;
        if (!string.IsNullOrWhiteSpace(txtBudget.Text))
        {
            budget = decimal.Parse(txtBudget.Text);
        }

        var balancePoint = decimal.Zero;
        if (!string.IsNullOrWhiteSpace(txtBalancePoint.Text))
        {
            balancePoint = decimal.Parse(txtBalancePoint.Text);
        }

        ObjCustomer = new TbCustomer
        {
            CompanyID = objEntity.CompanyID,
            BranchID = objEntity.BranchID,
            EntityID = entityId,
            CustomerNumber = _objInterfazCoreWebCounter.GoNextNumber(user.CompanyID, user.BranchID, "tb_customer", 0),
            IdentificationType = Convert.ToInt32(selectedIdentificationTypeId.Key),
            Identification = txtIdentification.Text,
            CountryID = paisId,
            StateID = departamentoId,
            CityID = municipioId,
            Location = txtLocation.Text,
            Address = txtAddress.Text,
            CurrencyID = Convert.ToInt32(selectedCurrencyId.Key),
            ClasificationID = Convert.ToInt32(selectedClasificacionId.Key),
            CategoryID = Convert.ToInt32(selectedCateogoryId.Key),
            SubCategoryID = Convert.ToInt32(selectedSubCateogoryId.Key),
            CustomerTypeID = Convert.ToInt32(selectedCustomerTypeId.Key),
            BirthDate = txtBirthDate.DateTime.Date,
            DateContract = dateOn,
            StatusID = Convert.ToInt32(selectedStatusId.Key),
            TypePay = Convert.ToInt32(selectedTypeId.Key),
            PayConditionID = Convert.ToInt32(selectedPayConditionId.Key),
            SexoID = Convert.ToInt32(selectedSexoId.Key),
            Reference1 = txtReference1.Text,
            Reference2 = txtReference2.Text,
            Reference3 = txtReference3.Text,
            Reference4 = txtReference4.Text,
            Reference5 = txtReference5.Text,
            BalancePoint = balancePoint,
            PhoneNumber = Encoding.UTF8.GetBytes(txtPhoneNumber.Text),
            TypeFirm = Convert.ToInt32(selectedTypeFirmId.Key),
            Budget = budget,
            IsActive = true,
            EntityContactID = _txtEmployerId,
            FormContactID = Convert.ToInt32(_objInterfazCoreWebParameter.GetParameterValue("CXC_FORM_CONTACT_ID_DEFAULT", user.CompanyID))
        };
        _objInterfazCoreWebAuditoria.SetAuditCreated(ObjCustomer, user, "");
        _customerModel.InsertAppPosme(ObjCustomer);

        var validateBiometric = _objInterfazCoreWebParameter.GetParameterValue("CXC_USE_BIOMETRIC", user.CompanyID);
        if (string.Compare(validateBiometric, "true", StringComparison.InvariantCultureIgnoreCase) == 0)
        {
            //Ingresar registro en el lector biometrico		
            var userBiometric = new v4posme_library_biometric.Models.User
            {
                Id = entityId,
                Name = "buscar en otra base",
                Email = "buscar en otra base",
                EmailVerifiedAt = null,
                CreatedAt = null,
                UpdatedAt = null,
                RememberToken = "buscar en otra base",
                Image = ""
            };
            _biometricUserModel.DeleteAppPosme(entityId);
            _biometricUserModel.InsertAppPosme(userBiometric);
        }

        //Ingresar Cuenta

        var objEntityAccount = new TbEntityAccount
        {
            CompanyID = objEntity.CompanyID,
            ComponentID = ObjComponent.ComponentID,
            ComponentItemID = entityId,
            Name = string.Empty,
            Description = string.Empty,
            AccountTypeID = 0,
            CurrencyID = 0,
            ClassID = 0,
            Balance = 0,
            CreditLimit = 0,
            MaxCredit = 0,
            DebitLimit = 0,
            MaxDebit = 0,
            StatusID = 0,
            AccountID = _txtAccountId,
            IsActive = true
        };
        _objInterfazCoreWebAuditoria.SetAuditCreated(objEntityAccount, user, "");
        _entityAccountModel.InsertAppPosme(objEntityAccount);

        var objCustomerCredit = new TbCustomerCredit
        {
            CompanyID = objEntity.CompanyID,
            BranchID = objEntity.BranchID,
            EntityID = entityId,
            LimitCreditDol = decimal.Parse(txtLimitCreditDol.Text),
            BalanceDol = decimal.Parse(txtLimitCreditDol.Text),
            IncomeDol = decimal.Parse(txtIncomeDol.Text)
        };
        _customerCreditModel.InsertAppPosme(objCustomerCredit);
        if (ObjListEmail.Count > 0)
        {
            foreach (var tbEntityEmail in ObjListEmail)
            {
                var objEntityEmail = new TbEntityEmail
                {
                    CompanyID = objEntity.CompanyID,
                    BranchID = objEntity.BranchID,
                    EntityID = entityId,
                    Email = tbEntityEmail.Email,
                    IsPrimary = tbEntityEmail.IsPrimary
                };
                _entityEmailModel.InsertAppPosme(objEntityEmail);
            }
        }

        if (ObjListPhone.Count > 0)
        {
            foreach (var tbEntityPhoneDto in ObjListPhone)
            {
                var objEntityPhone = new TbEntityPhone
                {
                    CompanyID = objEntity.CompanyID,
                    BranchID = objEntity.BranchID,
                    EntityID = entityId,
                    TypeID = tbEntityPhoneDto.TypeId,
                    Number = tbEntityPhoneDto.Number,
                    IsPrimary = tbEntityPhoneDto.IsPrimary
                };
                _entityPhoneModel.InsertAppPosme(objEntityPhone);
            }
        }

        var limitCreditLine = decimal.Zero;
        if (ObjListCustomerCreditLine.Count > 0)
        {
            foreach (var tbCustomerCreditLineDto in ObjListCustomerCreditLine)
            {
                var objCustomerCreditLine = new TbCustomerCreditLine
                {
                    CompanyID = objEntity.CompanyID,
                    BranchID = objEntity.BranchID,
                    EntityID = entityId,
                    CreditLineID = 1,
                    AccountNumber = _objInterfazCoreWebCounter.GoNextNumber(user.CompanyID, user.BranchID, "tb_customer_credit_line", 0)!,
                    CurrencyID = tbCustomerCreditLineDto.CurrencyId,
                    TypeAmortization = tbCustomerCreditLineDto.TypeAmortization,
                    LimitCredit = tbCustomerCreditLineDto.LimitCredit!.Value,
                    Balance = tbCustomerCreditLineDto.Balance!.Value,
                    InterestYear = tbCustomerCreditLineDto.InterestYear!.Value,
                    InterestPay = tbCustomerCreditLineDto.InterestPay!.Value,
                    TotalPay = tbCustomerCreditLineDto.TotalPay!.Value,
                    TotalDefeated = tbCustomerCreditLineDto.TotalDefeated!.Value,
                    DayExcluded = tbCustomerCreditLineDto.DayExcluded,
                    DateOpen = DateTime.Today,
                    PeriodPay = tbCustomerCreditLineDto.PeriodPay,
                    DateLastPay = DateTime.Today,
                    Term = tbCustomerCreditLineDto.Term,
                    Note = tbCustomerCreditLineDto.Note,
                    StatusID = tbCustomerCreditLineDto.StatusId,
                    IsActive = true
                };
                limitCreditLine = decimal.Add(limitCreditLine, objCustomerCreditLine.LimitCredit);
                exchangeRate = _objInterfazCoreWebCurrency.GetRatio(user.CompanyID, dateOn, 1, objCustomerCreditLine.CurrencyID, objCurrencyDolares.CurrencyID);
                exchangeRate = objCustomerCreditLine.LimitCredit;
                _customerCreditLineModel.InsertAppPosme(objCustomerCreditLine);

                if (exchangeRate.CompareTo(decimal.One) == 1)
                {
                    //sumar los limites en dolares
                    exchangeRate = decimal.Add(exchangeRate, exchangeRateAmount);
                }
                else
                {
                    //sumar los limite en cordoba
                    exchangeRate = decimal.Add(exchangeRateTotal, decimal.Divide(exchangeRateAmount, exchangeRate));
                }
            }
        }
        else
        {
            var workflowInitStage = _objInterfazCoreWebWorkflow.GetWorkflowInitStage("tb_customer_credit_line", "statusID", user.CompanyID, user.BranchID, role.RoleID);
            var objCustomerCreditLine = new TbCustomerCreditLine
            {
                CompanyID = objEntity.CompanyID,
                BranchID = objEntity.BranchID,
                EntityID = entityId,
                CreditLineID = Convert.ToInt32(creditLineDefault),
                AccountNumber = _objInterfazCoreWebCounter.GoNextNumber(user.CompanyID, user.BranchID, "tb_customer_credit_line", 0)!,
                CurrencyID = ObjCurrency!.CurrencyID,
                TypeAmortization = Convert.ToInt32(typeAmortizationDefault),
                LimitCredit = 300000,
                Balance = 300000,
                InterestYear = decimal.Parse(interesDefault),
                InterestPay = 0,
                TotalPay = 0,
                TotalDefeated = 0,
                DateOpen = DateTime.Today,
                PeriodPay = Convert.ToInt32(frecuencyDefault),
                DateLastPay = DateTime.Today,
                Term = Convert.ToInt32(plazoDefault),
                Note = "-",
                StatusID = workflowInitStage!.ElementAt(0).WorkflowStageID,
                IsActive = true,
                DayExcluded = Convert.ToInt32(dayExcludedDefault)
            };
            _customerCreditLineModel.InsertAppPosme(objCustomerCreditLine);
        }

        //Validar Limite de Credito
        if (exchangeRateTotal.CompareTo(objCustomerCredit.LimitCreditDol) > 0)
        {
            throw new Exception("LINEAS DE CREDITOS MAL CONFIGURADAS LÍMITE EXCEDIDO");
        }

        var pathFileOfApp = VariablesGlobales.ConfigurationBuilder["PATH_FILE_OF_APP"];
        var ruta = Path.Combine(pathFileOfApp!, $"company_{objEntity.CompanyID}", $"component_{ObjComponent.ComponentID}", $"component_item_{entityId}");
        if (!Directory.Exists(ruta))
        {
            Directory.CreateDirectory(ruta);
        }

        dbContextTransaction.Commit();
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

        int resultPermission = 0;
        if (appNeedAuthentication == "true")
        {
            var permited = _objInterfazCoreWebPermission.UrlPermited("app_cxc_customer", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (!permited)
            {
                throw new Exception(notAccessControl);
            }

            resultPermission = _objInterfazCoreWebPermission.UrlPermissionCmd("app_cxc_customer", "edit", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (resultPermission == permissionNone)
            {
                throw new Exception(notAllEdit);
            }
        }

        ObjComponent = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_customer");
        if (ObjComponent is null)
        {
            throw new Exception("EL COMPONENTE 'tb_account' NO EXISTE...");
        }

        var objCurrencyDolares = _objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyID);
        var dateOn = DateTime.Now.Date;
        var exchangeRate = decimal.Zero;
        var exchangeRateTotal = decimal.Zero;
        var exchangeRateAmount = decimal.Zero;

        if (ObjCustomer is null)
        {
            throw new Exception("No existe el cliente");
        }

        //Validar Edicion por el Usuario
        var permissionMe = VariablesGlobales.ConfigurationBuilder["PERMISSION_ME"];
        if (resultPermission == Convert.ToInt32(permissionMe) && ObjCustomer.CreatedBy != user.UserID)
        {
            throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_EDIT"]);
        }

        //Validar si el estado permite editar
        var commandEditableTotal = VariablesGlobales.ConfigurationBuilder["COMMAND_EDITABLE_TOTAL"];
        var validateWorkflowStage = _objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_customer", "statusID", ObjCustomer.StatusID!.Value, Convert.ToInt32(commandEditableTotal), user.CompanyID, user.BranchID, role.RoleID);
        if (!validateWorkflowStage!.Value)
        {
            throw new Exception(VariablesGlobales.ConfigurationBuilder["NOT_WORKFLOW_EDIT"]);
        }

        var commandEditable = VariablesGlobales.ConfigurationBuilder["COMMAND_EDITABLE"];
        validateWorkflowStage = _objInterfazCoreWebWorkflow.ValidateWorkflowStage("tb_customer", "statusID", ObjCustomer.StatusID!.Value, Convert.ToInt32(commandEditable), user.CompanyID, user.BranchID, role.RoleID);
        //El Estado solo permite editar el workflow
        if (validateWorkflowStage!.Value)
        {
            var selectedSatatus = txtStatusID.SelectedItem as ComboBoxItem;
            if (selectedSatatus is null)
            {
                return;
            }

            ObjCustomer.StatusID = Convert.ToInt32(selectedSatatus.Key);
            _customerModel.UpdateAppPosme(user.CompanyID, user.BranchID, _entityId, ObjCustomer);
        }
        else
        {
            var selectedCivilStatusId = txtCivilStatusID.SelectedItem as ComboBoxItem;
            var selectedProfesionId = txtProfesionID.SelectedItem as ComboBoxItem;
            ObjNatural.IsActive = true;
            ObjNatural.FirstName = txtFirstName.Text;
            ObjNatural.LastName = txtLastName.Text;
            ObjNatural.Address = txtAddress.Text;
            ObjNatural.StatusID = Convert.ToInt32(selectedCivilStatusId!.Key);
            ObjNatural.ProfesionID = Convert.ToInt32(selectedProfesionId!.Key);
            _naturalModel.UpdateAppPosme(user.CompanyID, user.BranchID, _entityId, ObjNatural);

            if (ObjLegal is null)
            {
                throw new Exception("No existe el los datos de Legal");
            }

            ObjLegal.IsActive = true;
            ObjLegal.ComercialName = txtCommercialName.Text;
            ObjLegal.LegalName = txtLegalName.Text;
            ObjLegal.Address = txtAddress.Text;
            _legalModel.UpdateAppPosme(user.CompanyID, user.BranchID, _entityId, ObjLegal);

            var findPaymentMethod = _customerPaymentMethod.GetRowByEntity(user.CompanyID, _entityId);
            var tipoTarjeta = txtTipoTarjeta.SelectedItem as ComboBoxItem;
            var typeId = 0;
            if (tipoTarjeta is not null)
            {
                typeId = Convert.ToInt32(tipoTarjeta.Key);
            }

            if (findPaymentMethod is null)
            {
                if (txtDatosTarjeta.Card is null)
                {
                    findPaymentMethod = new TbCustomerPaymentMethod
                    {
                        EntityID = _entityId,
                        StatusID = 1,
                        IsActive = true,
                        Name = string.IsNullOrWhiteSpace(txtNombreTarjeta.Text) ? "" : txtNombreTarjeta.Text,
                        Number = "",
                        Email = "",
                        ExpirationDate = "",
                        Cvc = "",
                        TypeId = typeId
                    };
                    _customerPaymentMethod.InsertAppPosme(findPaymentMethod);
                }
                else
                {
                    findPaymentMethod = new TbCustomerPaymentMethod
                    {
                        EntityID = _entityId,
                        StatusID = 1,
                        IsActive = true,
                        Name = string.IsNullOrWhiteSpace(txtNombreTarjeta.Text) ? "" : txtNombreTarjeta.Text,
                        Number = txtDatosTarjeta.Card.Number,
                        Email = txtEmailTarjeta.Text,
                        ExpirationDate = txtDatosTarjeta.Card.ExpirationDate.ToShortDateString(),
                        Cvc = txtDatosTarjeta.Card.CVC,
                        TypeId = typeId
                    };
                    _customerPaymentMethod.InsertAppPosme(findPaymentMethod);
                }
            }
            else
            {
                if (txtDatosTarjeta.Card is not null)
                {
                    findPaymentMethod.Name = txtNombreTarjeta.Text;
                    findPaymentMethod.Number = txtDatosTarjeta.Card.Number;
                    findPaymentMethod.Email = txtEmailTarjeta.Text;
                    findPaymentMethod.ExpirationDate = txtDatosTarjeta.Card.ExpirationDate.ToShortDateString();
                    findPaymentMethod.Cvc = txtDatosTarjeta.Card.CVC;
                    findPaymentMethod.TypeId = typeId;
                    _customerPaymentMethod.UpdateAppPosme(_entityId, findPaymentMethod);
                }
            }

            var selectedIdentificationType = txtIdentificationTypeID.SelectedItem as ComboBoxItem;
            ObjCustomer.IdentificationType = Convert.ToInt32(selectedIdentificationType!.Key);
            ObjCustomer.Identification = txtIdentification.Text;

            //validar que se permita la omision de la cedula
            var validarCedula = _objInterfazCoreWebParameter.GetParameterValue("CXC_VALIDAR_CEDULA_REPETIDA", user.CompanyID);
            if (string.Compare(validarCedula, "true", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                //Validar que ya existe el cliente
                var objCustomerOld = _customerModel.GetRowByIdentification(user.CompanyID, ObjCustomer.Identification);
                if (objCustomerOld is not null)
                {
                    if (objCustomerOld.EntityID == _entityId)
                    {
                        throw new Exception("Error identificacion del cliente ya existe.");
                    }
                }
            }

            //datos del recordatorio
            if (ObjListCustomerFrecuencyActuations.Count > 0)
            {
                var customerFrecuencyActuations = ObjListCustomerFrecuencyActuations.Select(dto => dto.CustomerFreFrecuencyActuations).ToList();
                _customerFrecuencyActuationsModel.DeleteWhereIdNotIn(_entityId, customerFrecuencyActuations);
                foreach (var frecuencyActuation in ObjListCustomerFrecuencyActuations)
                {
                    var idFrecuencia = frecuencyActuation.CustomerFreFrecuencyActuations ?? 0;
                    var objFrecuencyActuations = new TbCustomerFrecuencyActuation
                    {
                        EntityID = _entityId,
                        Name = frecuencyActuation.Name,
                        SituationID = frecuencyActuation.SituationID,
                        FrecuencyContactID = frecuencyActuation.FrecuencyContactID,
                        IsActive = 1,
                    };
                    if (idFrecuencia <= 0)
                    {
                        objFrecuencyActuations.CreatedOn = DateTime.Now;
                        objFrecuencyActuations.IsApply = 0;
                        _customerFrecuencyActuationsModel.InsertAppPosme(objFrecuencyActuations);
                    }
                    else
                    {
                        objFrecuencyActuations.CustomerFrecuencyActuations = idFrecuencia;
                        objFrecuencyActuations.CreatedOn = frecuencyActuation.CreatedOn;
                        objFrecuencyActuations.IsApply = frecuencyActuation.IsApply;
                        _customerFrecuencyActuationsModel.UpdateAppPosme(_entityId, idFrecuencia, objFrecuencyActuations);
                    }
                }
            }

            //datos del cliente
            var selectedCountry = txtCountryID.SelectedItem as ComboBoxItem;
            var selectedState = txtStateID.SelectedItem as ComboBoxItem;
            var selectedCity = txtCityID.SelectedItem as ComboBoxItem;
            var selectedCurrencyId = txtCurrencyID.SelectedItem as ComboBoxItem;
            var selectedClasificacionId = txtClasificationID.SelectedItem as ComboBoxItem;
            var selectedCateogoryId = txtCategoryID.SelectedItem as ComboBoxItem;
            var selectedSubCateogoryId = txtSubCategoryID.SelectedItem as ComboBoxItem;
            var selectedCustomerTypeId = txtCustomerTypeID.SelectedItem as ComboBoxItem;
            var selectedStatusId = txtStatusID.SelectedItem as ComboBoxItem;
            var selectedPayConditionId = txtPayConditionID.SelectedItem as ComboBoxItem;
            var selectedSexoId = txtSexoID.SelectedItem as ComboBoxItem;
            var selectedTypeFirmId = txtTypeFirmID.SelectedItem as ComboBoxItem;
            var selectedTypeId = txtTypePayID.SelectedItem as ComboBoxItem;
            var budget = ObjCustomer.Budget;
            if (!string.IsNullOrWhiteSpace(txtBudget.Text))
            {
                budget = decimal.Parse(txtBudget.Text);
            }

            var balancePoint = ObjCustomer.BalancePoint;
            if (!string.IsNullOrWhiteSpace(txtBalancePoint.Text))
            {
                balancePoint = decimal.Parse(txtBalancePoint.Text);
            }

            ObjCustomer.CountryID = Convert.ToInt32(selectedCountry!.Key);
            ObjCustomer.StateID = Convert.ToInt32(selectedState!.Key);
            ObjCustomer.CityID = Convert.ToInt32(selectedCity!.Key);
            ObjCustomer.Location = txtLocation.Text;
            ObjCustomer.Address = txtAddress.Text;
            ObjCustomer.CurrencyID = Convert.ToInt32(selectedCurrencyId!.Key);
            ObjCustomer.ClasificationID = Convert.ToInt32(selectedClasificacionId!.Key);
            ObjCustomer.CategoryID = Convert.ToInt32(selectedCateogoryId!.Key);
            ObjCustomer.SubCategoryID = Convert.ToInt32(selectedSubCateogoryId!.Key);
            ObjCustomer.CustomerTypeID = Convert.ToInt32(selectedCustomerTypeId!.Key);
            ObjCustomer.BirthDate = txtBirthDate.DateTime;
            ObjCustomer.StatusID = Convert.ToInt32(selectedStatusId!.Key);
            ObjCustomer.TypePay = Convert.ToInt32(selectedTypeId!.Key);
            ObjCustomer.PayConditionID = Convert.ToInt32(selectedPayConditionId!.Key);
            ObjCustomer.SexoID = Convert.ToInt32(selectedSexoId!.Key);
            ObjCustomer.Reference1 = txtReference1.Text;
            ObjCustomer.Reference2 = txtReference2.Text;
            ObjCustomer.Reference3 = txtReference3.Text;
            ObjCustomer.Reference4 = txtReference4.Text;
            ObjCustomer.Reference5 = txtReference5.Text;
            ObjCustomer.BalancePoint = balancePoint;
            ObjCustomer.PhoneNumber = Encoding.UTF8.GetBytes(txtPhoneNumber.Text);
            ObjCustomer.TypeFirm = Convert.ToInt32(selectedTypeFirmId!.Key);
            ObjCustomer.Budget = budget;
            ObjCustomer.IsActive = true;
            ObjCustomer.EntityContactID = _txtEmployerId;
            ObjCustomer.ModifiedOn = DateTime.Now;
            //$objCustomer["formContactID"]		= $this->request->getPost("txtFormContactID"); no tengo el campo en winform
            _customerModel.UpdateAppPosme(user.CompanyID, user.BranchID, _entityId, ObjCustomer);

            //Actualizar Customer Credit
            if (ObjCustomerCredit is not null)
            {
                var limitCreditDol = ObjCustomerCredit.LimitCreditDol;
                if (!string.IsNullOrWhiteSpace(txtLimitCreditDol.Text))
                {
                    limitCreditDol = decimal.Parse(txtLimitCreditDol.Text);
                }

                var incomeDol = ObjCustomerCredit.IncomeDol;
                if (!string.IsNullOrWhiteSpace(txtIncomeDol.Text))
                {
                    incomeDol = decimal.Parse(txtIncomeDol.Text);
                }

                var objCustomerCreditNew = new TbCustomerCredit
                {
                    CompanyID = ObjCustomerCredit.CompanyID,
                    BranchID = ObjCustomerCredit.BranchID,
                    EntityID = ObjCustomerCredit.EntityID,
                    LimitCreditDol = limitCreditDol,
                    BalanceDol = decimal.Subtract(limitCreditDol, decimal.Subtract(ObjCustomerCredit.LimitCreditDol, ObjCustomerCredit.BalanceDol)),
                    IncomeDol = incomeDol
                };
                _customerCreditModel.UpdateAppPosme(user.CompanyID, user.BranchID, _entityId, objCustomerCreditNew);
                ObjCustomerCredit = objCustomerCreditNew;
            }

            //actualizar cuenta
            var objListEntityAccount = _entityAccountModel.GetRowByEntity(user.CompanyID, ObjComponent.ComponentID, _entityId);
            var objFirstEntityAccount = objListEntityAccount.FirstOrDefault();
            if (objFirstEntityAccount is not null)
            {
                objFirstEntityAccount.AccountID = _txtAccountId;
                _entityAccountModel.UpdateAppPosme(objFirstEntityAccount.EntityAccountID, objFirstEntityAccount);
            }
        }

        //Email
        _entityEmailModel.DeleteByEntity(user.CompanyID, user.BranchID, _entityId);
        if (ObjListEmail.Count > 0)
        {
            foreach (var tbEntityEmail in ObjListEmail)
            {
                tbEntityEmail.CompanyID = user.CompanyID;
                tbEntityEmail.BranchID = user.BranchID;
                tbEntityEmail.EntityID = _entityId;
                _entityEmailModel.InsertAppPosme(tbEntityEmail);
            }
        }

        //Phone
        _entityPhoneModel.DeleteByEntity(user.CompanyID, user.BranchID, _entityId);
        if (ObjListPhone.Count > 0)
        {
            foreach (var tbEntityPhoneDto in ObjListPhone)
            {
                var objEntityPhone = new TbEntityPhone
                {
                    CompanyID = user.CompanyID,
                    BranchID = user.BranchID,
                    EntityID = _entityId,
                    TypeID = tbEntityPhoneDto.TypeId,
                    Number = tbEntityPhoneDto.Number,
                    IsPrimary = tbEntityPhoneDto.IsPrimary
                };
                _entityPhoneModel.InsertAppPosme(objEntityPhone);
            }
        }

        //Lineas de Creditos
        var limitCreditLine = decimal.Zero;
        var customerCreditLineId = ObjListCustomerCreditLine.Select(dto => dto.CustomerCreditLineId).ToList();
        _customerCreditLineModel.DeleteWhereIdNotIn(user.CompanyID, user.BranchID, _entityId, customerCreditLineId);
        if (ObjListCustomerCreditLine.Count > 0)
        {
            foreach (var customerCreditLineDto in ObjListCustomerCreditLine)
            {
                if (customerCreditLineDto.CustomerCreditLineId <= 0)
                {
                    var objCustomerCreditLine = new TbCustomerCreditLine
                    {
                        CompanyID = user.CompanyID,
                        BranchID = user.BranchID,
                        EntityID = _entityId,
                        CreditLineID = customerCreditLineDto.CreditLineId,
                        AccountNumber = _objInterfazCoreWebCounter.GoNextNumber(user.CompanyID, user.BranchID, "tb_customer_credit_line", 0)!,
                        CurrencyID = customerCreditLineDto.CurrencyId,
                        TypeAmortization = customerCreditLineDto.TypeAmortization,
                        LimitCredit = customerCreditLineDto.LimitCredit ?? decimal.Zero,
                        Balance = customerCreditLineDto.Balance ?? decimal.Zero,
                        InterestYear = customerCreditLineDto.InterestYear ?? decimal.Zero,
                        InterestPay = customerCreditLineDto.InterestPay ?? decimal.Zero,
                        TotalPay = customerCreditLineDto.TotalPay ?? decimal.Zero,
                        TotalDefeated = customerCreditLineDto.TotalDefeated ?? decimal.Zero,
                        DateOpen = DateTime.Now,
                        PeriodPay = customerCreditLineDto.PeriodPay,
                        DateLastPay = DateTime.Now,
                        Term = customerCreditLineDto.Term,
                        Note = customerCreditLineDto.Note,
                        StatusID = customerCreditLineDto.StatusId,
                        IsActive = true,
                        DayExcluded = customerCreditLineDto.DayExcluded
                    };
                    limitCreditLine = decimal.Add(limitCreditLine, objCustomerCreditLine.LimitCredit);
                    exchangeRate = _objInterfazCoreWebCurrency.GetRatio(user.CompanyID, dateOn, decimal.One, objCustomerCreditLine.CurrencyID, objCurrencyDolares.CurrencyID);
                    exchangeRateAmount = objCustomerCreditLine.LimitCredit;
                    _customerCreditLineModel.InsertAppPosme(objCustomerCreditLine);
                    if (objCustomerCreditLine.Balance > objCustomerCreditLine.LimitCredit)
                    {
                        throw new Exception("BALANCE NO PUEDE SER MAYOR QUE EL LIMITE EN LA LINEA");
                    }
                }
                else
                {
                    var objCustomerCreditLine = _customerCreditLineModel.GetRowByPk(customerCreditLineDto.CustomerCreditLineId);
                    if (objCustomerCreditLine is not null)
                    {
                        var objCustomerCreditLineNew = new TbCustomerCreditLine
                        {
                            CompanyID = customerCreditLineDto.CompanyId,
                            BranchID = customerCreditLineDto.BranchId,
                            EntityID = customerCreditLineDto.EntityId,
                            CreditLineID = customerCreditLineDto.CreditLineId,
                            AccountNumber = objCustomerCreditLine.AccountNumber,
                            CurrencyID = objCustomerCreditLine.CurrencyID,
                            LimitCredit = customerCreditLineDto.LimitCredit ?? decimal.Zero,
                            InterestYear = customerCreditLineDto.InterestYear ?? decimal.Zero,
                            InterestPay = objCustomerCreditLine.InterestPay,
                            TotalPay = objCustomerCreditLine.TotalPay,
                            TotalDefeated = objCustomerCreditLine.TotalDefeated,
                            DateOpen = objCustomerCreditLine.DateOpen,
                            Balance = decimal.Subtract(customerCreditLineDto.LimitCredit!.Value, decimal.Subtract(objCustomerCreditLine.LimitCredit, objCustomerCreditLine.Balance)),
                            PeriodPay = customerCreditLineDto.PeriodPay,
                            DateLastPay = objCustomerCreditLine.DateLastPay,
                            Term = customerCreditLineDto.Term,
                            Note = customerCreditLineDto.Note,
                            StatusID = customerCreditLineDto.StatusId,
                            IsActive = objCustomerCreditLine.IsActive,
                            TypeAmortization = customerCreditLineDto.TypeAmortization,
                            DayExcluded = customerCreditLineDto.DayExcluded
                        };
                        limitCreditLine = decimal.Add(limitCreditLine, objCustomerCreditLineNew.LimitCredit);
                        exchangeRate = _objInterfazCoreWebCurrency.GetRatio(user.CompanyID, dateOn, decimal.One, objCustomerCreditLine.CurrencyID, objCurrencyDolares!.CurrencyID);
                        exchangeRateAmount = objCustomerCreditLineNew.LimitCredit;
                        //Si el balance es mayor que el limite igual el balance al limite
                        if (objCustomerCreditLineNew.Balance.CompareTo(objCustomerCreditLineNew.LimitCredit) > 0)
                        {
                            objCustomerCreditLineNew.Balance = objCustomerCreditLineNew.LimitCredit;
                        }

                        //actualizar
                        _customerCreditLineModel.UpdateAppPosme(customerCreditLineDto.CustomerCreditLineId, objCustomerCreditLineNew);
                    }
                }

                //sumar los limites en dolares
                if (exchangeRate == 1) exchangeRateTotal = exchangeRateTotal + exchangeRateAmount;
                //sumar los limite en cordoba
                else exchangeRateTotal = exchangeRateTotal + (exchangeRateAmount / exchangeRate);
            }

            //Validar Limite de Credito
            if (exchangeRateTotal > ObjCustomerCredit.LimitCreditDol)
                throw new Exception("LINEAS DE CREDITOS MAL CONFIGURADAS LÍMITE EXCEDIDO");

            //Actualizar Balance
            if (ObjCustomerCredit.BalanceDol.CompareTo(ObjCustomerCredit.LimitCreditDol) > 0)
            {
                ObjCustomerCredit.BalanceDol = ObjCustomerCredit.LimitCreditDol;
                _customerCreditModel.UpdateAppPosme(user.CompanyID, user.BranchID, _entityId, ObjCustomerCredit);
            }
        }
    }

    public void CommandNew(object? sender, EventArgs e)
    {
        Close();
        var frmCustomerEdit = new FormCustomerEdit(TypeRender.New, 0)
        {
            MdiParent = CoreFormList.Principal()
        };
        frmCustomerEdit.Show();
    }

    public void CommandSave(object? sender, EventArgs e)
    {
        if (FnValidateFormAndSubmit())
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
                    _entityId = ObjCustomer.EntityID;
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
        HelperMethods.OnlyNumberDecimals(txtBudget);
        HelperMethods.OnlyNumberDecimals(txtLimitCreditDol);
        HelperMethods.OnlyNumberDecimals(txtIncomeDol);
    }

    public void LoadRender(TypeRender typeRedner)
    {
        _typeRender = typeRedner;
        var user = VariablesGlobales.Instance.User;
        if (user is null) return;
        switch (typeRedner)
        {
            case TypeRender.New:
            {
                Text = "Nuevo Cliente";
                lblTitulo.Text = @"CODIGO:#00000000";
                btnEliminar.Visible = false;
                btnNuevo.Visible = false;
                btnImprmir.Visible = false;
                txtBirthDate.DateTime = DateTime.Now;
                tabPageArchivos.PageVisible = false;
                tabLeads.PageVisible = false;
                gridControlArchivos.DataSource = null;
                btnEditLine.Visible = false;
                lcBuro.Visibility = LayoutVisibility.Never;
                lcHuella.Visibility = LayoutVisibility.Never;
                CoreWebRenderInView.LlenarComboBox(ObjListSexoId, txtSexoID, "CatalogItemID", "Name", ObjListSexoId.First().CatalogItemID);
                CoreWebRenderInView.LlenarComboBox(ObjListIdentificationType, txtIdentificationTypeID, "CatalogItemID", "Name", ObjListIdentificationType.First().CatalogItemID);
                CoreWebRenderInView.LlenarComboBox(ObjListWorkflowStage, txtStatusID, "WorkflowStageID", "Name", ObjListWorkflowStage.First().WorkflowStageID);
                CoreWebRenderInView.LlenarComboBox(ObjListClasificationId, txtClasificationID, "CatalogItemID", "Name", ObjListClasificationId.First().CatalogItemID);
                CoreWebRenderInView.LlenarComboBox(ObjListCustomerTypeId, txtCustomerTypeID, "CatalogItemID", "Name", ObjListCustomerTypeId.First().CatalogItemID);
                CoreWebRenderInView.LlenarComboBox(ObjListCategoryId, txtCategoryID, "CatalogItemID", "Name", ObjListCategoryId.First().CatalogItemID);
                CoreWebRenderInView.LlenarComboBox(ObjListSubCategoryId, txtSubCategoryID, "CatalogItemID", "Name", ObjListSubCategoryId.First().CatalogItemID);
                CoreWebRenderInView.LlenarComboBox(ObjListEstadoCivilId, txtCivilStatusID, "CatalogItemID", "Name", ObjListEstadoCivilId.First().CatalogItemID);
                CoreWebRenderInView.LlenarComboBox(ObjListProfesionId, txtProfesionID, "CatalogItemID", "Name", ObjListProfesionId.First().CatalogItemID);
                CoreWebRenderInView.LlenarComboBox(ObjListCountry, txtCountryID, "CatalogItemID", "Name", Convert.ToInt32(ObjParameterPais));
                CoreWebRenderInView.LlenarComboBox(ObjListTypeFirmId, txtTypeFirmID, "CatalogItemID", "Name", ObjListTypeFirmId.First().CatalogItemID);
                CoreWebRenderInView.LlenarComboBox(ObjListCurrency, txtCurrencyID, "CurrencyId", "Name", ObjListCurrency.First().CurrencyId);
                CoreWebRenderInView.LlenarComboBox(ObjListTypePay, txtTypePayID, "CatalogItemID", "Name", ObjListTypePay.First().CatalogItemID);
                CoreWebRenderInView.LlenarComboBox(ObjListPayConditionId, txtPayConditionID, "CatalogItemID", "Name", ObjListPayConditionId.First().CatalogItemID);
                CoreWebRenderInView.LlenarComboBox(ObjListTypeId, txtTipoTarjeta, "CatalogItemID", "Name", ObjListTypeId.First().CatalogItemID);
                CoreWebRenderInView.LlenarComboBoxGridControl(ObjListSituationId, cmbEstadoRecordatorio, "CatalogItemID", "Name");
                CoreWebRenderInView.LlenarComboBoxGridControl(ObjListFrecuencyContactId, cmbFrecuenciaRecordatorio, "CatalogItemID", "Name");

                txtIncomeDol.Text = "5000.00";
                txtLimitCreditDol.Text = "900000.00";
                break;
            }
            case TypeRender.Edit:
            {
                Text = @"Editar Cliente";
                lblTitulo.Text = @$"CODIGO:#{ObjCustomer.CustomerNumber}";
                btnEliminar.Visible = true;
                btnNuevo.Visible = true;
                btnImprmir.Visible = true;
                txtBirthDate.DateTime = DateTime.Now;
                tabPageArchivos.PageVisible = true;
                tabLeads.PageVisible = true;
                btnEditLine.Visible = true;
                lcBuro.Visibility = LayoutVisibility.Always;
                lcHuella.Visibility = LayoutVisibility.Always;
                CoreWebRenderInView.LlenarComboBox(ObjListSexoId, txtSexoID, "CatalogItemID", "Name", ObjCustomer.SexoID);
                CoreWebRenderInView.LlenarComboBox(ObjListIdentificationType, txtIdentificationTypeID, "CatalogItemID", "Name", ObjCustomer.IdentificationType);
                CoreWebRenderInView.LlenarComboBox(ObjListWorkflowStage, txtStatusID, "WorkflowStageID", "Name", ObjCustomer.StatusID);
                CoreWebRenderInView.LlenarComboBox(ObjListClasificationId, txtClasificationID, "CatalogItemID", "Name", ObjCustomer.ClasificationID);
                CoreWebRenderInView.LlenarComboBox(ObjListCustomerTypeId, txtCustomerTypeID, "CatalogItemID", "Name", ObjCustomer.CustomerTypeID);
                CoreWebRenderInView.LlenarComboBox(ObjListCategoryId, txtCategoryID, "CatalogItemID", "Name", ObjCustomer.CategoryID);
                CoreWebRenderInView.LlenarComboBox(ObjListSubCategoryId, txtSubCategoryID, "CatalogItemID", "Name", ObjCustomer.SubCategoryID);
                CoreWebRenderInView.LlenarComboBox(ObjListEstadoCivilId, txtCivilStatusID, "CatalogItemID", "Name", ObjNatural.StatusID);
                CoreWebRenderInView.LlenarComboBox(ObjListProfesionId, txtProfesionID, "CatalogItemID", "Name", ObjNatural.ProfesionID);
                CoreWebRenderInView.LlenarComboBox(ObjListCountry, txtCountryID, "CatalogItemID", "Name", ObjCustomer.CountryID);
                CoreWebRenderInView.LlenarComboBox(ObjListTypeFirmId, txtTypeFirmID, "CatalogItemID", "Name", ObjCustomer.TypeFirm);
                CoreWebRenderInView.LlenarComboBox(ObjListCurrency, txtCurrencyID, "CurrencyId", "Name", ObjCustomer.CurrencyID);
                CoreWebRenderInView.LlenarComboBox(ObjListTypePay, txtTypePayID, "CatalogItemID", "Name", ObjCustomer.TypePay);
                if (ObjPCItemTypeLeads.Count > 0)
                {
                    CoreWebRenderInView.LlenarComboBox(ObjPCItemTypeLeads, txtLeadTipo, "PublicCatalogDetailID", "Name", ObjPCItemTypeLeads.First().PublicCatalogDetailID);
                }

                if (ObjPCItemCategoryLeads.Count > 0)
                {
                    CoreWebRenderInView.LlenarComboBox(ObjPCItemCategoryLeads, txtLeadCategory, "PublicCatalogDetailID", "Name", ObjPCItemCategoryLeads.First().PublicCatalogDetailID);
                }

                if (ObjPCItemSubTypeLeads.Count > 0)
                {
                    CoreWebRenderInView.LlenarComboBox(ObjPCItemSubTypeLeads, txtLeadSubTipo, "PublicCatalogDetailID", "Name", ObjPCItemSubTypeLeads.First().PublicCatalogDetailID);
                }

                CoreWebRenderInView.LlenarComboBox(ObjListPayConditionId, txtPayConditionID, "CatalogItemID", "Name", ObjCustomer.PayConditionID);
                if (ObjPaymentMethod is not null)
                {
                    if (!string.IsNullOrWhiteSpace(ObjPaymentMethod.Number))
                    {
                        txtNombreTarjeta.Text = ObjPaymentMethod.Name;
                        txtDatosTarjeta.Card = new PaymentCard(ObjPaymentMethod.Number!, DateTime.Parse(ObjPaymentMethod.ExpirationDate!), ObjPaymentMethod.Cvc!);
                        txtEmailTarjeta.Text = ObjPaymentMethod.Email!;
                        CoreWebRenderInView.LlenarComboBox(ObjListTypeId, txtTipoTarjeta, "CatalogItemID", "Name", ObjPaymentMethod.TypeId);
                    }
                }
                else
                {
                    CoreWebRenderInView.LlenarComboBox(ObjListTypeId, txtTipoTarjeta, "CatalogItemID", "Name", ObjListTypeId.First().CatalogItemID);
                }

                CoreWebRenderInView.LlenarComboBoxGridControl(ObjListSituationId, cmbEstadoRecordatorio, "CatalogItemID", "Name");
                CoreWebRenderInView.LlenarComboBoxGridControl(ObjListFrecuencyContactId, cmbFrecuenciaRecordatorio, "CatalogItemID", "Name");
                gridControlArchivos.DataSource = null;
                _renderGridFiles = new RenderFileGridControl(user.CompanyID, ObjComponent!.ComponentID, _entityId);
                _renderGridFiles.RenderGridControl(gridControlArchivos);
                _renderGridFiles.LoadFiles();
                txtBirthDate.DateTime = ObjCustomer.BirthDate ?? DateTime.Today;
                txtFirstName.Text = ObjNatural.FirstName;
                txtLastName.Text = ObjNatural.LastName;
                txtLegalName.Text = ObjLegal.LegalName;
                txtCommercialName.Text = ObjLegal.ComercialName;
                txtIdentification.Text = ObjCustomer.Identification;
                txtPhoneNumber.Text = ObjCustomer.PhoneNumber is null ? "" : Encoding.UTF8.GetString(ObjCustomer.PhoneNumber);
                txtIncomeDol.Text = ObjCustomerCredit.IncomeDol.ToString("N2");
                txtLimitCreditDol.Text = ObjCustomerCredit.LimitCreditDol.ToString("N2");
                txtBalanceDol.Text = ObjCustomerCredit.BalanceDol.ToString("N2");
                txtBalancePoint.Text = ObjCustomer.BalancePoint!.Value.ToString("N2");
                _txtAccountId = ObjAccount?.AccountID ?? 0;
                txtAccountIDDescription.Text = ObjAccount is not null ? $"{ObjAccount.AccountNumber} {ObjAccount.Name}" : "";
                txtLocation.Text = ObjCustomer.Location;
                txtBudget.Text = ObjCustomer.Budget!.Value.ToString("N2");
                _txtEmployerId = ObjEmployerNatural?.EntityID ?? 0;
                txtEmployerDescription.Text = ObjEmployerNatural is not null ? $"{ObjEmployer!.EmployeNumber} {ObjEmployerNatural.FirstName} {ObjEmployerNatural.LastName}".ToUpper() : "";
                txtReference1.Text = ObjCustomer.Reference1;
                txtReference2.Text = ObjCustomer.Reference2;
                txtReference3.Text = ObjCustomer.Reference3;
                txtReference4.Text = ObjCustomer.Reference4;
                txtReference5.Text = ObjCustomer.Reference5;
                txtAddress.Text = ObjCustomer.Address;

                if (ObjCustomerSinRiesgo.Count > 0)
                {
                    var barManager1 = new BarManager();
                    barManager1.Form = this;
                    var popupMenu1 = new PopupMenu(barManager1);
                    var urlBase = VariablesGlobales.ConfigurationBuilder["APP_URL_RESOURCE_CSS_JS"];
                    var frmWebView = new FormTypeWebView();
                    foreach (var tbCustomerConsultasSinRiesgo in ObjCustomerSinRiesgo)
                    {
                        var button = new BarButtonItem(barManager1, tbCustomerConsultasSinRiesgo.Name);
                        button.ItemClick += (sender, args) =>
                        {
                            if (string.IsNullOrWhiteSpace(tbCustomerConsultasSinRiesgo.File))
                            {
                                return;
                            }

                            frmWebView.webView.Source = new Uri($"{urlBase}/app_cxc_record/index?file_exists={tbCustomerConsultasSinRiesgo.File}");
                            frmWebView.Show();
                        };
                        popupMenu1.AddItem(button);
                    }
                }

                break;
            }
        }
    }

    public void InitializeControl()
    {
    }

    #endregion

    #region Funciones

    private bool FnValidateLeads()
    {
        var leadCategory = txtLeadCategory.SelectedItem as ComboBoxItem;
        if (leadCategory is null)
        {
            dxErrorProvider.SetError(txtLeadCategory, "Debe seleccionar una categoria para el Leads");
            return false;
        }

        var leadTipo = txtLeadTipo.SelectedItem as ComboBoxItem;
        if (leadTipo is null)
        {
            dxErrorProvider.SetError(txtLeadTipo, "Debe seleccionar un tipo para el Leads");
            return false;
        }

        var leadSubTipo = txtLeadSubTipo.SelectedItem as ComboBoxItem;
        if (leadSubTipo is null)
        {
            dxErrorProvider.SetError(txtLeadSubTipo, "Debe seleccionar un sub-tipo para el Leads");
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtLeadComentario.Text))
        {
            dxErrorProvider.SetError(txtLeadComentario, "Debe especificar este campo para continuar");
            return false;
        }

        return true;
    }

    private void FnSaveLeads()
    {
        var userNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];
        var user = VariablesGlobales.Instance.User;
        if (user is null)
        {
            throw new Exception(userNotAutenticated);
        }

        var role = VariablesGlobales.Instance.Role;
        if (role is null)
        {
            throw new Exception("No hay role activo para este modulo");
        }

        _objInterfazCoreWebPermission.GetValueLicense(user.CompanyID, "app_cxc_customer/index");

        //Obtener el Componente de Transacciones Facturacion
        var objComponentShare = _objInterfazCoreWebTools.GetComponentIdByComponentName("tb_transaction_master_customer_leads");
        if (objComponentShare is null) throw new Exception("EL COMPONENTE 'tb_transaction_master_customer_leads' NO EXISTE...");

        var transactionModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionModel>();
        var transactionMasterModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionMasterModel>();

        //Obtener transaccion
        var transactionId = _objInterfazCoreWebTransaction.GetTransactionId(user.CompanyID, "tb_transaction_master_customer_leads", 0);
        var objT = transactionModel.GetByCompanyAndTransaction(user.CompanyID, transactionId ?? 0);
        if (objT is null)
        {
            throw new Exception("No hay transacción activa para el Leads");
        }

        var statusId = _objInterfazCoreWebWorkflow.GetWorkflowInitStage("tb_transaction_master_customer_leads", "statusID", user.CompanyID, user.BranchID, role.RoleID);
        if (statusId is null)
        {
            throw new Exception("No hay status");
        }

        var leadCategory = txtLeadCategory.SelectedItem as ComboBoxItem;
        var leadTipo = txtLeadTipo.SelectedItem as ComboBoxItem;
        var leadSubTipo = txtLeadSubTipo.SelectedItem as ComboBoxItem;
        var objTM = new TbTransactionMaster
        {
            CompanyID = user.CompanyID,
            TransactionNumber = _objInterfazCoreWebCounter.GoNextNumber(user.CompanyID, user.BranchID, "tb_transaction_master_customer_leads", 0) ?? "null",
            TransactionID = transactionId!.Value,
            BranchID = user.BranchID,
            TransactionCausalID = _objInterfazCoreWebTransaction.GetDefaultCausalId(user.CompanyID, transactionId.Value),
            EntityID = _entityId,
            TransactionOn = DateTime.Now,
            StatusIDChangeOn = DateTime.Now,
            ComponentID = objComponentShare.ComponentID,
            Note = txtLeadComentario.Text,
            Sign = (short?)objT.SignInventory!.Value,
            CurrencyID = 0,
            CurrencyID2 = 0,
            ExchangeRate = decimal.Zero,
            Reference1 = leadCategory!.Value!.ToString(),
            Reference2 = "",
            Reference3 = "",
            Reference4 = "",
            StatusID = statusId.ElementAt(0).WorkflowStageID,
            Amount = decimal.Zero,
            Discount = decimal.Zero,
            SubAmount = decimal.Zero,
            IsApplied = false,
            JournalEntryID = 0,
            ClassID = null,
            AreaID = Convert.ToInt32(leadTipo!.Key),
            PriorityID = Convert.ToInt32(leadSubTipo!.Key),
            SourceWarehouseID = null,
            TargetWarehouseID = null,
            IsActive = true,
            EntityIDSecondary = 0,
        };
        _objInterfazCoreWebAuditoria.SetAuditCreated(objTM, user, "");

        transactionMasterModel.InsertAppPosme(objTM);
    }

    private bool FnValidateFormAndSubmit()
    {
        if (string.IsNullOrWhiteSpace(txtIdentification.Text))
        {
            dxErrorProvider.SetError(txtIdentification, "Escribir la identificación");
            return false;
        }

        var selectedSexo = txtSexoID.SelectedItem as ComboBoxItem;
        if (selectedSexo is null)
        {
            dxErrorProvider.SetError(txtIdentification, "Seleccionar tipo de sexo");
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtFirstName.Text))
        {
            dxErrorProvider.SetError(txtFirstName, "Escribir Primer Nombre");
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtLastName.Text))
        {
            dxErrorProvider.SetError(txtLastName, "Escribir Segundo Nombre");
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtLegalName.Text))
        {
            dxErrorProvider.SetError(txtLegalName, "Escribir Nombre Legal");
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtCommercialName.Text))
        {
            dxErrorProvider.SetError(txtCommercialName, "Escribir Nombre Comercial");
            return false;
        }

        return true;
    }

    private void FonObjListCustomerFrecuencyActuations(List<TbCustomerFrecuencyActuation> objCustomerFrecuency)
    {
        ObjListCustomerFrecuencyActuations = new BindingList<FormCustomerEditCustomerFrecuencyActuationDTO>
        {
            AllowEdit = true,
            AllowNew = true,
            AllowRemove = true
        };
        if (objCustomerFrecuency.Count > 0)
        {
            foreach (var tbCustomerFrecuencyActuation in objCustomerFrecuency)
            {
                var dto = new FormCustomerEditCustomerFrecuencyActuationDTO
                {
                    EntityID = tbCustomerFrecuencyActuation.EntityID,
                    CreatedOn = tbCustomerFrecuencyActuation.CreatedOn,
                    CustomerFreFrecuencyActuations = tbCustomerFrecuencyActuation.CustomerFrecuencyActuations,
                    Name = tbCustomerFrecuencyActuation.Name,
                    SituationDisplay = ObjListSituationId.First(item => item.CatalogItemID == tbCustomerFrecuencyActuation.SituationID).Name,
                    FrecuencyDisplay = ObjListFrecuencyContactId.First(item => item.CatalogItemID == tbCustomerFrecuencyActuation.FrecuencyContactID).Name,
                    FrecuencyContactID = tbCustomerFrecuencyActuation.FrecuencyContactID,
                    SituationID = tbCustomerFrecuencyActuation.SituationID,
                    IsApply = tbCustomerFrecuencyActuation.IsApply,
                    IsActive = tbCustomerFrecuencyActuation.IsActive
                };
                ObjListCustomerFrecuencyActuations.Add(dto);
            }
        }

        gridControlRecordatorios.DataSource = ObjListCustomerFrecuencyActuations;
    }

    private void FnObjListEmail(List<TbEntityEmail> objEntitylistEmail)
    {
        ObjListEmail = new BindingList<TbEntityEmail>();
        if (objEntitylistEmail.Count > 0)
        {
            objEntitylistEmail.ForEach(ObjListEmail.Add);
        }

        gridControlEmail.DataSource = ObjListEmail;
    }

    private void FnObjListPhone(List<TbEntityPhoneDto> objEntityListPhone)
    {
        ObjListPhone = new BindingList<TbEntityPhoneDto>();
        if (objEntityListPhone.Count > 0)
        {
            objEntityListPhone.ForEach(ObjListPhone.Add);
        }

        gridControlTelefonos.DataSource = ObjListPhone;
    }

    private void FnObjListCustomerCreditLine(List<TbCustomerCreditLineDto> objCustomerCreditLine)
    {
        ObjListCustomerCreditLine = new BindingList<TbCustomerCreditLineDto>();
        if (objCustomerCreditLine.Count > 0)
        {
            foreach (var tbCustomerCreditLineDto in objCustomerCreditLine)
            {
                ObjListCustomerCreditLine.Add(tbCustomerCreditLineDto);
            }
        }

        gridControlCxclineas.DataSource = ObjListCustomerCreditLine;
    }

    private void FnOnCompleteNewEmployerPopPub(dynamic mensaje)
    {
        var diccionario = new Dictionary<string, string>();
        var objWebToolsHelper = new WebToolsHelper();
        diccionario.Add("entityID", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "entityID", "0"));
        diccionario.Add("Nombre", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Nombre", ""));
        diccionario.Add("Codigo", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Codigo", ""));

        FnOnCompleteNewEmployer(diccionario);
    }

    private void FnOnCompleteNewEmployer(Dictionary<string, string> diccionario)
    {
        Invoke(() =>
        {
            txtEmployerDescription.EditValue = $@"{diccionario["Codigo"]} / {diccionario["Nombre"]}";
            _txtEmployerId = Convert.ToInt32(diccionario["entityID"]);
        });
    }

    private void FnOnCompleteNewAccountPopPub(dynamic mensaje)
    {
        var diccionario = new Dictionary<string, string>();
        var objWebToolsHelper = new WebToolsHelper();
        diccionario.Add("accountID", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "accountID", "0"));
        diccionario.Add("Codigo", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Codigo", ""));
        diccionario.Add("Nombre", objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "Nombre", ""));

        FnOnCompleteNewAccount(diccionario);
    }

    private void FnOnCompleteNewAccount(Dictionary<string, string> diccionario)
    {
        Invoke(() =>
        {
            txtAccountIDDescription.EditValue = $@"{diccionario["Codigo"]} / {diccionario["Nombre"]}";
            _txtAccountId = Convert.ToInt32(diccionario["accountID"]);
        });
    }

    #endregion

    #region Eventos

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

        _backgroundWorker.DoWork += (ob, ev) =>
        {
            ComandDelete();
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

    private void btnNewLine_Click(object sender, EventArgs e)
    {
        var frmCustomerCreditLine = new FormCustomerEditCreditLine(new TbCustomerCreditLineDto(), ObjListCustomerCreditLine);
        var dialogResult = frmCustomerCreditLine.ShowDialog();
        if (dialogResult == DialogResult.Cancel)
        {
            return;
        }

        var tbCustomerCreditLineDto = frmCustomerCreditLine.TbCustomerCreditLineDto;
        if (!ObjListCustomerCreditLine.Any(dto => dto.CreditLineName!.Equals(tbCustomerCreditLineDto.CreditLineName)))
        {
            ObjListCustomerCreditLine.Add(tbCustomerCreditLineDto);
        }
        else
        {
            _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Linea de crédito", "Ya existe la linea de credito, añadir otra", this);
        }
    }

    private void btnEditLine_Click(object sender, EventArgs e)
    {
        if (ObjListCustomerCreditLine.Count == 0)
        {
            return;
        }

        var selectedValue = ObjListCustomerCreditLine.ElementAt(gridViewCxcLineas.FocusedRowHandle);
        var frmCustomerCreditLine = new FormCustomerEditCreditLine(selectedValue, ObjListCustomerCreditLine);
        var dialogResult = frmCustomerCreditLine.ShowDialog();
        if (dialogResult == DialogResult.Cancel)
        {
            return;
        }

        ObjListCustomerCreditLine.ResetBindings();
    }

    private void btnDeleteLine_Click(object sender, EventArgs e)
    {
        var focusedRowHandle = gridViewCxcLineas.FocusedRowHandle;
        if (ObjListCustomerCreditLine.Count > 0)
        {
            ObjListCustomerCreditLine.RemoveAt(focusedRowHandle);
        }
    }

    private void btnSearchEmployer_Click(object sender, EventArgs e)
    {
        var formTypeListSearch = new FormTypeListSearch("Lista de Colaboradores", ObjComponentEmployer!.ComponentID,
            "SELECCIONAR_EMPLOYEE", true, @"", false, "", 0, 5, "", true);
        formTypeListSearch.EventoCallBackAceptarEvent += EventoCallBackAceptarEmployer;
        formTypeListSearch.ShowDialog(this);
    }

    private void EventoCallBackAceptarEmployer(dynamic mensaje)
    {
        FnOnCompleteNewEmployerPopPub(mensaje);
    }

    private void btnClearEmployer_Click(object sender, EventArgs e)
    {
        txtEmployerDescription.EditValue = "";
        _txtEmployerId = 0;
    }

    private void btnNewPhones_Click(object sender, EventArgs e)
    {
        var frmTelefonos = new FormCustomerEditTelefonos();
        var dialogResult = frmTelefonos.ShowDialog(this);
        if (dialogResult == DialogResult.Cancel)
        {
            return;
        }

        ObjListPhone.Add(frmTelefonos.TbEntityPhoneDto);
        gridViewTelefonos.CustomColumnDisplayText += GridViewTelefonos_CustomColumnDisplayText;
    }

    private void GridViewTelefonos_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
    {
        if (e.Column.FieldName == colisPrimary.FieldName)
        {
            e.DisplayText = Convert.ToInt32(e.Value) switch
            {
                1 => "SI",
                0 => "NO",
                _ => e.DisplayText
            };
        }
    }

    private void btnDeletePhones_Click(object sender, EventArgs e)
    {
        ObjListPhone.RemoveAt(gridViewTelefonos.FocusedRowHandle);
    }

    private void btnNewEmail_Click(object sender, EventArgs e)
    {
        var frmEmail = new FormCustomerEditEmail();
        var dialogResult = frmEmail.ShowDialog(this);
        if (dialogResult == DialogResult.Cancel) return;
        ObjListEmail.Add(frmEmail.TbEntityEmail);
        gridViewEmail.CustomColumnDisplayText += GridViewEmail_CustomColumnDisplayText;
    }

    private void GridViewEmail_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
    {
        if (e.Column.FieldName == colEmailPrimary.FieldName)
        {
            e.DisplayText = Convert.ToInt32(e.Value) switch
            {
                1 => "SI",
                0 => "NO",
                _ => e.DisplayText
            };
        }
    }

    private void btnDeleteEmail_Click(object sender, EventArgs e)
    {
        ObjListEmail.RemoveAt(gridViewEmail.FocusedRowHandle);
    }

    private void btnSearchAccount_Click(object sender, EventArgs e)
    {
        var formTypeListSearch = new FormTypeListSearch("Lista de Cuentas", ObjComponentAccount!.ComponentID,
            "SELECCIONAR_CUENTA", true, @"", false, "", 0, 5, "", true);
        formTypeListSearch.EventoCallBackAceptarEvent += EventoCallBackAceptarAccount;
        formTypeListSearch.ShowDialog(this);
    }

    private void EventoCallBackAceptarAccount(dynamic mensaje)
    {
        FnOnCompleteNewAccountPopPub(mensaje);
    }

    private void btnClearAccount_Click(object sender, EventArgs e)
    {
        _txtAccountId = 0;
        txtAccountIDDescription.Clear();
    }

    private void btnAddRecordatorio_Click(object sender, EventArgs e)
    {
        gridViewRecordatorios.AddNewRow();
    }

    private void btnEliminarFilaRecordatorio_Click(object sender, EventArgs e)
    {
        if (gridViewRecordatorios.IsNewItemRow(gridViewRecordatorios.FocusedRowHandle))
        {
            return;
        }

        ObjListCustomerFrecuencyActuations.RemoveAt(gridViewRecordatorios.FocusedRowHandle);
    }

    private void cmbEstadoRecordatorio_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
    {
        if (e.Value != null)
        {
            var isItem = e.Value is ComboBoxItem;
            if (isItem)
            {
                var item = (ComboBoxItem)e.Value;
                e.Value = item.Value;
            }
            else
            {
                e.Value = e.Value.ToString();
            }
        }

        e.Handled = true;
    }

    private void cmbFrecuenciaRecordatorio_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
    {
        if (e.Value != null)
        {
            var isItem = e.Value is ComboBoxItem;
            if (isItem)
            {
                var item = (ComboBoxItem)e.Value;
                e.Value = item.Value;
            }
            else
            {
                e.Value = e.Value.ToString();
            }
        }

        e.Handled = true;
    }

    private void cmbFrecuenciaRecordatorio_SelectedValueChanged(object sender, EventArgs e)
    {
        if (sender is ComboBoxEdit editor)
        {
            var item = editor.SelectedItem as ComboBoxItem;
            if (item is null) return;
            var itemKey = Convert.ToInt32(item.Key);
            gridViewRecordatorios.SetFocusedRowCellValue(colFrecuenciaRecordatorioID, itemKey);
        }
    }

    private void cmbEstadoRecordatorio_SelectedValueChanged(object sender, EventArgs e)
    {
        if (sender is ComboBoxEdit editor)
        {
            var item = editor.SelectedItem as ComboBoxItem;
            if (item is null) return;
            var itemKey = Convert.ToInt32(item.Key);
            gridViewRecordatorios.SetFocusedRowCellValue(colEstadoRecordatorioID, itemKey);
        }
    }

    private void gridViewRecordatorios_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
    {
        var value = e.Value as string;
        var view = sender as GridView;
        foreach (GridColumn viewColumn in view.Columns)
        {
            if (viewColumn.FieldName == colDescripcionRecordatorio.FieldName ||
                viewColumn.FieldName == colEstadoRecordatorio.FieldName ||
                viewColumn.FieldName == colFrecuenciaRecordatorio.FieldName)
            {
                if (!string.IsNullOrWhiteSpace(value)) continue;
                e.ErrorText = "El valor no puede estar vacío.";
                e.Valid = false;
            }
        }
    }

    private void gridViewRecordatorios_InvalidValueException(object sender, InvalidValueExceptionEventArgs e)
    {
        e.ExceptionMode = ExceptionMode.NoAction;
    }

    private void gridViewRecordatorios_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
    {
        var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;

        var descripcionValue = view.GetRowCellValue(e.RowHandle, colDescripcionRecordatorio);
        if (descripcionValue == null || string.IsNullOrWhiteSpace(descripcionValue.ToString()))
        {
            e.Valid = false;
            view.SetColumnError(colDescripcionRecordatorio, "El campo 'Descripcion' no puede estar vacío");
        }

        var estadoValue = view.GetRowCellValue(e.RowHandle, colEstadoRecordatorio);
        if (estadoValue == null || string.IsNullOrWhiteSpace(estadoValue.ToString()))
        {
            e.Valid = false;
            view.SetColumnError(colEstadoRecordatorio, "El campo 'Estado' no puede estar vacío");
        }

        var frecuenciaValue = view.GetRowCellValue(e.RowHandle, colFrecuenciaRecordatorio);
        if (frecuenciaValue == null || string.IsNullOrWhiteSpace(frecuenciaValue.ToString()))
        {
            e.Valid = false;
            view.SetColumnError(colFrecuenciaRecordatorio, "El campo 'Frecuencia' no puede estar vacío");
        }
    }

    private void gridViewRecordatorios_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
    {
        if (e.Column.FieldName == colAccionRecordatorio.FieldName)
        {
            if (gridViewRecordatorios.IsNewItemRow(e.RowHandle))
            {
                e.RepositoryItem = btnAddRecordatorio;
            }
            else
            {
                e.RepositoryItem = btnEliminarFilaRecordatorio;
            }
        }
    }

    private void txtCountryID_SelectedValueChanged(object sender, EventArgs e)
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
            var permited = _objInterfazCoreWebPermission.UrlPermited("app_cxc_customer", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (!permited)
            {
                throw new Exception(notAccessControl);
            }

            var resultPermission = _objInterfazCoreWebPermission.UrlPermissionCmd("app_cxc_customer", "add", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (resultPermission == permissionNone)
            {
                throw new Exception(notAllEdit);
            }
        }


        var countryValue = txtCountryID.SelectedItem as ComboBoxItem;
        if (countryValue is not null)
        {
            var departamento = _objInterfazCoreWebCatalog.GetCatalogAllItemParent("tb_customer", "stateID", user.CompanyID, Convert.ToInt32(countryValue.Key));
            if (departamento.Count > 0)
            {
                if (ObjCustomer is not null)
                {
                    CoreWebRenderInView.LlenarComboBox(departamento, txtStateID, "CatalogItemID", "Name", ObjCustomer.StateID);
                }
                else
                {
                    CoreWebRenderInView.LlenarComboBox(departamento, txtStateID, "CatalogItemID", "Name", Convert.ToInt32(ObjParameterDepartamento));
                }
            }
        }
    }

    private void txtStateID_SelectedValueChanged(object sender, EventArgs e)
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
            var permited = _objInterfazCoreWebPermission.UrlPermited("app_cxc_customer", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (!permited)
            {
                throw new Exception(notAccessControl);
            }

            var resultPermission = _objInterfazCoreWebPermission.UrlPermissionCmd("app_cxc_customer", "add", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (resultPermission == permissionNone)
            {
                throw new Exception(notAllEdit);
            }
        }


        var municipioValue = txtStateID.SelectedItem as ComboBoxItem;
        if (municipioValue is not null)
        {
            var municipios = _objInterfazCoreWebCatalog.GetCatalogAllItemParent("tb_customer", "cityID", user.CompanyID, Convert.ToInt32(municipioValue.Key));
            txtCityID.Properties.Items.Clear();
            if (municipios.Count > 0)
            {
                if (ObjCustomer is not null)
                {
                    CoreWebRenderInView.LlenarComboBox(municipios, txtCityID, "CatalogItemID", "Name", ObjCustomer.CityID);
                }
                else
                {
                    CoreWebRenderInView.LlenarComboBox(municipios, txtCityID, "CatalogItemID", "Name", Convert.ToInt32(ObjParameterMunicipio));
                }
            }
        }
    }

    private void btnScanerHuella_Click(object sender, EventArgs e)
    {
        var api = new FormFingerprintApi();
        if (api.WebActiveSensorEnroll(_entityId))
        {
            _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Informacion, "Huella", "Se ha configurado correctamente el sensor de huell", this);
        }
        else
        {
            _objInterfazCoreWebRenderInView.GetMessageAlert(TypeError.Error, "Huella", "No fue posible configurar el sensor de huell", this);
        }
    }

    private void btnUploadFile_Click(object sender, EventArgs e)
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

    private void btnSaveLeads_Click(object sender, EventArgs e)
    {
        if (FnValidateLeads())
        {
            _backgroundWorker = new BackgroundWorker();
            if (!progressPanel.Visible)
            {
                progressPanel.Width = Width;
                progressPanel.Height = Height;
                progressPanel.Visible = true;
            }

            _backgroundWorker.DoWork += (ob, ev) => { FnSaveLeads(); };
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

    #endregion
}