using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Keyless]
public class VwGerenciaBalance
{
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? CentroCosto { get; set; }

    [StringLength(503)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string Cuenta { get; set; } = null!;

    [StringLength(4)]
    public string? Ano { get; set; }

    [StringLength(7)]
    public string? Mes { get; set; }

    [StringLength(2)]
    public string? MesOnly { get; set; }

    [Column("C$saldoInicial")]
    [Precision(18, 8)]
    public decimal? CSaldoInicial { get; set; }

    [Column("C$saldoFinal")]
    [Precision(20, 8)]
    public decimal? CSaldoFinal { get; set; }

    [Column("C$saldoMensual")]
    [Precision(19, 8)]
    public decimal CSaldoMensual { get; set; }
}
