using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_transaction_master_detail_temp")]
[Index("CompanyId", "TransactionId", "TransactionMasterId", "ComponentId", "ComponentItemId", Name = "IDX_transaction_concept")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbTransactionMasterDetailTemp
{
    [Key]
    [Column("transactionMasterDetailTemporalID")]
    public int TransactionMasterDetailTemporalId { get; set; }

    [Column("token")]
    [StringLength(500)]
    public string? Token { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("transactionID")]
    public int TransactionId { get; set; }

    [Column("transactionMasterID")]
    public int TransactionMasterId { get; set; }

    [Column("transactionMasterDetailID")]
    public int TransactionMasterDetailId { get; set; }

    [Column("componentID")]
    public int ComponentId { get; set; }

    [Column("componentItemID")]
    public int? ComponentItemId { get; set; }

    [Column("promotionID")]
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

    [Column("catalogStatusID")]
    public int? CatalogStatusId { get; set; }

    [Column("inventoryStatusID")]
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

    [Column("expirationDate", TypeName = "datetime")]
    public DateTime? ExpirationDate { get; set; }

    [Column("inventoryWarehouseSourceID")]
    public int? InventoryWarehouseSourceId { get; set; }

    [Column("inventoryWarehouseTargetID")]
    public int? InventoryWarehouseTargetId { get; set; }

    [Column("itemFormulatedApplied")]
    public bool ItemFormulatedApplied { get; set; }

    [Column("typePriceID")]
    public int TypePriceId { get; set; }

    [Column("skuCatalogItemID")]
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
}
