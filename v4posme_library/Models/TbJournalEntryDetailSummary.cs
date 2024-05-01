using System;
using System.Collections.Generic;
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
public partial class TbJournalEntryDetailSummary
{
    [Column("companyID")]
    public int? CompanyId { get; set; }

    [Column("branchID")]
    public int? BranchId { get; set; }

    [Column("loginID")]
    public int? LoginId { get; set; }

    [Column("tocken")]
    [StringLength(250)]
    public string? Tocken { get; set; }

    [Column("journalEntryID")]
    public int? JournalEntryId { get; set; }

    [Column("accountID")]
    public int? AccountId { get; set; }

    [Column("parentAccountID")]
    public int? ParentAccountId { get; set; }

    [Column("debit")]
    [Precision(18, 8)]
    public decimal? Debit { get; set; }

    [Column("credit")]
    [Precision(18, 8)]
    public decimal? Credit { get; set; }

    [Key]
    [Column("journalEntryDetailSummaryID")]
    public int JournalEntryDetailSummaryId { get; set; }
}
