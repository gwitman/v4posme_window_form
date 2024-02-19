using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_subelement")]
[Index("ElementId", Name = "IDX_SUBELEMENT_001")]
[Index("WorkflowId", Name = "IDX_SUBELEMENT_002")]
[Index("CatalogId", Name = "IDX_SUBELEMENT_003")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public partial class TbSubelement
{
    [Column("elementID", TypeName = "int(11)")]
    public int ElementId { get; set; }

    [Key]
    [Column("subElementID", TypeName = "int(11)")]
    public int SubElementId { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string? Name { get; set; }

    [Column("workflowID", TypeName = "int(11)")]
    public int? WorkflowId { get; set; }

    [Column("catalogID")]
    [StringLength(250)]
    public string? CatalogId { get; set; }
}
