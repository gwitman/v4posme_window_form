using v4posme_window.Views;

namespace v4posme_window.Libraries;

public static class CoreFormList
{
    private static PrincipalForm? _principal;
    public static Dictionary<string, Form> Formularios()
    {
        var formInvoiceBillingEdit = new FormInvoiceBillingEdit();
        var formInvoiceBillingList = new FormInvoiceBillingList();

        var forms = new Dictionary<string, Form>
        {
            
            { "core_dashboards", formInvoiceBillingList },
            { "app_invoice_billing/index.aspx", formInvoiceBillingList }
            

        };
        return forms;
    }

    public static PrincipalForm Principal()
    {
        return _principal ??= new PrincipalForm();
    }
}