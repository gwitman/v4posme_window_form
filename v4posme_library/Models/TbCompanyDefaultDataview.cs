using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_company_default_dataview")]
[Index("CompanyId", Name = "IDX_COMPANY_DEFAULT_DATAVIEW_001")]
[Index("ComponentId", Name = "IDX_COMPANY_DEFAULT_DATAVIEW_002")]
[Index("DataViewId", Name = "IDX_COMPANY_DEFAULT_DATAVIEW_003")]
[Index("CallerId", Name = "IDX_COMPANY_DEFAULT_DATAVIEW_004")]
[Index("TargetComponentId", Name = "IDX_COMPANY_DEFAULT_DATAVIEW_005")]
[Index("CompanyId", "ComponentId", "CallerId", "TargetComponentId", Name = "IDX_COMPANY_DEFAULT_DATAVIEW_006")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public class TbCompanyDefaultDataview
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("componentID", TypeName = "int(11)")]
    public int ComponentId { get; set; }

    [Column("dataViewID", TypeName = "int(11)")]
    public int DataViewId { get; set; }

    [Column("callerID", TypeName = "int(11)")]
    public int CallerId { get; set; }

    [Column("targetComponentID", TypeName = "int(11)")]
    public int? TargetComponentId { get; set; }

    [Key]
    [Column("companyDefaultDataviewID", TypeName = "int(11)")]
    public int CompanyDefaultDataviewId { get; set; }
}
