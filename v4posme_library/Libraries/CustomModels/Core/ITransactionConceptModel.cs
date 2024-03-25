using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface ITransactionConceptModel
{
    TbTransactionConcept GetRowByPk(int companyId,int transactionId,string name);
    
    List<TbTransactionConcept> GetByCompanyAndTransaction(int companyId,int transactionId);
}