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
                .Where(role => role.CompanyId == companyId
                               && role.BranchId == branchId
                               && role.RoleId == roleId)
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
            return add.Entity.RoleId;
        }

        public List<TbRole> GetRowByCompanyIDyBranchId(int companyId, int branchId)
        {
            using var context = new DataContext();
            return context.TbRoles.AsNoTracking()
                .Where(role => role.CompanyId == companyId
                               && role.BranchId == branchId
                               && role.IsActive!.Value
                               && !role.IsAdmin!.Value)
                .ToList();
        }

        public TbRole? GetRowByPk(int companyId, int branchId, int roleId)
        {
            using var context = new DataContext();
            return context.TbRoles.AsNoTracking().FirstOrDefault(role => role.CompanyId == companyId
                                                          && role.BranchId == branchId
                                                          && role.RoleId == roleId
                                                          && role.IsActive!.Value);
        }
    }
}