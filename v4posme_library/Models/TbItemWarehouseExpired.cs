using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_item_warehouse_expired")]
[Index("WarehouseId", Name = "IDX_ITEM_WAREHOUSE_EXPIRED_001")]
[Index("ItemId", Name = "IDX_ITEM_WAREHOUSE_EXPIRED_002")]
[Index("CompanyId", Name = "IDX_ITEM_WAREHOUSE_EXPIRED_003")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public class TbItemWarehouseExpired
{
    [Key]
    [Column("itemWarehouseExpiredID", TypeName = "int(11)")]
    public int ItemWarehouseExpiredId { get; set; }

    [Column("warehouseID", TypeName = "int(11)")]
    public int WarehouseId { get; set; }

    [Column("itemID", TypeName = "int(11)")]
    public int ItemId { get; set; }

    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("quantity")]
    [Precision(10, 2)]
    public decimal? Quantity { get; set; }

    [Column("lote")] [StringLength(50)] public string? Lote { get; set; }

    [Column("dateExpired", TypeName = "datetime")]
    public DateTime DateExpired { get; set; }

    [NotMapped] public string? ItemNumber { get; set; }
    [NotMapped] public string? ItemName { get; set; }
    [NotMapped] public int WarehouseNumber { get; set; }
    [NotMapped] public string? WarehouseName { get; set; }
}