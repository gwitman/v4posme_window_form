using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_list_price")]
[Index("CompanyId", Name = "IDX_LIST_PRICE_001")]
[Index("StatusId", Name = "IDX_LIST_PRICE_002")]
[Index("CompanyId", "StartOn", "EndOn", "IsActive", Name = "IDX_LIST_PRICE_003")]
[Index("ListPriceId", Name = "IDX_LIST_PRICE_004")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbListPrice
{
    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Key]
    [Column("listPriceID", TypeName = "int(11)")]
    public int ListPriceId { get; set; }

    [Column("startOn", TypeName = "datetime")]
    public DateTime StartOn { get; set; }

    [Column("endOn", TypeName = "datetime")]
    public DateTime? EndOn { get; set; }

    [Column("name")]
    [StringLength(200)]
    public string Name { get; set; } = null!;

    [Column("description")]
    [StringLength(550)]
    public string? Description { get; set; }

    [Column("statusID", TypeName = "int(11)")]
    public int StatusId { get; set; }

    [Column("createdOn", TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [Column("createdIn")]
    [StringLength(50)]
    public string CreatedIn { get; set; } = null!;

    [Column("createdBy", TypeName = "int(11)")]
    public int CreatedBy { get; set; }

    [Column("createdAt", TypeName = "int(11)")]
    public int CreatedAt { get; set; }

    [Column("isActive", TypeName = "bit(1)")]
    public ulong IsActive { get; set; }
}
