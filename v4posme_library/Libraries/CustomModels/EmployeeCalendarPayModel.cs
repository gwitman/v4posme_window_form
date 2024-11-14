using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class EmployeeCalendarPayModel : IEmployeeCalendarPayModel
{
    public void UpdateAppPosme(int calendarId, TbEmployeeCalendarPay data)
    {
        using var context = new DataContext();
        var find = context.TbEmployeeCalendarPays.Find(calendarId);
        if (find is null) return;
        data.CalendarID = find.CalendarID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteAppPosme(int calendarId)
    {
        using var context = new DataContext();
        var find = context.TbEmployeeCalendarPays.Find(calendarId);
        if (find is null) return;
        find.IsActive = false;
        context.SaveChanges();
    }

    public int InsertAppPosme(TbEmployeeCalendarPay data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.CalendarID;
    }

    public TbEmployeeCalendarPay? GetRowByPk(int calendarId)
    {
        using var context = new DataContext();
        return context.TbEmployeeCalendarPays.Find(calendarId);
    }
}