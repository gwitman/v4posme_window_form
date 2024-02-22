using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

/// <summary>
/// tabla para almacenar los tag de notificaciones
/// </summary>
[Table("tb_tag")]
[Index("Name", Name = "IDX_TAG_001")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public class TbTag
{
    [Key]
    [Column("tagID", TypeName = "int(11)")]
    public int TagId { get; set; }

    [Column("name")]
    [StringLength(150)]
    public string? Name { get; set; }

    [Column("description")]
    [StringLength(500)]
    public string? Description { get; set; }

    [Column("sendEmail", TypeName = "bit(1)")]
    public ulong? SendEmail { get; set; }

    [Column("sendNotificationApp", TypeName = "bit(1)")]
    public ulong? SendNotificationApp { get; set; }

    [Column("sendSMS", TypeName = "bit(1)")]
    public ulong? SendSms { get; set; }

    [Column("isActive", TypeName = "bit(1)")]
    public ulong? IsActive { get; set; }
}
