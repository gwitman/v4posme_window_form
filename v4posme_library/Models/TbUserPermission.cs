using System;
using System.Collections.Generic;
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
public partial class TbUserPermission
{
    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("branchID")]
    public int BranchId { get; set; }

    [Column("elementID")]
    public int ElementId { get; set; }

    [Column("roleID")]
    public int RoleId { get; set; }

    [Key]
    [Column("userPermissionID")]
    public int UserPermissionId { get; set; }

    [Column("selected")]
    public int? Selected { get; set; }

    [Column("inserted")]
    public int? Inserted { get; set; }

    [Column("deleted")]
    public int? Deleted { get; set; }

    [Column("edited")]
    public int? Edited { get; set; }
}
