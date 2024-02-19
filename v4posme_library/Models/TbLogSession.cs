using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_log_session")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public partial class TbLogSession
{
    [Key]
    [Column("session_id")]
    [StringLength(40)]
    public string SessionId { get; set; } = null!;

    [Column("ip_address")]
    [StringLength(45)]
    public string IpAddress { get; set; } = null!;

    [Column("user_agent")]
    [StringLength(120)]
    public string UserAgent { get; set; } = null!;

    [Column("last_activity", TypeName = "int(11)")]
    public int LastActivity { get; set; }

    [Column("user_data", TypeName = "text")]
    public string? UserData { get; set; }
}
