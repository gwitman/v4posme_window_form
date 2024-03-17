using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class NotificationModel : INotificationModel
{
    public void UpdateAppPosmeBySumary(string summary, TbNotification data)
    {
        using var context = new DataContext();
        var find = context.TbNotifications.FirstOrDefault(notification => notification.Summary == summary);
        if (find is null) return;
        data.NotificationId = find.NotificationId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void UpdateAppPosme(int notificationId, TbNotification data)
    {
        using var context = new DataContext();
        var find = context.TbNotifications.Find(notificationId);
        if (find is null) return;
        data.NotificationId = find.NotificationId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteAppPosme(int notificationId)
    {
        using var context = new DataContext();
        context.TbNotifications
            .Where(notification => notification.NotificationId == notificationId)
            .ExecuteUpdate(calls => calls.SetProperty(notification => notification.IsActive, (ulong?)0));
    }

    public int InsertAppPosme(TbNotification data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.NotificationId;
    }

    public TbNotification GetRowByPk(int notificationId)
    {
        using var context = new DataContext();
        return context.TbNotifications
            .Single(notification => notification.NotificationId == notificationId
                                    && notification.IsActive == 1);
    }

    public List<TbNotification> GetRows(int top)
    {
        using var context = new DataContext();
        return context.TbNotifications
            .Where(notification => notification.SendOn == null
                                   && notification.IsActive == 1)
            .Take(top)
            .ToList();
    }

    public List<TbNotification> GetRowsEmail(int top)
    {
        using var context = new DataContext();
        return context.TbNotifications
            .Where(notification => notification.SendEmailOn == null
                                   && notification.IsActive == 1)
            .Take(top)
            .ToList();
    }

    public List<TbNotification> GetRowsWhatsappPrimerEmployeerOcupado(DateTime datetimeCliente, string business)
    {
        using var context = new DataContext();
        var result = from n in context.TbNotifications
            where n.IsActive == 1 &&
                  n.Summary == business &&
                  (
                      (
                          DateTime.Parse(n.ProgramDate + " " + n.ProgramHour + ":00") + TimeSpan.FromMinutes(30) >=
                          datetimeCliente &&
                          DateTime.Parse(n.ProgramDate) == datetimeCliente.Date
                      ) ||
                      (
                          datetimeCliente - TimeSpan.FromMinutes(30) <=
                          DateTime.Parse(n.ProgramDate + " " + n.ProgramHour + ":00") &&
                          DateTime.Parse(n.ProgramDate) == datetimeCliente.Date
                      )
                  )
            select n;
        return result.ToList();
    }

    public List<TbNotification> GetRowsToAddedGoogleCalendar(int tagId, string business)
    {
        using var context = new DataContext();
        return context.TbNotifications
            .Where(notification => notification.IsActive == 1
                                   && notification.AddedCalendarGoogle == 0
                                   && notification.TagId == tagId
                                   && notification.Summary!.Contains(business))
            .ToList();
    }

    public List<TbNotification> GetRowsWhatsappPosMeSendMessage(int top)
    {
        using var context = new DataContext();
        var appHourDifference = VariablesGlobales.ConfigurationBuilder["APP_HOUR_DIFERENCE_MYSQL"];
        var timeSpan = TimeSpan.Parse(appHourDifference!);
        var timeSpanMinus = TimeSpan.Parse("-00:00:00");
        var timeSpanPlus = TimeSpan.Parse("+23:59:59");
        var result = from n in context.TbNotifications
            join t in context.TbTags on n.TagId equals t.TagId
            where n.IsActive == 1 &&
                  t.SendSms == 1 &&
                  n.SendWhatsappOn == null &&
                  DateTime.Parse(n.ProgramDate + " " + "00:00:00") >= DateTime.Now.Add(timeSpan).Add(timeSpanMinus) &&
                  DateTime.Parse(n.ProgramDate + " " + "00:00:00") <= DateTime.Now.Add(timeSpan).Add(timeSpanPlus)
            select n;
        return result.Take(top).ToList();
    }

    public List<TbNotification> GetRowsWhatsappPosMeCalendar(int top)
    {
        using var context = new DataContext();
        var appHourDifference = VariablesGlobales.ConfigurationBuilder["APP_HOUR_DIFERENCE_MYSQL"];
        var timeSpan = TimeSpan.Parse(appHourDifference!);
        var timeSpanMinus = TimeSpan.Parse("-00:30:00");
        var timeSpanPlus = TimeSpan.Parse("+00:30:00");
        var result = from n in context.TbNotifications
            where n.IsActive == 1 &&
                  n.SendWhatsappOn == null &&
                  DateTime.Parse(n.ProgramDate + " " + n.ProgramHour + ":00") >=
                  DateTime.Now.Add(timeSpan).Add(timeSpanMinus) &&
                  DateTime.Parse(n.ProgramDate + " " + n.ProgramHour + ":00") <=
                  DateTime.Now.Add(timeSpan).Add(timeSpanPlus)
            select n;
        return result.Take(top).ToList();
    }

    public TbNotification GetRowsByToMessage(string to, string message)
    {
        using var context = new DataContext();
        return context.TbNotifications
            .Single(notification => notification.To!.Contains(to)
                                    && notification.Message!.Contains(message)
                                    && notification.IsActive==1);
    }
}