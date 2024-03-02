using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class EmployeeCalendarPayModel : IEmployeeCalendarPayModel
{
    public void UpdateAppPosme(int calendarId, TbEmployeeCalendarPay data)
    {
        using var context = new DataContext();
        var find = context.TbEmployeeCalendarPays.Find(calendarId);
        if (find is null) return;
        data.CalendarId = find.CalendarId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.BulkSaveChanges();
    }

    public void DeleteAppPosme(int calendarId)
    {
        using var context = new DataContext();
        var find = context.TbEmployeeCalendarPays.Find(calendarId);
        if (find is null) return;
        find.IsActive = 0;
        context.BulkSaveChanges();
    }

    public int InsertAppPosme(TbEmployeeCalendarPay data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.BulkSaveChanges();
        return add.Entity.CalendarId;
    }

    public TbEmployeeCalendarPay? GetRowByPk(int calendarId)
    {
        using var context = new DataContext();
        return context.TbEmployeeCalendarPays.Find(calendarId);
    }
}