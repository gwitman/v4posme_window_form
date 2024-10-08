using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public class CustomerFrecuencyActuationsModel : ICustomerFrecuencyActuationsModel
{
    public int InsertAppPosme(TbCustomerFrecuencyActuation data)
    {
        using var context = new DataContext();
        var result = context.TbCustomerFrecuencyActuations.Add(data);
        context.SaveChanges();
        return result.Entity.CustomerFrecuencyActuations;
    }

    public void UpdateAppPosme(int entityId, int idFrecuencyActuation, TbCustomerFrecuencyActuation data)
    {
        using var context = new DataContext();
        var find = context.TbCustomerFrecuencyActuations.Find(idFrecuencyActuation);
        if (find is null)
        {
            return;
        }

        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteAppPosme(int entityId)
    {
        using var context = new DataContext();
        context.TbCustomerFrecuencyActuations.Where(actuation => actuation.EntityID == entityId)
            .ExecuteUpdate(calls => calls.SetProperty(actuation => actuation.IsActive, 0));
    }

    public void DeleteWhereIdNotIn(int entityId, List<int?> arrayId)
    {
        using var context = new DataContext();
        context.TbCustomerFrecuencyActuations
            .Where(actuation => actuation.EntityID == entityId && !arrayId.Contains(actuation.CustomerFrecuencyActuations))
            .ExecuteUpdate(calls => calls.SetProperty(actuation => actuation.IsActive, 0));
    }

    public TbCustomerFrecuencyActuation? GetRowByPk(int entityId, int id)
    {
        using var context = new DataContext();
        return context.TbCustomerFrecuencyActuations
            .SingleOrDefault(actuation => actuation.EntityID == entityId && 
                                          actuation.CustomerFrecuencyActuations == id &&
                                          actuation.IsActive!.Value==1);
    }

    public List<TbCustomerFrecuencyActuation> GetrowByEntityId(int entityId)
    {
        using var context = new DataContext();
        return context.TbCustomerFrecuencyActuations
            .Where(actuation => actuation.EntityID == entityId && actuation.IsActive!.Value == 1)
            .OrderByDescending(actuation => actuation.CustomerFrecuencyActuations)
            .ToList();
    }

    public List<TbExpiredRegisterDto> GetRowExpiredRegisters(string userName)
    {
        using var context = new DataContext();
        var currentDate = DateTime.Now;

        var query = from u in context.TbUsers
            join e in context.TbEmployees on u.EmployeeID equals e.EntityID
            join c in context.TbCustomers on e.EntityID equals c.EntityContactID
            join cf in context.TbCustomerFrecuencyActuations on c.EntityID equals cf.EntityID
            join ci in context.TbCatalogItems on cf.FrecuencyContactID equals ci.CatalogItemID
            where cf.CreatedOn!.Value.AddDays(ci.Sequence!.Value) <= currentDate
                  && u.Nickname == userName
                  && cf.IsActive!.Value == 1
                  && cf.IsApply!.Value == 0
            select new TbExpiredRegisterDto(u.Nickname, cf.Name, ci.Description);

        return query.ToList();
    }
}