using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_naturales")]
[Index("CompanyId", Name = "IDX_NATURALES_001")]
[Index("BranchId", Name = "IDX_NATURALES_002")]
[Index("EntityId", Name = "IDX_NATURALES_003")]
[Index("StatusId", Name = "IDX_NATURALES_004")]
[Index("ProfesionId", Name = "IDX_NATURALES_005")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_general_ci")]
public partial class TbNaturale
{
    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("branchID")]
    public int BranchId { get; set; }

    [Column("entityID")]
    public int EntityId { get; set; }

    [Column("firstName")]
    [StringLength(250)]
    public string? FirstName { get; set; }

    [Column("lastName")]
    [StringLength(250)]
    public string? LastName { get; set; }

    [Column("address")]
    [StringLength(250)]
    public string? Address { get; set; }

    /// <summary>
    /// Catalogo de Estado Civil
    /// </summary>
    [Column("statusID")]
    public int StatusId { get; set; }

    /// <summary>
    /// Catalogo de Profesion u Oficio
    /// </summary>
    [Column("profesionID")]
    public int ProfesionId { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }

    [Key]
    [Column("naturalesID")]
    public int NaturalesId { get; set; }
}
