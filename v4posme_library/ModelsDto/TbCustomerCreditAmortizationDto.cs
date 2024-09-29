namespace v4posme_library.ModelsDto;

public class TbCustomerCreditAmortizationDto
{
    public int? CreditAmortizationId { get; set; }
    
    public string? DocumentNumber { get; set; }

    public DateTime FechaVencimiento { get; set; }
    
    public DateTime DateApply { get; set; }
    
    public decimal? Remaining { get; set; }
    
    public decimal? ShareCapital { get; set; }
    
    public decimal? MontoEnMora { get; set; }

    public long? Orden { get; set; }
    
    public int? Mora { get; set; }
    
    public int? NumeroCuotasPendiente { get; set; }
    
    public string? StageCuota { get; set; }
    
    public string? StageDocumento { get; set; }

    public string? CustomerNumber { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public DateOnly? BirthDate { get; set; }

    public int? CurrencyId { get; set; }
    
    public int? ReportSinRiesgo { get; set; }
}