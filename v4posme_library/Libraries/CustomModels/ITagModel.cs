using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface ITagModel
{
    void UpdateAppPosme(int tagId,TbTag data);
    
    void DeleteAppPosme(int tagId);
    
    int InsertAppPosme(TbTag data);

    List<TbTag> GetRows();

    TbTag GetRowByPk(int tagId);
    
    TbTag GetRowByName(string name);
}