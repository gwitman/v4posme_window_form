using v4posme_window.Views;

namespace v4posme_window.Libraries;

public static class CoreFormList
{
    private static Form? _principal;
    public static Dictionary<string, Form> Formularios()
    {
        var formInvoiceBillingEdit = new FormInvoiceBillingEdit();
        var formInvoiceBillingList = new FormInvoiceBillingList();

        var forms = new Dictionary<string, Form>
        {
            { formInvoiceBillingEdit.GetType().Name, formInvoiceBillingEdit },
            { "core_dashboards", formInvoiceBillingList }
            
        };
        return forms;
    }

    public static Form Principal()
    {
        return _principal ??= new PrincipalForm();
    }
}