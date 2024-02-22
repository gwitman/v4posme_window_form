using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_estadistica_clases")]
[Index("CompanyId", Name = "IDX_ESTADISTICA_CLASES_001")]
[Index("ClaseId", Name = "IDX_ESTADISTICA_CLASES_002")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public class TbEstadisticaClas
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Key]
    [Column("claseID", TypeName = "int(11)")]
    public int ClaseId { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string Name { get; set; } = null!;

    [Column("isActive", TypeName = "bit(1)")]
    public ulong IsActive { get; set; }
}
