using Microsoft.EntityFrameworkCore;
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
        var find = FindEntityRelateds(customerCreditDocumentId, entityId, context);
        foreach (var entityRelated in find)
        {
            data.CcEntityRelatedId = entityRelated.CcEntityRelatedId;
            context.Entry(entityRelated).CurrentValues.SetValues(data);
        }

        //salvar los datos
        context.BulkSaveChanges();
    }

    public void DeleteAppPosme(int customerCreditDocumentId, int entityId)
    {
        using var context = new DataContext();
        FindEntityRelateds(customerCreditDocumentId, entityId, context)
            .ExecuteUpdate(calls => calls.SetProperty(related => related.IsActive, (ulong)0));
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
            .Where(related => related.CustomerCreditDocumentId == customerCreditDocumentId
                              && related.IsActive == 1)
            .ToList();
    }
}