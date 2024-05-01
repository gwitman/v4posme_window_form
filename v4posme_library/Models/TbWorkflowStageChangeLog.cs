using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_workflow_stage_change_log")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public partial class TbWorkflowStageChangeLog
{
    [Column("componentID")]
    public int ComponentId { get; set; }

    [Column("workflowID")]
    public int WorkflowId { get; set; }

    [Column("workflowStageID")]
    public int WorkflowStageId { get; set; }

    [Key]
    [Column("workflowStageChangeLogID")]
    public int WorkflowStageChangeLogId { get; set; }

    [Column("componentItemID")]
    public int? ComponentItemId { get; set; }

    [Column("description")]
    [StringLength(250)]
    public string? Description { get; set; }

    [Column("createdOn", TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column("createdBy")]
    public int? CreatedBy { get; set; }
}
