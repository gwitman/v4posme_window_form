namespace v4posme_library.ModelsDto;

public class TbJournalEntryDetailDto
{
    public int JournalEntryDetailId { get; set; }
    public int JournalEntryId { get; set; }
    public int CompanyId { get; set; }
    public int AccountId { get; set; }
    public bool IsActive { get; set; }
    public int? ClassId { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public string? Note { get; set; }
    public bool? IsApplied { get; set; }
    public int? BranchId { get; set; }
    public decimal TbExchangeRate { get; set; }
    public string? ClassNumber { get; set; }
    public string? AccountNumber { get; set; }
    public string? AccountName { get; set; }
}