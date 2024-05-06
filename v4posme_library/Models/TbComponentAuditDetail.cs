using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_component_audit_detail")]
[Index("CompanyId", Name = "IDX_COMPONENT_AUDIT_DETAIL_001")]
[Index("BranchId", Name = "IDX_COMPONENT_AUDIT_DETAIL_002")]
[Index("ComponentAuditId", Name = "IDX_COMPONENT_AUDIT_DETAIL_003")]
[Index("FieldId", Name = "IDX_COMPONENT_AUDIT_DETAIL_004")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbComponentAuditDetail
{
    [Key]
    [Column("componentAuditDetailID")]
    public int ComponentAuditDetailId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("branchID")]
    public int BranchId { get; set; }

    [Column("componentAuditID")]
    public int ComponentAuditId { get; set; }

    [Column("fieldID")]
    public int? FieldId { get; set; }

    [Column("oldValue")]
    [StringLength(250)]
    public string? OldValue { get; set; }

    [Column("newValue")]
    [StringLength(250)]
    public string? NewValue { get; set; }

    [Column("note")]
    [StringLength(250)]
    public string? Note { get; set; }
}
