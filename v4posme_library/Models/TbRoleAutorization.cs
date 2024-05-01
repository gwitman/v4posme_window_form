using System;
using System.Collections.Generic;
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
public partial class TbRoleAutorization
{
    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("componentAutorizationID")]
    public int ComponentAutorizationId { get; set; }

    [Column("roleID")]
    public int RoleId { get; set; }

    [Column("branchID")]
    public int BranchId { get; set; }

    [Key]
    [Column("roleAurotizationID")]
    public int RoleAurotizationId { get; set; }
}
