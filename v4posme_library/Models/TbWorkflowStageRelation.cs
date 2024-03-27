using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_workflow_stage_relation")]
[Index("ComponentId", Name = "IDX_WOFKFLOW_STAGE_RELATION_001")]
[Index("WorkflowId", Name = "IDX_WOFKFLOW_STAGE_RELATION_002")]
[Index("WorkflowStageId", Name = "IDX_WOFKFLOW_STAGE_RELATION_003")]
[Index("WorkflowStageTargetId", Name = "IDX_WOFKFLOW_STAGE_RELATION_004")]
[Index("WorkflowStageRelationId", Name = "IDX_WOFKFLOW_STAGE_RELATION_005")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public partial class TbWorkflowStageRelation
{
    [Column("componentID", TypeName = "int(11)")]
    public int ComponentId { get; set; }

    [Column("workflowID", TypeName = "int(11)")]
    public int WorkflowId { get; set; }

    [Column("workflowStageID", TypeName = "int(11)")]
    public int WorkflowStageId { get; set; }

    [Column("workflowStageTargetID", TypeName = "int(11)")]
    public int WorkflowStageTargetId { get; set; }

    [Key]
    [Column("workflowStageRelationID", TypeName = "int(11)")]
    public int WorkflowStageRelationId { get; set; }

    [Column("necesitaAuth")]
    public bool? NecesitaAuth { get; set; }

    [Column("AuthRolID", TypeName = "int(11)")]
    public int? AuthRolId { get; set; }
}
