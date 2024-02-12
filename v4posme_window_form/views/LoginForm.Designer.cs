namespace v4posme_window_form.views
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
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(v4posme_window_form.views.LoginForm));
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.ultraPanel1 = new Infragistics.Win.Misc.UltraPanel();
            this.progressPanel = new DevExpress.XtraWaitForm.ProgressPanel();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.btnIngresar = new DevExpress.XtraEditors.SimpleButton();
            this.btnPagar = new DevExpress.XtraEditors.SimpleButton();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.txtUsuario = new DevExpress.XtraEditors.TextEdit();
            this.chkPagar = new DevExpress.XtraEditors.CheckEdit();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.ultraPanel1.ClientArea.SuspendLayout();
            this.ultraPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsuario.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPagar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            this.SuspendLayout();
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 26);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(552, 82);
            this.dockPanel1_Container.TabIndex = 0;
            appearance1.BackColor = System.Drawing.Color.White;
            this.ultraPanel1.Appearance = appearance1;
            this.ultraPanel1.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraPanel1.ClientArea.Controls.Add(this.progressPanel);
            this.ultraPanel1.ClientArea.Controls.Add(this.btnCancelar);
            this.ultraPanel1.ClientArea.Controls.Add(this.btnIngresar);
            this.ultraPanel1.ClientArea.Controls.Add(this.btnPagar);
            this.ultraPanel1.ClientArea.Controls.Add(this.txtPassword);
            this.ultraPanel1.ClientArea.Controls.Add(this.txtUsuario);
            this.ultraPanel1.ClientArea.Controls.Add(this.chkPagar);
            this.ultraPanel1.ClientArea.Controls.Add(this.pictureEdit1);
            this.ultraPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraPanel1.Location = new System.Drawing.Point(0, 0);
            this.ultraPanel1.Name = "ultraPanel1";
            this.ultraPanel1.Size = new System.Drawing.Size(507, 512);
            this.ultraPanel1.TabIndex = 25;
            this.progressPanel.AnimationSpeed = 2;
            this.progressPanel.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.progressPanel.Appearance.Options.UseBackColor = true;
            this.progressPanel.Caption = "Cargando usuario";
            this.progressPanel.ContentAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.progressPanel.Description = "Buscando recursos de usuario";
            this.progressPanel.Location = new System.Drawing.Point(90, 186);
            this.progressPanel.Name = "progressPanel";
            this.progressPanel.Size = new System.Drawing.Size(312, 141);
            this.progressPanel.TabIndex = 30;
            this.progressPanel.Visible = false;
            this.btnCancelar.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(23)))), ((int)(((byte)(10)))));
            this.btnCancelar.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Appearance.Options.UseBackColor = true;
            this.btnCancelar.Appearance.Options.UseForeColor = true;
            this.btnCancelar.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(96)))), ((int)(((byte)(54)))));
            this.btnCancelar.AppearanceHovered.Options.UseBackColor = true;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btnCancelar.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnCancelar.ImageOptions.SvgImage")));
            this.btnCancelar.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.btnCancelar.Location = new System.Drawing.Point(301, 423);
            this.btnCancelar.LookAndFeel.SkinName = "Office 2016 Colorful";
            this.btnCancelar.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(101, 29);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click_1);
            this.btnIngresar.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(88)))), ((int)(((byte)(52)))));
            this.btnIngresar.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnIngresar.Appearance.Options.UseBackColor = true;
            this.btnIngresar.Appearance.Options.UseForeColor = true;
            this.btnIngresar.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(40)))), ((int)(((byte)(29)))));
            this.btnIngresar.AppearanceHovered.Options.UseBackColor = true;
            this.btnIngresar.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btnIngresar.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnIngresar.ImageOptions.SvgImage")));
            this.btnIngresar.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.CommonPalette;
            this.btnIngresar.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.btnIngresar.Location = new System.Drawing.Point(90, 423);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(111, 29);
            this.btnIngresar.TabIndex = 3;
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);
            this.btnPagar.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.btnPagar.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(121)))), ((int)(((byte)(145)))));
            this.btnPagar.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnPagar.Appearance.Options.UseBackColor = true;
            this.btnPagar.Appearance.Options.UseForeColor = true;
            this.btnPagar.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(154)))), ((int)(((byte)(134)))));
            this.btnPagar.AppearanceHovered.Options.UseBackColor = true;
            this.btnPagar.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btnPagar.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnPagar.ImageOptions.SvgImage")));
            this.btnPagar.ImageOptions.SvgImageSize = new System.Drawing.Size(28, 28);
            this.btnPagar.Location = new System.Drawing.Point(194, 349);
            this.btnPagar.Name = "btnPagar";
            this.btnPagar.Size = new System.Drawing.Size(101, 29);
            this.btnPagar.TabIndex = 27;
            this.btnPagar.Text = "Pagar";
            this.btnPagar.ToolTip = "Ir a pagar";
            this.btnPagar.ToolTipTitle = "Pagar";
            this.btnPagar.Visible = false;
            this.txtPassword.EnterMoveNextControl = true;
            this.txtPassword.Location = new System.Drawing.Point(126, 256);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.AdvancedModeOptions.AllowCaretAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.txtPassword.Properties.AdvancedModeOptions.Label = "Contraseña";
            this.txtPassword.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.txtPassword.Properties.ContextImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("txtPassword.Properties.ContextImageOptions.SvgImage")));
            this.txtPassword.Properties.LookAndFeel.SkinName = "WXI";
            this.txtPassword.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.txtPassword.Properties.UseSystemPasswordChar = true;
            this.txtPassword.Size = new System.Drawing.Size(242, 44);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.EditValueChanged += new System.EventHandler(this.txtPassword_EditValueChanged);
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            this.txtUsuario.EnterMoveNextControl = true;
            this.txtUsuario.Location = new System.Drawing.Point(126, 191);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Properties.AdvancedModeOptions.Label = "Usuario";
            this.txtUsuario.Properties.ContextImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("txtUsuario.Properties.ContextImageOptions.SvgImage")));
            this.txtUsuario.Properties.LookAndFeel.SkinName = "WXI";
            this.txtUsuario.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.txtUsuario.Size = new System.Drawing.Size(242, 44);
            this.txtUsuario.TabIndex = 0;
            this.txtUsuario.EditValueChanged += new System.EventHandler(this.txtUsuario_EditValueChanged);
            this.txtUsuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUsuario_KeyPress);
            this.chkPagar.Location = new System.Drawing.Point(204, 309);
            this.chkPagar.Name = "chkPagar";
            this.chkPagar.Properties.Caption = "Pagar";
            this.chkPagar.Size = new System.Drawing.Size(80, 18);
            this.chkPagar.TabIndex = 28;
            this.chkPagar.CheckedChanged += new System.EventHandler(this.chkPagar_CheckedChanged);
            this.pictureEdit1.Location = new System.Drawing.Point(107, 11);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit1.Size = new System.Drawing.Size(295, 128);
            this.pictureEdit1.TabIndex = 22;
            this.dxErrorProvider.ContainerControl = this;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6, 13);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 512);
            this.ControlBox = false;
            this.Controls.Add(this.ultraPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("LoginForm.IconOptions.Icon")));
            this.LookAndFeel.SkinName = "Office 2019 Colorful";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Iniciar Sesión";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ultraPanel1.ClientArea.ResumeLayout(false);
            this.ultraPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsuario.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPagar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            this.ResumeLayout(false);
        }
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;

        #endregion

        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private Infragistics.Win.Misc.UltraPanel ultraPanel1;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.CheckEdit chkPagar;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.TextEdit txtUsuario;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraEditors.SimpleButton btnIngresar;
        private DevExpress.XtraEditors.SimpleButton btnPagar;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        private DevExpress.XtraWaitForm.ProgressPanel progressPanel;
    }
}