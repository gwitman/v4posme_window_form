using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class JournalEntryDetailModel : IJournalEntryDetailModel
{
    public void DeleteAppPosme(int companyId, int journalEntryId, int journalEntryDetailId)
    {
        using var context = new DataContext();
        context.TbJournalEntryDetails.Where(detail => detail.JournalEntryID == journalEntryId
                                                      && detail.CompanyID == companyId &&
                                                      detail.JournalEntryDetailID == journalEntryDetailId)
            .ExecuteUpdate(calls => calls.SetProperty(detail => detail.IsActive, false));
    }

    public void DeleteWhereIdNotIn(int companyId, int journalEntryId, List<int> listDetailId)
    {
        using var context = new DataContext();
        context.TbJournalEntryDetails.Where(detail => detail.JournalEntryID == journalEntryId
                                                      && detail.CompanyID == companyId
                                                      && !listDetailId.Contains(detail.JournalEntryDetailID))
            .ExecuteUpdate(calls => calls.SetProperty(detail => detail.IsActive, false));
    }

    public void UpdateAppPosme(int companyId, int journalEntryId, int journalEntryDetailId, TbJournalEntryDetail data)
    {
        using var context = new DataContext();
        var tbJournalEntryDetail = context.TbJournalEntryDetails.Single(detail =>
            detail.JournalEntryID == journalEntryId
            && detail.CompanyID == companyId &&
            detail.JournalEntryDetailID == journalEntryDetailId);
        data.JournalEntryDetailID = tbJournalEntryDetail.JournalEntryDetailID;
        context.Entry(tbJournalEntryDetail).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int InsertAppPosme(TbJournalEntryDetail data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.JournalEntryDetailID;
    }

    public List<TbJournalEntryDetailDto> GetRowByJournalEntryId(int companyId, int journalEntryId)
    {
        using var dbContext = new DataContext();
        var result = from jed in dbContext.TbJournalEntryDetails
            join a in dbContext.TbAccounts on jed.AccountID equals a.AccountID
            join cc in dbContext.TbCenterCosts on jed.ClassID equals cc.ClassID into ccGroup
            from cc in ccGroup.DefaultIfEmpty()
            where jed.CompanyID == companyId && jed.JournalEntryID == journalEntryId && jed.IsActive
            select new TbJournalEntryDetailDto
            {
                JournalEntryDetailId = jed.JournalEntryDetailID,
                JournalEntryId = jed.JournalEntryID,
                CompanyId = jed.CompanyID,
                AccountId = jed.AccountID,
                IsActive = jed.IsActive,
                ClassId = jed.ClassID,
                Debit = jed.Debit,
                Credit = jed.Credit,
                Note = jed.Note,
                IsApplied = jed.IsApplied,
                BranchId = jed.BranchID,
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
            join a in dbContext.TbAccounts on jed.AccountID equals a.AccountID
            join cc in dbContext.TbCenterCosts on jed.ClassID equals cc.ClassID into ccGroup
            from cc in ccGroup.DefaultIfEmpty()
            where jed.CompanyID == companyId && jed.JournalEntryID == journalEntryId &&
                  jed.JournalEntryDetailID == journalEntryDetailId && jed.IsActive
            select new TbJournalEntryDetailDto
            {
                JournalEntryDetailId = jed.JournalEntryDetailID,
                JournalEntryId = jed.JournalEntryID,
                CompanyId = jed.CompanyID,
                AccountId = jed.AccountID,
                IsActive = jed.IsActive,
                ClassId = jed.ClassID,
                Debit = jed.Debit,
                Credit = jed.Credit,
                Note = jed.Note,
                IsApplied = jed.IsApplied,
                BranchId = jed.BranchID,
                TbExchangeRate = jed.TbExchangeRate,
                ClassNumber = cc.Number,
                AccountNumber = a.AccountNumber
            };
        return result.First();
    }
}