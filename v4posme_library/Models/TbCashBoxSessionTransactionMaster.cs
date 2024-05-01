using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.DataAnnotations;
using MySql.EntityFrameworkCore.DataAnnotations;

namespace v4posme_library.Models;

[Table("tb_cash_box_session_transaction_master")]
[Index("CompanyId", Name = "IDX_CASH_BOX_SESSION_TRANSACTION_MASTER_001")]
[Index("BranchId", Name = "IDX_CASH_BOX_SESSION_TRANSACTION_MASTER_002")]
[Index("CashBoxId", Name = "IDX_CASH_BOX_SESSION_TRANSACTION_MASTER_003")]
[Index("CashBoxSessionId", Name = "IDX_CASH_BOX_SESSION_TRANSACTION_MASTER_004")]
[Index("TransactionId", Name = "IDX_CASH_BOX_SESSION_TRANSACTION_MASTER_005")]
[Index("TransactionMasterId", Name = "IDX_CASH_BOX_SESSION_TRANSACTION_MASTER_006")]
[MySQLCharset("latin1")]
[Microsoft.EntityFrameworkCore.MySqlCollation("latin1_swedish_ci")]
public partial class TbCashBoxSessionTransactionMaster
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int BranchId { get; set; }

    [Column("cashBoxID", TypeName = "int(11)")]
    public int CashBoxId { get; set; }

    [Column("cashBoxSessionID", TypeName = "int(11)")]
    public int CashBoxSessionId { get; set; }

    [Column("transactionID", TypeName = "int(11)")]
    public int TransactionId { get; set; }

    [Column("transactionMasterID", TypeName = "int(11)")]
    public int TransactionMasterId { get; set; }

    [Key]
    [Column("cashBoxSessionTransactionMasterID", TypeName = "int(11)")]
    public int CashBoxSessionTransactionMasterId { get; set; }
}
