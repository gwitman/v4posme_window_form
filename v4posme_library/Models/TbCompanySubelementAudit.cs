using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_company_subelement_audit")]
[Index("ElementId", Name = "IDX_COMPANY_SUBELEMENT_AUDIT_001")]
[Index("SubElementId", Name = "IDX_COMPANY_SUBELEMENT_AUDIT_002")]
[Index("CompanyId", Name = "IDX_COMPANY_SUBELEMENT_AUDIT_003")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public class TbCompanySubelementAudit
{
    [Column("elementID", TypeName = "int(11)")]
    public int ElementId { get; set; }

    [Column("subElementID", TypeName = "int(11)")]
    public int SubElementId { get; set; }

    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Key]
    [Column("companySubelementAudiID", TypeName = "int(11)")]
    public int CompanySubelementAudiId { get; set; }
}
