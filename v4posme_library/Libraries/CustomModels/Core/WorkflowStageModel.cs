using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class WorkflowStageModel : IWorkflowStageModel
{
    public List<TbWorkflowStage> GetRowByPk(List<TbWorkflowStage> listWorkflowStageRole)
    {
        using var context = new DataContext();
        var query = context.TbWorkflowStages.AsNoTracking().Select(stage => stage);
        if (listWorkflowStageRole.Count > 0)
        {
            query = listWorkflowStageRole.Aggregate(query,
                (current, item) => current.Where(stage =>
                    stage.ComponentID == item.ComponentID && stage.WorkflowID == item.WorkflowID &&
                    stage.WorkflowStageID == item.WorkflowStageID && stage.IsActive));
        }

        return query.ToList();
    }

    public List<TbWorkflowStage>? GetRowByWorkflowIdAndFlavorId(int workflowId, int flavorId)
    {
        using var context = new DataContext();
        return context.TbWorkflowStages.AsNoTracking()
            .Where(stage => stage.WorkflowID == workflowId
                            && stage.FlavorID == flavorId
                            && stage.IsActive)
            .ToList();
    }

    public List<TbWorkflowStage>? GetRowByWorkflowStageId(int workflowId, int flavorId, int workflowStageId)
    {
        using var context = new DataContext();
        return context.TbWorkflowStages.AsNoTracking()
            .Where(stage => stage.WorkflowID == workflowId
                            && stage.WorkflowStageID == workflowStageId
                            && stage.FlavorID == flavorId
                            && stage.IsActive)
            .ToList();
    }

    public List<TbWorkflowStage> GetRowByWorkflowStageIdOnly(int workflowStageId)
    {
        using var context = new DataContext();
        return context.TbWorkflowStages.AsNoTracking()
            .Where(stage => stage.WorkflowStageID == workflowStageId
                            && stage.IsActive)
            .ToList();
    }

    public List<TbWorkflowStage>? GetRowByWorkflowIdAndFlavorIdInit(int workflowId, int flavorId)
    {
        using var context = new DataContext();
        return context.TbWorkflowStages.AsNoTracking()
            .Where(stage => stage.WorkflowID == workflowId
                            && stage.FlavorID == flavorId
                            && stage.IsActive && stage.IsInit)
            .ToList();
    }

    public List<TbWorkflowStage>? GetRowByWorkflowIdAndFlavorIdApplyFirst(int workflowId, int flavorId)
    {
        using var context = new DataContext();
        return context.TbWorkflowStages.AsNoTracking()
            .Where(stage => stage.WorkflowID == workflowId
                            && stage.FlavorID == flavorId
                            && stage.IsActive && stage.Aplicable!.Value)
            .ToList();
    }
}