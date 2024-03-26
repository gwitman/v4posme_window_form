using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface IWorkflowStageModel
{
    List<TbWorkflowStage> GetRowByPk(List<TbWorkflowStage> listWorkflowStageRole);
    
    List<TbWorkflowStage> GetRowByWorkflowIdAndFlavorId(int workflowId,int flavorId);
    
    List<TbWorkflowStage> GetRowByWorkflowStageId(int workflowId,int flavorId,int workflowStageId);
    
    List<TbWorkflowStage> GetRowByWorkflowStageIdOnly(int workflowStageId);
    
    List<TbWorkflowStage> GetRowByWorkflowIdAndFlavorIdInit(int workflowId,int flavorId);
    
    List<TbWorkflowStage> GetRowByWorkflowIdAndFlavorIdApplyFirst(int workflowId,int flavorId);
}