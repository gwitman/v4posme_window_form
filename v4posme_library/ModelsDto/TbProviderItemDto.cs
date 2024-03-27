namespace v4posme_library.ModelsDto;

public record TbProviderItemDto()
{
    public int EntityId { get; set; }
    public string? ProviderNumber { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ComercialName { get; set; }
}