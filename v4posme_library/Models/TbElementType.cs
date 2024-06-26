﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_element_type")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public partial class TbElementType
{
    [Key]
    [Column("elementTypeID")]
    public int ElementTypeId { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string? Name { get; set; }

    [Column("description")]
    [StringLength(250)]
    public string? Description { get; set; }
}
