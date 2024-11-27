using Microsoft.EntityFrameworkCore;
using v4posme_library.Libraries.CustomHelper;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public class CustomerCreditAmortizationModel : ICustomerCreditAmortizationModel
{
    public void UpdateAppPosme(int creditAmortizationId, TbCustomerCreditAmoritization data)
    {
        var context = VariablesGlobales.Instance.DataContext;
        var find = context.TbCustomerCreditAmoritizations.SingleOrDefault(amoritization => amoritization.CreditAmortizationID==creditAmortizationId);
        if (find is null) return;
        data.CreditAmortizationID = find.CreditAmortizationID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteAppPosme(int creditAmortizationId)
    {
        using var context = new DataContext();
        context.TbCustomerCreditAmoritizations
            .Where(amortization => amortization.CreditAmortizationID == creditAmortizationId)
            .ExecuteUpdate(calls => calls
                .SetProperty(amortization => amortization.IsActive, false));
    }

    public int InsertAppPosme(TbCustomerCreditAmoritization data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.CreditAmortizationID;
    }

    public TbCustomerCreditAmoritization? GetRowByPk(int creditAmortizationId)
    {
        using var context = new DataContext();
        return context.TbCustomerCreditAmoritizations
            .FirstOrDefault(amortization => amortization.IsActive
                                   && amortization.CreditAmortizationID == creditAmortizationId);
    }

    public List<TbCustomerCreditAmoritization> GetRowByDocument(int customerCreditDocumentId)
    {
        using var context = new DataContext();
        return context.TbCustomerCreditAmoritizations
            .Where(amortization => amortization.IsActive
                                   && amortization.CustomerCreditDocumentID == customerCreditDocumentId)
            .OrderBy(amortization => amortization.CreditAmortizationID)
            .ToList();
    }

    private static IQueryable<TbCustomerCreditAmoritization> FindCreditAmortizations(int customerCreditDocumentId,
        DataContext context)
    {
        return context.TbCustomerCreditAmoritizations
            .Join(context.TbWorkflowStages,
                amortization => amortization.StatusID,
                stage => stage.WorkflowStageID,
                (amortization, stage) => new { amortization, stage })
            .Where(key => key.amortization.CustomerCreditDocumentID == customerCreditDocumentId
                          && key.amortization.IsActive == true
                          && key.stage.Vinculable!.Value)
            .Select(k => k.amortization);
    }

    public List<TbCustomerCreditAmoritization>? GetRowByDocumentAndVinculable(int customerCreditDocumentId)
    {
        using var context = new DataContext();
        return FindCreditAmortizations(customerCreditDocumentId, context)
            .OrderBy(key => key.DateApply.Date)
            .ToList();
    }

    public List<TbCustomerCreditAmoritization> GetRowByDocumentAndNonVinculable(int customerCreditDocumentId)
    {
        using var context = new DataContext();
        return FindCreditAmortizations(customerCreditDocumentId, context)
            .OrderBy(key => key.CreditAmortizationID)
            .ToList();
    }

    public List<TbCustomerCreditAmortizationDto> GetRowByCustomerId(int customerId)
    {
        var container = new[] { 82, 83 };
        using var dbContext = new DataContext();
        return dbContext.TbCustomerCreditAmoritizations
            .Join(dbContext.TbWorkflowStages,
                i => i.StatusID,
                ws => ws.WorkflowStageID,
                (i, ws) => new { i, ws })
            .Join(dbContext.TbCustomerCreditDocuments,
                t => t.i.CustomerCreditDocumentID,
                cd => cd.CustomerCreditDocumentID,
                (t, cd) => new { t, cd })
            .Join(dbContext.TbWorkflowStages,
                t => t.cd.StatusID,
                wsd => wsd.WorkflowStageID,
                (t, wsd) => new { t, wsd })
            .Where(t => t.t.t.i.IsActive
                        && t.t.t.ws.Vinculable!.Value
                        && t.t.cd.EntityID == customerId
                        && !container.Contains(t.t.cd.StatusID))
            .Select(t => new TbCustomerCreditAmortizationDto
            {
                DocumentNumber = t.t.cd.DocumentNumber,
                DateApply = (t.t.t.i.DateApply),
                Remaining = t.t.t.i.Remaining,
                Orden = WebToolsHelper.ToUnixTimestamp(t.t.t.i.DateApply),
                Mora = (int?)(DateTime.Today.Subtract(t.t.t.i.DateApply).TotalDays),
                StageCuota = t.t.t.ws.Name,
                StageDocumento = t.wsd.Name
            })
            .ToList();
    }

    public List<TbCustomerCreditAmortizationDto> GetRowShareLate(int companyId)
    {
        using var dbContext = new DataContext();
        var result = from c in dbContext.TbCustomers
            join n in dbContext.TbNaturales on c.EntityID equals n.EntityID
            join ccd in dbContext.TbCustomerCreditDocuments on c.EntityID equals ccd.EntityID
            join cca in dbContext.TbCustomerCreditAmoritizations
                on ccd.CustomerCreditDocumentID equals cca.CustomerCreditDocumentID
            join ccaStatus in dbContext.TbWorkflowStages on cca.StatusID equals ccaStatus.WorkflowStageID
            join ccdStatus in dbContext.TbWorkflowStages on ccd.StatusID equals ccdStatus.WorkflowStageID
            where c.CompanyID == companyId
                  && ccdStatus.Vinculable!.Value
                  && c.IsActive!.Value
                  && cca.Remaining > 0
                  && cca.DateApply < DateTime.Now
            select new TbCustomerCreditAmortizationDto
            {
                CustomerNumber = c.CustomerNumber,
                FirstName = n.FirstName,
                LastName = n.LastName,
                BirthDate = DateOnly.FromDateTime(c.BirthDate!.Value),
                DocumentNumber = ccd.DocumentNumber,
                CurrencyId = ccd.CurrencyID,
                ReportSinRiesgo = ccd.ReportSinRiesgo,
                DateApply = (cca.DateApply),
                Remaining = cca.Remaining,
                ShareCapital = cca.ShareCapital
            };
        return result.ToList();
    }

    public TbCustomerCreditAmortizationDto GetRowBySummaryInformationCredit(string? documentNumber)
    {
        using var dbContext = new DataContext();
        var customerCreditAmortizations = dbContext.TbCustomerCreditAmoritizations;
        var result = dbContext.TbCustomerCreditDocuments
            .Where(c => c.DocumentNumber == documentNumber && c.IsActive)
            .Select(c => new TbCustomerCreditAmortizationDto
            {
                FechaVencimiento = (
                    customerCreditAmortizations
                        .Where(u => u.CustomerCreditDocumentID == c.CustomerCreditDocumentID)
                        .Select(u => u.DateApply).Max()),
                NumeroCuotasPendiente =
                    customerCreditAmortizations.Where(u =>
                            u.CustomerCreditDocumentID == c.CustomerCreditDocumentID
                            && u.Remaining > 0
                            && u.IsActive)
                        .Select(u => u.DateApply).Count(),
                MontoEnMora = customerCreditAmortizations.Where(u =>
                        u.CreditAmortizationID == c.CustomerCreditDocumentID
                        && u.Remaining > 0
                        && u.IsActive
                        && u.DateApply < DateTime.Now)
                    .Select(u => u.Remaining).Sum()
            });
        return result.Single();
    }

    public List<TbCustomerCreditAmortizationDto> GetRowByCreditDocumentAndBalanceMinim(int customerCreditDocumentId)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbCustomerCreditAmoritizations
            join ws in dbContext.TbWorkflowStages
                on i.StatusID equals ws.WorkflowStageID
            join cd in dbContext.TbCustomerCreditDocuments
                on i.CustomerCreditDocumentID equals cd.CustomerCreditDocumentID
            join wsd in dbContext.TbWorkflowStages
                on cd.StatusID equals wsd.WorkflowStageID
            where i.IsActive
                  && ws.Vinculable!.Value
                  && i.Remaining >= decimal.Zero && i.Remaining <= (decimal)0.2
                  && cd.CustomerCreditDocumentID == customerCreditDocumentId
            select new TbCustomerCreditAmortizationDto
            {
                CreditAmortizationId = i.CreditAmortizationID,
                DocumentNumber = cd.DocumentNumber,
                DateApply = (i.DateApply),
                Remaining = i.Remaining,
                Orden = Convert.ToUInt32(i.DateApply),
                Mora = EF.Functions.DateDiffDay((DateTime.Today), (i.DateApply.Date)),
                StageCuota = ws.Name,
                StageDocumento = wsd.Name
            };
        return result.ToList();
    }
}