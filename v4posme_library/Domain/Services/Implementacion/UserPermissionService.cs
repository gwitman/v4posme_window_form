using DevExpress.Xpo;
using System.Collections.Generic;
using System.Linq;
using v4posme_library.Domain.Services.Interfaz;
using v4posme_library.ModelsCode;
using v4posme_library.ModelsViews;
namespace v4posme_library.Domain.Services.Implementacion
{
    public class UserPermissionService : IUserPermissionService
    {

        public List<UserPermissionView> GetRowByCompanyIDyBranchIDyRoleId(int companyId, int branchId, int roleId)
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
                var up = new UserPermissionView
                {
                    Selected = item.Selected,
                    Deleted = item.Deleted,
                    Inserted = item.Inserted,
                    Edited = item.Edited,
                    Orden = item.Orden,
                    Display = item.Display,
                    BranchId = item.BranchID,
                    CompanyId = item.CompanyID,
                    ElementId = item.ElementID
                };
                list.Add(up);
            }
            return list;
        }
    }
}
