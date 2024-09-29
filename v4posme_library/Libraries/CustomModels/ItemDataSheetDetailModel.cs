using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class ItemDataSheetDetailModel : IItemDataSheetDetailModel
{
    public void UpdateAppPosme(int itemDataSheetDetailId, TbItemDataSheetDetail data)
    {
        using var context = new DataContext();
        var find = context.TbItemDataSheetDetails
            .Find(itemDataSheetDetailId);
        if (find is null) return;
        data.ItemDataSheetDetailID = find.ItemDataSheetDetailID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteAppPosme(int itemDataSheetDetailId)
    {
        using var context = new DataContext();
        context.TbItemDataSheetDetails
            .Where(detail => detail.ItemDataSheetDetailID == itemDataSheetDetailId)
            .ExecuteUpdate(calls =>
                calls.SetProperty(detail => detail.IsActive, 0));
    }

    public void DeleteWhereDataSheet(int itemDataSheetId)
    {
        using var context = new DataContext();
        context.TbItemDataSheetDetails
            .Where(detail => detail.ItemDataSheetID == itemDataSheetId)
            .ExecuteUpdate(calls =>
                calls.SetProperty(detail => detail.IsActive, 0));
    }

    public void DeleteWhereIdNotIn(int itemDataSheetId, List<int> listDsdId)
    {
        using var context = new DataContext();
        context.TbItemDataSheetDetails
            .Where(detail => detail.ItemDataSheetID == itemDataSheetId
                             && !listDsdId.Contains(detail.ItemDataSheetDetailID))
            .ExecuteUpdate(calls =>
                calls.SetProperty(detail => detail.IsActive, 0));
    }

    public int InsertAppPosme(TbItemDataSheetDetail data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.ItemDataSheetDetailID;
    }

    public TbItemDataSheetDetailDto GetRowByPk(int itemDataSheetDetailId)
    {
        using var context = new DataContext();
        var result = from i in context.TbItemDataSheetDetails
            join tm in context.TbItems on i.ItemID equals tm.ItemID
            where i.ItemDataSheetDetailID == itemDataSheetDetailId
                  && i.IsActive == 1
            select new TbItemDataSheetDetailDto
            {
                ItemDataSheetDetailId = i.ItemDataSheetDetailID,
                ItemDataSheetId = i.ItemDataSheetID,
                ItemId = i.ItemID,
                Quantity = i.Quantity,
                RelatedItemId = i.RelatedItemID,
                IsActive = i.IsActive,
                ItemNumber = tm.ItemNumber,
                Name = tm.Name
            };
        return result.Single();
    }

    public TbItemDataSheetDetailDto GetRowByPkItemId(int itemDataSheetId, int itemId)
    {
        using var context = new DataContext();
        var result = from i in context.TbItemDataSheetDetails
            join tm in context.TbItems on i.ItemID equals tm.ItemID
            where i.ItemDataSheetID == itemDataSheetId
                  && i.IsActive == 1
                  && tm.ItemID == itemId
            select new TbItemDataSheetDetailDto
            {
                ItemDataSheetDetailId = i.ItemDataSheetDetailID,
                ItemDataSheetId = i.ItemDataSheetID,
                ItemId = i.ItemID,
                Quantity = i.Quantity,
                RelatedItemId = i.RelatedItemID,
                IsActive = i.IsActive,
                ItemNumber = tm.ItemNumber,
                Name = tm.Name
            };
        return result.Single();
    }

    public List<TbItemDataSheetDetailDto> GetRowByItemDataSheet(int itemDataSheetId)
    {
        using var context = new DataContext();
        var result = from i in context.TbItemDataSheetDetails
            join tm in context.TbItems on i.ItemID equals tm.ItemID
            where i.ItemDataSheetID == itemDataSheetId
                  && i.IsActive == 1
            select new TbItemDataSheetDetailDto
            {
                ItemDataSheetDetailId = i.ItemDataSheetDetailID,
                ItemDataSheetId = i.ItemDataSheetID,
                ItemId = i.ItemID,
                Quantity = i.Quantity,
                RelatedItemId = i.RelatedItemID,
                IsActive = i.IsActive,
                ItemNumber = tm.ItemNumber,
                Name = tm.Name
            };
        return result.ToList();
    }
}