using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_component_autorization")]
[Index("CompanyId", Name = "IDX_COMPONENT_AUTORIZATION_001")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbComponentAutorization
{
    [Key]
    [Column("componentAutorizationID")]
    public int ComponentAutorizationId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("name")]
    [StringLength(150)]
    public string? Name { get; set; }
}
