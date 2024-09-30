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

namespace v4posme_window.Views
{
    public partial class FormInventoryItemCantidadImprimir : DevExpress.XtraEditors.XtraForm
    {
        public int CantidadImprimir { get; set; }
        public FormInventoryItemCantidadImprimir()
        {
            InitializeComponent();
            CantidadImprimir = 0;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            CantidadImprimir = 0;
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                return;
            }

            CantidadImprimir = Convert.ToInt32(txtCantidad.EditValue);
            DialogResult= DialogResult.OK;
            Close();
        }
    }
}