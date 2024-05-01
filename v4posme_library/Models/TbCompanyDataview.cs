using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_company_dataview")]
[Index("CompanyId", Name = "IDX_COMPANY_DATAVIEW_001")]
[Index("DataViewId", Name = "IDX_COMPANY_DATAVIEW_002")]
[Index("CallerId", Name = "IDX_COMPANY_DATAVIEW_003")]
[Index("ComponentId", Name = "IDX_COMPANY_DATAVIEW_004")]
[Index("CompanyId", "DataViewId", "CallerId", "ComponentId", "IsActive", Name = "IDX_COMPANY_DATAVIEW_005")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public partial class TbCompanyDataview
{
    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("dataViewID")]
    public int DataViewId { get; set; }

    [Column("callerID")]
    public int CallerId { get; set; }

    [Column("componentID")]
    public int ComponentId { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string? Name { get; set; }

    [Column("description")]
    [StringLength(250)]
    public string? Description { get; set; }

    [Column("sqlScript")]
    [StringLength(5000)]
    public string? SqlScript { get; set; }

    [Column("visibleColumns")]
    [StringLength(250)]
    public string? VisibleColumns { get; set; }

    [Column("nonVisibleColumns")]
    [StringLength(250)]
    public string? NonVisibleColumns { get; set; }

    [Column("summaryColumns")]
    [StringLength(255)]
    public string? SummaryColumns { get; set; }

    [Column("formatColumns")]
    [StringLength(255)]
    public string? FormatColumns { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }

    [Column("flavorID")]
    public int? FlavorId { get; set; }

    [Key]
    [Column("companyDataViewID")]
    public int CompanyDataViewId { get; set; }

    [Column("formatColumnsHeader")]
    [StringLength(255)]
    public string? FormatColumnsHeader { get; set; }
}
