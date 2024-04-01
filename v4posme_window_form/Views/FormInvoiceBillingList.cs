using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using v4posme_window.Interfaz;
using v4posme_window.Template;

namespace v4posme_window.Views
{
    public partial class FormInvoiceBillingList : FormTypeList, InterfaceFormTypeList
    {
        public GridView objControlGridView { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public FormInvoiceBillingList()
        {
            InitializeComponent();
        }        

        public void List()
        {
            throw new NotImplementedException();
        }
    }
}
