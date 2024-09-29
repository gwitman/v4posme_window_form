using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public class AccountingBalanceModel : IAccountingBalanceModel
{
    public void UpdateBalance(int companyId, int componentPeriodId, int componentCycleId, int accountId,
        decimal balance, decimal debit, decimal credit)
    {
        using var context = new DataContext();
        context.TbAccountingBalances
            .Where(accountingBalance =>
                accountingBalance.CompanyID == companyId &&
                accountingBalance.AccountID == accountId &&
                accountingBalance.ComponentPeriodID == componentPeriodId &&
                accountingBalance.ComponentCycleID == componentCycleId)
            .ExecuteUpdate(calls =>
                calls.SetProperty(accountingBalance => accountingBalance.Balance,
                        accountingBalance => accountingBalance.Balance + balance)
                    .SetProperty(accountingBalance => accountingBalance.Debit,
                        accountingBalance => accountingBalance.Debit + debit)
                    .SetProperty(accountingBalance => accountingBalance.Credit,
                        accountingBalance => accountingBalance.Credit + credit));
    }

    public int DeleteJournalEntryDetailSummary(int companyId, int branchId, int loginId)
    {
        using var context = new DataContext();
        return context.TbJournalEntryDetailSummaries
            .Where(summary => summary.CompanyID == companyId
                              && summary.BranchID == branchId
                              && summary.LoginID == loginId)
            .ExecuteDelete();
    }

    public void SetAccountBalance(int companyId, int branchId, int loginId, int cycleId, int periodId,
        int componentAccountId)
    {
        using var context = new DataContext();
        //SELECT accountID FROM tb_accounting_balance where companyID = $companyID and componentPeriodID = $periodID and componentCycleID = $cycleID and isActive = 1
        var accountIds = context.TbAccountingBalances
            .Where(balance => balance.CompanyID == companyId
                              && balance.ComponentPeriodID == periodId
                              && balance.ComponentCycleID == cycleId
                              && balance.IsActive)
            .Select(balance => balance.AccountID)
            .ToList();
        var query = context.TbAccounts
            .Where(account => account.CompanyID == companyId
                              && !accountIds.Contains(account.AccountID)
                              && account.IsActive!.Value)
            .Select(tbAccount => new TbAccountingBalance
            {
                ComponentCycleID = cycleId,
                ComponentPeriodID = periodId,
                CompanyID = companyId,
                ComponentID = componentAccountId,
                AccountID = tbAccount.AccountID,
                BranchID = branchId,
                Balance = decimal.Zero,
                Debit = decimal.Zero,
                Credit = decimal.Zero,
                ClassID = 0,
                IsActive = true
            })
            .ToList();
        context.TbAccountingBalances.AddRange(query);
        context.SaveChanges();
    }

    public void ClearCycle(int companyId, int periodId, int cycleId)
    {
        using var context = new DataContext();
        context.TbAccountingBalances
            .Where(balance => balance.CompanyID == companyId
                              && balance.ComponentPeriodID == periodId
                              && balance.ComponentCycleID == cycleId)
            .ExecuteUpdate(calls => calls
                .SetProperty(balance => balance.Debit, decimal.Zero)
                .SetProperty(balance => balance.Credit, decimal.Zero));
    }

    public void SetJournalSummary(int companyId, int branchId, int loginId, int cycleId, int journalTypeClosed)
    {
        using var context = new DataContext();
        var query = from je in context.TbJournalEntries
            join jed in context.TbJournalEntryDetails on je.JournalEntryID equals jed.JournalEntryID
            join wf in context.TbWorkflowStages on je.StatusID equals wf.WorkflowStageID
            join ac in context.TbAccounts on jed.AccountID equals ac.AccountID  
            where je.CompanyID == jed.CompanyID && je.CompanyID == companyId && je.AccountingCycleID == cycleId &&
                  je.IsActive && jed.IsActive
                  && je.JournalTypeID != journalTypeClosed && Decimal.Add(jed.Debit, jed.Credit) > Decimal.Zero
            group new { je.JournalEntryID, ac.AccountID, ac.ParentAccountID, jed.Debit, jed.Credit } by ac.AccountID
            into g
            select new
            {
                CompanyId = companyId,
                BranchId = branchId,
                LoginId = loginId,
                JournalEntryId = g.Select(x => x.JournalEntryID).First(),
                AccountId = g.Key,
                ParentAccountId = g.Select(x => x.ParentAccountID).First(),
                Debit = g.Sum(x => x.Debit),
                Credit = g.Sum(x => x.Credit)
            };
        var listTbJournalEntryDetailSummary = new List<TbJournalEntryDetailSummary>();
        foreach (var find in query)
        {
            var journal = new TbJournalEntryDetailSummary
            {
                CompanyID = find.CompanyId,
                BranchID = find.BranchId,
                LoginID = find.LoginId,
                JournalEntryID = find.JournalEntryId,
                AccountID = find.AccountId,
                ParentAccountID = find.ParentAccountId,
                Debit = find.Debit,
                Credit = find.Credit
            };
            listTbJournalEntryDetailSummary.Add(journal);
        }

        context.AddRange(listTbJournalEntryDetailSummary);
        context.SaveChanges();
    }

    public TbJournalEntryDetailSummary GetInfoAccount(int companyId, int branchId, int loginId, int accountId)
    {
        //al recuperar los datos, este ya incluye el debit y credit, como campos de la tabla
        using var context = new DataContext();
        return context.TbJournalEntryDetailSummaries
            .Single(summary => summary.CompanyID == companyId
                               && summary.BranchID == branchId
                               && summary.LoginID == loginId &&
                               summary.AccountID == accountId);
    }

    public int? GetMinAccountBy(int companyId, int branchId, int loginId, int accountId)
    {
        using var context = new DataContext();
        return context.TbJournalEntryDetailSummaries
            .Where(journal => journal.CompanyID == companyId && journal.BranchID == branchId &&
                              journal.LoginID == loginId && journal.AccountID > accountId)
            .Select(journal => journal.AccountID).Min();
    }

    public int? GetMinAccount(int companyId, int branchId, int loginId)
    {
        return Journal(companyId, branchId, loginId).Min();
    }

    public int? GetMaxAccount(int companyId, int branchId, int loginId)
    {
        return Journal(companyId, branchId, loginId).Max();
    }

    private static IQueryable<int?> Journal(int companyId, int branchId, int loginId)
    {
        using var context = new DataContext();
        return context.TbJournalEntryDetailSummaries
            .Where(journal => journal.CompanyID == companyId
                              && journal.BranchID == branchId
                              && journal.LoginID == loginId)
            .Select(journal => journal.AccountID);
    }
}