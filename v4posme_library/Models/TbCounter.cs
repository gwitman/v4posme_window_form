using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_counter")]
[Index("ComponentId", Name = "IDX_COUNTER_001")]
[Index("CompanyId", Name = "IDX_COUNTER_002")]
[Index("BranchId", Name = "IDX_COUNTER_003")]
[Index("ComponentItemId", Name = "IDX_COUNTER_004")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public class TbCounter
{
    [Key]
    [Column("counterID", TypeName = "int(11)")]
    public int CounterId { get; set; }

    [Column("componentID", TypeName = "int(11)")]
    public int ComponentId { get; set; }

    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("branchID", TypeName = "int(11)")]
    public int BranchId { get; set; }

    [Column("componentItemID", TypeName = "int(11)")]
    public int? ComponentItemId { get; set; }

    [Column("initialValue", TypeName = "int(11)")]
    public int? InitialValue { get; set; }

    [Column("currentValue", TypeName = "int(11)")]
    public int? CurrentValue { get; set; }

    [Column("seed", TypeName = "int(11)")]
    public int? Seed { get; set; }

    [Column("serie")]
    [StringLength(10)]
    public string? Serie { get; set; }

    [Column("length", TypeName = "int(11)")]
    public int? Length { get; set; }
}
