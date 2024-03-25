using Devart.Data.MySql.Entity;
using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsViews;

namespace v4posme_library.Libraries.CustomModels;

public class CustomerCreditAmortizationModel : ICustomerCreditAmortizationModel
{
    public void UpdateAppPosme(int creditAmortizationId, TbCustomerCreditAmortization data)
    {
        using var context = new DataContext();
        var find = context.TbCustomerCreditAmortizations
            .Find(creditAmortizationId);
        if (find is null) return;
        data.CreditAmortizationId = find.CreditAmortizationId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteAppPosme(int creditAmortizationId)
    {
        using var context = new DataContext();
        context.TbCustomerCreditAmortizations
            .Where(amortization => amortization.CreditAmortizationId == creditAmortizationId)
            .ExecuteUpdate(calls => calls
                .SetProperty(amortization => amortization.IsActive, (ulong)0));
    }

    public int InsertAppPosme(TbCustomerCreditAmortization data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.CreditAmortizationId;
    }

    public TbCustomerCreditAmortization GetRowByPk(int creditAmortizationId)
    {
        using var context = new DataContext();
        return context.TbCustomerCreditAmortizations
            .First(amortization => amortization.IsActive == 1
                                   && amortization.CreditAmortizationId == creditAmortizationId);
    }

    public List<TbCustomerCreditAmortization> GetRowByDocument(int customerCreditDocumentId)
    {
        using var context = new DataContext();
        return context.TbCustomerCreditAmortizations
            .Where(amortization => amortization.IsActive == 1
                                   && amortization.CustomerCreditDocumentId == customerCreditDocumentId)
            .OrderBy(amortization => amortization.CreditAmortizationId)
            .ToList();
    }

    private static IQueryable<TbCustomerCreditAmortization> FindCreditAmortizations(int customerCreditDocumentId,
        DataContext context)
    {
        return context.TbCustomerCreditAmortizations
            .Join(context.TbWorkflowStages,
                amortization => amortization.StatusId,
                stage => stage.WorkflowStageId,
                (amortization, stage) => new { amortization, stage })
            .Where(key => key.amortization.CustomerCreditDocumentId == customerCreditDocumentId
                          && key.amortization.IsActive == 1
                          && key.stage.Vinculable!.Value)
            .Select(k => k.amortization);
    }

    public List<TbCustomerCreditAmortization> GetRowByDocumentAndVinculable(int customerCreditDocumentId)
    {
        using var context = new DataContext();
        return FindCreditAmortizations(customerCreditDocumentId, context)
            .OrderBy(key => key.DateApply)
            .ToList();
    }

    public List<TbCustomerCreditAmortization> GetRowByDocumentAndNonVinculable(int customerCreditDocumentId)
    {
        using var context = new DataContext();
        return FindCreditAmortizations(customerCreditDocumentId, context)
            .OrderBy(key => key.CreditAmortizationId)
            .ToList();
    }

    public List<CustomerCreditAmortizationView> GetRowByCustomerId(int customerId)
    {
        var container = new[] { 82, 83 };
        using var dbContext = new DataContext();
        return dbContext.TbCustomerCreditAmortizations
            .Join(dbContext.TbWorkflowStages,
                i => i.StatusId,
                ws => ws.WorkflowStageId,
                (i, ws) => new { i, ws })
            .Join(dbContext.TbCustomerCreditDocuments,
                t => t.i.CustomerCreditDocumentId,
                cd => cd.CustomerCreditDocumentId,
                (t, cd) => new { t, cd })
            .Join(dbContext.TbWorkflowStages,
                t => t.cd.StatusId,
                wsd => wsd.WorkflowStageId,
                (t, wsd) => new { t, wsd })
            .Where(t => t.t.t.i.IsActive == 1
                        && t.t.t.ws.Vinculable!.Value == false
                        && t.t.cd.EntityId == customerId
                        && !container.Contains(t.t.cd.StatusId))
            .Select(t => new CustomerCreditAmortizationView
            {
                DocumentNumber = t.t.cd.DocumentNumber,
                DateApply = DateTime.Parse(t.t.t.i.DateApply.ToShortDateString()),
                Remaining = t.t.t.i.Remaining,
                Orden = MySqlFunctions.UnixTimestamp(t.t.t.i.DateApply.ToShortDateString()),
                Mora = MySqlFunctions.Datediff(DateTime.Now, DateTime.Parse(t.t.t.i.DateApply.ToShortDateString())),
                StageCuota = t.t.t.ws.Name,
                StageDocumento = t.wsd.Name
            })
            .ToList();
    }

    public List<CustomerCreditAmortizationView> GetRowShareLate(int companyId)
    {
        using var dbContext = new DataContext();
        var result = from c in dbContext.TbCustomers
            join n in dbContext.TbNaturales on c.EntityId equals n.EntityId
            join ccd in dbContext.TbCustomerCreditDocuments on c.EntityId equals ccd.EntityId
            join cca in dbContext.TbCustomerCreditAmortizations 
                on ccd.CustomerCreditDocumentId equals cca.CustomerCreditDocumentId
            join ccaStatus in dbContext.TbWorkflowStages on cca.StatusId equals ccaStatus.WorkflowStageId
            join ccdStatus in dbContext.TbWorkflowStages on ccd.StatusId equals ccdStatus.WorkflowStageId
            where c.CompanyId == companyId  
                  && ccdStatus.Vinculable!.Value
                  && c.IsActive
                  && cca.Remaining > 0
                  && cca.DateApply < DateTime.Today
            select new CustomerCreditAmortizationView
            {
                CustomerNumber = c.CustomerNumber,
                FirstName = n.FirstName,
                LastName = n.LastName,
                BirthDate = c.BirthDate,
                DocumentNumber = ccd.DocumentNumber,
                CurrencyId = ccd.CurrencyId,
                ReportSinRiesgo = ccd.ReportSinRiesgo,
                DateApply = cca.DateApply,
                Remaining = cca.Remaining,
                ShareCapital = cca.ShareCapital
            };
        return result.ToList();
    }

    public CustomerCreditAmortizationView GetRowBySummaryInformationCredit(string documentNumber)
    {
        using var dbContext = new DataContext();
        var customerCreditAmortizations = dbContext.TbCustomerCreditAmortizations;
        var result = dbContext.TbCustomerCreditDocuments
            .Where(c => c.DocumentNumber == documentNumber && c.IsActive == 1)
            .Select(c => new CustomerCreditAmortizationView
            {
                FechaVencimiento =
                    customerCreditAmortizations
                        .Where(u => u.CustomerCreditDocumentId == c.CustomerCreditDocumentId)
                        .Select(u => u.DateApply).Max(),
                NumeroCuotasPendiente =
                    customerCreditAmortizations.Where(u =>
                            u.CustomerCreditDocumentId == c.CustomerCreditDocumentId
                            && u.Remaining > 0
                            && u.IsActive == 1)
                        .Select(u => u.DateApply).Count(),
                MontoEnMora = customerCreditAmortizations.Where(u =>
                        u.CreditAmortizationId == c.CustomerCreditDocumentId
                        && u.Remaining > 0
                        && u.IsActive == 1
                        && u.DateApply < MySqlFunctions.Now())
                    .Select(u => u.Remaining).Sum()
            });
        return result.Single();
    }

    public List<CustomerCreditAmortizationView> GetRowByCreditDocumentAndBalanceMinim(int customerCreditDocumentId)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbCustomerCreditAmortizations
            join ws in dbContext.TbWorkflowStages
                on i.StatusId equals ws.WorkflowStageId
            join cd in dbContext.TbCustomerCreditDocuments
                on i.CustomerCreditDocumentId equals cd.CustomerCreditDocumentId
            join wsd in dbContext.TbWorkflowStages
                on cd.StatusId equals wsd.WorkflowStageId
            where i.IsActive == 1
                  && ws.Vinculable!.Value
                  && i.Remaining >= decimal.Zero && i.Remaining <= (decimal)0.2
                  && cd.CustomerCreditDocumentId == customerCreditDocumentId
            select new CustomerCreditAmortizationView
            {
                CreditAmortizationId = i.CreditAmortizationId,
                DocumentNumber = cd.DocumentNumber,
                DateApply = i.DateApply,
                Remaining = i.Remaining,
                Orden = Convert.ToUInt32(i.DateApply),
                Mora = MySqlFunctions.Datediff(MySqlFunctions.Now(), i.DateApply),
                StageCuota = ws.Name,
                StageDocumento = wsd.Name
            };
        return result.ToList();
    }
}