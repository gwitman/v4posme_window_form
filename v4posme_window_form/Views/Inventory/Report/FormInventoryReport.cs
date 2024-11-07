using DevExpress.XtraEditors;
using v4posme_library.Models;
using v4posme_window.Libraries;
using v4posme_window.Template;

namespace v4posme_window.Views.Inventory.Report
{
    public partial class FormInventoryReport : FormTypeReport
    {
        public FormInventoryReport()
        {
            InitializeComponent();
            InicializarLista("app_inventory_report");
            Text = "Reportes de Inventario";
        }

    }
}