using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomHelper;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_window.Interfaz;
using v4posme_window.Libraries;
using v4posme_window.Template;
using GridView = DevExpress.XtraGrid.Views.Grid.GridView;

namespace v4posme_window.Views
{
    public sealed partial class FormInvoiceBillingList : FormTypeList, IFormTypeList
    {
        private readonly CoreWebRenderInView _coreWebRender = new CoreWebRenderInView();
        private readonly WebToolsHelper _webToolsHelper = new WebToolsHelper();

        private readonly ICoreWebPermission _coreWebPermission =
            VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebPermission>();

        private readonly ICoreWebTools _coreWebTools =
            VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();

        private readonly ICoreWebView _coreWebView = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebView>();

        private int? dataViewId = 0;
        private DateTime? fecha = DateTime.Now;

        public GridView ObjControlGridView { get; set; }

        public FormInvoiceBillingList()
        {
            ObjControlGridView = new GridView();
            InitializeComponent();
            lblTitulo.Text = @"LISTA DE FACTURAS";
            Text = lblTitulo.Text;


            btnEditar.Click += Edit;
            btnEliminar.Click += Delete;
            btnNuevo.Click += New;
        }


        private void FormInvoiceBillingList_Load(object sender, EventArgs e)
        {
            var coreWebRenderInView = new CoreWebRenderInView();
            var resultPermission = 0;
            var appNeedAuthentication = VariablesGlobales.ConfigurationBuilder["APP_NEED_AUTHENTICATION"];
            var permissionNone = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_NONE"]);
            var urlSuffix = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX"];
            if (appNeedAuthentication!.Equals("true"))
            {
                var permited = _coreWebPermission.UrlPermited("app_invoice_billing", "index", urlSuffix,
                    VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                    VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                    VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    _coreWebRender.GetMessageAlert(TypeError.Error, "Permisos", "No tiene acceso a los controles",
                        this);
                    return;
                }

                resultPermission = _coreWebPermission.UrlPermissionCmd("app_invoice_billing", "index", urlSuffix,
                    VariablesGlobales.Instance.Role, VariablesGlobales.Instance.User,
                    VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                    VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                    VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    _coreWebRender.GetMessageAlert(TypeError.Error, "Permisos", "No se encontraron permisos", this);
                    return;
                }
            }

            var objComponent = _coreWebTools.GetComponentIdByComponentName("tb_transaction_master_billing");
            if (objComponent is null)
            {
                _coreWebRender.GetMessageAlert(TypeError.Error, "Error",
                    "00409 EL COMPONENTE 'tb_transaction_master_billing' NO EXISTE...", this);
                return;
            }

            var callerIdList = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["CALLERID_LIST"]);
            Dictionary<string, string> parameters;
            if (dataViewId == 0)
            {
                var targetComponentId = VariablesGlobales.Instance.Company.FlavorId;
                parameters = new Dictionary<string, string>
                {
                    { "{companyID}", VariablesGlobales.Instance.User.CompanyId.ToString() },
                    { "{fecha}", fecha.Value.ToShortDateString() }
                };
                var dataViewData = _coreWebView.GetViewDefault(VariablesGlobales.Instance.User,
                    objComponent.ComponentId, callerIdList, targetComponentId, resultPermission);
                {
                    targetComponentId = 0;
                    parameters = new Dictionary<string, string>
                    {
                        { "{companyID}", VariablesGlobales.Instance.User.CompanyId.ToString() },
                        { "{fecha}", fecha.Value.ToShortDateString() }
                    };
                    dataViewData = _coreWebView.GetViewDefault(VariablesGlobales.Instance.User,
                        objComponent.ComponentId, callerIdList, targetComponentId, resultPermission, parameters);
                }

                ObjControlGridView = coreWebRenderInView.RenderGrid(dataViewData!, "invoice", 0, centerPane);
            }
        }

        public void List()
        {
            throw new NotImplementedException();
        }

        public void Delete(object? sender, EventArgs? args)
        {
            new FormInvoiceBillingEdit(TypeOpenForm.NotInit, 0, 0, 0).ComandDelete();
        }

        public void Edit(object? sender, EventArgs? args)
        {
            new FormInvoiceBillingEdit(TypeOpenForm.Init,0,0,0).ShowDialog();
        }

        public void New(object? sender, EventArgs? args)
        {
            new FormInvoiceBillingEdit(TypeOpenForm.Init, 0, 0, 2).ShowDialog();
        }
    }
}