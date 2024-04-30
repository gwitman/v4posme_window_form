using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion;

class CoreWebConcept(IBdModel bdModel) : ICoreWebConcept
{
    public void OtherInput(int companyId, int transactionId, int transactionMasterId)
    {
         bdModel.ExecuteProcedure(
            $"CALL pr_concept_helper_other_input({companyId},{transactionId},{transactionMasterId}); ");
    }

    public void InputUnPost(int companyId, int transactionId, int transactionMasterId)
    {
         bdModel.ExecuteProcedure(
            $"CALL pr_concept_helper_input_unpost({companyId},{transactionId},{transactionMasterId}); ");
    }

    public void OtherOutPut(int companyId, int transactionId, int transactionMasterId)
    {
         bdModel.ExecuteProcedure(
            $"CALL pr_concept_helper_other_output({companyId},{transactionId},{transactionMasterId}); ");
    }

    public void Billing(int companyId, int transactionId, int transactionMasterId)
    {
         bdModel.ExecuteProcedure(
            $"CALL pr_concept_helper_billing({companyId},{transactionId},{transactionMasterId}); ");
    }

    public void Share(int companyId, int transactionId, int transactionMasterId)
    {
         bdModel.ExecuteProcedure(
            $"CALL pr_concept_helper_share({companyId},{transactionId},{transactionMasterId}); ");
    }

    public void Provider(int companyId, int transactionId, int transactionMasterId)
    {
         bdModel.ExecuteProcedure(
            $"CALL pr_concept_helper_provider({companyId},{transactionId},{transactionMasterId}); ");
    }

    public void CancelInvoice(int companyId, int transactionId, int transactionMasterId)
    {
         bdModel.ExecuteProcedure(
            $"CALL pr_concept_helper_cancelinvoice({companyId},{transactionId},{transactionMasterId}); ");
    }

    public void ShareCapital(int companyId, int transactionId, int transactionMasterId)
    {
         bdModel.ExecuteProcedure(
            $"CALL pr_concept_helper_sharecapital({companyId},{transactionId},{transactionMasterId}); ");
    }

    public void CalendarPay(int companyId, int transactionId, int transactionMasterId)
    {
         bdModel.ExecuteProcedure(
            $"CALL pr_concept_helper_calendarpay({companyId},{transactionId},{transactionMasterId}); ");
    }

    public void SalaryAdvance(int companyId, int transactionId, int transactionMasterId)
    {
         bdModel.ExecuteProcedure(
            $"CALL pr_concept_helper_salaryadvance({companyId},{transactionId},{transactionMasterId}); ");
    }
}