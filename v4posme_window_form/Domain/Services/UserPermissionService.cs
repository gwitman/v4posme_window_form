using DevExpress.Xpo;
using System.Collections.Generic;
using System.Linq;
using v4posme_window_form.Models;
using v4posme_window_form.Models.Tablas;
namespace v4posme_window_form.Domain.Services
{
    public class UserPermissionService : IUserPermissionService
    {

        public List<UserPermissionView> getRowByCompanyIDyBranchIDyRoleID(int companyId, int branchId, int roleId)
        {
            if (companyId == 0 || branchId == 0 || roleId == 0) return null;
            List<UserPermissionView> list = new List<UserPermissionView>();
            var userPermission = Session.DefaultSession.Query<UserPermission>();
            var element = Session.DefaultSession.Query<Element>();
            var menuElement = Session.DefaultSession.Query<MenuElement>();
            var result = from up in userPermission
                join el in element on up.ElementID equals el.ElementID
                join menu in menuElement on el.ElementID equals menu.ElementID
                where up.CompanyID == companyId && up.BranchID == branchId && up.RoleID == roleId
                select new {up.CompanyID, up.BranchID, up.RoleID, up.ElementID, up.Selected, up.Inserted, up.Deleted, up.Edited, menu.Orden, menu.Display};
            foreach (var item in result)
            {
                var up = new UserPermissionView();
                up.Selected = item.Selected;
                up.Deleted = item.Deleted;
                up.Inserted = item.Inserted;
                up.Edited = item.Edited;
                up.Orden = item.Orden;
                up.Display = item.Display;
                up.BranchId = item.BranchID;
                up.CompanyId = item.CompanyID;
                up.ElementId = item.ElementID;
                list.Add(up);
            }
            return list;
        }
    }
}
