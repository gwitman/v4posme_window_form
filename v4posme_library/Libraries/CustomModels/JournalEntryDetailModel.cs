using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class JournalEntryDetailModel : IJournalEntryDetailModel
{
    public void DeleteAppPosme(int companyId, int journalEntryId, int journalEntryDetailId)
    {
        using var context = new DataContext();
        context.TbJournalEntryDetails.Where(detail => detail.JournalEntryId == journalEntryId
                                                      && detail.CompanyId == companyId &&
                                                      detail.JournalEntryDetailId == journalEntryDetailId)
            .ExecuteUpdate(calls => calls.SetProperty(detail => detail.IsActive, false));
    }

    public void DeleteWhereIdNotIn(int companyId, int journalEntryId, List<int> listDetailId)
    {
        using var context = new DataContext();
        context.TbJournalEntryDetails.Where(detail => detail.JournalEntryId == journalEntryId
                                                      && detail.CompanyId == companyId
                                                      && !listDetailId.Contains(detail.JournalEntryDetailId))
            .ExecuteUpdate(calls => calls.SetProperty(detail => detail.IsActive, false));
    }

    public void UpdateAppPosme(int companyId, int journalEntryId, int journalEntryDetailId, TbJournalEntryDetail data)
    {
        using var context = new DataContext();
        var tbJournalEntryDetail = context.TbJournalEntryDetails.Single(detail =>
            detail.JournalEntryId == journalEntryId
            && detail.CompanyId == companyId &&
            detail.JournalEntryDetailId == journalEntryDetailId);
        data.JournalEntryDetailId = tbJournalEntryDetail.JournalEntryDetailId;
        context.Entry(tbJournalEntryDetail).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int InsertAppPosme(TbJournalEntryDetail data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.JournalEntryDetailId;
    }

    public List<TbJournalEntryDetailDto> GetRowByJournalEntryId(int companyId, int journalEntryId)
    {
        using var dbContext = new DataContext();
        var result = from jed in dbContext.TbJournalEntryDetails
            join a in dbContext.TbAccounts on jed.AccountId equals a.AccountId
            join cc in dbContext.TbCenterCosts on jed.ClassId equals cc.ClassId into ccGroup
            from cc in ccGroup.DefaultIfEmpty()
            where jed.CompanyId == companyId && jed.JournalEntryId == journalEntryId && jed.IsActive
            select new TbJournalEntryDetailDto
            {
                JournalEntryDetailId = jed.JournalEntryDetailId,
                JournalEntryId = jed.JournalEntryId,
                CompanyId = jed.CompanyId,
                AccountId = jed.AccountId,
                IsActive = jed.IsActive,
                ClassId = jed.ClassId,
                Debit = jed.Debit,
                Credit = jed.Credit,
                Note = jed.Note,
                IsApplied = jed.IsApplied,
                BranchId = jed.BranchId,
                TbExchangeRate = jed.TbExchangeRate,
                ClassNumber = cc.Number,
                AccountNumber = a.AccountNumber,
                AccountName = a.Name
            };
        return result.ToList();
    }

    public TbJournalEntryDetailDto GetRowByPk(int companyId, int journalEntryId, int journalEntryDetailId)
    {
        using var dbContext = new DataContext();
        var result = from jed in dbContext.TbJournalEntryDetails
            join a in dbContext.TbAccounts on jed.AccountId equals a.AccountId
            join cc in dbContext.TbCenterCosts on jed.ClassId equals cc.ClassId into ccGroup
            from cc in ccGroup.DefaultIfEmpty()
            where jed.CompanyId == companyId && jed.JournalEntryId == journalEntryId &&
                  jed.JournalEntryDetailId == journalEntryDetailId && jed.IsActive
            select new TbJournalEntryDetailDto
            {
                JournalEntryDetailId = jed.JournalEntryDetailId,
                JournalEntryId = jed.JournalEntryId,
                CompanyId = jed.CompanyId,
                AccountId = jed.AccountId,
                IsActive = jed.IsActive,
                ClassId = jed.ClassId,
                Debit = jed.Debit,
                Credit = jed.Credit,
                Note = jed.Note,
                IsApplied = jed.IsApplied,
                BranchId = jed.BranchId,
                TbExchangeRate = jed.TbExchangeRate,
                ClassNumber = cc.Number,
                AccountNumber = a.AccountNumber
            };
        return result.First();
    }
}