using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_dataview")]
[Index("CallerId", Name = "IDX_DATAVIEW_001")]
[Index("ComponentId", Name = "IDX_DATAVIEW_002")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public class TbDataview
{
    [Key]
    [Column("dataViewID", TypeName = "int(11)")]
    public int DataViewId { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string? Name { get; set; }

    [Column("description")]
    [StringLength(250)]
    public string? Description { get; set; }

    [Column("sqlScript")]
    [StringLength(5000)]
    public string? SqlScript { get; set; }

    [Column("visibleColumns")]
    [StringLength(250)]
    public string? VisibleColumns { get; set; }

    [Column("nonVisibleColumns")]
    [StringLength(250)]
    public string? NonVisibleColumns { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }

    [Column("callerID", TypeName = "int(11)")]
    public int CallerId { get; set; }

    [Column("componentID", TypeName = "int(11)")]
    public int ComponentId { get; set; }
}
