using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public class ComponentCycleModel(DataContext context) : IComponentCycleModel
{
    public void DeleteAppPosme(int componentCycleId)
    {
        
        var find = context.TbAccountingCycles
            .SingleOrDefault(cycle => cycle!.ComponentCycleID == componentCycleId);
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
            .Where(cycle => cycle!.CompanyID == companyId
                            && cycle.ComponentID == componentId
                            && cycle.ComponentPeriodID == componentPeriodId
                            && !array.Contains(cycle.ComponentCycleID))
            .ExecuteUpdate(calls => calls.SetProperty(cycle => cycle.IsActive, false));
    }

    public void UpdateAppPosme(int componentCycleId, TbAccountingCycle data)
    {
        
        var find = context.TbAccountingCycles
            .SingleOrDefault(cycle => cycle!.ComponentCycleID == componentCycleId);
        if (find is null)
        {
            return;
        }
        data.ComponentCycleID = find.ComponentCycleID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int InsertAppPosme(TbAccountingCycle? data)
    {
        
        var add = context.TbAccountingCycles.Add(data);
        context.SaveChanges();
        return add.Entity!.ComponentCycleID;
    }

    public List<TbAccountingCycle> GetRowByNotClosed(int companyId, int componentPeriodId, int workflowStageClosed)
    {
        
        return context.TbAccountingCycles
            .Where(cycle => cycle!.IsActive)
            .Where(cycle => cycle!.ComponentPeriodID == componentPeriodId)
            .Where(cycle => cycle!.CompanyID == companyId)
            .Where(cycle => cycle!.StatusID != workflowStageClosed)
            .OrderBy(cycle => cycle!.StartOn)
            .ToList();
    }

    public TbAccountingCycle? GetRowByCycleId(int cycleId)
    {
        
        return context.TbAccountingCycles
            .SingleOrDefault(cycle => cycle!.IsActive && cycle.ComponentCycleID == cycleId);
    }

    public int CountJournalInCycle(int cycleId, int companyId)
    {
        
        return context.TbJournalEntries
            .Count(entry => entry.IsActive
                            && entry.CompanyID == companyId
                            && entry.AccountingCycleID == cycleId);
    }

    public TbAccountingCycle? GetRowByCompanyIdFecha(int companyId, DateTime dateStart)
    {
        
        return context.TbAccountingCycles
            .SingleOrDefault(cycle => cycle!.IsActive &&
                             cycle.CompanyID == companyId &&
                             dateStart >= cycle.StartOn &&
                             dateStart <= cycle.EndOn);
    }

    public List<TbAccountingCycle> GetRowByCompanyIdTopCycleOpenAscAndOpen(int companyId, int top,
        int workflowStageClosed)
    {
        
        return context.TbAccountingCycles
            .Where(cycle => cycle!.IsActive)
            .Where(cycle => cycle!.CompanyID == companyId)
            .Where(cycle => cycle!.StatusID != workflowStageClosed)
            .OrderBy(cycle => cycle!.ComponentCycleID)
            .Take(top)
            .ToList();
    }

    public TbAccountingCycle? GetRowByPk(int periodId, int cycleId)
    {
        
        return context.TbAccountingCycles
            .Single(account => account!.IsActive
                               && account.ComponentPeriodID == periodId
                               && account.ComponentCycleID == cycleId);
    }

    public List<TbAccountingCycle> GetRowByCycleNotIn(int companyId, int componentId,
        int componentPeriodId, List<int> data)
    {
        
        return context.TbAccountingCycles
            .Where(cycle => cycle!.CompanyID != companyId
                            && cycle.ComponentID == componentId
                            && cycle.ComponentPeriodID == componentPeriodId
                            && cycle.IsActive
                            && !data.Contains(cycle.ComponentCycleID))
            .ToList();
    }

    public List<TbAccountingCycle> GetByComponentPeriodId(int componentPeriodId)
    {
        
        return context.TbAccountingCycles
            .Where(cycle => cycle!.ComponentPeriodID == componentPeriodId)
            .Where(cycle => cycle!.IsActive)
            .ToList();
    }
}