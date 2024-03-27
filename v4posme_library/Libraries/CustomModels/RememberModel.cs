using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class RememberModel : IRememberModel
{
    public void DeleteAppPosme(int rememberId)
    {
        using var context = new DataContext();
        var find = context.TbRemembers.Find(rememberId);
        if (find is null) return;
        find.IsActive = 0;
        context.SaveChanges();
    }

    public void UpdateAppPosme(int rememberId, TbRemember data)
    {
        using var context = new DataContext();
        var find = context.TbRemembers.Find(rememberId);
        if (find is null) return;
        data.RememberId = find.RememberId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int InsertAppPosme(TbRemember data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.RememberId;
    }

    public TbRemember GetRowByPk(int rememberId)
    {
        using var context = new DataContext();
        return context.TbRemembers.First(remember => remember.RememberId == rememberId && remember.IsActive == 1);
    }

    public List<TbRemember> GetByCompany(int companyId)
    {
        using var context = new DataContext();
        return context.TbRemembers
            .Where(remember => remember.CompanyId == companyId && remember.IsActive == 1)
            .ToList();
    }

    public List<TbRemember> GetNotificationCompanyByTagId(int companyId, int tagId)
    {
        using var context = new DataContext();
        var result = from c in context.TbRemembers
            join ci in context.TbCatalogItems on c.Period equals ci.CatalogItemId
            join ws in context.TbWorkflowStages on c.StatusId equals ws.WorkflowStageId
            where c.IsActive == 1 &&
                  ws.Vinculable.Value &&
                  c.CompanyId == companyId &&
                  c.TagId == tagId
            select c;
        return result.ToList();
    }

    public List<TbRemember> GetNotificationCompany(int companyId)
    {
        using var context = new DataContext();
        var result = from c in context.TbRemembers
            join ci in context.TbCatalogItems on c.Period equals ci.CatalogItemId
            join ws in context.TbWorkflowStages on c.StatusId equals ws.WorkflowStageId
            where c.IsActive == 1 &&
                  ws.Vinculable.Value &&
                  c.CompanyId == companyId
            select c;
        return result.ToList();
    }

    public TbRememberDto GetProcessNotification(int rememberId, DateTime fechaProcess)
    {
        using var context = new DataContext();
        var result = from c in context.TbRemembers
            join ci in context.TbCatalogItems on c.Period equals ci.CatalogItemId
            let diaProcesado =
                ci.Sequence == 30 ? fechaProcess.Day :
                ci.Sequence == 15 ? (fechaProcess).Day <= 15 ? fechaProcess.Day : fechaProcess.Day - 15 :
                ci.Sequence == 7 ? (int)fechaProcess.DayOfWeek :
                ci.Sequence == 365 ? fechaProcess.DayOfYear :
                0
            where c.RememberId == rememberId
            select new TbRememberDto
            {
                DiaProcesado = diaProcesado,
                Fecha = fechaProcess,
                Title = c.Title,
                Description = c.Description,
                TagId = c.TagId,
                LeerFile = c.LeerFile
            };
        return result.First();
    }
}