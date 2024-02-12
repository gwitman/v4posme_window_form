namespace v4posme_window_form.Views
{
    partial class SelectBranchForm
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
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.cmbSucursales = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSucursales.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(39, 26);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(92, 13);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "Seleccione Suursal:";
            // 
            // cmbSucursales
            // 
            this.cmbSucursales.Location = new System.Drawing.Point(39, 65);
            this.cmbSucursales.Name = "cmbSucursales";
            this.cmbSucursales.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSucursales.Size = new System.Drawing.Size(169, 20);
            this.cmbSucursales.TabIndex = 1;
            // 
            // SelectBranchForm
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbSucursales);
            this.Controls.Add(this.labelControl2);
            this.Name = "SelectBranchForm";
            this.Size = new System.Drawing.Size(260, 131);
            this.Load += new System.EventHandler(this.SelectBranchForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbSucursales.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit cmbSucursales;
    }
}