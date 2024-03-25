namespace v4posme_library.ModelsViews;

public class TbRoleAutorizationDto
{
    public int CompanyId { get; set; }
    public int BranchId { get; set; }
    public int RoleId { get; set; }
    public int ComponentAutorizationId { get; set; }
    public string? Name { get; set; }
    public int ComponentId { get; set; }
    public int WorkflowId { get; set; }
    public int WorkflowStageId { get; set; }
}