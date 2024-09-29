using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Interfaz;

public interface ICoreWebTransaction
{
    int GetCountTransactionBillingAnuladas(int companyId);

    int GetCountTransactionBillingCancel(int companyId);

    int GetDefaultCausalId(int companyId, int transactionId);

    void CreateInverseDocumentByTransaccion(int companyIdOriginal, int transactionIdOriginal,
        int transactionMasterIdOriginal,
        int transactionIdRevert, int transactionMasterIdRevert);

    int? GetTransactionId(int companyId, string? componentName, int componentItemId);

    TbTransaction? GetTransaction(int companyId, string? name);

    TbTransactionConcept GetConcept(int companyId, string? transactionName, string? conceptName);
}