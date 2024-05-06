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
    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("componentID")]
    public int ComponentId { get; set; }

    [Column("componentItemID")]
    public int ComponentItemId { get; set; }

    [Column("flavorID")]
    [StringLength(250)]
    public string FlavorId { get; set; } = null!;

    [Key]
    [Column("companyComponentFlavorID")]
    public int CompanyComponentFlavorId { get; set; }
}
