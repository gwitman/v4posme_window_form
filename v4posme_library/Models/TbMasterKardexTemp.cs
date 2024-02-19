using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_master_kardex_temp")]
[Index("ItemId", "CompanyId", Name = "IDX_COMPANY_ITEM")]
[Index("ItemId", "UserId", "CompanyId", Name = "IDX_COMPANY_ITEM_USER")]
[Index("UserId", "MinKardexId", "CompanyId", "ItemId", Name = "IDX_COMPANY_ITEM_USER_MINKARDEX")]
[Index("UserId", "CompanyId", Name = "IDX_COMPANY_USER")]
[Index("UserId", Name = "IDX_USER")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbMasterKardexTemp
{
    [Column("userID", TypeName = "int(11)")]
    public int UserId { get; set; }

    [Column("tokenID")]
    [StringLength(50)]
    public string? TokenId { get; set; }

    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("itemID", TypeName = "int(11)")]
    public int ItemId { get; set; }

    [Column("itemNumber")]
    [StringLength(50)]
    public string ItemNumber { get; set; } = null!;

    [Column("itemName")]
    [StringLength(50)]
    public string ItemName { get; set; } = null!;

    [Column("minKardexID", TypeName = "int(11)")]
    public int MinKardexId { get; set; }

    [Column("quantityInicial")]
    [Precision(19, 9)]
    public decimal QuantityInicial { get; set; }

    [Column("costInicial")]
    [Precision(19, 9)]
    public decimal CostInicial { get; set; }

    [Column("quantityInput")]
    [Precision(19, 9)]
    public decimal QuantityInput { get; set; }

    [Column("costInput")]
    [Precision(19, 9)]
    public decimal CostInput { get; set; }

    [Column("quantityOutput")]
    [Precision(19, 9)]
    public decimal QuantityOutput { get; set; }

    [Column("costOutput")]
    [Precision(19, 9)]
    public decimal CostOutput { get; set; }

    [Key]
    [Column("masterKardexTempID", TypeName = "int(11)")]
    public int MasterKardexTempId { get; set; }

    [Column("itemCategoryName")]
    [StringLength(50)]
    public string? ItemCategoryName { get; set; }
}
