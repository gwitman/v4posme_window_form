using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_item_data_sheet_detail")]
[Index("ItemDataSheetId", Name = "IDX_ITEM_DATASHEET_DETAIL_001")]
[Index("ItemId", Name = "IDX_ITEM_DATASHEET_DETAIL_002")]
[Index("RelatedItemId", Name = "IDX_ITEM_DATASHEET_DETAIL_003")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbItemDataSheetDetail
{
    [Key]
    [Column("itemDataSheetDetailID")]
    public int ItemDataSheetDetailId { get; set; }

    [Column("itemDataSheetID")]
    public int ItemDataSheetId { get; set; }

    [Column("itemID")]
    public int ItemId { get; set; }

    [Column("quantity")]
    [Precision(19, 9)]
    public decimal Quantity { get; set; }

    [Column("relatedItemID")]
    public int RelatedItemId { get; set; }

    [Column("isActive")]
    public sbyte IsActive { get; set; }
}
