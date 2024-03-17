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
public class TbPrice
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("listPriceID", TypeName = "int(11)")]
    public int ListPriceId { get; set; }

    [Column("itemID", TypeName = "int(11)")]
    public int ItemId { get; set; }

    [Key]
    [Column("priceID", TypeName = "int(11)")]
    public int PriceId { get; set; }

    [Column("typePriceID", TypeName = "int(11)")]
    public int TypePriceId { get; set; }

    [Column("percentage")]
    [Precision(19, 8)]
    public decimal Percentage { get; set; }

    [Column("price")] [Precision(19, 8)] public decimal Price { get; set; }

    [Column("percentageCommision")]
    [Precision(19, 8)]
    public decimal PercentageCommision { get; set; }

    [NotMapped] public string? TipoPrice { get; set; }
    [NotMapped] public string? ItemNumber { get; set; }
    [NotMapped] public string? ItemName { get; set; }
    [NotMapped] public decimal? Cost { get; set; }
    [NotMapped] public string? NameTypePrice { get; set; }
}