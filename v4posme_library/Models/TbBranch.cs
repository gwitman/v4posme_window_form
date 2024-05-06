using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_branch")]
[Index("CompanyId", Name = "IDX_BRANCH_001")]
[Index("CompanyId", "BranchId", "IsActive", Name = "IDX_BRANCH_002")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public partial class TbBranch
{
    [Column("companyID")]
    public int CompanyId { get; set; }

    [Key]
    [Column("branchID")]
    public int BranchId { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string Name { get; set; } = null!;

    [Column("createdOn", TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }
}
