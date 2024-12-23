using DevExpress.XtraEditors;

namespace v4posme_window.Views
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            progressPanel = new DevExpress.XtraWaitForm.ProgressPanel();
            txtPassword = new TextEdit();
            txtUsuario = new TextEdit();
            pictureEdit1 = new PictureEdit();
            dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(components);
            behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(components);
            toastNotificationsManager = new DevExpress.XtraBars.ToastNotifications.ToastNotificationsManager(components);
            btnCancelar = new SimpleButton();
            btnIngresar = new SimpleButton();
            btnPagar = new SimpleButton();
            chkPagar = new CheckEdit();
            cmbMontoPagar = new ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)txtPassword.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtUsuario.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureEdit1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dxErrorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)behaviorManager1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)toastNotificationsManager).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chkPagar.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbMontoPagar.Properties).BeginInit();
            SuspendLayout();
            // 
            // dockPanel1_Container
            // 
            dockPanel1_Container.Location = new Point(3, 26);
            dockPanel1_Container.Name = "dockPanel1_Container";
            dockPanel1_Container.Size = new Size(552, 82);
            dockPanel1_Container.TabIndex = 0;
            // 
            // progressPanel
            // 
            progressPanel.AnimationSpeed = 2F;
            progressPanel.Appearance.BackColor = Color.Transparent;
            progressPanel.Appearance.Options.UseBackColor = true;
            progressPanel.Caption = "Cargando usuario";
            progressPanel.ContentAlignment = ContentAlignment.MiddleCenter;
            progressPanel.Description = "Buscando recursos de usuario";
            progressPanel.Location = new Point(68, 159);
            progressPanel.Name = "progressPanel";
            progressPanel.Size = new Size(242, 343);
            progressPanel.TabIndex = 30;
            progressPanel.Visible = false;
            // 
            // txtPassword
            // 
            txtPassword.EnterMoveNextControl = true;
            txtPassword.Location = new Point(68, 225);
            txtPassword.Name = "txtPassword";
            txtPassword.Properties.AdvancedModeOptions.Label = "Contraseña";
            txtPassword.Properties.ContextImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("txtPassword.Properties.ContextImageOptions.SvgImage");
            txtPassword.Properties.LookAndFeel.SkinName = "WXI";
            txtPassword.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            txtPassword.Properties.UseSystemPasswordChar = true;
            txtPassword.Size = new Size(242, 44);
            txtPassword.TabIndex = 1;
            txtPassword.EditValueChanged += txtPassword_EditValueChanged;
            txtPassword.KeyPress += txtPassword_KeyPress;
            // 
            // txtUsuario
            // 
            txtUsuario.EnterMoveNextControl = true;
            txtUsuario.Location = new Point(68, 162);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Properties.AdvancedModeOptions.AllowCaretAnimation = DevExpress.Utils.DefaultBoolean.True;
            txtUsuario.Properties.AdvancedModeOptions.Label = "Usuario";
            txtUsuario.Properties.ContextImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("txtUsuario.Properties.ContextImageOptions.SvgImage");
            txtUsuario.Properties.LookAndFeel.SkinName = "WXI";
            txtUsuario.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            txtUsuario.Size = new Size(242, 44);
            txtUsuario.TabIndex = 0;
            txtUsuario.EditValueChanged += txtUsuario_EditValueChanged;
            txtUsuario.KeyPress += txtUsuario_KeyPress;
            // 
            // pictureEdit1
            // 
            pictureEdit1.EditValue = Properties.Resources.posMe_Logo_fondo_blanco;
            pictureEdit1.Location = new Point(82, 12);
            pictureEdit1.Name = "pictureEdit1";
            pictureEdit1.Properties.Appearance.BackColor = Color.Transparent;
            pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            pictureEdit1.Size = new Size(237, 114);
            pictureEdit1.TabIndex = 22;
            // 
            // dxErrorProvider
            // 
            dxErrorProvider.ContainerControl = this;
            // 
            // toastNotificationsManager
            // 
            toastNotificationsManager.ApplicationId = "f0a292da-e4c6-4f91-85c7-1fbdf0473140";
            // 
            // btnCancelar
            // 
            btnCancelar.Appearance.BackColor = Color.FromArgb(227, 23, 10);
            btnCancelar.Appearance.ForeColor = Color.White;
            btnCancelar.Appearance.Options.UseBackColor = true;
            btnCancelar.Appearance.Options.UseForeColor = true;
            btnCancelar.AppearanceHovered.BackColor = Color.FromArgb(225, 96, 54);
            btnCancelar.AppearanceHovered.Options.UseBackColor = true;
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.ImageOptions.SvgImageSize = new Size(24, 24);
            btnCancelar.Location = new Point(68, 463);
            btnCancelar.LookAndFeel.SkinName = "Office 2016 Colorful";
            btnCancelar.LookAndFeel.UseDefaultLookAndFeel = false;
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(242, 39);
            btnCancelar.TabIndex = 33;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click_1;
            // 
            // btnIngresar
            // 
            btnIngresar.Appearance.BackColor = Color.FromArgb(53, 88, 52);
            btnIngresar.Appearance.ForeColor = Color.White;
            btnIngresar.Appearance.Options.UseBackColor = true;
            btnIngresar.Appearance.Options.UseForeColor = true;
            btnIngresar.AppearanceHovered.BackColor = Color.FromArgb(20, 40, 29);
            btnIngresar.AppearanceHovered.Options.UseBackColor = true;
            btnIngresar.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.CommonPalette;
            btnIngresar.ImageOptions.SvgImageSize = new Size(24, 24);
            btnIngresar.Location = new Point(68, 410);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(242, 40);
            btnIngresar.TabIndex = 32;
            btnIngresar.Text = "Ingresar";
            btnIngresar.Click += btnIngresar_Click;
            // 
            // btnPagar
            // 
            btnPagar.Appearance.BackColor = Color.FromArgb(0, 121, 145);
            btnPagar.Appearance.ForeColor = Color.White;
            btnPagar.Appearance.Options.UseBackColor = true;
            btnPagar.Appearance.Options.UseForeColor = true;
            btnPagar.AppearanceHovered.BackColor = Color.FromArgb(67, 154, 134);
            btnPagar.AppearanceHovered.Options.UseBackColor = true;
            btnPagar.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            btnPagar.ImageOptions.SvgImageSize = new Size(28, 28);
            btnPagar.Location = new Point(68, 357);
            btnPagar.Name = "btnPagar";
            btnPagar.Size = new Size(242, 40);
            btnPagar.TabIndex = 34;
            btnPagar.Text = "Pagar";
            btnPagar.ToolTip = "Ir a pagar";
            btnPagar.ToolTipTitle = "Pagar";
            btnPagar.Visible = false;
            btnPagar.Click += btnPagar_Click;
            // 
            // chkPagar
            // 
            chkPagar.Location = new Point(68, 296);
            chkPagar.Name = "chkPagar";
            chkPagar.Properties.Caption = "Pagar";
            chkPagar.Size = new Size(80, 22);
            chkPagar.TabIndex = 35;
            chkPagar.CheckedChanged += chkPagar_CheckedChanged;
            // 
            // cmbMontoPagar
            // 
            cmbMontoPagar.Location = new Point(68, 326);
            cmbMontoPagar.Name = "cmbMontoPagar";
            cmbMontoPagar.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cmbMontoPagar.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            cmbMontoPagar.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            cmbMontoPagar.Properties.Items.AddRange(new object[] { "1", "5", "10", "15", "20", "25", "30", "50" });
            cmbMontoPagar.Size = new Size(242, 28);
            cmbMontoPagar.TabIndex = 36;
            cmbMontoPagar.Visible = false;
            // 
            // LoginForm
            // 
            Appearance.BackColor = Color.White;
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(385, 546);
            ControlBox = false;
            Controls.Add(progressPanel);
            Controls.Add(btnCancelar);
            Controls.Add(btnIngresar);
            Controls.Add(btnPagar);
            Controls.Add(chkPagar);
            Controls.Add(cmbMontoPagar);
            Controls.Add(pictureEdit1);
            Controls.Add(txtPassword);
            Controls.Add(txtUsuario);
            FormBorderStyle = FormBorderStyle.None;
            IconOptions.Icon = (Icon)resources.GetObject("LoginForm.IconOptions.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Iniciar Sesión";
            Load += LoginForm_Load;
            ((System.ComponentModel.ISupportInitialize)txtPassword.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtUsuario.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureEdit1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)dxErrorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)behaviorManager1).EndInit();
            ((System.ComponentModel.ISupportInitialize)toastNotificationsManager).EndInit();
            ((System.ComponentModel.ISupportInitialize)chkPagar.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbMontoPagar.Properties).EndInit();
            ResumeLayout(false);
        }

        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;

        #endregion

        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.TextEdit txtUsuario;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanel;
        private DevExpress.XtraBars.ToastNotifications.ToastNotificationsManager toastNotificationsManager;
        private SimpleButton btnCancelar;
        private SimpleButton btnIngresar;
        private SimpleButton btnPagar;
        private CheckEdit chkPagar;
        private ComboBoxEdit cmbMontoPagar;
    }
}