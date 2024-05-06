using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Keyless]
public partial class VwSinRiesgoReporteCredito
{
    [Column("companyID")]
    public int? CompanyId { get; set; }

    [Column("customerCreditDocumentID")]
    public int? CustomerCreditDocumentId { get; set; }

    [Column("entityID")]
    public int? EntityId { get; set; }

    [Column("TIPO DE ENTIDAD")]
    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string TipoDeEntidad { get; set; } = null!;

    [Column("NUMERO CORRELATIVO")]
    [StringLength(3)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string NumeroCorrelativo { get; set; } = null!;

    [Column("FECHA DE REPORTE")]
    [StringLength(10)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string? FechaDeReporte { get; set; }

    [Column("DEPARTAMENTO")]
    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string Departamento { get; set; } = null!;

    [Column("NUMERO DE CEDULA O RUC")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string NumeroDeCedulaORuc { get; set; } = null!;

    [Column("NOMBRE DE PERSONA")]
    [StringLength(501)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? NombreDePersona { get; set; }

    [Column("TIPO DE CREDITO")]
    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string? TipoDeCredito { get; set; }

    [Column("FECHA DE DESEMBOLSO")]
    [StringLength(10)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string? FechaDeDesembolso { get; set; }

    [Column("TIPO DE OBLIGACION")]
    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string? TipoDeObligacion { get; set; }

    [Column("MONTO AUTORIZADO")]
    [Precision(11, 2)]
    public decimal? MontoAutorizado { get; set; }

    [Column("PLAZO")]
    [Precision(13, 0)]
    public decimal? Plazo { get; set; }

    [Column("FRECUENCIA DE PAGO")]
    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string FrecuenciaDePago { get; set; } = null!;

    [Column("SALDO DEUDA")]
    [Precision(11, 2)]
    public decimal? SaldoDeuda { get; set; }

    [Column("ESTADO")]
    [StringLength(3)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string? Estado { get; set; }

    [Column("MONTO VENCIDO")]
    [Precision(34, 2)]
    public decimal? MontoVencido { get; set; }

    [Column("ANTIGUEDAD DE MORA")]
    public int? AntiguedadDeMora { get; set; }

    [Column("TIPO DE GARANTIA")]
    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string? TipoDeGarantia { get; set; }

    [Column("FORMA DE RECUPERACION")]
    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string? FormaDeRecuperacion { get; set; }

    [Column("NUMERO DE CREDITO")]
    [StringLength(50)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? NumeroDeCredito { get; set; }

    [Column("VALOR DE LA CUOTA")]
    [Precision(35, 2)]
    public decimal? ValorDeLaCuota { get; set; }
}
