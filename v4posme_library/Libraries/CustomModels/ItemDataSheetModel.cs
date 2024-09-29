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
        if (find is null) return;
        data.ItemDataSheetID = find.ItemDataSheetID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteAppPosme(int itemDataSheetId)
    {
        using var context = new DataContext();
        context.TbItemDataSheets
            .Where(sheet => sheet.ItemDataSheetID == itemDataSheetId)
            .ExecuteUpdate(calls => calls
                .SetProperty(sheet => sheet.IsActive, false));
    }

    public int InsertAppPosme(TbItemDataSheet data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.ItemDataSheetID;
    }

    public TbItemDataSheet GetRowByPk(int itemDataSheetId)
    {
        using var context = new DataContext();
        return context.TbItemDataSheets
            .Single(sheet => sheet.ItemDataSheetID == itemDataSheetId
                             && sheet.IsActive);
    }

    public TbItemDataSheet GetRowByItemId(int itemId)
    {
        using var context = new DataContext();
        return context.TbItemDataSheets
            .OrderByDescending(sheet => sheet.Version)
            .Single(sheet => sheet.ItemID == itemId
                             && sheet.IsActive);
    }
}