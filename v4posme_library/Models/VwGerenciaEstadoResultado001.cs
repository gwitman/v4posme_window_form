using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Keyless]
public partial class VwGerenciaEstadoResultado001
{
    [StringLength(503)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string Cuenta { get; set; } = null!;

    [StringLength(4)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string? Ano { get; set; }

    [StringLength(7)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string? Mes { get; set; }

    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string? MesOnly { get; set; }

    [Column("C$saldoInicial")]
    [Precision(19, 8)]
    public decimal? CSaldoInicial { get; set; }

    [Column("C$saldoFinal")]
    [Precision(21, 8)]
    public decimal? CSaldoFinal { get; set; }

    [Column("C$saldoMensual")]
    [Precision(20, 8)]
    public decimal CSaldoMensual { get; set; }
}
