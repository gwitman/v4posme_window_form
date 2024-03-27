using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_account")]
[Index("CompanyId", Name = "IDX_ACCOUNT_001")]
[Index("AccountTypeId", Name = "IDX_ACCOUNT_002")]
[Index("AccountLevelId", Name = "IDX_ACCOUNT_003")]
[Index("ParentAccountId", Name = "IDX_ACCOUNT_004")]
[Index("ClassId", Name = "IDX_ACCOUNT_005")]
[Index("AccountNumber", Name = "IDX_ACCOUNT_006")]
[Index("StatusId", Name = "IDX_ACCOUNT_007")]
[Index("CurrencyId", Name = "IDX_ACCOUNT_008")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbAccount
{
    [Key]
    [Column("accountID", TypeName = "int(11)")]
    public int AccountId { get; set; }

    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("accountTypeID", TypeName = "int(11)")]
    public int AccountTypeId { get; set; }

    [Column("accountLevelID", TypeName = "int(11)")]
    public int AccountLevelId { get; set; }

    [Column("parentAccountID", TypeName = "int(11)")]
    public int? ParentAccountId { get; set; }

    [Column("classID", TypeName = "int(11)")]
    public int? ClassId { get; set; }

    [Column("accountNumber")]
    [StringLength(250)]
    public string AccountNumber { get; set; } = null!;

    [Column("name")]
    [StringLength(250)]
    public string Name { get; set; } = null!;

    [Column("description")]
    [StringLength(500)]
    public string? Description { get; set; }

    [Column("isOperative")]
    public bool IsOperative { get; set; }

    [Column("statusID", TypeName = "int(11)")]
    public int StatusId { get; set; }

    [Column("currencyID", TypeName = "int(11)")]
    public int CurrencyId { get; set; }

    [Column("createdBy", TypeName = "int(11)")]
    public int? CreatedBy { get; set; }

    [Column("createdOn", TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column("createdIn")]
    [StringLength(250)]
    public string? CreatedIn { get; set; }

    [Column("createdAt")]
    [StringLength(250)]
    public string? CreatedAt { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }
}
