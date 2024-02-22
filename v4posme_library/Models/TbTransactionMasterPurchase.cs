using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_transaction_master_purchase")]
[Index("CompanyId", Name = "IDX_TRANSACTION_MASTER_PURCHASE_001")]
[Index("TransactionId", Name = "IDX_TRANSACTION_MASTER_PURCHASE_002")]
[Index("TransactionMasterId", Name = "IDX_TRANSACTION_MASTER_PURCHASE_003")]
[Index("PurchaseTypeId", Name = "IDX_TRANSACTION_MASTER_PURCHASE_004")]
[Index("TransportTypeId", Name = "IDX_TRANSACTION_MASTER_PURCHASE_005")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public class TbTransactionMasterPurchase
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("transactionID", TypeName = "int(11)")]
    public int TransactionId { get; set; }

    [Column("transactionMasterID", TypeName = "int(11)")]
    public int TransactionMasterId { get; set; }

    [Column("purchaseTypeID", TypeName = "int(11)")]
    public int PurchaseTypeId { get; set; }

    [Column("transportTypeID", TypeName = "int(11)")]
    public int TransportTypeId { get; set; }

    [Key]
    [Column("transactionMasterPurchaseID", TypeName = "int(11)")]
    public int TransactionMasterPurchaseId { get; set; }
}
