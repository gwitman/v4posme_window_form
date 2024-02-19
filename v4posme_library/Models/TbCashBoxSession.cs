using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_cash_box_session")]
[Index("CompanyId", Name = "IDX_CASH_BOX_SESSION_001")]
[Index("BranchId", Name = "IDX_CASH_BOX_SESSION_002")]
[Index("CashBoxId", Name = "IDX_CASH_BOX_SESSION_003")]
[Index("CashBoxSessionId", Name = "IDX_CASH_BOX_SESSION_004")]
[Index("StatusId", Name = "IDX_CASH_BOX_SESSION_005")]
[Index("UserId", Name = "IDX_CASH_BOX_SESSION_006")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbCashBoxSession
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int BranchId { get; set; }

    [Column("cashBoxID", TypeName = "int(11)")]
    public int CashBoxId { get; set; }

    [Key]
    [Column("cashBoxSessionID", TypeName = "int(11)")]
    public int CashBoxSessionId { get; set; }

    [Column("startOn", TypeName = "datetime")]
    public DateTime StartOn { get; set; }

    [Column("endOn", TypeName = "datetime")]
    public DateTime? EndOn { get; set; }

    [Column("statusID", TypeName = "int(11)")]
    public int StatusId { get; set; }

    [Column("isActive", TypeName = "bit(1)")]
    public ulong IsActive { get; set; }

    [Column("userID", TypeName = "int(11)")]
    public int? UserId { get; set; }
}
