using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class AccountTypeModel : IAccountTypeModel
{
    public void DeleteAppPosme(int companyId, int accountTypeId)
    {
        using var context = new DataContext();
        var find = context.TbAccountTypes
            .Single(account => account.CompanyID == companyId
                               && account.AccountTypeID == accountTypeId);
        find.IsActive = false;
        context.SaveChanges();
    }

    public void UpdateAppPosme(int companyId, int accountTypeId, TbAccountType data)
    {
        using var context = new DataContext();
        var find = context.TbAccountTypes
            .Single(account => account.CompanyID == companyId
                               && account.AccountTypeID == accountTypeId);
        data.AccountTypeID = find.AccountTypeID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int InsertAppPosme(TbAccountType data)
    {
        using var context = new DataContext();
        var add = context.TbAccountTypes.Add(data);
        context.SaveChanges();
        return add.Entity.AccountTypeID;
    }

    public int GetCountInAccount(int companyId, int accountTypeId)
    {
        using var context = new DataContext();
        return context.TbAccounts
            .Count(account => account.IsActive != null
                              && account.IsActive.Value
                              && account.CompanyID == companyId
                              && account.AccountTypeID == accountTypeId);
    }

    public List<TbAccountType> GetByCompany(int companyId)
    {
        using var context = new DataContext();
        return context.TbAccountTypes
            .Where(account => account.IsActive != null
                              && account.IsActive.Value
                              && account.CompanyID == companyId)
            .ToList();
    }

    public TbAccountType GetRowByPk(int companyId, int accountTypeId)
    {
        using var context = new DataContext();
        return context.TbAccountTypes
            .Single(account => account.IsActive != null
                               && account.IsActive.Value
                               && account.CompanyID == companyId
                               && account.AccountTypeID == accountTypeId);
    }
}