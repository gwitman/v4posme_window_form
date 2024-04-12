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
            simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            centerPane = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)stackPanel1).BeginInit();
            stackPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)centerPane).BeginInit();
            SuspendLayout();
            // 
            // panelControl1
            // 
            panelControl1.Controls.Add(stackPanel1);
            panelControl1.Dock = DockStyle.Top;
            panelControl1.Location = new Point(0, 0);
            panelControl1.Margin = new Padding(4, 4, 4, 4);
            panelControl1.Name = "panelControl1";
            panelControl1.Size = new Size(800, 69);
            panelControl1.TabIndex = 0;
            // 
            // stackPanel1
            // 
            stackPanel1.Controls.Add(simpleButton2);
            stackPanel1.Controls.Add(simpleButton3);
            stackPanel1.Location = new Point(0, 6);
            stackPanel1.Margin = new Padding(4, 4, 4, 4);
            stackPanel1.Name = "stackPanel1";
            stackPanel1.Padding = new Padding(2);
            stackPanel1.Size = new Size(227, 59);
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
            simpleButton2.Location = new Point(6, 13);
            simpleButton2.Margin = new Padding(4, 4, 4, 4);
            simpleButton2.Name = "simpleButton2";
            simpleButton2.Size = new Size(99, 32);
            simpleButton2.TabIndex = 1;
            simpleButton2.Text = "Cancelar";
            // 
            // simpleButton3
            // 
            simpleButton3.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            simpleButton3.Appearance.ForeColor = Color.White;
            simpleButton3.Appearance.Options.UseBackColor = true;
            simpleButton3.Appearance.Options.UseForeColor = true;
            simpleButton3.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            simpleButton3.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("simpleButton3.ImageOptions.SvgImage");
            simpleButton3.ImageOptions.SvgImageSize = new Size(16, 16);
            simpleButton3.Location = new Point(113, 13);
            simpleButton3.Margin = new Padding(4, 4, 4, 4);
            simpleButton3.Name = "simpleButton3";
            simpleButton3.Size = new Size(99, 32);
            simpleButton3.TabIndex = 2;
            simpleButton3.Text = "Aceptar";
            // 
            // centerPane
            // 
            centerPane.Dock = DockStyle.Fill;
            centerPane.Location = new Point(0, 69);
            centerPane.Margin = new Padding(4, 4, 4, 4);
            centerPane.Name = "centerPane";
            centerPane.Size = new Size(800, 409);
            centerPane.TabIndex = 1;
            // 
            // FormTypeListSearch
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 478);
            Controls.Add(centerPane);
            Controls.Add(panelControl1);
            IconOptions.Icon = (Icon)resources.GetObject("FormTypeListSearch.IconOptions.Icon");
            Margin = new Padding(4, 4, 4, 4);
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
            ((System.ComponentModel.ISupportInitialize)centerPane).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.Utils.Layout.StackPanel stackPanel1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.PanelControl centerPane;
    }
}