using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class TransactionConceptModel : ITransactionConceptModel
{
    public TbTransactionConcept GetRowByPk(int companyId, int transactionId, string name)
    {
        using var context = new DataContext();
        return context.TbTransactionConcepts
            .First(concept => concept.CompanyId == companyId
                              && concept.TransactionId == transactionId
                              && concept.Name == name
                              && concept.IsActive!.Value);
    }

    public List<TbTransactionConcept> GetByCompanyAndTransaction(int companyId, int transactionId)
    {
        using var context = new DataContext();
        return context.TbTransactionConcepts
            .Where(concept => concept.CompanyId == companyId
                              && concept.TransactionId == transactionId
                              && concept.IsActive!.Value)
            .ToList();
    }
}