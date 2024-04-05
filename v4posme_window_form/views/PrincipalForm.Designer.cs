namespace v4posme_window.Views
{
    partial class PrincipalForm
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
            accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            menuElement = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            fluentDesignFormControl1 = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl();
            barCompanyNane = new DevExpress.XtraBars.BarStaticItem();
            ((System.ComponentModel.ISupportInitialize)accordionControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)fluentDesignFormControl1).BeginInit();
            SuspendLayout();
            // 
            // accordionControl1
            // 
            accordionControl1.Dock = DockStyle.Left;
            accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] { menuElement });
            accordionControl1.Location = new Point(0, 0);
            accordionControl1.Name = "accordionControl1";
            accordionControl1.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Touch;
            accordionControl1.ShowFilterControl = DevExpress.XtraBars.Navigation.ShowFilterControl.Always;
            accordionControl1.Size = new Size(260, 696);
            accordionControl1.TabIndex = 1;
            accordionControl1.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // menuElement
            // 
            menuElement.Expanded = true;
            menuElement.Name = "menuElement";
            menuElement.Text = "Menú";
            // 
            // fluentDesignFormControl1
            // 
            fluentDesignFormControl1.Location = new Point(0, 0);
            fluentDesignFormControl1.Name = "fluentDesignFormControl1";
            fluentDesignFormControl1.Size = new Size(961, 0);
            fluentDesignFormControl1.TabIndex = 2;
            fluentDesignFormControl1.TabStop = false;
            fluentDesignFormControl1.TitleItemLinks.Add(barCompanyNane);
            // 
            // barCompanyNane
            // 
            barCompanyNane.Caption = "Company Name";
            barCompanyNane.Id = 0;
            barCompanyNane.Name = "barCompanyNane";
            // 
            // PrincipalForm
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(961, 696);
            Controls.Add(accordionControl1);
            Controls.Add(fluentDesignFormControl1);
            IsMdiContainer = true;
            Name = "PrincipalForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PosMe";
            WindowState = FormWindowState.Maximized;
            Load += PrincipalForm_Load;
            ((System.ComponentModel.ISupportInitialize)accordionControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)fluentDesignFormControl1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl fluentDesignFormControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement menuElement;
        private DevExpress.XtraBars.BarStaticItem barCompanyNane;
    }
}