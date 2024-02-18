using v4posme_library.ModelsCode;
namespace v4posme_library.Domain.Services.Interfaz
{
    public interface IMembershipService
    {
        Membership GetRowByCompanyIdBranchIdUserId(int companyId, int branchId, int userId);
    }
}
