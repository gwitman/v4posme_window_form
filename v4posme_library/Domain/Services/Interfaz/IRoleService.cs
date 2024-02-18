using v4posme_library.ModelsCode;
namespace v4posme_library.Domain.Services.Interfaz
{
    public interface IRoleService
    {
        Role GetRowByPk(int companyId, int branchId, int roleId);
    }
}
