using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Interfaz
{
    public interface ICoreWebPermission
    {
        int GetElementId(string? controller, string? method, string? suffix, List<TbMenuElement> dataMenuTop,
            List<TbMenuElement> dataMenuLeft, List<TbMenuElement> dataMenuBodyReport,
            List<TbMenuElement> dataMenuBodyTop, List<TbMenuElement> dataMenuHiddenPopup);

        bool UrlPermited(string? controller, string? method, string? suffix, List<TbMenuElement>? dataMenuTop,
            List<TbMenuElement>? dataMenuLeft, List<TbMenuElement>? dataMenuBodyReport,
            List<TbMenuElement>? dataMenuBodyTop, List<TbMenuElement>? dataMenuHiddenPopup);

        int UrlPermissionCmd(string? controller, string? method, string? suffix, TbRole? role, TbUser? user,
            List<TbMenuElement>? dataMenuTop, List<TbMenuElement>? dataMenuLeft,
            List<TbMenuElement>? dataMenuBodyReport,
            List<TbMenuElement>? dataMenuBodyTop, List<TbMenuElement>? dataMenuHiddenPopup);

        void GetValueLicense(int companyId, string? url);
       
    }
}