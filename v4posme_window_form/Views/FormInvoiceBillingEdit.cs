using DevExpress.XtraReports.Design.ParameterEditor;
using DevExpress.XtraRichEdit.Layout;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomHelper;
using v4posme_window.Interfaz;
using v4posme_window.Libraries;
using v4posme_window.Template;

namespace v4posme_window.Views
{
    public partial class FormInvoiceBillingEdit : Form, IFormTypeEdit
    {
        private int CompanyId { get; set; }
        private int TransactionId { get; set; }
        private int TransactionMasterId { get; set; }

        public FormInvoiceBillingEdit(TypeOpenForm typeOpen, int companyId, int transactionId, int transactionMasterId)
        {
            InitializeComponent();
            CompanyId = companyId;
            TransactionId = transactionId;
            TransactionMasterId = transactionMasterId;

            if (typeOpen == TypeOpenForm.Init)
            {
                PreRender();
            }

            if (typeOpen == TypeOpenForm.Init && transactionMasterId > 0)
            {
                LoadEdit();
            }

            if (typeOpen == TypeOpenForm.Init && transactionMasterId == 0)
            {
                LoadNew();
            }
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
            var c = 0;
        }

        public void LoadNew()
        {
            var c = 0;
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


        public void PreRender()
        {
            var i = 0;
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            var formTypeListSearch = new FormTypeListSearch(33, "SELECCIONAR_ITEM_BILLING_POPUP_INVOICE", true,
                "{warehouseID:4,listPriceID:12,typePriceID:154,currencyID:1}", false, "", 0, 5, "");
            formTypeListSearch.EventoCallBackAceptar_ += EventoCallBackAceptar;
            formTypeListSearch.ShowDialog(this);
        }


        private void EventoCallBackAceptar(dynamic mensaje)
        {
            // Realizar la lógica que desees en el formulario padre
            WebToolsHelper objWebToolsHelper = new WebToolsHelper();
            MessageBox.Show("Evento en el formulario hijo: " +
                            objWebToolsHelper.helper_RequestGetValueObjet(mensaje, "itemID", "0"));
        }

        private void FormInvoiceBillingEdit_Load(object sender, EventArgs e)
        {
            var imagenInvoice = VariablesGlobales.ConfigurationBuilder["PATH_IMAGE_IN_INVOICE_POSME"];
            if (imagenInvoice is not null)
            {
                if (File.Exists(imagenInvoice))
                {
                    pictureEdit2.Image = Image.FromFile(imagenInvoice);
                }
            }
            var imageCustomer = VariablesGlobales.ConfigurationBuilder["PATH_IMAGE_IN_INVOICE_CUSTOMER"];
            if (imageCustomer is not null)
            {
                if (File.Exists(imageCustomer))
                {
                    pictureEdit1.Image = Image.FromFile(imageCustomer);
                }
            }
        }
    }
}