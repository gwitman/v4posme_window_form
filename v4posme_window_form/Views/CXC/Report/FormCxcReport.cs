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

namespace v4posme_window.Views.CXC.Report
{
    public partial class FormCxcReport : FormTypeReport
    {
        public FormCxcReport()
        {
            InitializeComponent();
            InicializarLista("app_cxc_report");
            Text = "Reportes CXC";
        }
    }
}