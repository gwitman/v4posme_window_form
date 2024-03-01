using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IEmployeeCalendarPayDetailModel
{
    void UpdateAppPosme(int calendarDetailId, TbEmployeeCalendarPayDetail data);
    
    void DeleteWhereIdNotIn(int calendarId,List<int> arrayId);

    void DeleteAppPosme(int calendarDetailId);
    
    int InsertAppPosme(TbEmployeeCalendarPayDetail data);

    TbEmployeeCalendarPayDetail GetRowByPk(int calendarDetailId);
    
    List<TbEmployeeCalendarPayDetail> GetRowByCalendarId(int calendarId);
}