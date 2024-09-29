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
                on new { tm.CompanyID, tm.TransactionID, tm.TransactionMasterID } equals new
                    { td.CompanyID, td.TransactionID, td.TransactionMasterID }
            join i in context.TbItems on td.CompanyID equals i.CompanyID
            join cc in context.TbCompanyComponentConcepts on i.ItemID equals cc.ComponentItemID
            where  td.CompanyID == companyId
                   && td.TransactionID == transactionId
                   && td.TransactionMasterID == transactionMasterId
                   && cc.ComponentID == componentId
                   && td.IsActive.Value
            select cc;
        return result.ToList();
    }
}