using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_error")]
[Index("TagId", Name = "IDX_ERROR_002")]
[Index("IsActive", "IsRead", "UserId", Name = "IDX_ERROR_003")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbError
{
    [Key]
    [Column("errorID")]
    public int ErrorId { get; set; }

    [Column("tagID")]
    public int? TagId { get; set; }

    [Column("notificated")]
    [StringLength(50)]
    public string? Notificated { get; set; }

    [Column("message")]
    [StringLength(500)]
    public string? Message { get; set; }

    [Column("isActive")]
    public sbyte? IsActive { get; set; }

    [Column("isRead")]
    public sbyte? IsRead { get; set; }

    [Column("userID")]
    public int? UserId { get; set; }

    [Column("createdOn", TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column("readOn", TypeName = "datetime")]
    public DateTime? ReadOn { get; set; }
}
