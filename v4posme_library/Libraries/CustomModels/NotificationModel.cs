using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class NotificationModel : INotificationModel
{
    public void UpdateAppPosmeBySumary(string? summary, TbNotification data)
    {
        using var context = new DataContext();
        var notifications = context.TbNotifications
            .Where(notification => notification.Summary == summary)
            .ToList();
        foreach (var notification in notifications)
        {
            data.NotificationID = notification.NotificationID;
            context.Entry(notification).CurrentValues.SetValues(data);
        }

        context.SaveChanges();
    }

    public void UpdateAppPosme(int notificationId, TbNotification data)
    {
        using var context = new DataContext();
        var find = context.TbNotifications.Find(notificationId);
        if (find is null) return;
        data.NotificationID = find.NotificationID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteAppPosme(int notificationId)
    {
        using var context = new DataContext();
        context.TbNotifications
            .Where(notification => notification.NotificationID == notificationId)
            .ExecuteUpdate(calls => calls.SetProperty(notification => notification.IsActive, false));
    }

    public int InsertAppPosme(TbNotification data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.NotificationID;
    }

    public TbNotification GetRowByPk(int notificationId)
    {
        using var context = new DataContext();
        return context.TbNotifications
            .Single(notification => notification.NotificationID == notificationId
                                    && notification.IsActive!.Value);
    }

    public List<TbNotification> GetRows(int top)
    {
        using var context = new DataContext();
        return context.TbNotifications
            .Where(notification => notification.SendOn == null
                                   && notification.IsActive!.Value)
            .Take(top)
            .ToList();
    }

    public List<TbNotification> GetRowsEmail(int top)
    {
        using var context = new DataContext();
        return context.TbNotifications
            .Where(notification => notification.SendEmailOn == null
                                   && notification.IsActive!.Value)
            .Take(top)
            .ToList();
    }

    public List<TbNotification> GetRowsWhatsappPrimerEmployeerOcupado(DateTime datetimeCliente, string? business)
    {
        using var context = new DataContext();
        FormattableString sql = $$"""
                                              select *
                                  from tb_notification n
                                      where n.isActive = 1
                                  and n.summary = '{{business}}'
                                  and (
                                      (
                                          ADDTIME(CAST(CONCAT(n.programDate, ' ', n.programHour, ':00') AS DATETIME), '+00:30:00') >=
                                          CAST('{{datetimeCliente}}' AS DATETIME) and
                                  CAST(n.programDate AS DATETIME) = CAST('{{datetimeCliente}}' AS DATE)
                                      )
                                  or
                                  (
                                      ADDTIME(CAST('{{datetimeCliente}}' AS DATETIME), '-00:30:00') <=
                                      CAST(CONCAT(n.programDate, ' ', n.programHour, ':00') AS DATETIME) and
                                  CAST(n.programDate AS DATETIME) = CAST('{{datetimeCliente}}' AS DATE)
                                      )
                                      )
                                  """;
        return context.TbNotifications.FromSql(sql).ToList();
    }

    public List<TbNotification> GetRowsToAddedGoogleCalendar(int tagId, string? business)
    {
        using var context = new DataContext();
        return context.TbNotifications
            .Where(notification => notification.IsActive!.Value
                                   && notification.AddedCalendarGoogle == false
                                   && notification.TagID == tagId
                                   && notification.Summary!.Contains(business))
            .ToList();
    }

    public List<TbNotification> GetRowsWhatsappPosMeSendMessage(int top)
    {
        using var context = new DataContext();
        var appHourDifference = VariablesGlobales.ConfigurationBuilder["APP_HOUR_DIFERENCE_MYSQL"];
        FormattableString query = $"""
                                   select n.*
                                   from tb_notification n
                                            inner join tb_tag t on n.TagID = t.TagID
                                   where n.isActive = 1
                                     and t.sendSMS = 1
                                     and n.sendWhatsappOn is null
                                     and CAST(CONCAT(n.programDate, ' ', '00:00', ':00') AS DATETIME) >=
                                         ADDTIME(ADDTIME(CURRENT_DATE(), '{appHourDifference}'), '-00:00:00')
                                   
                                     and CAST(CONCAT(n.programDate, ' ', '00:00', ':00') AS DATETIME) <=
                                         ADDTIME(ADDTIME(CURRENT_DATE(), '{appHourDifference}'), '+23:59:59')
                                   limit 0,{top}
                                   """;
        return context.TbNotifications.FromSql(query).ToList();
    }

    public List<TbNotification> GetRowsWhatsappPosMeCalendar(int top)
    {
        using var context = new DataContext();
        var appHourDifference = VariablesGlobales.ConfigurationBuilder["APP_HOUR_DIFERENCE_MYSQL"];
        FormattableString? sql = $"""
                                  select *
                                  from tb_notification n
                                  where n.isActive = 1
                                    and n.sendWhatsappOn is null
                                    and CAST(CONCAT(n.programDate, ' ', n.programHour, ':00') AS DATETIME) >
                                        ADDTIME(ADDTIME(now(), '{appHourDifference}'), '-00:30:00')
                                  
                                    and CAST(CONCAT(n.programDate, ' ', n.programHour, ':00') AS DATETIME) <=
                                        ADDTIME(ADDTIME(now(), '{appHourDifference}'), '+00:30:00')

                                  limit 0,{top}
                                  """;
        var result = context.TbNotifications.FromSql(sql);
        return result.Take(top).ToList();
    }

    public TbNotification GetRowsByToMessage(string? to, string? message)
    {
        using var context = new DataContext();
        return context.TbNotifications
            .Single(notification => notification.To!.Contains(to)
                                    && notification.Message!.Contains(message)
                                    && notification.IsActive!.Value);
    }
}