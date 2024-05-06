using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_entity_account")]
[Index("CompanyId", Name = "IDX_ENTITY_ACCOUNT_001")]
[Index("ComponentId", Name = "IDX_ENTITY_ACCOUNT_002")]
[Index("ComponentItemId", Name = "IDX_ENTITY_ACCOUNT_003")]
[Index("AccountTypeId", Name = "IDX_ENTITY_ACCOUNT_004")]
[Index("CurrencyId", Name = "IDX_ENTITY_ACCOUNT_005")]
[Index("ClassId", Name = "IDX_ENTITY_ACCOUNT_006")]
[Index("StatusId", Name = "IDX_ENTITY_ACCOUNT_007")]
[Index("AccountId", Name = "IDX_ENTITY_ACCOUNT_008")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbEntityAccount
{
    [Key]
    [Column("entityAccountID")]
    public int EntityAccountId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("componentID")]
    public int ComponentId { get; set; }

    [Column("componentItemID")]
    public int ComponentItemId { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("description")]
    [StringLength(250)]
    public string? Description { get; set; }

    [Column("accountTypeID")]
    public int AccountTypeId { get; set; }

    [Column("currencyID")]
    public int CurrencyId { get; set; }

    [Column("classID")]
    public int? ClassId { get; set; }

    [Column("balance")]
    [Precision(19, 9)]
    public decimal Balance { get; set; }

    [Column("creditLimit")]
    [Precision(19, 9)]
    public decimal CreditLimit { get; set; }

    [Column("maxCredit")]
    [Precision(19, 9)]
    public decimal MaxCredit { get; set; }

    [Column("debitLimit")]
    [Precision(19, 9)]
    public decimal DebitLimit { get; set; }

    [Column("maxDebit")]
    [Precision(19, 9)]
    public decimal MaxDebit { get; set; }

    [Column("statusID")]
    public int StatusId { get; set; }

    [Column("accountID")]
    public int? AccountId { get; set; }

    [Column("createdBy")]
    public int CreatedBy { get; set; }

    [Column("createdOn", TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [Column("createdIn")]
    [StringLength(50)]
    public string CreatedIn { get; set; } = null!;

    [Column("createdAt")]
    public int CreatedAt { get; set; }

    [Column("isActive", TypeName = "bit(1)")]
    public ulong IsActive { get; set; }
}
