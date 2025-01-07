using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class EmployeeCalendarPayDetailModel : IEmployeeCalendarPayDetailModel
{
    public void UpdateAppPosme(int calendarDetailId, TbEmployeeCalendarPayDetail data)
    {
        using var context = new DataContext();
        var find = context.TbEmployeeCalendarPayDetails
            .Single(detail => detail.CalendarDetailID == calendarDetailId);
        data.CalendarDetailID = find.CalendarDetailID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteWhereIdNotIn(int calendarId, List<int> arrayId)
    {
        using var context = new DataContext();
        var find = context.TbEmployeeCalendarPayDetails
            .Where(detail => detail.CalendarID == calendarId
                             && !arrayId.Contains(detail.CalendarDetailID));
        Console.WriteLine(find.ToQueryString());
        find.ExecuteUpdate(calls => calls.SetProperty(detail => detail.IsActive, false));
    }

    public void DeleteAppPosme(int calendarDetailId)
    {
        using var context = new DataContext();
        var find = context.TbEmployeeCalendarPayDetails
            .Single(detail => detail.CalendarDetailID == calendarDetailId);
        find.IsActive = false;
        context.SaveChanges();
    }

    public int InsertAppPosme(TbEmployeeCalendarPayDetail data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.CalendarDetailID;
    }

    public TbEmployeeCalendarPayDetailDto GetRowByPk(int calendarDetailId)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbEmployeeCalendarPayDetails
            join e in dbContext.TbEmployees on i.EmployeeID equals e.EntityID
            join n in dbContext.TbNaturales on i.EmployeeID equals n.EntityID
            where i.CalendarDetailID == calendarDetailId
                  && i.IsActive
            select new TbEmployeeCalendarPayDetailDto
            {
                CalendarDetailId = i.CalendarDetailID,
                CalendarId = i.CalendarID,
                EmployeeId = i.EmployeeID,
                Salary = i.PlusSalary,
                Commission = i.PlusCommission,
                Adelantos = i.MinusAdelantos,
                Neto = i.EqualNeto,
                IsActive = i.IsActive,
                FirstName = n.FirstName,
                LastName = n.LastName,
                EmployeNumber = e.EmployeNumber,
                HourCost = e.HourCost,
                ComissionPorcentage = e.ComissionPorcentage
            };
        return result.Single();
    }

    public List<TbEmployeeCalendarPayDetailDto> GetRowByCalendarId(int calendarId)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbEmployeeCalendarPayDetails
            join e in dbContext.TbEmployees on i.EmployeeID equals e.EntityID
            join n in dbContext.TbNaturales on i.EmployeeID equals n.EntityID
            where i.CalendarID == calendarId
                  && i.IsActive
            select new TbEmployeeCalendarPayDetailDto
            {
                CalendarDetailId = i.CalendarDetailID,
                CalendarId = i.CalendarID,
                EmployeeId = i.EmployeeID,
                Salary = i.PlusSalary,
                Commission = i.PlusCommission,
                Adelantos = i.MinusAdelantos,
                Neto = i.EqualNeto,
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