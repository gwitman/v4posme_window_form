using System.Diagnostics;
using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion;

class CoreWebWorkflow : ICoreWebWorkflow
{
    private static readonly string? ElementTypeTable = VariablesGlobales.ConfigurationBuilder["ELEMENT_TYPE_TABLE"];
    private readonly IComponentModel _componentModel = VariablesGlobales.Instance.UnityContainer.Resolve<IComponentModel>();
    private readonly IElementModel _elementModel = VariablesGlobales.Instance.UnityContainer.Resolve<IElementModel>();
    private readonly ISubElementModel _subElementModel = VariablesGlobales.Instance.UnityContainer.Resolve<ISubElementModel>();

    private readonly ICompanyComponentFlavorModel _componentFlavor =
        VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyComponentFlavorModel>();

    private readonly IRoleModel _roleModel = VariablesGlobales.Instance.UnityContainer.Resolve<IRoleModel>();

    private readonly IRoleAutorizationModel _roleAutorizationModel =
        VariablesGlobales.Instance.UnityContainer.Resolve<IRoleAutorizationModel>();

    private readonly IWorkflowModel _workflowModel = VariablesGlobales.Instance.UnityContainer.Resolve<IWorkflowModel>();

    private readonly IWorkflowStageModel _workflowStageModel =
        VariablesGlobales.Instance.UnityContainer.Resolve<IWorkflowStageModel>();

    public List<TbWorkflowStage>? GetWorkflowAllStage(string table, string field, int companyId, int branchId,
        int roleId)
    {
        var objElement = _elementModel.GetRowByName(table, Convert.ToInt32(ElementTypeTable));
        if (objElement is null)
        {
            throw new Exception($"NO EXISTE LA TABLA '{table}' DENTRO DE LOS REGISTROS DE ELEMENT ");
        }

        var objSubElement = _subElementModel.GetRowByNameAndElementId(objElement.ElementId, field);
        if (objSubElement is null)
        {
            throw new Exception(
                $"NO EXISTE EL CAMPO '{field}' DENTRO DE LOS REGISTROS DE SUBELEMENT PARA EL ELEMENTO '{table}' ");
        }

        var objComponent = _componentModel.GetRowByName("tb_workflow");
        if (objComponent is null)
        {
            throw new Exception("NO EXISTE EL COMPONENTE 'tb_workflow' DENTROS DE LOS REGISTROS DE 'Component' ");
        }

        if (objSubElement.WorkflowId is null)
        {
            throw new Exception($"EN LA TABLA SUBELEMENT PARA '{field}' NO EXISTE EL WORKFLOW CONFIGURADO");
        }

        var objWorkflow = _workflowModel.GetRowByWorkflowId(objSubElement.WorkflowId!.Value);
        if (objWorkflow is null)
        {
            throw new Exception("NO EXISTE EL WORKFLOW ");
        }

        var objCompanyComponentFlavor =
            _componentFlavor.GetRowByCompanyAndComponentAndComponentItemId(companyId, objComponent.ComponentId,
                objWorkflow.WorkflowId);
        if (objCompanyComponentFlavor is null)
        {
            throw new Exception("NO EXISTE EL FLAVOR PARA EL COMPONENTE DE WORKFLOW ");
        }

        var objWorkflowStage =
            _workflowStageModel.GetRowByWorkflowIdAndFlavorId(objWorkflow.WorkflowId,
                Convert.ToInt32(objCompanyComponentFlavor.FlavorId));

        var objWorkflowStageRole = _roleAutorizationModel.GetRowByRole(companyId, branchId, roleId);
        var objRole = _roleModel.GetRowByPk(companyId, branchId, roleId);
        if (objRole is null)
        {
            throw new Exception("NO EXISTE EL ROLE ");
        }

        if (objRole.IsAdmin!.Value)
        {
            return objWorkflowStage;
        }

        if (objWorkflowStageRole is null)
        {
            return new List<TbWorkflowStage>();
        }

        if (objWorkflowStage is null)
        {
            return objWorkflowStage;
        }

        var query = from i in objWorkflowStage
            let exists = objWorkflowStageRole.Any(ii => ii.WorkflowStageId == i.WorkflowStageId)
            where !exists
            select i;
        foreach (var i in query)
        {
            objWorkflowStage.Remove(i);
        }
        return objWorkflowStage;
    }

    public List<TbWorkflowStage> GetWorkflowInitStage(string table, string field, int companyId, int branchId,
        int roleId)
    {
        throw new NotImplementedException();
    }

    public List<TbWorkflowStage> GetWorkflowStage(string table, string field, int stageId, int companyId, int branchId,
        int roleId)
    {
        throw new NotImplementedException();
    }

    public List<TbWorkflowStage> GetWorkflowStageApplyFirst(string table, string field, int companyId, int branchId,
        int roleId)
    {
        throw new NotImplementedException();
    }

    public List<TbWorkflowStage> GetWorkflowStageByStageInit(string table, string field, int startStageId,
        int companyId, int branchId, int roleId)
    {
        throw new NotImplementedException();
    }

    public bool ValidateWorkflowStage(string table, string field, int stageId, string cmd, int companyId, int branchId,
        int roleId)
    {
        throw new NotImplementedException();
    }
}