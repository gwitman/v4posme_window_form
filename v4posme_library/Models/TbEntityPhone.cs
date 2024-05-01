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
    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("branchID")]
    public int BranchId { get; set; }

    [Column("entityID")]
    public int EntityId { get; set; }

    [Key]
    [Column("entityPhoneID")]
    public long EntityPhoneId { get; set; }

    [Column("typeID")]
    public int? TypeId { get; set; }

    [Column("number")]
    [StringLength(250)]
    public string? Number { get; set; }

    [Column("isPrimary")]
    public sbyte? IsPrimary { get; set; }
}
