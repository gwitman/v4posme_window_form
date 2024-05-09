using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_razones_financieras_tmp")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbRazonesFinancierasTmp
{
    [Key]
    [Column("rzID")]
    public int RzId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("branchID")]
    public int BranchId { get; set; }

    [Column("loginID")]
    public int LoginId { get; set; }

    [Column("token")]
    [StringLength(150)]
    public string Token { get; set; } = null!;

    [Column("name")]
    [StringLength(150)]
    public string Name { get; set; } = null!;

    [Column("sequence")]
    [StringLength(5)]
    public string Sequence { get; set; } = null!;

    [Column("value")]
    [Precision(10, 2)]
    public decimal Value { get; set; }

    [Column("simbol")]
    [StringLength(5)]
    public string Simbol { get; set; } = null!;

    [Column("description")]
    [StringLength(500)]
    public string Description { get; set; } = null!;
}
