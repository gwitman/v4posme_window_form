namespace v4posme_window.Views.CXC.Customer
{
    partial class FormCustomerList
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
            backgroundWorker = new System.ComponentModel.BackgroundWorker();
            SuspendLayout();
            // 
            // progressPanel
            // 
            progressPanel.Appearance.BackColor = Color.Transparent;
            progressPanel.Appearance.Options.UseBackColor = true;
            // 
            // FormCustomerList
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(970, 532);
            Name = "FormCustomerList";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Listado Clientes";
            WindowState = FormWindowState.Maximized;
            Load += FormCustomerList_Load;
            Enter += FormCustomerList_Enter;
            ResumeLayout(false);
        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}