using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface IWorkflowStageRelationModel
{
    List<TbWorkflowStage> GetRowBySourceWorkflowStageId(int workflowId, int flavorId, int sourceStageId);
}