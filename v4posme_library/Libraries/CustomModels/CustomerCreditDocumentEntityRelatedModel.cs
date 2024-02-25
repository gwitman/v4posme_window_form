using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class CustomerCreditDocumentEntityRelatedModel : ICustomerCreditDocumentEntityRelatedModel
{
    private static IQueryable<TbCustomerCreditDocumentEntityRelated> FindEntityRelateds(int customerCreditDocumentId,
        int entityId, DataContext context)
    {
        return context.TbCustomerCreditDocumentEntityRelateds
            .Where(related => related.CustomerCreditDocumentId == customerCreditDocumentId
                              && related.EntityId == entityId);
    }

    public void UpdateAppPosme(int customerCreditDocumentId, int entityId, TbCustomerCreditDocumentEntityRelated data)
    {
        using var context = new DataContext();
        var find = FindEntityRelateds(customerCreditDocumentId, entityId, context).Single();
        context.Entry(find).CurrentValues.SetValues(data);
        context.BulkSaveChanges();
    }

    public void DeleteAppPosme(int customerCreditDocumentId, int entityId)
    {
        using var context = new DataContext();
        var find = FindEntityRelateds(customerCreditDocumentId, entityId, context).Single();
        find.IsActive = 0;
        context.BulkSaveChanges();
    }

    public int InsertAppPosme(TbCustomerCreditDocumentEntityRelated data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.BulkSaveChanges();
        return add.Entity.CcEntityRelatedId;
    }

    public TbCustomerCreditDocumentEntityRelated GetRowByPk(int ccEntityRelatedId)
    {
        using var context = new DataContext();
        return context.TbCustomerCreditDocumentEntityRelateds
            .Single(related => related.CcEntityRelatedId == ccEntityRelatedId
                               && related.IsActive == 1);
    }

    public TbCustomerCreditDocumentEntityRelated GetRowByEntity(int customerCreditDocumentId, int entityId)
    {
        using var context = new DataContext();
        return FindEntityRelateds(customerCreditDocumentId, entityId, context)
            .Single(related => related.IsActive == 1);
    }

    public List<TbCustomerCreditDocumentEntityRelated> GetRowByDocument(int customerCreditDocumentId)
    {
        using var context = new DataContext();
        return context.TbCustomerCreditDocumentEntityRelateds
            .Where(related => related.CustomerCreditDocumentId == customerCreditDocumentId)
            .ToList();
    }
}