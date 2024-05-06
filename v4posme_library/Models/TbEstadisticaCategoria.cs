using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_estadistica_categorias")]
[Index("ClaseId", Name = "IDX_ESTADISTICA_CATEGORIA_001")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbEstadisticaCategoria
{
    [Column("claseID")]
    public int ClaseId { get; set; }

    [Key]
    [Column("categoriaID")]
    public int CategoriaId { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string Name { get; set; } = null!;

    [Column("startValue")]
    [Precision(18, 5)]
    public decimal? StartValue { get; set; }

    [Column("endValue")]
    [Precision(18, 5)]
    public decimal? EndValue { get; set; }

    [Column("isActive", TypeName = "bit(1)")]
    public ulong IsActive { get; set; }
}
