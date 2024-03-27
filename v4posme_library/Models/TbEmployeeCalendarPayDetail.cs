using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_employee_calendar_pay_detail")]
[Index("EmployeeId", Name = "IDX_EMPLOYEE_CALENDAR_PAY_DETAIL_001")]
[Index("CalendarId", Name = "IDX_EMPLOYEE_CALENDAR_PAY_DETAIL_002")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbEmployeeCalendarPayDetail
{
    [Key]
    [Column("calendarDetailID", TypeName = "int(11)")]
    public int CalendarDetailId { get; set; }

    [Column("calendarID", TypeName = "int(11)")]
    public int CalendarId { get; set; }

    [Column("employeeID", TypeName = "int(11)")]
    public int EmployeeId { get; set; }

    [Column("salary")]
    [Precision(18, 2)]
    public decimal Salary { get; set; }

    [Column("commission")]
    [Precision(18, 2)]
    public decimal Commission { get; set; }

    [Column("adelantos")]
    [Precision(18, 2)]
    public decimal Adelantos { get; set; }

    [Column("neto")]
    [Precision(18, 2)]
    public decimal Neto { get; set; }

    [Column("isActive", TypeName = "bit(1)")]
    public ulong IsActive { get; set; }
}
