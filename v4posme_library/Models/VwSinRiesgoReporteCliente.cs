using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Keyless]
public partial class VwSinRiesgoReporteCliente
{
    [Column("FECHA REPORTE")]
    [StringLength(10)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string? FechaReporte { get; set; }

    [Column("IDENTIFICACION")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string Identificacion { get; set; } = null!;

    [Column("TIPO DE PERSONA")]
    [StringLength(1)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string TipoDePersona { get; set; } = null!;

    [Column("NACIONALIDAD")]
    [StringLength(12)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string Nacionalidad { get; set; } = null!;

    [Column("SEXO")]
    [StringLength(1)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string Sexo { get; set; } = null!;

    [Column("FECHA DE NACIMIENTO")]
    [StringLength(10)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string? FechaDeNacimiento { get; set; }

    [Column("ESTADO CIVIL")]
    [StringLength(3)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string EstadoCivil { get; set; } = null!;

    [Column("DIRECCION")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? Direccion { get; set; }

    [Column("DEPARTAMENTO")]
    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string Departamento { get; set; } = null!;

    [Column("MUNICIPIO")]
    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string Municipio { get; set; } = null!;

    [Column("DIRECCION DE TRABAJO")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? DireccionDeTrabajo { get; set; }

    [Column("DEPARTAMENTO DE TRABAJO")]
    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string DepartamentoDeTrabajo { get; set; } = null!;

    [Column("MUNICIPIO DE TRABAJO")]
    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
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
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string CorreoElectronico { get; set; } = null!;

    [Column("OCUPACION")]
    [StringLength(11)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string Ocupacion { get; set; } = null!;

    [Column("ACTIVIDAD ECONOMICA")]
    [StringLength(8)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string ActividadEconomica { get; set; } = null!;

    [Column("SECTOR")]
    [StringLength(7)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string Sector { get; set; } = null!;
}
