namespace v4posme_library.Libraries.CustomLibraries.Interfaz;

public interface ICoreWebConcept
{
    void OtherInput(int companyId,int transactionId,int transactionMasterId);
    void InputUnPost(int companyId,int transactionId,int transactionMasterId);
    void OtherOutPut(int companyId,int transactionId,int transactionMasterId);
    
    void Billing(int companyId,int transactionId,int transactionMasterId);
    void Share(int companyId,int transactionId,int transactionMasterId);
    void Provider(int companyId,int transactionId,int transactionMasterId);
    void CancelInvoice(int companyId,int transactionId,int transactionMasterId);
    void ShareCapital(int companyId,int transactionId,int transactionMasterId);
    void CalendarPay(int companyId,int transactionId,int transactionMasterId);
    void SalaryAdvance(int companyId,int transactionId,int transactionMasterId);
}