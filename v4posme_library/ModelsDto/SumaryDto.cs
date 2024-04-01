namespace v4posme_library.ModelsDto;

public record SumaryDto(decimal? TotalPay, decimal? TotalIntest, decimal? TotalCuotas, decimal? PagoMensual,List<DetailDto>? ListDetailDto);