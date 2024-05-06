using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_item_config_loto")]
[Index("ItemId", Name = "IDX_ITEM_CONFIG_LOTO_001")]
[MySqlCharSet("utf8mb3")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class TbItemConfigLoto
{
    [Key]
    [Column("itemConfigLotoID")]
    public int ItemConfigLotoId { get; set; }

    [Column("isActive")]
    public int IsActive { get; set; }

    [Column("maxSale")]
    [Precision(19, 2)]
    public decimal MaxSale { get; set; }

    [Column("turno1Inicio")]
    public int Turno1Inicio { get; set; }

    [Column("turno1Fin")]
    public int Turno1Fin { get; set; }

    [Column("turno2Inicio")]
    public int Turno2Inicio { get; set; }

    [Column("turno2Fin")]
    public int Turno2Fin { get; set; }

    [Column("turno3Inicio")]
    public int Turno3Inicio { get; set; }

    [Column("turno3Fin")]
    public int Turno3Fin { get; set; }

    [Column("itemID")]
    public int ItemId { get; set; }
}
