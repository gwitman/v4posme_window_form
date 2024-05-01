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
    [Column("componentID")]
    public int ComponentId { get; set; }

    [Column("workflowID")]
    public int WorkflowId { get; set; }

    [Key]
    [Column("workflowStageID")]
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

    [Column("flavorID")]
    public int? FlavorId { get; set; }

    [Column("editableParcial")]
    public bool? EditableParcial { get; set; }

    [Column("editableTotal")]
    public bool? EditableTotal { get; set; }

    [Column("eliminable")]
    public bool? Eliminable { get; set; }

    /// <summary>
    /// Este campo es util para saber si el documento debe de aumentar o disminuir inventario o para saver si el documento debe de ser contabilizado
    /// </summary>
    [Column("aplicable")]
    public bool? Aplicable { get; set; }

    [Column("vinculable")]
    public bool? Vinculable { get; set; }

    [Column("isActive")]
    public bool IsActive { get; set; }

    [Column("isInit")]
    public bool IsInit { get; set; }
}
