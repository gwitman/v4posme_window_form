using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_parameter")]
[Index("Name", Name = "IDX_PARAMETER_001")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public class TbParameter
{
    [Key]
    [Column("parameterID", TypeName = "int(11)")]
    public int ParameterId { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string? Name { get; set; }

    [Column("description")]
    [StringLength(250)]
    public string? Description { get; set; }

    [Column("isRequiered")]
    public bool IsRequiered { get; set; }

    [Column("isEdited")]
    public bool IsEdited { get; set; }
}
