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

namespace v4posme_window.Template
{
    public partial class FormTypeWebView : DevExpress.XtraEditors.XtraForm
    {
        public FormTypeWebView()
        {
            InitializeComponent();
        }

        private void FormTypeWebView_Resize(object sender, EventArgs e)
        {
            webView.Size = this.ClientSize - new Size(webView.Location);
        }
    }
}