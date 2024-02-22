using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_type_menu_element")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public class TbTypeMenuElement
{
    [Key]
    [Column("typeMenuElementID", TypeName = "int(11)")]
    public int TypeMenuElementId { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string Name { get; set; } = null!;
}
