using v4posme_library.Models;
namespace v4posme_library.Libraries.Services.Interfaz
{
    public interface IRoleService
    {
        TbRole? GetRowByPk(int companyId, int branchId, int roleId);
    }
}
