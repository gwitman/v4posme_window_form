using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_bank")]
[Index("AccountId", Name = "IDX_BANK_001")]
[Index("CurrencyId", Name = "IDX_BANK_002")]
[Index("CompanyId", Name = "IDX_BANK_003")]
public partial class TbBank
{
    [Key]
    [Column("bankID")]
    public int BankId { get; set; }

    [Column("companyID")]
    public int? CompanyId { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string? Name { get; set; }

    [Column("accountID")]
    public int? AccountId { get; set; }

    [Column("currencyID")]
    public int? CurrencyId { get; set; }

    [Column("balance")]
    [Precision(19, 8)]
    public decimal? Balance { get; set; }

    [Column("isActive")]
    public int? IsActive { get; set; }
}
