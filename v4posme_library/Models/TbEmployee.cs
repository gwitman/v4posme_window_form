using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace v4posme_library.Models;

[Table("tb_employee")]
[Index("CompanyId", Name = "IDX_EMPLOYEE_001")]
[Index("BranchId", Name = "IDX_EMPLOYEE_002")]
[Index("EntityId", Name = "IDX_EMPLOYEE_003")]
[Index("EmployeNumber", Name = "IDX_EMPLOYEE_004")]
[Index("NumberIdentification", Name = "IDX_EMPLOYEE_005")]
[Index("IdentificationTypeId", Name = "IDX_EMPLOYEE_006")]
[Index("CountryId", Name = "IDX_EMPLOYEE_007")]
[Index("StateId", Name = "IDX_EMPLOYEE_008")]
[Index("DepartamentId", Name = "IDX_EMPLOYEE_009")]
[Index("AreaId", Name = "IDX_EMPLOYEE_010")]
[Index("ClasificationId", Name = "IDX_EMPLOYEE_011")]
[Index("CategoryId", Name = "IDX_EMPLOYEE_012")]
[Index("TypeEmployeeId", Name = "IDX_EMPLOYEE_013")]
[Index("ParentEmployeeId", Name = "IDX_EMPLOYEE_014")]
[MySqlCharSet("latin1")]
[MySqlCollation("latin1_swedish_ci")]
public partial class TbEmployee
{
    [Key]
    [Column("employeeID")]
    public int EmployeeId { get; set; }

    [Column("companyID")]
    public int CompanyId { get; set; }

    [Column("branchID")]
    public int BranchId { get; set; }

    [Column("entityID")]
    public int EntityId { get; set; }

    [Column("employeNumber")]
    [StringLength(250)]
    public string EmployeNumber { get; set; } = null!;

    [Column("numberIdentification")]
    [StringLength(250)]
    public string? NumberIdentification { get; set; }

    [Column("identificationTypeID")]
    public int? IdentificationTypeId { get; set; }

    [Column("socialSecurityNumber")]
    [StringLength(250)]
    public string? SocialSecurityNumber { get; set; }

    [Column("address")]
    [StringLength(250)]
    public string? Address { get; set; }

    [Column("countryID")]
    public int? CountryId { get; set; }

    [Column("stateID")]
    public int? StateId { get; set; }

    [Column("cityID")]
    public int? CityId { get; set; }

    [Column("departamentID")]
    public int? DepartamentId { get; set; }

    [Column("areaID")]
    public int? AreaId { get; set; }

    [Column("clasificationID")]
    public int? ClasificationId { get; set; }

    [Column("categoryID")]
    public int? CategoryId { get; set; }

    [Column("reference1")]
    [StringLength(250)]
    public string? Reference1 { get; set; }

    [Column("reference2")]
    [StringLength(250)]
    public string? Reference2 { get; set; }

    [Column("typeEmployeeID")]
    public int? TypeEmployeeId { get; set; }

    [Column("hourCost")]
    [Precision(18, 4)]
    public decimal HourCost { get; set; }

    [Column("comissionPorcentage")]
    [Precision(18, 4)]
    public decimal ComissionPorcentage { get; set; }

    [Column("parentEmployeeID")]
    public int? ParentEmployeeId { get; set; }

    [Column("startOn")]
    public DateOnly? StartOn { get; set; }

    [Column("endOn")]
    public DateOnly? EndOn { get; set; }

    [Column("statusID")]
    public int? StatusId { get; set; }

    [Column("createdOn", TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    [Column("createdIn")]
    [StringLength(250)]
    public string? CreatedIn { get; set; }

    [Column("createdAt")]
    public int? CreatedAt { get; set; }

    [Column("createdBy")]
    public int? CreatedBy { get; set; }

    [Column("isActive", TypeName = "bit(1)")]
    public ulong? IsActive { get; set; }
}
