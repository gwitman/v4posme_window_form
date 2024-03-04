using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public class AccountingBalanceModel : IAccountingBalanceModel
{
    public TbAccountingBalance UpdateBalance(int companyId, int componentPeriodId, int componentCycleId, int accountId,
        decimal balance, decimal debit, decimal credit)
    {
        using var context = new DataContext();
        var find = context.TbAccountingBalances
            .Single(accountingBalance =>
                accountingBalance.ComponentPeriodId == componentPeriodId &&
                accountingBalance.CompanyId == companyId &&
                accountingBalance.AccountId == accountId &&
                accountingBalance.ComponentCycleId == componentCycleId);
        find.Balance = balance;
        find.Debit = debit;
        find.Credit = credit;
        context.SaveChanges();
        return find;
    }

    public int DeleteJournalEntryDetailSummary(int companyId, int branchId, int loginId)
    {
        using var context = new DataContext();
        var find = context.TbJournalEntryDetailSummaries.Single(summary =>
            summary.CompanyId == companyId && summary.BranchId == branchId && summary.LoginId == loginId);
        context.TbJournalEntryDetailSummaries.Remove(find);
        return context.SaveChanges();
    }

    public void SetAccountBalance(int companyId, int branchId, int loginId, int cycleId, int periodId,
        int componentAccountId)
    {
        using var context = new DataContext();
        //SELECT accountID FROM tb_accounting_balance where companyID = $companyID and componentPeriodID = $periodID and componentCycleID = $cycleID and isActive = 1
        var accountIds = context.TbAccountingBalances
            .Where(balance => balance.CompanyId == companyId
                              && balance.ComponentPeriodId == periodId
                              && balance.ComponentCycleId == cycleId
                              && balance.IsActive)
            .Select(balance => balance.AccountId)
            .ToList();
        var query = context.TbAccounts
            .Where(account => account.CompanyId == companyId
                              && !accountIds.Contains(account.AccountId)
                              && account.IsActive!.Value)
            .ToList();
        var listAccountingBalance = query.Select(tbAccount => new TbAccountingBalance
            {
                ComponentCycleId = cycleId,
                ComponentPeriodId = periodId,
                CompanyId = companyId,
                ComponentId = componentAccountId,
                AccountId = tbAccount.AccountId,
                BranchId = branchId,
                Balance = Decimal.Zero,
                Debit = Decimal.Zero,
                Credit = Decimal.Zero,
                ClassId = 0,
                IsActive = true
            })
            .ToList();

        context.TbAccountingBalances.AddRange(listAccountingBalance);
        context.SaveChanges();
    }

    public void ClearCycle(int companyId, int periodId, int cycleId)
    {
        using var context = new DataContext();
        var find = context.TbAccountingBalances
            .Single(balance => balance.CompanyId == companyId
                               && balance.ComponentPeriodId == periodId
                               && balance.ComponentCycleId == cycleId);
        find.Debit = Decimal.Zero;
        find.Credit = Decimal.Zero;
        context.BulkSaveChanges();
    }

    public void SetJournalSummary(int companyId, int branchId, int loginId, int cycleId, int journalTypeClosed)
    {
        using var context = new DataContext();
        var query = from je in context.TbJournalEntries
            join jed in context.TbJournalEntryDetails on je.JournalEntryId equals jed.JournalEntryId
            join wf in context.TbWorkflowStages on je.StatusId equals wf.WorkflowStageId
            join ac in context.TbAccounts on jed.AccountId equals ac.AccountId
            where je.CompanyId == jed.CompanyId && je.CompanyId == companyId && je.AccountingCycleId == cycleId &&
                  je.IsActive && jed.IsActive
                  && je.JournalTypeId != journalTypeClosed && Decimal.Add(jed.Debit, jed.Credit) > Decimal.Zero
            group new { je.JournalEntryId, ac.AccountId, ac.ParentAccountId, jed.Debit, jed.Credit } by ac.AccountId
            into g
            select new
            {
                CompanyId = companyId,
                BranchId = branchId,
                LoginId = loginId,
                JournalEntryId = g.Select(x => x.JournalEntryId).First(),
                AccountId = g.Key,
                ParentAccountId = g.Select(x => x.ParentAccountId).First(),
                Debit = g.Sum(x => x.Debit),
                Credit = g.Sum(x => x.Credit)
            };
        var listTbJournalEntryDetailSummary = new List<TbJournalEntryDetailSummary>();
        foreach (var find in query)
        {
            var journal = new TbJournalEntryDetailSummary
            {
                CompanyId = find.CompanyId,
                BranchId = find.BranchId,
                LoginId = find.LoginId,
                JournalEntryId = find.JournalEntryId,
                AccountId = find.AccountId,
                ParentAccountId = find.ParentAccountId,
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
        using var context = new DataContext();
        return context.TbJournalEntryDetailSummaries.Single(summary =>
            summary.CompanyId == companyId && summary.BranchId == branchId && summary.LoginId == loginId &&
            summary.AccountId == accountId);
    }

    public int? GetMinAccountBy(int companyId, int branchId, int loginId, int minAccountId)
    {
        using var context = new DataContext();
        return (from journal in context.TbJournalEntryDetailSummaries
            where journal.CompanyId == companyId
                  && journal.BranchId == branchId
                  && journal.LoginId == loginId
                  && journal.AccountId > minAccountId
            select journal.AccountId).Single();
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
        return from journal in context.TbJournalEntryDetailSummaries
            where journal.CompanyId == companyId
                  && journal.BranchId == branchId
                  && journal.LoginId == loginId
            select journal.AccountId;
    }
}