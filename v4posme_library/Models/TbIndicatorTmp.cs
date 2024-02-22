using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_indicator_tmp")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public class TbIndicatorTmp
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int BranchId { get; set; }

    [Column("loginID", TypeName = "int(11)")]
    public int LoginId { get; set; }

    [Column("tokenID")]
    [StringLength(250)]
    public string TokenId { get; set; } = null!;

    [Column("indicadorID", TypeName = "int(11)")]
    public int IndicadorId { get; set; }

    [Column("value")]
    [Precision(18, 2)]
    public decimal Value { get; set; }

    [Key]
    [Column("indicatorTmpID", TypeName = "int(11)")]
    public int IndicatorTmpId { get; set; }
}
