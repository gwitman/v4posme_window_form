using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Keyless]
public class VwContabilidadComprobante
{
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string CodigoComprobante { get; set; } = null!;

    public DateOnly FechaComprobante { get; set; }

    [Precision(18, 8)]
    public decimal TipoCambioComprobante { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? EstadoComprobante { get; set; }

    [Precision(19, 2)]
    public decimal DebitoComprobante { get; set; }

    [Precision(19, 2)]
    public decimal CrditoComprobante { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? TipoComprobante { get; set; }

    [StringLength(5)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string MonedaComprobante { get; set; } = null!;

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? CentroCostoCuenta { get; set; }

    [StringLength(251)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string CodigoCuenta { get; set; } = null!;

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string NombreCuenta { get; set; } = null!;

    [Precision(18, 8)]
    public decimal DebitoCuenta { get; set; }

    [Precision(18, 8)]
    public decimal CreditoCuenta { get; set; }

    [StringLength(350)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string TipoCuenta { get; set; } = null!;

    [StringLength(500)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string BeneficiarioComprobante { get; set; } = null!;

    [StringLength(550)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? NotaComprobante { get; set; }
}
