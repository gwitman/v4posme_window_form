using System;
using System.Collections.Generic;
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
public partial class TbProviderItem
{
    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("branchID")]
    public int BranchId { get; set; }

    [Column("entityID")]
    public int EntityId { get; set; }

    [Column("itemID")]
    public int ItemId { get; set; }

    [Key]
    [Column("providerItemID")]
    public int ProviderItemId { get; set; }
}
