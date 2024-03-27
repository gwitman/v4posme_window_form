using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_accounting_balance")]
[Index("ComponentCycleId", Name = "IDX_ACCOUNTING_BALANCE_001")]
[Index("ComponentPeriodId", Name = "IDX_ACCOUNTING_BALANCE_002")]
[Index("CompanyId", Name = "IDX_ACCOUNTING_BALANCE_003")]
[Index("ComponentId", Name = "IDX_ACCOUNTING_BALANCE_004")]
[Index("AccountId", Name = "IDX_ACCOUNTING_BALANCE_005")]
[Index("BranchId", Name = "IDX_ACCOUNTING_BALANCE_006")]
[Index("ClassId", Name = "IDX_ACCOUNTING_BALANCE_007")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbAccountingBalance
{
    [Key]
    [Column("accountBalanceID", TypeName = "int(11)")]
    public int AccountBalanceId { get; set; }

    [Column("componentCycleID", TypeName = "int(11)")]
    public int ComponentCycleId { get; set; }

    [Column("componentPeriodID", TypeName = "int(11)")]
    public int ComponentPeriodId { get; set; }

    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("componentID", TypeName = "int(11)")]
    public int ComponentId { get; set; }

    [Column("accountID", TypeName = "int(11)")]
    public int AccountId { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int BranchId { get; set; }

    [Column("balance")]
    [Precision(18, 8)]
    public decimal? Balance { get; set; }

    [Column("debit")]
    [Precision(18, 8)]
    public decimal Debit { get; set; }

    [Column("credit")]
    [Precision(18, 8)]
    public decimal Credit { get; set; }

    [Column("classID", TypeName = "int(11)")]
    public int? ClassId { get; set; }

    [Column("isActive")]
    public bool IsActive { get; set; }
}
