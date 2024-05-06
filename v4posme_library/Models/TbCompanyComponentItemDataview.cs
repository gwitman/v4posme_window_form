using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_company_component_item_dataview")]
[Index("CompanyId", Name = "IDX_COMPANY_COMPONENT_ITEM_DATAVIEW_001")]
[Index("ComponentId", Name = "IDX_COMPANY_COMPONENT_ITEM_DATAVIEW_002")]
[Index("DataViewId", Name = "IDX_COMPANY_COMPONENT_ITEM_DATAVIEW_003")]
[Index("CallerId", Name = "IDX_COMPANY_COMPONENT_ITEM_DATAVIEW_004")]
[Index("FlavorId", Name = "IDX_COMPANY_COMPONENT_ITEM_DATAVIEW_005")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public partial class TbCompanyComponentItemDataview
{
    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("componentID")]
    public int ComponentId { get; set; }

    [Column("dataViewID")]
    public int DataViewId { get; set; }

    [Column("callerID")]
    public int CallerId { get; set; }

    [Column("flavorID")]
    public int FlavorId { get; set; }

    [Key]
    [Column("companyComponentItemDataviewID")]
    public int CompanyComponentItemDataviewId { get; set; }
}
