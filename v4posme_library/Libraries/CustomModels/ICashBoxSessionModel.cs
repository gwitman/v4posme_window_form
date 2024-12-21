using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface ICashBoxSessionModel
{
    List<TbCashBoxSession> GetRowByUserIdAndStatusId(int userId, int statusId);

    int InsertAppPosme(TbCashBoxSession data);

    void UpdateAppPosme(int cashBoxSessionID, TbCashBoxSession data, DataContext? dataContext = null);
}