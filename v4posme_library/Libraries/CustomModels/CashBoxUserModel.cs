using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public class CashBoxUserModel : ICashBoxUserModel
{
    public List<TbCashBoxUser> GetRowByCompanyIdAndUserId(int companyId, int userId)
    {
        using var context = new DataContext();
        return context.TbCashBoxUsers.Where(user => user.CompanyID == companyId && user.UserID == userId).ToList();
    }
}