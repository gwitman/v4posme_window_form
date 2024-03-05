using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IItemDataSheetDetailModel
{
    void UpdateAppPosme(int itemDataSheetDetailId,TbItemDataSheetDetail data);

    void DeleteAppPosme(int itemDataSheetDetailId);
    
    void DeleteWhereDataSheet(int itemDataSheetId);
    
    void DeleteWhereIdNotIn(int itemDataSheetId,List<int> listDsdId);
    
    int InsertAppPosme(TbItemDataSheetDetail data);

    TbItemDataSheetDetail GetRowByPk(int itemDataSheetDetailId);
    
    TbItemDataSheetDetail GetRowByPkItemId(int itemDataSheetId,int itemId);
    
    List<TbItemDataSheetDetail> GetRowByItemDataSheet(int itemDataSheetId);
}