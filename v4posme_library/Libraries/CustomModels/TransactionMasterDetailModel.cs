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
    

    public int InsertAppPosme(TbTransactionMasterDetail data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.TransactionMasterDetailId;
    }

    public void UpdateAppPosme(int companyId, int transactionId, int transactionMasterId,
        int transactionMasterDetailId, TbTransactionMasterDetail data)
    {
        using var context = new DataContext();
        var find = context.TbTransactionMasterDetails
            .FirstOrDefault(detail => detail.CompanyId == companyId
                                      && detail.TransactionId == transactionId
                                      && detail.TransactionMasterId == transactionMasterId
                                      && detail.TransactionMasterDetailId == transactionMasterDetailId);
        if (find is null) return;
        data.TransactionMasterDetailId = find.TransactionMasterDetailId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public TbTransactionMasterDetailDto GetRowByPk(int companyId, int transactionId, int transactionMasterId,
        int transactionMasterDetailId, int componentId = 33)
    {
        using var context = new DataContext();
        var mapper = new Mapper(_mapperConfiguration);
        var result = componentId switch
        {
            33 => from td in context.TbTransactionMasterDetails
                join item in context.TbItems on new { td.CompanyId, ComponentItemId = (int)td.ComponentItemId } equals
                    new { item.CompanyId, ComponentItemId = item.ItemId }
                join ci in context.TbCatalogItems on Convert.ToInt32(item.UnitMeasureId) equals ci.CatalogItemId
                where td.CompanyId == companyId
                      && td.TransactionId == transactionId
                      && td.TransactionMasterId == transactionMasterId
                      && td.TransactionMasterDetailId == transactionMasterDetailId
                      && td.IsActive!.Value
                select new TbTransactionMasterDetailDto
                {
                    CompanyId = td.CompanyId,
                    TransactionId = td.TransactionId,
                    TransactionMasterId = td.TransactionMasterId,
                    TransactionMasterDetailId = td.TransactionMasterDetailId,
                    ComponentId = td.ComponentId,
                    ComponentItemId = td.ComponentItemId,
                    PromotionId = td.PromotionId,
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
                    CatalogStatusId = td.CatalogStatusId,
                    InventoryStatusId = td.InventoryStatusId,
                    IsActive = td.IsActive,
                    QuantityStock = td.QuantityStock,
                    QuantiryStockInTraffic = td.QuantiryStockInTraffic,
                    QuantityStockUnaswared = td.QuantityStockUnaswared,
                    RemaingStock = td.RemaingStock,
                    ExpirationDate = td.ExpirationDate,
                    InventoryWarehouseSourceId = td.InventoryWarehouseSourceId,
                    InventoryWarehouseTargetId = td.InventoryWarehouseTargetId,
                    ItemNumber = item.ItemNumber,
                    ItemName = item.Name,
                    UnitMeasureName = ci.Name,
                    DescriptionReference = td.DescriptionReference,
                    ExchangeRateReference = td.ExchangeRateReference,
                    Lote = td.Lote,
                    TypePriceId = td.TypePriceId,
                    SkuCatalogItemId = td.SkuCatalogItemId,
                    SkuQuantity = td.SkuQuantity,
                    SkuQuantityBySku = td.SkuQuantityBySku,
                    SkuFormatoDescription = td.SkuFormatoDescription,
                    ItemNameLog = td.ItemNameLog,
                    AmountCommision = td.AmountCommision
                },
            64 => from td in context.TbTransactionMasterDetails
                join i in context.TbCustomerCreditDocuments on new
                    { td.CompanyId, ComponentItemId = (int)td.ComponentItemId } equals new
                    { i.CompanyId, ComponentItemId = i.CustomerCreditDocumentId }
                where td.CompanyId == companyId
                      && td.TransactionId == transactionId
                      && td.TransactionMasterId == transactionMasterId
                      && td.TransactionMasterDetailId == transactionMasterDetailId
                      && td.IsActive.Value
                select mapper.Map<TbTransactionMasterDetailDto>(td),
            _ => from td in context.TbTransactionMasterDetails
                where td.CompanyId == companyId
                      && td.TransactionId == transactionId
                      && td.TransactionMasterId == transactionMasterId
                      && td.TransactionMasterDetailId == transactionMasterDetailId
                      && td.IsActive.Value
                select mapper.Map<TbTransactionMasterDetailDto>(td)
        };
        return result.First();
    }

    public TbTransactionMasterDetail? GetRowByPKK(     int transactionMasterDetailId )
    {
        using var context = new DataContext();
        var result = context.TbTransactionMasterDetails.FirstOrDefault(c => c.TransactionMasterDetailId == transactionMasterDetailId);
        return result;
    }


    public TbTransactionMasterDetailDto GetRowByTransactionAndItems(int companyId, int transactionId,
        int transactionMasterId,
        List<int> listTmdId)
    {
        using var context = new DataContext();
        var result = from td in context.TbTransactionMasterDetails
            join i in context.TbItems on new { td.CompanyId, ComponentItemId = td.ComponentItemId } equals new
                { i.CompanyId, ComponentItemId = (int?)i.ItemId }
            join ci in context.TbCatalogItems on Convert.ToInt32(i.UnitMeasureId) equals ci.CatalogItemId
            where td.CompanyId == companyId
                  && td.TransactionId == transactionId
                  && td.TransactionMasterId == transactionMasterId
                  && td.IsActive.Value
                  && listTmdId.Contains(i.ItemId)
            select new TbTransactionMasterDetailDto
            {
                CompanyId = td.CompanyId,
                TransactionId = td.TransactionId,
                TransactionMasterId = td.TransactionMasterId,
                TransactionMasterDetailId = td.TransactionMasterDetailId,
                ComponentId = td.ComponentId,
                ComponentItemId = td.ComponentItemId,
                PromotionId = td.PromotionId,
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
                CatalogStatusId = td.CatalogStatusId,
                InventoryStatusId = td.InventoryStatusId,
                IsActive = td.IsActive,
                QuantityStock = td.QuantityStock,
                QuantiryStockInTraffic = td.QuantiryStockInTraffic,
                QuantityStockUnaswared = td.QuantityStockUnaswared,
                RemaingStock = td.RemaingStock,
                ExpirationDate = td.ExpirationDate,
                InventoryWarehouseSourceId = td.InventoryWarehouseSourceId,
                InventoryWarehouseTargetId = td.InventoryWarehouseTargetId,
                ItemNumber = i.ItemNumber,
                BarCode = i.BarCode,
                ItemName = i.Name,
                UnitMeasureName = ci.Name,
                DescriptionReference = td.DescriptionReference,
                ExchangeRateReference = td.ExchangeRateReference,
                Lote = td.Lote,
                TypePriceId = td.TypePriceId,
                SkuCatalogItemId = td.SkuCatalogItemId,
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
                on new { tm.CompanyId, tm.TransactionId, tm.TransactionMasterId }
                equals new { td.CompanyId, td.TransactionId, td.TransactionMasterId }
            join i in context.TbItems
                on new { td.CompanyId, ComponentItemId = td.ComponentItemId } equals new
                    { i.CompanyId, ComponentItemId = (int?)i.ItemId }
            join w in context.TbItemWarehouses
                on new { SourceWarehouseId = (int)tm.SourceWarehouseId, i.ItemId } equals new
                    { SourceWarehouseId = w.WarehouseId, w.ItemId }
            where td.CompanyId == companyId &&
                  td.TransactionId == transactionId &&
                  td.TransactionMasterId == transactionMasterId &&
                  td.IsActive.Value
            select new TbTransactionMasterDetailDto
            {
                CompanyId = w.CompanyId,
                BranchId = w.BranchId,
                WarehouseId = w.WarehouseId,
                ItemId = w.ItemId,
                Quantity = w.Quantity,
                Cost = w.Cost,
                QuantityMax = w.QuantityMax,
                QuantityMin = w.QuantityMin,
                DescriptionReference = td.DescriptionReference,
                ExchangeRateReference = td.ExchangeRateReference,
                ExpirationDate = td.ExpirationDate,
                Lote = td.Lote,
                TypePriceId = td.TypePriceId,
                SkuCatalogItemId = td.SkuCatalogItemId,
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
                    on new { tm.CompanyId, tm.TransactionId, tm.TransactionMasterId }
                    equals new { td.CompanyId, td.TransactionId, td.TransactionMasterId }
                join i in context.TbCatalogItems
                    on td.ComponentItemId equals i.CatalogItemId
                where td.CompanyId == companyId &&
                      td.TransactionId == transactionId &&
                      td.TransactionMasterId == transactionMasterId &&
                      td.IsActive.Value
                select new TbTransactionMasterDetailDto
                {
                    CompanyId = tm.CompanyId,
                    TransactionId = tm.TransactionId,
                    TransactionMasterId = tm.TransactionMasterId,
                    TransactionMasterDetailId = td.TransactionMasterDetailId,
                    ComponentId = td.ComponentId,
                    ComponentItemId = td.ComponentItemId,
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
                    on new { tm.CompanyId, tm.TransactionId, tm.TransactionMasterId }
                    equals new { td.CompanyId, td.TransactionId, td.TransactionMasterId }
                join i in context.TbPublicCatalogDetails
                    on td.ComponentItemId equals i.PublicCatalogDetailId
                where td.CompanyId == companyId &&
                      td.TransactionId == transactionId &&
                      td.TransactionMasterId == transactionMasterId &&
                      td.IsActive.Value
                select new TbTransactionMasterDetailDto
                {
                    CompanyId = tm.CompanyId,
                    TransactionId = tm.TransactionId,
                    TransactionMasterId = tm.TransactionMasterId,
                    TransactionMasterDetailId = td.TransactionMasterDetailId,
                    ComponentId = td.ComponentId,
                    ComponentItemId = td.ComponentItemId,
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
                    on new { tm.CompanyId, tm.TransactionId, tm.TransactionMasterId }
                    equals new { td.CompanyId, td.TransactionId, td.TransactionMasterId }
                join tcom in context.TbCatalogItems
                    on td.ComponentItemId equals tcom.CatalogItemId
                where td.CompanyId == companyId &&
                      td.TransactionId == transactionId &&
                      td.TransactionMasterId == transactionMasterId &&
                      td.IsActive.Value
                select new TbTransactionMasterDetailDto
                {
                    CompanyId = tm.CompanyId,
                    TransactionId = tm.TransactionId,
                    TransactionMasterId = tm.TransactionMasterId,
                    TransactionMasterDetailId = td.TransactionMasterDetailId,
                    ComponentId = td.ComponentId,
                    ComponentItemId = td.ComponentItemId,
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
            join i in context.TbItems on new { td.CompanyId, ComponentItemId = (int)td.ComponentItemId } equals new
                { i.CompanyId, ComponentItemId = i.ItemId }
            join ci in context.TbCatalogItems on Convert.ToInt32(i.UnitMeasureId) equals ci.CatalogItemId
            where td.CompanyId == companyId &&
                  td.TransactionId == transactionId &&
                  td.TransactionMasterId == transactionMasterId &&
                  td.IsActive.Value
            select new TbTransactionMasterDetailDto
            {
                CompanyId = td.CompanyId,
                TransactionId = td.TransactionId,
                TransactionMasterId = td.TransactionMasterId,
                TransactionMasterDetailId = td.TransactionMasterDetailId,
                ComponentId = td.ComponentId,
                ComponentItemId = td.ComponentItemId,
                PromotionId = td.PromotionId,
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
                CatalogStatusId = td.CatalogStatusId,
                InventoryStatusId = td.InventoryStatusId,
                IsActive = td.IsActive,
                QuantityStock = td.QuantityStock,
                QuantiryStockInTraffic = td.QuantiryStockInTraffic,
                QuantityStockUnaswared = td.QuantityStockUnaswared,
                RemaingStock = td.RemaingStock,
                ExpirationDate = td.ExpirationDate,
                InventoryWarehouseSourceId = td.InventoryWarehouseSourceId,
                InventoryWarehouseTargetId = td.InventoryWarehouseTargetId,
                ItemNumber = i.ItemNumber,
                BarCode = i.BarCode.Contains(',')
                    ? i.BarCode.Substring(0, i.BarCode.IndexOf(',') + 1) + "..."
                    : i.BarCode,
                ItemName = i.Name,
                UnitMeasureName = ci.Name,
                DescriptionReference = td.DescriptionReference,
                ExchangeRateReference = td.ExchangeRateReference,
                Lote = td.Lote,
                TypePriceId = td.TypePriceId,
                SkuCatalogItemId = td.SkuCatalogItemId,
                SkuQuantity = td.SkuQuantity,
                SkuQuantityBySku = td.SkuQuantityBySku,
                SkuFormatoDescription = td.SkuFormatoDescription,
                ItemNameLog = td.ItemNameLog,
                AmountCommision = td.AmountCommision
            };
        return result.ToList();
    }

    public List<TbTransactionMasterDetail> GetRowByTransactionToShare(int companyId, int transactionId,
        int transactionMasterId)
    {
        using var context = new DataContext();
        return context.TbTransactionMasterDetails
            .Where(detail => detail.CompanyId == companyId
                             && detail.TransactionId == transactionId
                             && detail.TransactionMasterId == transactionMasterId
                             && detail.IsActive!.Value)
            .ToList();
    }

    public void DeleteWhereTm(int companyId, int transactionId, int transactionMasterId)
    {
        using var context = new DataContext();
        context.TbTransactionMasterDetails
            .Where(detail => detail.CompanyId == companyId
                             && detail.TransactionId == transactionId
                             && detail.TransactionMasterId == transactionMasterId)
            .ExecuteUpdate(calls => calls.SetProperty(detail => detail.IsActive, false));
    }

    public void DeleteWhereIdNotIn(int companyId, int transactionId, int transactionMasterId, List<int> listTmdId)
    {
        using var context = new DataContext();
        context.TbTransactionMasterDetails
            .Where(detail => detail.CompanyId == companyId
                             && detail.TransactionId == transactionId
                             && detail.TransactionMasterId == transactionMasterId
                             && !listTmdId.Contains(detail.TransactionMasterDetailId))
            .ExecuteUpdate(calls => calls.SetProperty(detail => detail.IsActive, false));
    }

    public List<TbTransactionMasterDetailDto> GlobalProGetRowBySalesByEmployeerMonthOnlySales(int companyId,
        DateTime dateFirst, DateTime dateLast)
    {
        using var context = new DataContext();
        var result = from t in context.TbTransactionMasters
            join ws in context.TbWorkflowStages on t.StatusId equals ws.WorkflowStageId
            join nat in context.TbNaturales on t.EntityIdsecondary equals nat.EntityId into natGroup
            from nat in natGroup.DefaultIfEmpty()
            join td in context.TbTransactionMasterDetails on t.TransactionMasterId equals td.TransactionMasterId
            join i in context.TbItems on td.ComponentItemId equals i.ItemId
            where t.TransactionId == 19 &&
                  t.IsActive!.Value! &&
                  ws.Aplicable!.Value! &&
                  t.CompanyId == companyId &&
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
            join ws in context.TbWorkflowStages on t.StatusId equals ws.WorkflowStageId
            join nat in context.TbNaturales on t.EntityIdsecondary equals nat.EntityId into natGroup
            from nat in natGroup.DefaultIfEmpty()
            join td in context.TbTransactionMasterDetails on t.TransactionMasterId equals td.TransactionMasterId
            join i in context.TbItems on td.ComponentItemId equals i.ItemId
            where t.TransactionId == 19 &&
                  t.IsActive!.Value &&
                  ws.Aplicable!.Value &&
                  t.CompanyId == companyId &&
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
            join ws in context.TbWorkflowStages on t.StatusId equals ws.WorkflowStageId
            join nat in context.TbNaturales on t.EntityIdsecondary equals nat.EntityId into natGroup
            from nat in natGroup.DefaultIfEmpty()
            where t.TransactionId == 19 &&
                  t.IsActive!.Value &&
                  ws.Aplicable!.Value &&
                  t.CompanyId == companyId &&
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
            join ws in context.TbWorkflowStages on t.StatusId equals ws.WorkflowStageId
            join nat in context.TbNaturales on t.EntityIdsecondary equals nat.EntityId into natGroup
            from nat in natGroup.DefaultIfEmpty()
            where t.TransactionId == 19 &&
                  t.IsActive!.Value &&
                  ws.Aplicable!.Value &&
                  t.CompanyId == companyId &&
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
                master => master.PriorityId,
                detail => detail.PublicCatalogDetailId,
                (master, detail) => new { master, detail })
            .Where(source => source.master.TransactionId == 42
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
                customer => customer.CategoryId,
                item => item.CatalogItemId,
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
                customer => customer.ClasificationId,
                item => item.CatalogItemId,
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
                customer => customer.EntityContactId,
                item => item.EntityId,
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
                customer => customer.StatusId,
                stage => stage.WorkflowStageId,
                (customer, stage) => new { customer, stage })
            .Join(context.TbNaturales, arg => arg.customer.EntityContactId,
                naturale => naturale.EntityId, (arg1, naturale) => new { arg1, naturale })
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
                customer => customer.StatusId,
                stage => stage.WorkflowStageId,
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
            join ws in context.TbWorkflowStages on t.StatusId equals ws.WorkflowStageId
            join nat in context.TbNaturales on t.EntityIdsecondary equals nat.EntityId into natJoin
            from nat in natJoin.DefaultIfEmpty()
            where list.Contains(t.TransactionId)
                  && t.IsActive!.Value
                  && ws.Aplicable!.Value
                  && t.CompanyId == companyId
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
                item => item.RealStateEmployerAgentId,
                naturale => naturale.EntityId,
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
                master => master.StatusId,
                stage => stage.WorkflowStageId,
                (master, stage) => new { master, stage })
            .Where(arg => list.Contains(arg.master.TransactionId)
                          && arg.master.IsActive!.Value
                          && arg.stage.Aplicable!.Value
                          && arg.master.CompanyId == companyId
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
                t => t.StatusId,
                ws => ws.WorkflowStageId,
                (t, ws) => new { t, ws })
            .Where(t1 => list.Contains(t1.t.TransactionId)
                         && t1.t.IsActive!.Value
                         && t1.ws.Aplicable!.Value
                         && t1.t.CompanyId == companyId
                         && t1.t.TransactionOn >= dateFirst
                         && t1.t.TransactionOn <= dateLast)
            .GroupJoin(context.TbNaturales,
                t1 => t1.t.EntityIdsecondary,
                nat => nat.EntityId,
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
            join ws in context.TbWorkflowStages on t.StatusId equals ws.WorkflowStageId
            join nat in context.TbNaturales on t.EntityIdsecondary equals nat.EntityId into natJoin
            from nat in natJoin.DefaultIfEmpty()
            where list.Contains(t.TransactionId)
                  && t.IsActive!.Value
                  && ws.Aplicable!.Value
                  && t.CompanyId == companyId
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
}