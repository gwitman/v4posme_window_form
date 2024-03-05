using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IErrorModel
{
    void UpdateAppPosme(int errorId,TbError data);
    
    void UpdateTagId(int tagId,int companyId,TbError data);
    
    int DeleteAppPosme(int errorId);
    
    int DeleteByTagId(int tagId,int companyId);
    
    int InsertAppPosme(TbError data);
    
    TbError GetRowByPk(int errorId);
    
    List<TbError> GetRowByUser(int userId);
    
    int GetRowByUserCount(int userId);
    
    List<TbError> GetRowByUserAllAndTagId(int userId,int tagId);
    
    List<TbError> GetRowByUserAll(int userId);
    
    TbError GetRowByMessageUser(int userId,string message);
}