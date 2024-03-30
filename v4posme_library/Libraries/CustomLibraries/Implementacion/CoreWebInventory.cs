using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion;

class CoreWebInventory : ICoreWebInventory
{
    private readonly IBdModel _bdModel = VariablesGlobales.Instance.UnityContainer.Resolve<IBdModel>();

    public void CalculateKardexNewInput(int companyId, int transactionId, int transactionMasterId)
    {
        _bdModel.ExecuteRender<int>(
            $"CALL pr_inventory_calculate_kardex_new_input ({companyId},{transactionId},{transactionMasterId}); ");
    }

    public void CalculateKardexNewOutput(int companyId, int transactionId, int transactionMasterId)
    {
        _bdModel.ExecuteRender<int>(
            $"CALL pr_inventory_calculate_kardex_new_output ({companyId},{transactionId},{transactionMasterId}); ");
    }
}