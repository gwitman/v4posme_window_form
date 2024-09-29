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
                on new { w.WorkflowID, w.ComponentID } equals new { ws.WorkflowID, ws.ComponentID }
            join wsr in context.TbWorkflowStageRelations.AsNoTracking()
                on new { ws.WorkflowID, ws.WorkflowStageID } equals new { wsr.WorkflowID, wsr.WorkflowStageID }
            join ws2 in context.TbWorkflowStages.AsNoTracking()
                on new { wsr.WorkflowID, WorkflowStageID = wsr.WorkflowStageTargetID } equals new { ws2.WorkflowID, ws2.WorkflowStageID }
            where w.IsActive && ws.IsActive && ws2.IsActive
                  && w.WorkflowID == workflowId
                  && ws.FlavorID == flavorId
                  && ws2.FlavorID == flavorId
                  && ws.WorkflowStageID == sourceStageId
            select ws2;
        return query.ToList();
    }
}