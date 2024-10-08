using Microsoft.EntityFrameworkCore.ChangeTracking;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class AccountModel : IAccountModel
{
    public TbAccount? GetRowByPk(int companyId, int accountId)
    {
        using var context = new DataContext();
        return context.TbAccounts.SingleOrDefault(account =>
            account.IsActive!.Value && account.AccountID == accountId && account.CompanyID == companyId);
    }

    public void DeleteAppPosme(int companyId, int accountId)
    {
        using var context = new DataContext();
        var find = GetRowByPk(companyId, accountId);
        find.IsActive = false;
        context.BulkSaveChanges();
    }

    public void UpdateAppPosme(int companyId, int accountId, TbAccount data)
    {
        using var context = new DataContext();
        var find = GetRowByPk(companyId, accountId);
        data.AccountID = find.AccountID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.BulkSaveChanges();
    }

    public int InsertAppPosme(TbAccount data)
    {
        using var context = new DataContext();
        EntityEntry<TbAccount> add = context.TbAccounts.Add(data);
        context.BulkSaveChanges();
        return add.Entity.AccountID;
    }

    public int GetIsParent(int companyId, int accountId)
    {
        using var context = new DataContext();
        return context.TbAccounts.Count(account =>
            account.IsActive!.Value && account.ParentAccountID == accountId && account.CompanyID == companyId);
    }

    public int GetCountAccount(int companyId)
    {
        using var context = new DataContext();
        return context.TbAccounts.Count(account => account.IsActive!.Value && account.CompanyID == companyId);
    }

    public TbAccount GetByAccountNumber(string? accountNumber, int companyId)
    {
        using var context = new DataContext();
        return context.TbAccounts.Single(account =>
            account.IsActive!.Value && account.CompanyID == companyId && account.AccountNumber.Equals(accountNumber));
    }

    public List<TbAccount> GetByCompany(int companyId)
    {
        using var context = new DataContext();
        return context.TbAccounts.Where(account => account.IsActive!.Value && account.CompanyID == companyId)
            .OrderBy(account => account.AccountNumber)
            .ToList();
    }

    public List<TbAccount> GetByCompanyOperative(int companyId)
    {
        using var context = new DataContext();
        return context.TbAccounts.Where(account =>
                account.IsActive!.Value && account.CompanyID == companyId && account.IsOperative)
            .OrderBy(account => account.AccountNumber)
            .ToList();
    }
}