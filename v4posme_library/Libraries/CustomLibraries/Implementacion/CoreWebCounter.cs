using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels.Core;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion;

class CoreWebCounter : ICoreWebCounter
{
    private ICounterModel _counterModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICounterModel>();
    private IComponentModel _componentModel = VariablesGlobales.Instance.UnityContainer.Resolve<IComponentModel>();

    public string GetFillNumber(int companyId, int branchId, string componentName, int componentItemId, int number)
    {
        // Obtener componente
        var objComponente = _componentModel.GetRowByName(componentName);
        if (objComponente is null) throw new Exception("NO EXISTE EL COMPONENTE '" + componentName + "'");

        // Obtener el contador
        var objCounter = _counterModel.GetRowByPk(companyId, branchId, objComponente.ComponentId, componentItemId);
        if (objCounter is null) throw new Exception("NO EXISTE EL CONTADOR");

        var value = number.ToString().PadLeft(objCounter.Length!.Value, '0');
        value = objCounter.Serie + value;

        // Retornar valor
        return value;
    }

    public string GetCurrenctNumber(int companyId, int branchId, string componentName, int componentItemId)
    {
        // Obtener componente
        var objComponente = _componentModel.GetRowByName(componentName);
        if (objComponente is null) throw new Exception("NO EXISTE EL COMPONENTE '" + componentName + "'");

        // Obtener el contador
        var objCounter = _counterModel.GetRowByPk(companyId, branchId, objComponente.ComponentId, componentItemId);
        if (objCounter is null) throw new Exception("NO EXISTE EL CONTADOR");

        var value = objCounter.CurrentValue!.Value.ToString().PadLeft(objCounter.Length!.Value, '0');
        value = objCounter.Serie + value;

        // Retornar valor
        return value;
    }

    public string GoNextNumber(int companyId, int branchId, string componentName, int componentItemId)
    {
        // Obtener componente
        var objComponente = _componentModel.GetRowByName(componentName);
        if (objComponente == null)
            throw new Exception("NO EXISTE EL COMPONENTE '" + componentName + "'");

        // Obtener el contador
        var objCounter = _counterModel.GetRowByPk(companyId, branchId, objComponente.ComponentId, componentItemId);
        if (objCounter == null)
            throw new Exception("NO EXISTE EL CONTADOR");

        // Actualizar
        objCounter.CurrentValue = objCounter.CurrentValue!.Value + objCounter.Seed!.Value;
        _counterModel.UpdateAppPosme(companyId, branchId, objComponente.ComponentId, componentItemId, objCounter);

        // Obtener valor
        string value = objCounter.CurrentValue!.Value.ToString().PadLeft(objCounter.Length!.Value, '0');
        value = objCounter.Serie + value;

        // Retornar valor
        return value;
    }
}