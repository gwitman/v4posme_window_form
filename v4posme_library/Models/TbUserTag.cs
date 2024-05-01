using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_user_tag")]
[Index("TagId", Name = "IDX_USER_TAG_001")]
[Index("CompanyId", Name = "IDX_USER_TAG_002")]
[Index("BranchId", Name = "IDX_USER_TAG_003")]
[Index("UserId", Name = "IDX_USER_TAG_004")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbUserTag
{
    [Column("tagID")]
    public int TagId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("branchID")]
    public int BranchId { get; set; }

    [Column("userID")]
    public int UserId { get; set; }

    [Key]
    [Column("userTagID")]
    public int UserTagId { get; set; }
}
