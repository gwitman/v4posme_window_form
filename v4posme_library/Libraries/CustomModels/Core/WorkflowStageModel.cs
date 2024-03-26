using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class WorkflowStageModel : IWorkflowStageModel
{
    public List<TbWorkflowStage> GetRowByPk(List<TbWorkflowStage> listWorkflowStageRole)
    {
        using var context = new DataContext();
        var query = context.TbWorkflowStages.Select(stage => stage);
        if (listWorkflowStageRole.Count > 0)
        {
            query = listWorkflowStageRole.Aggregate(query,
                (current, item) => current.Where(stage =>
                    stage.ComponentId == item.ComponentId && stage.WorkflowId == item.WorkflowId &&
                    stage.WorkflowStageId == item.WorkflowStageId && stage.IsActive));
        }

        return query.ToList();
    }

    public List<TbWorkflowStage> GetRowByWorkflowIdAndFlavorId(int workflowId, int flavorId)
    {
        using var context = new DataContext();
        return context.TbWorkflowStages
            .Where(stage => stage.WorkflowId == workflowId
                            && stage.FlavorId == flavorId
                            && stage.IsActive)
            .ToList();
    }

    public List<TbWorkflowStage> GetRowByWorkflowStageId(int workflowId, int flavorId, int workflowStageId)
    {
        using var context = new DataContext();
        return context.TbWorkflowStages
            .Where(stage => stage.WorkflowId == workflowId
                            && stage.WorkflowStageId == workflowStageId
                            && stage.FlavorId == flavorId
                            && stage.IsActive)
            .ToList();
    }

    public List<TbWorkflowStage> GetRowByWorkflowStageIdOnly(int workflowStageId)
    {
        using var context = new DataContext();
        return context.TbWorkflowStages
            .Where(stage => stage.WorkflowStageId == workflowStageId
                            && stage.IsActive)
            .ToList();
    }

    public List<TbWorkflowStage> GetRowByWorkflowIdAndFlavorIdInit(int workflowId, int flavorId)
    {
        using var context = new DataContext();
        return context.TbWorkflowStages
            .Where(stage => stage.WorkflowId == workflowId
                            && stage.FlavorId == flavorId
                            && stage.IsActive && stage.IsInit)
            .ToList();
    }

    public List<TbWorkflowStage> GetRowByWorkflowIdAndFlavorIdApplyFirst(int workflowId, int flavorId)
    {
        using var context = new DataContext();
        return context.TbWorkflowStages
            .Where(stage => stage.WorkflowId == workflowId
                            && stage.FlavorId == flavorId
                            && stage.IsActive && stage.Aplicable!.Value)
            .ToList();
    }
}