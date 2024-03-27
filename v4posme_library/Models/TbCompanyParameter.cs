using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_company_parameter")]
[Index("ParameterId", Name = "IDX_COMPANY_PARAMETER_001")]
[Index("CompanyId", Name = "IDX_COMPANY_PARAMETER_002")]
[Index("ParameterId", "CompanyId", Name = "IDX_COMPANY_PARAMETER_003")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbCompanyParameter
{
    [Column("parameterID", TypeName = "int(11)")]
    public int ParameterId { get; set; }

    [Column("companyID", TypeName = "int(11)")]
    public int CompanyId { get; set; }

    [Column("display")]
    [StringLength(250)]
    public string Display { get; set; } = null!;

    [Column("description")]
    [StringLength(250)]
    public string Description { get; set; } = null!;

    [Column("value")]
    [StringLength(300)]
    public string Value { get; set; } = null!;

    [Column("customValue")]
    [StringLength(300)]
    public string CustomValue { get; set; } = null!;

    [Key]
    [Column("companyParameterID", TypeName = "int(11)")]
    public int CompanyParameterId { get; set; }
}
