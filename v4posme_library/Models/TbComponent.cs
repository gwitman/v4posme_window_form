using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_component")]
[Index("Name", Name = "IDX_COMPONENT_001")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public partial class TbComponent
{
    [Key]
    [Column("componentID")]
    public int ComponentId { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string? Name { get; set; }
}
