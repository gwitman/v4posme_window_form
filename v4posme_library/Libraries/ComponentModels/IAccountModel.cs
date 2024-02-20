using v4posme_library.Models;
namespace v4posme_library.Libraries.ComponentModels;

public interface IAccountModel
{
    TbAccount GetRowByPk(int companyId,int accountId);
}