﻿using Microsoft.EntityFrameworkCore;
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
                accountingBalance.CompanyId == companyId &&
                accountingBalance.AccountId == accountId &&
                accountingBalance.ComponentPeriodId == componentPeriodId &&
                accountingBalance.ComponentCycleId == componentCycleId)
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
            .Where(summary => summary.CompanyId == companyId
                              && summary.BranchId == branchId
                              && summary.LoginId == loginId)
            .ExecuteDelete();
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
            .Select(tbAccount => new TbAccountingBalance
            {
                ComponentCycleId = cycleId,
                ComponentPeriodId = periodId,
                CompanyId = companyId,
                ComponentId = componentAccountId,
                AccountId = tbAccount.AccountId,
                BranchId = branchId,
                Balance = decimal.Zero,
                Debit = decimal.Zero,
                Credit = decimal.Zero,
                ClassId = 0,
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
            .Where(balance => balance.CompanyId == companyId
                              && balance.ComponentPeriodId == periodId
                              && balance.ComponentCycleId == cycleId)
            .ExecuteUpdate(calls => calls
                .SetProperty(balance => balance.Debit, decimal.Zero)
                .SetProperty(balance => balance.Credit, decimal.Zero));
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
        //al recuperar los datos, este ya incluye el debit y credit, como campos de la tabla
        using var context = new DataContext();
        return context.TbJournalEntryDetailSummaries
            .Single(summary => summary.CompanyId == companyId
                               && summary.BranchId == branchId
                               && summary.LoginId == loginId &&
                               summary.AccountId == accountId);
    }

    public int? GetMinAccountBy(int companyId, int branchId, int loginId, int accountId)
    {
        using var context = new DataContext();
        return context.TbJournalEntryDetailSummaries
            .Where(journal => journal.CompanyId == companyId && journal.BranchId == branchId &&
                              journal.LoginId == loginId && journal.AccountId > accountId)
            .Select(journal => journal.AccountId).Min();
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
            .Where(journal => journal.CompanyId == companyId
                              && journal.BranchId == branchId
                              && journal.LoginId == loginId)
            .Select(journal => journal.AccountId);
    }
}