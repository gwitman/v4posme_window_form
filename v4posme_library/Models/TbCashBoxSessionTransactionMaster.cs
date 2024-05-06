using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_cash_box_session_transaction_master")]
[Index("CompanyId", Name = "IDX_CASH_BOX_SESSION_TRANSACTION_MASTER_001")]
[Index("BranchId", Name = "IDX_CASH_BOX_SESSION_TRANSACTION_MASTER_002")]
[Index("CashBoxId", Name = "IDX_CASH_BOX_SESSION_TRANSACTION_MASTER_003")]
[Index("CashBoxSessionId", Name = "IDX_CASH_BOX_SESSION_TRANSACTION_MASTER_004")]
[Index("TransactionId", Name = "IDX_CASH_BOX_SESSION_TRANSACTION_MASTER_005")]
[Index("TransactionMasterId", Name = "IDX_CASH_BOX_SESSION_TRANSACTION_MASTER_006")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbCashBoxSessionTransactionMaster
{
    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("branchID")]
    public int BranchId { get; set; }

    [Column("cashBoxID")]
    public int CashBoxId { get; set; }

    [Column("cashBoxSessionID")]
    public int CashBoxSessionId { get; set; }

    [Column("transactionID")]
    public int TransactionId { get; set; }

    [Column("transactionMasterID")]
    public int TransactionMasterId { get; set; }

    [Key]
    [Column("cashBoxSessionTransactionMasterID")]
    public int CashBoxSessionTransactionMasterId { get; set; }
}
