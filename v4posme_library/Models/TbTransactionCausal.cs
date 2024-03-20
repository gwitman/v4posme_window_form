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
public class TbTransactionCausal
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("transactionID", TypeName = "int(11)")]
    public int TransactionId { get; set; }

    [Key]
    [Column("transactionCausalID", TypeName = "int(11)")]
    public int TransactionCausalId { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int? BranchId { get; set; }

    [Column("name")] [StringLength(50)] public string Name { get; set; } = null!;

    [Column("warehouseSourceID", TypeName = "int(11)")]
    public int? WarehouseSourceId { get; set; }

    [Column("warehouseTargetID", TypeName = "int(11)")]
    public int? WarehouseTargetId { get; set; }

    [Column("isDefault", TypeName = "bit(1)")]
    public ulong IsDefault { get; set; }

    [Column("isActive", TypeName = "bit(1)")]
    public ulong IsActive { get; set; }

    [NotMapped] public string? Branch { get; set; }
    [NotMapped] public string? WarehouseSourceDescription { get; set; }
    [NotMapped] public string? WarehouseTargetDescription { get; set; }
}