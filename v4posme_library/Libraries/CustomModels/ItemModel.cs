using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class ItemModel : IItemModel
{
    public void UpdateAppPosme(int companyId, int itemId, TbItem data)
    {
        using var context = new DataContext();
        var find = context.TbItems
            .Single(item => item.CompanyId == companyId
                            && item.ItemId == itemId);
        data.ItemId = find.ItemId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteAppPosme(int companyId, int itemId)
    {
        using var context = new DataContext();
        context.TbItems
            .Where(item => item.CompanyId == companyId
                           && item.ItemId == itemId)
            .ExecuteUpdate(calls =>
                calls.SetProperty(item => item.IsActive, false));
    }

    public int InsertAppPosme(TbItem data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.ItemId;
    }

    public TbItem GetRowByCode(int companyId, string itemNumber)
    {
        using var context = new DataContext();
        return context.TbItems
            .Single(item => item.CompanyId == companyId
                            && item.ItemNumber == itemNumber
                            && item.IsActive!.Value);
    }

    public TbItem GetRowByCodeBarra(int companyId, string itemNumber)
    {
        using var context = new DataContext();
        return context.TbItems
            .Single(item => item.CompanyId == companyId
                            && item.BarCode == itemNumber
                            && item.IsActive!.Value);
    }

    public List<TbItem> GetRowByCodeBarraSimilar(int companyId, string itemNumber)
    {
        using var context = new DataContext();
        return context.TbItems
            .Where(item => item.CompanyId == companyId
                           && EF.Functions.Like(item.ItemNumber, "%" + itemNumber + "%")
                           && item.IsActive!.Value)
            .ToList();
    }

    public TbItem GetRowByPk(int companyId, int itemId)
    {
        using var context = new DataContext();
        return context.TbItems
            .Single(item => item.CompanyId == companyId
                            && item.ItemId == itemId
                            && item.IsActive!.Value);
    }

    public TbItem GetRwByPkAndInactive(int companyId, int itemId)
    {
        using var context = new DataContext();
        return context.TbItems
            .Single(item => item.CompanyId == companyId
                            && item.ItemId == itemId);
    }

    public List<TbItem> GetRowsByPk(int companyId, List<int> listItem)
    {
        using var context = new DataContext();
        return context.TbItems
            .Where(item => item.CompanyId == companyId
                           && listItem.Contains(item.ItemId)
                           && item.IsActive!.Value)
            .ToList();
    }

    public List<TbItem> GetRowByCompany(int companyId)
    {
        using var context = new DataContext();
        return context.TbItems
            .Where(item => item.CompanyId == companyId
                           && item.IsActive!.Value)
            .ToList();
    }

    public int GetCount(int companyId)
    {
        using var context = new DataContext();
        return context.TbItems
            .Count(item => item.CompanyId == companyId
                           && item.IsActive!.Value);
    }

    public List<TbItem> GetRowByTransactionMasterId(int transactionMasterId)
    {
        using var dbContext = new DataContext();
        var result = from tm in dbContext.TbTransactionMasters
            join td in dbContext.TbTransactionMasterDetails on tm.TransactionMasterId equals td.TransactionMasterId
            join i in dbContext.TbItems on td.ComponentItemId equals i.ItemId
            join catalogItem in dbContext.TbCatalogItems on Convert.ToInt32(i.UnitMeasureId) equals catalogItem
                .CatalogItemId
            where i.IsActive!.Value && tm.TransactionMasterId == transactionMasterId
            select new TbItem
            {
                CompanyId = i.CompanyId,
                BranchId = i.BranchId,
                InventoryCategoryId = i.InventoryCategoryId,
                ItemId = i.ItemId,
                FamilyId = i.FamilyId,
                ItemNumber = i.ItemNumber,
                BarCode = i.BarCode,
                Name = i.Name.Replace("\"", ""),
                Description = i.Description.Replace("\"", ""),
                UnitMeasureId = i.UnitMeasureId,
                DisplayId = i.DisplayId,
                Capacity = i.Capacity,
                DisplayUnitMeasureId = i.DisplayUnitMeasureId,
                DefaultWarehouseId = i.DefaultWarehouseId,
                Quantity = i.Quantity,
                QuantityMax = i.QuantityMax,
                QuantityMin = i.QuantityMin,
                Cost = i.Cost,
                Reference1 = i.Reference1,
                Reference2 = i.Reference2,
                StatusId = i.StatusId,
                IsPerishable = i.IsPerishable,
                FactorBox = i.FactorBox,
                FactorProgram = i.FactorProgram,
                CreatedIn = i.CreatedIn,
                CreatedAt = i.CreatedAt,
                CreatedBy = i.CreatedBy,
                CreatedOn = i.CreatedOn,
                IsActive = i.IsActive,
                IsInvoiceQuantityZero = i.IsInvoiceQuantityZero,
                IsServices = i.IsServices,
                CurrencyId = i.CurrencyId,
                IsInvoice = i.IsInvoice,
                Reference3 = i.Reference3,
                UnitMeasureName = catalogItem.Name,
                ItemNameLog = td.ItemNameLog.Replace("\"", "")
            };
        return result.ToList();
    }
}