namespace v4posme_window.Template
{
    partial class FormTypeHeadModal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTypeHeadModal));
            stackPanel1 = new DevExpress.Utils.Layout.StackPanel();
            btnAceptar = new DevExpress.XtraEditors.SimpleButton();
            btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)stackPanel1).BeginInit();
            stackPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // stackPanel1
            // 
            stackPanel1.Controls.Add(btnAceptar);
            stackPanel1.Controls.Add(btnCancelar);
            stackPanel1.Dock = DockStyle.Top;
            stackPanel1.LayoutDirection = DevExpress.Utils.Layout.StackPanelLayoutDirection.RightToLeft;
            stackPanel1.Location = new Point(0, 0);
            stackPanel1.Name = "stackPanel1";
            stackPanel1.Padding = new Padding(1, 1, 40, 1);
            stackPanel1.Size = new Size(464, 50);
            stackPanel1.TabIndex = 0;
            stackPanel1.UseSkinIndents = true;
            // 
            // btnAceptar
            // 
            btnAceptar.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            btnAceptar.Appearance.ForeColor = Color.White;
            btnAceptar.Appearance.Options.UseBackColor = true;
            btnAceptar.Appearance.Options.UseForeColor = true;
            btnAceptar.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            btnAceptar.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnAceptar.ImageOptions.SvgImage");
            btnAceptar.ImageOptions.SvgImageSize = new Size(16, 16);
            btnAceptar.Location = new Point(347, 8);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(75, 34);
            btnAceptar.TabIndex = 5;
            btnAceptar.Text = "Aceptar";
            // 
            // btnCancelar
            // 
            btnCancelar.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Warning;
            btnCancelar.Appearance.ForeColor = Color.White;
            btnCancelar.Appearance.Options.UseBackColor = true;
            btnCancelar.Appearance.Options.UseForeColor = true;
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            btnCancelar.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnCancelar.ImageOptions.SvgImage");
            btnCancelar.ImageOptions.SvgImageSize = new Size(16, 16);
            btnCancelar.Location = new Point(268, 8);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 34);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            // 
            // FormTypeHeadModal
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(464, 330);
            Controls.Add(stackPanel1);
            Name = "FormTypeHeadModal";
            Text = "FormTypeHeadModal";
            ((System.ComponentModel.ISupportInitialize)stackPanel1).EndInit();
            stackPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.Utils.Layout.StackPanel stackPanel1;
        public DevExpress.XtraEditors.SimpleButton btnAceptar;
        public DevExpress.XtraEditors.SimpleButton btnCancelar;
    }
}