﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_element")]
[Index("ElementTypeId", Name = "IDX_ELEMENT_001")]
[Index("ElementTypeId", "Name", Name = "IDX_ELEMENT_002")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public partial class TbElement
{
    [Key]
    [Column("elementID")]
    public int ElementId { get; set; }

    [Column("elementTypeID")]
    public int ElementTypeId { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string? Name { get; set; }

    [Column("columnAutoIncrement")]
    public int ColumnAutoIncrement { get; set; }
}
