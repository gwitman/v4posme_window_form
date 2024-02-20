using v4posme_library.Models;
namespace v4posme_library.Libraries.CustomModels;

public interface IComponentCycleModel
{
    void DeleteAppPosme(int componentCycleId);
    void DeleteNotInArray(int companyId, int componentId, int componentPeriodId, List<int> array);
    void UpdateAppPosme(int componentCycleId, TbAccountingCycle data);
    int InsertAppPosme(TbAccountingCycle data);
    List<TbAccountingCycle> GetByComponentPeriodId(int componentPeriodId);
    List<TbAccountingCycle> GetRowByNotClosed(int companyId, int componentPeriodId, int workflowStageClosed);
    TbAccountingCycle GetRowByCompanyIdFecha(int companyId, DateTime dateStart);
    List<TbAccountingCycle> GetRowByCompanyIdTopCycleOpenAscAndOpen(int companyId, int top, int workflowStageClosed);
    TbAccountingCycle GetRowByPk(int periodId, int cycleId);
    List<TbAccountingCycle> GetRowByCycleNotIn(int companyId, int componentId, int componentPeriodId, List<int> data);
    TbAccountingCycle GetRowByCycleId(int cycleId);
    int CountJournalInCycle(int cycleId, int companyId);

}
