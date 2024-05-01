using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_public_catalog_detail")]
[Index("PublicCatalogId", Name = "IDX_PUBLIC_CATALOG_DETAIL_001")]
[Index("FlavorId", Name = "IDX_PUBLIC_CATALOG_DETAIL_002")]
[Index("ParentCatalogDetailId", Name = "IDX_PUBLIC_CATALOG_DETAIL_003")]
[Index("IsActive", Name = "IDX_PUBLIC_CATALOG_DETAIL_004")]
public partial class TbPublicCatalogDetail
{
    [Key]
    [Column("publicCatalogDetailID")]
    public int PublicCatalogDetailId { get; set; }

    [Column("publicCatalogID")]
    public int? PublicCatalogId { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string? Name { get; set; }

    [Column("display")]
    [StringLength(255)]
    public string? Display { get; set; }

    [Column("flavorID")]
    public int? FlavorId { get; set; }

    [Column("description")]
    [StringLength(255)]
    public string? Description { get; set; }

    [Column("sequence")]
    [StringLength(255)]
    public string? Sequence { get; set; }

    [Column("parentCatalogDetailID")]
    public int? ParentCatalogDetailId { get; set; }

    [Column("ratio")]
    [StringLength(255)]
    public string? Ratio { get; set; }

    [Column("reference1")]
    [StringLength(255)]
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

    [Column("parentName")]
    [StringLength(255)]
    public string? ParentName { get; set; }

    [Column("isActive")]
    public string? IsActive { get; set; }
}
