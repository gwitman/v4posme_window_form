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
public class TbItemDataSheetDetail
{
    [Key]
    [Column("itemDataSheetDetailID", TypeName = "int(11)")]
    public int ItemDataSheetDetailId { get; set; }

    [Column("itemDataSheetID", TypeName = "int(11)")]
    public int ItemDataSheetId { get; set; }

    [Column("itemID", TypeName = "int(11)")]
    public int ItemId { get; set; }

    [Column("quantity")]
    [Precision(19, 9)]
    public decimal Quantity { get; set; }

    [Column("relatedItemID", TypeName = "int(11)")]
    public int RelatedItemId { get; set; }

    [Column("isActive", TypeName = "tinyint(4)")]
    public sbyte IsActive { get; set; }

    [NotMapped] public string? ItemNumber { get; set; }
    [NotMapped] public string? Name { get; set; }
}