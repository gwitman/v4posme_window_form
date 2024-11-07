namespace v4posme_window.Views.Inventory.Product
{
    partial class FormInventoryItemEditConcepts
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
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            txtValueOut = new DevExpress.XtraEditors.TextEdit();
            txtValueIn = new DevExpress.XtraEditors.TextEdit();
            txtNameConcept = new DevExpress.XtraEditors.TextEdit();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            btnAceptar = new DevExpress.XtraEditors.SimpleButton();
            btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            bindingSource1 = new BindingSource(components);
            dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtValueOut.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtValueIn.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtNameConcept.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dxErrorProvider1).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(txtValueOut);
            layoutControl1.Controls.Add(txtValueIn);
            layoutControl1.Controls.Add(txtNameConcept);
            layoutControl1.Location = new Point(12, 3);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.Root = Root;
            layoutControl1.Size = new Size(463, 164);
            layoutControl1.TabIndex = 0;
            layoutControl1.Text = "layoutControl1";
            // 
            // txtValueOut
            // 
            txtValueOut.Location = new Point(117, 109);
            txtValueOut.Name = "txtValueOut";
            txtValueOut.Size = new Size(325, 28);
            txtValueOut.StyleController = layoutControl1;
            txtValueOut.TabIndex = 6;
            // 
            // txtValueIn
            // 
            txtValueIn.Location = new Point(117, 65);
            txtValueIn.Name = "txtValueIn";
            txtValueIn.Size = new Size(325, 28);
            txtValueIn.StyleController = layoutControl1;
            txtValueIn.TabIndex = 5;
            // 
            // txtNameConcept
            // 
            txtNameConcept.Location = new Point(117, 21);
            txtNameConcept.Name = "txtNameConcept";
            txtNameConcept.Size = new Size(325, 28);
            txtNameConcept.StyleController = layoutControl1;
            txtNameConcept.TabIndex = 4;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1, layoutControlItem2, layoutControlItem3 });
            Root.Name = "Root";
            Root.Size = new Size(463, 164);
            Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = txtNameConcept;
            layoutControlItem1.Location = new Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new Size(437, 44);
            layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlItem1.Text = "Nombre";
            layoutControlItem1.TextSize = new Size(80, 13);
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = txtValueIn;
            layoutControlItem2.Location = new Point(0, 44);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new Size(437, 44);
            layoutControlItem2.Spacing = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlItem2.Text = "Valor de Entrada";
            layoutControlItem2.TextSize = new Size(80, 13);
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = txtValueOut;
            layoutControlItem3.Location = new Point(0, 88);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new Size(437, 50);
            layoutControlItem3.Spacing = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            layoutControlItem3.Text = "Valor de Salida";
            layoutControlItem3.TextSize = new Size(80, 13);
            // 
            // btnAceptar
            // 
            btnAceptar.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            btnAceptar.Appearance.Options.UseBackColor = true;
            btnAceptar.Location = new Point(339, 200);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(126, 31);
            btnAceptar.TabIndex = 1;
            btnAceptar.Text = "Aceptar";
            btnAceptar.Click += btnAceptar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            btnCancelar.Appearance.Options.UseBackColor = true;
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(188, 200);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(126, 31);
            btnCancelar.TabIndex = 2;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            // 
            // dxErrorProvider1
            // 
            dxErrorProvider1.ContainerControl = this;
            // 
            // FormInventoryItemEditConcepts
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(477, 256);
            Controls.Add(btnCancelar);
            Controls.Add(layoutControl1);
            Controls.Add(btnAceptar);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "FormInventoryItemEditConcepts";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Conceptos";
            Load += FormInventoryItemConcepts_Load;
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)txtValueOut.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtValueIn.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtNameConcept.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dxErrorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit txtValueOut;
        private DevExpress.XtraEditors.TextEdit txtValueIn;
        private DevExpress.XtraEditors.TextEdit txtNameConcept;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton btnAceptar;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private BindingSource bindingSource1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
    }
}