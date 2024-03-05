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
        data.ItemDataSheetId = find.ItemDataSheetId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteAppPosme(int itemDataSheetId)
    {
        using var context = new DataContext();
        context.TbItemDataSheets
            .Where(sheet => sheet.ItemDataSheetId == itemDataSheetId)
            .ExecuteUpdate(calls => calls
                .SetProperty(sheet => sheet.IsActive, (ulong)0));
    }

    public int InsertAppPosme(TbItemDataSheet data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.ItemDataSheetId;
    }

    public TbItemDataSheet GetRowByPk(int itemDataSheetId)
    {
        using var context = new DataContext();
        return context.TbItemDataSheets
            .Single(sheet => sheet.ItemDataSheetId == itemDataSheetId
                             && sheet.IsActive == 1);
    }

    public TbItemDataSheet GetRowByItemId(int itemId)
    {
        using var context = new DataContext();
        return context.TbItemDataSheets
            .OrderByDescending(sheet => sheet.Version)
            .Single(sheet => sheet.ItemId == itemId
                             && sheet.IsActive == 1);
    }
}