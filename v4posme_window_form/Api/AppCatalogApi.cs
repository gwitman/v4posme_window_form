using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using Unity;
using v4posme_library.Models;

namespace v4posme_window.Api;

public class AppCatalogApi
{
    private readonly ICoreWebCatalog objInterfazCoreWebCatalog = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCatalog>();

    public List<TbCatalogItem>? GetCatalogItemByParentCatalogItemId(string tableName, string fieldName, int catalogItemId)
    {
        var user = VariablesGlobales.Instance.User;
        var userNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];
        if (user is null)
        {
            throw new Exception(userNotAutenticated);
        }

        return objInterfazCoreWebCatalog.GetCatalogAllItemParent(tableName, fieldName, user.CompanyID, catalogItemId);
    }
}