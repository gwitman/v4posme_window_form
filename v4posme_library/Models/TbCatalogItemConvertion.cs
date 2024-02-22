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
public class TbCatalogItemConvertion
{
    [Key]
    [Column("catalogItemConvertionID", TypeName = "int(11)")]
    public int CatalogItemConvertionId { get; set; }

    [Column("componentID", TypeName = "int(11)")]
    public int ComponentId { get; set; }

    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("catalogID", TypeName = "int(11)")]
    public int CatalogId { get; set; }

    [Column("catalogItemID", TypeName = "int(11)")]
    public int CatalogItemId { get; set; }

    [Column("targetCatalogItemID", TypeName = "int(11)")]
    public int? TargetCatalogItemId { get; set; }

    [Column("ratio")]
    [Precision(18, 4)]
    public decimal? Ratio { get; set; }

    [Column("registerDate", TypeName = "datetime")]
    public DateTime? RegisterDate { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }
}
