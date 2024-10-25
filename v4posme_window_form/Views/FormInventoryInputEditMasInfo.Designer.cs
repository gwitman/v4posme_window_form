namespace v4posme_window.Views
{
    partial class FormInventoryInputEditMasInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInventoryInputEditMasInfo));
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            txtVencimiento = new DevExpress.XtraEditors.DateEdit();
            txtLote = new DevExpress.XtraEditors.TextEdit();
            txtPrecio1 = new DevExpress.XtraEditors.TextEdit();
            txtPrecio2 = new DevExpress.XtraEditors.TextEdit();
            txtReference4TransactionMasterDetail = new DevExpress.XtraEditors.TextEdit();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtVencimiento.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtVencimiento.Properties.CalendarTimeProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtLote.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtPrecio1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtPrecio2.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtReference4TransactionMasterDetail.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(txtVencimiento);
            layoutControl1.Controls.Add(txtLote);
            layoutControl1.Controls.Add(txtPrecio1);
            layoutControl1.Controls.Add(txtPrecio2);
            layoutControl1.Controls.Add(txtReference4TransactionMasterDetail);
            layoutControl1.Dock = DockStyle.Fill;
            layoutControl1.Location = new Point(0, 50);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(562, 0, 650, 400);
            layoutControl1.Root = Root;
            layoutControl1.Size = new Size(441, 369);
            layoutControl1.TabIndex = 1;
            layoutControl1.Text = "layoutControl1";
            // 
            // txtVencimiento
            // 
            txtVencimiento.EditValue = new DateTime(2024, 10, 18, 9, 44, 54, 884);
            txtVencimiento.EnterMoveNextControl = true;
            txtVencimiento.Location = new Point(23, 42);
            txtVencimiento.Name = "txtVencimiento";
            txtVencimiento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            txtVencimiento.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            txtVencimiento.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Fluent;
            txtVencimiento.Properties.EditFormat.FormatString = "";
            txtVencimiento.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            txtVencimiento.Properties.MaskSettings.Set("mask", "d");
            txtVencimiento.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            txtVencimiento.Size = new Size(395, 28);
            txtVencimiento.StyleController = layoutControl1;
            txtVencimiento.TabIndex = 4;
            // 
            // txtLote
            // 
            txtLote.EnterMoveNextControl = true;
            txtLote.Location = new Point(23, 105);
            txtLote.Name = "txtLote";
            txtLote.Size = new Size(395, 28);
            txtLote.StyleController = layoutControl1;
            txtLote.TabIndex = 5;
            // 
            // txtPrecio1
            // 
            txtPrecio1.EnterMoveNextControl = true;
            txtPrecio1.Location = new Point(23, 168);
            txtPrecio1.Name = "txtPrecio1";
            txtPrecio1.Size = new Size(395, 28);
            txtPrecio1.StyleController = layoutControl1;
            txtPrecio1.TabIndex = 6;
            // 
            // txtPrecio2
            // 
            txtPrecio2.EnterMoveNextControl = true;
            txtPrecio2.Location = new Point(23, 231);
            txtPrecio2.Name = "txtPrecio2";
            txtPrecio2.Size = new Size(395, 28);
            txtPrecio2.StyleController = layoutControl1;
            txtPrecio2.TabIndex = 7;
            // 
            // txtReference4TransactionMasterDetail
            // 
            txtReference4TransactionMasterDetail.Location = new Point(23, 294);
            txtReference4TransactionMasterDetail.Name = "txtReference4TransactionMasterDetail";
            txtReference4TransactionMasterDetail.Size = new Size(395, 28);
            txtReference4TransactionMasterDetail.StyleController = layoutControl1;
            txtReference4TransactionMasterDetail.TabIndex = 8;
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1, layoutControlItem2, layoutControlItem3, layoutControlItem4, layoutControlItem5 });
            Root.Name = "Root";
            Root.Padding = new DevExpress.XtraLayout.Utils.Padding(20, 20, 20, 20);
            Root.Size = new Size(441, 369);
            Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = txtVencimiento;
            layoutControlItem1.Location = new Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new Size(401, 63);
            layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 10);
            layoutControlItem1.Text = "Vencimiento";
            layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem1.TextSize = new Size(81, 13);
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = txtLote;
            layoutControlItem2.Location = new Point(0, 63);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new Size(401, 63);
            layoutControlItem2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 10);
            layoutControlItem2.Text = "Lote";
            layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem2.TextSize = new Size(81, 13);
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = txtPrecio1;
            layoutControlItem3.Location = new Point(0, 126);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new Size(401, 63);
            layoutControlItem3.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 10);
            layoutControlItem3.Text = "Precio por mayor";
            layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem3.TextSize = new Size(81, 13);
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = txtPrecio2;
            layoutControlItem4.Location = new Point(0, 189);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new Size(401, 63);
            layoutControlItem4.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 10);
            layoutControlItem4.Text = "Precio especial";
            layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem4.TextSize = new Size(81, 13);
            // 
            // layoutControlItem5
            // 
            layoutControlItem5.Control = txtReference4TransactionMasterDetail;
            layoutControlItem5.Location = new Point(0, 252);
            layoutControlItem5.Name = "layoutControlItem5";
            layoutControlItem5.Size = new Size(401, 77);
            layoutControlItem5.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 10);
            layoutControlItem5.Text = "Expandir código";
            layoutControlItem5.TextLocation = DevExpress.Utils.Locations.Top;
            layoutControlItem5.TextSize = new Size(81, 13);
            // 
            // FormInventoryInputEditMasInfo
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(441, 419);
            Controls.Add(layoutControl1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            IconOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("FormInventoryInputEditMasInfo.IconOptions.SvgImage");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormInventoryInputEditMasInfo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Más Informción";
            Load += FormInventoryInputEditMasInfo_Load;
            Controls.SetChildIndex(layoutControl1, 0);
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)txtVencimiento.Properties.CalendarTimeProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtVencimiento.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtLote.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtPrecio1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtPrecio2.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtReference4TransactionMasterDetail.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.DateEdit txtVencimiento;
        private DevExpress.XtraEditors.TextEdit txtLote;
        private DevExpress.XtraEditors.TextEdit txtPrecio1;
        private DevExpress.XtraEditors.TextEdit txtPrecio2;
        private DevExpress.XtraEditors.TextEdit txtReference4TransactionMasterDetail;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}