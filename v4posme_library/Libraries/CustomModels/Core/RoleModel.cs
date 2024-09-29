using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core
{
    public class RoleModel : IRoleModel
    {
        public int UpdateAppPosme(int companyId, int branchId, int roleId, TbRole obj)
        {
            using var context = new DataContext();
            return context.TbRoles
                .Where(role => role.CompanyID == companyId
                               && role.BranchID == branchId
                               && role.RoleID == roleId)
                .ExecuteUpdate(calls => calls
                    .SetProperty(role => role.Name, obj.Name)
                    .SetProperty(role => role.Description, obj.Description)
                    .SetProperty(role => role.UrlDefault, obj.UrlDefault)
                    .SetProperty(role => role.IsAdmin, obj.IsAdmin)
                    .SetProperty(role => role.IsActive, obj.IsActive));
        }

        public int InsertAppPosme(TbRole obj)
        {
            using var context = new DataContext();
            var add = context.Add(obj);
            context.SaveChanges();
            return add.Entity.RoleID;
        }

        public List<TbRole> GetRowByCompanyIDyBranchId(int companyId, int branchId)
        {
            using var context = new DataContext();
            return context.TbRoles.AsNoTracking()
                .Where(role => role.CompanyID == companyId
                               && role.BranchID == branchId
                               && role.IsActive!.Value
                               && !role.IsAdmin!.Value)
                .ToList();
        }

        public TbRole? GetRowByPk(int companyId, int branchId, int roleId)
        {
            using var context = new DataContext();
            return context.TbRoles.AsNoTracking().FirstOrDefault(role => role.CompanyID == companyId
                                                          && role.BranchID == branchId
                                                          && role.RoleID == roleId
                                                          && role.IsActive!.Value);
        }
    }
}