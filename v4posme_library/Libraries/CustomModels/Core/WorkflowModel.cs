using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class WorkflowModel : IWorkflowModel
{
    public TbWorkflow? GetRowByWorkflowId(int workflowId)
    {
        using var context = new DataContext();
        return context.TbWorkflows.AsNoTracking()
            .FirstOrDefault(workflow => workflow!.WorkflowId == workflowId
                               && workflow.IsActive);
    }
}