namespace v4posme_window.Template
{
    partial class FormTypeReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTypeReport));
            ListBoxControlReport = new DevExpress.XtraEditors.ImageListBoxControl();
            svgImageCollection = new DevExpress.Utils.SvgImageCollection(components);
            ((System.ComponentModel.ISupportInitialize)ListBoxControlReport).BeginInit();
            ((System.ComponentModel.ISupportInitialize)svgImageCollection).BeginInit();
            SuspendLayout();
            // 
            // ListBoxControlReport
            // 
            ListBoxControlReport.Dock = DockStyle.Fill;
            ListBoxControlReport.HorizontalScrollbar = true;
            ListBoxControlReport.ItemHeight = 65;
            ListBoxControlReport.Location = new Point(0, 0);
            ListBoxControlReport.Name = "ListBoxControlReport";
            ListBoxControlReport.Size = new Size(696, 519);
            ListBoxControlReport.TabIndex = 1;
            ListBoxControlReport.HtmlElementMouseClick += ListBoxControlReport_HtmlElementMouseClick;
            // 
            // FormTypeReport
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(696, 519);
            Controls.Add(ListBoxControlReport);
            IconOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("FormTypeReport.IconOptions.SvgImage");
            Name = "FormTypeReport";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormTypeReport";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)ListBoxControlReport).EndInit();
            ((System.ComponentModel.ISupportInitialize)svgImageCollection).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DevExpress.Utils.SvgImageCollection svgImageCollection;
        public DevExpress.XtraEditors.ImageListBoxControl ListBoxControlReport;
    }
}