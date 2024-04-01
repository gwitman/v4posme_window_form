namespace v4posme_library.ModelsDto;

public record TableAmortizationDetailDto(int Pnum,DateTime? Date,decimal? Principal,decimal? Interes,decimal? Cuota,decimal? Saldo,decimal? SaldoInicial,int Cpmnt);