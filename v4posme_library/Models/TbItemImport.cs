using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Keyless]
[Table("tb_item_import")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbItemImport
{
    [Column("itemNumber")]
    [StringLength(255)]
    public string? ItemNumber { get; set; }

    [Column("fisico")]
    public int? Fisico { get; set; }

    [Column("sistema")]
    public int? Sistema { get; set; }
}
