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
public partial class TbCounter
{
    [Key]
    [Column("counterID")]
    public int CounterId { get; set; }

    [Column("componentID")]
    public int ComponentId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("branchID")]
    public int BranchId { get; set; }

    [Column("componentItemID")]
    public int? ComponentItemId { get; set; }

    [Column("initialValue")]
    public int? InitialValue { get; set; }

    [Column("currentValue")]
    public int? CurrentValue { get; set; }

    [Column("seed")]
    public int? Seed { get; set; }

    [Column("serie")]
    [StringLength(10)]
    public string? Serie { get; set; }

    [Column("length")]
    public int? Length { get; set; }
}
