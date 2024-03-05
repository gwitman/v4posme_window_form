using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class ItemDataSheetModel : IItemDataSheetModel
{
    public void UpdateAppPosme(int itemDataSheetId, TbItemDataSheet data)
    {
        using var context = new DataContext();
        var find = context.TbItemDataSheets
            .Find(itemDataSheetId);
        if(find is null) return;
        data.ItemDataSheetId = find.ItemDataSheetId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteAppPosme(int itemDataSheetId)
    {
        using var context = new DataContext();
        context.TbItemDataSheets
            .Where(sheet => sheet.ItemDataSheetId == itemDataSheetId)
            .ExecuteUpdate(calls => calls.SetProperty(sheet => sheet.IsActive, (ulong)0));
    }

    public int InsertAppPosme(TbItemDataSheet data)
    {
        throw new NotImplementedException();
    }

    public TbItemDataSheet GetRowByPk(int itemDataSheetId)
    {
        throw new NotImplementedException();
    }

    public TbItemDataSheet GetRowByItemId(int itemId)
    {
        throw new NotImplementedException();
    }
}