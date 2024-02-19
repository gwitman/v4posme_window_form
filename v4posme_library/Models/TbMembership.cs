using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_membership")]
[Index("RoleId", Name = "IDX_MEMBERSHIP_001")]
[Index("CompanyId", Name = "IDX_MEMBERSHIP_002")]
[Index("BranchId", Name = "IDX_MEMBERSHIP_003")]
[Index("UserId", Name = "IDX_MEMBERSHIP_004")]
[Index("CompanyId", "BranchId", "UserId", Name = "IDX_MEMBERSHIP_005")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public partial class TbMembership
{
    [Key]
    [Column("membershipID", TypeName = "int(11)")]
    public int MembershipId { get; set; }

    [Column("roleID", TypeName = "int(11)")]
    public int RoleId { get; set; }

    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int BranchId { get; set; }

    [Column("userID", TypeName = "int(11)")]
    public int UserId { get; set; }
}
