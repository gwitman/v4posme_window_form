namespace v4posme_library.ModelsDto;

public record TableAmortizationDto(decimal? TotalPay, decimal? TotalIntest, decimal? TotalCuotas, decimal? PagoMensual,List<TableAmortizationDetailDto>? ListDetailDto);