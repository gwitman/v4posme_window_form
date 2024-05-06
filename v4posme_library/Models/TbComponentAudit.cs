using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_component_audit")]
[Index("CompanyId", Name = "IDX_COMPONENT_AUDIT_001")]
[Index("BranchId", Name = "IDX_COMPONENT_AUDIT_002")]
[Index("ElementId", Name = "IDX_COMPONENT_AUDIT_003")]
[Index("ElementItemId", Name = "IDX_COMPONENT_AUDIT_004")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbComponentAudit
{
    [Key]
    [Column("componentAuditID")]
    public int ComponentAuditId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("branchID")]
    public int BranchId { get; set; }

    [Column("elementID")]
    public int? ElementId { get; set; }

    [Column("elementItemID")]
    public int? ElementItemId { get; set; }

    [Column("modifiedOn", TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    [Column("modifiedAt")]
    public int? ModifiedAt { get; set; }

    [Column("modifiedIn")]
    [StringLength(250)]
    public string? ModifiedIn { get; set; }

    [Column("modifiedBy")]
    public int? ModifiedBy { get; set; }
}
