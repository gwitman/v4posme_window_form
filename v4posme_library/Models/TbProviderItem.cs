using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_provider_item")]
[Index("CompanyId", Name = "IDX_PROVIDER_ITEM_001")]
[Index("BranchId", Name = "IDX_PROVIDER_ITEM_002")]
[Index("EntityId", Name = "IDX_PROVIDER_ITEM_003")]
[Index("ItemId", Name = "IDX_PROVIDER_ITEM_004")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public class TbProviderItem
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int BranchId { get; set; }

    [Column("entityID", TypeName = "int(11)")]
    public int EntityId { get; set; }

    [Column("itemID", TypeName = "int(11)")]
    public int ItemId { get; set; }

    [Key]
    [Column("providerItemID", TypeName = "int(11)")]
    public int ProviderItemId { get; set; }

    [NotMapped] public string? ProviderNumber { get; set; }
    [NotMapped] public string? FirstName { get; set; }
    [NotMapped] public string? LastName { get; set; }
    [NotMapped] public string? ComercialName { get; set; }
}