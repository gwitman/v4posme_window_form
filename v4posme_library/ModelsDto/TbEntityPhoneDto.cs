namespace v4posme_library.ModelsDto;

public record TbEntityPhoneDto()
{
    public int CompanyId { get; set; }
    public int BranchId { get; set; }
    public int EntityId { get; set; }
    public long EntityPhoneId { get; set; }
    public int? TypeId { get; set; }
    public string? TypeIdDescription { get; set; }
    public string? Number { get; set; }
    public sbyte? IsPrimary { get; set; }
}