namespace v4posme_library.Libraries.CustomLibraries.Interfaz;

public interface ICoreWebConcept
{
    int OtherInput(int companyId,int transactionId,int transactionMasterId);
    int InputUnPost(int companyId,int transactionId,int transactionMasterId);
    int OtherOutPut(int companyId,int transactionId,int transactionMasterId);
    int ReturnsProvider(int companyId,int transactionId,int transactionMasterId);
    int Billing(int companyId,int transactionId,int transactionMasterId);
    int Share(int companyId,int transactionId,int transactionMasterId);
    int Provider(int companyId,int transactionId,int transactionMasterId);
    int CancelInvoice(int companyId,int transactionId,int transactionMasterId);
    int ShareCapital(int companyId,int transactionId,int transactionMasterId);
    int CalendarPay(int companyId,int transactionId,int transactionMasterId);
    int SalaryAdvance(int companyId,int transactionId,int transactionMasterId);
}