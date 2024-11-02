using AutoMapper;
using v4posme_library.Models;

namespace v4posme_library.ModelsDto;

public class TbTransactionMasterDetailDto
{
    public int CompanyId { get; set; }
    public int TransactionId { get; set; }
    public int TransactionMasterId { get; set; }
    public int TransactionMasterDetailId { get; set; }
    public int? ComponentId { get; set; }
    public int? ComponentItemId { get; set; }
    public int? PromotionId { get; set; }
    public decimal? Amount { get; set; }
    public decimal? Cost { get; set; }
    public decimal? Quantity { get; set; }
    public decimal? Discount { get; set; }
    public decimal? UnitaryAmount { get; set; }
    public decimal? UnitaryCost { get; set; }
    public decimal? UnitaryPrice { get; set; }
    public string? Reference1 { get; set; }
    public string? Reference2 { get; set; }
    public string? Reference3 { get; set; }
    public string? Reference4 { get; set; }
    public string? Reference5 { get; set; }
    public string? Reference6 { get; set; }
    public string? Reference7 { get; set; }
    public int? CatalogStatusId { get; set; }
    public int? InventoryStatusId { get; set; }
    public bool? IsActive { get; set; }
    public decimal? QuantityStock { get; set; }
    public decimal? QuantiryStockInTraffic { get; set; }
    public decimal? QuantityStockUnaswared { get; set; }
    public decimal? RemaingStock { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public int? InventoryWarehouseSourceId { get; set; }
    public int? InventoryWarehouseTargetId { get; set; }
    public string? ItemNumber { get; set; }
    public string? ItemName { get; set; }
    public string? UnitMeasureName { get; set; }
    public string? DescriptionReference { get; set; }
    public decimal? ExchangeRateReference { get; set; }
    public string? Lote { get; set; }
    public int TypePriceId { get; set; }
    public int SkuCatalogItemId { get; set; }
    public decimal SkuQuantity { get; set; }
    public decimal SkuQuantityBySku { get; set; }
    public string? SkuFormatoDescription { get; set; }
    public string? ItemNameLog { get; set; }
    public decimal AmountCommision { get; set; }
    public string? BarCode { get; set; }
    public int BranchId { get; set; }
    public int WarehouseId { get; set; }
    public int ItemId { get; set; }
    public decimal QuantityMax { get; set; }
    public decimal QuantityMin { get; set; }
    public string? Description { get; set; }
    public string? Display { get; set; }
    public string? TipoFile { get; set; }
    public decimal? Tax1 { get; set; }
    public decimal? Tax2 { get; set; }
    public decimal? Tax3 { get; set; }
    public decimal? Tax4 { get; set; }
    public string? FirstName { get; set; }
    public decimal? Monto { get; set; }
    public string? Indicador { get; set; }
    public int Cantidad { get; set; }
    public string? Agente { get; set; }
}