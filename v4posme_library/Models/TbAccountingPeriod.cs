using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_accounting_period")]
[Index("CompanyId", Name = "IDX_ACCOUNTING_PERIOD_001")]
[Index("ComponentId", Name = "IDX_ACCOUNTING_PERIOD_002")]
[Index("StatusId", Name = "IDX_ACCOUNTING_PERIOD_003")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public class TbAccountingPeriod
{
    [Key]
    [Column("componentPeriodID", TypeName = "int(11)")]
    public int ComponentPeriodId { get; set; }

    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("componentID", TypeName = "int(11)")]
    public int ComponentId { get; set; }

    [Column("number")]
    [StringLength(250)]
    public string Number { get; set; } = null!;

    [Column("name")]
    [StringLength(250)]
    public string Name { get; set; } = null!;

    [Column("description")]
    [StringLength(250)]
    public string? Description { get; set; }

    [Column("startOn", TypeName = "datetime")]
    public DateTime StartOn { get; set; }

    [Column("endOn", TypeName = "datetime")]
    public DateTime EndOn { get; set; }

    [Column("statusID", TypeName = "int(11)")]
    public int StatusId { get; set; }

    [Column("isActive")]
    public bool IsActive { get; set; }

    [Column("createdBy", TypeName = "int(11)")]
    public int? CreatedBy { get; set; }

    [Column("createdOn", TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column("createdAt", TypeName = "int(11)")]
    public int? CreatedAt { get; set; }

    [Column("createdIn")]
    [StringLength(250)]
    public string? CreatedIn { get; set; }
}
