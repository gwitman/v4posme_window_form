using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using v4posme_window.Template;

namespace v4posme_window.Views.Box.Report
{
    public partial class FormBoxReport : FormTypeReport
    {
        public FormBoxReport()
        {
            InitializeComponent();
            InicializarLista("app_box_report");
            Text = "Reportes de Caja";
        }
    }
}