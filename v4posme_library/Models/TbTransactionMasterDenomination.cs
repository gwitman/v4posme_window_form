using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_transaction_master_denomination")]
[Index("CompanyId", Name = "IDX_TRANSACTION_MASTER_DENOMINATION_001")]
[Index("TransactionId", Name = "IDX_TRANSACTION_MASTER_DENOMINATION_002")]
[Index("TransactionMasterId", Name = "IDX_TRANSACTION_MASTER_DENOMINATION_003")]
[Index("ComponentId", Name = "IDX_TRANSACTION_MASTER_DENOMINATION_004")]
[Index("CatalogItemId", Name = "IDX_TRANSACTION_MASTER_DENOMINATION_005")]
[Index("CurrencyId", Name = "IDX_TRANSACTION_MASTER_DENOMINATION_006")]
[MySqlCharSet("utf8mb3")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class TbTransactionMasterDenomination
{
    [Key]
    [Column("transactionMasterDenominationID")]
    public int TransactionMasterDenominationId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("transactionID")]
    public int TransactionId { get; set; }

    [Column("transactionMasterID")]
    public int TransactionMasterId { get; set; }

    [Column("isActive")]
    public int IsActive { get; set; }

    [Column("componentID")]
    public int ComponentId { get; set; }

    [Column("catalogItemID")]
    public int CatalogItemId { get; set; }

    [Column("currencyID")]
    public int CurrencyId { get; set; }

    [Column("exchangeRate")]
    [Precision(19, 8)]
    public decimal ExchangeRate { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("ratio")]
    [Precision(19, 8)]
    public decimal Ratio { get; set; }

    [Column("reference1")]
    [StringLength(150)]
    public string? Reference1 { get; set; }

    [Column("reference2")]
    [StringLength(150)]
    public string? Reference2 { get; set; }
}
