using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Keyless]
public partial class VwSinRiesgoReporteCreditosToSystema
{
    [Column("companyID")]
    public int? CompanyId { get; set; }

    [Column("TIPO_DE_ENTIDAD")]
    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string TipoDeEntidad { get; set; } = null!;

    [Column("NUMERO_CORRELATIVO")]
    [StringLength(3)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string NumeroCorrelativo { get; set; } = null!;

    [Column("FECHA_DE_REPORTE")]
    [StringLength(10)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string? FechaDeReporte { get; set; }

    [Column("DEPARTAMENTO")]
    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string Departamento { get; set; } = null!;

    [Column("NUMERO_DE_CEDULA_O_RUC")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string NumeroDeCedulaORuc { get; set; } = null!;

    [Column("NOMBRE_DE_PERSONA")]
    [StringLength(501)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? NombreDePersona { get; set; }

    [Column("TIPO_DE_CREDITO")]
    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string? TipoDeCredito { get; set; }

    [Column("FECHA_DE_DESEMBOLSO")]
    [StringLength(10)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string? FechaDeDesembolso { get; set; }

    [Column("TIPO_DE_OBLIGACION")]
    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string? TipoDeObligacion { get; set; }

    [Column("MONTO_AUTORIZADO")]
    [Precision(11, 2)]
    public decimal? MontoAutorizado { get; set; }

    [Column("PLAZO")]
    [Precision(13, 0)]
    public decimal? Plazo { get; set; }

    [Column("FRECUENCIA_DE_PAGO")]
    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string FrecuenciaDePago { get; set; } = null!;

    [Column("SALDO_DEUDA")]
    [Precision(11, 2)]
    public decimal? SaldoDeuda { get; set; }

    [Column("ESTADO")]
    [StringLength(3)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string? Estado { get; set; }

    [Column("MONTO_VENCIDO")]
    [Precision(34, 2)]
    public decimal? MontoVencido { get; set; }

    [Column("ANTIGUEDAD_DE_MORA")]
    public int? AntiguedadDeMora { get; set; }

    [Column("TIPO_DE_GARANTIA")]
    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string? TipoDeGarantia { get; set; }

    [Column("FORMA_DE_RECUPERACION")]
    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string? FormaDeRecuperacion { get; set; }

    [Column("NUMERO_DE_CREDITO")]
    [StringLength(50)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? NumeroDeCredito { get; set; }

    [Column("VALOR_DE_LA_CUOTA")]
    [Precision(35, 2)]
    public decimal? ValorDeLaCuota { get; set; }
}
