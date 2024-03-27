namespace v4posme_library.ModelsDto;

public record TbPriceDto()
{
    public int CompanyId { get; set; }
    public int ListPriceId { get; set; }
    public int ItemId { get; set; }
    public int PriceId { get; set; }
    public int TypePriceId { get; set; }
    public decimal Percentage { get; set; }
    public decimal Price { get; set; }
    public string? TipoPrice { get; set; }
    public string? ItemNumber { get; set; }
    public string? ItemName { get; set; }
    public decimal Cost { get; set; }
    public decimal PercentageCommision { get; set; }
    public string? NameTypePrice { get; set; }
}