﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_catalog")]
[Index("CatalogId", Name = "IDX_CATALOG_001")]
[Index("CatalogId", "IsActive", Name = "IDX_CATALOG_002")]
[Index("Name", "IsActive", Name = "IDX_CATALOG_003")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public partial class TbCatalog
{
    [Key]
    [Column("catalogID")]
    public int CatalogId { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string? Name { get; set; }

    [Column("orden")]
    public int Orden { get; set; }

    [Column("description")]
    [StringLength(250)]
    public string? Description { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }
}
