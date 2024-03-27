namespace v4posme_library.ModelsDto;

public record TbProviderDto()
{
    public int EntityId { get; set; }
    public int CompanyId { get; set; }
    public string? ProviderNumber { get; set; }
    public string? NumberIdentification { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}