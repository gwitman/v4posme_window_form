using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_center_cost")]
[Index("ClassId", Name = "IDX_CENTER_COST_001")]
[Index("CompanyId", Name = "IDX_CENTER_COST_002")]
[Index("AccountLevelId", Name = "IDX_CENTER_COST_003")]
[Index("ParentAccountId", Name = "IDX_CENTER_COST_004")]
[Index("ParentClassId", Name = "IDX_CENTER_COST_005")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public class TbCenterCost
{
    [Key]
    [Column("classID", TypeName = "int(11)")]
    public int ClassId { get; set; }

    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("accountLevelID", TypeName = "int(11)")]
    public int AccountLevelId { get; set; }

    [Column("parentAccountID", TypeName = "int(11)")]
    public int? ParentAccountId { get; set; }

    [Column("parentClassID", TypeName = "int(11)")]
    public int? ParentClassId { get; set; }

    [Column("number")]
    [StringLength(250)]
    public string Number { get; set; } = null!;

    [Column("description")]
    [StringLength(250)]
    public string? Description { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }

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
