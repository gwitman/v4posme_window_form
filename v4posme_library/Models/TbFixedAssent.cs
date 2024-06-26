﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_fixed_assent")]
[Index("CompanyId", Name = "IDX_FIDEX_ASSENT_001")]
[Index("BranchId", Name = "IDX_FIDEX_ASSENT_002")]
[Index("FixedAssentCode", Name = "IDX_FIDEX_ASSENT_003")]
[Index("ColorId", Name = "IDX_FIDEX_ASSENT_004")]
[Index("AsignedEmployeeId", Name = "IDX_FIDEX_ASSENT_005")]
[Index("CategoryId", Name = "IDX_FIDEX_ASSENT_006")]
[Index("TypeId", Name = "IDX_FIDEX_ASSENT_007")]
[Index("TypeDepresiationId", Name = "IDX_FIDEX_ASSENT_008")]
[Index("StatusId", Name = "IDX_FIDEX_ASSENT_009")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbFixedAssent
{
    [Column("companyID")]
    public int? CompanyId { get; set; }

    [Column("branchID")]
    public int? BranchId { get; set; }

    [Key]
    [Column("fixedAssentID")]
    public int FixedAssentId { get; set; }

    [Column("fixedAssentCode")]
    [StringLength(50)]
    public string? FixedAssentCode { get; set; }

    [Column("name")]
    [StringLength(250)]
    public string? Name { get; set; }

    [Column("description")]
    [StringLength(250)]
    public string? Description { get; set; }

    [Column("modelNumber")]
    [StringLength(50)]
    public string? ModelNumber { get; set; }

    [Column("marca")]
    [StringLength(50)]
    public string? Marca { get; set; }

    [Column("colorID")]
    public int? ColorId { get; set; }

    [Column("chasisNumber")]
    [StringLength(250)]
    public string? ChasisNumber { get; set; }

    [Column("reference1")]
    [StringLength(250)]
    public string? Reference1 { get; set; }

    [Column("reference2")]
    [StringLength(250)]
    public string? Reference2 { get; set; }

    [Column("year")]
    public int? Year { get; set; }

    [Column("asignedEmployeeID")]
    public int? AsignedEmployeeId { get; set; }

    [Column("categoryID")]
    public int? CategoryId { get; set; }

    [Column("typeID")]
    public int? TypeId { get; set; }

    [Column("typeDepresiationID")]
    public int? TypeDepresiationId { get; set; }

    [Column("yearOfUtility")]
    public int? YearOfUtility { get; set; }

    [Column("priceStart")]
    [Precision(28, 8)]
    public decimal? PriceStart { get; set; }

    [Column("isForaneo")]
    public bool IsForaneo { get; set; }

    [Column("statusID")]
    public int StatusId { get; set; }

    [Column("createdIn")]
    [StringLength(50)]
    public string CreatedIn { get; set; } = null!;

    [Column("createdOn", TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [Column("createdAt")]
    public int CreatedAt { get; set; }

    [Column("createdBy")]
    public int CreatedBy { get; set; }

    [Column("isActive", TypeName = "bit(1)")]
    public ulong IsActive { get; set; }
}
