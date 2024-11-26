using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;
using v4posme_library.ModelsDto;

namespace v4posme_window.Api;

public class FormCxcApi
{
    private readonly ICoreWebPermission objInterfazCoreWebPermission = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebPermission>();

    private readonly ICustomerCreditDocumentModel customerCreditDocumentModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerCreditDocumentModel>();

    public List<TbCustomerCreditDocumentDto> GetCustomerBalance(int customerId, int currencId)
    {
        var userNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];
        var notAccessControl = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_CONTROL"];
        var appNeedAuthentication = VariablesGlobales.ConfigurationBuilder["APP_NEED_AUTHENTICATION"];
        var notParameter = VariablesGlobales.ConfigurationBuilder["NOT_PARAMETER"];
        var urlSuffix = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX"];
        var user = VariablesGlobales.Instance.User;

        if (user is null)
        {
            throw new Exception(userNotAutenticated);
        }

        if (appNeedAuthentication == "true")
        {
            var permited = objInterfazCoreWebPermission.UrlPermited("app_cxc_api", "index", urlSuffix!, VariablesGlobales.Instance.ListMenuTop, VariablesGlobales.Instance.ListMenuLeft, VariablesGlobales.Instance.ListMenuBodyReport, VariablesGlobales.Instance.ListMenuBodyTop, VariablesGlobales.Instance.ListMenuHiddenPopup);
            if (!permited)
            {
                throw new Exception(notAccessControl);
            }
        }

        var companyId = user.CompanyID;
        if (companyId<=0)
        {
            throw new Exception(notParameter);
        }
        return customerCreditDocumentModel.GetRowByEntityApplied(companyId, customerId, currencId);
    }
}