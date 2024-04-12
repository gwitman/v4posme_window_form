using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using v4posme_window.Interfaz;
using v4posme_window.Libraries;
using v4posme_window.Template;

namespace v4posme_window.Views
{
    public partial class FormInvoiceBillingEdit : Form, IFormTypeEdit
    {
        public FormInvoiceBillingEdit()
        {
            InitializeComponent();
        }


        public void ComandDelete()
        {
            throw new NotImplementedException();
        }

        public void ComandPrinter()
        {
            throw new NotImplementedException();
        }

        public void LoadEdit()
        {
            throw new NotImplementedException();
        }

        public void LoadNew()
        {
            throw new NotImplementedException();
        }

        public void SaveInsert()
        {
            throw new NotImplementedException();
        }

        public void SaveUpdate()
        {
            throw new NotImplementedException();
        }

        public static void Delete(int transactionMasterID)
        {
            throw new NotImplementedException();
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }

        private void dateEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void tablePanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            var formTypeListSearch = new FormTypeListSearch();
            formTypeListSearch.ShowDialog(this);
        }

        public void PreRender()
        {
            throw new NotImplementedException();
        }
    }
}
