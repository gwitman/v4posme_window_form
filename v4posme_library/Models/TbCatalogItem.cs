﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_catalog_item")]
[Index("CatalogId", Name = "IDX_CATALOG_ITEM_001")]
[Index("FlavorId", Name = "IDX_CATALOG_ITEM_002")]
[Index("ParentCatalogId", Name = "IDX_CATALOG_ITEM_003")]
[Index("ParentCatalogItemId", Name = "IDX_CATALOG_ITEM_004")]
[Index("CatalogItemId", Name = "IDX_CATALOG_ITEM_005")]
[Index("CatalogId", "CatalogItemId", Name = "IDX_CATALOG_ITEM_006")]
[Index("CatalogId", "FlavorId", Name = "IDX_CATALOG_ITEM_007")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public partial class TbCatalogItem
{
    [Column("catalogID")]
    public int CatalogId { get; set; }

    [Key]
    [Column("catalogItemID")]
    public int CatalogItemId { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string? Name { get; set; }

    [Column("display")]
    [StringLength(250)]
    public string? Display { get; set; }

    [Column("flavorID")]
    public int? FlavorId { get; set; }

    [Column("description")]
    [StringLength(250)]
    public string? Description { get; set; }

    [Column("sequence")]
    public int? Sequence { get; set; }

    [Column("parentCatalogID")]
    public int? ParentCatalogId { get; set; }

    [Column("parentCatalogItemID")]
    public int? ParentCatalogItemId { get; set; }

    [Column("ratio")]
    [Precision(19, 8)]
    public decimal Ratio { get; set; }

    [Column("reference1")]
    [StringLength(150)]
    public string? Reference1 { get; set; }

    [Column("reference2")]
    [StringLength(255)]
    public string? Reference2 { get; set; }

    [Column("reference3")]
    [StringLength(255)]
    public string? Reference3 { get; set; }

    [Column("reference4")]
    [StringLength(255)]
    public string? Reference4 { get; set; }
}
