using v4posme_library.Models;
namespace v4posme_library.Libraries.Services.Interfaz
{
    public interface ICoreMenuService
    {
        List<TbMenuElement>? GetMenuTop(int companyId, int branchId, int roleId);

        List<TbMenuElement>? GetMenuLeft(int companyId, int branchId, int roleId);
        
        List<TbMenuElement>? GetMenuBodyReport(int companyId, int branchId, int roleId);

        List<TbMenuElement>? GetMenuHiddenPopup(int companyId, int branchId, int roleId);

        List<string>? RenderMenuLeft(TbCompany? company, List<TbMenuElement>? menuElements);
    }
}
