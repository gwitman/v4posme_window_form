namespace v4posme_library.ModelsDto;

public record TbListPriceDto()
{
    public int CompanyId { get; set; }
    public int ListPriceId { get; set; }
    public DateTime StartOn { get; set; }
    public DateTime? EndOn { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int StatusId { get; set; }
    public DateTime CreatedOn { get; set; }
    public string? CreatedIn { get; set; }
    public int CreatedBy { get; set; }
    public int CreatedAt { get; set; }
    public ulong IsActive { get; set; }
    public string? StatusName { get; set; }
}