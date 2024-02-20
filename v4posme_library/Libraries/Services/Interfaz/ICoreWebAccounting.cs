namespace v4posme_library.Libraries.Services.Interfaz;

public interface ICoreWebAccounting
{
    bool CycleIsCloseById(int companyId, int cycleId);
    bool CycleIsEmptyById(int companyId, int cycleId);
    bool CycleIsCloseByDate(int companyId, DateTime dateOn);
    bool CycleIsEmptyByDate(int companyId, DateTime dateOn);
    bool PeriodIsCloseById(int companyId, int periodId);
    bool PeriodIsEmptyByDate(int companyId, DateTime dateOn);
    bool PeriodIsCloseByDate(int companyId, DateTime dateOn);
    void MayorizateAccount(int companyId, int branchId, int loginId, int accountId, int componentPeriodId, int componentCycleId, decimal balance, decimal debit, decimal credit);
}
