using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_kardex")]
[Index("ItemId", Name = "IDX_KARDEX_001")]
[Index("CompanyId", Name = "IDX_KARDEX_002")]
[Index("BranchId", Name = "IDX_KARDEX_003")]
[Index("WarehouseId", Name = "IDX_KARDEX_004")]
[Index("TransactionId", Name = "IDX_KARDEX_005")]
[Index("TransactionMasterId", Name = "IDX_KARDEX_006")]
[Index("TransactionDetailId", Name = "IDX_KARDEX_007")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbKardex
{
    [Column("itemID")]
    public int ItemId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("branchID")]
    public int BranchId { get; set; }

    [Column("warehouseID")]
    public int WarehouseId { get; set; }

    [Key]
    [Column("kardexID")]
    public int KardexId { get; set; }

    [Column("kardexCode")]
    public int? KardexCode { get; set; }

    [Column("kardexDate", TypeName = "datetime")]
    public DateTime? KardexDate { get; set; }

    [Column("sign")]
    public int Sign { get; set; }

    [Column("transactionID")]
    public int? TransactionId { get; set; }

    [Column("transactionMasterID")]
    public int? TransactionMasterId { get; set; }

    [Column("transactionDetailID")]
    public int? TransactionDetailId { get; set; }

    [Column("movementOn", TypeName = "datetime")]
    public DateTime MovementOn { get; set; }

    [Column("oldQuantity")]
    [Precision(18, 4)]
    public decimal OldQuantity { get; set; }

    [Column("oldQuantityWarehouse")]
    [Precision(18, 4)]
    public decimal? OldQuantityWarehouse { get; set; }

    [Column("oldCost")]
    [Precision(18, 4)]
    public decimal OldCost { get; set; }

    [Column("oldCostWarehouse")]
    [Precision(18, 4)]
    public decimal? OldCostWarehouse { get; set; }

    [Column("transactionQuantity")]
    [Precision(18, 4)]
    public decimal TransactionQuantity { get; set; }

    [Column("transactionCost")]
    [Precision(18, 4)]
    public decimal TransactionCost { get; set; }

    [Column("newQuantity")]
    [Precision(18, 4)]
    public decimal NewQuantity { get; set; }

    [Column("newQuantityWarehouse")]
    [Precision(18, 4)]
    public decimal? NewQuantityWarehouse { get; set; }

    [Column("newCost")]
    [Precision(18, 4)]
    public decimal NewCost { get; set; }

    [Column("newCostWarehouse")]
    [Precision(18, 4)]
    public decimal? NewCostWarehouse { get; set; }

    [Column("quantityInWarehouseCurrent")]
    [Precision(19, 4)]
    public decimal QuantityInWarehouseCurrent { get; set; }

    [Column("quantityInCurrent")]
    [Precision(19, 4)]
    public decimal QuantityInCurrent { get; set; }
}
