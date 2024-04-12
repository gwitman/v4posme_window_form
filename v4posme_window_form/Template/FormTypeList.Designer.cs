using DevExpress.Utils.Svg;

namespace v4posme_window.Template
{
    partial class FormTypeList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTypeList));
            stackPanel1 = new DevExpress.Utils.Layout.StackPanel();
            lblTitulo = new DevExpress.XtraEditors.LabelControl();
            panelControl1 = new DevExpress.XtraEditors.PanelControl();
            stackPanel2 = new DevExpress.Utils.Layout.StackPanel();
            txtFecha = new DevExpress.XtraEditors.DateEdit();
            textEdit1 = new DevExpress.XtraEditors.TextEdit();
            flowLayoutPanel1 = new FlowLayoutPanel();
            btnVistas = new DevExpress.XtraEditors.SimpleButton();
            btnEditar = new DevExpress.XtraEditors.SimpleButton();
            btnEliminar = new DevExpress.XtraEditors.SimpleButton();
            btnNuevo = new DevExpress.XtraEditors.SimpleButton();
            centerPane = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)stackPanel1).BeginInit();
            stackPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
            panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)stackPanel2).BeginInit();
            stackPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtFecha.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtFecha.Properties.CalendarTimeProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)textEdit1.Properties).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)centerPane).BeginInit();
            SuspendLayout();
            // 
            // stackPanel1
            // 
            stackPanel1.Appearance.BackColor = Color.WhiteSmoke;
            stackPanel1.Appearance.Options.UseBackColor = true;
            stackPanel1.Controls.Add(lblTitulo);
            stackPanel1.Dock = DockStyle.Top;
            stackPanel1.Location = new Point(0, 0);
            stackPanel1.Name = "stackPanel1";
            stackPanel1.Size = new Size(1008, 47);
            stackPanel1.TabIndex = 0;
            stackPanel1.UseSkinIndents = true;
            // 
            // lblTitulo
            // 
            lblTitulo.Appearance.Font = new Font("Tahoma", 10F, FontStyle.Bold);
            lblTitulo.Appearance.ForeColor = Color.DimGray;
            lblTitulo.Appearance.Options.UseFont = true;
            lblTitulo.Appearance.Options.UseForeColor = true;
            lblTitulo.Location = new Point(17, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(129, 16);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Titulo del Formulario";
            // 
            // panelControl1
            // 
            panelControl1.Controls.Add(stackPanel2);
            panelControl1.Controls.Add(flowLayoutPanel1);
            panelControl1.Dock = DockStyle.Top;
            panelControl1.Location = new Point(0, 47);
            panelControl1.Name = "panelControl1";
            panelControl1.Padding = new Padding(0, 5, 0, 5);
            panelControl1.Size = new Size(1008, 62);
            panelControl1.TabIndex = 1;
            // 
            // stackPanel2
            // 
            stackPanel2.Controls.Add(txtFecha);
            stackPanel2.Controls.Add(textEdit1);
            stackPanel2.Dock = DockStyle.Left;
            stackPanel2.Location = new Point(2, 7);
            stackPanel2.Name = "stackPanel2";
            stackPanel2.Size = new Size(393, 48);
            stackPanel2.TabIndex = 2;
            stackPanel2.UseSkinIndents = true;
            // 
            // txtFecha
            // 
            txtFecha.EditValue = new DateTime(2024, 4, 8, 21, 9, 29, 0);
            txtFecha.Location = new Point(17, 2);
            txtFecha.Name = "txtFecha";
            txtFecha.Properties.AdvancedModeOptions.AllowCaretAnimation = DevExpress.Utils.DefaultBoolean.True;
            txtFecha.Properties.AdvancedModeOptions.Label = "Fecha";
            txtFecha.Properties.AdvancedModeOptions.LabelAppearance.ForeColor = Color.Silver;
            txtFecha.Properties.AdvancedModeOptions.LabelAppearance.Options.UseForeColor = true;
            txtFecha.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            txtFecha.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            txtFecha.Properties.ContextImageOptions.SvgImage = (SvgImage)resources.GetObject("txtFecha.Properties.ContextImageOptions.SvgImage");
            txtFecha.Size = new Size(187, 44);
            txtFecha.TabIndex = 0;
            // 
            // textEdit1
            // 
            textEdit1.Location = new Point(210, 3);
            textEdit1.Name = "textEdit1";
            textEdit1.Properties.AdvancedModeOptions.Label = "Filtrar";
            textEdit1.Properties.AdvancedModeOptions.LabelAppearance.ForeColor = Color.Silver;
            textEdit1.Properties.AdvancedModeOptions.LabelAppearance.Options.UseForeColor = true;
            textEdit1.Properties.Appearance.Font = new Font("Tahoma", 8.5F);
            textEdit1.Properties.Appearance.Options.UseFont = true;
            textEdit1.Properties.ContextImageOptions.SvgImage = (SvgImage)resources.GetObject("textEdit1.Properties.ContextImageOptions.SvgImage");
            textEdit1.Properties.ContextImageOptions.SvgImageSize = new Size(20, 20);
            textEdit1.Properties.UseAdvancedMode = DevExpress.Utils.DefaultBoolean.True;
            textEdit1.Size = new Size(163, 42);
            textEdit1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(btnVistas);
            flowLayoutPanel1.Controls.Add(btnEditar);
            flowLayoutPanel1.Controls.Add(btnEliminar);
            flowLayoutPanel1.Controls.Add(btnNuevo);
            flowLayoutPanel1.Dock = DockStyle.Right;
            flowLayoutPanel1.Location = new Point(615, 7);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(391, 48);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // btnVistas
            // 
            btnVistas.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            btnVistas.ImageOptions.SvgImage = Properties.Resources.bo_appearance1;
            btnVistas.Location = new Point(3, 3);
            btnVistas.Name = "btnVistas";
            btnVistas.Size = new Size(89, 42);
            btnVistas.TabIndex = 5;
            btnVistas.Text = "Vistas";
            // 
            // btnEditar
            // 
            btnEditar.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            btnEditar.ImageOptions.SvgImage = (SvgImage)resources.GetObject("btnEditar.ImageOptions.SvgImage");
            btnEditar.Location = new Point(98, 3);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(89, 42);
            btnEditar.TabIndex = 2;
            btnEditar.Text = "Editar";
            // 
            // btnEliminar
            // 
            btnEliminar.ImageOptions.SvgImage = (SvgImage)resources.GetObject("btnEliminar.ImageOptions.SvgImage");
            btnEliminar.Location = new Point(193, 3);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(89, 42);
            btnEliminar.TabIndex = 3;
            btnEliminar.Text = "Delete";
            // 
            // btnNuevo
            // 
            btnNuevo.ImageOptions.SvgImage = (SvgImage)resources.GetObject("btnNuevo.ImageOptions.SvgImage");
            btnNuevo.Location = new Point(288, 3);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(89, 42);
            btnNuevo.TabIndex = 4;
            btnNuevo.Text = "Nuevo";
            // 
            // centerPane
            // 
            centerPane.Dock = DockStyle.Fill;
            centerPane.Location = new Point(0, 109);
            centerPane.Name = "centerPane";
            centerPane.Size = new Size(1008, 513);
            centerPane.TabIndex = 2;
            // 
            // FormTypeList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 622);
            Controls.Add(centerPane);
            Controls.Add(panelControl1);
            Controls.Add(stackPanel1);
            Name = "FormTypeList";
            Text = "FormTypeList";
            ((System.ComponentModel.ISupportInitialize)stackPanel1).EndInit();
            stackPanel1.ResumeLayout(false);
            stackPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
            panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)stackPanel2).EndInit();
            stackPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)txtFecha.Properties.CalendarTimeProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtFecha.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)textEdit1.Properties).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)centerPane).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.Utils.Layout.StackPanel stackPanel1;
        internal DevExpress.XtraEditors.LabelControl lblTitulo;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.Utils.Layout.StackPanel stackPanel2;
        internal DevExpress.XtraEditors.PanelControl centerPane;
        public DevExpress.XtraEditors.SimpleButton btnVistas;
        public DevExpress.XtraEditors.SimpleButton btnEditar;
        public DevExpress.XtraEditors.SimpleButton btnEliminar;
        public DevExpress.XtraEditors.SimpleButton btnNuevo;
        public DevExpress.XtraEditors.TextEdit textEdit1;
        public DevExpress.XtraEditors.DateEdit txtFecha;
    }
}