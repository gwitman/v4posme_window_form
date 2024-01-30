using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using v4posme_window_form.views;
using v4posme_window_form.Views;

namespace v4posme_window_form
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var loginForm = new LoginForm();
            if(loginForm.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new PrincipalForm());
            }
            else
            {
                Application.Exit();
            }            
        }
    }
}
