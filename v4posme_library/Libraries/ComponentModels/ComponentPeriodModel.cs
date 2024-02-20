using v4posme_library.Models;
namespace v4posme_library.Libraries.ComponentModels;

public class ComponentPeriodModel:IComponentPeriodModel
{

    public TbAccountingPeriod GetRowByPk(int componentPeriodId)
    {
        using var context = new DataContext();
        return context.TbAccountingPeriods.Single(period=>period.IsActive && period.ComponentPeriodId == componentPeriodId);
    }
    public TbAccountingPeriod GetRowByCompanyIdFecha(int companyID, DateTime dateStart)
    {
        using var context = new DataContext();
        return context.TbAccountingPeriods.Single(period=>period.IsActive && period.CompanyId == companyID && dateStart>= period.StartOn && dateStart <= period.EndOn);
    }
    public int CountJournalInPeriod(int periodId, int companyId)
    {
        using var context = new DataContext();
        return context.TbJournalEntries
            .Join(context.TbAccountingCycles, entry=>entry.AccountingCycleId, cycle=>cycle.ComponentCycleId, (entry, cycle)=>new {entry, cycle})
            .Count(data=>data.entry.IsActive && data.entry.CompanyId==companyId && data.cycle.ComponentPeriodId==periodId);
    }
}
