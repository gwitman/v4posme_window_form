using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_component_element")]
[Index("ElementId", Name = "IDX_COMPONENT_ELEMENT_001")]
[Index("ComponentId", Name = "IDX_COMPONENT_ELEMENT_002")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public partial class TbComponentElement
{
    [Column("elementID")]
    public int ElementId { get; set; }

    [Column("componentID")]
    public int ComponentId { get; set; }

    [Key]
    [Column("componentElementID")]
    public int ComponentElementId { get; set; }
}
