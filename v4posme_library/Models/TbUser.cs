using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_user")]
[Index("CompanyId", Name = "IDX_USER_001")]
[Index("BranchId", Name = "IDX_USER_002")]
[Index("UserId", Name = "IDX_USER_003")]
[Index("EmployeeId", Name = "IDX_USER_004")]
[Index("Nickname", "Password", "IsActive", Name = "IDX_USER_005")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public partial class TbUser
{
    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("branchID")]
    public int BranchId { get; set; }

    [Key]
    [Column("userID")]
    public int UserId { get; set; }

    [Column("nickname")]
    [StringLength(250)]
    public string? Nickname { get; set; }

    [Column("password")]
    [StringLength(250)]
    public string? Password { get; set; }

    [Column("createdOn")]
    [StringLength(250)]
    public string? CreatedOn { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }

    [Column("email")]
    [StringLength(250)]
    public string Email { get; set; } = null!;

    [Column("createdBy")]
    public int CreatedBy { get; set; }

    [Column("employeeID")]
    public int EmployeeId { get; set; }

    [Column("useMobile")]
    public int UseMobile { get; set; }

    [Column("phone")]
    [StringLength(255)]
    public string? Phone { get; set; }

    [Column("lastPayment", TypeName = "datetime")]
    public DateTime? LastPayment { get; set; }

    [Column("comercio")]
    [StringLength(255)]
    public string? Comercio { get; set; }

    [Column("foto")]
    [StringLength(255)]
    public string? Foto { get; set; }

    [Column("token_google_calendar")]
    [StringLength(1200)]
    public string? TokenGoogleCalendar { get; set; }
}
