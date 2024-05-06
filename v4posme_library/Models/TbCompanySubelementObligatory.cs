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
public partial class TbCompanySubelementObligatory
{
    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("elementID")]
    public int ElementId { get; set; }

    [Column("subElementID")]
    public int SubElementId { get; set; }

    [Key]
    [Column("companySubelementObligatoryID")]
    public int CompanySubelementObligatoryId { get; set; }
}
