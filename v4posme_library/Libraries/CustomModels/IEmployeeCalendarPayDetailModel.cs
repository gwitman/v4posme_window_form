using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public interface IEmployeeCalendarPayDetailModel
{
    void UpdateAppPosme(int calendarDetailId, TbEmployeeCalendarPayDetail data);
    
    void DeleteWhereIdNotIn(int calendarId,List<int> arrayId);

    void DeleteAppPosme(int calendarDetailId);
    
    int InsertAppPosme(TbEmployeeCalendarPayDetail data);

    TbEmployeeCalendarPayDetailDto GetRowByPk(int calendarDetailId);
    
    List<TbEmployeeCalendarPayDetailDto> GetRowByCalendarId(int calendarId);
}