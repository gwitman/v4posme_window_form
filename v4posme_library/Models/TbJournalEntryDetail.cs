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
public partial class TbJournalEntryDetail
{
    [Key]
    [Column("journalEntryDetailID")]
    public int JournalEntryDetailId { get; set; }

    [Column("journalEntryID")]
    public int JournalEntryId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("accountID")]
    public int AccountId { get; set; }

    [Column("isActive")]
    public bool IsActive { get; set; }

    [Column("classID")]
    public int? ClassId { get; set; }

    [Column("debit")]
    [Precision(18, 8)]
    public decimal Debit { get; set; }

    [Column("credit")]
    [Precision(18, 8)]
    public decimal Credit { get; set; }

    [Column("note")]
    [StringLength(250)]
    public string? Note { get; set; }

    [Column("isApplied")]
    public bool? IsApplied { get; set; }

    [Column("branchID")]
    public int? BranchId { get; set; }

    [Column("tb_exchange_rate")]
    [Precision(18, 8)]
    public decimal TbExchangeRate { get; set; }
}
