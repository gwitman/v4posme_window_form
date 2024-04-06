using DevExpress.XtraEditors;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomLibraries.Interfaz;

namespace v4posme_window.Views
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
            var userService = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebAuthentication>();
            var userTools = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();
            progressPanel.Visible = true;

            if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                dxErrorProvider.SetError(txtUsuario, "Debe especificar un usuario para continuar.");
                if (progressPanel.Visible)
                {
                    progressPanel.Visible = false;
                }
            }
            else
            {
                dxErrorProvider.SetError(txtUsuario, "");
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                dxErrorProvider.SetError(txtPassword, "Debe especificar una contraseña para continuar.");
                if (progressPanel.Visible)
                {
                    progressPanel.Visible = false;
                }
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
                if (progressPanel.Visible)
                {
                    progressPanel.Visible = false;
                }
                //return Task.CompletedTask;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                XtraMessageBox.Show("Debe especificar una contraseña para continuar",
                    UsuarioTitulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                if (progressPanel.Visible)
                {
                    progressPanel.Visible = false;
                }
                //return Task.CompletedTask;
            }
            if (!progressPanel.Visible)
            {
                progressPanel.Visible = true;
            }
            await Task.Run(() =>
            {
                var nickname = txtUsuario.Text;
                var password = txtPassword.Text;
                VariablesGlobales.Instance.User = userService.GetUserByNickname(nickname);
                if (VariablesGlobales.Instance.User == null)
                {
                    XtraMessageBox.Show("Nombre de usuario no registrado, intente nuevamente",
                        UsuarioTitulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                VariablesGlobales.Instance.User = userService.GetUserByPasswordAndNickname(nickname, password);
                if (VariablesGlobales.Instance.User is null)
                {
                    XtraMessageBox.Show("Contraseña incorrecta, intente nuevamente",
                        UsuarioTitulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (VariablesGlobales.Instance.MessageLogin is not null)
                {
                    XtraMessageBox.Show(VariablesGlobales.Instance.MessageLogin, "PosMe", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

                if (VariablesGlobales.Instance.User is null) return;
                userTools.Log("Usuario logeado al sistema: " + VariablesGlobales.Instance.User.Nickname);
                DialogResult = DialogResult.OK;
                //return Task.CompletedTask;
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