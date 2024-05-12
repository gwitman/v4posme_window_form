using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_transaction_causal")]
[Index("CompanyId", Name = "IDX_TRANSACTION_CAUSAL_001")]
[Index("TransactionId", Name = "IDX_TRANSACTION_CAUSAL_002")]
[Index("BranchId", Name = "IDX_TRANSACTION_CAUSAL_003")]
[Index("WarehouseSourceId", Name = "IDX_TRANSACTION_CAUSAL_004")]
[Index("WarehouseTargetId", Name = "IDX_TRANSACTION_CAUSAL_005")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbTransactionCausal
{
    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("transactionID")]
    public int TransactionId { get; set; }

    [Key]
    [Column("transactionCausalID")]
    public int TransactionCausalId { get; set; }

    [Column("branchID")]
    public int? BranchId { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("warehouseSourceID")]
    public int? WarehouseSourceId { get; set; }

    [Column("warehouseTargetID")]
    public int? WarehouseTargetId { get; set; }

    [Column("isDefault", TypeName = "bit(1)")]
    public bool? IsDefault { get; set; }

    [Column("isActive", TypeName = "bit(1)")]
    public bool? IsActive { get; set; }
}
