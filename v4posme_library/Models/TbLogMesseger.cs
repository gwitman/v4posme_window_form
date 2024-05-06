using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_log_messeger")]
[MySqlCharSet("utf8mb3")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class TbLogMesseger
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("errno")]
    public int Errno { get; set; }

    [Column("errtype")]
    [StringLength(32)]
    public string Errtype { get; set; } = null!;

    [Column("errstr", TypeName = "text")]
    public string Errstr { get; set; } = null!;

    [Column("errfile")]
    [StringLength(255)]
    public string Errfile { get; set; } = null!;

    [Column("errline")]
    public int Errline { get; set; }

    [Column("user_agent")]
    [StringLength(450)]
    public string UserAgent { get; set; } = null!;

    [Column("ip_address")]
    [StringLength(45)]
    public string IpAddress { get; set; } = null!;

    [Column("time", TypeName = "datetime")]
    public DateTime Time { get; set; }
}
