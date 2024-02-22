using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_company_subelement_obligatory")]
[Index("CompanyId", Name = "IDX_COMPANY_SUBELEMENT_OBLIGATORI_001")]
[Index("ElementId", Name = "IDX_COMPANY_SUBELEMENT_OBLIGATORI_002")]
[Index("SubElementId", Name = "IDX_COMPANY_SUBELEMENT_OBLIGATORI_003")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public class TbCompanySubelementObligatory
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("elementID", TypeName = "int(11)")]
    public int ElementId { get; set; }

    [Column("subElementID", TypeName = "int(11)")]
    public int SubElementId { get; set; }

    [Key]
    [Column("companySubelementObligatoryID", TypeName = "int(11)")]
    public int CompanySubelementObligatoryId { get; set; }
}
