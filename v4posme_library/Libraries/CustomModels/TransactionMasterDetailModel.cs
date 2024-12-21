using System.Diagnostics;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MySqlX.XDevAPI.Common;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class TransactionMasterDetailModel : ITransactionMasterDetailModel
{
    private readonly MapperConfiguration _mapperConfiguration = new(expression =>
        expression.CreateMap<TbTransactionMasterDetail, TbTransactionMasterDetailDto>());


    public int InsertAppPosme(TbTransactionMasterDetail data, DataContext? dataContext = null)
    {
        if (dataContext == null)
        {
            using var context = new DataContext();
            return InsertAppPosme(data, context);
        }

        var add = dataContext.Add(data);
        dataContext.SaveChanges();
        return add.Entity.TransactionMasterDetailID;
    }

    public void UpdateAppPosme(int companyId, int transactionId, int transactionMasterId, int transactionMasterDetailId, TbTransactionMasterDetail data, DataContext? dataContext = null)
    {
        if (dataContext == null)
        {
            using var context = new DataContext();
            UpdateAppPosme(companyId, transactionId, transactionMasterId, transactionMasterDetailId, data, context);
            return;
        }
        var find = dataContext.TbTransactionMasterDetails
            .FirstOrDefault(detail => detail.CompanyID == companyId
                                      && detail.TransactionID == transactionId
                                      && detail.TransactionMasterID == transactionMasterId
                                      && detail.TransactionMasterDetailID == transactionMasterDetailId);
        if (find is null) return;
        data.TransactionMasterDetailID = find.TransactionMasterDetailID;
        dataContext.Entry(find).CurrentValues.SetValues(data);
        dataContext.SaveChanges();
    }

    public TbTransactionMasterDetailDto GetRowByPk(int companyId, int transactionId, int transactionMasterId,
        int transactionMasterDetailId, int componentId = 33)
    {
        using var context = new DataContext();
        var mapper = new Mapper(_mapperConfiguration);
        var result = componentId switch
        {
            33 => from td in context.TbTransactionMasterDetails
                join item in context.TbItems on new { td.CompanyID, ComponentItemId = (int)td.ComponentItemID } equals
                    new { item.CompanyID, ComponentItemId = item.ItemID }
                join ci in context.TbCatalogItems on Convert.ToInt32(item.UnitMeasureID) equals ci.CatalogItemID
                where td.CompanyID == companyId
                      && td.TransactionID == transactionId
                      && td.TransactionMasterID == transactionMasterId
                      && td.TransactionMasterDetailID == transactionMasterDetailId
                      && td.IsActive!.Value
                select new TbTransactionMasterDetailDto
                {
                    CompanyId = td.CompanyID,
                    TransactionId = td.TransactionID,
                    TransactionMasterId = td.TransactionMasterID,
                    TransactionMasterDetailId = td.TransactionMasterDetailID,
                    ComponentId = td.ComponentID,
                    ComponentItemId = td.ComponentItemID,
                    PromotionId = td.PromotionID,
                    Amount = td.Amount,
                    Cost = td.Cost,
                    Quantity = td.Quantity,
                    Discount = td.Discount,
                    UnitaryAmount = td.UnitaryAmount,
                    UnitaryCost = td.UnitaryCost,
                    UnitaryPrice = td.UnitaryPrice,
                    Reference1 = td.Reference1,
                    Reference2 = td.Reference2,
                    Reference3 = td.Reference3,
                    Reference4 = td.Reference4,
                    Reference5 = td.Reference5,
                    Reference6 = td.Reference6,
                    Reference7 = td.Reference7,
                    CatalogStatusId = td.CatalogStatusID,
                    InventoryStatusId = td.InventoryStatusID,
                    IsActive = td.IsActive,
                    QuantityStock = td.QuantityStock,
                    QuantiryStockInTraffic = td.QuantiryStockInTraffic,
                    QuantityStockUnaswared = td.QuantityStockUnaswared,
                    RemaingStock = td.RemaingStock,
                    ExpirationDate = td.ExpirationDate,
                    InventoryWarehouseSourceId = td.InventoryWarehouseSourceID,
                    InventoryWarehouseTargetId = td.InventoryWarehouseTargetID,
                    ItemNumber = item.ItemNumber,
                    ItemName = item.Name,
                    UnitMeasureName = ci.Name,
                    DescriptionReference = td.DescriptionReference,
                    ExchangeRateReference = td.ExchangeRateReference,
                    Lote = td.Lote,
                    TypePriceId = td.TypePriceID,
                    SkuCatalogItemId = td.SkuCatalogItemID,
                    SkuQuantity = td.SkuQuantity,
                    SkuQuantityBySku = td.SkuQuantityBySku,
                    SkuFormatoDescription = td.SkuFormatoDescription,
                    ItemNameLog = td.ItemNameLog,
                    AmountCommision = td.AmountCommision
                },
            64 => from td in context.TbTransactionMasterDetails
                join i in context.TbCustomerCreditDocuments on new
                    { td.CompanyID, ComponentItemId = (int)td.ComponentItemID } equals new
                    { i.CompanyID, ComponentItemId = i.CustomerCreditDocumentID }
                where td.CompanyID == companyId
                      && td.TransactionID == transactionId
                      && td.TransactionMasterID == transactionMasterId
                      && td.TransactionMasterDetailID == transactionMasterDetailId
                      && td.IsActive.Value
                select mapper.Map<TbTransactionMasterDetailDto>(td),
            _ => from td in context.TbTransactionMasterDetails
                where td.CompanyID == companyId
                      && td.TransactionID == transactionId
                      && td.TransactionMasterID == transactionMasterId
                      && td.TransactionMasterDetailID == transactionMasterDetailId
                      && td.IsActive.Value
                select mapper.Map<TbTransactionMasterDetailDto>(td)
        };
        return result.First();
    }

    public TbTransactionMasterDetail? GetRowByPKK(int transactionMasterDetailId)
    {
        using var context = new DataContext();
        var result = context.TbTransactionMasterDetails.SingleOrDefault(c => c.TransactionMasterDetailID == transactionMasterDetailId);
        return result;
    }


    public TbTransactionMasterDetailDto GetRowByTransactionAndItems(int companyId, int transactionId,
        int transactionMasterId,
        List<int> listTmdId)
    {
        using var context = new DataContext();
        var result = from td in context.TbTransactionMasterDetails
            join i in context.TbItems on new { td.CompanyID, ComponentItemId = td.ComponentItemID } equals new
                { i.CompanyID, ComponentItemId = (int?)i.ItemID }
            join ci in context.TbCatalogItems on Convert.ToInt32(i.UnitMeasureID) equals ci.CatalogItemID
            where td.CompanyID == companyId
                  && td.TransactionID == transactionId
                  && td.TransactionMasterID == transactionMasterId
                  && td.IsActive.Value
                  && listTmdId.Contains(i.ItemID)
            select new TbTransactionMasterDetailDto
            {
                CompanyId = td.CompanyID,
                TransactionId = td.TransactionID,
                TransactionMasterId = td.TransactionMasterID,
                TransactionMasterDetailId = td.TransactionMasterDetailID,
                ComponentId = td.ComponentID,
                ComponentItemId = td.ComponentItemID,
                PromotionId = td.PromotionID,
                Amount = td.Amount,
                Cost = td.Cost,
                Quantity = td.Quantity,
                Discount = td.Discount,
                UnitaryAmount = td.UnitaryAmount,
                UnitaryCost = td.UnitaryCost,
                UnitaryPrice = td.UnitaryPrice,
                Reference1 = td.Reference1,
                Reference2 = td.Reference2,
                Reference3 = td.Reference3,
                Reference4 = td.Reference4,
                Reference5 = td.Reference5,
                Reference6 = td.Reference6,
                Reference7 = td.Reference7,
                CatalogStatusId = td.CatalogStatusID,
                InventoryStatusId = td.InventoryStatusID,
                IsActive = td.IsActive,
                QuantityStock = td.QuantityStock,
                QuantiryStockInTraffic = td.QuantiryStockInTraffic,
                QuantityStockUnaswared = td.QuantityStockUnaswared,
                RemaingStock = td.RemaingStock,
                ExpirationDate = td.ExpirationDate,
                InventoryWarehouseSourceId = td.InventoryWarehouseSourceID,
                InventoryWarehouseTargetId = td.InventoryWarehouseTargetID,
                ItemNumber = i.ItemNumber,
                BarCode = i.BarCode,
                ItemName = i.Name,
                UnitMeasureName = ci.Name,
                DescriptionReference = td.DescriptionReference,
                ExchangeRateReference = td.ExchangeRateReference,
                Lote = td.Lote,
                TypePriceId = td.TypePriceID,
                SkuCatalogItemId = td.SkuCatalogItemID,
                SkuQuantity = td.SkuQuantity,
                SkuQuantityBySku = td.SkuQuantityBySku,
                SkuFormatoDescription = td.SkuFormatoDescription,
                ItemNameLog = td.ItemNameLog,
                AmountCommision = td.AmountCommision
            };
        return result.First();
    }

    public List<TbTransactionMasterDetailDto> GetRowByTransactionAndWarehouse(int companyId, int transactionId,
        int transactionMasterId)
    {
        using var context = new DataContext();
        var resultado = from tm in context.TbTransactionMasters
            join td in context.TbTransactionMasterDetails
                on new { tm.CompanyID, tm.TransactionID, tm.TransactionMasterID }
                equals new { td.CompanyID, td.TransactionID, td.TransactionMasterID }
            join i in context.TbItems
                on new { td.CompanyID, ComponentItemId = td.ComponentItemID } equals new
                    { i.CompanyID, ComponentItemId = (int?)i.ItemID }
            join w in context.TbItemWarehouses
                on new { SourceWarehouseId = (int)tm.SourceWarehouseID, i.ItemID } equals new
                    { SourceWarehouseId = w.WarehouseID, w.ItemID }
            where td.CompanyID == companyId &&
                  td.TransactionID == transactionId &&
                  td.TransactionMasterID == transactionMasterId &&
                  td.IsActive.Value
            select new TbTransactionMasterDetailDto
            {
                CompanyId = w.CompanyID,
                BranchId = w.BranchID,
                WarehouseId = w.WarehouseID,
                ItemId = w.ItemID,
                Quantity = w.Quantity,
                Cost = w.Cost,
                QuantityMax = w.QuantityMax,
                QuantityMin = w.QuantityMin,
                DescriptionReference = td.DescriptionReference,
                ExchangeRateReference = td.ExchangeRateReference,
                ExpirationDate = td.ExpirationDate,
                Lote = td.Lote,
                TypePriceId = td.TypePriceID,
                SkuCatalogItemId = td.SkuCatalogItemID,
                SkuQuantity = td.SkuQuantity,
                SkuQuantityBySku = td.SkuQuantityBySku,
                SkuFormatoDescription = td.SkuFormatoDescription,
                ItemNameLog = td.ItemNameLog,
                AmountCommision = td.AmountCommision
            };
        return resultado.ToList();
    }

    public List<TbTransactionMasterDetailDto> GetRowByTransactionAndComponent(int companyId, int transactionId,
        int transactionMasterId, int componentId)
    {
        using var context = new DataContext();
        var resultado = componentId switch
        {
            3 => from tm in context.TbTransactionMasters
                join td in context.TbTransactionMasterDetails
                    on new { tm.CompanyID, tm.TransactionID, tm.TransactionMasterID }
                    equals new { td.CompanyID, td.TransactionID, td.TransactionMasterID }
                join i in context.TbCatalogItems
                    on td.ComponentItemID equals i.CatalogItemID
                where td.CompanyID == companyId &&
                      td.TransactionID == transactionId &&
                      td.TransactionMasterID == transactionMasterId &&
                      td.IsActive.Value
                select new TbTransactionMasterDetailDto
                {
                    CompanyId = tm.CompanyID,
                    TransactionId = tm.TransactionID,
                    TransactionMasterId = tm.TransactionMasterID,
                    TransactionMasterDetailId = td.TransactionMasterDetailID,
                    ComponentId = td.ComponentID,
                    ComponentItemId = td.ComponentItemID,
                    Reference3 = td.Reference3,
                    ItemName = i.Name,
                    Description = i.Description,
                    Display = i.Display,
                    Reference1 = i.Reference1,
                    SkuFormatoDescription = td.SkuFormatoDescription,
                    ItemNameLog = td.ItemNameLog,
                    AmountCommision = td.AmountCommision
                },
            92 => from tm in context.TbTransactionMasters
                join td in context.TbTransactionMasterDetails
                    on new { tm.CompanyID, tm.TransactionID, tm.TransactionMasterID }
                    equals new { td.CompanyID, td.TransactionID, td.TransactionMasterID }
                join i in context.TbPublicCatalogDetails
                    on td.ComponentItemID equals i.PublicCatalogDetailID
                where td.CompanyID == companyId &&
                      td.TransactionID == transactionId &&
                      td.TransactionMasterID == transactionMasterId &&
                      td.IsActive.Value
                select new TbTransactionMasterDetailDto
                {
                    CompanyId = tm.CompanyID,
                    TransactionId = tm.TransactionID,
                    TransactionMasterId = tm.TransactionMasterID,
                    TransactionMasterDetailId = td.TransactionMasterDetailID,
                    ComponentId = td.ComponentID,
                    ComponentItemId = td.ComponentItemID,
                    Reference3 = td.Reference3,
                    ItemName = i.Name,
                    Description = i.Description,
                    Display = i.Display,
                    Reference1 = i.Reference1,
                    Reference2 = i.Reference2,
                    Reference4 = i.Reference4,
                    SkuFormatoDescription = td.SkuFormatoDescription,
                    ItemNameLog = td.ItemNameLog,
                    AmountCommision = td.AmountCommision
                },
            100 => from tm in context.TbTransactionMasters
                join td in context.TbTransactionMasterDetails
                    on new { tm.CompanyID, tm.TransactionID, tm.TransactionMasterID }
                    equals new { td.CompanyID, td.TransactionID, td.TransactionMasterID }
                join tcom in context.TbCatalogItems
                    on td.ComponentItemID equals tcom.CatalogItemID
                where td.CompanyID == companyId &&
                      td.TransactionID == transactionId &&
                      td.TransactionMasterID == transactionMasterId &&
                      td.IsActive.Value
                select new TbTransactionMasterDetailDto
                {
                    CompanyId = tm.CompanyID,
                    TransactionId = tm.TransactionID,
                    TransactionMasterId = tm.TransactionMasterID,
                    TransactionMasterDetailId = td.TransactionMasterDetailID,
                    ComponentId = td.ComponentID,
                    ComponentItemId = td.ComponentItemID,
                    Reference1 = td.Reference1,
                    Reference2 = td.Reference2,
                    Reference3 = td.Reference3,
                    TipoFile = tcom.Name
                },
            _ => null
        };
        return resultado != null ? resultado.ToList() : [];
    }

    public List<TbTransactionMasterDetailDto> GetRowByTransaction(int companyId, int transactionId,
        int transactionMasterId)
    {
        using var context = new DataContext();
        var result = from td in context.TbTransactionMasterDetails
            join i in context.TbItems on new { td.CompanyID, ComponentItemId = (int)td.ComponentItemID } equals new
                { i.CompanyID, ComponentItemId = i.ItemID }
            join ci in context.TbCatalogItems on Convert.ToInt32(i.UnitMeasureID) equals ci.CatalogItemID
            where td.CompanyID == companyId &&
                  td.TransactionID == transactionId &&
                  td.TransactionMasterID == transactionMasterId &&
                  td.IsActive.Value
            select new TbTransactionMasterDetailDto
            {
                CompanyId = td.CompanyID,
                TransactionId = td.TransactionID,
                TransactionMasterId = td.TransactionMasterID,
                TransactionMasterDetailId = td.TransactionMasterDetailID,
                ComponentId = td.ComponentID,
                ComponentItemId = td.ComponentItemID,
                PromotionId = td.PromotionID,
                Amount = td.Amount,
                Cost = td.Cost,
                Quantity = td.Quantity,
                Discount = td.Discount,
                UnitaryAmount = td.UnitaryAmount,
                UnitaryCost = td.UnitaryCost,
                UnitaryPrice = td.UnitaryPrice,
                Tax1 = td.Tax1,
                Tax2 = td.Tax2,
                Tax3 = td.Tax3,
                Tax4 = td.Tax4,
                Reference1 = td.Reference1,
                Reference2 = td.Reference2,
                Reference3 = td.Reference3,
                Reference4 = td.Reference4,
                Reference5 = td.Reference5,
                Reference6 = td.Reference6,
                Reference7 = td.Reference7,
                CatalogStatusId = td.CatalogStatusID,
                InventoryStatusId = td.InventoryStatusID,
                IsActive = td.IsActive,
                QuantityStock = td.QuantityStock,
                QuantiryStockInTraffic = td.QuantiryStockInTraffic,
                QuantityStockUnaswared = td.QuantityStockUnaswared,
                RemaingStock = td.RemaingStock,
                ExpirationDate = td.ExpirationDate,
                InventoryWarehouseSourceId = td.InventoryWarehouseSourceID,
                InventoryWarehouseTargetId = td.InventoryWarehouseTargetID,
                ItemNumber = i.ItemNumber,
                BarCode = i.BarCode.Contains(',') ? i.BarCode.Substring(0, i.BarCode.IndexOf(',') + 1) + "..." : i.BarCode,
                ItemName = i.Name,
                ItemId = i.ItemID,
                UnitMeasureName = ci.Name,
                DescriptionReference = td.DescriptionReference,
                ExchangeRateReference = td.ExchangeRateReference,
                Lote = td.Lote,
                TypePriceId = td.TypePriceID,
                SkuCatalogItemId = td.SkuCatalogItemID,
                SkuQuantity = td.SkuQuantity,
                SkuQuantityBySku = td.SkuQuantityBySku,
                SkuFormatoDescription = td.SkuFormatoDescription,
                ItemNameLog = td.ItemNameLog,
                AmountCommision = td.AmountCommision
            };
        return result.ToList();
    }

    public List<TbTransactionMasterDetail> GetRowByTransactionToShare(int companyId, int transactionId, int transactionMasterId)
    {
        using var context = new DataContext();
        return context.TbTransactionMasterDetails
            .Where(detail => detail.CompanyID == companyId
                             && detail.TransactionID == transactionId
                             && detail.TransactionMasterID == transactionMasterId
                             && detail.IsActive!.Value)
            .ToList();
    }

    public void DeleteWhereTm(int companyId, int transactionId, int transactionMasterId)
    {
        using var context = new DataContext();
        var tbTransactionMasterDetails = context.TbTransactionMasterDetails
            .Where(detail => detail.CompanyID == companyId
                             && detail.TransactionID == transactionId
                             && detail.TransactionMasterID == transactionMasterId).ToList();
        foreach (var tbTransactionMasterDetail in tbTransactionMasterDetails)
        {
            tbTransactionMasterDetail.IsActive = false;
        }

        context.SaveChanges();
    }

    public void DeleteWhereIdNotIn(int companyId, int transactionId, int transactionMasterId, List<int> listTmdId, DataContext? dataContext = null)
    {
        if (dataContext == null)
        {
            using var context = new DataContext();
            DeleteWhereIdNotIn(companyId, transactionId, transactionMasterId, listTmdId, context);
            return;
        }

        var tbTransactionMasterDetails = dataContext.TbTransactionMasterDetails
            .Where(detail => detail.CompanyID == companyId
                             && detail.TransactionID == transactionId
                             && detail.TransactionMasterID == transactionMasterId
                             && !listTmdId.Contains(detail.TransactionMasterDetailID)).ToList();
        foreach (var tbTransactionMasterDetail in tbTransactionMasterDetails)
        {
            tbTransactionMasterDetail.IsActive = false;
        }

        dataContext.SaveChanges();
    }

    public List<TbTransactionMasterDetailDto> GlobalProGetRowBySalesByEmployeerMonthOnlySales(int companyId,
        DateTime dateFirst, DateTime dateLast)
    {
        using var context = new DataContext();
        var result = from t in context.TbTransactionMasters
            join ws in context.TbWorkflowStages on t.StatusID equals ws.WorkflowStageID
            join nat in context.TbNaturales on t.EntityIDSecondary equals nat.EntityID into natGroup
            from nat in natGroup.DefaultIfEmpty()
            join td in context.TbTransactionMasterDetails on t.TransactionMasterID equals td.TransactionMasterID
            join i in context.TbItems on td.ComponentItemID equals i.ItemID
            where t.TransactionID == 19 &&
                  t.IsActive!.Value! &&
                  ws.Aplicable!.Value! &&
                  t.CompanyID == companyId &&
                  t.TransactionOn >= dateFirst &&
                  t.TransactionOn <= dateLast &&
                  !i.Name.ToLower().Contains("repara")
            group new { nat, td } by nat.FirstName
            into g
            select new TbTransactionMasterDetailDto
            {
                FirstName = g.Key ?? "ND",
                Monto = g.Sum(x => x.td.UnitaryPrice * x.td.Quantity)
            };
        return result.ToList();
    }

    public List<TbTransactionMasterDetailDto> GlobalProGetRowBySalesByEmployeerMonthOnlyTenico(int companyId,
        DateTime dateFirst, DateTime dateLast)
    {
        using var context = new DataContext();
        var result = from t in context.TbTransactionMasters
            join ws in context.TbWorkflowStages on t.StatusID equals ws.WorkflowStageID
            join nat in context.TbNaturales on t.EntityIDSecondary equals nat.EntityID into natGroup
            from nat in natGroup.DefaultIfEmpty()
            join td in context.TbTransactionMasterDetails on t.TransactionMasterID equals td.TransactionMasterID
            join i in context.TbItems on td.ComponentItemID equals i.ItemID
            where t.TransactionID == 19 &&
                  t.IsActive!.Value &&
                  ws.Aplicable!.Value &&
                  t.CompanyID == companyId &&
                  t.TransactionOn >= dateFirst &&
                  t.TransactionOn <= dateLast &&
                  i.Name.Contains("repara", StringComparison.CurrentCultureIgnoreCase)
            group td by i.Name
            into g
            select new TbTransactionMasterDetailDto
            {
                FirstName = g.Key.Replace("Reparacion de Laptop", ""),
                Monto = g.Sum(x => x.UnitaryPrice * x.Quantity)
            };
        return result.ToList();
    }

    public List<TbTransactionMasterDetailDto> GlobalProGetMonthOnlySales(int companyId, DateTime dateFirst,
        DateTime dateLast)
    {
        using var context = new DataContext();
        var result = from t in context.TbTransactionMasters
            join ws in context.TbWorkflowStages on t.StatusID equals ws.WorkflowStageID
            join nat in context.TbNaturales on t.EntityIDSecondary equals nat.EntityID into natGroup
            from nat in natGroup.DefaultIfEmpty()
            where t.TransactionID == 19 &&
                  t.IsActive!.Value &&
                  ws.Aplicable!.Value &&
                  t.CompanyID == companyId &&
                  t.TransactionOn >= dateFirst &&
                  t.TransactionOn <= dateLast &&
                  ws.Aplicable.Value
            group t by t.TransactionOn.Value.Month
            into g
            select new TbTransactionMasterDetailDto
            {
                FirstName = g.Key.ToString(),
                Monto = g.Sum(x => x.SubAmount)
            };
        return result.ToList();
    }

    public List<TbTransactionMasterDetailDto> GlobalProGetDaySales(int companyId, DateTime dateFirst, DateTime dateLast)
    {
        using var context = new DataContext();
        var result = from t in context.TbTransactionMasters
            join ws in context.TbWorkflowStages on t.StatusID equals ws.WorkflowStageID
            join nat in context.TbNaturales on t.EntityIDSecondary equals nat.EntityID into natGroup
            from nat in natGroup.DefaultIfEmpty()
            where t.TransactionID == 19 &&
                  t.IsActive!.Value &&
                  ws.Aplicable!.Value &&
                  t.CompanyID == companyId &&
                  t.TransactionOn >= dateFirst &&
                  t.TransactionOn <= dateLast &&
                  ws.Aplicable.Value
            group t by t.TransactionOn!.Value.Day
            into g
            select new TbTransactionMasterDetailDto
            {
                FirstName = g.Key.ToString(),
                Monto = g.Sum(x => x.SubAmount)
            };
        return result.ToList();
    }

    public List<TbTransactionMasterDetailDto> RealStateGetClienteFuenteDeContacto(int companyId, DateTime dateFirst,
        DateTime dateLast)
    {
        using var context = new DataContext();
        return context.TbTransactionMasters
            .Join(context.TbPublicCatalogDetails,
                master => master.PriorityID,
                detail => detail.PublicCatalogDetailID,
                (master, detail) => new { master, detail })
            .Where(source => source.master.TransactionID == 42
                             && source.master.CreatedOn >= dateFirst
                             && source.master.CreatedOn <= dateLast)
            .GroupBy(arg => arg.detail.Name)
            .Select(grouping => new TbTransactionMasterDetailDto
            {
                Indicador = grouping.Key,
                Cantidad = grouping.Count()
            }).ToList();
    }

    public List<TbTransactionMasterDetailDto> RealStateGetClientesInteres(int companyId, DateTime dateFirst,
        DateTime dateLast)
    {
        using var context = new DataContext();
        return context.TbCustomers
            .Join(context.TbCatalogItems,
                customer => customer.CategoryID,
                item => item.CatalogItemID,
                (customer, item) => new { customer, item })
            .Where(arg => arg.customer.CreatedOn >= dateFirst
                          && arg.customer.CreatedOn <= dateLast)
            .GroupBy(item => item.item.Name)
            .Select(items => new TbTransactionMasterDetailDto
            {
                Indicador = items.Key,
                Cantidad = items.Count()
            }).ToList();
    }

    public List<TbTransactionMasterDetailDto> RealStateGetClientesTipoPropiedad(int companyId, DateTime dateFirst,
        DateTime dateLast)
    {
        using var context = new DataContext();
        return context.TbCustomers
            .Join(context.TbCatalogItems,
                customer => customer.ClasificationID,
                item => item.CatalogItemID,
                (customer, item) => new { customer, item })
            .Where(arg => arg.customer.CreatedOn >= dateFirst
                          && arg.customer.CreatedOn <= dateLast)
            .GroupBy(item => item.item.Name)
            .Select(items => new TbTransactionMasterDetailDto
            {
                Indicador = items.Key,
                Cantidad = items.Count()
            }).ToList();
    }

    public List<TbTransactionMasterDetailDto> RealStateGetClientesPorAgentes(int companyId, DateTime dateFirst,
        DateTime dateLast)
    {
        using var context = new DataContext();
        return context.TbCustomers
            .Join(context.TbNaturales,
                customer => customer.EntityContactID,
                item => item.EntityID,
                (customer, item) => new { customer, item })
            .Where(arg => arg.customer.CreatedOn >= dateFirst
                          && arg.customer.CreatedOn <= dateLast)
            .GroupBy(item => item.item.FirstName)
            .Select(items => new TbTransactionMasterDetailDto
            {
                Indicador = items.Key,
                Cantidad = items.Count()
            }).ToList();
    }

    public List<TbTransactionMasterDetailDto> RealStateGetClientesClasificacionPorAgentes(int companyId,
        DateTime dateFirst, DateTime dateLast)
    {
        using var context = new DataContext();
        return context.TbCustomers
            .Join(context.TbWorkflowStages,
                customer => customer.StatusID,
                stage => stage.WorkflowStageID,
                (customer, stage) => new { customer, stage })
            .Join(context.TbNaturales, arg => arg.customer.EntityContactID,
                naturale => naturale.EntityID, (arg1, naturale) => new { arg1, naturale })
            .Where(arg => arg.arg1.customer.CreatedOn >= dateFirst
                          && arg.arg1.customer.CreatedOn <= dateLast)
            .GroupBy(arg => new { arg.arg1.stage.Name, arg.naturale.FirstName })
            .Select(items => new TbTransactionMasterDetailDto
            {
                Indicador = items.Key.Name,
                Agente = items.Key.FirstName,
                Cantidad = items.Count()
            }).ToList();
    }

    public List<TbTransactionMasterDetailDto> RealStateGetClientesCerrados(int companyId, DateTime dateFirst,
        DateTime dateLast)
    {
        using var context = new DataContext();
        return context.TbCustomers
            .Join(context.TbWorkflowStages,
                customer => customer.StatusID,
                stage => stage.WorkflowStageID,
                (customer, stage) => new { customer, stage })
            .Where(arg => arg.customer.CreatedOn >= dateFirst
                          && arg.customer.CreatedOn <= dateLast)
            .GroupBy(arg => arg.stage.Name)
            .Select(grouping => new TbTransactionMasterDetailDto
            {
                Indicador = grouping.Key,
                Cantidad = grouping.Count()
            }).ToList();
    }

    public List<TbTransactionMasterDetailDto> RealStateGetAgenteEfectividad(int companyId, DateTime dateFirst,
        DateTime dateLast)
    {
        using var context = new DataContext();
        var list = new List<int>();
        list.Add(19);
        var query = from t in context.TbTransactionMasters
            join ws in context.TbWorkflowStages on t.StatusID equals ws.WorkflowStageID
            join nat in context.TbNaturales on t.EntityIDSecondary equals nat.EntityID into natJoin
            from nat in natJoin.DefaultIfEmpty()
            where list.Contains(t.TransactionID)
                  && t.IsActive!.Value
                  && ws.Aplicable!.Value
                  && t.CompanyID == companyId
                  && t.TransactionOn >= dateFirst && t.TransactionOn <= dateLast
            group t by new { Day = t.TransactionOn!.Value.Day }
            into g
            select new TbTransactionMasterDetailDto
            {
                FirstName = g.Key.ToString(),
                Monto = g.Sum(t => t.SubAmount)
            };
        return query.ToList();
    }

    public List<TbTransactionMasterDetailDto> RealStateGetPropiedadesPorAgentes(int companyId, DateTime dateFirst,
        DateTime dateLast)
    {
        using var context = new DataContext();
        return context.TbItems
            .Join(context.TbNaturales,
                item => item.RealStateEmployerAgentID,
                naturale => naturale.EntityID,
                (item, naturale) => new { item, naturale })
            .Where(arg => arg.item.CreatedOn >= dateFirst
                          && arg.item.CreatedOn <= dateLast)
            .GroupBy(arg => arg.naturale.FirstName)
            .Select(grouping => new TbTransactionMasterDetailDto
            {
                FirstName = grouping.Key,
                Cantidad = grouping.Count()
            }).ToList();
    }

    public List<TbTransactionMasterDetailDto> RealStateGetPropiedadesPorAgentesMetas(int companyId, DateTime dateFirst,
        DateTime dateLast)
    {
        using var context = new DataContext();
        var list = new List<int>();
        list.Add(19);
        return context.TbTransactionMasters
            .Join(context.TbWorkflowStages,
                master => master.StatusID,
                stage => stage.WorkflowStageID,
                (master, stage) => new { master, stage })
            .Where(arg => list.Contains(arg.master.TransactionID)
                          && arg.master.IsActive!.Value
                          && arg.stage.Aplicable!.Value
                          && arg.master.CompanyID == companyId
                          && arg.master.TransactionOn >= dateFirst
                          && arg.master.TransactionOn <= dateLast)
            .GroupBy(arg => arg.master.TransactionOn!.Value)
            .Select(grouping => new TbTransactionMasterDetailDto
            {
                FirstName = grouping.Key.Day.ToString(),
                Monto = grouping.Sum(arg => arg.master.SubAmount)
            }).ToList();
    }

    public List<TbTransactionMasterDetailDto> RealStateGetPropiedadesRendimientoAnualVentas(int companyId,
        DateTime dateFirst, DateTime dateLast)
    {
        using var context = new DataContext();
        var list = new List<int> { 19 };
        var query = context.TbTransactionMasters
            .Join(context.TbWorkflowStages,
                t => t.StatusID,
                ws => ws.WorkflowStageID,
                (t, ws) => new { t, ws })
            .Where(t1 => list.Contains(t1.t.TransactionID)
                         && t1.t.IsActive!.Value
                         && t1.ws.Aplicable!.Value
                         && t1.t.CompanyID == companyId
                         && t1.t.TransactionOn >= dateFirst
                         && t1.t.TransactionOn <= dateLast)
            .GroupJoin(context.TbNaturales,
                t1 => t1.t.EntityIDSecondary,
                nat => nat.EntityID,
                (t1, natJoin) => new { t1, natJoin })
            .SelectMany(t1 => t1.natJoin.DefaultIfEmpty(),
                (t1, nat) => new
                {
                    FirstName = t1.t1.t.TransactionOn!.Value.Day, // Assuming you meant to use the day as the first name
                    Monto = t1.t1.t.SubAmount
                });

        var result = query.GroupBy(x => x.FirstName)
            .Select(g => new TbTransactionMasterDetailDto
            {
                FirstName = g.Key.ToString(),
                Monto = g.Sum(x => x.Monto)
            });
        return result.ToList();
    }

    public List<TbTransactionMasterDetailDto> RealStateGetPropiedadesRendimientoAnualEnlistamiento(int companyId,
        DateTime dateFirst, DateTime dateLast)
    {
        using var context = new DataContext();
        var list = new List<int> { 19 };
        var result = from t in context.TbTransactionMasters
            join ws in context.TbWorkflowStages on t.StatusID equals ws.WorkflowStageID
            join nat in context.TbNaturales on t.EntityIDSecondary equals nat.EntityID into natJoin
            from nat in natJoin.DefaultIfEmpty()
            where list.Contains(t.TransactionID)
                  && t.IsActive!.Value
                  && ws.Aplicable!.Value
                  && t.CompanyID == companyId
                  && t.TransactionOn >= dateFirst
                  && t.TransactionOn <= dateLast
            group t by t.TransactionOn!.Value.Day
            into g
            select new TbTransactionMasterDetailDto
            {
                FirstName = g.Key.ToString(),
                Monto = g.Sum(t => t.SubAmount)
            };
        return result.ToList();
    }

    public List<TbTransactionMasterDetailDto> GetRowByShareId(int companyId, int transactionMasterId)
    {
        using var context = new DataContext();
        var query =
            from tm in context.TbTransactionMasters.AsNoTracking()
            join ws in context.TbWorkflowStages.AsNoTracking()
                on tm.StatusID equals ws.WorkflowStageID
            join tmd in context.TbTransactionMasterDetails.AsNoTracking()
                on tm.TransactionMasterID equals tmd.TransactionMasterID
            join ccd in context.TbCustomerCreditDocuments.AsNoTracking()
                on tmd.Reference1 equals ccd.DocumentNumber
            join wss in context.TbWorkflowStages.AsNoTracking()
                on ccd.StatusID equals wss.WorkflowStageID
            join fac in context.TbTransactionMasters.AsNoTracking()
                on ccd.DocumentNumber equals fac.TransactionNumber
            join ul in context.TbTransactionMasterDetails.AsNoTracking()
                on fac.TransactionMasterID equals ul.TransactionMasterID
            join i in context.TbItems.AsNoTracking()
                on ul.ComponentItemID equals i.ItemID
            join nat in context.TbNaturales.AsNoTracking()
                on ccd.EntityID equals nat.EntityID
            join cus in context.TbCustomers.AsNoTracking()
                on nat.EntityID equals cus.EntityID
            where
                tm.IsActive!.Value &&
                tm.TransactionID == 23 &&
                tm.StatusID == 80 && // "aplicado"
                ccd.StatusID == 82 && // "cancelado"
                tm.TransactionMasterID == transactionMasterId
            select new TbTransactionMasterDetailDto
            {
                TransactionNumber = tm.TransactionNumber,
                CustomerNumber = cus.CustomerNumber,
                FirstName = nat.FirstName,
                FacturaCancelada = fac.TransactionNumber,
                CreatedOn = fac.CreatedOn,
                ItemName = i.Name,
                Quantity = ul.Quantity,
                UnitaryPrice = ul.UnitaryPrice,
                Tax1 = ul.Tax1 ?? 0,
                Tax2 = ul.Tax2 ?? 0,
                Amount = ul.Amount
            };
        return query.ToList();
    }
}