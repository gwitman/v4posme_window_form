using System;
using System.Collections.Generic;
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
public partial class TbTransactionMasterConcept
{
    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("transactionID")]
    public int TransactionId { get; set; }

    [Column("transactionMasterID")]
    public int TransactionMasterId { get; set; }

    [Column("componentID")]
    public int? ComponentId { get; set; }

    [Column("componentItemID")]
    public int? ComponentItemId { get; set; }

    [Column("conceptID")]
    public int? ConceptId { get; set; }

    [Column("value")]
    [Precision(18, 4)]
    public decimal? Value { get; set; }

    [Column("currencyID")]
    public int? CurrencyId { get; set; }

    [Column("exchangeRate")]
    [Precision(10, 4)]
    public decimal? ExchangeRate { get; set; }

    [Key]
    [Column("transactionMasterConceptID")]
    public int TransactionMasterConceptId { get; set; }
}
