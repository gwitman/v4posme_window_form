using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_currency")]
[Index("CurrencyId", Name = "IDX_CURRENCY_001")]
[Index("Name", "IsActive", Name = "IDX_CURRENCY_002")]
[Index("CurrencyId", "IsActive", Name = "IDX_CURRENCY_003")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbCurrency
{
    [Key]
    [Column("currencyID")]
    public int CurrencyId { get; set; }

    [Column("simbol")]
    [StringLength(5)]
    public string Simbol { get; set; } = null!;

    [Column("name")]
    [StringLength(250)]
    public string Name { get; set; } = null!;

    [Column("description")]
    [StringLength(250)]
    public string? Description { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }
}
