using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels.Core
{
    public class UserPermissionModel : IUserPermissionModel
    {
        public int InsertAppPosme(TbUserPermission data)
        {
            using var context = new DataContext();
            var add = context.Add(data);
            context.SaveChanges();
            return add.Entity.UserPermissionID;
        }

        public int DeleteByRole(int companyId, int branchId, int roleId)
        {
            using var context = new DataContext();
            return context.TbUserPermissions
                .Where(permission => permission.CompanyID == companyId
                                     && permission.BranchID == branchId
                                     && permission.RoleID == roleId)
                .ExecuteDelete();
        }

        public List<TbUserPermissionDto> GetRowByCompanyIdyBranchIdyRoleId(int companyId, int branchId, int roleId)
        {
            if (companyId == 0 || branchId == 0 || roleId == 0) return [];
            using var context = new DataContext();
            var query = from userPermission in context.TbUserPermissions.AsNoTracking()
                join element in context.TbElements.AsNoTracking() on userPermission.ElementID equals element.ElementID
                join menuElement in context.TbMenuElements.AsNoTracking() on userPermission.ElementID equals menuElement.ElementID
                where userPermission.CompanyID == companyId
                      && userPermission.BranchID == branchId
                      && userPermission.RoleID == roleId
                      && menuElement.CompanyID == companyId
                select new TbUserPermissionDto
                {
                    CompanyId = userPermission.CompanyID,
                    BranchId = userPermission.BranchID,
                    RoleId = userPermission.RoleID,
                    ElementId = userPermission.ElementID,
                    Selected = userPermission.Selected,
                    Inserted = userPermission.Inserted,
                    Deleted = userPermission.Deleted,
                    Edited = userPermission.Edited,
                    MenuElementOrden = menuElement.Orden,
                    MenuElementDisplay = menuElement.Display
                };

            return query.ToList();
        }

        public TbUserPermission? GetRowByPk(int companyId, int branchId, int roleId, int elementId)
        {
            using var context = new DataContext();
            return context.TbUserPermissions.AsNoTracking()
                .FirstOrDefault(permission => permission!.CompanyID == companyId
                                              && permission.BranchID == branchId
                                              && permission.RoleID == roleId
                                              && permission.ElementID == elementId);
        }
    }
}