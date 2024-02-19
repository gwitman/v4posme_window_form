using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_entity_email")]
[Index("CompanyId", Name = "IDX_ENTITY_EMAIL_001")]
[Index("BranchId", Name = "IDX_ENTITY_EMAIL_002")]
[Index("EntityId", Name = "IDX_ENTITY_EMAIL_003")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbEntityEmail
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int BranchId { get; set; }

    [Column("entityID", TypeName = "int(11)")]
    public int EntityId { get; set; }

    [Key]
    [Column("entityEmailID", TypeName = "bigint(20)")]
    public long EntityEmailId { get; set; }

    [Column("email")]
    [StringLength(250)]
    public string? Email { get; set; }

    [Column("isPrimary", TypeName = "tinyint(4)")]
    public sbyte? IsPrimary { get; set; }
}
