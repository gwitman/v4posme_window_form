using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Keyless]
public partial class VwInventoryListItemRealEstate
{
    [Column("Codigo interno")]
    public int CodigoInterno { get; set; }

    [Column("itemID")]
    [StringLength(121)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string ItemId { get; set; } = null!;

    [Column("createdOn", TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string Codigo { get; set; } = null!;

    [StringLength(250)]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public string Nombre { get; set; } = null!;

    [Column("Pagina Web Url")]
    [StringLength(1200)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? PaginaWebUrl { get; set; }

    [Column("Pagina Web", TypeName = "text")]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? PaginaWeb { get; set; }

    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string Amueblado { get; set; } = null!;

    public int? Aires { get; set; }

    [Precision(18, 4)]
    public decimal Niveles { get; set; }

    [Column("Hora de visita")]
    [Precision(18, 4)]
    public decimal HoraDeVisita { get; set; }

    [Precision(18, 4)]
    public decimal? Baños { get; set; }

    [Precision(18, 4)]
    public decimal? Habitaciones { get; set; }

    [Column("Diseño de propiedad")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string DiseñoDePropiedad { get; set; } = null!;

    [Column("Tipo de casa")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? TipoDeCasa { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? Proposito { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string Moneda { get; set; } = null!;

    [Column("Fecha de enlistamiento", TypeName = "datetime")]
    public DateTime? FechaDeEnlistamiento { get; set; }

    [Column("Fecha de actualizacion", TypeName = "datetime")]
    public DateTime? FechaDeActualizacion { get; set; }

    [Column("Precio Venta")]
    [Precision(19, 8)]
    public decimal? PrecioVenta { get; set; }

    [Column("Precio Renta")]
    [Precision(19, 8)]
    public decimal? PrecioRenta { get; set; }

    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string Disponible { get; set; } = null!;

    [Column("Area de contruccion M2")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? AreaDeContruccionM2 { get; set; }

    [Column("Area de terreno V2")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? AreaDeTerrenoV2 { get; set; }

    [Column("ID Encuentra 24")]
    [StringLength(255)]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public string? IdEncuentra24 { get; set; }

    [Column("Baño de servicio")]
    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string BañoDeServicio { get; set; } = null!;

    [Column("Baño de visita")]
    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string BañoDeVisita { get; set; } = null!;

    [Column("Cuarto de servicio")]
    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string CuartoDeServicio { get; set; } = null!;

    [Column("Walk in closet")]
    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string WalkInCloset { get; set; } = null!;

    [Column("Piscina privada")]
    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string PiscinaPrivada { get; set; } = null!;

    [Column("Area club con piscina")]
    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string AreaClubConPiscina { get; set; } = null!;

    [Column("Acepta mascota")]
    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string AceptaMascota { get; set; } = null!;

    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string Corretaje { get; set; } = null!;

    [Column("Plan de referido")]
    [StringLength(2)]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string PlanDeReferido { get; set; } = null!;

    [Column("Link Youtube Url")]
    [StringLength(255)]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public string? LinkYoutubeUrl { get; set; }

    [Column("Link Youtube", TypeName = "text")]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public string? LinkYoutube { get; set; }

    [Column("Pagina Web Link Url")]
    [StringLength(255)]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public string? PaginaWebLinkUrl { get; set; }

    [Column("Pagina Web Link", TypeName = "text")]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public string? PaginaWebLink { get; set; }

    [Column("Foto Url")]
    [StringLength(255)]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public string? FotoUrl { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public string? Foto { get; set; }

    [Column("Google Url")]
    [StringLength(255)]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public string? GoogleUrl { get; set; }

    [Column(TypeName = "text")]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public string? Google { get; set; }

    [Column("Otros Link Url")]
    [StringLength(255)]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public string? OtrosLinkUrl { get; set; }

    [Column("Otros Link", TypeName = "text")]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public string? OtrosLink { get; set; }

    [Column("Estilo de cocina")]
    [StringLength(255)]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public string? EstiloDeCocina { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string Agente { get; set; } = null!;

    [StringLength(255)]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public string? Zona { get; set; }

    [StringLength(255)]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public string? Condominio { get; set; }

    [StringLength(255)]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public string? Ubicacion { get; set; }

    [Column("Exclusividad de agente")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? ExclusividadDeAgente { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? Pais { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? Estado { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? Ciudad { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }
}
