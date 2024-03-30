using DevExpress.XtraEditors;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
namespace v4posme_window_form.views
{
    public partial class LoginForm : XtraForm
    {
        private const string UsuarioTitulo = "Usuario";

        public LoginForm()
        {
            InitializeComponent();
        }

        private void chkPagar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPagar.Checked)
            {
                btnPagar.Visible = true;
                btnPagar.Enabled = true;
                cmbMontoPagar.Visible = true;
            }
            else
            {
                btnPagar.Visible = false;
                btnPagar.Enabled = false;
                cmbMontoPagar.Visible = false;
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private async void btnIngresar_Click(object sender, EventArgs e)
        {
            progressPanel.Visible = true;
            await Task.Run(async () =>
            {
                var usuarioService = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebAuthentication>();
                if (string.IsNullOrEmpty(txtUsuario.Text))
                {
                    dxErrorProvider.SetError(txtUsuario, "Debe especificar un usuario para continuar.");
                }
                else
                {
                    dxErrorProvider.SetError(txtUsuario, "");
                }
                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    dxErrorProvider.SetError(txtPassword, "Debe especificar una contraseña para continuar.");
                }
                else
                {
                    dxErrorProvider.SetError(txtPassword, "");
                }
                if (string.IsNullOrEmpty(txtUsuario.Text))
                {
                    XtraMessageBox.Show("Debe especificar un usuario para continuar",
                        UsuarioTitulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsuario.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    XtraMessageBox.Show("Debe especificar una contraseña para continuar",
                        UsuarioTitulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Focus();
                    return;
                }
                var nickname = txtUsuario.Text;
                var password = txtPassword.Text;
                VariablesGlobales.Instance.User = usuarioService.GetUserByNickname(nickname);
                if (VariablesGlobales.Instance.User == null)
                {
                    XtraMessageBox.Show("Nombre de usuario no registrado, intente nuevamente",
                        UsuarioTitulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //txtUsuario.Focus();
                    return;
                }
                VariablesGlobales.Instance.User = usuarioService.GetUserByPasswordAndNickname(nickname, password);
                if (VariablesGlobales.Instance.User == null)
                {
                    XtraMessageBox.Show("Contraseña incorrecta, intente nuevamente",
                        UsuarioTitulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    /*progressPanel.Visible = false;
                    txtPassword.Focus();*/
                    return;
                }
                if (VariablesGlobales.Instance.MessageLogin.Length > 0)
                {
                    XtraMessageBox.Show(VariablesGlobales.Instance.MessageLogin, "PosMe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                /*XtraMessageBox.Show("Credenciales ingresada correctamente.",
                    UsuarioTitulo, MessageBoxButtons.OK, MessageBoxIcon.Information);*/
                var logger = new Logger();
                logger.Log("Usuario logeado al sistema: " + VariablesGlobales.Instance.User.Nickname);
                DialogResult = DialogResult.OK;
            });
            progressPanel.Visible = false;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            //pictureEdit1.Image = Resources.PosMeLogo;
            txtUsuario.Focus();
        }

        private void txtUsuario_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsuario.Text))
            {
                dxErrorProvider.SetError(txtUsuario, "");
            }
        }

        private void txtPassword_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Text))
            {
                dxErrorProvider.SetError(txtPassword, "");
            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ((char)Keys.Enter))
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ((char)Keys.Enter))
            {
                btnIngresar.Focus();
            }
        }

        private void ultraPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
