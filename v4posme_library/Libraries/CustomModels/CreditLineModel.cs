using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public class CreditLineModel : ICreditLineModel
{
    private static IQueryable<TbCreditLine> Find(int companyId, int creditLineId, DataContext context)
    {
        return context.TbCreditLines
            .Where(line => line.CompanyID == companyId
                           && line.CreditLineID == creditLineId);
    }

    public int InsertAppPosme(TbCreditLine data)
    {
        using var context = new DataContext();
        var add = context.TbCreditLines.Add(data);
        context.BulkSaveChanges();
        return add.Entity.CreditLineID;
    }

    public void UpdateAppPosme(int companyId, int creditLineId, TbCreditLine data)
    {
        using var context = new DataContext();
        var find = Find(companyId, creditLineId, context).Single();
        data.CreditLineID = find.CreditLineID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.BulkSaveChanges();
    }

    public void DeleteAppPosme(int companyId, int creditLineId)
    {
        using var context = new DataContext();
        Find(companyId, creditLineId, context)
            .ExecuteUpdate(calls => calls.SetProperty(line => line.IsActive, false));
    }

    public List<TbCreditLine>? GetRowByCompany(int companyId)
    {
        using var context = new DataContext();
        return context.TbCreditLines
            .Where(line => line.CompanyID == companyId
                           && line.IsActive)
            .ToList();
    }

    public TbCreditLine GetRowByPk(int companyId, int creditLineId)
    {
        using var context = new DataContext();
        return Find(companyId, creditLineId, context).Single(line => line.IsActive);
    }
}