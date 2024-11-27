using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class CustomerCreditDocumentModel : ICustomerCreditDocumentModel
{
    private static IQueryable<TbCustomerCreditDocument> FindDocuments(int customerCreditDocumentId, DataContext context)
    {
        return context.TbCustomerCreditDocuments
            .Where(document => document.CustomerCreditDocumentID == customerCreditDocumentId);
    }

    public void UpdateAppPosme(int customerCreditDocumentId, TbCustomerCreditDocument data)
    {
        using var context = VariablesGlobales.Instance.DataContext;
        var find = FindDocuments(customerCreditDocumentId, context).SingleOrDefault();
        if (find is null)
        {
            return;
        }

        data.CustomerCreditDocumentID = find.CustomerCreditDocumentID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }


    public void DeleteAppPosme(int customerCreditDocumentId)
    {
        using var context = new DataContext();
        var find = FindDocuments(customerCreditDocumentId, context).Single();
        find.IsActive = false;
        context.SaveChanges();
    }

    public int InsertAppPosme(TbCustomerCreditDocument data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.CustomerCreditDocumentID;
    }

    public TbCustomerCreditDocumentDto? GetRowByPk(int customerCreditDocumentId)
    {
        using var context = new DataContext();
        var customerCreditDocuments = context.TbCustomerCreditDocuments.AsNoTracking();
        var creditAmortizations = context.TbCustomerCreditAmoritizations.AsNoTracking();
        var result = from i in customerCreditDocuments
            join cur in context.TbCurrencies on i.CurrencyID equals cur.CurrencyID
            where i.CustomerCreditDocumentID == customerCreditDocumentId && i.IsActive
            select new TbCustomerCreditDocumentDto
            {
                CustomerCreditDocumentId = i.CustomerCreditDocumentID,
                CompanyId = i.CompanyID,
                EntityId = i.EntityID,
                CustomerCreditLineId = i.CustomerCreditLineID,
                DocumentNumber = i.DocumentNumber,
                DateOn = i.DateOn,
                Amount = i.Amount,
                Interes = i.Interes,
                Term = i.Term,
                ExchangeRate = i.ExchangeRate,
                Reference1 = i.Reference1,
                Reference2 = i.Reference2,
                Reference3 = i.Reference3,
                StatusId = i.StatusID,
                IsActive = i.IsActive,
                Balance = i.Balance,
                BalanceProvicioned = i.BalanceProvicioned,
                CurrencyId = i.CurrencyID,
                CurrencyName = cur.Name,
                CurrencySymbol = cur.Simbol,
                BalanceNew = (from ccd in customerCreditDocuments
                    join ccda in creditAmortizations
                        on ccd.CustomerCreditDocumentID equals ccda.CustomerCreditDocumentID
                    where ccd.CustomerCreditDocumentID == i.CustomerCreditDocumentID
                    select ccda.Remaining).Sum(),
                ReportSinRiesgo = i.ReportSinRiesgo,
                DateFinish = customerCreditDocuments.Where(document => document.CustomerCreditDocumentID == i.CustomerCreditDocumentID)
                    .Join(creditAmortizations, document => document.CustomerCreditDocumentID, amoritization => amoritization.CustomerCreditDocumentID,
                        (document, amoritization) => amoritization.DateApply).Max()
            };
        return result.SingleOrDefault();
    }

    public TbCustomerCreditDocument? GetRowByPkk(int customerCreditDocumentId)
    {
        using var context = new DataContext();
        return context.TbCustomerCreditDocuments.AsNoTracking().SingleOrDefault(document => document.CustomerCreditDocumentID == customerCreditDocumentId);
    }

    public List<TbCustomerCreditDocument> GetRowByEntity(int companyId, int entityId)
    {
        using var context = new DataContext();
        return context.TbCustomerCreditDocuments
            .Where(document => document.CompanyID == companyId
                               && document.EntityID == entityId
                               && document.IsActive)
            .ToList();
    }

    public List<TbCustomerCreditDocumentDto> GetRowByEntityApplied(int companyId, int entityId, int currencyId)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbCustomerCreditDocuments
            join cc in dbContext.TbCustomerCreditAmoritizations
                on i.CustomerCreditDocumentID equals cc.CustomerCreditDocumentID
            join a in dbContext.TbWorkflowStages on i.StatusID equals a.WorkflowStageID
            where i.CompanyID == companyId
                  && i.EntityID == entityId
                  && i.IsActive
                  && a.Aplicable!.Value
                  && i.CurrencyID == currencyId
            group new { i, cc } by new
            {
                i.CustomerCreditDocumentID,
                i.CompanyID,
                i.EntityID,
                i.CustomerCreditLineID,
                i.DocumentNumber,
                i.DateOn,
                i.Amount,
                i.Interes,
                i.Term,
                i.ExchangeRate,
                i.Reference1,
                i.Reference2,
                i.Reference3,
                i.StatusID,
                i.IsActive,
                i.Balance,
                i.CurrencyID,
                i.ReportSinRiesgo
            }
            into g
            select new TbCustomerCreditDocumentDto
            {
                CustomerCreditDocumentId = g.Key.CustomerCreditDocumentID,
                CompanyId = g.Key.CompanyID,
                EntityId = g.Key.EntityID,
                CustomerCreditLineId = g.Key.CustomerCreditLineID,
                DocumentNumber = g.Key.DocumentNumber,
                DateOn = g.Key.DateOn,
                Amount = g.Key.Amount,
                Interes = g.Key.Interes,
                Term = g.Key.Term,
                ExchangeRate = g.Key.ExchangeRate,
                Reference1 = g.Key.Reference1,
                Reference2 = g.Key.Reference2,
                Reference3 = g.Key.Reference3,
                StatusId = g.Key.StatusID,
                IsActive = g.Key.IsActive,
                Balance = g.Key.Balance,
                CurrencyId = g.Key.CurrencyID,
                ReportSinRiesgo = g.Key.ReportSinRiesgo,
                Remaining = g.Sum(x => x.cc.Remaining)
            };
        return result.ToList();
    }

    public List<TbCustomerCreditDocument> GetRowByEntityCreditLine(int companyId, int entityId, int creditLineId)
    {
        using var context = new DataContext();
        return context.TbCustomerCreditDocuments
            .Where(document => document.CompanyID == companyId
                               && document.EntityID == entityId
                               && document.CustomerCreditLineID == creditLineId
                               && document.IsActive)
            .ToList();
    }

    public TbCustomerCreditDocumentDto? GetRowByDocument(int companyId, int entityId, string? documentNumber)
    {
        using var context = new DataContext();
        var creditDocuments = context.TbCustomerCreditDocuments;
        var creditAmortizations = context.TbCustomerCreditAmoritizations;

        try
        {
            var result = from i in creditDocuments
                where i.CompanyID == companyId
                      && i.EntityID == entityId
                      && i.DocumentNumber == documentNumber
                      && i.IsActive
                select new TbCustomerCreditDocumentDto
                {
                    CustomerCreditDocumentId = i.CustomerCreditDocumentID,
                    CompanyId = i.CompanyID,
                    EntityId = i.EntityID,
                    CustomerCreditLineId = i.CustomerCreditLineID,
                    DocumentNumber = i.DocumentNumber,
                    DateOn = i.DateOn,
                    Amount = i.Amount,
                    Interes = i.Interes,
                    Term = i.Term,
                    ExchangeRate = i.ExchangeRate,
                    Reference1 = i.Reference1,
                    Reference2 = i.Reference2,
                    Reference3 = i.Reference3,
                    StatusId = i.StatusID,
                    IsActive = i.IsActive,
                    Balance = i.Balance,
                    CurrencyId = i.CurrencyID,
                    ReportSinRiesgo = i.ReportSinRiesgo,
                    DateFinish = null
                };

            var objCustomerCreditDocumentDto = result.SingleOrDefault();
            if (objCustomerCreditDocumentDto is null)
                objCustomerCreditDocumentDto = new TbCustomerCreditDocumentDto();

            var objListCreditAmortiation = from ccd
                    in creditAmortizations
                where ccd.CustomerCreditDocumentID == objCustomerCreditDocumentDto.CustomerCreditDocumentId
                select new { ccd };


            objCustomerCreditDocumentDto.DateApply = (objListCreditAmortiation.Max(u => u.ccd.DateApply));

            return objCustomerCreditDocumentDto;
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            return null;
        }
    }

    public List<TbCustomerCreditDocument> GetRowByBalanceBetweenCeroAndCeroPuntoCinco(int companyId)
    {
        using var context = new DataContext();
        return context.TbCustomerCreditDocuments
            .Where(document => document.CompanyID == companyId
                               && document.IsActive
                               && document.Balance > 0
                               && document.Balance <= (decimal)0.2)
            .ToList();
    }

    public List<TbCustomerCreditDocumentDto> GetRowByBalancePending(int companyId, int entityId,
        int customerCreditDocumentId, int currencyId)
    {
        using var dbContext = new DataContext();
        var result = from d in dbContext.TbCustomerCreditDocuments
            join a in dbContext.TbCustomerCreditAmoritizations
                on d.CustomerCreditDocumentID equals a.CustomerCreditDocumentID
            join wsa in dbContext.TbWorkflowStages on a.StatusID equals wsa.WorkflowStageID
            join wsd in dbContext.TbWorkflowStages on d.StatusID equals wsd.WorkflowStageID
            where d.IsActive
                  && d.CompanyID == companyId
                  && a.IsActive
                  && wsa.Aplicable!.Value
                  && wsd.Aplicable!.Value
                  && a.Remaining > 0
                  && d.EntityID == entityId
                  && a.CustomerCreditDocumentID >= customerCreditDocumentId
                  && d.CurrencyID == currencyId
            group new { d, a, wsa } by new
            {
                d.EntityID,
                d.CustomerCreditDocumentID,
                d.CustomerCreditLineID,
                d.DocumentNumber,
                d.Balance,
                d.CurrencyID,
                d.StatusID
            }
            into g
            select new TbCustomerCreditDocumentDto
            {
                EntityId = g.Key.EntityID,
                CustomerCreditDocumentId = g.Key.CustomerCreditDocumentID,
                CustomerCreditLineId = g.Key.CustomerCreditLineID,
                DocumentNumber = g.Key.DocumentNumber,
                Balance = g.Key.Balance,
                CurrencyId = g.Key.CurrencyID,
                StatusId = g.Key.StatusID,
                CreditAmortizationId = g.Min(x => x.a.CreditAmortizationID),
                DateApply = g.Min(x => x.a.DateApply),
                Remaining = g.Sum(x => x.a.Remaining),
                StatusAmotization = g.Min(x => x.a.StatusID),
                StatusAmortizatonName = g.Min(x => x.wsa.Name)
            };
        return result.ToList();
    }
}