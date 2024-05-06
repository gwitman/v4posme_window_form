using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_company_currency")]
[Index("CompanyId", Name = "IDX_COMPANY_CURRENCY_001")]
[Index("CurrencyId", Name = "IDX_COMPANY_CURRENCY_002")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbCompanyCurrency
{
    [Column("currencyID")]
    public int CurrencyId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("simb")]
    [StringLength(5)]
    public string? Simb { get; set; }

    [Key]
    [Column("companyCurrencyID")]
    public int CompanyCurrencyId { get; set; }
}
