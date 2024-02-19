using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_transaction_concept")]
[Index("CompanyId", Name = "IDX_TRANSACTION_CONCEPT_001")]
[Index("TransactionId", Name = "IDX_TRANSACTION_CONCEPT_002")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbTransactionConcept
{
    [Key]
    [Column("conceptID", TypeName = "int(11)")]
    public int ConceptId { get; set; }

    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("transactionID", TypeName = "int(11)")]
    public int TransactionId { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string? Name { get; set; }

    [Column("orden", TypeName = "int(11)")]
    public int? Orden { get; set; }

    [Column("sign", TypeName = "int(11)")]
    public int? Sign { get; set; }

    [Column("visible", TypeName = "int(11)")]
    public int? Visible { get; set; }

    [Column("base")]
    [Precision(10, 8)]
    public decimal? Base { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }
}
