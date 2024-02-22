using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Keyless]
public class VwTransaccionMasterConcept232425
{
    [Column("transactionMasterID", TypeName = "int(11)")]
    public int TransactionMasterId { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string Descripcion { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? Fecha { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string Documento { get; set; } = null!;

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string Moneda { get; set; } = null!;

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? Concepto { get; set; }

    [Precision(18, 4)]
    public decimal? Valor { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? Componente { get; set; }

    [Column("componentItemID", TypeName = "int(11)")]
    public int? ComponentItemId { get; set; }

    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string? Referencia1 { get; set; }
}
