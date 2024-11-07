namespace v4posme_window.Views.Inventory.Inputunpost
{
    partial class FormInventoryInputList
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
            SuspendLayout();
            // 
            // progressPanel
            // 
            progressPanel.Appearance.BackColor = Color.Transparent;
            progressPanel.Appearance.Options.UseBackColor = true;
            // 
            // FormInventoryInputList
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1129, 579);
            Name = "FormInventoryInputList";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormInventoryInputList";
            WindowState = FormWindowState.Maximized;
            Load += FormInventoryInput_Load;
            Enter += FormInventoryInput_Enter;
            ResumeLayout(false);
        }

        #endregion
    }
}