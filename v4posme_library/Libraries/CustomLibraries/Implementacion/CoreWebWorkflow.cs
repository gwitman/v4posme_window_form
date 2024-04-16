using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion;

class CoreWebWorkflow(
    IWorkflowStageRelationModel workflowStageRelationModel,
    IComponentModel componentModel,
    IElementModel elementModel,
    ISubElementModel subElementModel,
    ICompanyComponentFlavorModel componentFlavor,
    IRoleModel roleModel,
    IRoleAutorizationModel roleAutorizationModel,
    IWorkflowModel workflowModel)
    : ICoreWebWorkflow
{
    private static readonly string? ElementTypeTable = VariablesGlobales.ConfigurationBuilder["ELEMENT_TYPE_TABLE"];

    private static readonly int CommandVinculate =
        Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_VINCULATE"]);

    private static readonly int CommandEditable =
        Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_EDITABLE"]);

    private static readonly int CommandEditableTotal =
        Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_EDITABLE_TOTAL"]);

    private static readonly int CommandEliminable =
        Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_ELIMINABLE"]);

    private static readonly int CommandAplicable =
        Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["COMMAND_APLICABLE"]);

    private readonly IWorkflowStageModel _workflowStageModel =
        VariablesGlobales.Instance.UnityContainer.Resolve<IWorkflowStageModel>();

    public List<TbWorkflowStage>? GetWorkflowAllStage(string table, string field, int companyId, int branchId,
        int roleId)
    {
        var objElement = elementModel.GetRowByName(table, Convert.ToInt32(ElementTypeTable));
        if (objElement is null)
        {
            throw new Exception($"NO EXISTE LA TABLA '{table}' DENTRO DE LOS REGISTROS DE ELEMENT ");
        }

        var objSubElement = subElementModel.GetRowByNameAndElementId(objElement.ElementId, field);
        if (objSubElement is null)
        {
            throw new Exception(
                $"NO EXISTE EL CAMPO '{field}' DENTRO DE LOS REGISTROS DE SUBELEMENT PARA EL ELEMENTO '{table}' ");
        }

        var objComponent = componentModel.GetRowByName("tb_workflow");
        if (objComponent is null)
        {
            throw new Exception("NO EXISTE EL COMPONENTE 'tb_workflow' DENTROS DE LOS REGISTROS DE 'Component' ");
        }

        if (objSubElement.WorkflowId is null)
        {
            throw new Exception($"EN LA TABLA SUBELEMENT PARA '{field}' NO EXISTE EL WORKFLOW CONFIGURADO");
        }

        var objWorkflow = workflowModel.GetRowByWorkflowId(objSubElement.WorkflowId!.Value);
        if (objWorkflow is null)
        {
            throw new Exception("NO EXISTE EL WORKFLOW ");
        }

        var objCompanyComponentFlavor =
            componentFlavor.GetRowByCompanyAndComponentAndComponentItemId(companyId, objComponent.ComponentId,
                objWorkflow.WorkflowId);
        if (objCompanyComponentFlavor is null)
        {
            throw new Exception("NO EXISTE EL FLAVOR PARA EL COMPONENTE DE WORKFLOW ");
        }

        var objWorkflowStage =
            _workflowStageModel.GetRowByWorkflowIdAndFlavorId(objWorkflow.WorkflowId,
                Convert.ToInt32(objCompanyComponentFlavor.FlavorId));

        var objWorkflowStageRole = roleAutorizationModel.GetRowByRole(companyId, branchId, roleId);
        var objRole = roleModel.GetRowByPk(companyId, branchId, roleId);
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
            return new List<TbWorkflowStage>();
        }

        var aux = new List<TbWorkflowStage>();
        foreach (var stage in objWorkflowStage)
        {
            var exist = false;
            foreach (var unused in objWorkflowStageRole.Where(dto => stage.WorkflowStageId == dto.WorkflowStageId))
            {
                exist = true;
            }

            if (exist)
            {
                aux.Add(stage);
            }
        }

        return aux;
    }

    public List<TbWorkflowStage>? GetWorkflowInitStage(string table, string field, int companyId,
        int branchId, int roleId)
    {
        var objElement = elementModel.GetRowByName(table, Convert.ToInt32(ElementTypeTable));
        if (objElement is null)
        {
            throw new Exception($"NO EXISTE LA TABLA '{table}' DENTRO DE LOS REGISTROS DE ELEMENT ");
        }

        var objSubElement = subElementModel.GetRowByNameAndElementId(objElement.ElementId, field);
        if (objSubElement is null)
        {
            throw new Exception(
                $"NO EXISTE EL CAMPO '{field}' DENTRO DE LOS REGISTROS DE SUBELEMENT PARA EL ELEMENTO 'table' ");
        }

        var objComponent = componentModel.GetRowByName("tb_workflow");
        if (objComponent is null)
        {
            throw new Exception("NO EXISTE EL COMPONENTE 'tb_workflow' DENTROS DE LOS REGISTROS DE 'Component' ");
        }

        if (objSubElement.WorkflowId is null)
        {
            throw new Exception($"EN LA TABLA SUBELEMENT PARA '{field}' NO EXISTE EL WORKFLOW CONFIGURADO");
        }

        var objWorkflow = workflowModel.GetRowByWorkflowId(objSubElement.WorkflowId!.Value);
        if (objWorkflow is null)
        {
            throw new Exception("NO EXISTE EL WORKFLOW ");
        }

        var objCompanyComponentFlavor = componentFlavor.GetRowByCompanyAndComponentAndComponentItemId(
            companyId, objComponent.ComponentId, objWorkflow.WorkflowId);
        if (objCompanyComponentFlavor is null)
        {
            throw new Exception("NO EXISTE EL FLAVOR PARA EL COMPONENTE DE WORKFLOW ");
        }

        var objWorkflowStage =
            _workflowStageModel.GetRowByWorkflowIdAndFlavorIdInit(objWorkflow.WorkflowId,
                Convert.ToInt32(objCompanyComponentFlavor.FlavorId));
        var objWorkflowStageRole = roleAutorizationModel.GetRowByRole(companyId, branchId, roleId);
        var objRole = roleModel.GetRowByPk(companyId, branchId, roleId);
        if (objRole is null)
        {
            throw new Exception("NO EXISTE EL ROL ");
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
            return new List<TbWorkflowStage>();
        }

        var exist = false;
        var count =
            objWorkflowStageRole
                .Where(stageRole => stageRole.WorkflowStageId == objWorkflowStage[0].WorkflowStageId);
        foreach (var unused in count)
        {
            exist = true;
        }

        return exist ? objWorkflowStage : null;
    }

    public List<TbWorkflowStage> GetWorkflowStage(string table, string field, int stageId, int companyId, int branchId,
        int roleId)
    {
        var objElement = elementModel.GetRowByName(table, Convert.ToInt32(ElementTypeTable));
        if (objElement is null)
        {
            throw new Exception($"NO EXISTE LA TABLA '{table}' DENTRO DE LOS REGISTROS DE ELEMENT ");
        }

        var objSubElement = subElementModel.GetRowByNameAndElementId(objElement.ElementId, field);
        if (objSubElement is null)
        {
            throw new Exception(
                $"NO EXISTE EL CAMPO '{field}' DENTRO DE LOS REGISTROS DE SUBELEMENT PARA EL ELEMENTO '{table}' ");
        }

        var objComponent = componentModel.GetRowByName("tb_workflow");
        if (objComponent is null)
        {
            throw new Exception("NO EXISTE EL COMPONENTE 'tb_workflow' DENTROS DE LOS REGISTROS DE 'Component' ");
        }

        if (objSubElement.WorkflowId is null)
        {
            throw new Exception($"EN LA TABLA SUBELEMENT PARA '{field}' NO EXISTE EL WORKFLOW CONFIGURADO");
        }

        var objWorkflow = workflowModel.GetRowByWorkflowId(objSubElement.WorkflowId!.Value);
        if (objWorkflow is null)
        {
            throw new Exception("NO EXISTE EL WORKFLOW ");
        }

        var objCompanyComponentFlavor =
            componentFlavor.GetRowByCompanyAndComponentAndComponentItemId(companyId, objComponent.ComponentId,
                objWorkflow.WorkflowId);
        if (objCompanyComponentFlavor is null)
        {
            throw new Exception("NO EXISTE EL FLAVOR PARA EL COMPONENTE DE WORKFLOW ");
        }

        var objWorkflowStage =
            _workflowStageModel.GetRowByWorkflowStageId(objWorkflow.WorkflowId,
                Convert.ToInt32(objCompanyComponentFlavor.FlavorId),
                stageId);

        var objWorkflowStageRole = roleAutorizationModel.GetRowByRole(companyId, branchId, roleId);

        var objRole = roleModel.GetRowByPk(companyId, branchId, roleId);
        if (objRole is null)
        {
            throw new Exception("NO EXISTE EL ROL ");
        }

        if (objWorkflowStage is null)
        {
            return new List<TbWorkflowStage>();
        }

        if (objRole.IsAdmin!.Value)
        {
            return objWorkflowStage;
        }

        if (objWorkflowStageRole is null)
        {
            return new List<TbWorkflowStage>();
        }

        var aux = new List<TbWorkflowStage>();
        foreach (var stage in objWorkflowStage)
        {
            var exist = false;
            var filter = objWorkflowStageRole.Where(stageRole => stageRole.WorkflowStageId == stage.WorkflowStageId);
            foreach (var unused in filter)
            {
                exist = true;
            }

            if (exist)
            {
                aux.Add(stage);
            }
        }

        return aux;
    }

    public List<TbWorkflowStage>? GetWorkflowStageApplyFirst(string table, string field, int companyId, int branchId,
        int roleId)
    {
        var objElement = elementModel.GetRowByName(table, Convert.ToInt32(ElementTypeTable));
        if (objElement is null)
        {
            throw new Exception($"NO EXISTE LA TABLA '{table}' DENTRO DE LOS REGISTROS DE ELEMENT ");
        }

        var objSubElement = subElementModel.GetRowByNameAndElementId(objElement.ElementId, field);
        if (objSubElement is null)
        {
            throw new Exception(
                $"NO EXISTE EL CAMPO '{field}' DENTRO DE LOS REGISTROS DE SUBELEMENT PARA EL ELEMENTO '{table}' ");
        }

        var objComponent = componentModel.GetRowByName("tb_workflow");
        if (objComponent is null)
        {
            throw new Exception("NO EXISTE EL COMPONENTE 'tb_workflow' DENTROS DE LOS REGISTROS DE 'Component' ");
        }

        if (objSubElement.WorkflowId is null)
        {
            throw new Exception($"EN LA TABLA SUBELEMENT PARA '{field}' NO EXISTE EL WORKFLOW CONFIGURADO");
        }

        var objWorkflow = workflowModel.GetRowByWorkflowId(objSubElement.WorkflowId!.Value);
        if (objWorkflow is null)
        {
            throw new Exception("NO EXISTE EL WORKFLOW ");
        }

        var objCompanyComponentFlavor =
            componentFlavor.GetRowByCompanyAndComponentAndComponentItemId(companyId, objComponent.ComponentId,
                objWorkflow.WorkflowId);
        if (objCompanyComponentFlavor is null)
        {
            throw new Exception("NO EXISTE EL FLAVOR PARA EL COMPONENTE DE WORKFLOW ");
        }

        var objWorkflowStage =
            _workflowStageModel.GetRowByWorkflowIdAndFlavorIdApplyFirst(objWorkflow.WorkflowId,
                Convert.ToInt32(objCompanyComponentFlavor.FlavorId));
        var objWorkflowStageRole = roleAutorizationModel.GetRowByRole(companyId, branchId, roleId);
        var objRole = roleModel.GetRowByPk(companyId, branchId, roleId);
        if (objRole is null)
        {
            throw new Exception("NO EXISTE EL ROL ");
        }

        if (objWorkflowStage is null)
        {
            return new List<TbWorkflowStage>();
        }

        if (objRole.IsAdmin!.Value)
        {
            return objWorkflowStage;
        }

        if (objWorkflowStageRole is null)
        {
            return new List<TbWorkflowStage>();
        }


        var exist = false;
        var filter = objWorkflowStageRole
            .Where(dto => dto.WorkflowStageId == objWorkflowStage[0].WorkflowStageId);
        foreach (var unused in filter)
        {
            exist = true;
        }

        return exist ? objWorkflowStage : null;
    }

    public List<TbWorkflowStage> GetWorkflowStageByStageInit(string table, string field, int startStageId,
        int companyId, int branchId, int roleId)
    {
        var objElement = elementModel.GetRowByName(table, Convert.ToInt32(ElementTypeTable));
        if (objElement is null)
        {
            throw new Exception($"NO EXISTE LA TABLA '{table}' DENTRO DE LOS REGISTROS DE ELEMENT ");
        }

        var objSubElement = subElementModel.GetRowByNameAndElementId(objElement.ElementId, field);
        if (objSubElement is null)
        {
            throw new Exception(
                $"NO EXISTE EL CAMPO '{field}' DENTRO DE LOS REGISTROS DE SUBELEMENT PARA EL ELEMENTO '{table}' ");
        }

        var objComponent = componentModel.GetRowByName("tb_workflow");
        if (objComponent is null)
        {
            throw new Exception("NO EXISTE EL COMPONENTE 'tb_workflow' DENTROS DE LOS REGISTROS DE 'Component' ");
        }

        if (objSubElement.WorkflowId is null)
        {
            throw new Exception($"EN LA TABLA SUBELEMENT PARA '{field}' NO EXISTE EL WORKFLOW CONFIGURADO");
        }

        var objWorkflow = workflowModel.GetRowByWorkflowId(objSubElement.WorkflowId!.Value);
        if (objWorkflow is null)
        {
            throw new Exception("NO EXISTE EL WORKFLOW ");
        }

        var objCompanyComponentFlavor =
            componentFlavor.GetRowByCompanyAndComponentAndComponentItemId(companyId, objComponent.ComponentId,
                objWorkflow.WorkflowId);
        if (objCompanyComponentFlavor is null)
        {
            throw new Exception("NO EXISTE EL FLAVOR PARA EL COMPONENTE DE WORKFLOW ");
        }

        var objWorkflowStage =
            workflowStageRelationModel.GetRowBySourceWorkflowStageId(objWorkflow.WorkflowId,
                Convert.ToInt32(objCompanyComponentFlavor.FlavorId), startStageId);
        var objWorkflowStageRole = roleAutorizationModel.GetRowByRole(companyId, branchId, roleId);
        var objRole = roleModel.GetRowByPk(companyId, branchId, roleId);
        if (objRole is null)
        {
            throw new Exception("NO EXISTE EL ROL ");
        }

        if (objWorkflowStage is null)
        {
            return new List<TbWorkflowStage>();
        }

        if (objRole.IsAdmin!.Value)
        {
            return objWorkflowStage;
        }

        if (objWorkflowStageRole is null)
        {
            return new List<TbWorkflowStage>();
        }

        var aux = new List<TbWorkflowStage>();
        foreach (var stage in objWorkflowStage)
        {
            var exist = false;
            foreach (var unused in objWorkflowStageRole.Where(dto => dto.WorkflowStageId == stage.WorkflowStageId))
            {
                exist = true;
            }

            if (!exist) continue;
            aux.Add(stage);
        }

        return aux;
    }

    public int? ValidateWorkflowStage(string table, string field, int stageId, int cmd, int companyId, int branchId,
        int roleId)
    {
        var objWorkflowStage = GetWorkflowStage(table, field, stageId, companyId, branchId, roleId);
        if (objWorkflowStage.Count <= 0)
        {
            throw new Exception(
                $"NO EXISTE EL WORKFLOW STAGE TABLE: {table} , FIELD:$field , COMPANY: $companyID, WORKFLOWSTAGEID: {stageId} ");
        }

        var tbWorkflowStage = objWorkflowStage[0];

        if (cmd == CommandVinculate)
        {
            return tbWorkflowStage.Vinculable;
        }

        if (cmd == CommandEditable)
        {
            return tbWorkflowStage.EditableParcial;
        }

        if (cmd == CommandEditableTotal)
        {
            return tbWorkflowStage.EditableTotal;
        }

        if (cmd == CommandEliminable)
        {
            return tbWorkflowStage.Eliminable;
        }

        return cmd == CommandAplicable ? tbWorkflowStage.Aplicable : 0;
    }
}