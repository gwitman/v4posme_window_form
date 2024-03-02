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
        data.EntityAccountId = find.EntityAccountId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.BulkSaveChanges();
    }

    public int InsertAppPosme(TbEntityAccount data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.BulkSaveChanges();
        return add.Entity.EntityAccountId;
    }

    public void DeleteAppPosme(int entityAccountId)
    {
        using var context = new DataContext();
        var find = context.TbEntityAccounts
            .Find(entityAccountId);
        if (find is null) return;
        find.IsActive = 0;
        context.BulkSaveChanges();
    }

    public List<TbEntityAccount> GetRowByEntity(int companyId, int componentId, int componentItemId)
    {
        using var context = new DataContext();
        return context.TbEntityAccounts
            .Where(account => account.CompanyId == companyId
                              && account.ComponentId == componentId
                              && account.ComponentItemId == componentItemId
                              && account.IsActive == 1)
            .ToList();
    }

    public TbEntityAccount GetRowByPk(int entityAccountId)
    {
        using var context = new DataContext();
        return context.TbEntityAccounts
            .Single(account => account.EntityAccountId == entityAccountId
                               && account.IsActive == 1);
    }

    public TbEntityAccount GetRowByAccountId(int companyId, int componentId, int componentItemId, int accountId)
    {
        using var context = new DataContext();
        return context.TbEntityAccounts
            .Single(account => account.CompanyId == companyId
                               && account.ComponentId == componentId
                               && account.ComponentItemId == componentItemId
                               && account.AccountId == accountId
                               && account.IsActive == 1);
    }
}