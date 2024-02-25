using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public class ComponentPeriodModel : IComponentPeriodModel
{
    private IQueryable<TbAccountingPeriod> FindByCompanyIdAndComponentIdAndComponentPeriodId(int companyId,
        int componentId, int componentPeriodId, DataContext context)
    {
        return context.TbAccountingPeriods
            .Where(period => period.CompanyId == companyId
                             && period.ComponentId == componentId
                             && period.ComponentPeriodId == componentPeriodId);
    }

    public void DeleteAppPosme(int companyId, int componentId, int componentPeriodId)
    {
        using var context = new DataContext();
        FindByCompanyIdAndComponentIdAndComponentPeriodId(companyId, companyId, componentPeriodId, context)
            .ExecuteUpdate(calls => calls.SetProperty(
                period => period.IsActive,
                period => false));
    }

    public void UpdateAppPosme(int companyId, int componentId, int componentPeriodId, TbAccountingPeriod data)
    {
        using var context = new DataContext();
        var find = FindByCompanyIdAndComponentIdAndComponentPeriodId(companyId, companyId, componentPeriodId, context)
            .Single();
        context.Entry(find).CurrentValues.SetValues(data);
        context.BulkSaveChanges();
    }

    public int InsertAppPosme(TbAccountingPeriod data)
    {
        using var context = new DataContext();
        var add = context.TbAccountingPeriods.Add(data);
        context.BulkSaveChanges();
        return add.Entity.ComponentPeriodId;
    }

    public int GetCountPeriod(int companyId)
    {
        using var context = new DataContext();
        return context.TbAccountingPeriods
            .Count(period => period.IsActive
                             && period.CompanyId == companyId);
    }

    public List<TbAccountingPeriod> GetRowByCompany(int companyId)
    {
        using var context = new DataContext();
        return context.TbAccountingPeriods
            .Where(period => period.CompanyId == companyId
                             && period.IsActive)
            .OrderBy(pe => pe.StartOn)
            .ToList();
    }

    public List<TbAccountingPeriod> GetRowByNotClosed(int companyId, int workflowStageClosed)
    {
        using var context = new DataContext();
        return context.TbAccountingPeriods
            .Where(period => period.CompanyId == companyId
                             && period.IsActive
                             && period.StatusId != workflowStageClosed)
            .OrderBy(pe => pe.StartOn)
            .ToList();
    }

    public List<TbAccountingPeriod> ValidateTime(int companyId, int componentId, DateTime dateStart, DateTime dateEnd)
    {
        using var context = new DataContext();
        return context.TbAccountingPeriods
            .Where(period => period.IsActive
                             && period.CompanyId == companyId
                             && period.ComponentId == componentId
                             && dateStart >= period.StartOn && dateStart <= period.EndOn
                             || dateEnd >= period.StartOn && dateEnd <= period.EndOn).ToList();
    }

    public TbAccountingPeriod GetRowByPk(int componentPeriodId)
    {
        using var context = new DataContext();
        return context.TbAccountingPeriods
            .Single(period => period.IsActive
                              && period.ComponentPeriodId == componentPeriodId);
    }

    public TbAccountingPeriod GetRowByCompanyIdFecha(int companyId, DateTime dateStart)
    {
        using var context = new DataContext();
        return context.TbAccountingPeriods
            .Single(period =>
                period.IsActive
                && period.CompanyId == companyId
                && dateStart >= period.StartOn
                && dateStart <= period.EndOn);
    }

    public int CountJournalInPeriod(int periodId, int companyId)
    {
        using var context = new DataContext();
        return context.TbJournalEntries
            .Join(context.TbAccountingCycles,
                entry => entry.AccountingCycleId,
                cycle => cycle.ComponentCycleId,
                (entry, cycle) => new { entry, cycle })
            .Count(data => data.entry.IsActive
                           && data.entry.CompanyId == companyId
                           && data.cycle.ComponentPeriodId == periodId);
    }
}