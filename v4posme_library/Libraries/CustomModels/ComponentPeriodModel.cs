using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public class ComponentPeriodModel : IComponentPeriodModel
{
    private IQueryable<TbAccountingPeriod> FindByCompanyIdAndComponentIdAndComponentPeriodId(int companyId,
        int componentId, int componentPeriodId, DataContext context)
    {
        return context.TbAccountingPeriods
            .Where(period => period.CompanyID == companyId
                             && period.ComponentID == componentId
                             && period.ComponentPeriodID == componentPeriodId);
    }

    public void DeleteAppPosme(int companyId, int componentId, int componentPeriodId)
    {
        using var context = new DataContext();
        FindByCompanyIdAndComponentIdAndComponentPeriodId(companyId, componentId, componentPeriodId, context)
            .ExecuteUpdate(calls => calls.SetProperty(
                period => period.IsActive,
                period => false));
    }

    public void UpdateAppPosme(int companyId, int componentId, int componentPeriodId, TbAccountingPeriod data)
    {
        using var context = new DataContext();
        var find = FindByCompanyIdAndComponentIdAndComponentPeriodId(companyId, componentId, componentPeriodId, context)
            .Single();
        data.ComponentPeriodID = find.ComponentPeriodID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int InsertAppPosme(TbAccountingPeriod data)
    {
        using var context = new DataContext();
        var add = context.TbAccountingPeriods.Add(data);
        context.SaveChanges();
        return add.Entity.ComponentPeriodID;
    }

    public int GetCountPeriod(int companyId)
    {
        using var context = new DataContext();
        return context.TbAccountingPeriods
            .Count(period => period.IsActive
                             && period.CompanyID == companyId);
    }

    public List<TbAccountingPeriod> GetRowByCompany(int companyId)
    {
        using var context = new DataContext();
        return context.TbAccountingPeriods
            .Where(period => period.CompanyID == companyId
                             && period.IsActive)
            .OrderBy(pe => pe.StartOn)
            .ToList();
    }

    public List<TbAccountingPeriod> GetRowByNotClosed(int companyId, int workflowStageClosed)
    {
        using var context = new DataContext();
        return context.TbAccountingPeriods
            .Where(period => period.CompanyID == companyId
                             && period.IsActive
                             && period.StatusID != workflowStageClosed)
            .OrderBy(pe => pe.StartOn)
            .ToList();
    }

    public List<TbAccountingPeriod> ValidateTime(int companyId, int componentId, DateTime dateStart, DateTime dateEnd)
    {
        using var context = new DataContext();
        return context.TbAccountingPeriods
            .Where(period => period.IsActive
                             && period.CompanyID == companyId
                             && period.ComponentID == componentId
                             && dateStart >= period.StartOn && dateStart <= period.EndOn
                             || dateEnd >= period.StartOn && dateEnd <= period.EndOn).ToList();
    }

    public TbAccountingPeriod GetRowByPk(int componentPeriodId)
    {
        using var context = new DataContext();
        return context.TbAccountingPeriods
            .Single(period => period.IsActive
                              && period.ComponentPeriodID == componentPeriodId);
    }

    public TbAccountingPeriod GetRowByCompanyIdFecha(int companyId, DateTime dateStart)
    {
        using var context = new DataContext();
        return context.TbAccountingPeriods
            .Single(period =>
                period.IsActive
                && period.CompanyID == companyId
                && dateStart >= period.StartOn
                && dateStart <= period.EndOn);
    }

    public int CountJournalInPeriod(int periodId, int companyId)
    {
        using var context = new DataContext();
        return context.TbJournalEntries
            .Join(context.TbAccountingCycles,
                entry => entry.AccountingCycleID,
                cycle => cycle.ComponentCycleID,
                (entry, cycle) => new { entry, cycle })
            .Count(data => data.entry.IsActive
                           && data.entry.CompanyID == companyId
                           && data.cycle.ComponentPeriodID == periodId);
    }
}