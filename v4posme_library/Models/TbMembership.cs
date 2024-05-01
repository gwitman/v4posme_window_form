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
    [Column("membershipID")]
    public int MembershipId { get; set; }

    [Column("roleID")]
    public int RoleId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("branchID")]
    public int BranchId { get; set; }

    [Column("userID")]
    public int UserId { get; set; }
}
