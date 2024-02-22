using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_role")]
[Index("CompanyId", Name = "IDX_ROLE_001")]
[Index("BranchId", Name = "IDX_ROLE_002")]
[Index("RoleId", "CompanyId", "BranchId", "IsActive", Name = "IDX_ROLE_003")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public class TbRole
{
    [Key]
    [Column("roleID", TypeName = "int(11)")]
    public int RoleId { get; set; }

    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int BranchId { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string? Name { get; set; }

    [Column("description")]
    [StringLength(250)]
    public string? Description { get; set; }

    [Column("isAdmin")]
    public bool? IsAdmin { get; set; }

    [Column("createdOn", TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column("urlDefault")]
    [StringLength(250)]
    public string UrlDefault { get; set; } = null!;

    [Column("isActive")]
    public bool? IsActive { get; set; }

    [Column("createdBy", TypeName = "int(11)")]
    public int CreatedBy { get; set; }
}
