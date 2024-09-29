namespace v4posme_window.Template
{
    partial class FormTypeHeadEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTypeHeadEdit));
            lblTitulo = new DevExpress.XtraEditors.LabelControl();
            progressPanel = new DevExpress.XtraWaitForm.ProgressPanel();
            stackPanel1 = new DevExpress.Utils.Layout.StackPanel();
            stackPanel2 = new DevExpress.Utils.Layout.StackPanel();
            btnImprmir = new DevExpress.XtraEditors.SimpleButton();
            btnGuardar = new DevExpress.XtraEditors.SimpleButton();
            btnEliminar = new DevExpress.XtraEditors.SimpleButton();
            btnNuevo = new DevExpress.XtraEditors.SimpleButton();
            btnRegresar = new DevExpress.XtraEditors.SimpleButton();
            backgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)stackPanel1).BeginInit();
            stackPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)stackPanel2).BeginInit();
            stackPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.Appearance.Font = new Font("Tahoma", 10F, FontStyle.Bold);
            lblTitulo.Appearance.ForeColor = Color.Gray;
            lblTitulo.Appearance.Options.UseFont = true;
            lblTitulo.Appearance.Options.UseForeColor = true;
            lblTitulo.Appearance.Options.UseTextOptions = true;
            lblTitulo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            lblTitulo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
            lblTitulo.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            lblTitulo.ImageOptions.Alignment = ContentAlignment.MiddleLeft;
            lblTitulo.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("lblTitulo.ImageOptions.SvgImage");
            lblTitulo.Location = new Point(5, 2);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Padding = new Padding(3, 0, 0, 0);
            lblTitulo.Size = new Size(75, 36);
            lblTitulo.TabIndex = 67;
            lblTitulo.Text = "Titulo";
            lblTitulo.UseMnemonic = false;
            // 
            // progressPanel
            // 
            progressPanel.AnimationSpeed = 2F;
            progressPanel.Appearance.BackColor = Color.Transparent;
            progressPanel.Appearance.Options.UseBackColor = true;
            progressPanel.Caption = "Procesando";
            progressPanel.ContentAlignment = ContentAlignment.MiddleCenter;
            progressPanel.Description = "Buscando recursos de usuario";
            progressPanel.Location = new Point(0, 0);
            progressPanel.Name = "progressPanel";
            progressPanel.Size = new Size(333, 7);
            progressPanel.TabIndex = 74;
            progressPanel.Visible = false;
            // 
            // stackPanel1
            // 
            stackPanel1.Appearance.BackColor = Color.WhiteSmoke;
            stackPanel1.Appearance.Options.UseBackColor = true;
            stackPanel1.Controls.Add(lblTitulo);
            stackPanel1.Dock = DockStyle.Top;
            stackPanel1.Location = new Point(0, 0);
            stackPanel1.Name = "stackPanel1";
            stackPanel1.Padding = new Padding(2);
            stackPanel1.Size = new Size(862, 41);
            stackPanel1.TabIndex = 73;
            stackPanel1.UseSkinIndents = true;
            // 
            // stackPanel2
            // 
            stackPanel2.Appearance.BackColor = Color.WhiteSmoke;
            stackPanel2.Appearance.BorderColor = Color.FromArgb(64, 64, 64);
            stackPanel2.Appearance.Options.UseBackColor = true;
            stackPanel2.Appearance.Options.UseBorderColor = true;
            stackPanel2.AutoSize = true;
            stackPanel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            stackPanel2.Controls.Add(btnImprmir);
            stackPanel2.Controls.Add(btnGuardar);
            stackPanel2.Controls.Add(btnEliminar);
            stackPanel2.Controls.Add(btnNuevo);
            stackPanel2.Controls.Add(btnRegresar);
            stackPanel2.Dock = DockStyle.Top;
            stackPanel2.LayoutDirection = DevExpress.Utils.Layout.StackPanelLayoutDirection.RightToLeft;
            stackPanel2.Location = new Point(0, 41);
            stackPanel2.Name = "stackPanel2";
            stackPanel2.Padding = new Padding(5);
            stackPanel2.Size = new Size(862, 59);
            stackPanel2.TabIndex = 75;
            stackPanel2.UseSkinIndents = true;
            // 
            // btnImprmir
            // 
            btnImprmir.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Warning;
            btnImprmir.Appearance.ForeColor = Color.White;
            btnImprmir.Appearance.Options.UseBackColor = true;
            btnImprmir.Appearance.Options.UseFont = true;
            btnImprmir.Appearance.Options.UseForeColor = true;
            btnImprmir.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            btnImprmir.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            btnImprmir.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnImprmir.ImageOptions.SvgImage");
            btnImprmir.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.Full;
            btnImprmir.Location = new Point(748, 10);
            btnImprmir.Name = "btnImprmir";
            btnImprmir.Size = new Size(105, 39);
            btnImprmir.TabIndex = 4;
            btnImprmir.Text = "Imprimir";
            // 
            // btnGuardar
            // 
            btnGuardar.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            btnGuardar.Appearance.ForeColor = Color.White;
            btnGuardar.Appearance.Options.UseBackColor = true;
            btnGuardar.Appearance.Options.UseFont = true;
            btnGuardar.Appearance.Options.UseForeColor = true;
            btnGuardar.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            btnGuardar.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            btnGuardar.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnGuardar.ImageOptions.SvgImage");
            btnGuardar.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.Full;
            btnGuardar.Location = new Point(637, 10);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(105, 39);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "Guardar";
            // 
            // btnEliminar
            // 
            btnEliminar.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            btnEliminar.Appearance.ForeColor = Color.White;
            btnEliminar.Appearance.Options.UseBackColor = true;
            btnEliminar.Appearance.Options.UseForeColor = true;
            btnEliminar.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            btnEliminar.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnEliminar.ImageOptions.SvgImage");
            btnEliminar.Location = new Point(526, 10);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(105, 39);
            btnEliminar.TabIndex = 2;
            btnEliminar.Text = "Eliminar";
            // 
            // btnNuevo
            // 
            btnNuevo.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            btnNuevo.Appearance.ForeColor = Color.White;
            btnNuevo.Appearance.Options.UseBackColor = true;
            btnNuevo.Appearance.Options.UseForeColor = true;
            btnNuevo.AppearanceHovered.BackColor = Color.DimGray;
            btnNuevo.AppearanceHovered.Options.UseBackColor = true;
            btnNuevo.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            btnNuevo.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnNuevo.ImageOptions.SvgImage");
            btnNuevo.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.Full;
            btnNuevo.Location = new Point(415, 10);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(105, 39);
            btnNuevo.TabIndex = 3;
            btnNuevo.Text = "Nuevo";
            // 
            // btnRegresar
            // 
            btnRegresar.Appearance.BackColor = Color.Gray;
            btnRegresar.Appearance.ForeColor = Color.White;
            btnRegresar.Appearance.Options.UseBackColor = true;
            btnRegresar.Appearance.Options.UseForeColor = true;
            btnRegresar.AppearanceHovered.BackColor = Color.DimGray;
            btnRegresar.AppearanceHovered.Options.UseBackColor = true;
            btnRegresar.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            btnRegresar.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnRegresar.ImageOptions.SvgImage");
            btnRegresar.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.Full;
            btnRegresar.Location = new Point(304, 10);
            btnRegresar.Name = "btnRegresar";
            btnRegresar.Size = new Size(105, 39);
            btnRegresar.TabIndex = 1;
            btnRegresar.Text = "Regresar";
            // 
            // FormTypeHeadEdit
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(862, 526);
            Controls.Add(stackPanel2);
            Controls.Add(progressPanel);
            Controls.Add(stackPanel1);
            Name = "FormTypeHeadEdit";
            Text = "FormTypeHeadEdit";
            ((System.ComponentModel.ISupportInitialize)stackPanel1).EndInit();
            stackPanel1.ResumeLayout(false);
            stackPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)stackPanel2).EndInit();
            stackPanel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public DevExpress.XtraEditors.LabelControl lblTitulo;
        private DevExpress.Utils.Layout.StackPanel stackPanel1;
        public DevExpress.XtraWaitForm.ProgressPanel progressPanel;
        private DevExpress.Utils.Layout.StackPanel stackPanel2;
        public System.ComponentModel.BackgroundWorker backgroundWorker;
        public DevExpress.XtraEditors.SimpleButton btnRegresar;
        public DevExpress.XtraEditors.SimpleButton btnEliminar;
        public DevExpress.XtraEditors.SimpleButton btnNuevo;
        public DevExpress.XtraEditors.SimpleButton btnGuardar;
        public DevExpress.XtraEditors.SimpleButton btnImprmir;
    }
}