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
using v4posme_window.Libraries;

namespace v4posme_window.Views.Box.Share
{
    public partial class FormShareEditOpcionPrint : DevExpress.XtraEditors.XtraForm
    {
        public ComboBoxItem? Result { get; set; }

        public FormShareEditOpcionPrint()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Result = cmbOpciones.SelectedItem as ComboBoxItem;
                DialogResult = DialogResult.OK;
            }
            catch (Exception exception)
            {
                Result = null;
                DialogResult = DialogResult.Cancel;
            }

            Close();
        }

        private void FormShareEditOpcionPrint_Load(object sender, EventArgs e)
        {
            LoadRender();
        }

        private void LoadRender()
        {
            var listaOpciones = new List<ComboBoxItem>
            {
                new ComboBoxItem("1", "General"),
                new ComboBoxItem("2", "Básico"),
                new ComboBoxItem("3", "Individual"),
                new ComboBoxItem("4", "Fac. Canceladas"),
            };

            cmbOpciones.Properties.Items.AddRange(listaOpciones);
            cmbOpciones.SelectedIndex = 0;
        }
    }
}