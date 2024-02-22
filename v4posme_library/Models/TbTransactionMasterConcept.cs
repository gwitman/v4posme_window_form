using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_transaction_master_concept")]
[Index("CompanyId", Name = "IDX_TRANSACTION_MASTER_COMCEPT_001")]
[Index("TransactionId", Name = "IDX_TRANSACTION_MASTER_COMCEPT_002")]
[Index("TransactionMasterId", Name = "IDX_TRANSACTION_MASTER_COMCEPT_003")]
[Index("ComponentId", Name = "IDX_TRANSACTION_MASTER_COMCEPT_004")]
[Index("ComponentItemId", Name = "IDX_TRANSACTION_MASTER_COMCEPT_005")]
[Index("ConceptId", Name = "IDX_TRANSACTION_MASTER_COMCEPT_006")]
[Index("CurrencyId", Name = "IDX_TRANSACTION_MASTER_COMCEPT_007")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public class TbTransactionMasterConcept
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("transactionID", TypeName = "int(11)")]
    public int TransactionId { get; set; }

    [Column("transactionMasterID", TypeName = "int(11)")]
    public int TransactionMasterId { get; set; }

    [Column("componentID", TypeName = "int(11)")]
    public int? ComponentId { get; set; }

    [Column("componentItemID", TypeName = "int(11)")]
    public int? ComponentItemId { get; set; }

    [Column("conceptID", TypeName = "int(11)")]
    public int? ConceptId { get; set; }

    [Column("value")]
    [Precision(18, 4)]
    public decimal? Value { get; set; }

    [Column("currencyID", TypeName = "int(11)")]
    public int? CurrencyId { get; set; }

    [Column("exchangeRate")]
    [Precision(10, 4)]
    public decimal? ExchangeRate { get; set; }

    [Key]
    [Column("transactionMasterConceptID", TypeName = "int(11)")]
    public int TransactionMasterConceptId { get; set; }
}
