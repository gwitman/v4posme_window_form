using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface INotificationModel
{
    void UpdateAppPosmeBySumary(string summary, TbNotification data);

    void UpdateAppPosme(int notificationId, TbNotification data);
    
    void DeleteAppPosme(int notificationId);
    
    int InsertAppPosme(TbNotification data);
    
    TbNotification GetRowByPk(int notificationId);
    
    List<TbNotification> GetRows(int top);
    
    List<TbNotification> GetRowsEmail(int top);
    
    List<TbNotification> GetRowsWhatsappPrimerEmployeerOcupado(DateTime datetimeCliente,string business);
    
    List<TbNotification> GetRowsToAddedGoogleCalendar(int tagId,string business);
    
    List<TbNotification> GetRowsWhatsappPosMeSendMessage(int top);
    
    List<TbNotification> GetRowsWhatsappPosMeCalendar(int top);
    
    TbNotification GetRowsByToMessage(string to,string message);
}