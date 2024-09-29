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
            .Where(entry => entry.CompanyID == companyId
                            && entry.JournalEntryID == journalEntryId)
            .ExecuteUpdate(calls =>
                calls.SetProperty(entry => entry.IsActive, false));
    }

    public void UpdateAppPosme(int companyId, int journalEntryId, TbJournalEntry data)
    {
        using var context = new DataContext();
        var find = context.TbJournalEntries
            .Single(entry => entry.CompanyID == companyId
                             && entry.JournalEntryID == journalEntryId);
        data.JournalEntryID = find.JournalEntryID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int InsertAppPosme(TbJournalEntry data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.JournalEntryID;
    }

    public TbJournalEntryDto GetRowByCode(int companyId, string? journalNumber)
    {
        using var context = new DataContext();
        var result = from je in context.TbJournalEntries
            join ws in context.TbWorkflowStages on je.StatusID equals ws.WorkflowStageID
            join ci in context.TbCatalogItems on je.JournalTypeID equals ci.CatalogItemID
            join cu in context.TbCurrencies on je.CurrencyID equals cu.CurrencyID
            where je.CompanyID == companyId
                  && je.JournalNumber == journalNumber
                  && je.IsActive
            select new TbJournalEntryDto
            {
                JournalEntryId = je.JournalEntryID,
                CompanyId = je.CompanyID,
                JournalNumber = je.JournalNumber,
                EntryName = je.EntryName,
                JournalDate = DateOnly.FromDateTime(je.JournalDate),
                TbExchangeRate = je.TbExchangeRate,
                CreatedOn = je.CreatedOn,
                CreatedIn = je.CreatedIn,
                CreatedAt = je.CreatedAt,
                CreatedBy = je.CreatedBy,
                IsActive = je.IsActive,
                IsApplied = je.IsApplied,
                StatusId = je.StatusID,
                Note = je.Note,
                Reference1 = je.Reference1,
                Reference2 = je.Reference2,
                Reference3 = je.Reference3,
                JournalTypeId = je.JournalTypeID,
                CurrencyId = je.CurrencyID,
                AccountingCycleId = je.AccountingCycleID,
                WorkflowStageName = ws.Name,
                JournalTypeName = ci.Display,
                CurrencyName = cu.Name,
                IsModule = je.IsModule,
                TransactionMasterId = je.TransactionMasterID
            };
        return result.Single();
    }

    public TbJournalEntryDto GetRowByPk(int companyId, int journalEntryId)
    {
        using var context = new DataContext();
        var result = from je in context.TbJournalEntries
            join ws in context.TbWorkflowStages on je.StatusID equals ws.WorkflowStageID
            join ci in context.TbCatalogItems on je.JournalTypeID equals ci.CatalogItemID
            join cu in context.TbCurrencies on je.CurrencyID equals cu.CurrencyID
            where je.CompanyID == companyId && je.JournalEntryID == journalEntryId && je.IsActive
            select new TbJournalEntryDto
            {
                JournalEntryId = je.JournalEntryID,
                CompanyId = je.CompanyID,
                JournalNumber = je.JournalNumber,
                EntryName = je.EntryName,
                JournalDate = DateOnly.FromDateTime(je.JournalDate),
                TbExchangeRate = je.TbExchangeRate,
                CreatedOn = je.CreatedOn,
                CreatedIn = je.CreatedIn,
                CreatedAt = je.CreatedAt,
                CreatedBy = je.CreatedBy,
                IsActive = je.IsActive,
                IsApplied = je.IsApplied,
                StatusId = je.StatusID,
                Note = je.Note,
                Reference1 = je.Reference1,
                Reference2 = je.Reference2,
                Reference3 = je.Reference3,
                JournalTypeId = je.JournalTypeID,
                CurrencyId = je.CurrencyID,
                AccountingCycleId = je.AccountingCycleID,
                WorkflowStageName = ws.Name,
                JournalTypeName = ci.Display,
                CurrencyName = cu.Name,
                IsModule = je.IsModule,
                TransactionMasterId = je.TransactionMasterID,
                IsTemplated = je.IsTemplated,
                TitleTemplated = je.TitleTemplated
            };
        return result.Single();
    }

    public TbJournalEntry GetRowByPkNext(int companyId, int journalEntryId)
    {
        using var context = new DataContext();
        return context.TbJournalEntries
            .Where(entry => entry.CompanyID == companyId
                            && entry.JournalEntryID >= journalEntryId
                            && entry.IsActive)
            .OrderByDescending(entry => entry.JournalEntryID)
            .First();
    }

    public TbJournalEntry GetRowByPkBack(int companyId, int journalEntryId)
    {
        using var context = new DataContext();
        return context.TbJournalEntries
            .Where(entry => entry.CompanyID == companyId
                            && entry.JournalEntryID <= journalEntryId
                            && entry.IsActive)
            .OrderBy(entry => entry.JournalEntryID)
            .First();
    }
}