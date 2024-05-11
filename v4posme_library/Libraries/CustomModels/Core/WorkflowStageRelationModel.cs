using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class WorkflowStageRelationModel : IWorkflowStageRelationModel
{
    public List<TbWorkflowStage>? GetRowBySourceWorkflowStageId(int workflowId, int flavorId, int sourceStageId)
    {
        using var context = new DataContext();
        var query = from w in context.TbWorkflows.AsNoTracking() 
            join ws in context.TbWorkflowStages.AsNoTracking()
                on new { w.WorkflowId, w.ComponentId } equals new { ws.WorkflowId, ws.ComponentId }
            join wsr in context.TbWorkflowStageRelations.AsNoTracking()
                on new { ws.WorkflowId, ws.WorkflowStageId } equals new { wsr.WorkflowId, wsr.WorkflowStageId }
            join ws2 in context.TbWorkflowStages.AsNoTracking()
                on new { wsr.WorkflowId, WorkflowStageId = wsr.WorkflowStageTargetId }
                equals new { ws2.WorkflowId, ws2.WorkflowStageId }
            where w.IsActive && ws.IsActive && ws2.IsActive
                  && w.WorkflowId == workflowId
                  && ws.FlavorId == flavorId
                  && ws2.FlavorId == flavorId
                  && ws.WorkflowStageId == sourceStageId
            select ws2;
        return query.ToList();
    }
}