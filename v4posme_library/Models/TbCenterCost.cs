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
public partial class TbCenterCost
{
    [Key]
    [Column("classID")]
    public int ClassId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("accountLevelID")]
    public int AccountLevelId { get; set; }

    [Column("parentAccountID")]
    public int? ParentAccountId { get; set; }

    [Column("parentClassID")]
    public int? ParentClassId { get; set; }

    [Column("number")]
    [StringLength(250)]
    public string Number { get; set; } = null!;

    [Column("description")]
    [StringLength(250)]
    public string? Description { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }

    [Column("createdBy")]
    public int? CreatedBy { get; set; }

    [Column("createdOn", TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column("createdAt")]
    public int? CreatedAt { get; set; }

    [Column("createdIn")]
    [StringLength(250)]
    public string? CreatedIn { get; set; }
}
