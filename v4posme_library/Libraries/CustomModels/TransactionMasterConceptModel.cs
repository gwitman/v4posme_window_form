using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class TransactionMasterConceptModel : ITransactionMasterConceptModel
{
    public List<TbCompanyComponentConcept> GetRowByTransactionMasterConcept(int companyId, int transactionId,
        int transactionMasterId, int componentId)
    {
        using var context = new DataContext();
        var result = from tm in context.TbTransactionMasters
            join td in context.TbTransactionMasterDetails
                on new { tm.CompanyId, tm.TransactionId, tm.TransactionMasterId } equals new
                    { td.CompanyId, td.TransactionId, td.TransactionMasterId }
            join i in context.TbItems on td.CompanyId equals i.CompanyId
            join cc in context.TbCompanyComponentConcepts on i.ItemId equals cc.ComponentItemId
            where  td.CompanyId == companyId
                   && td.TransactionId == transactionId
                   && td.TransactionMasterId == transactionMasterId
                   && cc.ComponentId == componentId
                   && td.IsActive.Value
            select cc;
        return result.ToList();
    }
}