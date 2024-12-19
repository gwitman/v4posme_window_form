using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface ICashBoxUserModel
{
    List<TbCashBoxUser> GetRowByCompanyIdAndUserId(int companyId, int userId);
}