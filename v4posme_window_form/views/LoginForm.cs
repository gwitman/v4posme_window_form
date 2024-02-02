using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
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
            //VariablesGlobales.Instance.User = validarUsuario.validarNickName(nickname);
            progressPanel.Visible = true;
            var taskUser = usuarioService.validarNickname(nickname);
            Task.Run(()=>
            {
                VariablesGlobales.Instance.User = taskUser.Result;
            });
            /*VariablesGlobales.Instance.User = usuarioService.validarNickname(nickname);*/
            if(VariablesGlobales.Instance.User == null)
            {
                XtraMessageBox.Show("Nombre de usuario no registrado, intente nuevamente",
                    USUARIO_TITULO, MessageBoxButtons.OK,MessageBoxIcon.Error);
                IsOk=false;
                progressPanel.Visible = false;
                return;
            }
            Task.Run(() =>
            {
                VariablesGlobales.Instance.User = usuarioService.validar(taskUser, password).Result;
            });
            if (VariablesGlobales.Instance.User == null)
            {
                XtraMessageBox.Show("Contraseña incorrecta, intente nuevamente",
                    USUARIO_TITULO, MessageBoxButtons.OK, MessageBoxIcon.Error);
                IsOk =false;
                progressPanel.Visible = false;
                return;
            }
            if (progressPanel.Visible)
            {
                progressPanel.Visible = false;
            }
            XtraMessageBox.Show("Credenciales ingresada correctamente.",
                    USUARIO_TITULO, MessageBoxButtons.OK, MessageBoxIcon.Information);
            var logger = new Logger();
            logger.Log("Usuario logeado al sistema: " + VariablesGlobales.Instance.User.Nickname);
            DialogResult = DialogResult.OK;
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
