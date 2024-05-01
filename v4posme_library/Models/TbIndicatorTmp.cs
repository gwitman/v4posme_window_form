using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_indicator_tmp")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbIndicatorTmp
{
    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("branchID")]
    public int BranchId { get; set; }

    [Column("loginID")]
    public int LoginId { get; set; }

    [Column("tokenID")]
    [StringLength(250)]
    public string TokenId { get; set; } = null!;

    [Column("indicadorID")]
    public int IndicadorId { get; set; }

    [Column("value")]
    [Precision(18, 2)]
    public decimal Value { get; set; }

    [Key]
    [Column("indicatorTmpID")]
    public int IndicatorTmpId { get; set; }
}
