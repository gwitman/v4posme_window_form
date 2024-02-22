using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_workflow_stage_change_log")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public class TbWorkflowStageChangeLog
{
    [Column("componentID", TypeName = "int(11)")]
    public int ComponentId { get; set; }

    [Column("workflowID", TypeName = "int(11)")]
    public int WorkflowId { get; set; }

    [Column("workflowStageID", TypeName = "int(11)")]
    public int WorkflowStageId { get; set; }

    [Key]
    [Column("workflowStageChangeLogID", TypeName = "int(11)")]
    public int WorkflowStageChangeLogId { get; set; }

    [Column("componentItemID", TypeName = "int(11)")]
    public int? ComponentItemId { get; set; }

    [Column("description")]
    [StringLength(250)]
    public string? Description { get; set; }

    [Column("createdOn", TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column("createdBy", TypeName = "int(11)")]
    public int? CreatedBy { get; set; }
}
