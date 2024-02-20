using v4posme_library.Models;
namespace v4posme_library.Libraries.ComponentModels;

class AccountModel : IAccountModel
{
    public TbAccount GetRowByPk(int companyId, int accountId)
    {
        using var context = new DataContext();
        return context.TbAccounts.Single(account=>account.IsActive!.Value && account.AccountId == accountId && account.CompanyId == companyId);
    }
}
