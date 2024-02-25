using Devart.Data.MySql.Entity;
using v4posme_library.Models;
using v4posme_library.ModelsViews;

namespace v4posme_library.Libraries.CustomModels;

class CustomerCreditAmortizationModel : ICustomerCreditAmortizationModel
{
    public void UpdateAppPosme(int creditAmortizationId, TbCustomerCreditAmortization data)
    {
        using var context = new DataContext();
        var find = context.TbCustomerCreditAmortizations
            .Find(creditAmortizationId);
        if (find is null) return;
        context.Entry(find).CurrentValues.SetValues(data);
        context.BulkSaveChanges();
    }

    public void DeleteAppPosme(int creditAmortizationId)
    {
        using var context = new DataContext();
        var find = context.TbCustomerCreditAmortizations
            .Find(creditAmortizationId);
        if (find is null) return;
        find.IsActive = 0;
        context.BulkSaveChanges();
    }

    public int InsertAppPosme(TbCustomerCreditAmortization data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.BulkSaveChanges();
        return add.Entity.CreditAmortizationId;
    }

    public TbCustomerCreditAmortization GetRowByPk(int creditAmortizationId)
    {
        using var context = new DataContext();
        return context.TbCustomerCreditAmortizations
            .Single(amortization => amortization.IsActive == 1
                                    && amortization.CreditAmortizationId ==
                                    creditAmortizationId);
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
        return dbContext.TbCustomers
            .Join(dbContext.TbNaturales,
                c => c.EntityId,
                n => n.EntityId,
                (c, n) => new { c, n })
            .Join(dbContext.TbCustomerCreditDocuments,
                t => t.c.EntityId,
                ccd => ccd.EntityId,
                (t, ccd) => new { t.n, t.c, ccd })
            .Join(dbContext.TbCustomerCreditAmortizations,
                t => t.ccd.CustomerCreditDocumentId,
                cca => cca.CustomerCreditDocumentId,
                (t, cca) => new { t.c, t.n, cca, t.ccd })
            .Join(dbContext.TbWorkflowStages, t => t.cca.StatusId, ccaStatus => ccaStatus.WorkflowStageId,
                (t, ccaStatus) => new { t.cca, t.c, t.n, t.ccd, ccaStatus })
            .Join(dbContext.TbWorkflowStages, t => t.ccd.StatusId, ccdStatus => ccdStatus.WorkflowStageId,
                (t, ccdStatus) => new { t.n, t.c, t.ccd, t.cca, ccdStatus })
            .Where(t => t.ccdStatus.Vinculable!.Value && t.c.IsActive == 1 &&
                        t.cca.Remaining > 0 &&
                        t.cca.DateApply < DateTime.Now.Date &&
                        t.c.CompanyId == companyId)
            .Select(t => new CustomerCreditAmortizationView
            {
                CustomerNumber = t.c.CustomerNumber,
                FirstName = t.n.FirstName,
                LastName = t.n.LastName,
                BirthDate = t.c.BirthDate!.Value,
                DocumentNumber = t.ccd.DocumentNumber,
                CurrencyId = t.ccd.CurrencyId,
                ReportSinRiesgo = t.ccd.ReportSinRiesgo,
                DateApply = t.cca.DateApply,
                Remaining = t.cca.Remaining,
                ShareCapital = t.cca.ShareCapital
            }).ToList();
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

    public CustomerCreditAmortizationView GetRowByCreditDocumentAndBalanceMinim(int customerCreditDocumentId)
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
        return result.Single();
    }
}