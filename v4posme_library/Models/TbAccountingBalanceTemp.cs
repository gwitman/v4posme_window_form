﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_accounting_balance_temp")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbAccountingBalanceTemp
{
    [Key]
    [Column("accountingBalanceTempID")]
    public int AccountingBalanceTempId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("branchID")]
    public int BranchId { get; set; }

    [Column("loginID")]
    public int LoginId { get; set; }

    [Column("tocken")]
    [StringLength(500)]
    public string? Tocken { get; set; }

    [Column("accountID")]
    public int AccountId { get; set; }

    [Column("parentAccountID")]
    public int? ParentAccountId { get; set; }

    [Column("accountNumber")]
    [StringLength(250)]
    public string AccountNumber { get; set; } = null!;

    [Column("name")]
    [StringLength(250)]
    public string? Name { get; set; }

    [Column("isOperative", TypeName = "bit(18)")]
    public ulong IsOperative { get; set; }

    [Column("statusID")]
    public int StatusId { get; set; }

    [Column("accountTypeID")]
    public int AccountTypeId { get; set; }

    [Column("naturaleza")]
    [StringLength(1)]
    public string Naturaleza { get; set; } = null!;

    [Column("balanceStart")]
    [Precision(18, 8)]
    public decimal BalanceStart { get; set; }

    [Column("debit")]
    [Precision(18, 8)]
    public decimal Debit { get; set; }

    [Column("credit")]
    [Precision(18, 8)]
    public decimal Credit { get; set; }

    [Column("balanceEnd")]
    [Precision(18, 8)]
    public decimal BalanceEnd { get; set; }
}
