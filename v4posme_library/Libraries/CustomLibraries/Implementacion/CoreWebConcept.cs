using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion;

class CoreWebConcept(IBdModel bdModel) : ICoreWebConcept
{
    public int OtherInput(int companyId, int transactionId, int transactionMasterId)
    {
        return bdModel.ExecuteRender<int>(
            $"CALL pr_concept_helper_other_input({companyId},{transactionId},{transactionMasterId}); ");
    }

    public int InputUnPost(int companyId, int transactionId, int transactionMasterId)
    {
        return bdModel.ExecuteRender<int>(
            $"CALL pr_concept_helper_input_unpost({companyId},{transactionId},{transactionMasterId}); ");
    }

    public int OtherOutPut(int companyId, int transactionId, int transactionMasterId)
    {
        return bdModel.ExecuteRender<int>(
            $"CALL pr_concept_helper_other_output({companyId},{transactionId},{transactionMasterId}); ");
    }

    public void ReturnsProvider(int companyId, int transactionId, int transactionMasterId)
    {
        bdModel.ExecuteProcedure(
            $"CALL pr_concept_helper_returns_provider({companyId},{transactionId},{transactionMasterId}); ");
    }

    public int Billing(int companyId, int transactionId, int transactionMasterId)
    {
        return bdModel.ExecuteRender<int>(
            $"CALL pr_concept_helper_billing({companyId},{transactionId},{transactionMasterId}); ");
    }

    public int Share(int companyId, int transactionId, int transactionMasterId)
    {
        return bdModel.ExecuteRender<int>(
            $"CALL pr_concept_helper_share({companyId},{transactionId},{transactionMasterId}); ");
    }

    public int Provider(int companyId, int transactionId, int transactionMasterId)
    {
        return bdModel.ExecuteRender<int>(
            $"CALL pr_concept_helper_provider({companyId},{transactionId},{transactionMasterId}); ");
    }

    public int CancelInvoice(int companyId, int transactionId, int transactionMasterId)
    {
        return bdModel.ExecuteRender<int>(
            $"CALL pr_concept_helper_cancelinvoice({companyId},{transactionId},{transactionMasterId}); ");
    }

    public int ShareCapital(int companyId, int transactionId, int transactionMasterId)
    {
        return bdModel.ExecuteRender<int>(
            $"CALL pr_concept_helper_sharecapital({companyId},{transactionId},{transactionMasterId}); ");
    }

    public int CalendarPay(int companyId, int transactionId, int transactionMasterId)
    {
        return bdModel.ExecuteRender<int>(
            $"CALL pr_concept_helper_calendarpay({companyId},{transactionId},{transactionMasterId}); ");
    }

    public int SalaryAdvance(int companyId, int transactionId, int transactionMasterId)
    {
        return bdModel.ExecuteRender<int>(
            $"CALL pr_concept_helper_salaryadvance({companyId},{transactionId},{transactionMasterId}); ");
    }
}