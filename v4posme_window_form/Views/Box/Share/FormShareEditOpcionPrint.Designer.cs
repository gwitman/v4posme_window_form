namespace v4posme_window.Views.Box.Share
{
    partial class FormShareEditOpcionPrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormShareEditOpcionPrint));
            btnAceptar = new DevExpress.XtraEditors.SimpleButton();
            btnCancel = new DevExpress.XtraEditors.SimpleButton();
            cmbOpciones = new DevExpress.XtraEditors.ComboBoxEdit();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)cmbOpciones.Properties).BeginInit();
            SuspendLayout();
            // 
            // btnAceptar
            // 
            btnAceptar.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            btnAceptar.Appearance.Options.UseBackColor = true;
            btnAceptar.Location = new Point(292, 112);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(75, 23);
            btnAceptar.TabIndex = 1;
            btnAceptar.Text = "Aceptar";
            btnAceptar.Click += btnAceptar_Click;
            // 
            // btnCancel
            // 
            btnCancel.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            btnCancel.Appearance.Options.UseBackColor = true;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(209, 112);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancelar";
            // 
            // cmbOpciones
            // 
            cmbOpciones.Location = new Point(36, 41);
            cmbOpciones.Name = "cmbOpciones";
            cmbOpciones.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cmbOpciones.Size = new Size(331, 28);
            cmbOpciones.TabIndex = 3;
            // 
            // labelControl1
            // 
            labelControl1.Location = new Point(36, 10);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new Size(173, 13);
            labelControl1.TabIndex = 4;
            labelControl1.Text = "Seleccione una opción para imprimir:";
            // 
            // FormShareEditOpcionPrint
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(389, 158);
            ControlBox = false;
            Controls.Add(labelControl1);
            Controls.Add(cmbOpciones);
            Controls.Add(btnCancel);
            Controls.Add(btnAceptar);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            IconOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("FormShareEditOpcionPrint.IconOptions.SvgImage");
            Name = "FormShareEditOpcionPrint";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Imprimir";
            Load += FormShareEditOpcionPrint_Load;
            ((System.ComponentModel.ISupportInitialize)cmbOpciones.Properties).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnAceptar;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.ComboBoxEdit cmbOpciones;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}