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
using v4posme_window_form.Domain.Services;

namespace v4posme_window_form.Views
{
    public partial class SelectBranchForm : XtraUserControl
    {
        IBranchService branchService=new BranchService();

        public SelectBranchForm()
        {
            InitializeComponent();
        }

        private void SelectBranchForm_Load(object sender, EventArgs e)
        {

        }

        private void loadBranch()
        {
            
        }
    }
}
