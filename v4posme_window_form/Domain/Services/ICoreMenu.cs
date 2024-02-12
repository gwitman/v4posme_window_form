using System.Collections.Generic;
using v4posme_window_form.Models.Tablas;

namespace v4posme_window_form.Domain.Services
{
    public interface ICoreMenu
    {
        List<MenuElement> GetMenuTop(int companyId, int branchId, int roleId);
        List<MenuElement> GetMenuLeft(int companyId, int branchId, int roleId);
        
        List<MenuElement> GetMenuBodyReport(int companyId, int branchId, int roleId);

        List<MenuElement> GetMenuHiddenPopup(int companyId, int branchId, int roleId);
    }
}
