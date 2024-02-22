using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_journal_entry_detail_summary")]
[Index("CompanyId", Name = "IDX_JOURNAL_ENTRY_DETAIL_SUMMARY_001")]
[Index("BranchId", Name = "IDX_JOURNAL_ENTRY_DETAIL_SUMMARY_002")]
[Index("LoginId", Name = "IDX_JOURNAL_ENTRY_DETAIL_SUMMARY_003")]
[Index("JournalEntryId", Name = "IDX_JOURNAL_ENTRY_DETAIL_SUMMARY_004")]
[Index("AccountId", Name = "IDX_JOURNAL_ENTRY_DETAIL_SUMMARY_005")]
[Index("ParentAccountId", Name = "IDX_JOURNAL_ENTRY_DETAIL_SUMMARY_006")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public class TbJournalEntryDetailSummary
{
    [Column("companyID", TypeName = "int(11)")]
    public int? CompanyId { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int? BranchId { get; set; }

    [Column("loginID", TypeName = "int(11)")]
    public int? LoginId { get; set; }

    [Column("tocken")]
    [StringLength(250)]
    public string? Tocken { get; set; }

    [Column("journalEntryID", TypeName = "int(11)")]
    public int? JournalEntryId { get; set; }

    [Column("accountID", TypeName = "int(11)")]
    public int? AccountId { get; set; }

    [Column("parentAccountID", TypeName = "int(11)")]
    public int? ParentAccountId { get; set; }

    [Column("debit")]
    [Precision(18, 8)]
    public decimal? Debit { get; set; }

    [Column("credit")]
    [Precision(18, 8)]
    public decimal? Credit { get; set; }

    [Key]
    [Column("journalEntryDetailSummaryID", TypeName = "int(11)")]
    public int JournalEntryDetailSummaryId { get; set; }
}
