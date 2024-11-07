namespace v4posme_window.Views.Inventory.Product
{
    partial class FormInventoryItemCantidadImprimir
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInventoryItemCantidadImprimir));
            pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            txtCantidad = new DevExpress.XtraEditors.TextEdit();
            btnAceptar = new DevExpress.XtraEditors.SimpleButton();
            btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)pictureEdit1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtCantidad.Properties).BeginInit();
            SuspendLayout();
            // 
            // pictureEdit1
            // 
            pictureEdit1.EditValue = resources.GetObject("pictureEdit1.EditValue");
            pictureEdit1.Location = new Point(29, 23);
            pictureEdit1.Name = "pictureEdit1";
            pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            pictureEdit1.Size = new Size(57, 47);
            pictureEdit1.TabIndex = 0;
            // 
            // labelControl1
            // 
            labelControl1.Location = new Point(110, 23);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new Size(150, 13);
            labelControl1.TabIndex = 1;
            labelControl1.Text = "Especifique cantidad a imprimir:";
            // 
            // txtCantidad
            // 
            txtCantidad.Location = new Point(110, 42);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Properties.DisplayFormat.FormatString = "N";
            txtCantidad.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            txtCantidad.Properties.EditFormat.FormatString = "N";
            txtCantidad.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            txtCantidad.Size = new Size(297, 28);
            txtCantidad.TabIndex = 2;
            // 
            // btnAceptar
            // 
            btnAceptar.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            btnAceptar.Appearance.Options.UseBackColor = true;
            btnAceptar.Location = new Point(332, 89);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(75, 23);
            btnAceptar.TabIndex = 3;
            btnAceptar.Text = "Aceptar";
            btnAceptar.Click += btnAceptar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            btnCancelar.Appearance.Options.UseBackColor = true;
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(239, 89);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 23);
            btnCancelar.TabIndex = 4;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            // 
            // FormInventoryItemCantidadImprimir
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(419, 132);
            ControlBox = false;
            Controls.Add(btnCancelar);
            Controls.Add(btnAceptar);
            Controls.Add(txtCantidad);
            Controls.Add(labelControl1);
            Controls.Add(pictureEdit1);
            IconOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("FormInventoryItemCantidadImprimir.IconOptions.SvgImage");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormInventoryItemCantidadImprimir";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cantidad a imprimir";
            ((System.ComponentModel.ISupportInitialize)pictureEdit1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtCantidad.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtCantidad;
        private DevExpress.XtraEditors.SimpleButton btnAceptar;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
    }
}