using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class CustomerCreditLineModel : ICustomerCreditLineModel
{
    public void UpdateAppPosme(int customerCreditLineId, TbCustomerCreditLine? data)
    {
        using var context = new DataContext();
        var find = context.TbCustomerCreditLines
            .Find(customerCreditLineId);
        if (find == null) return;
        data.CustomerCreditLineID = find.CustomerCreditLineID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int InsertAppPosme(TbCustomerCreditLine data)
    {
        using var context = new DataContext();
        var add = context.TbCustomerCreditLines.Add(data);
        context.SaveChanges();
        return add.Entity.CustomerCreditLineID;
    }

    public void DeleteAppPosme(int customerCreditLineId)
    {
        using var context = new DataContext();
        var find = context.TbCustomerCreditLines.Find(customerCreditLineId);
        if (find is null) return;
        find.IsActive = false;
        context.SaveChanges();
    }

    public void DeleteWhereIdNotIn(int companyId, int branchId, int entityId, List<int> listCustomerCreditLineId)
    {
        using var context = new DataContext();
        context.TbCustomerCreditLines
            .Where(line => line.CompanyID == companyId
                           && line.BranchID == branchId
                           && line.EntityID == entityId
                           && !listCustomerCreditLineId.Contains(line.CustomerCreditLineID))
            .ExecuteUpdate(calls => calls.SetProperty(line => line.IsActive, false));
    }

    public List<TbCustomerCreditLineDto> GetRowByEntityAndLine(int companyId, int branchId, int entityId, int creditLineId)
    {
        using var context = new DataContext();
        var result = from i in context.TbCustomerCreditLines
            join cr in context.TbCurrencies on i.CurrencyID equals cr.CurrencyID
            join cl in context.TbCreditLines on i.CreditLineID equals cl.CreditLineID
            where i.CompanyID == companyId
                  && i.BranchID == branchId
                  && i.EntityID == entityId
                  && i.CreditLineID == creditLineId
                  && i.IsActive
            select new TbCustomerCreditLineDto
            {
                CustomerCreditLineId = i.CustomerCreditLineID,
                CompanyId = i.CompanyID,
                BranchId = i.BranchID,
                EntityId = i.EntityID,
                CreditLineId = i.CreditLineID,
                AccountNumber = i.AccountNumber,
                CurrencyId = i.CurrencyID,
                LimitCredit = i.LimitCredit,
                Balance = i.Balance,
                InterestYear = i.InterestYear,
                InterestPay = i.InterestPay,
                TotalPay = i.TotalPay,
                TotalDefeated = i.TotalDefeated,
                DateOpen = (i.DateOpen),
                PeriodPay = i.PeriodPay,
                DateLastPay =( i.DateLastPay),
                Term = i.Term,
                Note = i.Note,
                StatusId = i.StatusID,
                IsActive = i.IsActive,
                CurrencyName = cr.Name,
                TypeAmortization = i.TypeAmortization,
                CreditLineName = cl.Name
            };
        return result.ToList();
    }

    public List<TbCustomerCreditLineDto> GetRowByEntity(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        var result = from i in context.TbCustomerCreditLines
            join cl in context.TbCreditLines on i.CreditLineID equals cl.CreditLineID
            join ws in context.TbWorkflowStages on i.StatusID equals ws.WorkflowStageID
            join cr in context.TbCurrencies on i.CurrencyID equals cr.CurrencyID
            join ci2 in context.TbCatalogItems on i.PeriodPay equals ci2.CatalogItemID
            join ci3 in context.TbCatalogItems on i.TypeAmortization equals ci3.CatalogItemID
            where i.CompanyID == companyId
                  && i.BranchID == branchId
                  && i.EntityID == entityId
                  && i.IsActive
            select new TbCustomerCreditLineDto
            {
                CustomerCreditLineId = i.CustomerCreditLineID,
                CompanyId = i.CompanyID,
                BranchId = i.BranchID,
                EntityId = i.EntityID,
                CreditLineId = i.CreditLineID,
                CreditLineName= cl.Name,
                AccountNumber = i.AccountNumber,
                CurrencyId = i.CurrencyID,
                LimitCredit = i.LimitCredit,
                DayExcluded = i.DayExcluded,
                Balance = i.Balance,
                InterestYear = i.InterestYear,
                InterestPay = i.InterestPay,
                TotalPay = i.TotalPay,
                TotalDefeated = i.TotalDefeated,
                DateOpen =(i.DateOpen),
                PeriodPay = i.PeriodPay,
                DateLastPay = (i.DateLastPay),
                Term = i.Term,
                Note = i.Note,
                StatusId = i.StatusID,
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

    public List<TbCustomerCreditLineDto> GetRowByEntityBalanceMayorCero(int companyId, int branchId, int entityId)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbCustomerCreditLines
            join cl in dbContext.TbCreditLines on i.CreditLineID equals cl.CreditLineID
            join ws in dbContext.TbWorkflowStages on i.StatusID equals ws.WorkflowStageID
            join cr in dbContext.TbCurrencies on i.CurrencyID equals cr.CurrencyID
            join ci2 in dbContext.TbCatalogItems on i.PeriodPay equals ci2.CatalogItemID
            join ci3 in dbContext.TbCatalogItems on i.TypeAmortization equals ci3.CatalogItemID
            where i.CompanyID == companyId
                  && i.BranchID == branchId
                  && i.EntityID == entityId
                  && i.IsActive
                  && i.Balance > 0
            select new TbCustomerCreditLineDto
            {
                CustomerCreditLineId = i.CustomerCreditLineID,
                CompanyId = i.CompanyID,
                BranchId = i.BranchID,
                EntityId = i.EntityID,
                CreditLineId = i.CreditLineID,
                AccountNumber = i.AccountNumber,
                CurrencyId = i.CurrencyID,
                LimitCredit = i.LimitCredit,
                Balance = i.Balance,
                InterestYear = i.InterestYear,
                InterestPay = i.InterestPay,
                TotalPay = i.TotalPay,
                TotalDefeated = i.TotalDefeated,
                DateOpen = (i.DateOpen),
                PeriodPay = i.PeriodPay,
                DateLastPay = (i.DateLastPay.Value),
                Term = i.Term,
                Note = i.Note,
                StatusId = i.StatusID,
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

    public List<TbCustomerCreditLineDto> GetRowByBranchId(int companyId, int branchId)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbCustomerCreditLines
            join cl in dbContext.TbCreditLines on i.CreditLineID equals cl.CreditLineID
            join ws in dbContext.TbWorkflowStages on i.StatusID equals ws.WorkflowStageID
            join cr in dbContext.TbCurrencies on i.CurrencyID equals cr.CurrencyID
            join ci2 in dbContext.TbCatalogItems on i.PeriodPay equals ci2.CatalogItemID
            join ci3 in dbContext.TbCatalogItems on i.TypeAmortization equals ci3.CatalogItemID
            where i.CompanyID == companyId
                  && i.BranchID == branchId
                  && i.IsActive
            select new TbCustomerCreditLineDto
            {
                CustomerCreditLineId = i.CustomerCreditLineID,
                CompanyId = i.CompanyID,
                BranchId = i.BranchID,
                EntityId = i.EntityID,
                CreditLineId = i.CreditLineID,
                AccountNumber = i.AccountNumber,
                CurrencyId = i.CurrencyID,
                LimitCredit = i.LimitCredit,
                Balance = i.Balance,
                InterestYear = i.InterestYear,
                InterestPay = i.InterestPay,
                TotalPay = i.TotalPay,
                TotalDefeated = i.TotalDefeated,
                DateOpen = (i.DateOpen),
                PeriodPay = i.PeriodPay,
                DateLastPay = (i.DateLastPay.Value),
                Term = i.Term,
                Note = i.Note,
                StatusId = i.StatusID,
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

    public TbCustomerCreditLine? GetRowByPk(int customerCreditLineId)
    {
        using var context = new DataContext();
        return context.TbCustomerCreditLines
            .SingleOrDefault(line => line.CustomerCreditLineID == customerCreditLineId
                            && line.IsActive);
    }
}