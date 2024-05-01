using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.DataAnnotations;
using MySql.EntityFrameworkCore.DataAnnotations;

namespace v4posme_library.Models;

[Table("tb_cash_box")]
[Index("CompanyId", Name = "IDX_CASH_BOX_001")]
[Index("BranchId", Name = "IDX_CASH_BOX_002")]
[Index("CashBoxCode", Name = "IDX_CASH_BOX_003")]
[Index("StatusId", Name = "IDX_CASH_BOX_004")]
[Index("CashBoxId", Name = "IDX_CASH_BOX_005")]
[MySQLCharset("latin1")]
[Microsoft.EntityFrameworkCore.MySqlCollation("latin1_swedish_ci")]
public partial class TbCashBox
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int BranchId { get; set; }

    [Key]
    [Column("cashBoxID", TypeName = "int(11)")]
    public int CashBoxId { get; set; }

    [Column("cashBoxCode")]
    [StringLength(10)]
    public string CashBoxCode { get; set; } = null!;

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("description")]
    [StringLength(550)]
    public string? Description { get; set; }

    [Column("statusID", TypeName = "int(11)")]
    public int StatusId { get; set; }

    [Column("isActive", TypeName = "bit(1)")]
    public ulong IsActive { get; set; }
}
