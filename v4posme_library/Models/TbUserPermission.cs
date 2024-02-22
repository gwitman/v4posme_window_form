using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_user_permission")]
[Index("CompanyId", Name = "IDX_USER_PERMISSION_001")]
[Index("BranchId", Name = "IDX_USER_PERMISSION_002")]
[Index("ElementId", Name = "IDX_USER_PERMISSION_003")]
[Index("RoleId", Name = "IDX_USER_PERMISSION_004")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public class TbUserPermission
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int BranchId { get; set; }

    [Column("elementID", TypeName = "int(11)")]
    public int ElementId { get; set; }

    [Column("roleID", TypeName = "int(11)")]
    public int RoleId { get; set; }

    [Key]
    [Column("userPermissionID", TypeName = "int(11)")]
    public int UserPermissionId { get; set; }

    [Column("selected")]
    public bool? Selected { get; set; }

    [Column("inserted")]
    public bool? Inserted { get; set; }

    [Column("deleted")]
    public bool? Deleted { get; set; }

    [Column("edited")]
    public bool? Edited { get; set; }
}
