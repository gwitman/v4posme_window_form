﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_account_level")]
[Index("CompanyId", Name = "IDX_ACCOUNT_LEVEL_001")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbAccountLevel
{
    [Column("companyID")]
    public int CompanyId { get; set; }

    [Key]
    [Column("accountLevelID")]
    public int AccountLevelId { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string Name { get; set; } = null!;

    [Column("description")]
    [StringLength(250)]
    public string? Description { get; set; }

    [Column("lengthTotal")]
    public int LengthTotal { get; set; }

    [Column("split")]
    [StringLength(1)]
    public string? Split { get; set; }

    [Column("lengthGroup")]
    public int? LengthGroup { get; set; }

    [Column("isOperative")]
    public bool IsOperative { get; set; }

    [Column("createdBy")]
    public int? CreatedBy { get; set; }

    [Column("createdOn", TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column("createdIn")]
    [StringLength(250)]
    public string? CreatedIn { get; set; }

    [Column("createdAt")]
    public int? CreatedAt { get; set; }

    [Column("isActive")]
    public bool IsActive { get; set; }
}
