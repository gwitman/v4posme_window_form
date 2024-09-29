namespace v4posme_library.ModelsDto;

public record TbJournalEntryDto()
{
    public int JournalEntryId { get; set; }
    public int CompanyId { get; set; }
    public string? JournalNumber { get; set; }
    public string? EntryName { get; set; }
    public DateOnly JournalDate { get; set; }
    public decimal TbExchangeRate { get; set; }
    public DateTime? CreatedOn { get; set; }
    public string? CreatedIn { get; set; }
    public int? CreatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public bool IsActive { get; set; }
    public bool IsApplied { get; set; }
    public int StatusId { get; set; }
    public string? Note { get; set; }
    public string? Reference1 { get; set; }
    public string? Reference2 { get; set; }
    public string? Reference3 { get; set; }
    public int JournalTypeId { get; set; }
    public int CurrencyId { get; set; }
    public int AccountingCycleId { get; set; }
    public string? WorkflowStageName { get; set; }
    public string? JournalTypeName { get; set; }
    public string? CurrencyName { get; set; }
    public bool IsModule { get; set; }
    public int TransactionMasterId { get; set; }
    public bool IsTemplated { get; set; }
    public string? TitleTemplated { get; set; }
}