using Microsoft.EntityFrameworkCore;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Models;
using v4posme_library.ModelsViews;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion
{
    public class UserPermissionService : IUserPermissionService
    {

        public List<UserPermissionView> GetRowByCompanyIDyBranchIDyRoleId(int companyId, int branchId, int roleId)
        {
            if (companyId == 0 || branchId == 0 || roleId == 0) return new List<UserPermissionView>();
            var sql = $$"""
                        select tb_user_permission.companyID,tb_user_permission.branchID,tb_user_permission.roleID,tb_user_permission.elementID,
                        tb_user_permission.selected,tb_user_permission.inserted,tb_user_permission.deleted,
                        tb_user_permission.edited,tb_menu_element.orden,tb_menu_element.display
                        from tb_user_permission
                        inner join  tb_element on tb_element.elementID = tb_user_permission.elementID
                        inner join  tb_menu_element on tb_menu_element.elementID = tb_user_permission.elementID
                        where tb_user_permission.companyID = {{companyId}}
                        and tb_user_permission.branchID = {{branchId}}
                        and tb_user_permission.roleID = {{roleId}}
                                     and tb_menu_element.companyID = {{companyId}}
                        """;
            using var context = new DataContext();
            var query = context.Database.SqlQueryRaw<UserPermissionView>(sql);
            return query.ToList();
        }
    }
}
