using System.Diagnostics.CodeAnalysis;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class CustomerCreditDocumentModel : ICustomerCreditDocumentModel
{
    private static IQueryable<TbCustomerCreditDocument> FindDocuments(int customerCreditDocumentId, DataContext context)
    {
        return context.TbCustomerCreditDocuments
            .Where(document => document.CustomerCreditDocumentId == customerCreditDocumentId);
    }

    public void UpdateAppPosme(int customerCreditDocumentId, TbCustomerCreditDocument data)
    {
        using var context = new DataContext();
        var find = FindDocuments(customerCreditDocumentId, context).SingleOrDefault();
        if (find is null)
        {
            return;
        }

        data.CustomerCreditDocumentId = find.CustomerCreditDocumentId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.BulkSaveChanges();
    }

    

    public void DeleteAppPosme(int customerCreditDocumentId)
    {
        using var context = new DataContext();
        var find = FindDocuments(customerCreditDocumentId, context).Single();
        find.IsActive = 0;
        context.BulkSaveChanges();
    }

    public int InsertAppPosme(TbCustomerCreditDocument data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.BulkSaveChanges();
        return add.Entity.CustomerCreditDocumentId;
    }

    public TbCustomerCreditDocumentDto? GetRowByPk(int customerCreditDocumentId)
    {
        using var context = new DataContext();
        var customerCreditDocuments = context.TbCustomerCreditDocuments;
        var creditAmortizations = context.TbCustomerCreditAmortizations;
        var result = from i in customerCreditDocuments
            join cur in context.TbCurrencies on i.CurrencyId equals cur.CurrencyId
            where i.CustomerCreditDocumentId == customerCreditDocumentId && i.IsActive == 1
            select new TbCustomerCreditDocumentDto
            {
                CustomerCreditDocumentId = i.CustomerCreditDocumentId,
                CompanyId = i.CompanyId,
                EntityId = i.EntityId,
                CustomerCreditLineId = i.CustomerCreditLineId,
                DocumentNumber = i.DocumentNumber,
                DateOn = i.DateOn,
                Amount = i.Amount,
                Interes = i.Interes,
                Term = i.Term,
                ExchangeRate = i.ExchangeRate,
                Reference1 = i.Reference1,
                Reference2 = i.Reference2,
                Reference3 = i.Reference3,
                StatusId = i.StatusId,
                IsActive = i.IsActive,
                Balance = i.Balance,
                BalanceProvicioned = i.BalanceProvicioned,
                CurrencyId = i.CurrencyId,
                CurrencyName = cur.Name,
                CurrencySymbol = cur.Simbol,
                BalanceNew = (from ccd in customerCreditDocuments
                    join ccda in creditAmortizations
                        on ccd.CustomerCreditDocumentId equals ccda.CustomerCreditDocumentId
                    where ccd.CustomerCreditDocumentId == i.CustomerCreditDocumentId
                    select ccda.Remaining).Sum(),
                ReportSinRiesgo = i.ReportSinRiesgo
            };
        return result.SingleOrDefault();
    }

    public List<TbCustomerCreditDocument> GetRowByEntity(int companyId, int entityId)
    {
        using var context = new DataContext();
        return context.TbCustomerCreditDocuments
            .Where(document => document.CompanyId == companyId
                               && document.EntityId == entityId
                               && document.IsActive == 1)
            .ToList();
    }

    public List<TbCustomerCreditDocumentDto> GetRowByEntityApplied(int companyId, int entityId, int currencyId)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbCustomerCreditDocuments
            join cc in dbContext.TbCustomerCreditAmortizations
                on i.CustomerCreditDocumentId equals cc.CustomerCreditDocumentId
            join a in dbContext.TbWorkflowStages on i.StatusId equals a.WorkflowStageId
            where i.CompanyId == companyId
                  && i.EntityId == entityId
                  && i.IsActive == 1
                  && a.Aplicable!.Value
                  && i.CurrencyId == currencyId
            group new { i, cc } by new
            {
                i.CustomerCreditDocumentId,
                i.CompanyId,
                i.EntityId,
                i.CustomerCreditLineId,
                i.DocumentNumber,
                i.DateOn,
                i.Amount,
                i.Interes,
                i.Term,
                i.ExchangeRate,
                i.Reference1,
                i.Reference2,
                i.Reference3,
                i.StatusId,
                i.IsActive,
                i.Balance,
                i.CurrencyId,
                i.ReportSinRiesgo
            }
            into g
            select new TbCustomerCreditDocumentDto
            {
                CustomerCreditDocumentId = g.Key.CustomerCreditDocumentId,
                CompanyId = g.Key.CompanyId,
                EntityId = g.Key.EntityId,
                CustomerCreditLineId = g.Key.CustomerCreditLineId,
                DocumentNumber = g.Key.DocumentNumber,
                DateOn = g.Key.DateOn,
                Amount = g.Key.Amount,
                Interes = g.Key.Interes,
                Term = g.Key.Term,
                ExchangeRate = g.Key.ExchangeRate,
                Reference1 = g.Key.Reference1,
                Reference2 = g.Key.Reference2,
                Reference3 = g.Key.Reference3,
                StatusId = g.Key.StatusId,
                IsActive = g.Key.IsActive,
                Balance = g.Key.Balance,
                CurrencyId = g.Key.CurrencyId,
                ReportSinRiesgo = g.Key.ReportSinRiesgo,
                Remaining = g.Sum(x => x.cc.Remaining)
            };
        return result.ToList();
    }

    public List<TbCustomerCreditDocument> GetRowByEntityCreditLine(int companyId, int entityId, int creditLineId)
    {
        using var context = new DataContext();
        return context.TbCustomerCreditDocuments
            .Where(document => document.CompanyId == companyId
                               && document.EntityId == entityId
                               && document.CustomerCreditLineId == creditLineId
                               && document.IsActive == 1)
            .ToList();
    }

    public TbCustomerCreditDocumentDto? GetRowByDocument(int companyId, int entityId, string documentNumber)
    {
        using var context = new DataContext();
        var creditDocuments = context.TbCustomerCreditDocuments;
        var creditAmortizations = context.TbCustomerCreditAmortizations;
      
        try
        {
            var result = from i in creditDocuments
                where i.CompanyId == companyId
                      && i.EntityId == entityId
                      && i.DocumentNumber == documentNumber
                      && i.IsActive == 1
                select new TbCustomerCreditDocumentDto
                {
                    CustomerCreditDocumentId = i.CustomerCreditDocumentId,
                    CompanyId = i.CompanyId,
                    EntityId = i.EntityId,
                    CustomerCreditLineId = i.CustomerCreditLineId,
                    DocumentNumber = i.DocumentNumber,
                    DateOn = i.DateOn,
                    Amount = i.Amount,
                    Interes = i.Interes,
                    Term = i.Term,
                    ExchangeRate = i.ExchangeRate,
                    Reference1 = i.Reference1,
                    Reference2 = i.Reference2,
                    Reference3 = i.Reference3,
                    StatusId = i.StatusId,
                    IsActive = i.IsActive,
                    Balance = i.Balance,
                    CurrencyId = i.CurrencyId,
                    ReportSinRiesgo = i.ReportSinRiesgo,
                    DateFinish = null 
                };

            TbCustomerCreditDocumentDto objCustomerCreditDocumentDto = result.Single();
            if (objCustomerCreditDocumentDto is null)
                objCustomerCreditDocumentDto = new TbCustomerCreditDocumentDto();

            var objListCreditAmortiation = from ccd 
                                           in  creditAmortizations 
                                           where ccd.CustomerCreditDocumentId == objCustomerCreditDocumentDto.CustomerCreditDocumentId 
                                           select new { ccd };


            if(objListCreditAmortiation is not null )
                objCustomerCreditDocumentDto.DateApply = objListCreditAmortiation.Max(u => u.ccd.DateApply);

            return objCustomerCreditDocumentDto;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public List<TbCustomerCreditDocument> GetRowByBalanceBetweenCeroAndCeroPuntoCinco(int companyId)
    {
        using var context = new DataContext();
        return context.TbCustomerCreditDocuments
            .Where(document => document.CompanyId == companyId
                               && document.IsActive == 1
                               && document.Balance > 0
                               && document.Balance <= (decimal)0.2)
            .ToList();
    }

    public List<TbCustomerCreditDocumentDto> GetRowByBalancePending(int companyId, int entityId,
        int customerCreditDocumentId, int currencyId)
    {
        using var dbContext = new DataContext();
        var result = from d in dbContext.TbCustomerCreditDocuments
            join a in dbContext.TbCustomerCreditAmortizations
                on d.CustomerCreditDocumentId equals a.CustomerCreditDocumentId
            join wsa in dbContext.TbWorkflowStages on a.StatusId equals wsa.WorkflowStageId
            join wsd in dbContext.TbWorkflowStages on d.StatusId equals wsd.WorkflowStageId
            where d.IsActive == 1
                  && d.CompanyId == companyId
                  && a.IsActive == 1
                  && wsa.Aplicable.Value
                  && wsd.Aplicable.Value
                  && a.Remaining > 0
                  && d.EntityId == entityId
                  && a.CustomerCreditDocumentId >= customerCreditDocumentId
                  && d.CurrencyId == currencyId
            group new { d, a, wsa } by new
            {
                d.EntityId,
                d.CustomerCreditDocumentId,
                d.CustomerCreditLineId,
                d.DocumentNumber,
                d.Balance,
                d.CurrencyId,
                d.StatusId
            }
            into g
            select new TbCustomerCreditDocumentDto
            {
                EntityId = g.Key.EntityId,
                CustomerCreditDocumentId = g.Key.CustomerCreditDocumentId,
                CustomerCreditLineId = g.Key.CustomerCreditLineId,
                DocumentNumber = g.Key.DocumentNumber,
                Balance = g.Key.Balance,
                CurrencyId = g.Key.CurrencyId,
                StatusId = g.Key.StatusId,
                CreditAmortizationId = g.Min(x => x.a.CustomerCreditDocumentId),
                DateApply = g.Min(x => x.a.DateApply),
                Remaining = g.Sum(x => x.a.Remaining),
                StatusAmotization = g.Min(x => x.a.StatusId),
                StatusAmortizatonName = g.Min(x => x.wsa.Name)
            };
        return result.ToList();
    }
}