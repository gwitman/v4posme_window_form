using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_relationship")]
[Index("EmployeeId", Name = "IDX_RELATIONSHIP_001")]
[Index("CustomerId", Name = "IDX_RELATIONSHIP_002")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbRelationship
{
    [Key]
    [Column("relationshipID", TypeName = "int(11)")]
    public int RelationshipId { get; set; }

    [Column("employeeID", TypeName = "int(11)")]
    public int EmployeeId { get; set; }

    [Column("customerID", TypeName = "int(11)")]
    public int CustomerId { get; set; }

    [Column("startOn")]
    public DateOnly StartOn { get; set; }

    [Column("endOn")]
    public DateOnly EndOn { get; set; }

    [Column("isActive", TypeName = "bit(1)")]
    public ulong IsActive { get; set; }
}
