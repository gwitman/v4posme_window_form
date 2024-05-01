using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_catalog_item_convertion")]
[Index("ComponentId", Name = "IDX_CATALOG_ITEM_CONVERSATION_001")]
[Index("CompanyId", Name = "IDX_CATALOG_ITEM_CONVERSATION_002")]
[Index("CatalogId", Name = "IDX_CATALOG_ITEM_CONVERSATION_003")]
[Index("CatalogItemId", Name = "IDX_CATALOG_ITEM_CONVERSATION_004")]
[Index("TargetCatalogItemId", Name = "IDX_CATALOG_ITEM_CONVERSATION_005")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbCatalogItemConvertion
{
    [Key]
    [Column("catalogItemConvertionID")]
    public int CatalogItemConvertionId { get; set; }

    [Column("componentID")]
    public int ComponentId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("catalogID")]
    public int CatalogId { get; set; }

    [Column("catalogItemID")]
    public int CatalogItemId { get; set; }

    [Column("targetCatalogItemID")]
    public int? TargetCatalogItemId { get; set; }

    [Column("ratio")]
    [Precision(18, 4)]
    public decimal? Ratio { get; set; }

    [Column("registerDate", TypeName = "datetime")]
    public DateTime? RegisterDate { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }
}
