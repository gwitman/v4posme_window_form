using System.ComponentModel;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;
using v4posme_library.Models;
using v4posme_library.ModelsDto;
using v4posme_window.Libraries;
using v4posme_window.Template;
using ComboBoxItem = v4posme_window.Libraries.ComboBoxItem;

namespace v4posme_window.Views.CXC.Customer
{
    public partial class FormCustomerEditCreditLine : FormTypeHeadModal
    {
        private readonly ICoreWebPermission objInterfazCoreWebPermission = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebPermission>();
        private readonly ICoreWebWorkflow objInterfazCoreWebWorkflow = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebWorkflow>();
        private readonly ICoreWebCatalog objInterfazCoreWebCatalog = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCatalog>();
        private readonly ICoreWebParameter objInterfazCoreWebParameter = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebParameter>();
        private readonly ICreditLineModel creditLineModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICreditLineModel>();
        private readonly ICompanyCurrencyModel companyCurrencyModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyCurrencyModel>();
        public TbCustomerCreditLineDto TbCustomerCreditLineDto { get; }
        public BindingList<TbCustomerCreditLineDto> ObjListCustomerCreditLine { get; set; }
        private string? originalName = "";

        public FormCustomerEditCreditLine(TbCustomerCreditLineDto customerCreditLine, BindingList<TbCustomerCreditLineDto> objListCustomerCreditLine)
        {
            InitializeComponent();
            TbCustomerCreditLineDto = customerCreditLine;
            ObjListCustomerCreditLine = objListCustomerCreditLine;
            btnAceptar.Click += btnAceptar_Click;
            btnCancelar.Click += btnCancelar_Click;
        }


        private void btnCancelar_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object? sender, EventArgs e)
        {
            var selectedCreditLineId = txtCreditLineID.SelectedItem as ComboBoxItem;
            if (selectedCreditLineId is null)
            {
                dxErrorProvider.SetError(txtCreditLineID, "No puede estar vacio");
                return;
            }

            var creditLineId = Convert.ToInt32(selectedCreditLineId.Key);
            if (!selectedCreditLineId.Value!.ToString()!.Equals(originalName))
            {
                var validarCreidtLineId = ObjListCustomerCreditLine.Any(dto => dto.CreditLineId == creditLineId);
                if (validarCreidtLineId)
                {
                    dxErrorProvider.SetError(txtCreditLineID, "Ya está seleccionada una linea de credito con este valor");
                    return;
                }
            }

            var selectedPeriodoPay = txtPeriodPay.SelectedItem as ComboBoxItem;
            if (selectedPeriodoPay is null)
            {
                dxErrorProvider.SetError(txtCreditLineID, "No puede estar vacio");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtInteresYear.Text))
            {
                dxErrorProvider.SetError(txtCreditLineID, "No puede estar vacio");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTerm.Text))
            {
                dxErrorProvider.SetError(txtCreditLineID, "No puede estar vacio");
                return;
            }

            var selectedCurrencyId = txtCurrencyID.SelectedItem as ComboBoxItem;
            if (selectedCurrencyId is null)
            {
                dxErrorProvider.SetError(txtCurrencyID, "No puede estar vacio");
                return;
            }

            var typeAmortization = txtTypeAmorization.SelectedItem as ComboBoxItem;
            if (typeAmortization is null)
            {
                dxErrorProvider.SetError(txtTypeAmorization, "No puede estar vacio");
                return;
            }

            var dayExcluded = txtDayExcluded.SelectedItem as ComboBoxItem;
            if (dayExcluded is null)
            {
                dxErrorProvider.SetError(txtDayExcluded, "No puede estar vacio");
                return;
            }

            var statusId = txtStatusID.SelectedItem as ComboBoxItem;
            if (statusId is null)
            {
                dxErrorProvider.SetError(txtStatusID, "No puede estar vacio");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtInteresYear.Text))
            {
                dxErrorProvider.SetError(txtInteresYear, "No puede estar vacio");
                return;
            }
                
            TbCustomerCreditLineDto.InterestPay = decimal.Zero;
            TbCustomerCreditLineDto.InterestYear = decimal.Parse(txtInteresYear.Text);
            TbCustomerCreditLineDto.AccountNumber = string.IsNullOrWhiteSpace(txtNumber.Text) ? "N/D" : TbCustomerCreditLineDto.AccountNumber;
            TbCustomerCreditLineDto.CreditLineId = creditLineId;
            TbCustomerCreditLineDto.PeriodPay = Convert.ToInt32(selectedPeriodoPay.Key);
            TbCustomerCreditLineDto.DayExcluded = Convert.ToInt32(dayExcluded.Key);
            TbCustomerCreditLineDto.PeriodPayLabel = selectedPeriodoPay.Value!.ToString();
            TbCustomerCreditLineDto.CurrencyId = Convert.ToInt32(selectedCurrencyId.Key);
            TbCustomerCreditLineDto.StatusId = Convert.ToInt32(statusId.Key);
            TbCustomerCreditLineDto.TypeAmortization = Convert.ToInt32(typeAmortization.Key);
            TbCustomerCreditLineDto.CurrencyName = selectedCurrencyId.Value!.ToString();
            TbCustomerCreditLineDto.CreditLineName = selectedCreditLineId.Value.ToString();
            TbCustomerCreditLineDto.LimitCredit = decimal.Parse(txtLimitCredit.Text);
            TbCustomerCreditLineDto.Balance = decimal.Parse(txtLimitCredit.Text);
            TbCustomerCreditLineDto.StatusName = statusId.Value!.ToString();
            TbCustomerCreditLineDto.Note = txtNote.Text;
            TbCustomerCreditLineDto.Term = Convert.ToInt32(txtTerm.Text);
            TbCustomerCreditLineDto.TotalDefeated = decimal.Zero;
            TbCustomerCreditLineDto.TotalPay = decimal.Zero;
            DialogResult = DialogResult.OK;
        }

        private void FormCustomerEditCreditLine_Load(object sender, EventArgs e)
        {
            LoadValues();
            RenderValues();
            HelperMethods.OnlyNumberDecimals(txtInteresYear);
            HelperMethods.OnlyNumberDecimals(txtLimitCredit);
            HelperMethods.OnlyNumberInt(txtTerm);
            txtLimitCredit.Text = TbCustomerCreditLineDto.LimitCredit.HasValue ? TbCustomerCreditLineDto.LimitCredit.Value.ToString("N2") : @"300000";
            
        }

        private void LoadValues()
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
                var permited = objInterfazCoreWebPermission.UrlPermited("app_cxc_customer", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(notAccessControl);
                }

                var resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd("app_cxc_customer", "index", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    throw new Exception(notAllEdit);
                }
            }

            if (TbCustomerCreditLineDto.CustomerCreditLineId > 0)
            {
                originalName = TbCustomerCreditLineDto.CreditLineName;
            }

            ObjListLine = creditLineModel.GetRowByCompany(user.CompanyID);
            ObjCurrencyList = companyCurrencyModel.GetByCompany(user.CompanyID);
            ObjListWorkflowStage = objInterfazCoreWebWorkflow.GetWorkflowInitStage("tb_customer_credit_line", "statusID", user.CompanyID, user.BranchID, role.RoleID);
            ObjListPay = objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer_credit_line", "periodPay", user.CompanyID);
            ObjListTypeAmortization = objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer_credit_line", "typeAmortization", user.CompanyID);
            ObjParameterCurrenyDefault = objInterfazCoreWebParameter.GetParameter("ACCOUNTING_CURRENCY_NAME_FUNCTION", user.CompanyID);
            ObjParameterAmortizationDefault = objInterfazCoreWebParameter.GetParameter("CXC_TYPE_AMORTIZATION", user.CompanyID);
            ObjParameterInteresDefault = objInterfazCoreWebParameter.GetParameter("CXC_INTERES_DEFAULT", user.CompanyID);
            ObjParameterPayDefault = objInterfazCoreWebParameter.GetParameter("CXC_FRECUENCIA_PAY_DEFAULT", user.CompanyID);
            ObjParameterCxcPlazoDefault = objInterfazCoreWebParameter.GetParameterValue("CXC_PLAZO_DEFAULT", user.CompanyID);
            ObjParameterCxcDayExcludedInCredit = objInterfazCoreWebParameter.GetParameterValue("CXC_DAY_EXCLUDED_IN_CREDIT", user.CompanyID);
            ObjListDayExcluded = objInterfazCoreWebCatalog.GetCatalogAllItem("tb_customer_credit_line", "dayExcluded", user.CompanyID);
        }

        private void RenderValues()
        {
            if (ObjListWorkflowStage is null)
            {
                throw new Exception("No se ha configurado el Workflow");
            }

            if (ObjParameterInteresDefault is null)
            {
                throw new Exception("Configure el parametro de interes por default");
            }

            if (ObjParameterPayDefault is null)
            {
                throw new Exception("Configure el parametro de pago por default");
            }

            if (ObjParameterAmortizationDefault is null)
            {
                throw new Exception("Configure el parametro de Amortización por default");
            }

            if (TbCustomerCreditLineDto.CustomerCreditLineId > 0)
            {
                CoreWebRenderInView.LlenarComboBox(ObjListLine, txtCreditLineID, "CreditLineID", "Name", TbCustomerCreditLineDto.CreditLineId);
                CoreWebRenderInView.LlenarComboBox(ObjCurrencyList, txtCurrencyID, "CurrencyId", "Name", TbCustomerCreditLineDto.CurrencyId);
                CoreWebRenderInView.LlenarComboBox(ObjListWorkflowStage, txtStatusID, "WorkflowStageID", "Name", TbCustomerCreditLineDto.StatusId);
                CoreWebRenderInView.LlenarComboBox(ObjListPay, txtPeriodPay, "CatalogItemID", "Name", TbCustomerCreditLineDto.PeriodPay);
                CoreWebRenderInView.LlenarComboBox(ObjListTypeAmortization, txtTypeAmorization, "CatalogItemID", "Name", TbCustomerCreditLineDto.TypeAmortization);
                CoreWebRenderInView.LlenarComboBox(ObjListDayExcluded, txtDayExcluded, "CatalogItemID", "Name", TbCustomerCreditLineDto.DayExcluded);
                txtInteresYear.EditValue = TbCustomerCreditLineDto.InterestYear;
                txtTerm.EditValue = TbCustomerCreditLineDto.Term;
                txtLimitCredit.Text = TbCustomerCreditLineDto.LimitCredit!.Value.ToString("N2");
                txtNumber.Text = TbCustomerCreditLineDto.AccountNumber;
                txtNote.Text = TbCustomerCreditLineDto.Note;
            }
            else
            {
                CoreWebRenderInView.LlenarComboBox(ObjListLine, txtCreditLineID, "CreditLineID", "Name", ObjListLine.ElementAt(0).CreditLineID);
                CoreWebRenderInView.LlenarComboBox(ObjCurrencyList, txtCurrencyID, "CurrencyId", "Name", ObjCurrencyList.First(dto => dto.Name == ObjParameterCurrenyDefault!.Value).CurrencyId);
                CoreWebRenderInView.LlenarComboBox(ObjListWorkflowStage, txtStatusID, "WorkflowStageID", "Name", ObjListWorkflowStage.ElementAt(0).WorkflowStageID);
                CoreWebRenderInView.LlenarComboBox(ObjListPay, txtPeriodPay, "CatalogItemID", "Name", ObjListPay.First(item => item.CatalogItemID == Convert.ToInt32(ObjParameterPayDefault.Value)).CatalogItemID);
                CoreWebRenderInView.LlenarComboBox(ObjListTypeAmortization, txtTypeAmorization, "CatalogItemID", "Name", ObjListTypeAmortization.First(item => item.CatalogItemID == Convert.ToInt32(ObjParameterAmortizationDefault.Value)).CatalogItemID);
                CoreWebRenderInView.LlenarComboBox(ObjListDayExcluded, txtDayExcluded, "CatalogItemID", "Name", Convert.ToInt32(ObjParameterCxcDayExcludedInCredit));
                txtInteresYear.EditValue = ObjParameterInteresDefault.Value;
                txtTerm.EditValue = ObjParameterCxcPlazoDefault;
                txtLimitCredit.Text = @"300000.00";
            }
        }

        private string? ObjParameterCxcPlazoDefault;

        private TbCompanyParameter? ObjParameterPayDefault;

        private TbCompanyParameter? ObjParameterInteresDefault;

        private TbCompanyParameter? ObjParameterAmortizationDefault;

        private TbCompanyParameter? ObjParameterCurrenyDefault;

        private List<TbCatalogItem>? ObjListTypeAmortization = [];

        private List<TbCatalogItem>? ObjListPay = [];

        private List<TbWorkflowStage>? ObjListWorkflowStage;

        private List<TbCompanyCurrencyDto>? ObjCurrencyList = [];

        private List<TbCreditLine>? ObjListLine = [];

        private List<TbCatalogItem>? ObjListDayExcluded = [];

        private string? ObjParameterCxcDayExcludedInCredit;
    }
}