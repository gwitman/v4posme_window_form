using v4posme_window.Views;

namespace v4posme_window.Libraries;

public static class CoreFormList
{
    private static PrincipalForm? _principal;

    public static Dictionary<string, Form> Formularios()
    {
        var formInvoiceBillingList = new FormInvoiceBillingList();
        var fromInventoryItemList = new FormInventoryItemList();
        var formCustomerList = new FormCustomerList();
        var formInventoyInput = new FormInventoryInputList();
        var formInventoryReport = new FormInventoryReport();

        var forms = new Dictionary<string, Form>
        {
            { "core_dashboards", formInvoiceBillingList },
            { "app_cxc_customer/index.aspx", formCustomerList },
            { "app_inventory_item/index.aspx", fromInventoryItemList },
            { "app_invoice_billing/index.aspx", formInvoiceBillingList },
            { "app_inventory_inputunpost/index.aspx", formInventoyInput },
            { "app_inventory_report/index.aspx", formInventoryReport }
        };
        return forms;
    }

    public static PrincipalForm Principal()
    {
        return _principal ??= new PrincipalForm();
    }
}