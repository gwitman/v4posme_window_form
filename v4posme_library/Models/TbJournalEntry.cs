using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_journal_entry")]
[Index("CompanyId", Name = "IDX_JOURNAL_ENTRY_001")]
[Index("JournalNumber", Name = "IDX_JOURNAL_ENTRY_002")]
[Index("StatusId", Name = "IDX_JOURNAL_ENTRY_003")]
[Index("JournalTypeId", Name = "IDX_JOURNAL_ENTRY_004")]
[Index("CurrencyId", Name = "IDX_JOURNAL_ENTRY_005")]
[Index("AccountingCycleId", Name = "IDX_JOURNAL_ENTRY_006")]
[Index("TransactionMasterId", Name = "IDX_JOURNAL_ENTRY_007")]
[Index("TransactionId", Name = "IDX_JOURNAL_ENTRY_008")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbJournalEntry
{
    [Key]
    [Column("journalEntryID")]
    public int JournalEntryId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("journalNumber")]
    [StringLength(250)]
    public string JournalNumber { get; set; } = null!;

    [Column("journalDate")]
    public DateOnly JournalDate { get; set; }

    [Column("tb_exchange_rate")]
    [Precision(18, 8)]
    public decimal TbExchangeRate { get; set; }

    [Column("createdOn", TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column("createdIn")]
    [StringLength(250)]
    public string? CreatedIn { get; set; }

    [Column("createdAt")]
    public int? CreatedAt { get; set; }

    [Column("createdBy")]
    public int? CreatedBy { get; set; }

    [Column("isActive")]
    public bool IsActive { get; set; }

    [Column("isApplied")]
    public bool IsApplied { get; set; }

    [Column("titleTemplated")]
    [StringLength(250)]
    public string TitleTemplated { get; set; } = null!;

    [Column("isTemplated")]
    public bool IsTemplated { get; set; }

    [Column("statusID")]
    public int StatusId { get; set; }

    [Column("note")]
    [StringLength(550)]
    public string? Note { get; set; }

    [Column("reference1")]
    [StringLength(250)]
    public string? Reference1 { get; set; }

    [Column("reference2")]
    [StringLength(250)]
    public string? Reference2 { get; set; }

    [Column("reference3")]
    [StringLength(250)]
    public string? Reference3 { get; set; }

    [Column("debit")]
    [Precision(19, 2)]
    public decimal Debit { get; set; }

    [Column("credit")]
    [Precision(19, 2)]
    public decimal Credit { get; set; }

    [Column("journalTypeID")]
    public int JournalTypeId { get; set; }

    [Column("currencyID")]
    public int CurrencyId { get; set; }

    [Column("accountingCycleID")]
    public int AccountingCycleId { get; set; }

    [Column("entryName")]
    [StringLength(500)]
    public string EntryName { get; set; } = null!;

    [Column("isModule", TypeName = "bit(1)")]
    public ulong IsModule { get; set; }

    [Column("transactionMasterID")]
    public int TransactionMasterId { get; set; }

    [Column("transactionID")]
    public int TransactionId { get; set; }
}
