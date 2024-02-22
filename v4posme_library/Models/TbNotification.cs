using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

/// <summary>
/// Tabla de Notificaciones
/// </summary>
[Table("tb_notification")]
[Index("ErrorId", Name = "IDX_NOTIFICATION_001")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public class TbNotification
{
    [Key]
    [Column("notificationID", TypeName = "int(11)")]
    public int NotificationId { get; set; }

    [Column("errorID", TypeName = "int(11)")]
    public int? ErrorId { get; set; }

    [Column("from")]
    [StringLength(500)]
    public string? From { get; set; }

    [Column("to")]
    [StringLength(500)]
    public string? To { get; set; }

    [Column("subject")]
    [StringLength(500)]
    public string? Subject { get; set; }

    [Column("message")]
    [StringLength(5000)]
    public string? Message { get; set; }

    [Column("summary")]
    [StringLength(500)]
    public string? Summary { get; set; }

    [Column("title")]
    [StringLength(500)]
    public string? Title { get; set; }

    [Column("tagID", TypeName = "int(11)")]
    public int? TagId { get; set; }

    [Column("createdOn", TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column("isActive", TypeName = "bit(1)")]
    public ulong? IsActive { get; set; }

    [Column("phoneFrom")]
    [StringLength(255)]
    public string? PhoneFrom { get; set; }

    [Column("phoneTo")]
    [StringLength(255)]
    public string? PhoneTo { get; set; }

    [Column("programDate")]
    [StringLength(255)]
    public string? ProgramDate { get; set; }

    [Column("programHour")]
    [StringLength(255)]
    public string? ProgramHour { get; set; }

    [Column("sendOn", TypeName = "datetime")]
    public DateTime? SendOn { get; set; }

    [Column("sendEmailOn", TypeName = "datetime")]
    public DateTime? SendEmailOn { get; set; }

    [Column("sendWhatsappOn", TypeName = "datetime")]
    public DateTime? SendWhatsappOn { get; set; }

    [Column("addedCalendarGoogle", TypeName = "bit(1)")]
    public ulong? AddedCalendarGoogle { get; set; }

    [Column("quantityOcupation", TypeName = "int(11)")]
    public int? QuantityOcupation { get; set; }

    [Column("quantityDisponible", TypeName = "int(11)")]
    public int? QuantityDisponible { get; set; }

    [Column("googleCalendarEventID")]
    [StringLength(255)]
    public string? GoogleCalendarEventId { get; set; }
}
