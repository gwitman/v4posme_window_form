using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using v4posme_window_form.Domain;

namespace v4posme_window_form.views
{
    public partial class LoginForm : DevExpress.XtraEditors.XtraForm
    {
        private const string USUARIO_TITULO = "Usuario";

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void chkPagar_CheckedChanged(object sender, EventArgs e)
        {
            if(chkPagar.Checked) {
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
                    USUARIO_TITULO, MessageBoxButtons.OK,MessageBoxIcon.Error);
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
            var validarUsuario = new ValidarUsuario();
            var usuario = validarUsuario.validarNickName(nickname);
            if(usuario == null)
            {
                XtraMessageBox.Show("Nombre de usuario no registrado, intente nuevamente",
                    USUARIO_TITULO, MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            usuario = validarUsuario.validarUsuario(usuario, password);
            if(usuario == null)
            {
                XtraMessageBox.Show("Contraseña incorrecta, intente nuevamente",
                    USUARIO_TITULO, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var logger = new Logger();
            logger.Log("Usuario logeado al sistema: " + usuario.Nickname);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUsuario.Focus();
        }

        private void txtUsuario_EditValueChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtUsuario.Text))
            {
                dxErrorProvider.SetError(txtUsuario, "");
            }
        }

        private void txtPassword_EditValueChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtPassword.Text))
            {
                dxErrorProvider.SetError(txtPassword, "");
            }
        }
    }
}
