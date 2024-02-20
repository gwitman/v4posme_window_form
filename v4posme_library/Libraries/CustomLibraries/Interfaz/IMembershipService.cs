using v4posme_library.Models;
namespace v4posme_library.Libraries.CustomLibraries.Interfaz
{
    public interface IMembershipService
    {
        TbMembership? GetRowByCompanyIdBranchIdUserId(int companyId, int branchId, int userId);
    }
}
