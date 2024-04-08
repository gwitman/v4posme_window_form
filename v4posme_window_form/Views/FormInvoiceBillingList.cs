using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomHelper;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_window.Interfaz;
using v4posme_window.Libraries;
using v4posme_window.Template;

namespace v4posme_window.Views
{
    public sealed partial class FormInvoiceBillingList : FormTypeList, InterfaceFormTypeList
    {
        private readonly CoreWebRenderInView _coreWebRender = new CoreWebRenderInView();

        private readonly WebToolsHelper _webToolsHelper = new WebToolsHelper();

        private readonly ICoreWebPermission _coreWebPermission =
            VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebPermission>();

        private readonly ICoreWebTools _coreWebTools =
            VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();

        private readonly ICoreWebView _coreWebView = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebView>();

        private int? dataViewId = 0;
        private DateTime? fecha;

        public GridView objControlGridView
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public FormInvoiceBillingList()
        {
            InitializeComponent();
            lblTitulo.Text = @"LISTA DE FACTURAS";
            Text = lblTitulo.Text;
        }

        public void List()
        {
            throw new NotImplementedException();
        }

        private void FormInvoiceBillingList_Load(object sender, EventArgs e)
        {
            bool resultPermission = true;
            var appNeedAuthentication = VariablesGlobales.ConfigurationBuilder["APP_NEED_AUTHENTICATION"];
            var urlSuffix = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX"];
            if (appNeedAuthentication!.Equals("true"))
            {
                var permited = _coreWebPermission.UrlPermited("app_invoice_billing", "index", urlSuffix,
                    VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                    VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                    VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    _coreWebRender.GetMessageAlert(TypeMessage.Error, "Permisos", "No tiene acceso a los controles",
                        this);
                    return;
                }

                resultPermission = _coreWebPermission.UrlPermissionCmd("app_invoice_billing", "index", urlSuffix,
                    VariablesGlobales.Instance.Role, VariablesGlobales.Instance.User,
                    VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft,
                    VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop,
                    VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!resultPermission)
                {
                    _coreWebRender.GetMessageAlert(TypeMessage.Error, "Permisos", "No se encontraron permisos",
                        this);
                    return;
                }
            }

            var objComponent = _coreWebTools.GetComponentIdByComponentName("tb_transaction_master_billing");
            if (objComponent is null)
            {
                _coreWebRender.GetMessageAlert(TypeMessage.Error, "Error",
                    "00409 EL COMPONENTE 'tb_transaction_master_billing' NO EXISTE...", this);
                return;
            }

            var calleridList = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["CALLERID_LIST"]);
            if (dataViewId is null)
            {
                var targetComponentId = VariablesGlobales.Instance.Company.FlavorId;
                var parameters = new Dictionary<string, string>
                {
                    { "companyID", VariablesGlobales.Instance.User.CompanyId.ToString() },
                    { "companyID", fecha.Value.ToShortDateString() }
                };
                var dataViewData = _coreWebView.GetViewDefault(VariablesGlobales.Instance.User,
                    objComponent.ComponentId, calleridList, targetComponentId, resultPermission ? 1 : -1, parameters);
                if (dataViewData is null)
                {
                    targetComponentId = 0;
                    parameters = new Dictionary<string, string>
                    {
                        { "companyID", VariablesGlobales.Instance.User.CompanyId.ToString() },
                        { "companyID", fecha.Value.ToShortDateString() }
                    };
                    dataViewData = _coreWebView.GetViewDefault(VariablesGlobales.Instance.User,
                        objComponent.ComponentId, calleridList, targetComponentId, resultPermission ? 1 : -1, parameters);
                }

                _coreWebRender.RenderGrid(dataViewData!, "invoice", 0, centerPane);
            }
        }
    }
}