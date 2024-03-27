namespace v4posme_library.ModelsDto;

public record TbTransactionMasterDenominationDto()
{
    public int CompanyId { get; set; }
    public int CatalogItemId { get; set; }
    public int CurrencyId { get; set; }
    public int Quantity { get; set; }
    public string? Reference1 { get; set; }
    public string? Reference2 { get; set; }
    public int IsActive { get; set; }
    public int TransactionId { get; set; }
    public int TransactionMasterDenominationId { get; set; }
    public int TransactionMasterId { get; set; }
    public int ComponentId { get; set; }
    public decimal ExchangeRate { get; set; }
    public decimal Ratio { get; set; }
    public string? DenominationName { get; set; }
}