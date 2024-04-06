using System.Diagnostics;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraBars.ToastNotifications;
using DevExpress.XtraEditors;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_window.Libraries;

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
            var coreWebRender = new CoreWebRenderInView();
            var userService = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebAuthentication>();
            var userTools = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();
            var coreWebParameter = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebParameter>();
            progressPanel.Visible = true;

            if (string.IsNullOrEmpty(txtUsuario.Text))
            {
                dxErrorProvider.SetError(txtUsuario, "Debe especificar un usuario para continuar.");
                coreWebRender.GetMessageAlert(TypeMessage.Error, "Usuario","Debe especificar un usuario para continuar.", this);
                if (progressPanel.Visible)
                {
                    progressPanel.Visible = false;
                }
                return;
            }
            else
            {
                dxErrorProvider.SetError(txtUsuario, "");
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                dxErrorProvider.SetError(txtPassword, "Debe especificar una contraseña para continuar.");
                coreWebRender.GetMessageAlert(TypeMessage.Error, "Usuario","Debe especificar una contraseña para continuar.", this);
                if (progressPanel.Visible)
                {
                    progressPanel.Visible = false;
                }

                return;
            }
            else
            {
                dxErrorProvider.SetError(txtPassword, "");
            }

            if (!progressPanel.Visible)
            {
                progressPanel.Visible = true;
            }

            var validar = 0;
            await Task.Run(() =>
            {
                var nickname = txtUsuario.Text;
                var password = txtPassword.Text;
                VariablesGlobales.Instance.User = userService.GetUserByNickname(nickname);
                if (VariablesGlobales.Instance.User == null)
                {
                    validar = 1;
                    return;
                }

                VariablesGlobales.Instance.User = userService.GetUserByPasswordAndNickname(nickname, password);
                if (VariablesGlobales.Instance.User is null)
                {
                    validar = 2;
                    return;
                }

                if (VariablesGlobales.Instance.User is null) return;
                
                //si existe el usuario
                userTools.Log($@"Usuario logeado al sistema: {VariablesGlobales.Instance.User.Nickname}, {DateTime.Now.ToLongDateString()}");

                //Obtener Datos
                var pagoCantidadDeMeses = decimal.Zero;
                if (!string.IsNullOrEmpty(cmbMontoPagar.Text))
                {
                    pagoCantidadDeMeses = decimal.Parse(cmbMontoPagar.Text);
                }

                var companyId                       = VariablesGlobales.Instance.User.CompanyId;
                var parameterCantidadTransacciones  = coreWebParameter.GetParameter("CORE_QUANTITY_TRANSACCION", companyId).Value;
                var parameterBalance                = coreWebParameter.GetParameter("CORE_CUST_PRICE_BALANCE", companyId).Value;
                var parameterSendBox                = coreWebParameter.GetParameter("CORE_PAYMENT_SENDBOX", companyId).Value;
                var parameterSendBoxUsuario         = coreWebParameter.GetParameter("CORE_PAYMENT_PRUEBA_USUARIO", companyId).Value;
                var parameterSendBoxClave           = coreWebParameter.GetParameter("CORE_PAYMENT_PRUEBA_CLAVE", companyId).Value;
                var parameterProduccionUsuario      = coreWebParameter.GetParameter("CORE_PAYMENT_PRODUCCION_USUARIO", companyId).Value;
                var parameterProduccionClave        = coreWebParameter.GetParameter("CORE_PAYMENT_PRODUCCION_CLAVE", companyId).Value;
                var parameterPrice                  = coreWebParameter.GetParameter("CORE_CUST_PRICE", companyId).Value;
                var parameterTipoPlan               = coreWebParameter.GetParameter("CORE_CUST_PRICE_TIPO_PLAN", companyId).Value;
                var pagoCantidadMonto               = pagoCantidadDeMeses * decimal.Parse(parameterPrice);

                if (decimal.Compare(pagoCantidadMonto, decimal.Zero) > 0)
                {
                    var url = $@"https://posme.net/core_acount/payment/pagoCantidadDeMeses/{pagoCantidadDeMeses}";
                    Process.Start(new ProcessStartInfo{FileName = url,UseShellExecute = true});
                }

                var subject     = $@"Inicio de session: {nickname}";
                var body        = $@"
                    Estimados Señores de {VariablesGlobales.Instance.Company.Name}
                    En sus manos:
                    Su balance de uso es: {parameterBalance}, Cantidad de Transacciones: {parameterCantidadTransacciones}
                    Fecha {DateTime.Now.ToLongDateString()}
                 ";

                userTools.SendEmail(subject, body);
                DialogResult = DialogResult.OK;
            });
            switch (validar)
            {
                case 1:
                    coreWebRender.GetMessageAlert(TypeMessage.Error, "Error","Nombre de usuario no registrado, intente nuevamente", this);
                    break;
                case 2:
                    coreWebRender.GetMessageAlert(TypeMessage.Error, "Error","Contraseña incorrecta, intente nuevamente", this);
                    break;
                case 0: break;
            }


            if (VariablesGlobales.Instance.MessageLogin is not null)
            {
                coreWebRender.GetMessageAlert(TypeMessage.Error, "posMe", VariablesGlobales.Instance.MessageLogin,this);
            }

            if (progressPanel.Visible)
            {
                progressPanel.Visible = false;
            }

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

        private void ultraPanel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}