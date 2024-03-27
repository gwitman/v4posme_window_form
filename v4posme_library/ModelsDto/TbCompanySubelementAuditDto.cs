namespace v4posme_library.ModelsDto;

public class TbCompanySubelementAuditDto
{
    public int CompanyId { get; init; }
    public int ElementId { get; init; } 
    public int SubElementId { get; init; } 
    public string? Name { get; init; }
}