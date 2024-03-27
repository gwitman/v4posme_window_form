namespace v4posme_library.ModelsDto;

public record TbUserTagDto()
{
    public int TagId { get; set; }
    public int UserId { get; set; }
    public int CompanyId { get; set; }
    public int BranchId { get; set; }
    public string? UserEmail { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ulong? SendEmail { get; set; }
    public ulong? SendNotificationApp { get; set; }
    public ulong? SendSms { get; set; }
    public ulong? IsActive { get; set; }
}