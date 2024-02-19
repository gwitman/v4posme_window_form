using v4posme_library.Models;
namespace v4posme_library.Domain.Services.Interfaz
{
    public interface IMembershipService
    {
        TbMembership GetRowByCompanyIdBranchIdUserId(int companyId, int branchId, int userId);
    }
}
