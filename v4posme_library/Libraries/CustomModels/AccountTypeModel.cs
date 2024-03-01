using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class AccountTypeModel : IAccountTypeModel
{
    public void DeleteAppPosme(int companyId, int accountTypeId)
    {
        using var context = new DataContext();
        var find = context.TbAccountTypes
            .Single(account => account.CompanyId == companyId
                               && account.AccountTypeId == accountTypeId);
        find.IsActive = false;
        context.BulkSaveChanges();
    }

    public void UpdateAppPosme(int companyId, int accountTypeId, TbAccountType data)
    {
        using var context = new DataContext();
        var find = context.TbAccountTypes
            .Single(account => account.CompanyId == companyId
                               && account.AccountTypeId == accountTypeId);
        data.AccountTypeId = find.AccountTypeId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.BulkSaveChanges();
    }

    public int InsertAppPosme(TbAccountType data)
    {
        using var context = new DataContext();
        var add = context.TbAccountTypes.Add(data);
        context.BulkSaveChanges();
        return add.Entity.AccountTypeId;
    }

    public int GetCountInAccount(int companyId, int accountTypeId)
    {
        using var context = new DataContext();
        return context.TbAccounts
            .Count(account => account.IsActive != null
                              && account.IsActive.Value
                              && account.CompanyId == companyId
                              && account.AccountTypeId == accountTypeId);
    }

    public List<TbAccountType> GetByCompany(int companyId)
    {
        using var context = new DataContext();
        return context.TbAccountTypes
            .Where(account => account.IsActive != null
                              && account.IsActive.Value
                              && account.CompanyId == companyId)
            .ToList();
    }

    public TbAccountType GetRowByPk(int companyId, int accountTypeId)
    {
        using var context = new DataContext();
        return context.TbAccountTypes
            .Single(account => account.IsActive != null
                               && account.IsActive.Value
                               && account.CompanyId == companyId
                               && account.AccountTypeId == accountTypeId);
    }
}