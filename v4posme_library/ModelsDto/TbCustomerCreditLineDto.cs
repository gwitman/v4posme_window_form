namespace v4posme_library.ModelsDto;

public class TbCustomerCreditLineDto
{
    public int CustomerCreditLineId { get; set; }
    public int CompanyId { get; set; }
    public int BranchId { get; set; }
    public int EntityId { get; set; }
    public int CreditLineId { get; set; }
    public string? AccountNumber { get; set; }
    public int CurrencyId { get; set; }
    public decimal? LimitCredit { get; set; }
    public decimal? Balance { get; set; }
    public decimal? InterestYear { get; set; }
    public decimal? InterestPay { get; set; }
    public decimal? TotalPay { get; set; }
    public decimal? TotalDefeated { get; set; }
    public DateOnly? DateOpen { get; set; }
    public int PeriodPay { get; set; }
    public DateOnly? DateLastPay { get; set; }
    public int? Term { get; set; }
    public string? Note { get; set; }
    public int StatusId { get; set; }
    public ulong? IsActive { get; set; }
    public string? CurrencyName { get; set; }
    public int TypeAmortization { get; set; }
    public string? CreditLineName { get; set; }
    public string? Line { get; set; }
    public string? StatusName { get; set; }
    public string? TypeAmortizationLabel { get; set; }
    public string? PeriodPayLabel { get; set; }
}