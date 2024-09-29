using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class ItemModel : IItemModel
{
    public void UpdateAppPosme(int companyId, int itemId, TbItem data)
    {
        using var context = new DataContext();
        var find = context.TbItems
            .Single(item => item.CompanyID == companyId
                            && item.ItemID == itemId);
        data.ItemID = find.ItemID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteAppPosme(int companyId, int itemId)
    {
        using var context = new DataContext();
        context.TbItems
            .Where(item => item.CompanyID == companyId
                           && item.ItemID == itemId)
            .ExecuteUpdate(calls =>
                calls.SetProperty(item => item.IsActive, false));
    }

    public int InsertAppPosme(TbItem data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.ItemID;
    }

    public TbItem? GetRowByCode(int companyId, string? itemNumber)
    {
        using var context = new DataContext();
        return context.TbItems
            .SingleOrDefault(item => item.CompanyID == companyId
                            && item.ItemNumber == itemNumber
                            && item.IsActive!.Value);
    }

    public TbItem? GetRowByCodeBarra(int companyId, string? itemNumber)
    {
        using var context = new DataContext();
        return context.TbItems
            .SingleOrDefault(item => item.CompanyID == companyId
                            && item.BarCode == itemNumber
                            && item.IsActive!.Value);
    }

    public List<TbItem> GetRowByCodeBarraSimilar(int companyId, string? itemNumber)
    {
        using var context = new DataContext();
        return context.TbItems
            .Where(item => item.CompanyID == companyId
                           && EF.Functions.Like(item.ItemNumber, "%" + itemNumber + "%")
                           && item.IsActive!.Value)
            .ToList();
    }

    public TbItem? GetRowByPk(int companyId, int itemId)
    {
        using var context = new DataContext();
        return context.TbItems.AsNoTracking()
            .SingleOrDefault(item => item.CompanyID == companyId
                            && item.ItemID == itemId
                            && item.IsActive!.Value);
    }

    public TbItem GetRwByPkAndInactive(int companyId, int itemId)
    {
        using var context = new DataContext();
        return context.TbItems
            .Single(item => item.CompanyID == companyId
                            && item.ItemID == itemId);
    }

    public List<TbItem> GetRowsByPk(int companyId, List<int> listItem)
    {
        using var context = new DataContext();
        return context.TbItems
            .Where(item => item.CompanyID == companyId
                           && listItem.Contains(item.ItemID)
                           && item.IsActive!.Value)
            .ToList();
    }

    public List<TbItem> GetRowByCompany(int companyId)
    {
        using var context = new DataContext();
        return context.TbItems
            .Where(item => item.CompanyID == companyId
                           && item.IsActive!.Value)
            .ToList();
    }

    public int GetCount(int companyId)
    {
        using var context = new DataContext();
        return context.TbItems
            .Count(item => item.CompanyID == companyId
                           && item.IsActive!.Value);
    }

    public List<TbItemDto> GetRowByTransactionMasterId(int transactionMasterId)
    {
        using var dbContext = new DataContext();
        var result = from tm in dbContext.TbTransactionMasters
            join td in dbContext.TbTransactionMasterDetails on tm.TransactionMasterID equals td.TransactionMasterID
            join i in dbContext.TbItems on td.ComponentItemID equals i.ItemID
            join catalogItem in dbContext.TbCatalogItems on Convert.ToInt32(i.UnitMeasureID) equals catalogItem
                .CatalogItemID
            where i.IsActive!.Value && tm.TransactionMasterID == transactionMasterId
            select new TbItemDto
            {
                CompanyId = i.CompanyID,
                BranchId = i.BranchID,
                InventoryCategoryId = i.InventoryCategoryID,
                ItemId = i.ItemID,
                FamilyId = i.FamilyID,
                ItemNumber = i.ItemNumber,
                BarCode = i.BarCode,
                Name = i.Name.Replace("\"", ""),
                Description = i.Description.Replace("\"", ""),
                UnitMeasureId = i.UnitMeasureID,
                DisplayId = i.DisplayID,
                Capacity = i.Capacity,
                DisplayUnitMeasureId = i.DisplayUnitMeasureID,
                DefaultWarehouseId = i.DefaultWarehouseID,
                Quantity = i.Quantity,
                QuantityMax = i.QuantityMax,
                QuantityMin = i.QuantityMin,
                Cost = i.Cost,
                Reference1 = i.Reference1,
                Reference2 = i.Reference2,
                StatusId = i.StatusID,
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
                CurrencyId = i.CurrencyID,
                IsInvoice = i.IsInvoice,
                Reference3 = i.Reference3,
                UnitMeasureName = catalogItem.Name,
                ItemNameLog = td.ItemNameLog.Replace("\"", "")
            };
        return result.ToList();
    }
}