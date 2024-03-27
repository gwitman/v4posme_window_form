namespace v4posme_library.ModelsDto;

public class TbCustomerCreditDocumentDto
{
    public int? CustomerCreditDocumentId { get; set; }
    public int CompanyId { get; set; }
    public int EntityId { get; set; }
    public int CustomerCreditLineId { get; set; }
    public string? DocumentNumber { get; set; }
    public DateOnly? DateOn { get; set; }
    public decimal? Amount { get; set; }
    public decimal? Interes { get; set; }
    public int Term { get; set; }
    public decimal? ExchangeRate { get; set; }
    public string? Reference1 { get; set; }
    public string? Reference2 { get; set; }
    public string? Reference3 { get; set; }
    public int StatusId { get; set; }
    public ulong? IsActive { get; set; }
    public decimal? Balance { get; set; }
    public decimal? BalanceProvicioned { get; set; }
    public int CurrencyId { get; set; }
    public string? CurrencyName { get; set; }
    public string? CurrencySymbol { get; set; }
    public decimal? BalanceNew { get; set; }
    public int ReportSinRiesgo { get; set; }
    public decimal? Remaining { get; set; }
    public DateTime? DateFinish { get; set; }
    public int CreditAmortizationId { get; set; }
    public DateTime DateApply { get; set; }
    public int StatusAmotization { get; set; }
    public string? StatusAmortizatonName { get; set; }
}