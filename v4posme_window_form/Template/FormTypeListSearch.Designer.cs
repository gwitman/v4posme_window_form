namespace v4posme_window.Template
{
    partial class FormTypeListSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTypeListSearch));
            panelControl1 = new DevExpress.XtraEditors.PanelControl();
            stackPanel1 = new DevExpress.Utils.Layout.StackPanel();
            simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            btnAceptar = new DevExpress.XtraEditors.SimpleButton();
            txtFilter = new DevExpress.XtraEditors.TextEdit();
            btnAtras = new DevExpress.XtraEditors.SimpleButton();
            btnSiguiente = new DevExpress.XtraEditors.SimpleButton();
            centerPane = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)stackPanel1).BeginInit();
            stackPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtFilter.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)centerPane).BeginInit();
            SuspendLayout();
            // 
            // panelControl1
            // 
            panelControl1.Controls.Add(stackPanel1);
            panelControl1.Dock = DockStyle.Top;
            panelControl1.Location = new Point(0, 0);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new Size(686, 56);
            panelControl1.TabIndex = 0;
            // 
            // stackPanel1
            // 
            stackPanel1.Controls.Add(simpleButton2);
            stackPanel1.Controls.Add(btnAceptar);
            stackPanel1.Controls.Add(txtFilter);
            stackPanel1.Controls.Add(btnAtras);
            stackPanel1.Controls.Add(btnSiguiente);
            stackPanel1.Location = new Point(0, 5);
            stackPanel1.Name = "stackPanel1";
            stackPanel1.Padding = new Padding(2);
            stackPanel1.Size = new Size(675, 48);
            stackPanel1.TabIndex = 0;
            stackPanel1.UseSkinIndents = true;
            // 
            // simpleButton2
            // 
            simpleButton2.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            simpleButton2.Appearance.ForeColor = Color.White;
            simpleButton2.Appearance.Options.UseBackColor = true;
            simpleButton2.Appearance.Options.UseForeColor = true;
            simpleButton2.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            simpleButton2.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("simpleButton2.ImageOptions.SvgImage");
            simpleButton2.ImageOptions.SvgImageSize = new Size(16, 16);
            simpleButton2.Location = new Point(5, 11);
            simpleButton2.Name = "simpleButton2";
            simpleButton2.Size = new Size(85, 26);
            simpleButton2.TabIndex = 1;
            simpleButton2.Text = "Cancelar";
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
            btnAceptar.Location = new Point(96, 11);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(85, 26);
            btnAceptar.TabIndex = 2;
            btnAceptar.Text = "Aceptar";
            btnAceptar.Click += btnAceptar_Click;
            // 
            // txtFilter
            // 
            txtFilter.Location = new Point(187, 10);
            txtFilter.Name = "txtFilter";
            txtFilter.Size = new Size(239, 28);
            txtFilter.TabIndex = 20;
            txtFilter.KeyPress += txtFilter_KeyPress;
            // 
            // btnAtras
            // 
            btnAtras.Appearance.BackColor = Color.Salmon;
            btnAtras.Appearance.ForeColor = Color.White;
            btnAtras.Appearance.Options.UseBackColor = true;
            btnAtras.Appearance.Options.UseForeColor = true;
            btnAtras.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            btnAtras.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnAtras.ImageOptions.SvgImage");
            btnAtras.ImageOptions.SvgImageSize = new Size(16, 16);
            btnAtras.Location = new Point(432, 11);
            btnAtras.Name = "btnAtras";
            btnAtras.Size = new Size(85, 26);
            btnAtras.TabIndex = 4;
            btnAtras.Text = "Atras";
            btnAtras.Click += btnAtras_Click;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Appearance.BackColor = Color.Salmon;
            btnSiguiente.Appearance.ForeColor = Color.White;
            btnSiguiente.Appearance.Options.UseBackColor = true;
            btnSiguiente.Appearance.Options.UseForeColor = true;
            btnSiguiente.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            btnSiguiente.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnSiguiente.ImageOptions.SvgImage");
            btnSiguiente.ImageOptions.SvgImageSize = new Size(16, 16);
            btnSiguiente.Location = new Point(523, 11);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(85, 26);
            btnSiguiente.TabIndex = 3;
            btnSiguiente.Text = "Siguiente";
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // centerPane
            // 
            centerPane.Dock = DockStyle.Fill;
            centerPane.Location = new Point(0, 56);
            centerPane.Name = "centerPane";
            centerPane.Size = new Size(686, 324);
            centerPane.TabIndex = 1;
            // 
            // FormTypeListSearch
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(686, 380);
            Controls.Add(centerPane);
            Controls.Add(panelControl1);
            IconOptions.Icon = (Icon)resources.GetObject("FormTypeListSearch.IconOptions.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormTypeListSearch";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Buscar Producto";
            Load += FormTypeListSearch_Load;
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)stackPanel1).EndInit();
            stackPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)txtFilter.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)centerPane).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.Utils.Layout.StackPanel stackPanel1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton btnAceptar;
        private DevExpress.XtraEditors.PanelControl centerPane;
        private DevExpress.XtraEditors.SimpleButton btnSiguiente;
        private DevExpress.XtraEditors.SimpleButton btnAtras;
        private DevExpress.XtraEditors.TextEdit txtFilter;
    }
}