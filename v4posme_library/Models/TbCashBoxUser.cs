﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_cash_box_user")]
[Index("CompanyId", Name = "IDX_CASH_BOX_USER_001")]
[Index("BranchId", Name = "IDX_CASH_BOX_USER_002")]
[Index("UserId", Name = "IDX_CASH_BOX_USER_003")]
[Index("CashBoxId", Name = "IDX_CASH_BOX_USER_004")]
[Index("TypeId", Name = "IDX_CASH_BOX_USER_005")]
[Index("CashBoxUserId", Name = "IDX_CASH_BOX_USER_006")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbCashBoxUser
{
    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("branchID")]
    public int BranchId { get; set; }

    [Column("userID")]
    public int UserId { get; set; }

    [Column("cashBoxID")]
    public int CashBoxId { get; set; }

    [Column("typeID")]
    public int TypeId { get; set; }

    [Key]
    [Column("cashBoxUserID")]
    public int CashBoxUserId { get; set; }
}
