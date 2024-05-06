using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Keyless]
public partial class VwGerenciaDesembolsosDetalle
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

    [Column("creditAmortizationID")]
    public int CreditAmortizationId { get; set; }

    public DateOnly FechaCuota { get; set; }

    [StringLength(4)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string? AnoCuota { get; set; }

    [StringLength(7)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string? Mes1Cuota { get; set; }

    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string? Mes2Cuota { get; set; }

    [Column("C$BalanceStartCuota")]
    public double? CBalanceStartCuota { get; set; }

    [Column("C$InteresCuota")]
    public double? CInteresCuota { get; set; }

    [Column("C$CapitalCuota")]
    public double? CCapitalCuota { get; set; }

    [Column("C$BalanceEndCuota")]
    public double? CBalanceEndCuota { get; set; }

    [Column("C$ShareCuota")]
    public double? CShareCuota { get; set; }

    [Column("C$RemainingCuota")]
    public double? CRemainingCuota { get; set; }

    [Column("C$shareCapital")]
    public double? CShareCapital { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? EstadoCuota { get; set; }

    [Column("diasAtrazoCuota")]
    public long? DiasAtrazoCuota { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string Moneda { get; set; } = null!;

    public double? TipoCambioActual { get; set; }

    [Column("C$CapitalPagado")]
    public double? CCapitalPagado { get; set; }

    [Column("C$CapitalPendiente")]
    public double? CCapitalPendiente { get; set; }

    [Column("C$IntaresPagado")]
    public double? CIntaresPagado { get; set; }

    [Column("C$InteresPendiente")]
    public double? CInteresPendiente { get; set; }
}
