using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Interfaz;

public interface ICoreWebWorkflow
{
    List<TbWorkflowStage>? GetWorkflowAllStage(string table, string field, int companyId, int branchId, int roleId);

    List<TbWorkflowStage> GetWorkflowInitStage(string table, string field, int companyId, int branchId, int roleId);

    List<TbWorkflowStage> GetWorkflowStage(string table, string field, int stageId, int companyId, int branchId,
        int roleId);

    List<TbWorkflowStage> GetWorkflowStageApplyFirst(string table, string field, int companyId, int branchId,
        int roleId);

    List<TbWorkflowStage> GetWorkflowStageByStageInit(string table, string field, int startStageId,
        int companyId, int branchId, int roleId);
    
    bool ValidateWorkflowStage(string table,string field,int stageId,string cmd,int companyId,int branchId,int roleId);
}