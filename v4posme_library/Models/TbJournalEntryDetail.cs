using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_journal_entry_detail")]
[Index("JournalEntryId", Name = "IDX_JOURNAL_ENTRY_DETAIL_001")]
[Index("CompanyId", Name = "IDX_JOURNAL_ENTRY_DETAIL_002")]
[Index("AccountId", Name = "IDX_JOURNAL_ENTRY_DETAIL_003")]
[Index("ClassId", Name = "IDX_JOURNAL_ENTRY_DETAIL_004")]
[Index("BranchId", Name = "IDX_JOURNAL_ENTRY_DETAIL_005")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public class TbJournalEntryDetail
{
    [Key]
    [Column("journalEntryDetailID", TypeName = "int(11)")]
    public int JournalEntryDetailId { get; set; }

    [Column("journalEntryID", TypeName = "int(11)")]
    public int JournalEntryId { get; set; }

    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("accountID", TypeName = "int(11)")]
    public int AccountId { get; set; }

    [Column("isActive")] public bool IsActive { get; set; }

    [Column("classID", TypeName = "int(11)")]
    public int? ClassId { get; set; }

    [Column("debit")] [Precision(18, 8)] public decimal Debit { get; set; }

    [Column("credit")] [Precision(18, 8)] public decimal Credit { get; set; }

    [Column("note")] [StringLength(250)] public string? Note { get; set; }

    [Column("isApplied")] public bool? IsApplied { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int? BranchId { get; set; }

    [Column("tb_exchange_rate")]
    [Precision(18, 8)]
    public decimal TbExchangeRate { get; set; }

    [NotMapped] public string? ClassNumber { get; set; }
    [NotMapped] public string? AccountNumber { get; set; }
    [NotMapped] public string? AccountName { get; set; }
}