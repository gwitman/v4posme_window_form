using v4posme_library.Models;
using v4posme_window.Template;

namespace v4posme_window.Views.CXC.Customer
{
    public partial class FormCustomerEditEmail : FormTypeHeadModal
    {
        public FormCustomerEditEmail()
        {
            InitializeComponent();
            btnAceptar.Click += BtnAceptar_Click;
            btnCancelar.Click += BtnCancelar_Click;
            TbEntityEmail = new TbEntityEmail();
        }

        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
           Close();
        }

        private void BtnAceptar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                dxErrorProvider.SetError(txtEmail, "No puede estar vacio este campo");
                return;
            }

            TbEntityEmail.Email = txtEmail.Text;
            TbEntityEmail.IsPrimary = (sbyte)(txtIsPrimary.Checked ? 1 : 0);
            DialogResult = DialogResult.OK;
        }

        private void FormCustomerEditEmail_Load(object sender, EventArgs e)
        {
            
        }

        public TbEntityEmail TbEntityEmail { get; set; }
    }
}