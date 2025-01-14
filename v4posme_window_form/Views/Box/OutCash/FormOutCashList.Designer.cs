﻿using v4posme_window.Views.Box.InputCash;

namespace v4posme_window.Views.Box.OutCash
{
    partial class FormOutCashList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInputCashList));
            SuspendLayout();
            // 
            // progressPanel
            // 
            progressPanel.Appearance.BackColor = Color.Transparent;
            progressPanel.Appearance.Options.UseBackColor = true;
            // 
            // FormInputCashList
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(964, 625);
            IconOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("FormInputCashList.IconOptions.SvgImage");
            Name = "FormInputCashList";
            Text = "LISTA DE INGRESOS A CAJA";
            Activated += FormInputCashList_Load;
            Load += FormInputCashList_Load;
            Enter += FormInputCashList_Load;
            ResumeLayout(false);
        }

        #endregion
    }
}