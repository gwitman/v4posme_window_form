﻿using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace v4posme_window.Views.Invoice.Billing
{
    public partial class FormInvoiceBillingEditHelpDialog : XtraForm
    {
        public FormInvoiceBillingEditHelpDialog()
        {
            InitializeComponent();
        }

        private void FormInvoiceBillingEditHelpDialog_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
            }
        }
    }
}