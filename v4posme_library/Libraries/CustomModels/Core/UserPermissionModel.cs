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
            return add.Entity.UserPermissionId;
        }

        public int DeleteByRole(int companyId, int branchId, int roleId)
        {
            using var context = new DataContext();
            return context.TbUserPermissions
                .Where(permission => permission.CompanyId == companyId
                                     && permission.BranchId == branchId
                                     && permission.RoleId == roleId)
                .ExecuteDelete();
        }

        public List<TbUserPermissionDto> GetRowByCompanyIdyBranchIdyRoleId(int companyId, int branchId, int roleId)
        {
            if (companyId == 0 || branchId == 0 || roleId == 0) return [];
            using var context = new DataContext();
            var query = from userPermission in context.TbUserPermissions
                join element in context.TbElements on userPermission.ElementId equals element.ElementId
                join menuElement in context.TbMenuElements on userPermission.ElementId equals menuElement.ElementId
                where userPermission.CompanyId == companyId
                      && userPermission.BranchId == branchId
                      && userPermission.RoleId == roleId
                      && menuElement.CompanyId == companyId
                select new TbUserPermissionDto
                {
                    CompanyId = userPermission.CompanyId,
                    BranchId = userPermission.BranchId,
                    RoleId = userPermission.RoleId,
                    ElementId = userPermission.ElementId,
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
            return context.TbUserPermissions
                .FirstOrDefault(permission => permission!.CompanyId == companyId
                                              && permission.BranchId == branchId
                                              && permission.RoleId == roleId
                                              && permission.ElementId == elementId);
        }
    }
}