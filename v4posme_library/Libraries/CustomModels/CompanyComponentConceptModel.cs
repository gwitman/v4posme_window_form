using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public class CompanyComponentConceptModel : ICompanyComponentConceptModel
{
    public int InsertAppPosme(TbCompanyComponentConcept? data)
    {
        using var context = new DataContext();
        var add = context.TbCompanyComponentConcepts.Add(data);
        context.BulkSaveChanges();
        return add.Entity.CompanyComponentConceptID;
    }

    public void UpdateAppPosme(int companyId, int componentId, int componentItemId, string? name,
        TbCompanyComponentConcept data)
    {
        using var context = new DataContext();
        var find = context.TbCompanyComponentConcepts
            .Single(concepts => concepts.CompanyID == companyId
                                && concepts.ComponentID == componentId
                                && concepts.ComponentItemID == componentItemId
                                && concepts.Name.Equals(name));
        data.CompanyComponentConceptID = find.CompanyComponentConceptID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.BulkSaveChanges();
    }

    public void DeleteWhereComponentItemId(int companyId, int componentId, int componentItemId)
    {
        using var context = new DataContext();
        var findValues=context.TbCompanyComponentConcepts
            .Where(concepts => concepts.CompanyID == companyId
                               && concepts.ComponentID == componentId
                               && concepts.ComponentItemID == componentItemId);
        if (findValues.Any())
        {
            findValues.ExecuteDelete();
        }
    }

    public TbCompanyComponentConcept? GetRowByPk(int companyId, int componentId, int componentItemId, string? name)
    {
        using var context = new DataContext();
        return context.TbCompanyComponentConcepts
            .SingleOrDefault(concepts =>
                concepts!.CompanyID == companyId
                && concepts.ComponentID == componentId
                && concepts.ComponentItemID == componentItemId
                && concepts.Name.Equals(name));
    }

    public List<TbCompanyComponentConcept> GetRowByComponentItemId(int companyId, int componentId, int componentItemId)
    {
        using var context = new DataContext();
        return context.TbCompanyComponentConcepts
            .Where(concepts => concepts.CompanyID == companyId
                               && concepts.ComponentID == componentId
                               && concepts.ComponentItemID == componentItemId)
            .ToList();
    }

    public List<TbCompanyComponentConcept> GetRowByTransactionMasterId(int companyId, int componentId,
        int transactionMasterId)
    {
        using var context = new DataContext();
        var finds = from tm in context.TbTransactionMasters
            join masterDetail in context.TbTransactionMasterDetails on tm.TransactionMasterID equals masterDetail
                .TransactionMasterID
            join componentConcept in context.TbCompanyComponentConcepts on masterDetail.ComponentItemID equals
                componentConcept.ComponentItemID
            where componentConcept.CompanyID == companyId && componentConcept.ComponentID == componentId &&
                  tm.TransactionMasterID == transactionMasterId
            select componentConcept;
        return finds.ToList();
    }
}