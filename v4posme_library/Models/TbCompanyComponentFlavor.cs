using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_company_component_flavor")]
[Index("CompanyId", Name = "IDX_COMPANY_COMPONENT_FLAVOR_001")]
[Index("ComponentId", Name = "IDX_COMPANY_COMPONENT_FLAVOR_002")]
[Index("ComponentItemId", Name = "IDX_COMPANY_COMPONENT_FLAVOR_003")]
[Index("FlavorId", Name = "IDX_COMPANY_COMPONENT_FLAVOR_004")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public partial class TbCompanyComponentFlavor
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("componentID", TypeName = "int(11)")]
    public int ComponentId { get; set; }

    [Column("componentItemID", TypeName = "int(11)")]
    public int ComponentItemId { get; set; }

    [Column("flavorID")]
    [StringLength(250)]
    public string FlavorId { get; set; } = null!;

    [Key]
    [Column("companyComponentFlavorID", TypeName = "int(11)")]
    public int CompanyComponentFlavorId { get; set; }
}
