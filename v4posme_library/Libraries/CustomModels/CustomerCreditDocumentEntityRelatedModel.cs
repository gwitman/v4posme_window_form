using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class CustomerCreditDocumentEntityRelatedModel : ICustomerCreditDocumentEntityRelatedModel
{
    private static IQueryable<TbCustomerCreditDocumentEntityRelated> FindEntityRelateds(int customerCreditDocumentId,
        int entityId, DataContext context)
    {
        return context.TbCustomerCreditDocumentEntityRelateds
            .Where(related => related.CustomerCreditDocumentID == customerCreditDocumentId
                              && related.EntityID == entityId);
    }

    public void UpdateAppPosme(int customerCreditDocumentId, int entityId, TbCustomerCreditDocumentEntityRelated data)
    {
        using var context = new DataContext();
        var find = FindEntityRelateds(customerCreditDocumentId, entityId, context);
        foreach (var entityRelated in find)
        {
            data.CcEntityRelatedID = entityRelated.CcEntityRelatedID;
            context.Entry(entityRelated).CurrentValues.SetValues(data);
        }

        //salvar los datos
        context.BulkSaveChanges();
    }

    public void DeleteAppPosme(int customerCreditDocumentId, int entityId)
    {
        using var context = new DataContext();
        FindEntityRelateds(customerCreditDocumentId, entityId, context)
            .ExecuteUpdate(calls => calls.SetProperty(related => related.IsActive, false));
    }

    public int InsertAppPosme(TbCustomerCreditDocumentEntityRelated data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.BulkSaveChanges();
        return add.Entity.CcEntityRelatedID;
    }

    public TbCustomerCreditDocumentEntityRelated GetRowByPk(int ccEntityRelatedId)
    {
        using var context = new DataContext();
        return context.TbCustomerCreditDocumentEntityRelateds
            .Single(related => related.CcEntityRelatedID == ccEntityRelatedId
                               && related.IsActive);
    }

    public TbCustomerCreditDocumentEntityRelated GetRowByEntity(int customerCreditDocumentId, int entityId)
    {
        using var context = new DataContext();
        return FindEntityRelateds(customerCreditDocumentId, entityId, context)
            .Single(related => related.IsActive);
    }

    public List<TbCustomerCreditDocumentEntityRelated> GetRowByDocument(int customerCreditDocumentId)
    {
        using var context = new DataContext();
        return context.TbCustomerCreditDocumentEntityRelateds
            .Where(related => related.CustomerCreditDocumentID == customerCreditDocumentId
                              && related.IsActive)
            .ToList();
    }
}