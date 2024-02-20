using v4posme_library.Models;
namespace v4posme_library.Libraries.ComponentModels;

public class ComponentCycleModel : IComponentCycleModel
{
    public TbAccountingCycle GetRowByCycleId(int cycleId)
    {
        using var context = new DataContext();
        return context.TbAccountingCycles.First(cycle=>cycle.IsActive && cycle.ComponentCycleId == cycleId);
    }
    public int CountJournalInCycle(int cycleId, int companyId)
    {
        using var context = new DataContext();
        return context.TbJournalEntries.Count(entry=>entry.IsActive && entry.CompanyId == companyId && entry.AccountingCycleId == cycleId);
    }
    public TbAccountingCycle GetRowByCompanyIdFecha(int companyId, DateTime dateStart)
    {
        using var context = new DataContext();
        return context.TbAccountingCycles.Single(cycle=>cycle.IsActive && cycle.CompanyId == companyId && dateStart >= cycle.StartOn && dateStart <= cycle.EndOn);
    }
    public TbAccountingCycle GetRowByPk(int periodId, int cycleId)
    {
        using var context = new DataContext();
        return context.TbAccountingCycles.Single(account=>account.IsActive && account.ComponentPeriodId == periodId && account.ComponentCycleId == cycleId);
    }
}
