using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_indicator")]
[Index("CompanyId", Name = "IDX_INDICATOR_001")]
[Index("Code", Name = "IDX_INDICATOR_002")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbIndicator
{
    [Key]
    [Column("indicadorID")]
    public int IndicadorId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("code")]
    [StringLength(25)]
    public string Code { get; set; } = null!;

    [Column("name")]
    [StringLength(250)]
    public string Name { get; set; } = null!;

    [Column("label")]
    [StringLength(250)]
    public string Label { get; set; } = null!;

    [Column("description")]
    [StringLength(500)]
    public string Description { get; set; } = null!;

    [Column("order")]
    public int Order { get; set; }

    [Column("script")]
    [StringLength(5000)]
    public string Script { get; set; } = null!;

    [Column("posfix")]
    [StringLength(250)]
    public string Posfix { get; set; } = null!;

    [Column("prefix")]
    [StringLength(250)]
    public string Prefix { get; set; } = null!;

    [Column("isActive", TypeName = "bit(1)")]
    public ulong IsActive { get; set; }

    [Column("isGroup", TypeName = "bit(1)")]
    public ulong? IsGroup { get; set; }
}
