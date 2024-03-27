using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Keyless]
public partial class VwGerenciaDesembolsosResuman
{
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string CodigoCliente { get; set; } = null!;

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? Nombre { get; set; }

    [StringLength(5)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string Moneda { get; set; } = null!;

    [Column(TypeName = "int(5)")]
    public int? Edad { get; set; }

    [Column("C$Monto")]
    [Precision(17, 2)]
    public decimal? CMonto { get; set; }

    [Column("C$Balance")]
    [Precision(17, 2)]
    public decimal? CBalance { get; set; }

    [Column("C$Provisionado")]
    [Precision(17, 2)]
    public decimal? CProvisionado { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? Estado { get; set; }

    [Precision(19, 9)]
    public decimal Interes { get; set; }

    [Column(TypeName = "int(11)")]
    public int Plazo { get; set; }

    [Precision(18, 4)]
    public decimal TipoCambio { get; set; }

    public DateOnly Fecha { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? TipoAmortizacion { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? PeriodoPago { get; set; }

    [StringLength(4)]
    public string? Anio { get; set; }

    [StringLength(7)]
    public string? Mes { get; set; }

    [StringLength(2)]
    public string? MesUnicamente { get; set; }

    [StringLength(50)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string Factura { get; set; } = null!;
}
