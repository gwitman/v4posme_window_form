using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public class AccountLevelModel : IAccountLevelModel
{
    public void DeleteAppPosme(int companyId, int accountLevelId)
    {
        using var context = new DataContext();
        var find = context.TbAccountLevels.Single(account =>
            account.CompanyID == companyId && account.AccountLevelID == accountLevelId);
        find.IsActive = false;
        context.SaveChanges();
    }

    public void UpdateAppPosme(int companyId, int accountLevelId, TbAccountLevel data)
    {
        using var context = new DataContext();
        var find = context.TbAccountLevels.Single(account =>
            account.CompanyID == companyId && account.AccountLevelID == accountLevelId);
        data.AccountLevelID = find.AccountLevelID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int InsertAppPosme(TbAccountLevel data)
    {
        using var context = new DataContext();
        var insert = context.TbAccountLevels.Add(data);
        context.SaveChanges();
        return insert.Entity.AccountLevelID;
    }

    public int GetCountInAccount(int companyId, int accountLevelId)
    {
        using var context = new DataContext();
        return context.TbAccounts
            .Count(account => account.IsActive != null
                              && account.IsActive.Value
                              && account.CompanyID == companyId &&
                              account.AccountLevelID == accountLevelId);
    }

    public List<TbAccountLevel> GetByCompany(int companyId)
    {
        using var context = new DataContext();
        return context.TbAccountLevels
            .Where(account => account.IsActive
                              && account.CompanyID == companyId)
            .ToList();
    }

    public TbAccountLevel GetRowByPk(int companyId, int accountLevelId)
    {
        using var context = new DataContext();
        return context.TbAccountLevels
            .Single(account => account.IsActive
                               && account.CompanyID == companyId
                               && account.AccountLevelID == accountLevelId);
    }
}