using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_item_config_loto")]
[Index("ItemId", Name = "IDX_ITEM_CONFIG_LOTO_001")]
[MySqlCharSet("utf8")]
[MySqlCollation("utf8_general_ci")]
public class TbItemConfigLoto
{
    [Key]
    [Column("itemConfigLotoID", TypeName = "int(11)")]
    public int ItemConfigLotoId { get; set; }

    [Column("isActive", TypeName = "int(11)")]
    public int IsActive { get; set; }

    [Column("maxSale")]
    [Precision(19, 2)]
    public decimal MaxSale { get; set; }

    [Column("turno1Inicio", TypeName = "int(11)")]
    public int Turno1Inicio { get; set; }

    [Column("turno1Fin", TypeName = "int(11)")]
    public int Turno1Fin { get; set; }

    [Column("turno2Inicio", TypeName = "int(11)")]
    public int Turno2Inicio { get; set; }

    [Column("turno2Fin", TypeName = "int(11)")]
    public int Turno2Fin { get; set; }

    [Column("turno3Inicio", TypeName = "int(11)")]
    public int Turno3Inicio { get; set; }

    [Column("turno3Fin", TypeName = "int(11)")]
    public int Turno3Fin { get; set; }

    [Column("itemID", TypeName = "int(11)")]
    public int ItemId { get; set; }
}
