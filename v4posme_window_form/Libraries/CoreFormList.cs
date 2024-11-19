using v4posme_window.Views;
using v4posme_window.Views.Box.Report;
using v4posme_window.Views.Box.Share;
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

    public static Form GetFormulario(string path)
    {
        
        if (path == "core_dashboards") 
        {
            return new FormInvoiceBillingList();
        }
        if (path == "app_cxc_customer/index.aspx")
        {
            return new FormCustomerList();
        }
        if (path == "app_inventory_item/index.aspx")
        {
            return new FormInventoryItemList();
        }
        if (path == "app_invoice_billing/index.aspx")
        {
            return new FormInvoiceBillingList();
        }
        if (path == "app_inventory_inputunpost/index.aspx")
        {
            return new FormInventoryInputList();
        }
        if (path == "app_inventory_report/index.aspx")
        {
            return new FormInventoryReport();
        }
        if (path == "app_sales_report/index.aspx")
        {
            return new FormSalesReport();
        }
        if (path == "app_box_report/index.aspx")
        {
            return new FormBoxReport(); 
        }
        if (path == "app_cxc_report/index.aspx")
        {
            return new FormCxcReport();
        }
        if (path == "app_box_share/index.aspx")
        {
            new FormShareList();
        }

        return new FormInvoiceBillingList();
    }

    public static PrincipalForm Principal()
    {
        return _principal ??= new PrincipalForm();
    }
}