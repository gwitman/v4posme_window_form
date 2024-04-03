using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Interfaz
{
    public interface ICoreWebMenu
    {
        List<TbMenuElement>? GetMenuTop(int companyId, int branchId, int roleId);

        List<TbMenuElement>? GetMenuLeft(int companyId, int branchId, int roleId);
        
        List<TbMenuElement>? GetMenuBodyReport(int companyId, int branchId, int roleId);

        List<TbMenuElement>? GetMenuHiddenPopup(int companyId, int branchId, int roleId);

      
    }
}
