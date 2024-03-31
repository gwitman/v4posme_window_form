namespace v4posme_library.ModelsDto;

public record DetailDto()
{
    public int Pnum { get; set; }
    public DateTime Date { get; set; }
    public decimal Principal { get; set; }
    public decimal Interes { get; set; }
    public decimal Cuota { get; set; }
    public decimal Saldo { get; set; }
    public decimal SaldoInicial { get; set; }
    public int Cpmnt { get; set; }
}