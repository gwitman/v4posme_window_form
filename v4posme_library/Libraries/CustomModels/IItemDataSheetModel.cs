using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IItemDataSheetModel
{
    void UpdateAppPosme(int itemDataSheetId,TbItemDataSheet data);
    
    void DeleteAppPosme(int itemDataSheetId);
    
    int InsertAppPosme(TbItemDataSheet data);
    
    TbItemDataSheet GetRowByPk(int itemDataSheetId);
    
    TbItemDataSheet GetRowByItemId(int itemId);
}