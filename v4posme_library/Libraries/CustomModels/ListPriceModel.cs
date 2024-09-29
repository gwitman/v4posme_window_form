using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class ListPriceModel : IListPriceModel
{
    public void UpdateAppPosme(int companyId, int listPriceId, TbListPrice data)
    {
        using var context = new DataContext();
        var find = context.TbListPrices
            .Single(price => price.CompanyID == companyId
                             && price.ListPriceID == listPriceId);
        data.ListPriceID = find.ListPriceID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteAppPosme(int companyId, int listPriceId)
    {
        using var context = new DataContext();
        context.TbListPrices
            .Where(price => price.CompanyID == companyId
                            && price.ListPriceID == listPriceId)
            .ExecuteUpdate(calls => calls
                .SetProperty(price => price.IsActive, false));
    }

    public int InsertAppPosme(TbListPrice data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.ListPriceID;
    }

    public TbListPriceDto GetRowByPk(int companyId, int listPriceId)
    {
        using var context = new DataContext();
        var result = from i in context.TbListPrices
            join ws in context.TbWorkflowStages on i.StatusID equals ws.WorkflowStageID
            where i.CompanyID == companyId && i.ListPriceID == listPriceId && i.IsActive
            select new TbListPriceDto
            {
                CompanyId = i.CompanyID,
                ListPriceId = i.ListPriceID,
                StartOn = i.StartOn,
                EndOn = i.EndOn,
                Name = i.Name,
                Description = i.Description,
                StatusId = i.StatusID,
                CreatedOn = i.CreatedOn,
                CreatedIn = i.CreatedIn,
                CreatedBy = i.CreatedBy,
                CreatedAt = i.CreatedAt,
                IsActive = i.IsActive,
                StatusName = ws.Name
            };
        return result.First();
    }

    public TbListPrice? GetListPriceToApply(int companyId)
    {
        using var context = new DataContext();
        return context.TbListPrices
            .Where(price => price!.CompanyID == companyId
                            && DateTime.Now >= price.StartOn
                            && DateTime.Now <= price.EndOn
                            && price.IsActive)
            .OrderByDescending(price => price!.ListPriceID)
            .FirstOrDefault();
    }
}