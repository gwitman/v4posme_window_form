using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_workflow_stage")]
[Index("ComponentId", Name = "IDX_WORKFLOW_STAGE_001")]
[Index("WorkflowId", Name = "IDX_WORKFLOW_STAGE_002")]
[Index("WorkflowStageId", Name = "IDX_WORKFLOW_STAGE_003")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public partial class TbWorkflowStage
{
    [Column("componentID", TypeName = "int(11)")]
    public int ComponentId { get; set; }

    [Column("workflowID", TypeName = "int(11)")]
    public int WorkflowId { get; set; }

    [Key]
    [Column("workflowStageID", TypeName = "int(11)")]
    public int WorkflowStageId { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string? Name { get; set; }

    [Column("description")]
    [StringLength(250)]
    public string? Description { get; set; }

    [Column("display")]
    [StringLength(250)]
    public string? Display { get; set; }

    [Column("flavorID", TypeName = "int(11)")]
    public int? FlavorId { get; set; }

    [Column("editableParcial")]
    public int? EditableParcial { get; set; }

    [Column("editableTotal")]
    public int? EditableTotal { get; set; }

    [Column("eliminable")]
    public int? Eliminable { get; set; }

    /// <summary>
    /// Este campo es util para saber si el documento debe de aumentar o disminuir inventario o para saver si el documento debe de ser contabilizado
    /// </summary>
    [Column("aplicable")]
    public int? Aplicable { get; set; }

    [Column("vinculable")]
    public int? Vinculable { get; set; }

    [Column("isActive")]
    public bool IsActive { get; set; }

    [Column("isInit")]
    public int IsInit { get; set; }
}
