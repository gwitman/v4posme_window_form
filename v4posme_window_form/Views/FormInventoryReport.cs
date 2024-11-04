using DevExpress.XtraEditors;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Models;
using v4posme_window.Libraries;
using v4posme_window.Template;
using Microsoft.Web.WebView2.Core;

namespace v4posme_window.Views
{
    public partial class FormInventoryReport : XtraForm
    {
        private readonly ICoreWebPermission _objInterfazCoreWebPermission = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebPermission>();
        private readonly ICoreWebMenu _objInterfazCoreWebMenu = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebMenu>();

        public FormInventoryReport()
        {
            InitializeComponent();
        }

        private void FormInventoryReport_Load(object sender, EventArgs e)
        {
            var userNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];
            var notAccessControl = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_CONTROL"];
            var notAccessFunction = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_FUNCTION"];
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
            var parentMenuElement = 0;
            if (appNeedAuthentication == "true")
            {
                var permited = _objInterfazCoreWebPermission.UrlPermited("app_inventory_report", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (!permited)
                {
                    throw new Exception(notAccessControl);
                }

                resultPermission = _objInterfazCoreWebPermission.UrlPermissionCmd("app_inventory_report", "index", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
                if (resultPermission == permissionNone)
                {
                    throw new Exception(notAccessFunction);
                }

                parentMenuElement = _objInterfazCoreWebPermission.GetElementId("app_inventory_report", "index", urlSuffix, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            }

            CoreWebRenderInView.RenderListViewReport(ListBoxControlReport, VariablesGlobales.Instance.ListMenuBodyReport, parentMenuElement, svgImageCollection);
        }

        private void ListBoxControlReport_HtmlElementMouseClick(object sender, ListBoxHtmlElementMouseEventArgs e)
        {
            var appUrlResourceCssJs = VariablesGlobales.ConfigurationBuilder["APP_URL_RESOURCE_CSS_JS"];
            var frmWebView = new FormTypeWebView();
            if (e.Item is TbMenuElement tbMenuElement)
            {
                frmWebView.Text = tbMenuElement.Display ?? "Reporte";
                var url = $"{appUrlResourceCssJs}/{tbMenuElement.Address}";
                frmWebView.webView.Source = new Uri(url);
                frmWebView.Show();
            }
        }
    }
}