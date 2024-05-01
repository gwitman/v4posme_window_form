using System;
using System.Collections.Generic;
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
public partial class TbItemWarehouseExpired
{
    [Key]
    [Column("itemWarehouseExpiredID")]
    public int ItemWarehouseExpiredId { get; set; }

    [Column("warehouseID")]
    public int WarehouseId { get; set; }

    [Column("itemID")]
    public int ItemId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("quantity")]
    [Precision(10, 2)]
    public decimal? Quantity { get; set; }

    [Column("lote")]
    [StringLength(50)]
    public string? Lote { get; set; }

    [Column("dateExpired", TypeName = "datetime")]
    public DateTime DateExpired { get; set; }
}
