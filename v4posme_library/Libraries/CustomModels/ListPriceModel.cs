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
            .Single(price => price.CompanyId == companyId
                             && price.ListPriceId == listPriceId);
        data.ListPriceId = find.ListPriceId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteAppPosme(int companyId, int listPriceId)
    {
        using var context = new DataContext();
        context.TbListPrices
            .Where(price => price.CompanyId == companyId
                            && price.ListPriceId == listPriceId)
            .ExecuteUpdate(calls => calls
                .SetProperty(price => price.IsActive, (ulong)0));
    }

    public int InsertAppPosme(TbListPrice data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.ListPriceId;
    }

    public TbListPriceDto GetRowByPk(int companyId, int listPriceId)
    {
        using var context = new DataContext();
        var result = from i in context.TbListPrices
            join ws in context.TbWorkflowStages on i.StatusId equals ws.WorkflowStageId
            where i.CompanyId == companyId && i.ListPriceId == listPriceId && i.IsActive == 1
            select new TbListPriceDto
            {
                CompanyId = i.CompanyId,
                ListPriceId = i.ListPriceId,
                StartOn = i.StartOn,
                EndOn = i.EndOn,
                Name = i.Name,
                Description = i.Description,
                StatusId = i.StatusId,
                CreatedOn = i.CreatedOn,
                CreatedIn = i.CreatedIn,
                CreatedBy = i.CreatedBy,
                CreatedAt = i.CreatedAt,
                IsActive = i.IsActive,
                StatusName = ws.Name
            };
        return result.First();
    }

    public TbListPrice GetListPriceToApply(int companyId)
    {
        using var context = new DataContext();
        return context.TbListPrices
            .Where(price => price.CompanyId == companyId
                            && DateTime.Now >= price.StartOn
                            && DateTime.Now <= price.EndOn
                            && price.IsActive == 1)
            .OrderByDescending(price => price.ListPriceId)
            .First();
    }
}