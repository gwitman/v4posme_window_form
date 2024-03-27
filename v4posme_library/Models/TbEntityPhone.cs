using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_entity_phone")]
[Index("CompanyId", Name = "IDX_ENTITY_PHONE_001")]
[Index("BranchId", Name = "IDX_ENTITY_PHONE_002")]
[Index("EntityId", Name = "IDX_ENTITY_PHONE_003")]
[Index("TypeId", Name = "IDX_ENTITY_PHONE_004")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbEntityPhone
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int BranchId { get; set; }

    [Column("entityID", TypeName = "int(11)")]
    public int EntityId { get; set; }

    [Key]
    [Column("entityPhoneID", TypeName = "bigint(20)")]
    public long EntityPhoneId { get; set; }

    [Column("typeID", TypeName = "int(11)")]
    public int? TypeId { get; set; }

    [Column("number")]
    [StringLength(250)]
    public string? Number { get; set; }

    [Column("isPrimary", TypeName = "tinyint(4)")]
    public sbyte? IsPrimary { get; set; }
}
