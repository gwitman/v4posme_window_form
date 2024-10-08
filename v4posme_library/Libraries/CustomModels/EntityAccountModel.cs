using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class EntityAccountModel : IEntityAccountModel
{
    public void UpdateAppPosme(int entityAccountId, TbEntityAccount data)
    {
        using var context = new DataContext();
        var find = context.TbEntityAccounts
            .Find(entityAccountId);
        if (find is null) return;
        data.EntityAccountID = find.EntityAccountID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int InsertAppPosme(TbEntityAccount data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.EntityAccountID;
    }

    public void DeleteAppPosme(int entityAccountId)
    {
        using var context = new DataContext();
        var find = context.TbEntityAccounts
            .Find(entityAccountId);
        if (find is null) return;
        find.IsActive = false;
        context.SaveChanges();
    }

    public List<TbEntityAccount> GetRowByEntity(int companyId, int componentId, int componentItemId)
    {
        using var context = new DataContext();
        return context.TbEntityAccounts
            .Where(account => account.CompanyID == companyId
                              && account.ComponentID == componentId
                              && account.ComponentItemID == componentItemId
                              && account.IsActive)
            .ToList();
    }

    public TbEntityAccount GetRowByPk(int entityAccountId)
    {
        using var context = new DataContext();
        return context.TbEntityAccounts
            .Single(account => account.EntityAccountID == entityAccountId
                               && account.IsActive);
    }

    public TbEntityAccount GetRowByAccountId(int companyId, int componentId, int componentItemId, int accountId)
    {
        using var context = new DataContext();
        return context.TbEntityAccounts
            .Single(account => account.CompanyID == companyId
                               && account.ComponentID == componentId
                               && account.ComponentItemID == componentItemId
                               && account.AccountID == accountId
                               && account.IsActive);
    }
}