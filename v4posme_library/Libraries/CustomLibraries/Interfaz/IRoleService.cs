using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Interfaz
{
    public interface IRoleService
    {
        TbRole? GetRowByPk(int companyId, int branchId, int roleId);
    }
}
