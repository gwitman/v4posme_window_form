using Microsoft.EntityFrameworkCore.ChangeTracking;
using v4posme_library.Models;
namespace v4posme_library.Libraries.CustomModels;

public class ComponentCycleModel : IComponentCycleModel
{
    public void DeleteAppPosme(int componentCycleId)
    {
        using var context = new DataContext();
        var find = context.TbAccountingCycles.Single(cycle=>cycle.ComponentCycleId == componentCycleId);
        find.IsActive = false;
        context.SaveChanges();
    }
    public void DeleteNotInArray(int companyId, int componentId, int componentPeriodId, List<int> array)
    {
        using var context = new DataContext();
        var find = context.TbAccountingCycles
            .Where(cycle=>cycle.CompanyId == companyId)
            .Where(cycle=>cycle.ComponentId == componentId)
            .Where(cycle=>cycle.ComponentPeriodId == componentPeriodId)
            .Single(cycle=>!array.Contains(cycle.ComponentCycleId));
        find.IsActive = false;
        context.SaveChanges();
    }
    public void UpdateAppPosme(int componentCycleId, TbAccountingCycle data)
    {
        using var context = new DataContext();
        var find = context.TbAccountingCycles
            .Single(cycle=>cycle.ComponentCycleId == componentCycleId);
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int InsertAppPosme(TbAccountingCycle data)
    {
        using var context = new DataContext();
        EntityEntry<TbAccountingCycle> add = context.TbAccountingCycles.Add(data);
        context.SaveChanges();
        return add.Entity.ComponentCycleId;
    }
    public List<TbAccountingCycle> GetByComponentPeriodId(int componentPeriodId)
    {
        using var context = new DataContext();
        return context.TbAccountingCycles
            .Where(cycle=>cycle.IsActive &&
                          cycle.ComponentPeriodId == componentPeriodId)
            .OrderBy(cycle=>cycle.StartOn)
            .ToList();
    }
    public List<TbAccountingCycle> GetRowByNotClosed(int companyId, int componentPeriodId, int workflowStageClosed)
    {
        using var context = new DataContext();
        return context.TbAccountingCycles
            .Where(cycle=>cycle.IsActive)
            .Where(cycle=>cycle.ComponentPeriodId == componentPeriodId)
            .Where(cycle=>cycle.CompanyId == companyId)
            .Where(cycle=>cycle.StatusId != workflowStageClosed)
            .OrderBy(cycle=>cycle.StartOn)
            .ToList();
    }
    public TbAccountingCycle GetRowByCycleId(int cycleId)
    {
        using var context = new DataContext();
        return context.TbAccountingCycles
            .First(cycle=>cycle.IsActive && cycle.ComponentCycleId == cycleId);
    }
    public int CountJournalInCycle(int cycleId, int companyId)
    {
        using var context = new DataContext();
        return context.TbJournalEntries
            .Count(entry=>entry.IsActive && entry.CompanyId == companyId
                                         && entry.AccountingCycleId == cycleId);
    }
    public TbAccountingCycle GetRowByCompanyIdFecha(int companyId, DateTime dateStart)
    {
        using var context = new DataContext();
        return context.TbAccountingCycles
            .Single(cycle=>cycle.IsActive &&
                           cycle.CompanyId == companyId &&
                           dateStart >= cycle.StartOn &&
                           dateStart <= cycle.EndOn);
    }
    public List<TbAccountingCycle> GetRowByCompanyIdTopCycleOpenAscAndOpen(int companyId, int top, int workflowStageClosed)
    {
        using var context = new DataContext();
        return context.TbAccountingCycles
            .Where(cycle=>cycle.IsActive)
            .Where(cycle=>cycle.CompanyId == companyId)
            .Where(cycle=>cycle.StatusId != workflowStageClosed)
            .OrderBy(cycle=>cycle.ComponentCycleId)
            .Take(top)
            .ToList();
    }
    public TbAccountingCycle GetRowByPk(int periodId, int cycleId)
    {
        using var context = new DataContext();
        return context.TbAccountingCycles
            .Single(account=>account.IsActive
                             && account.ComponentPeriodId == periodId
                             && account.ComponentCycleId == cycleId);
    }
    public List<TbAccountingCycle> GetRowByCycleNotIn(int companyId, int componentId, int componentPeriodId, List<int> data)
    {
        using var context = new DataContext();
        return context.TbAccountingCycles
            .Where(cycle=>cycle.CompanyId == companyId)
            .Where(cycle=>cycle.ComponentId == componentId)
            .Where(cycle=>cycle.ComponentPeriodId == componentPeriodId)
            .Where(cycle=>cycle.IsActive)
            .WhereBulkNotContains(data, x=>x.ComponentCycleId)
            .Where(cycle=>!data.Contains(cycle.ComponentCycleId))
            .ToList();
    }
}
