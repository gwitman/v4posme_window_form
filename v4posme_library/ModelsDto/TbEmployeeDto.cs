namespace v4posme_library.ModelsDto;

public record TbEmployeeDto()
{
    public int CompanyId { get; set; }
    public int BranchId { get; set; }
    public int EntityId { get; set; }
    public string? EmployeNumber { get; set; }
    public string? NumberIdentification { get; set; }
    public int? IdentificationTypeId { get; set; }
    public string? SocialSecurityNumber { get; set; }
    public string? Address { get; set; }
    public int? CountryId { get; set; }
    public int? StateId { get; set; }
    public int? CityId { get; set; }
    public int? DepartamentId { get; set; }
    public int? AreaId { get; set; }
    public int? ClasificationId { get; set; }
    public int? CategoryId { get; set; }
    public string? Reference1 { get; set; }
    public string? Reference2 { get; set; }
    public int? TypeEmployeeId { get; set; }
    public decimal HourCost { get; set; }
    public int? ParentEmployeeId { get; set; }
    public DateOnly? StartOn { get; set; }
    public DateOnly? EndOn { get; set; }
    public int? StatusId { get; set; }
    public DateTime? CreatedOn { get; set; }
    public string? CreatedIn { get; set; }
    public int? CreatedAt { get; set; }
    public int? CreatedBy { get; set; }
    public ulong? IsActive { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}