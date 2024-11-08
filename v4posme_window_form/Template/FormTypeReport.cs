using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using Unity;
using v4posme_window.Libraries;
using DevExpress.XtraEditors;
using v4posme_library.Models;

namespace v4posme_window.Template;

public partial class FormTypeReport : XtraForm
{
    private readonly ICoreWebPermission objInterfazCoreWebPermission = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebPermission>();

    public FormTypeReport()
    {
        InitializeComponent();
    }

    protected void InicializarLista(string controller)
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
            var permited = objInterfazCoreWebPermission.UrlPermited(controller, "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (!permited)
            {
                throw new Exception(notAccessControl);
            }

            resultPermission = objInterfazCoreWebPermission.UrlPermissionCmd(controller ,"index", urlSuffix!, role, user, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (resultPermission == permissionNone)
            {
                throw new Exception(notAccessFunction);
            }

            parentMenuElement = objInterfazCoreWebPermission.GetElementId(controller, "index", urlSuffix, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
        }

        CoreWebRenderInView.RenderListViewReport(ListBoxControlReport, VariablesGlobales.Instance.ListMenuBodyReport, parentMenuElement, svgImageCollection);
    }

    private void ListBoxControlReport_HtmlElementMouseClick(object sender, ListBoxHtmlElementMouseEventArgs e)
    {
        var appUrlResourceCssJs = VariablesGlobales.ConfigurationBuilder["APP_URL_RESOURCE_CSS_JS"];
        var urlSuffixOld = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX_OLD"];
        var urlSuffixNew = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX_NEW"];
        var frmWebView = new FormTypeWebView();
        if (e.Item is TbMenuElement tbMenuElement)
        {
            frmWebView.Text = tbMenuElement.Display ?? "Reporte";
            var address = tbMenuElement.Address!.Replace(urlSuffixOld!, urlSuffixNew);
            var webToken = VariablesGlobales.ConfigurationBuilder["WEB_TOKEN"];
            var url = $"{appUrlResourceCssJs}/{address}?webtoken={webToken}";
            frmWebView.webView.Source = new Uri(url);
            frmWebView.Show();
        }
    }
}