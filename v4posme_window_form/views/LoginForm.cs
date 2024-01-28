using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace v4posme_window_form.views
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void chkPagar_CheckedChanged(object sender, EventArgs e)
        {
            if(chkPagar.Checked) {
                btnPagar.Visible = true;
                btnPagar.Enabled = true;
            }
            else
            {
                btnPagar.Visible = false;
                btnPagar.Enabled = false;
            }
        }
    }
}
