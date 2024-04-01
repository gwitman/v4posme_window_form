namespace v4posme_library.Libraries.CustomLibraries.Interfaz;

public interface ICoreWebAmortization
{
    void CancelDocument(int companyId,int customerCreditDocumentId,decimal amount);
    void ShareCapital(int companyId,int customerCreditDocumentId,decimal amount);
    void ChangeStatus(int companyId,int customerCreditDocumentId);
    void ApplyCuote(int companyId,int customerCreditDocumentId,decimal amount,int amoritizationId);
}