using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_item_warehouse")]
[Index("CompanyId", Name = "IDX_ITEM_WAREHOUSE_001")]
[Index("BranchId", Name = "IDX_ITEM_WAREHOUSE_002")]
[Index("WarehouseId", Name = "IDX_ITEM_WAREHOUSE_003")]
[Index("ItemId", Name = "IDX_ITEM_WAREHOUSE_004")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public class TbItemWarehouse
{
    [Key]
    [Column("itemWarehouseId", TypeName = "int(11)")]
    public int ItemWarehouseId { get; set; }

    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int BranchId { get; set; }

    [Column("warehouseID", TypeName = "int(11)")]
    public int WarehouseId { get; set; }

    [Column("itemID", TypeName = "int(11)")]
    public int ItemId { get; set; }

    [Column("quantity")]
    [Precision(18, 4)]
    public decimal Quantity { get; set; }

    [Column("cost")]
    [Precision(18, 4)]
    public decimal Cost { get; set; }

    [Column("quantityMax")]
    [Precision(18, 4)]
    public decimal QuantityMax { get; set; }

    [Column("quantityMin")]
    [Precision(18, 4)]
    public decimal QuantityMin { get; set; }
}
