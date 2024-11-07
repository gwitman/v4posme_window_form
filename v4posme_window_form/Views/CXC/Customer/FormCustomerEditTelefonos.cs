using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.ModelsDto;
using v4posme_window.Libraries;
using v4posme_window.Template;

namespace v4posme_window.Views.CXC.Customer
{
    public partial class FormCustomerEditTelefonos : FormTypeHeadModal
    {
        private readonly ICoreWebPermission objInterfazCoreWebPermission = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebPermission>();
        private readonly ICoreWebCatalog objInterfazCoreWebCatalog = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCatalog>();

        public FormCustomerEditTelefonos()
        {
            InitializeComponent();
            btnCancelar.Click += BtnCancelarOnClick;
            btnAceptar.Click += BtnAceptar_Click;
            TbEntityPhoneDto = new();
        }

        private void BtnAceptar_Click(object? sender, EventArgs e)
        {
            var selectedType = txtEntityPhoneTypeID.SelectedItem as ComboBoxItem;
            if (selectedType is null)
            {
                dxErrorProvider.SetError(txtEntityPhoneTypeID, "Debe especificar este campo para continuar");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEntityPhoneNumber.Text))
            {
                dxErrorProvider.SetError(txtEntityPhoneNumber, "Debe especificar este campo para continuar");
                return;
            }

            TbEntityPhoneDto = new TbEntityPhoneDto
            {
                Number = txtEntityPhoneNumber.Text,
                TypeId = Convert.ToInt32(selectedType.Key),
                TypeIdDescription = selectedType.Value!.ToString(),
                IsPrimary = (sbyte?)(txtIsPrimary.Checked ? 1 : 0)
            };
            DialogResult = DialogResult.OK;
        }

        private void BtnCancelarOnClick(object? sender, EventArgs e)
        {
            Close();
        }

        private void FormCustomerEditTelefonos_Load(object sender, EventArgs e)
        {
            LoadRender();
            LoadValues();
        }

        private void LoadRender()
        {
            txtEntityPhoneNumber.Properties.Mask.EditMask = @"+(999) 0000-0000";
            txtEntityPhoneNumber.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Regular;
            txtEntityPhoneNumber.Properties.Mask.SaveLiteral = true;
            txtEntityPhoneNumber.Properties.Mask.UseMaskAsDisplayFormat = true;
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

                var resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd("app_cxc_customer", "add", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    throw new Exception(notAllEdit);
                }
            }

            var objListPhoneTypeID = objInterfazCoreWebCatalog.GetCatalogAllItem("tb_entity_phone", "typeID", user.CompanyID);
            CoreWebRenderInView.LlenarComboBox(objListPhoneTypeID, txtEntityPhoneTypeID, "CatalogItemID", "Name", objListPhoneTypeID.ElementAt(0).CatalogItemID);
        }

        public TbEntityPhoneDto TbEntityPhoneDto { get; set; }
    }
}