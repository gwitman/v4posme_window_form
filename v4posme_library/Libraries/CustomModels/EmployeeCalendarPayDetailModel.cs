using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class EmployeeCalendarPayDetailModel : IEmployeeCalendarPayDetailModel
{
    public void UpdateAppPosme(int calendarDetailId, TbEmployeeCalendarPayDetail data)
    {
        using var context = new DataContext();
        var find = context.TbEmployeeCalendarPayDetails
            .Single(detail => detail.CalendarDetailId == calendarDetailId);
        data.CalendarDetailId = find.CalendarDetailId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.BulkSaveChanges();
    }

    public void DeleteWhereIdNotIn(int calendarId, List<int> arrayId)
    {
        using var context = new DataContext();
        var find = context.TbEmployeeCalendarPayDetails
            .Where(detail => detail.CalendarId == calendarId
                             && !arrayId.Contains(detail.CalendarDetailId));
        Console.WriteLine(find.ToQueryString());
        find.ExecuteUpdate(calls => calls.SetProperty(detail => detail.IsActive, 0));
    }

    public void DeleteAppPosme(int calendarDetailId)
    {
        using var context = new DataContext();
        var find = context.TbEmployeeCalendarPayDetails
            .Single(detail => detail.CalendarDetailId == calendarDetailId);
        find.IsActive = 0;
        context.BulkSaveChanges();
    }

    public int InsertAppPosme(TbEmployeeCalendarPayDetail data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.BulkSaveChanges();
        return add.Entity.CalendarDetailId;
    }

    public TbEmployeeCalendarPayDetail GetRowByPk(int calendarDetailId)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbEmployeeCalendarPayDetails
            join e in dbContext.TbEmployees on i.EmployeeId equals e.EntityId
            join n in dbContext.TbNaturales on i.EmployeeId equals n.EntityId
            where i.CalendarDetailId == calendarDetailId
                  && i.IsActive == 1
            select new TbEmployeeCalendarPayDetail
            {
                CalendarDetailId = i.CalendarDetailId,
                CalendarId = i.CalendarId,
                EmployeeId = i.EmployeeId,
                Salary = i.Salary,
                Commission = i.Commission,
                Adelantos = i.Adelantos,
                Neto = i.Neto,
                IsActive = i.IsActive,
                FirstName = n.FirstName,
                LastName = n.LastName,
                EmployeNumber = e.EmployeNumber,
                HourCost = e.HourCost,
                ComissionPorcentage = e.ComissionPorcentage
            };
        return result.Single();
    }

    public List<TbEmployeeCalendarPayDetail> GetRowByCalendarId(int calendarId)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbEmployeeCalendarPayDetails
            join e in dbContext.TbEmployees on i.EmployeeId equals e.EntityId
            join n in dbContext.TbNaturales on i.EmployeeId equals n.EntityId
            where i.CalendarId == calendarId
                  && i.IsActive == 1
            select new TbEmployeeCalendarPayDetail
            {
                CalendarDetailId = i.CalendarDetailId,
                CalendarId = i.CalendarId,
                EmployeeId = i.EmployeeId,
                Salary = i.Salary,
                Commission = i.Commission,
                Adelantos = i.Adelantos,
                Neto = i.Neto,
                IsActive = i.IsActive,
                FirstName = n.FirstName,
                LastName = n.LastName,
                EmployeNumber = e.EmployeNumber,
                HourCost = e.HourCost,
                ComissionPorcentage = e.ComissionPorcentage
            };
        return result.ToList();
    }
}