using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IUserTagModel
{
    int DeleteByUser(int userId);
    
    int DeleteAppPosme(int tagId,int userId);
    
    int InsertAppPosme(TbUserTag data);
    
    List<TbUserTag> GetRowByUser(int userId);
    
    List<TbUserTag> GetRowByPk(int tagId);
}