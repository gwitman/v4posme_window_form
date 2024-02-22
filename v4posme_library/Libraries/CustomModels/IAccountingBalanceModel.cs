using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IAccountingBalanceModel
{
    TbAccountingBalance UpdateBalance(int companyId, int componentPeriodId, int componentCycleId, int accountId, decimal balance, decimal debit, decimal credit);
    int DeleteJournalEntryDetailSummary(int companyId, int branchId, int loginId);
    void SetAccountBalance(int companyId, int branchId, int loginId, int cycleId, int periodId, int componentAccountId);
    void ClearCycle(int companyId, int periodId, int cycleId);
    void SetJournalSummary(int companyId, int branchId, int loginId, int cycleId, int journalTypeClosed);
    int? GetMinAccount(int companyId, int branchId, int loginId);
    int? GetMaxAccount(int companyId, int branchId, int loginId);
    TbJournalEntryDetailSummary GetInfoAccount(int companyId, int branchId, int loginId, int accountId);
    int? GetMinAccountBy(int companyId, int branchId, int loginId, int minAccountId);
}
