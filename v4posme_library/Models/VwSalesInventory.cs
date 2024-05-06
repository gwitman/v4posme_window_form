using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Keyless]
public partial class VwSalesInventory
{
    [Column("createdOn", TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column("createdOnDay")]
    public int? CreatedOnDay { get; set; }

    [Column("currency")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string Currency { get; set; } = null!;

    [Column("tipo")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string Tipo { get; set; } = null!;

    [Column("causal")]
    [StringLength(50)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string Causal { get; set; } = null!;

    [Column("transactionNumber")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string TransactionNumber { get; set; } = null!;

    [Column("statusName")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? StatusName { get; set; }

    [Column("companiaName")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? CompaniaName { get; set; }

    [Column("warehouseName")]
    [StringLength(50)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string WarehouseName { get; set; } = null!;

    [Column("customerNumber")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string CustomerNumber { get; set; } = null!;

    [Column("firstName")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_general_ci")]
    public string? FirstName { get; set; }

    [Column("itemNumber")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string ItemNumber { get; set; } = null!;

    [Column("name")]
    [StringLength(250)]
    [MySqlCharSet("utf8mb3")]
    [MySqlCollation("utf8mb3_general_ci")]
    public string Name { get; set; } = null!;

    [Column("categoryName")]
    [StringLength(250)]
    [MySqlCharSet("latin1")]
    [MySqlCollation("latin1_swedish_ci")]
    public string CategoryName { get; set; } = null!;

    [Column("tipoCambio")]
    [Precision(9, 4)]
    public decimal? TipoCambio { get; set; }

    [Column("quantity")]
    [Precision(18, 4)]
    public decimal? Quantity { get; set; }

    [Column("unitaryCost")]
    [Precision(18, 4)]
    public decimal? UnitaryCost { get; set; }

    [Column("cost")]
    [Precision(36, 8)]
    public decimal? Cost { get; set; }

    [Column("unitaryAmount")]
    [Precision(27, 8)]
    public decimal? UnitaryAmount { get; set; }

    [Column("amount")]
    [Precision(27, 8)]
    public decimal? Amount { get; set; }

    [Column("utility")]
    [Precision(37, 8)]
    public decimal? Utility { get; set; }
}
