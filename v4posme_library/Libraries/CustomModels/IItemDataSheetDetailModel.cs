using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public interface IItemDataSheetDetailModel
{
    void UpdateAppPosme(int itemDataSheetDetailId,TbItemDataSheetDetail data);

    void DeleteAppPosme(int itemDataSheetDetailId);
    
    void DeleteWhereDataSheet(int itemDataSheetId);
    
    void DeleteWhereIdNotIn(int itemDataSheetId,List<int> listDsdId);
    
    int InsertAppPosme(TbItemDataSheetDetail data);

    TbItemDataSheetDetailDto GetRowByPk(int itemDataSheetDetailId);
    
    TbItemDataSheetDetailDto GetRowByPkItemId(int itemDataSheetId, int itemId);
    
    List<TbItemDataSheetDetailDto> GetRowByItemDataSheet(int itemDataSheetId);
}