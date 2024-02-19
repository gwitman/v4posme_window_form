using v4posme_library.Models;
namespace v4posme_library.Domain.Services.Interfaz
{
    public interface IRoleService
    {
        TbRole GetRowByPk(int companyId, int branchId, int roleId);
    }
}
