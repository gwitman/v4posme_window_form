using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_transaction_master_detail_credit")]
[Index("TransactionMasterId", Name = "IDX_TRANSACTION_MASTER_DETAIL_CREDIT_001")]
[Index("TransactionMasterDetailId", Name = "IDX_TRANSACTION_MASTER_DETAIL_CREDIT_002")]
[Index("CurrencyId", Name = "IDX_TRANSACTION_MASTER_DETAIL_CREDIT_003")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbTransactionMasterDetailCredit
{
    [Key]
    [Column("transactionMasterDetailCreditID")]
    public int TransactionMasterDetailCreditId { get; set; }

    [Column("transactionMasterID")]
    public int TransactionMasterId { get; set; }

    [Column("transactionMasterDetailID")]
    public int TransactionMasterDetailId { get; set; }

    [Column("capital")]
    [Precision(19, 8)]
    public decimal Capital { get; set; }

    [Column("interest")]
    [Precision(19, 8)]
    public decimal Interest { get; set; }

    [Column("dayDalay")]
    public int DayDalay { get; set; }

    [Column("interestMora")]
    [Precision(19, 8)]
    public decimal InterestMora { get; set; }

    [Column("currencyID")]
    public int CurrencyId { get; set; }

    [Column("exchangeRate")]
    [Precision(19, 8)]
    public decimal ExchangeRate { get; set; }

    [Column("reference1")]
    [StringLength(1500)]
    public string? Reference1 { get; set; }

    [Column("reference2")]
    [StringLength(100)]
    public string? Reference2 { get; set; }

    [Column("reference3")]
    [StringLength(100)]
    public string? Reference3 { get; set; }

    [Column("reference4")]
    [StringLength(100)]
    public string? Reference4 { get; set; }

    [Column("reference5")]
    [StringLength(100)]
    public string? Reference5 { get; set; }

    [Column("reference6")]
    [StringLength(100)]
    public string? Reference6 { get; set; }

    [Column("reference7")]
    [StringLength(100)]
    public string? Reference7 { get; set; }

    [Column("reference8")]
    [StringLength(100)]
    public string? Reference8 { get; set; }

    [Column("reference9")]
    [StringLength(100)]
    public string? Reference9 { get; set; }
}
