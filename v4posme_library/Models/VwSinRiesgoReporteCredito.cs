using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Keyless]
public class VwSinRiesgoReporteCredito
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("customerCreditDocumentID", TypeName = "int(11)")]
    public int CustomerCreditDocumentId { get; set; }

    [Column("entityID", TypeName = "int(11)")]
    public int EntityId { get; set; }

    [Column("TIPO DE ENTIDAD")]
    [StringLength(2)]
    public string TipoDeEntidad { get; set; } = null!;

    [Column("NUMERO CORRELATIVO")]
    [StringLength(3)]
    public string NumeroCorrelativo { get; set; } = null!;

    [Column("FECHA DE REPORTE")]
    [StringLength(10)]
    public string? FechaDeReporte { get; set; }

    [Column("DEPARTAMENTO")]
    [StringLength(2)]
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
    public string? TipoDeCredito { get; set; }

    [Column("FECHA DE DESEMBOLSO")]
    [StringLength(10)]
    public string? FechaDeDesembolso { get; set; }

    [Column("TIPO DE OBLIGACION")]
    [StringLength(2)]
    public string? TipoDeObligacion { get; set; }

    [Column("MONTO AUTORIZADO")]
    [Precision(11, 2)]
    public decimal? MontoAutorizado { get; set; }

    [Column("PLAZO")]
    [Precision(13, 0)]
    public decimal? Plazo { get; set; }

    [Column("FRECUENCIA DE PAGO")]
    [StringLength(2)]
    public string FrecuenciaDePago { get; set; } = null!;

    [Column("SALDO DEUDA")]
    [Precision(11, 2)]
    public decimal? SaldoDeuda { get; set; }

    [Column("ESTADO")]
    [StringLength(3)]
    public string? Estado { get; set; }

    [Column("MONTO VENCIDO")]
    [Precision(34, 2)]
    public decimal? MontoVencido { get; set; }

    [Column("ANTIGUEDAD DE MORA", TypeName = "int(7)")]
    public int? AntiguedadDeMora { get; set; }

    [Column("TIPO DE GARANTIA")]
    [StringLength(2)]
    public string? TipoDeGarantia { get; set; }

    [Column("FORMA DE RECUPERACION")]
    [StringLength(2)]
    public string? FormaDeRecuperacion { get; set; }

    [Column("NUMERO DE CREDITO")]
    [StringLength(50)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string NumeroDeCredito { get; set; } = null!;

    [Column("VALOR DE LA CUOTA")]
    [Precision(35, 2)]
    public decimal? ValorDeLaCuota { get; set; }
}
