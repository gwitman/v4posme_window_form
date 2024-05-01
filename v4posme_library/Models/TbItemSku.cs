using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_item_sku")]
[Index("ItemId", Name = "IDX_WAREHOUSE_SKU_001")]
[Index("CatalogItemId", Name = "IDX_WAREHOUSE_SKU_002")]
public partial class TbItemSku
{
    [Key]
    [Column("skuID")]
    public int SkuId { get; set; }

    [Column("itemID")]
    public int ItemId { get; set; }

    [Column("catalogItemID")]
    public int CatalogItemId { get; set; }

    [Column("value")]
    [Precision(10, 2)]
    public decimal Value { get; set; }
}
