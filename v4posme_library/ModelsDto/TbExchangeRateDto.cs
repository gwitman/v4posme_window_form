namespace v4posme_library.ModelsDto;

public class TbExchangeRateDto
{
    public DateOnly Date { get; set; }
    public decimal Value { get; set; }
    public double? Ratio { get; set; }
    public string? NameSource { get; set; }
    public string? SimbSource { get; set; }
    public string? NameTarget { get; set; }
    public string? SimbTarget { get; set; }
}