using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using v4posme_window_form.Domain;
using v4posme_window_form.Domain.Services;

namespace v4posme_window_form.views
{
    public partial class LoginForm : XtraForm
    {
        private const string USUARIO_TITULO = "Usuario";

        private IUsuarioService usuarioService = new UsuarioService();

        public bool IsOk { get; set; }
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
            }
            else
            {
                btnPagar.Visible = false;
                btnPagar.Enabled = false;
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
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
                    USUARIO_TITULO, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsuario.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                XtraMessageBox.Show("Debe especificar una contraseña para continuar",
                    USUARIO_TITULO, MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return;
            }
            var nickname = txtUsuario.Text;
            var password = txtPassword.Text;
            progressPanel.Visible = true;
            VariablesGlobales.Instance.User = usuarioService.validarNickname(nickname);
            if (VariablesGlobales.Instance.User == null)
            {
                XtraMessageBox.Show("Nombre de usuario no registrado, intente nuevamente",
                    USUARIO_TITULO, MessageBoxButtons.OK, MessageBoxIcon.Error);
                IsOk = false;
                progressPanel.Visible = false;
                txtUsuario.Focus();
                return;
            }
            VariablesGlobales.Instance.User = usuarioService.validar(VariablesGlobales.Instance.User, password);
            if (VariablesGlobales.Instance.User == null)
            {
                XtraMessageBox.Show("Contraseña incorrecta, intente nuevamente",
                    USUARIO_TITULO, MessageBoxButtons.OK, MessageBoxIcon.Error);
                IsOk = false;
                progressPanel.Visible = false;
                txtPassword.Focus();
                return;
            }
            if (!VariablesGlobales.Instance.User.IsActive)
            {
                XtraMessageBox.Show("Usuario no está activo, ingresar con usario activo o comunicarse con el administrador para activar usuario.",
                    USUARIO_TITULO, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                IsOk = false;
                progressPanel.Visible = false;
                return;
            }
            XtraMessageBox.Show("Credenciales ingresada correctamente.",
                    USUARIO_TITULO, MessageBoxButtons.OK, MessageBoxIcon.Information);
            var logger = new Logger();
            logger.Log("Usuario logeado al sistema: " + VariablesGlobales.Instance.User.Nickname);
            progressPanel.Visible = false;
            DialogResult = DialogResult.OK;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
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
    }
}
