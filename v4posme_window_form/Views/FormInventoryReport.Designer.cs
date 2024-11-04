namespace v4posme_window.Views
{
    partial class FormInventoryReport
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
            ListBoxControlReport = new DevExpress.XtraEditors.ImageListBoxControl();
            svgImageCollection = new DevExpress.Utils.SvgImageCollection(components);
            ((System.ComponentModel.ISupportInitialize)ListBoxControlReport).BeginInit();
            ((System.ComponentModel.ISupportInitialize)svgImageCollection).BeginInit();
            SuspendLayout();
            // 
            // ListBoxControlReport
            // 
            ListBoxControlReport.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ListBoxControlReport.HorizontalScrollbar = true;
            ListBoxControlReport.ItemHeight = 65;
            ListBoxControlReport.Location = new Point(12, 12);
            ListBoxControlReport.Name = "ListBoxControlReport";
            ListBoxControlReport.Size = new Size(810, 500);
            ListBoxControlReport.TabIndex = 0;
            ListBoxControlReport.HtmlElementMouseClick += ListBoxControlReport_HtmlElementMouseClick;
            // 
            // FormInventoryReport
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(834, 524);
            Controls.Add(ListBoxControlReport);
            Name = "FormInventoryReport";
            Text = "FormInventoryReport";
            Load += FormInventoryReport_Load;
            ((System.ComponentModel.ISupportInitialize)ListBoxControlReport).EndInit();
            ((System.ComponentModel.ISupportInitialize)svgImageCollection).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.ImageListBoxControl ListBoxControlReport;
        private DevExpress.Utils.SvgImageCollection svgImageCollection;
    }
}