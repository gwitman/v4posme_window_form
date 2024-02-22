using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Keyless]
public class VwSinRiesgoReporteCliente
{
    [Column("FECHA REPORTE")]
    [StringLength(10)]
    public string? FechaReporte { get; set; }

    [Column("IDENTIFICACION")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string Identificacion { get; set; } = null!;

    [Column("TIPO DE PERSONA")]
    [StringLength(1)]
    public string TipoDePersona { get; set; } = null!;

    [Column("NACIONALIDAD")]
    [StringLength(12)]
    public string Nacionalidad { get; set; } = null!;

    [Column("SEXO")]
    [StringLength(1)]
    public string Sexo { get; set; } = null!;

    [Column("FECHA DE NACIMIENTO")]
    [StringLength(10)]
    public string? FechaDeNacimiento { get; set; }

    [Column("ESTADO CIVIL")]
    [StringLength(3)]
    public string EstadoCivil { get; set; } = null!;

    [Column("DIRECCION")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? Direccion { get; set; }

    [Column("DEPARTAMENTO")]
    [StringLength(2)]
    public string Departamento { get; set; } = null!;

    [Column("MUNICIPIO")]
    [StringLength(2)]
    public string Municipio { get; set; } = null!;

    [Column("DIRECCION DE TRABAJO")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? DireccionDeTrabajo { get; set; }

    [Column("DEPARTAMENTO DE TRABAJO")]
    [StringLength(2)]
    public string DepartamentoDeTrabajo { get; set; } = null!;

    [Column("MUNICIPIO DE TRABAJO")]
    [StringLength(2)]
    public string MunicipioDeTrabajo { get; set; } = null!;

    [Column("TELEFONO DOMICILIAR")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? TelefonoDomiciliar { get; set; }

    [Column("TELEFONO TRABAJO")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? TelefonoTrabajo { get; set; }

    [Column("CELULAR")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? Celular { get; set; }

    [Column("CORREO ELECTRONICO")]
    [StringLength(0)]
    public string CorreoElectronico { get; set; } = null!;

    [Column("OCUPACION")]
    [StringLength(11)]
    public string Ocupacion { get; set; } = null!;

    [Column("ACTIVIDAD ECONOMICA")]
    [StringLength(8)]
    public string ActividadEconomica { get; set; } = null!;

    [Column("SECTOR")]
    [StringLength(7)]
    public string Sector { get; set; } = null!;
}
