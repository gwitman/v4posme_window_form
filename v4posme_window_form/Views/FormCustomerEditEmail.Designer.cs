namespace v4posme_window.Views
{
    partial class FormCustomerEditEmail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCustomerEditEmail));
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            txtEmail = new DevExpress.UITemplates.Collection.Editors.EmailBox();
            txtIsPrimary = new DevExpress.XtraEditors.CheckEdit();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtEmail).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtIsPrimary.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dxErrorProvider).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(txtEmail);
            layoutControl1.Controls.Add(txtIsPrimary);
            layoutControl1.Location = new Point(12, 56);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.Root = Root;
            layoutControl1.Size = new Size(376, 149);
            layoutControl1.TabIndex = 1;
            layoutControl1.Text = "layoutControl1";
            // 
            // txtEmail
            // 
            txtEmail.EditorSize = DevExpress.XtraEditors.Internal.HtmlTextBoxBase.EditSize.Large;
            txtEmail.FooterLabel = "Debe ser un correo valido";
            txtEmail.Location = new Point(16, 34);
            txtEmail.Name = "txtEmail";
            txtEmail.Placeholder = "example@email.com";
            txtEmail.Size = new Size(344, 42);
            txtEmail.StyleController = layoutControl1;
            txtEmail.TabIndex = 2;
            // 
            // txtIsPrimary
            // 
            txtIsPrimary.Location = new Point(16, 97);
            txtIsPrimary.Name = "txtIsPrimary";
            txtIsPrimary.Properties.Caption = "Es Primario";
            txtIsPrimary.Size = new Size(344, 22);
            txtIsPrimary.StyleController = layoutControl1;
            txtIsPrimary.TabIndex = 5;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem2, layoutControlItem1 });
            Root.Name = "Root";
            Root.Size = new Size(376, 149);
            Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = txtIsPrimary;
            layoutControlItem2.Location = new Point(0, 81);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new Size(350, 42);
            layoutControlItem2.TextSize = new Size(0, 0);
            layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = txtEmail;
            layoutControlItem1.Location = new Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new Size(350, 81);
            layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 15);
            layoutControlItem1.Text = "E-mail";
            layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem1.TextSize = new Size(28, 13);
            layoutControlItem1.TextToControlDistance = 5;
            // 
            // dxErrorProvider
            // 
            dxErrorProvider.ContainerControl = this;
            // 
            // FormCustomerEditEmail
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(416, 211);
            Controls.Add(layoutControl1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            IconOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("FormCustomerEditEmail.IconOptions.SvgImage");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormCustomerEditEmail";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Agregar Email";
            Load += FormCustomerEditEmail_Load;
            Controls.SetChildIndex(layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)txtEmail).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtIsPrimary.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dxErrorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.CheckEdit txtIsPrimary;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        private DevExpress.UITemplates.Collection.Editors.EmailBox txtEmail;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}