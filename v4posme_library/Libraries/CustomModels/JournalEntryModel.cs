using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class JournalEntryModel : IJournalEntryModel
{
    public void DeleteAppPosme(int companyId, int journalEntryId)
    {
        using var context = new DataContext();
        context.TbJournalEntries
            .Where(entry => entry.CompanyId == companyId
                            && entry.JournalEntryId == journalEntryId)
            .ExecuteUpdate(calls =>
                calls.SetProperty(entry => entry.IsActive, false));
    }

    public void UpdateAppPosme(int companyId, int journalEntryId, TbJournalEntry data)
    {
        using var context = new DataContext();
        var find = context.TbJournalEntries
            .Single(entry => entry.CompanyId == companyId
                             && entry.JournalEntryId == journalEntryId);
        data.JournalEntryId = find.JournalEntryId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int InsertAppPosme(TbJournalEntry data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.JournalEntryId;
    }

    public TbJournalEntryDto GetRowByCode(int companyId, string journalNumber)
    {
        using var context = new DataContext();
        var result = from je in context.TbJournalEntries
            join ws in context.TbWorkflowStages on je.StatusId equals ws.WorkflowStageId
            join ci in context.TbCatalogItems on je.JournalTypeId equals ci.CatalogItemId
            join cu in context.TbCurrencies on je.CurrencyId equals cu.CurrencyId
            where je.CompanyId == companyId
                  && je.JournalNumber == journalNumber
                  && je.IsActive
            select new TbJournalEntryDto
            {
                JournalEntryId = je.JournalEntryId,
                CompanyId = je.CompanyId,
                JournalNumber = je.JournalNumber,
                EntryName = je.EntryName,
                JournalDate = je.JournalDate,
                TbExchangeRate = je.TbExchangeRate,
                CreatedOn = je.CreatedOn,
                CreatedIn = je.CreatedIn,
                CreatedAt = je.CreatedAt,
                CreatedBy = je.CreatedBy,
                IsActive = je.IsActive,
                IsApplied = je.IsApplied,
                StatusId = je.StatusId,
                Note = je.Note,
                Reference1 = je.Reference1,
                Reference2 = je.Reference2,
                Reference3 = je.Reference3,
                JournalTypeId = je.JournalTypeId,
                CurrencyId = je.CurrencyId,
                AccountingCycleId = je.AccountingCycleId,
                WorkflowStageName = ws.Name,
                JournalTypeName = ci.Display,
                CurrencyName = cu.Name,
                IsModule = je.IsModule,
                TransactionMasterId = je.TransactionMasterId
            };
        return result.Single();
    }

    public TbJournalEntryDto GetRowByPk(int companyId, int journalEntryId)
    {
        using var context = new DataContext();
        var result = from je in context.TbJournalEntries
            join ws in context.TbWorkflowStages on je.StatusId equals ws.WorkflowStageId
            join ci in context.TbCatalogItems on je.JournalTypeId equals ci.CatalogItemId
            join cu in context.TbCurrencies on je.CurrencyId equals cu.CurrencyId
            where je.CompanyId == companyId && je.JournalEntryId == journalEntryId && je.IsActive
            select new TbJournalEntryDto
            {
                JournalEntryId = je.JournalEntryId,
                CompanyId = je.CompanyId,
                JournalNumber = je.JournalNumber,
                EntryName = je.EntryName,
                JournalDate = je.JournalDate,
                TbExchangeRate = je.TbExchangeRate,
                CreatedOn = je.CreatedOn,
                CreatedIn = je.CreatedIn,
                CreatedAt = je.CreatedAt,
                CreatedBy = je.CreatedBy,
                IsActive = je.IsActive,
                IsApplied = je.IsApplied,
                StatusId = je.StatusId,
                Note = je.Note,
                Reference1 = je.Reference1,
                Reference2 = je.Reference2,
                Reference3 = je.Reference3,
                JournalTypeId = je.JournalTypeId,
                CurrencyId = je.CurrencyId,
                AccountingCycleId = je.AccountingCycleId,
                WorkflowStageName = ws.Name,
                JournalTypeName = ci.Display,
                CurrencyName = cu.Name,
                IsModule = je.IsModule,
                TransactionMasterId = je.TransactionMasterId,
                IsTemplated = je.IsTemplated,
                TitleTemplated = je.TitleTemplated
            };
        return result.Single();
    }

    public TbJournalEntry GetRowByPkNext(int companyId, int journalEntryId)
    {
        using var context = new DataContext();
        return context.TbJournalEntries
            .Where(entry => entry.CompanyId == companyId
                            && entry.JournalEntryId >= journalEntryId
                            && entry.IsActive)
            .OrderByDescending(entry => entry.JournalEntryId)
            .First();
    }

    public TbJournalEntry GetRowByPkBack(int companyId, int journalEntryId)
    {
        using var context = new DataContext();
        return context.TbJournalEntries
            .Where(entry => entry.CompanyId == companyId
                            && entry.JournalEntryId <= journalEntryId
                            && entry.IsActive)
            .OrderBy(entry => entry.JournalEntryId)
            .First();
    }
}