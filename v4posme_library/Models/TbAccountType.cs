using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_account_type")]
[Index("AccountTypeId", Name = "IDX_ACCOUNT_TYPE_001")]
[Index("CompanyId", Name = "IDX_ACCOUNT_TYPE_002")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbAccountType
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Key]
    [Column("accountTypeID", TypeName = "int(11)")]
    public int AccountTypeId { get; set; }

    [Column("name")]
    [StringLength(350)]
    public string Name { get; set; } = null!;

    [Column("naturaleza")]
    [StringLength(1)]
    public string Naturaleza { get; set; } = null!;

    [Column("description")]
    [StringLength(500)]
    public string? Description { get; set; }

    [Column("createdBy", TypeName = "int(11)")]
    public int CreatedBy { get; set; }

    [Column("createdAt")]
    [StringLength(250)]
    public string CreatedAt { get; set; } = null!;

    [Column("createdOn", TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [Column("createdIn")]
    [StringLength(250)]
    public string CreatedIn { get; set; } = null!;

    [Column("isActive")]
    public bool? IsActive { get; set; }
}
