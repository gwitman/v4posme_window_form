using System.Collections.Generic;
using v4posme_library.ModelsCode;

namespace v4posme_library.Domain.Services
{
    public interface ICoreMenuService
    {
        List<MenuElement> GetMenuTop(int companyId, int branchId, int roleId);

        List<MenuElement> GetMenuLeft(int companyId, int branchId, int roleId);
        
        List<MenuElement> GetMenuBodyReport(int companyId, int branchId, int roleId);

        List<MenuElement> GetMenuHiddenPopup(int companyId, int branchId, int roleId);

        List<string> RenderMenuLeft(Company company, List<MenuElement> menuElements);
    }
}
