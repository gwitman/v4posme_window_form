using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_log")]
[Index("CompanyId", Name = "IDX_LOG_001")]
[Index("BranchId", Name = "IDX_LOG_002")]
[Index("LoginId", Name = "IDX_LOG_003")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbLog
{
    [Key]
    [Column("logID")]
    public int LogId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("branchID")]
    public int BranchId { get; set; }

    [Column("loginID")]
    public int LoginId { get; set; }

    [Column("token")]
    [StringLength(255)]
    public string Token { get; set; } = null!;

    [Column("procedureName")]
    [StringLength(150)]
    public string ProcedureName { get; set; } = null!;

    [Column("code")]
    public int Code { get; set; }

    [Column("description")]
    [StringLength(350)]
    public string Description { get; set; } = null!;

    [Column("createdOn", TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }
}
