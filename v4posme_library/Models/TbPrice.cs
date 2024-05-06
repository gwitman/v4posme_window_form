using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_price")]
[Index("CompanyId", Name = "IDX_PRICE_001")]
[Index("ListPriceId", Name = "IDX_PRICE_002")]
[Index("ItemId", Name = "IDX_PRICE_003")]
[Index("TypePriceId", Name = "IDX_PRICE_004")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbPrice
{
    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("listPriceID")]
    public int ListPriceId { get; set; }

    [Column("itemID")]
    public int ItemId { get; set; }

    [Key]
    [Column("priceID")]
    public int PriceId { get; set; }

    [Column("typePriceID")]
    public int TypePriceId { get; set; }

    [Column("percentage")]
    [Precision(19, 8)]
    public decimal Percentage { get; set; }

    [Column("price")]
    [Precision(19, 8)]
    public decimal Price { get; set; }

    [Column("percentageCommision")]
    [Precision(19, 8)]
    public decimal PercentageCommision { get; set; }
}
