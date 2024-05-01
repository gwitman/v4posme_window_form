using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_company")]
[Index("FlavorId", Name = "IDX_COMPANY_001")]
[Index("CompanyId", "IsActive", Name = "IDX_COMPANY_002")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public partial class TbCompany
{
    [Key]
    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string? Name { get; set; }

    [Column("address")]
    [StringLength(550)]
    public string? Address { get; set; }

    [Column("createdOn", TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }

    [Column("flavorID")]
    public int FlavorId { get; set; }

    [Column("type")]
    [StringLength(255)]
    public string Type { get; set; } = null!;
}
