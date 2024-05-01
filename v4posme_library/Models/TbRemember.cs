using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_remember")]
[Index("CompanyId", Name = "IDX_REMEMBER_001")]
[Index("StatusId", Name = "IDX_REMEMBER_002")]
[Index("Period", Name = "IDX_REMEMBER_003")]
[Index("RememberId", Name = "IDX_REMEMBER_004")]
[Index("TagId", Name = "IDX_REMEMBER_005")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbRemember
{
    [Key]
    [Column("rememberID")]
    public int RememberId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("title")]
    [StringLength(250)]
    public string Title { get; set; } = null!;

    [Column("description")]
    [StringLength(500)]
    public string Description { get; set; } = null!;

    [Column("period")]
    public int Period { get; set; }

    [Column("day")]
    public int Day { get; set; }

    [Column("statusID")]
    public int StatusId { get; set; }

    [Column("lastNotificationOn", TypeName = "datetime")]
    public DateTime? LastNotificationOn { get; set; }

    [Column("isTemporal", TypeName = "bit(1)")]
    public ulong IsTemporal { get; set; }

    [Column("createdBy")]
    public int CreatedBy { get; set; }

    [Column("createdOn", TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [Column("createdIn")]
    [StringLength(50)]
    public string CreatedIn { get; set; } = null!;

    [Column("createdAt")]
    public int CreatedAt { get; set; }

    [Column("isActive", TypeName = "bit(1)")]
    public ulong IsActive { get; set; }

    [Column("tagID")]
    public int? TagId { get; set; }

    [Column("leerFile")]
    public int? LeerFile { get; set; }
}
