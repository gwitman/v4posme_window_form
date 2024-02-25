using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IComponentPeriodModel
{
    void DeleteAppPosme(int companyId,int componentId,int componentPeriodId);
    void UpdateAppPosme(int companyId,int componentId,int componentPeriodId,TbAccountingPeriod data);
    int InsertAppPosme(TbAccountingPeriod data);
    int GetCountPeriod(int companyId);
    List<TbAccountingPeriod> GetRowByCompany(int companyId);
    List<TbAccountingPeriod> GetRowByNotClosed(int companyId, int workflowStageClosed);
    List<TbAccountingPeriod> ValidateTime(int companyId,int componentId,DateTime dateStart,DateTime dateEnd);
    TbAccountingPeriod GetRowByPk(int componentPeriodId);
    TbAccountingPeriod GetRowByCompanyIdFecha(int companyId,DateTime dateStart);
    int CountJournalInPeriod(int periodId, int companyId);
}
