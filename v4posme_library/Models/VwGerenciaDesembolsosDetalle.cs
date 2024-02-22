using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Keyless]
public class VwGerenciaDesembolsosDetalle
{
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? Colaborador { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? NombreColaborador { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string Cliente { get; set; } = null!;

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? NombreCliente { get; set; }

    [StringLength(50)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string Factura { get; set; } = null!;

    [Column("creditAmortizationID", TypeName = "int(11)")]
    public int CreditAmortizationId { get; set; }

    public DateOnly FechaCuota { get; set; }

    [StringLength(4)]
    public string? AnoCuota { get; set; }

    [StringLength(7)]
    public string? Mes1Cuota { get; set; }

    [StringLength(2)]
    public string? Mes2Cuota { get; set; }

    [Column("C$BalanceStartCuota", TypeName = "double(19,2)")]
    public double? CBalanceStartCuota { get; set; }

    [Column("C$InteresCuota", TypeName = "double(19,2)")]
    public double? CInteresCuota { get; set; }

    [Column("C$CapitalCuota", TypeName = "double(19,2)")]
    public double? CCapitalCuota { get; set; }

    [Column("C$BalanceEndCuota", TypeName = "double(19,2)")]
    public double? CBalanceEndCuota { get; set; }

    [Column("C$ShareCuota", TypeName = "double(19,2)")]
    public double? CShareCuota { get; set; }

    [Column("C$RemainingCuota", TypeName = "double(19,2)")]
    public double? CRemainingCuota { get; set; }

    [Column("C$shareCapital", TypeName = "double(19,2)")]
    public double? CShareCapital { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? EstadoCuota { get; set; }

    [Column("diasAtrazoCuota", TypeName = "bigint(11)")]
    public long? DiasAtrazoCuota { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string Moneda { get; set; } = null!;

    public double? TipoCambioActual { get; set; }

    [Column("C$CapitalPagado", TypeName = "double(19,2)")]
    public double? CCapitalPagado { get; set; }

    [Column("C$CapitalPendiente", TypeName = "double(19,2)")]
    public double? CCapitalPendiente { get; set; }

    [Column("C$IntaresPagado", TypeName = "double(19,2)")]
    public double? CIntaresPagado { get; set; }

    [Column("C$InteresPendiente", TypeName = "double(19,2)")]
    public double? CInteresPendiente { get; set; }
}
