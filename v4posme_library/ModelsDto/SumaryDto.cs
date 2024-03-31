namespace v4posme_library.ModelsDto;

public record SumaryDto()
{
    public decimal TotalPay { get; set; }
    public decimal TotalIntest { get; set; }
    public int TotalCuotas { get; set; }
    public decimal PagoMensual { get; set; }
    public List<DetailDto>? ListDetailDto { get; set; }
}