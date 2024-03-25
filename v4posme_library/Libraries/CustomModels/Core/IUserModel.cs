using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface IUserModel
{
    int InsertAppPosme(TbUser data);
    
    void UpdateAppPosme(int companyId,int branchId,int userId,TbUser data);
    
    List<TbUser> GetRowByComercio(string comercio);
    
    List<TbUser> GetRowByFoto(string foto);
    
    TbUser GetRowByExistNickname(string nickname);

    TbUser GetRowByNiknamePassword(string nickname, string password);
    
    TbUser GetRowByEmail(string email);
    
    TbUser GetRowByPk(int companyId,int branchId,int userId);
    
    List<TbUser> GetAll(int companyId);
    
    List<TbUser> GetUserByBussnes(int companyId,string bussines);
    
    int  GetCountUser(int companyId);
    
    int  GetCount(int companyId);
}