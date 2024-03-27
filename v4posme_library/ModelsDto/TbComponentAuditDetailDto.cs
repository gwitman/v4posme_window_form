namespace v4posme_library.ModelsDto;

public class TbComponentAuditDetailDto
{
    public DateTime? ModifiedOn { get; set; }
    public string? Nickname { get; set; }
    public string? Name { get; set; }
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
}