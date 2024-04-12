using v4posme_window.Views;

namespace v4posme_window.Libraries;

public static class CoreFormList
{
    private static PrincipalForm? _principal;
    public static Dictionary<string, Form> Formularios()
    {
        var formInvoiceBillingEdit = new FormInvoiceBillingEdit(TypeOpenForm.Init,0,0,0);
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