using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_transaction_master_summary_concept_tmp")]
[Index("CompanyId", "BranchId", "LoginId", "TransactionId", Name = "IDX_1")]
[Index("CompanyId", "BranchId", "LoginId", "TransactionId", "TransactionMasterId", "TransactionMasterCausalId", Name = "IDX_2")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public class TbTransactionMasterSummaryConceptTmp
{
    [Key]
    [Column("ID", TypeName = "int(11)")]
    public int Id { get; set; }

    [Column("companyID", TypeName = "int(11)")]
    public int? CompanyId { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int? BranchId { get; set; }

    [Column("loginID", TypeName = "int(11)")]
    public int? LoginId { get; set; }

    [Column("transactionID", TypeName = "int(11)")]
    public int? TransactionId { get; set; }

    [Column("transactionMasterID", TypeName = "int(11)")]
    public int? TransactionMasterId { get; set; }

    [Column("transactionMasterCausalID", TypeName = "int(11)")]
    public int? TransactionMasterCausalId { get; set; }

    [Column("journalEntryID", TypeName = "int(11)")]
    public int? JournalEntryId { get; set; }

    [Column("currencyID", TypeName = "int(11)")]
    public int? CurrencyId { get; set; }

    [Column("transactionNumber")]
    [StringLength(250)]
    public string? TransactionNumber { get; set; }

    [Column("transactionDate", TypeName = "datetime")]
    public DateTime? TransactionDate { get; set; }

    [Column("exchangeRate")]
    [Precision(26, 8)]
    public decimal? ExchangeRate { get; set; }

    [Column("conceptID", TypeName = "int(11)")]
    public int? ConceptId { get; set; }

    [Column("value")]
    [Precision(26, 8)]
    public decimal? Value { get; set; }

    [Column("reference1")]
    [StringLength(250)]
    public string? Reference1 { get; set; }

    [Column("reference2")]
    [StringLength(250)]
    public string? Reference2 { get; set; }

    [Column("reference3")]
    [StringLength(250)]
    public string? Reference3 { get; set; }
}
