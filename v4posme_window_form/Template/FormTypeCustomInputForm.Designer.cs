using DevExpress.XtraEditors;

namespace v4posme_window.Template
{
    partial class FormTypeCustomInputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTypeCustomInputForm));
            buttonOk = new SimpleButton();
            buttonCancel = new SimpleButton();
            textBoxInput = new TextEdit();
            ((System.ComponentModel.ISupportInitialize)textBoxInput.Properties).BeginInit();
            SuspendLayout();
            // 
            // buttonOk
            // 
            buttonOk.Appearance.BackColor = Color.FromArgb(53, 88, 52);
            buttonOk.Appearance.ForeColor = Color.White;
            buttonOk.Appearance.Options.UseBackColor = true;
            buttonOk.Appearance.Options.UseForeColor = true;
            buttonOk.AppearanceHovered.BackColor = Color.FromArgb(20, 40, 29);
            buttonOk.AppearanceHovered.Options.UseBackColor = true;
            buttonOk.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.CommonPalette;
            buttonOk.ImageOptions.SvgImageSize = new Size(24, 24);
            buttonOk.Location = new Point(45, 105);
            buttonOk.Margin = new Padding(4);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(282, 49);
            buttonOk.TabIndex = 4;
            buttonOk.Text = "OK";
            buttonOk.Click += ButtonOk_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Appearance.BackColor = Color.FromArgb(227, 23, 10);
            buttonCancel.Appearance.ForeColor = Color.White;
            buttonCancel.Appearance.Options.UseBackColor = true;
            buttonCancel.Appearance.Options.UseForeColor = true;
            buttonCancel.AppearanceHovered.BackColor = Color.FromArgb(225, 96, 54);
            buttonCancel.AppearanceHovered.Options.UseBackColor = true;
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.ImageOptions.SvgImageSize = new Size(24, 24);
            buttonCancel.Location = new Point(45, 162);
            buttonCancel.LookAndFeel.SkinName = "Office 2016 Colorful";
            buttonCancel.LookAndFeel.UseDefaultLookAndFeel = false;
            buttonCancel.Margin = new Padding(4);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(282, 48);
            buttonCancel.TabIndex = 5;
            buttonCancel.Text = "Cancelar";
            buttonCancel.Click += ButtonCancel_Click;
            // 
            // textBoxInput
            // 
            textBoxInput.EnterMoveNextControl = true;
            textBoxInput.Location = new Point(45, 23);
            textBoxInput.Margin = new Padding(4);
            textBoxInput.Name = "textBoxInput";
            textBoxInput.Properties.AdvancedModeOptions.AllowCaretAnimation = DevExpress.Utils.DefaultBoolean.True;
            textBoxInput.Properties.AdvancedModeOptions.Label = "Escribir";
            textBoxInput.Properties.ContextImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("textBoxInput.Properties.ContextImageOptions.SvgImage");
            textBoxInput.Properties.LookAndFeel.SkinName = "WXI";
            textBoxInput.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            textBoxInput.Size = new Size(282, 56);
            textBoxInput.TabIndex = 6;
            // 
            // FormTypeCustomInputForm
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(382, 273);
            Controls.Add(textBoxInput);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOk);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "FormTypeCustomInputForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Enter text";
            ((System.ComponentModel.ISupportInitialize)textBoxInput.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SimpleButton buttonOk;
        private SimpleButton buttonCancel;
        private TextEdit textBoxInput;
    }
}