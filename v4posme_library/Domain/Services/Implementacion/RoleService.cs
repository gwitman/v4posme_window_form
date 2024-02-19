using v4posme_library.Domain.Services.Interfaz;
using v4posme_library.Models;
namespace v4posme_library.Domain.Services.Implementacion
{
    public class RoleService : IRoleService
    {
        public TbRole GetRowByPk(int companyId, int branchId, int roleId)
        {
            using var context = new DataContext();
            return context.TbRoles.First(role=>role.CompanyId == companyId && role.BranchId == branchId && role.RoleId == roleId && role.IsActive!.Value);
        }
    }
}
