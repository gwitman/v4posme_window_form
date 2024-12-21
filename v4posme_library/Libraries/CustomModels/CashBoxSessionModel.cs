using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public class CashBoxSessionModel : ICashBoxSessionModel
{
    public List<TbCashBoxSession> GetRowByUserIdAndStatusId(int userId, int statusId)
    {
        using var context = new DataContext();
        return context.TbCashBoxSessions.Where(session => session.UserID == userId && session.StatusID == statusId).ToList();
    }

    public int InsertAppPosme(TbCashBoxSession data)
    {
        using var context = new DataContext();
        var add = context.Add(data).Entity;
        context.Add(add);
        context.SaveChanges();
        return add.CashBoxSessionID;
    }

    public void UpdateAppPosme(int cashBoxSessionID, TbCashBoxSession data, DataContext? dataContext = null)
    {
        if (dataContext == null)
        {
            using var context = new DataContext();
            UpdateAppPosme(cashBoxSessionID, data, context);
            return;
        }
        dataContext.Update(data);
        dataContext.SaveChanges();
    }
}