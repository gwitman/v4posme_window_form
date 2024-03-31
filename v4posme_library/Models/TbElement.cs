using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_element")]
[Index("ElementTypeId", Name = "IDX_ELEMENT_001")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public partial class TbElement
{
    [Key]
    [Column("elementID", TypeName = "int(11)")]
    public int ElementId { get; set; }

    [Column("elementTypeID", TypeName = "int(11)")]
    public int ElementTypeId { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string? Name { get; set; }

    [Column("columnAutoIncrement", TypeName = "int(11)")]
    public int? ColumnAutoIncrement { get; set; }
}
