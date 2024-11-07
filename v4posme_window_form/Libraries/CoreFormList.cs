using v4posme_window.Views;
using v4posme_window.Views.Box.Report;
using v4posme_window.Views.CXC.Customer;
using v4posme_window.Views.CXC.Report;
using v4posme_window.Views.Inventory.Inputunpost;
using v4posme_window.Views.Inventory.Product;
using v4posme_window.Views.Inventory.Report;
using v4posme_window.Views.Invoice.Billing;
using v4posme_window.Views.Sales.Report;

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
        var formSalesReport = new FormSalesReport();
        var formBoxReport = new FormBoxReport();
        var formCxcReport = new FormCxcReport();

        var forms = new Dictionary<string, Form>
        {
            { "core_dashboards", formInvoiceBillingList },
            { "app_cxc_customer/index.aspx", formCustomerList },
            { "app_inventory_item/index.aspx", fromInventoryItemList },
            { "app_invoice_billing/index.aspx", formInvoiceBillingList },
            { "app_inventory_inputunpost/index.aspx", formInventoyInput },
            { "app_inventory_report/index.aspx", formInventoryReport },
            { "app_sales_report/index.aspx", formSalesReport },
            { "app_box_report/index.aspx", formBoxReport },
            { "app_cxc_report/index.aspx", formCxcReport },
        };
        return forms;
    }

    public static PrincipalForm Principal()
    {
        return _principal ??= new PrincipalForm();
    }
}