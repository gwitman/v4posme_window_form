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
    [Column("componentID")]
    public int ComponentId { get; set; }

    [Column("workflowID")]
    public int WorkflowId { get; set; }

    [Column("workflowStageID")]
    public int WorkflowStageId { get; set; }

    [Column("workflowStageTargetID")]
    public int WorkflowStageTargetId { get; set; }

    [Key]
    [Column("workflowStageRelationID")]
    public int WorkflowStageRelationId { get; set; }

    [Column("necesitaAuth")]
    public bool? NecesitaAuth { get; set; }

    [Column("AuthRolID")]
    public int? AuthRolId { get; set; }
}
