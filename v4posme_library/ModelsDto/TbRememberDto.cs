namespace v4posme_library.ModelsDto;

public record TbRememberDto()
{
    public int DiaProcesado { get; set; }
    public DateTime Fecha { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? TagId { get; set; }
    public int? LeerFile { get; set; }
}