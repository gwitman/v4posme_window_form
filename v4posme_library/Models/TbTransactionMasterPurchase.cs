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
public partial class TbTransactionMasterPurchase
{
    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("transactionID")]
    public int TransactionId { get; set; }

    [Column("transactionMasterID")]
    public int TransactionMasterId { get; set; }

    [Column("purchaseTypeID")]
    public int PurchaseTypeId { get; set; }

    [Column("transportTypeID")]
    public int TransportTypeId { get; set; }

    [Key]
    [Column("transactionMasterPurchaseID")]
    public int TransactionMasterPurchaseId { get; set; }
}
