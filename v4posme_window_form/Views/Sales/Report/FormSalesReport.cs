using v4posme_window.Template;

namespace v4posme_window.Views.Sales.Report
{
    public partial class FormSalesReport : FormTypeReport
    {
        public FormSalesReport()
        {
            InitializeComponent();
            InicializarLista("app_sales_report");
            Text = "Reporte de Ventas";
        }
    }
}