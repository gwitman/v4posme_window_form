using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public class CompanyComponentConceptModel : ICompanyComponentConceptModel
{
    public int InsertAppPosme(TbCompanyComponentConcept data)
    {
        using var context = new DataContext();
        var add = context.TbCompanyComponentConcepts.Add(data);
        context.BulkSaveChanges();
        return add.Entity.CompanyComponentConceptId;
    }

    public void UpdateAppPosme(int companyId, int componentId, int componentItemId, string name,
        TbCompanyComponentConcept data)
    {
        using var context = new DataContext();
        var find = context.TbCompanyComponentConcepts
            .Single(concepts => concepts.CompanyId == companyId
                                && concepts.ComponentId == componentId
                                && concepts.ComponentItemId == componentItemId
                                && concepts.Name.Equals(name));
        data.CompanyComponentConceptId = find.CompanyComponentConceptId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.BulkSaveChanges();
    }

    public void DeleteWhereComponentItemId(int companyId, int componentId, int componentItemId)
    {
        using var context = new DataContext();
        context.TbCompanyComponentConcepts
            .Where(concepts => concepts.CompanyId == companyId
                               && concepts.ComponentId == componentId
                               && concepts.ComponentItemId == componentItemId)
            .ExecuteDelete();
    }

    public TbCompanyComponentConcept GetRowByPk(int companyId, int componentId, int componentItemId, string name)
    {
        using var context = new DataContext();
        return context.TbCompanyComponentConcepts
            .Single(concepts =>
                concepts.CompanyId == companyId
                && concepts.ComponentId == componentId
                && concepts.ComponentItemId == componentItemId
                && concepts.Name.Equals(name));
    }

    public List<TbCompanyComponentConcept> GetRowByComponentItemId(int companyId, int componentId, int componentItemId)
    {
        using var context = new DataContext();
        return context.TbCompanyComponentConcepts
            .Where(concepts => concepts.CompanyId == companyId
                               && concepts.ComponentId == componentId
                               && concepts.ComponentItemId == componentItemId)
            .ToList();
    }

    public List<TbCompanyComponentConcept> GetRowByTransactionMasterId(int companyId, int componentId,
        int transactionMasterId)
    {
        using var context = new DataContext();
        var finds = from tm in context.TbTransactionMasters
            join masterDetail in context.TbTransactionMasterDetails on tm.TransactionMasterId equals masterDetail
                .TransactionMasterId
            join componentConcept in context.TbCompanyComponentConcepts on masterDetail.ComponentItemId equals
                componentConcept.ComponentItemId
            where componentConcept.CompanyId == companyId && componentConcept.ComponentId == componentId &&
                  tm.TransactionMasterId == transactionMasterId
            select componentConcept;
        return finds.ToList();
    }
}