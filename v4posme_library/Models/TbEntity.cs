using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_entity")]
[Index("CompanyId", Name = "IDX_ENTITY_001")]
[Index("BranchId", Name = "IDX_ENTITY_002")]
[Index("EntityId", Name = "IDX_ENTITY_003")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public partial class TbEntity
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int BranchId { get; set; }

    [Key]
    [Column("entityID", TypeName = "int(11)")]
    public int EntityId { get; set; }

    [Column("createdAt", TypeName = "int(11)")]
    public int? CreatedAt { get; set; }

    [Column("createdOn", TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column("createdBy", TypeName = "bigint(20)")]
    public long? CreatedBy { get; set; }

    [Column("createdIn")]
    [StringLength(250)]
    public string? CreatedIn { get; set; }

    [Column("imagenBiometric")]
    [StringLength(500)]
    public string? ImagenBiometric { get; set; }
}
