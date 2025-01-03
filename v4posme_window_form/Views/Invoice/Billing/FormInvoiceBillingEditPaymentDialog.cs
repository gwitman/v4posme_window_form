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
    public partial class FormInvoiceBillingEditPaymentDialog : XtraForm
    {
        private readonly FormInvoiceBillingEdit formInvoiceBillingEdit;

        public FormInvoiceBillingEditPaymentDialog()
        {
            InitializeComponent();
            formInvoiceBillingEdit = new FormInvoiceBillingEdit();
        }

        public FormInvoiceBillingEditPaymentDialog(FormInvoiceBillingEdit formInvoiceBillingEdit)
        {
            InitializeComponent();
            this.formInvoiceBillingEdit = formInvoiceBillingEdit;
            txtReceiptAmount.Focus();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            formInvoiceBillingEdit.btnAplicar_Click(sender, e);
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            formInvoiceBillingEdit.btnRegistrar_Click(sender, e);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            formInvoiceBillingEdit.btnNew_Click(sender, e);
        }

        private void txtReceiptAmount_EditValueChanged(object sender, EventArgs e)
        {
            formInvoiceBillingEdit.txtReceiptAmount_EditValueChanged(sender, e);
        }

        private void txtReceiptAmount_KeyDown(object sender, KeyEventArgs e)
        {
            formInvoiceBillingEdit.txtReceiptAmount_KeyDown(sender, e);
        }

        private void txtReceiptAmountDol_EditValueChanged(object sender, EventArgs e)
        {
            formInvoiceBillingEdit.txtReceiptAmountDol_EditValueChanged(sender, e);
        }

        private void txtReceiptAmountDol_KeyDown(object sender, KeyEventArgs e)
        {
            formInvoiceBillingEdit.txtReceiptAmountDol_KeyDown(sender, e);
        }

        private void txtReceiptAmountDol_KeyPress(object sender, KeyPressEventArgs e)
        {
            formInvoiceBillingEdit.txtReceiptAmountDol_KeyPress(sender, e);
        }

        private void txtReceiptAmountDol_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            formInvoiceBillingEdit.txtReceiptAmountDol_PreviewKeyDown(sender, e);
        }

        private void txtReceiptAmountTarjeta_EditValueChanged(object sender, EventArgs e)
        {
            formInvoiceBillingEdit.txtReceiptAmountTarjeta_EditValueChanged(sender, e);
        }

        private void txtReceiptAmountTarjetaDol_EditValueChanged(object sender, EventArgs e)
        {
            formInvoiceBillingEdit.txtReceiptAmountTarjetaDol_EditValueChanged(sender, e);
        }

        private void txtReceiptAmountBank_EditValueChanged(object sender, EventArgs e)
        {
            formInvoiceBillingEdit.txtReceiptAmountBank_EditValueChanged(sender, e);
        }

        private void txtReceiptAmountBankDol_EditValueChanged(object sender, EventArgs e)
        {
            formInvoiceBillingEdit.txtReceiptAmountBankDol_EditValueChanged(sender, e);
        }

        private void txtReceiptAmountPoint_EditValueChanged(object sender, EventArgs e)
        {
            formInvoiceBillingEdit.txtReceiptAmountPoint_EditValueChanged(sender, e);
        }

        private void FormInvoiceBillingEditPaymentDialog_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F4:
                    btnNew_Click(sender, EventArgs.Empty);
                    break;
                case Keys.F5:
                    btnRegistrar_Click(sender, EventArgs.Empty);
                    break;
                case Keys.F6:
                    btnAplicar_Click(sender, EventArgs.Empty);
                    break;
            }
        }
    }
}