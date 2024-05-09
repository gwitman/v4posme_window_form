using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_exchange_rate")]
[Index("CurrencyId", Name = "IDX_EXCHANGE_RATE_001")]
[Index("CompanyId", Name = "IDX_EXCHANGE_RATE_002")]
[Index("TargetCurrencyId", Name = "IDX_EXCHANGE_RATE_003")]
[Index("Date", Name = "IDX_EXCHANGE_RATE_004")]
[Index("CurrencyId", "CompanyId", "Date", "TargetCurrencyId", Name = "IDX_EXCHANGE_RATE_005")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbExchangeRate
{
    [Column("currencyID")]
    public int CurrencyId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("date")]
    public DateOnly Date { get; set; }

    [Column("targetCurrencyID")]
    public int TargetCurrencyId { get; set; }

    [Column("ratio")]
    public double? Ratio { get; set; }

    [Column("value")]
    [Precision(19, 4)]
    public decimal Value { get; set; }

    [Key]
    [Column("exchangeRateID")]
    public int ExchangeRateId { get; set; }
}
