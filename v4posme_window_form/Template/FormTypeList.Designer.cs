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
            dateEdit1 = new DevExpress.XtraEditors.DateEdit();
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
            ((System.ComponentModel.ISupportInitialize)dateEdit1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dateEdit1.Properties.CalendarTimeProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)textEdit1.Properties).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)centerPane).BeginInit();
            SuspendLayout();
            // 
            // stackPanel1
            // 
            stackPanel1.Controls.Add(lblTitulo);
            stackPanel1.Dock = DockStyle.Top;
            stackPanel1.Location = new Point(0, 0);
            stackPanel1.Name = "stackPanel1";
            stackPanel1.Size = new Size(1008, 67);
            stackPanel1.TabIndex = 0;
            stackPanel1.UseSkinIndents = true;
            // 
            // lblTitulo
            // 
            lblTitulo.Appearance.Font = new Font("Tahoma", 10F);
            lblTitulo.Appearance.Options.UseFont = true;
            lblTitulo.Location = new Point(13, 25);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(119, 16);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Titulo del Formulario";
            // 
            // panelControl1
            // 
            panelControl1.Controls.Add(stackPanel2);
            panelControl1.Controls.Add(flowLayoutPanel1);
            panelControl1.Dock = DockStyle.Top;
            panelControl1.Location = new Point(0, 67);
            panelControl1.Name = "panelControl1";
            panelControl1.Padding = new Padding(0, 5, 0, 5);
            panelControl1.Size = new Size(1008, 56);
            panelControl1.TabIndex = 1;
            // 
            // stackPanel2
            // 
            stackPanel2.Controls.Add(dateEdit1);
            stackPanel2.Controls.Add(textEdit1);
            stackPanel2.Dock = DockStyle.Left;
            stackPanel2.Location = new Point(2, 7);
            stackPanel2.Name = "stackPanel2";
            stackPanel2.Size = new Size(393, 42);
            stackPanel2.TabIndex = 2;
            stackPanel2.UseSkinIndents = true;
            // 
            // dateEdit1
            // 
            dateEdit1.EditValue = null;
            dateEdit1.Location = new Point(13, 10);
            dateEdit1.Name = "dateEdit1";
            dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            dateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            dateEdit1.Size = new Size(187, 20);
            dateEdit1.TabIndex = 0;
            // 
            // textEdit1
            // 
            textEdit1.Location = new Point(204, 9);
            textEdit1.Name = "textEdit1";
            textEdit1.Properties.ContextImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("textEdit1.Properties.ContextImageOptions.SvgImage");
            textEdit1.Properties.ContextImageOptions.SvgImageSize = new Size(20, 20);
            textEdit1.Size = new Size(164, 24);
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
            flowLayoutPanel1.Size = new Size(391, 42);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // btnVistas
            // 
            btnVistas.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            btnVistas.ImageOptions.SvgImage = Properties.Resources.bo_appearance1;
            btnVistas.Location = new Point(3, 3);
            btnVistas.Name = "btnVistas";
            btnVistas.Size = new Size(89, 34);
            btnVistas.TabIndex = 5;
            btnVistas.Text = "Vistas";
            // 
            // btnEditar
            // 
            btnEditar.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            btnEditar.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnEditar.ImageOptions.SvgImage");
            btnEditar.Location = new Point(98, 3);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(89, 34);
            btnEditar.TabIndex = 2;
            btnEditar.Text = "Editar";
            // 
            // btnEliminar
            // 
            btnEliminar.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnEliminar.ImageOptions.SvgImage");
            btnEliminar.Location = new Point(193, 3);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(89, 34);
            btnEliminar.TabIndex = 3;
            btnEliminar.Text = "Delete";
            // 
            // btnNuevo
            // 
            btnNuevo.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnNuevo.ImageOptions.SvgImage");
            btnNuevo.Location = new Point(288, 3);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(89, 34);
            btnNuevo.TabIndex = 4;
            btnNuevo.Text = "Nuevo";
            // 
            // centerPane
            // 
            centerPane.Dock = DockStyle.Fill;
            centerPane.Location = new Point(0, 123);
            centerPane.Name = "centerPane";
            centerPane.Size = new Size(1008, 499);
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
            ((System.ComponentModel.ISupportInitialize)dateEdit1.Properties.CalendarTimeProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)dateEdit1.Properties).EndInit();
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
        internal DevExpress.XtraEditors.SimpleButton btnVistas;
        internal DevExpress.XtraEditors.SimpleButton btnEditar;
        internal DevExpress.XtraEditors.SimpleButton btnEliminar;
        internal DevExpress.XtraEditors.SimpleButton btnNuevo;
        internal DevExpress.XtraEditors.TextEdit textEdit1;
        internal DevExpress.XtraEditors.DateEdit dateEdit1;
        internal DevExpress.XtraEditors.PanelControl centerPane;
    }
}