using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class CustomerCreditLineModel : ICustomerCreditLineModel
{
    public void UpdateAppPosme(int customerCreditLineId, TbCustomerCreditLine data)
    {
        using var context = new DataContext();
        var find = context.TbCustomerCreditLines
            .Find(customerCreditLineId);
        if (find == null) return;
        data.CustomerCreditLineId = find.CustomerCreditLineId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.BulkSaveChanges();
    }

    public int InsertAppPosme(TbCustomerCreditLine data)
    {
        using var context = new DataContext();
        var add = context.TbCustomerCreditLines.Add(data);
        context.SaveChanges();
        return add.Entity.CustomerCreditLineId;
    }

    public void DeleteAppPosme(int customerCreditLineId)
    {
        using var context = new DataContext();
        var find = context.TbCustomerCreditLines.Find(customerCreditLineId);
        if (find is null) return;
        find.IsActive = 0;
        context.BulkSaveChanges();
    }

    public void DeleteWhereIdNotIn(int companyId, int branchId, int entityId, List<int> listCustomerCreditLineId)
    {
        using var context = new DataContext();
        context.TbCustomerCreditLines
            .Where(line => line.CompanyId == companyId
                           && line.BranchId == branchId
                           && line.EntityId == entityId)
            .WhereBulkNotContains(listCustomerCreditLineId, line => line.CustomerCreditLineId)
            .ExecuteUpdate(calls => calls.SetProperty(line => line.IsActive, 0));
    }

    public List<TbCustomerCreditLine> GetRowByEntityAndLine(int companyId, int branchId, int entityId, int creditLineId)
    {
        using var context = new DataContext();
        var result = from i in context.TbCustomerCreditLines
            join cr in context.TbCurrencies on i.CurrencyId equals cr.CurrencyId
            join cl in context.TbCreditLines on i.CreditLineId equals cl.CreditLineId
            where i.CompanyId == companyId
                  && i.BranchId == branchId
                  && i.EntityId == entityId
                  && i.CreditLineId == creditLineId
                  && i.IsActive == 1
            select new TbCustomerCreditLine
            {
                CustomerCreditLineId = i.CustomerCreditLineId,
                CompanyId = i.CompanyId,
                BranchId = i.BranchId,
                EntityId = i.EntityId,
                CreditLineId = i.CreditLineId,
                AccountNumber = i.AccountNumber,
                CurrencyId = i.CurrencyId,
                LimitCredit = i.LimitCredit,
                Balance = i.Balance,
                InterestYear = i.InterestYear,
                InterestPay = i.InterestPay,
                TotalPay = i.TotalPay,
                TotalDefeated = i.TotalDefeated,
                DateOpen = i.DateOpen,
                PeriodPay = i.PeriodPay,
                DateLastPay = i.DateLastPay,
                Term = i.Term,
                Note = i.Note,
                StatusId = i.StatusId,
                IsActive = i.IsActive,
                CurrencyName = cr.Name,
                TypeAmortization = i.TypeAmortization,
                CreditLineName = cl.Name
            };
        return result.ToList();
    }

    public List<TbCustomerCreditLine> GetRowByEntity(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        var result = from i in context.TbCustomerCreditLines
            join cl in context.TbCreditLines on i.CreditLineId equals cl.CreditLineId
            join ws in context.TbWorkflowStages on i.StatusId equals ws.WorkflowStageId
            join cr in context.TbCurrencies on i.CurrencyId equals cr.CurrencyId
            join ci2 in context.TbCatalogItems on i.PeriodPay equals ci2.CatalogItemId
            join ci3 in context.TbCatalogItems on i.TypeAmortization equals ci3.CatalogItemId
            where i.CompanyId == companyId
                  && i.BranchId == branchId
                  && i.EntityId == entityId
                  && i.IsActive == 1
            select new TbCustomerCreditLine
            {
                CustomerCreditLineId = i.CustomerCreditLineId,
                CompanyId = i.CompanyId,
                BranchId = i.BranchId,
                EntityId = i.EntityId,
                CreditLineId = i.CreditLineId,
                AccountNumber = i.AccountNumber,
                CurrencyId = i.CurrencyId,
                LimitCredit = i.LimitCredit,
                Balance = i.Balance,
                InterestYear = i.InterestYear,
                InterestPay = i.InterestPay,
                TotalPay = i.TotalPay,
                TotalDefeated = i.TotalDefeated,
                DateOpen = i.DateOpen,
                PeriodPay = i.PeriodPay,
                DateLastPay = i.DateLastPay,
                Term = i.Term,
                Note = i.Note,
                StatusId = i.StatusId,
                IsActive = i.IsActive,
                Line = cl.Name,
                StatusName = ws.Name,
                CurrencyName = cr.Name,
                TypeAmortization = i.TypeAmortization,
                TypeAmortizationLabel = ci3.Name,
                PeriodPayLabel = ci2.Name
            };
        return result.ToList();
    }

    public List<TbCustomerCreditLine> GetRowByEntityBalanceMayorCero(int companyId, int branchId, int entityId)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbCustomerCreditLines
            join cl in dbContext.TbCreditLines on i.CreditLineId equals cl.CreditLineId
            join ws in dbContext.TbWorkflowStages on i.StatusId equals ws.WorkflowStageId
            join cr in dbContext.TbCurrencies on i.CurrencyId equals cr.CurrencyId
            join ci2 in dbContext.TbCatalogItems on i.PeriodPay equals ci2.CatalogItemId
            join ci3 in dbContext.TbCatalogItems on i.TypeAmortization equals ci3.CatalogItemId
            where i.CompanyId == companyId
                  && i.BranchId == branchId
                  && i.EntityId == entityId
                  && i.IsActive == 1
                  && i.Balance > 0
            select new TbCustomerCreditLine
            {
                CustomerCreditLineId = i.CustomerCreditLineId,
                CompanyId = i.CompanyId,
                BranchId = i.BranchId,
                EntityId = i.EntityId,
                CreditLineId = i.CreditLineId,
                AccountNumber = i.AccountNumber,
                CurrencyId = i.CurrencyId,
                LimitCredit = i.LimitCredit,
                Balance = i.Balance,
                InterestYear = i.InterestYear,
                InterestPay = i.InterestPay,
                TotalPay = i.TotalPay,
                TotalDefeated = i.TotalDefeated,
                DateOpen = i.DateOpen,
                PeriodPay = i.PeriodPay,
                DateLastPay = i.DateLastPay,
                Term = i.Term,
                Note = i.Note,
                StatusId = i.StatusId,
                IsActive = i.IsActive,
                Line = cl.Name,
                StatusName = ws.Name,
                CurrencyName = cr.Name,
                TypeAmortization = i.TypeAmortization,
                TypeAmortizationLabel = ci3.Name,
                PeriodPayLabel = ci2.Name
            };
        return result.ToList();
    }

    public List<TbCustomerCreditLine> GetRowByBranchId(int companyId, int branchId)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbCustomerCreditLines
            join cl in dbContext.TbCreditLines on i.CreditLineId equals cl.CreditLineId
            join ws in dbContext.TbWorkflowStages on i.StatusId equals ws.WorkflowStageId
            join cr in dbContext.TbCurrencies on i.CurrencyId equals cr.CurrencyId
            join ci2 in dbContext.TbCatalogItems on i.PeriodPay equals ci2.CatalogItemId
            join ci3 in dbContext.TbCatalogItems on i.TypeAmortization equals ci3.CatalogItemId
            where i.CompanyId == companyId
                  && i.BranchId == branchId
                  && i.IsActive == 1
            select new TbCustomerCreditLine
            {
                CustomerCreditLineId = i.CustomerCreditLineId,
                CompanyId = i.CompanyId,
                BranchId = i.BranchId,
                EntityId = i.EntityId,
                CreditLineId = i.CreditLineId,
                AccountNumber = i.AccountNumber,
                CurrencyId = i.CurrencyId,
                LimitCredit = i.LimitCredit,
                Balance = i.Balance,
                InterestYear = i.InterestYear,
                InterestPay = i.InterestPay,
                TotalPay = i.TotalPay,
                TotalDefeated = i.TotalDefeated,
                DateOpen = i.DateOpen,
                PeriodPay = i.PeriodPay,
                DateLastPay = i.DateLastPay,
                Term = i.Term,
                Note = i.Note,
                StatusId = i.StatusId,
                IsActive = i.IsActive,
                Line = cl.Name,
                StatusName = ws.Name,
                CurrencyName = cr.Name,
                TypeAmortization = i.TypeAmortization,
                TypeAmortizationLabel = ci3.Name,
                PeriodPayLabel = ci2.Name
            };
        return result.ToList();
    }

    public TbCustomerCreditLine GetRowByPk(int customerCreditLineId)
    {
        using var context = new DataContext();
        return context.TbCustomerCreditLines
            .Single(line => line.CustomerCreditLineId == customerCreditLineId
                            && line.IsActive == 1);
    }
}