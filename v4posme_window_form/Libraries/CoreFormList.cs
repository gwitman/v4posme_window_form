using v4posme_window.Views;
using v4posme_window.Views.Box.Attendance;
using v4posme_window.Views.Box.CancelDocument;
using v4posme_window.Views.Box.InputCash;
using v4posme_window.Views.Box.Report;
using v4posme_window.Views.Box.Share;
using v4posme_window.Views.Box.ShareCapital;
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

    public static Form GetFormulario(string path) => path switch
    {
        "core_dashboards" => new FormInvoiceBillingList(),
        "app_cxc_customer/index.aspx" => new FormCustomerList(),
        "app_inventory_item/index.aspx" => new FormInventoryItemList(),
        "app_invoice_billing/index.aspx" => new FormInvoiceBillingList(),
        "app_inventory_inputunpost/index.aspx" => new FormInventoryInputList(),
        "app_inventory_report/index.aspx" => new FormInventoryReport(),
        "app_sales_report/index.aspx" => new FormSalesReport(),
        "app_box_report/index.aspx" => new FormBoxReport(),
        "app_cxc_report/index.aspx" => new FormCxcReport(),
        "app_box_share/index.aspx" => new FormShareList(),
        "app_box_sharecapital/index.aspx" => new FormShareCapitalList(),
        "app_box_canceldocument/index.aspx" => new FormCancelDocumentList(),
        "app_box_attendance/index.aspx" => new FormAttendanceList(),
        "app_box_inputcash/index.aspx" => new FormInputCashList(),
        _ => new FormInvoiceBillingList(),
    };

    public static PrincipalForm Principal()
    {
        return _principal ??= new PrincipalForm();
    }
}