using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion;

class CoreWebTransaction(
    ICoreWebParameter coreWebParameter,
    ITransactionModel transactionModel,
    ICompanyComponentFlavorModel companyComponentFlavor,
    IComponentModel componentModel)
    : ICoreWebTransaction
{
    private readonly ICoreWebTools _coreWebTools = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();

    public int GetCountTransactionBillingAnuladas(int companyId)
    {
        var invoiceAnuladasStatus = coreWebParameter.GetParameter("INVOICE_BILLING_ANULADAS", companyId);
        var objComponent = _coreWebTools.GetComponentIdByComponentName("tb_transaction_master_billing");
        if (objComponent is null)
        {
            throw new Exception("EL COMPONENTE 'tb_transaction_master_billing' NO EXISTE...");
        }

        var transactionId = this.GetTransactionId(companyId, "tb_transaction_master_billing", 0);
        if (transactionId is null)
        {
            throw new Exception("LA TRANSACCION  'tb_transaction_master_billing' NO EXISTE...");
        }

        return transactionModel.GetCounterTransactionMaster(companyId, transactionId!.Value,
            Convert.ToInt32(invoiceAnuladasStatus.Value));
    }

    public int GetCountTransactionBillingCancel(int companyId)
    {
        var invoiceCancelStatus = coreWebParameter.GetParameter("INVOICE_BILLING_CANCEL", companyId);
        var objComponent = _coreWebTools.GetComponentIdByComponentName("tb_transaction_master_billing");
        if (objComponent is null)
        {
            throw new Exception("EL COMPONENTE 'tb_transaction_master_billing' NO EXISTE...");
        }

        var transactionId = GetTransactionId(companyId, "tb_transaction_master_billing", 0);
        if (transactionId is null)
        {
            throw new Exception("LA TRANSACCION  'tb_transaction_master_billing' NO EXISTE...");
        }

        return transactionModel.GetCounterTransactionMaster(companyId, transactionId!.Value,
            Convert.ToInt32(invoiceCancelStatus.Value));
    }

    public int GetDefaultCausalId(int companyId, int transactionId)
    {
        var transactionCausalModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionCausalModel>();
        var objCausal = transactionCausalModel.GetCausalDefaultId(companyId, transactionId);
        if (objCausal is null)
        {
            throw new Exception("NO HAY UN CAUSAL POR DEFECTO PARA LA TRANSACCION");
        }

        return objCausal.TransactionCausalId;
    }

    public void CreateInverseDocumentByTransaccion(int companyIdOriginal, int transactionIdOriginal,
        int transactionMasterIdOriginal, int transactionIdRevert, int transactionMasterIdRevert)
    {
        var bdModel = VariablesGlobales.Instance.UnityContainer.Resolve<IBdModel>();
        object[]? param =
        [
            companyIdOriginal, transactionIdOriginal, transactionMasterIdOriginal, transactionIdRevert,
            transactionMasterIdRevert
        ];
        bdModel.ExecuteRenderWidthParameter(
            "CALL pr_transaction_revert (@companyIdOriginal,@transactionIdOriginal,@transactionMasterIdOriginal, " +
            "@transactionIdRevert,@transactionMasterIdRevert);", param);
    }

    public int? GetTransactionId(int companyId, string componentName, int componentItemId)
    {
        var objComponent = componentModel.GetRowByName(componentName);
        if (objComponent is null)
        {
            throw new Exception($"NO EXISTE EL COMPONENTE '{componentName}' DENTROS DE LOS REGISTROS DE 'Component' ");
        }

        var objCompanyComponentFlavor = companyComponentFlavor
            .GetRowByCompanyAndComponentAndComponentItemId(companyId, objComponent.ComponentId, componentItemId);
        if (objCompanyComponentFlavor is null)
        {
            throw new Exception("NO EXISTE EL FLAVOR PARA EL COMPONENTE DE CATALOGO ");
        }

        return Convert.ToInt32(objCompanyComponentFlavor.FlavorId);
    }

    public TbTransaction? GetTransaction(int companyId, string name)
    {
        return transactionModel.GetRowByPk(companyId, name);
    }

    public TbTransactionConcept GetConcept(int companyId, string transactionName, string conceptName)
    {
        var transactionConceptModel = VariablesGlobales.Instance.UnityContainer.Resolve<ITransactionConceptModel>();
        var objT = transactionModel.GetRowByPk(companyId, transactionName);
        if (objT is null)
        {
            throw new Exception($"NO EXISTE LA TRANSACCION {transactionName}");
        }

        return transactionConceptModel.GetRowByPk(companyId, objT.TransactionId, conceptName);
    }
}