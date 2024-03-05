using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class ItemDataSheetDetailModel : IItemDataSheetDetailModel
{
    public void UpdateAppPosme(int itemDataSheetDetailId, TbItemDataSheetDetail data)
    {
        using var context = new DataContext();
        var find = context.TbItemDataSheetDetails
            .Find(itemDataSheetDetailId);
        if (find is null) return;
        data.ItemDataSheetDetailId = find.ItemDataSheetDetailId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteAppPosme(int itemDataSheetDetailId)
    {
        using var context = new DataContext();
        context.TbItemDataSheetDetails
            .Where(detail => detail.ItemDataSheetDetailId == itemDataSheetDetailId)
            .ExecuteUpdate(calls =>
                calls.SetProperty(detail => detail.IsActive, 0));
    }

    public void DeleteWhereDataSheet(int itemDataSheetId)
    {
        using var context = new DataContext();
        context.TbItemDataSheetDetails
            .Where(detail => detail.ItemDataSheetId == itemDataSheetId)
            .ExecuteUpdate(calls =>
                calls.SetProperty(detail => detail.IsActive, 0));
    }

    public void DeleteWhereIdNotIn(int itemDataSheetId, List<int> listDsdId)
    {
        using var context = new DataContext();
        context.TbItemDataSheetDetails
            .Where(detail => detail.ItemDataSheetId == itemDataSheetId
                             && !listDsdId.Contains(detail.ItemDataSheetDetailId))
            .ExecuteUpdate(calls =>
                calls.SetProperty(detail => detail.IsActive, 0));
    }

    public int InsertAppPosme(TbItemDataSheetDetail data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.ItemDataSheetDetailId;
    }

    public TbItemDataSheetDetail GetRowByPk(int itemDataSheetDetailId)
    {
        using var context = new DataContext();
        var result = from i in context.TbItemDataSheetDetails
            join tm in context.TbItems on i.ItemId equals tm.ItemId
            where i.ItemDataSheetDetailId == itemDataSheetDetailId
                  && i.IsActive == 1
            select new TbItemDataSheetDetail
            {
                ItemDataSheetDetailId = i.ItemDataSheetDetailId,
                ItemDataSheetId = i.ItemDataSheetId,
                ItemId = i.ItemId,
                Quantity = i.Quantity,
                RelatedItemId = i.RelatedItemId,
                IsActive = i.IsActive,
                ItemNumber = tm.ItemNumber,
                Name = tm.Name
            };
        return result.Single();
    }

    public TbItemDataSheetDetail GetRowByPkItemId(int itemDataSheetId, int itemId)
    {
        using var context = new DataContext();
        var result = from i in context.TbItemDataSheetDetails
            join tm in context.TbItems on i.ItemId equals tm.ItemId
            where i.ItemDataSheetId == itemDataSheetId
                  && i.IsActive == 1
                  && tm.ItemId == itemId
            select new TbItemDataSheetDetail
            {
                ItemDataSheetDetailId = i.ItemDataSheetDetailId,
                ItemDataSheetId = i.ItemDataSheetId,
                ItemId = i.ItemId,
                Quantity = i.Quantity,
                RelatedItemId = i.RelatedItemId,
                IsActive = i.IsActive,
                ItemNumber = tm.ItemNumber,
                Name = tm.Name
            };
        return result.Single();
    }

    public List<TbItemDataSheetDetail> GetRowByItemDataSheet(int itemDataSheetId)
    {
        using var context = new DataContext();
        var result = from i in context.TbItemDataSheetDetails
            join tm in context.TbItems on i.ItemId equals tm.ItemId
            where i.ItemDataSheetId == itemDataSheetId
                  && i.IsActive == 1
            select new TbItemDataSheetDetail
            {
                ItemDataSheetDetailId = i.ItemDataSheetDetailId,
                ItemDataSheetId = i.ItemDataSheetId,
                ItemId = i.ItemId,
                Quantity = i.Quantity,
                RelatedItemId = i.RelatedItemId,
                IsActive = i.IsActive,
                ItemNumber = tm.ItemNumber,
                Name = tm.Name
            };
        return result.ToList();
    }
}