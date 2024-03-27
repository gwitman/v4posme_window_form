using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_transaction_master_info")]
[Index("CompanyId", Name = "IDX_TRANSACTION_MASTER_INFO_001")]
[Index("TransactionId", Name = "IDX_TRANSACTION_MASTER_INFO_002")]
[Index("TransactionMasterId", Name = "IDX_TRANSACTION_MASTER_INFO_003")]
[Index("ZoneId", Name = "IDX_TRANSACTION_MASTER_INFO_004")]
[Index("RouteId", Name = "IDX_TRANSACTION_MASTER_INFO_005")]
[Index("MesaId", Name = "IDX_TRANSACTION_MASTER_INFO_006")]
[Index("ReceiptAmountBankId", Name = "IDX_TRANSACTION_MASTER_INFO_007")]
[Index("ReceiptAmountBankDolId", Name = "IDX_TRANSACTION_MASTER_INFO_008")]
[Index("ReceiptAmountCardBankId", Name = "IDX_TRANSACTION_MASTER_INFO_009")]
[Index("ReceiptAmountCardBankDolId", Name = "IDX_TRANSACTION_MASTER_INFO_010")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbTransactionMasterInfo
{
    [Key]
    [Column("transactionMasterInfoID", TypeName = "int(11)")]
    public int TransactionMasterInfoId { get; set; }

    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("transactionID", TypeName = "int(11)")]
    public int TransactionId { get; set; }

    [Column("transactionMasterID", TypeName = "int(11)")]
    public int TransactionMasterId { get; set; }

    [Column("zoneID", TypeName = "int(11)")]
    public int? ZoneId { get; set; }

    [Column("routeID", TypeName = "int(11)")]
    public int? RouteId { get; set; }

    [Column("mesaID", TypeName = "int(11)")]
    public int MesaId { get; set; }

    [Column("referenceClientName")]
    [StringLength(250)]
    public string? ReferenceClientName { get; set; }

    [Column("referenceClientIdentifier")]
    [StringLength(50)]
    public string? ReferenceClientIdentifier { get; set; }

    [Column("changeAmount")]
    [Precision(19, 9)]
    public decimal ChangeAmount { get; set; }

    [Column("receiptAmountPoint")]
    [Precision(10, 2)]
    public decimal? ReceiptAmountPoint { get; set; }

    [Column("receiptAmount")]
    [Precision(19, 9)]
    public decimal? ReceiptAmount { get; set; }

    [Column("receiptAmountDol")]
    [Precision(19, 5)]
    public decimal ReceiptAmountDol { get; set; }

    [Column("reference1")]
    [StringLength(150)]
    public string? Reference1 { get; set; }

    [Column("reference2")]
    [StringLength(150)]
    public string? Reference2 { get; set; }

    [Column("receiptAmountBank")]
    [Precision(10, 2)]
    public decimal ReceiptAmountBank { get; set; }

    [Column("receiptAmountBankID", TypeName = "int(11)")]
    public int? ReceiptAmountBankId { get; set; }

    [Column("receiptAmountBankReference")]
    [StringLength(255)]
    public string? ReceiptAmountBankReference { get; set; }

    [Column("receiptAmountBankDol")]
    [Precision(10, 0)]
    public decimal ReceiptAmountBankDol { get; set; }

    [Column("receiptAmountBankDolID", TypeName = "int(11)")]
    public int? ReceiptAmountBankDolId { get; set; }

    [Column("receiptAmountBankDolReference")]
    [StringLength(255)]
    public string? ReceiptAmountBankDolReference { get; set; }

    [Column("receiptAmountCard")]
    [Precision(10, 0)]
    public decimal ReceiptAmountCard { get; set; }

    [Column("receiptAmountCardBankID", TypeName = "int(11)")]
    public int? ReceiptAmountCardBankId { get; set; }

    [Column("receiptAmountCardBankReference")]
    [StringLength(255)]
    public string? ReceiptAmountCardBankReference { get; set; }

    [Column("receiptAmountCardDol")]
    [Precision(10, 0)]
    public decimal ReceiptAmountCardDol { get; set; }

    [Column("receiptAmountCardBankDolID", TypeName = "int(11)")]
    public int? ReceiptAmountCardBankDolId { get; set; }

    [Column("receiptAmountCardBankDolReference")]
    [StringLength(255)]
    public string? ReceiptAmountCardBankDolReference { get; set; }
}
