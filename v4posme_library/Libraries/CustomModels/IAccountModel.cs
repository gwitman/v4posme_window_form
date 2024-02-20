using v4posme_library.Models;
namespace v4posme_library.Libraries.CustomModels;

public interface IAccountModel
{
    TbAccount GetRowByPk(int companyId, int accountId);
    void DeleteAppPosme(int companyId, int accountId);
    void UpdateAppPosme(int companyId, int accountId, TbAccount data);
    int InsertAppPosme(TbAccount data);
    int GetIsParent(int companyId, int accountId);
    TbAccount GetByAccountNumber(string accountNumber, int companyId);
    List<TbAccount> GetByCompany(int companyId);
    List<TbAccount> GetByCompanyOperative(int companyId);
    int GetCountAccount(int companyId);
}
