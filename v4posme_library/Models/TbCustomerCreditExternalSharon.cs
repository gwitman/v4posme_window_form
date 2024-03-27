using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Keyless]
[Table("tb_customer_credit_external_sharon")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbCustomerCreditExternalSharon
{
    [Column("customerID", TypeName = "int(11)")]
    public int CustomerId { get; set; }

    [Column("companyID", TypeName = "int(11)")]
    public int? CompanyId { get; set; }

    [Column("TIPO_DE_ENTIDAD")]
    [StringLength(5)]
    public string? TipoDeEntidad { get; set; }

    [Column("NUMERO_CORRELATIVO")]
    [StringLength(50)]
    public string NumeroCorrelativo { get; set; } = null!;

    [Column("FECHA_DE_REPORTE")]
    [StringLength(50)]
    public string FechaDeReporte { get; set; } = null!;

    [Column("DEPARTAMENTO")]
    [StringLength(250)]
    public string Departamento { get; set; } = null!;

    [Column("NUMERO_DE_CEDULA_O_RUC")]
    [StringLength(250)]
    public string NumeroDeCedulaORuc { get; set; } = null!;

    [Column("NOMBRE_DE_PERSONA")]
    [StringLength(250)]
    public string NombreDePersona { get; set; } = null!;

    [Column("TIPO_DE_CREDITO")]
    [StringLength(250)]
    public string TipoDeCredito { get; set; } = null!;

    [Column("FECHA_DE_DESEMBOLSO")]
    [StringLength(50)]
    public string FechaDeDesembolso { get; set; } = null!;

    [Column("TIPO_DE_OBLIGACION")]
    [StringLength(50)]
    public string TipoDeObligacion { get; set; } = null!;

    [Column("MONTO_AUTORIZADO")]
    [Precision(19, 2)]
    public decimal MontoAutorizado { get; set; }

    [Column("PLAZO", TypeName = "int(11)")]
    public int? Plazo { get; set; }

    [Column("FRECUENCIA_DE_PAGO")]
    [StringLength(250)]
    public string FrecuenciaDePago { get; set; } = null!;

    [Column("SALDO_DEUDA")]
    [Precision(19, 2)]
    public decimal SaldoDeuda { get; set; }

    [Column("ESTADO")]
    [StringLength(450)]
    public string Estado { get; set; } = null!;

    [Column("MONTO_VENCIDO")]
    [Precision(19, 2)]
    public decimal MontoVencido { get; set; }

    [Column("ANTIGUEDAD_DE_MORA")]
    [StringLength(250)]
    public string AntiguedadDeMora { get; set; } = null!;

    [Column("TIPO_DE_GARANTIA")]
    [StringLength(250)]
    public string TipoDeGarantia { get; set; } = null!;

    [Column("FORMA_DE_RECUPERACION")]
    [StringLength(250)]
    public string FormaDeRecuperacion { get; set; } = null!;

    [Column("NUMERO_DE_CREDITO")]
    [StringLength(250)]
    public string NumeroDeCredito { get; set; } = null!;

    [Column("VALOR_DE_LA_CUOTA")]
    [Precision(19, 2)]
    public decimal ValorDeLaCuota { get; set; }

    [Column("isActive", TypeName = "bit(1)")]
    public ulong IsActive { get; set; }

    [Column("createdOn")]
    public DateOnly CreatedOn { get; set; }
}
