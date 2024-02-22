using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_role_autorization")]
[Index("CompanyId", Name = "IDX_ROLE_AUROTIZATION_001")]
[Index("ComponentAutorizationId", Name = "IDX_ROLE_AUROTIZATION_002")]
[Index("RoleId", Name = "IDX_ROLE_AUROTIZATION_003")]
[Index("BranchId", Name = "IDX_ROLE_AUROTIZATION_004")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public class TbRoleAutorization
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("componentAutorizationID", TypeName = "int(11)")]
    public int ComponentAutorizationId { get; set; }

    [Column("roleID", TypeName = "int(11)")]
    public int RoleId { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int BranchId { get; set; }

    [Key]
    [Column("roleAurotizationID", TypeName = "int(11)")]
    public int RoleAurotizationId { get; set; }
}
