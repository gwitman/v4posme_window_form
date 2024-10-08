namespace v4posme_window.Views
{
    partial class FormCustomerEditTelefonos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCustomerEditTelefonos));
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            txtEntityPhoneNumber = new DevExpress.UITemplates.Collection.Editors.PhoneNumberBox();
            txtEntityPhoneTypeID = new DevExpress.XtraEditors.ComboBoxEdit();
            txtIsPrimary = new DevExpress.XtraEditors.CheckEdit();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            dxErrorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtEntityPhoneNumber).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtEntityPhoneTypeID.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtIsPrimary.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dxErrorProvider).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(txtEntityPhoneNumber);
            layoutControl1.Controls.Add(txtEntityPhoneTypeID);
            layoutControl1.Controls.Add(txtIsPrimary);
            layoutControl1.Dock = DockStyle.Fill;
            layoutControl1.Location = new Point(0, 50);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.Root = Root;
            layoutControl1.Size = new Size(439, 201);
            layoutControl1.TabIndex = 2;
            layoutControl1.Text = "layoutControl1";
            // 
            // txtEntityPhoneNumber
            // 
            txtEntityPhoneNumber.EditorSize = DevExpress.XtraEditors.Internal.HtmlTextBoxBase.EditSize.Large;
            txtEntityPhoneNumber.Location = new Point(23, 104);
            txtEntityPhoneNumber.Name = "txtEntityPhoneNumber";
            txtEntityPhoneNumber.Size = new Size(393, 30);
            txtEntityPhoneNumber.StyleController = layoutControl1;
            txtEntityPhoneNumber.TabIndex = 7;
            // 
            // txtEntityPhoneTypeID
            // 
            txtEntityPhoneTypeID.Location = new Point(23, 42);
            txtEntityPhoneTypeID.Name = "txtEntityPhoneTypeID";
            txtEntityPhoneTypeID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            txtEntityPhoneTypeID.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            txtEntityPhoneTypeID.Size = new Size(393, 28);
            txtEntityPhoneTypeID.StyleController = layoutControl1;
            txtEntityPhoneTypeID.TabIndex = 4;
            // 
            // txtIsPrimary
            // 
            txtIsPrimary.Location = new Point(23, 150);
            txtIsPrimary.Name = "txtIsPrimary";
            txtIsPrimary.Properties.Caption = "Es primario";
            txtIsPrimary.Properties.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.SvgCheckBox1;
            txtIsPrimary.RightToLeft = RightToLeft.No;
            txtIsPrimary.Size = new Size(393, 22);
            txtIsPrimary.StyleController = layoutControl1;
            txtIsPrimary.TabIndex = 6;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1, layoutControlItem3, layoutControlItem2 });
            Root.Name = "Root";
            Root.Padding = new DevExpress.XtraLayout.Utils.Padding(20, 20, 20, 20);
            Root.Size = new Size(439, 201);
            Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = txtEntityPhoneTypeID;
            layoutControlItem1.Location = new Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new Size(399, 63);
            layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 10);
            layoutControlItem1.Text = "Tipo:";
            layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem1.TextSize = new Size(24, 13);
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = txtIsPrimary;
            layoutControlItem3.Location = new Point(0, 127);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new Size(399, 34);
            layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem3.TextSize = new Size(0, 0);
            layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = txtEntityPhoneNumber;
            layoutControlItem2.Location = new Point(0, 63);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new Size(399, 64);
            layoutControlItem2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 10);
            layoutControlItem2.Text = "Teléfono";
            layoutControlItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem2.TextSize = new Size(42, 13);
            layoutControlItem2.TextToControlDistance = 5;
            // 
            // dxErrorProvider
            // 
            dxErrorProvider.ContainerControl = this;
            // 
            // FormCustomerEditTelefonos
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(439, 251);
            Controls.Add(layoutControl1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            IconOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("FormCustomerEditTelefonos.IconOptions.SvgImage");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormCustomerEditTelefonos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Agregar Teléfono";
            Load += FormCustomerEditTelefonos_Load;
            Controls.SetChildIndex(layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)txtEntityPhoneNumber).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtEntityPhoneTypeID.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtIsPrimary.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dxErrorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.ComboBoxEdit txtEntityPhoneTypeID;
        private DevExpress.XtraEditors.CheckEdit txtIsPrimary;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider;
        private DevExpress.UITemplates.Collection.Editors.PhoneNumberBox txtEntityPhoneNumber;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}