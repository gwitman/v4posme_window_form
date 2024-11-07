using DevExpress.XtraEditors;
using v4posme_window.Dto;
using v4posme_window.Libraries;

namespace v4posme_window.Views.Inventory.Product
{
    public partial class FormInventoryItemEditConcepts : XtraForm
    {
        public FormInventoryItemEditConcepts()
        {
            InitializeComponent();
            HelperMethods.OnlyNumberDecimals(txtValueIn);
            HelperMethods.OnlyNumberDecimals(txtValueOut);
        }

        public string? NameConcept => txtNameConcept.Text;
        public decimal ValueIn => (decimal)txtValueIn.EditValue;
        public decimal ValueOut => (decimal)txtValueOut.EditValue;

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNameConcept.Text))
            {
                txtNameConcept.ErrorText = "Debe especificar este campo";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtValueIn.Text))
            {
                txtValueIn.ErrorText = "Debe especificar este campo";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtValueOut.Text))
            {
                txtValueOut.ErrorText = "Debe especificar este campo";
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void FormInventoryItemConcepts_Load(object sender, EventArgs e)
        {
            // Specify the type of records stored in the BindingSource.
            bindingSource1.DataSource = typeof(FormInventoryItemEditConceptDTO);
            bindingSource1.Add(new FormInventoryItemEditConceptDTO("", 0, 0));
            txtNameConcept.DataBindings.Add(new Binding("EditValue", bindingSource1, "NameConcept", true));
            txtValueIn.DataBindings.Add(new Binding("EditValue", bindingSource1, "ValueIn", true));
            txtValueOut.DataBindings.Add(new Binding("EditValue", bindingSource1, "ValueOut", true));
            dxErrorProvider1.DataSource = bindingSource1;
            dxErrorProvider1.ContainerControl = this;
        }
    }
}