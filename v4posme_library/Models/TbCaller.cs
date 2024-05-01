using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.DataAnnotations;
using MySql.EntityFrameworkCore.DataAnnotations;

namespace v4posme_library.Models;

[Table("tb_caller")]
[MySQLCharset("latin1")]
[Microsoft.EntityFrameworkCore.MySqlCollation("latin1_general_ci")]
public partial class TbCaller
{
    [Key]
    [Column("callerID", TypeName = "int(11)")]
    public int CallerId { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string? Name { get; set; }

    [Column("description")]
    [StringLength(250)]
    public string? Description { get; set; }
}
