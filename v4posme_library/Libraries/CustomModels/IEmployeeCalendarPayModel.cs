using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IEmployeeCalendarPayModel
{
    void UpdateAppPosme(int calendarId, TbEmployeeCalendarPay data);
    
    void DeleteAppPosme(int calendarId);

    int InsertAppPosme(TbEmployeeCalendarPay data);
    
    TbEmployeeCalendarPay? GetRowByPk(int calendarId);
}