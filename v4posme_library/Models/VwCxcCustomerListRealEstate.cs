using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Keyless]
public partial class VwCxcCustomerListRealEstate
{
    [Column("entityID", TypeName = "int(11)")]
    public int EntityId { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string Codigo { get; set; } = null!;

    public DateOnly? Contacto { get; set; }

    public DateOnly? Modificacion { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? Cliente { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? Sexo { get; set; }

    [StringLength(307)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? Estado { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? Clasificacion { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? Categoria { get; set; }

    [Precision(10, 2)]
    public decimal? Presupuesto { get; set; }

    [MaxLength(255)]
    public byte[]? Telefono { get; set; }

    [Column("Ubicacion Interes")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? UbicacionInteres { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string Agente { get; set; } = null!;

    [Column("Encuentra 24")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? Encuentra24 { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? Mensaje { get; set; }

    [Column("Comentario 1")]
    [StringLength(255)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? Comentario1 { get; set; }

    [Column("Comentario 2")]
    [StringLength(255)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? Comentario2 { get; set; }

    [StringLength(255)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? Ubicacion { get; set; }

    [Column("Forma de contacto")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? FormaDeContacto { get; set; }
}
