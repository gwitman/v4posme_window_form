using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public class ComponentCycleModel(DataContext context) : IComponentCycleModel
{
    public void DeleteAppPosme(int componentCycleId)
    {
        
        var find = context.TbAccountingCycles
            .SingleOrDefault(cycle => cycle!.ComponentCycleId == componentCycleId);
        if (find is null)
        {
            return;
        }
        find.IsActive = false;
        context.SaveChanges();
    }

    public void DeleteNotInArray(int companyId, int componentId, int componentPeriodId, List<int> array)
    {
        
        context.TbAccountingCycles
            .Where(cycle => cycle!.CompanyId == companyId
                            && cycle.ComponentId == componentId
                            && cycle.ComponentPeriodId == componentPeriodId
                            && !array.Contains(cycle.ComponentCycleId))
            .ExecuteUpdate(calls => calls.SetProperty(cycle => cycle.IsActive, false));
    }

    public void UpdateAppPosme(int componentCycleId, TbAccountingCycle data)
    {
        
        var find = context.TbAccountingCycles
            .SingleOrDefault(cycle => cycle!.ComponentCycleId == componentCycleId);
        if (find is null)
        {
            return;
        }
        data.ComponentCycleId = find.ComponentCycleId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.BulkSaveChanges();
    }

    public int InsertAppPosme(TbAccountingCycle? data)
    {
        
        var add = context.TbAccountingCycles.Add(data);
        context.SaveChanges();
        return add.Entity!.ComponentCycleId;
    }

    public List<TbAccountingCycle?> GetRowByNotClosed(int companyId, int componentPeriodId, int workflowStageClosed)
    {
        
        return context.TbAccountingCycles
            .Where(cycle => cycle!.IsActive)
            .Where(cycle => cycle!.ComponentPeriodId == componentPeriodId)
            .Where(cycle => cycle!.CompanyId == companyId)
            .Where(cycle => cycle!.StatusId != workflowStageClosed)
            .OrderBy(cycle => cycle!.StartOn)
            .ToList();
    }

    public TbAccountingCycle? GetRowByCycleId(int cycleId)
    {
        
        return context.TbAccountingCycles
            .SingleOrDefault(cycle => cycle!.IsActive && cycle.ComponentCycleId == cycleId);
    }

    public int CountJournalInCycle(int cycleId, int companyId)
    {
        
        return context.TbJournalEntries
            .Count(entry => entry.IsActive
                            && entry.CompanyId == companyId
                            && entry.AccountingCycleId == cycleId);
    }

    public TbAccountingCycle? GetRowByCompanyIdFecha(int companyId, DateTime dateStart)
    {
        
        return context.TbAccountingCycles
            .Single(cycle => cycle!.IsActive &&
                             cycle.CompanyId == companyId &&
                             dateStart >= cycle.StartOn &&
                             dateStart <= cycle.EndOn);
    }

    public List<TbAccountingCycle?> GetRowByCompanyIdTopCycleOpenAscAndOpen(int companyId, int top,
        int workflowStageClosed)
    {
        
        return context.TbAccountingCycles
            .Where(cycle => cycle!.IsActive)
            .Where(cycle => cycle!.CompanyId == companyId)
            .Where(cycle => cycle!.StatusId != workflowStageClosed)
            .OrderBy(cycle => cycle!.ComponentCycleId)
            .Take(top)
            .ToList();
    }

    public TbAccountingCycle? GetRowByPk(int periodId, int cycleId)
    {
        
        return context.TbAccountingCycles
            .Single(account => account!.IsActive
                               && account.ComponentPeriodId == periodId
                               && account.ComponentCycleId == cycleId);
    }

    public List<TbAccountingCycle?> GetRowByCycleNotIn(int companyId, int componentId,
        int componentPeriodId, List<int> data)
    {
        
        return context.TbAccountingCycles
            .Where(cycle => cycle!.CompanyId != companyId
                            && cycle.ComponentId == componentId
                            && cycle.ComponentPeriodId == componentPeriodId
                            && cycle.IsActive
                            && !data.Contains(cycle.ComponentCycleId))
            .ToList();
    }

    public List<TbAccountingCycle?> GetByComponentPeriodId(int componentPeriodId)
    {
        
        return context.TbAccountingCycles
            .Where(cycle => cycle!.ComponentPeriodId == componentPeriodId)
            .Where(cycle => cycle!.IsActive)
            .ToList();
    }
}