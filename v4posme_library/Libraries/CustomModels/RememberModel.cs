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
        find.IsActive = false;
        context.SaveChanges();
    }

    public void UpdateAppPosme(int rememberId, TbRemember data)
    {
        using var context = new DataContext();
        var find = context.TbRemembers.Find(rememberId);
        if (find is null) return;
        data.RememberID = find.RememberID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int InsertAppPosme(TbRemember data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.RememberID;
    }

    public TbRemember GetRowByPk(int rememberId)
    {
        using var context = new DataContext();
        return context.TbRemembers.First(remember => remember.RememberID == rememberId && remember.IsActive);
    }

    public List<TbRemember> GetByCompany(int companyId)
    {
        using var context = new DataContext();
        return context.TbRemembers
            .Where(remember => remember.CompanyID == companyId && remember.IsActive)
            .ToList();
    }

    public List<TbRemember> GetNotificationCompanyByTagId(int companyId, int tagId)
    {
        using var context = new DataContext();
        var result = from c in context.TbRemembers
            join ci in context.TbCatalogItems on c.Period equals ci.CatalogItemID
            join ws in context.TbWorkflowStages on c.StatusID equals ws.WorkflowStageID
            where c.IsActive &&
                  ws.Vinculable!.Value &&
                  c.CompanyID == companyId &&
                  c.TagID == tagId
            select c;
        return result.ToList();
    }

    public List<TbRemember> GetNotificationCompany(int companyId)
    {
        using var context = new DataContext();
        var result = from c in context.TbRemembers
            join ci in context.TbCatalogItems on c.Period equals ci.CatalogItemID
            join ws in context.TbWorkflowStages on c.StatusID equals ws.WorkflowStageID
            where c.IsActive &&
                  ws.Vinculable!.Value &&
                  c.CompanyID == companyId
            select c;
        return result.ToList();
    }

    public TbRememberDto GetProcessNotification(int rememberId, DateTime fechaProcess)
    {
        using var context = new DataContext();
        var result = from c in context.TbRemembers
            join ci in context.TbCatalogItems on c.Period equals ci.CatalogItemID
            let diaProcesado =
                ci.Sequence == 30 ? fechaProcess.Day :
                ci.Sequence == 15 ? (fechaProcess).Day <= 15 ? fechaProcess.Day : fechaProcess.Day - 15 :
                ci.Sequence == 7 ? (int)fechaProcess.DayOfWeek :
                ci.Sequence == 365 ? fechaProcess.DayOfYear :
                0
            where c.RememberID == rememberId
            select new TbRememberDto
            {
                DiaProcesado = diaProcesado,
                Fecha = fechaProcess,
                Title = c.Title,
                Description = c.Description,
                TagId = c.TagID,
                LeerFile = c.LeerFile
            };
        return result.First();
    }
}