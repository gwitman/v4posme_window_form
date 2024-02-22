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
[MySqlCharSet("utf8")]
[MySqlCollation("utf8_general_ci")]
public class TbTransactionMasterDenomination
{
    [Key]
    [Column("transactionMasterDenominationID", TypeName = "int(11)")]
    public int TransactionMasterDenominationId { get; set; }

    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("transactionID", TypeName = "int(11)")]
    public int TransactionId { get; set; }

    [Column("transactionMasterID", TypeName = "int(11)")]
    public int TransactionMasterId { get; set; }

    [Column("isActive", TypeName = "int(11)")]
    public int IsActive { get; set; }

    [Column("componentID", TypeName = "int(11)")]
    public int ComponentId { get; set; }

    [Column("catalogItemID", TypeName = "int(11)")]
    public int CatalogItemId { get; set; }

    [Column("currencyID", TypeName = "int(11)")]
    public int CurrencyId { get; set; }

    [Column("exchangeRate")]
    [Precision(19, 8)]
    public decimal ExchangeRate { get; set; }

    [Column("quantity", TypeName = "int(11)")]
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
