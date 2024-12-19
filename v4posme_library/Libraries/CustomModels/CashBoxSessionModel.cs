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
}