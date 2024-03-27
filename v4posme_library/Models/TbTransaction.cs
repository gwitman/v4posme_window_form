using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_transaction")]
[Index("CompanyId", Name = "IDX_TRANSACTION_001")]
[Index("WorkflowId", Name = "IDX_TRANSACTION_002")]
[Index("JournalTypeId", Name = "IDX_TRANSACTION_003")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbTransaction
{
    [Key]
    [Column("transactionID", TypeName = "int(11)")]
    public int TransactionId { get; set; }

    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string Name { get; set; } = null!;

    [Column("description")]
    [StringLength(250)]
    public string? Description { get; set; }

    [Column("workflowID", TypeName = "int(11)")]
    public int? WorkflowId { get; set; }

    [Column("isCountable")]
    public bool? IsCountable { get; set; }

    [Column("reference1")]
    [StringLength(250)]
    public string? Reference1 { get; set; }

    [Column("reference2")]
    [StringLength(250)]
    public string? Reference2 { get; set; }

    [Column("reference3")]
    [StringLength(250)]
    public string? Reference3 { get; set; }

    [Column("generateTransactionNumber")]
    public bool? GenerateTransactionNumber { get; set; }

    [Column("decimalPlaces", TypeName = "int(11)")]
    public int? DecimalPlaces { get; set; }

    [Column("journalTypeID", TypeName = "int(11)")]
    public int? JournalTypeId { get; set; }

    [Column("signInventory", TypeName = "int(11)")]
    public int? SignInventory { get; set; }

    [Column("isRevert")]
    public bool? IsRevert { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }
}
