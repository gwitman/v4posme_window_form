using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public interface IUserTagModel
{
    int DeleteByUser(int userId);
    
    int DeleteAppPosme(int tagId,int userId);
    
    int InsertAppPosme(TbUserTag data);
    
    List<TbUserTagDto> GetRowByUser(int userId);
    
    List<TbUserTagDto> GetRowByPk(int tagId);
}