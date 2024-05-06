using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_workflow")]
[Index("WorkflowId", Name = "IDX_WORKFLOW_001")]
[Index("ComponentId", Name = "IDX_WORKFLOW_002")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public partial class TbWorkflow
{
    [Key]
    [Column("workflowID")]
    public int WorkflowId { get; set; }

    [Column("componentID")]
    public int ComponentId { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string? Name { get; set; }

    [Column("description")]
    [StringLength(250)]
    public string? Description { get; set; }

    [Column("isActive")]
    public bool IsActive { get; set; }
}
