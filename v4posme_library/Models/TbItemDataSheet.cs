using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_item_data_sheet")]
[Index("ItemId", Name = "IDX_ITEM_DATASHEET_001")]
[Index("StatusId", Name = "IDX_ITEM_DATASHEET_002")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbItemDataSheet
{
    [Key]
    [Column("itemDataSheetID")]
    public int ItemDataSheetId { get; set; }

    [Column("itemID")]
    public int ItemId { get; set; }

    [Column("version")]
    public int Version { get; set; }

    [Column("statusID")]
    public int StatusId { get; set; }

    [Column("name")]
    [StringLength(150)]
    public string Name { get; set; } = null!;

    [Column("description")]
    [StringLength(500)]
    public string? Description { get; set; }

    [Column("createdOn", TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [Column("createdBy")]
    public int CreatedBy { get; set; }

    [Column("createdIn")]
    [StringLength(25)]
    public string CreatedIn { get; set; } = null!;

    [Column("createdAt")]
    public int CreatedAt { get; set; }

    [Column("isActive", TypeName = "bit(1)")]
    public ulong IsActive { get; set; }
}
