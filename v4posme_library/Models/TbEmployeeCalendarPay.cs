using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_employee_calendar_pay")]
[Index("CompanyId", Name = "IDX_EMPLOYEE_CALENDAR_PAY_001")]
[Index("AccountingCycleId", Name = "IDX_EMPLOYEE_CALENDAR_PAY_002")]
[Index("TypeId", Name = "IDX_EMPLOYEE_CALENDAR_PAY_003")]
[Index("CurrencyId", Name = "IDX_EMPLOYEE_CALENDAR_PAY_004")]
[Index("StatusId", Name = "IDX_EMPLOYEE_CALENDAR_PAY_005")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbEmployeeCalendarPay
{
    [Key]
    [Column("calendarID", TypeName = "int(11)")]
    public int CalendarId { get; set; }

    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("accountingCycleID", TypeName = "int(11)")]
    public int AccountingCycleId { get; set; }

    [Column("name")]
    [StringLength(300)]
    public string Name { get; set; } = null!;

    [Column("number")]
    [StringLength(15)]
    public string Number { get; set; } = null!;

    [Column("typeID", TypeName = "int(11)")]
    public int TypeId { get; set; }

    [Column("currencyID", TypeName = "int(11)")]
    public int CurrencyId { get; set; }

    [Column("statusID", TypeName = "int(11)")]
    public int StatusId { get; set; }

    [Column("description")]
    [StringLength(1500)]
    public string? Description { get; set; }

    [Column("createdBy", TypeName = "int(11)")]
    public int CreatedBy { get; set; }

    [Column("createdAt", TypeName = "int(11)")]
    public int CreatedAt { get; set; }

    [Column("createdOn", TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [Column("createdIn")]
    [StringLength(50)]
    public string CreatedIn { get; set; } = null!;

    [Column("isActive", TypeName = "bit(1)")]
    public ulong IsActive { get; set; }
}
