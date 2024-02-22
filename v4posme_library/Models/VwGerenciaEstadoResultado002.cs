using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Keyless]
public class VwGerenciaEstadoResultado002
{
    [StringLength(4)]
    public string? Ano { get; set; }

    [StringLength(7)]
    public string? Mes { get; set; }

    [StringLength(2)]
    public string? MesOnly { get; set; }

    [Column("C$saldoInicial")]
    [Precision(41, 8)]
    public decimal? CSaldoInicial { get; set; }

    [Column("C$saldoFinal")]
    [Precision(43, 8)]
    public decimal? CSaldoFinal { get; set; }

    [Column("C$saldoMensual")]
    [Precision(42, 8)]
    public decimal? CSaldoMensual { get; set; }
}
