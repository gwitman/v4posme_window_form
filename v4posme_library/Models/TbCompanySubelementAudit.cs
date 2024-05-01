using System;
using System.Collections.Generic;
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
public partial class TbCompanySubelementAudit
{
    [Column("elementID")]
    public int ElementId { get; set; }

    [Column("subElementID")]
    public int SubElementId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Key]
    [Column("companySubelementAudiID")]
    public int CompanySubelementAudiId { get; set; }
}
