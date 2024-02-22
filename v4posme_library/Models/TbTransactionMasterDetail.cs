using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_transaction_master_detail")]
[Index("CompanyId", Name = "IDX_TRANSACTION_MASTER_DETAIL_001")]
[Index("TransactionId", Name = "IDX_TRANSACTION_MASTER_DETAIL_002")]
[Index("TransactionMasterId", Name = "IDX_TRANSACTION_MASTER_DETAIL_003")]
[Index("ComponentId", Name = "IDX_TRANSACTION_MASTER_DETAIL_004")]
[Index("ComponentItemId", Name = "IDX_TRANSACTION_MASTER_DETAIL_005")]
[Index("CatalogStatusId", Name = "IDX_TRANSACTION_MASTER_DETAIL_006")]
[Index("InventoryStatusId", Name = "IDX_TRANSACTION_MASTER_DETAIL_007")]
[Index("InventoryWarehouseSourceId", Name = "IDX_TRANSACTION_MASTER_DETAIL_008")]
[Index("InventoryWarehouseTargetId", Name = "IDX_TRANSACTION_MASTER_DETAIL_009")]
[Index("TypePriceId", Name = "IDX_TRANSACTION_MASTER_DETAIL_010")]
[Index("SkuCatalogItemId", Name = "IDX_TRANSACTION_MASTER_DETAIL_011")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public class TbTransactionMasterDetail
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("transactionID", TypeName = "int(11)")]
    public int TransactionId { get; set; }

    [Column("transactionMasterID", TypeName = "int(11)")]
    public int TransactionMasterId { get; set; }

    [Key]
    [Column("transactionMasterDetailID", TypeName = "int(11)")]
    public int TransactionMasterDetailId { get; set; }

    [Column("componentID", TypeName = "int(11)")]
    public int? ComponentId { get; set; }

    [Column("componentItemID", TypeName = "int(11)")]
    public int? ComponentItemId { get; set; }

    [Column("promotionID", TypeName = "int(11)")]
    public int? PromotionId { get; set; }

    [Column("amount")]
    [Precision(18, 4)]
    public decimal? Amount { get; set; }

    [Column("cost")]
    [Precision(18, 4)]
    public decimal? Cost { get; set; }

    [Column("quantity")]
    [Precision(18, 4)]
    public decimal? Quantity { get; set; }

    [Column("discount")]
    [Precision(18, 4)]
    public decimal? Discount { get; set; }

    [Column("unitaryAmount")]
    [Precision(18, 4)]
    public decimal? UnitaryAmount { get; set; }

    [Column("tax1")]
    [Precision(18, 4)]
    public decimal? Tax1 { get; set; }

    [Column("tax2")]
    [Precision(18, 4)]
    public decimal? Tax2 { get; set; }

    [Column("tax3")]
    [Precision(18, 4)]
    public decimal? Tax3 { get; set; }

    [Column("tax4")]
    [Precision(18, 4)]
    public decimal? Tax4 { get; set; }

    [Column("unitaryCost")]
    [Precision(18, 4)]
    public decimal? UnitaryCost { get; set; }

    [Column("unitaryPrice")]
    [Precision(18, 4)]
    public decimal? UnitaryPrice { get; set; }

    [Column("reference1")]
    [StringLength(250)]
    public string? Reference1 { get; set; }

    [Column("reference2")]
    [StringLength(250)]
    public string? Reference2 { get; set; }

    [Column("reference3")]
    [StringLength(250)]
    public string? Reference3 { get; set; }

    [Column("reference4")]
    [StringLength(250)]
    public string? Reference4 { get; set; }

    [Column("reference5")]
    [StringLength(250)]
    public string? Reference5 { get; set; }

    [Column("reference6")]
    [StringLength(250)]
    public string? Reference6 { get; set; }

    [Column("reference7")]
    [StringLength(250)]
    public string? Reference7 { get; set; }

    [Column("descriptionReference")]
    [StringLength(800)]
    public string? DescriptionReference { get; set; }

    [Column("exchangeRateReference")]
    [Precision(18, 8)]
    public decimal? ExchangeRateReference { get; set; }

    [Column("catalogStatusID", TypeName = "int(11)")]
    public int? CatalogStatusId { get; set; }

    [Column("inventoryStatusID", TypeName = "int(11)")]
    public int? InventoryStatusId { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }

    [Column("quantityStock")]
    [Precision(18, 4)]
    public decimal? QuantityStock { get; set; }

    [Column("quantiryStockInTraffic")]
    [Precision(18, 4)]
    public decimal? QuantiryStockInTraffic { get; set; }

    [Column("quantityStockUnaswared")]
    [Precision(18, 4)]
    public decimal? QuantityStockUnaswared { get; set; }

    [Column("remaingStock")]
    [Precision(18, 4)]
    public decimal? RemaingStock { get; set; }

    [Column("lote")]
    [StringLength(255)]
    public string? Lote { get; set; }

    [Column("expirationDate", TypeName = "datetime")]
    public DateTime? ExpirationDate { get; set; }

    [Column("inventoryWarehouseSourceID", TypeName = "int(11)")]
    public int? InventoryWarehouseSourceId { get; set; }

    [Column("inventoryWarehouseTargetID", TypeName = "int(11)")]
    public int? InventoryWarehouseTargetId { get; set; }

    [Column("itemFormulatedApplied")]
    public bool ItemFormulatedApplied { get; set; }

    [Column("typePriceID", TypeName = "int(11)")]
    public int TypePriceId { get; set; }

    [Column("skuCatalogItemID", TypeName = "int(11)")]
    public int SkuCatalogItemId { get; set; }

    [Column("skuQuantity")]
    [Precision(10, 2)]
    public decimal SkuQuantity { get; set; }

    [Column("skuQuantityBySku")]
    [Precision(10, 2)]
    public decimal SkuQuantityBySku { get; set; }

    [Column("skuFormatoDescription")]
    [StringLength(255)]
    public string? SkuFormatoDescription { get; set; }

    [Column("itemNameLog")]
    [StringLength(255)]
    public string? ItemNameLog { get; set; }

    [Column("amountCommision")]
    [Precision(19, 8)]
    public decimal AmountCommision { get; set; }
}
