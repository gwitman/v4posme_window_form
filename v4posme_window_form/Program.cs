using DevExpress.Xpo.DB;
using DevExpress.Xpo;
using System;
using System.Windows.Forms;
using v4posme_window_form.views;
using v4posme_window_form.Views;
using System.Configuration;
using v4posme_window_form.Domain;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Alerter;

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
            DevExpress.XtraEditors.WindowsFormsSettings.UseAdvancedTextEdit = DevExpress.Utils.DefaultBoolean.True;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var loginForm = new LoginForm();
            try
            {
                IDataLayer dataLayer = XpoDefault.GetDataLayer(VariablesGlobales.ConnectionString, AutoCreateOption.None);
                XpoDefault.DataLayer = dataLayer;
            }
            catch (Exception ex)
            {
                var alert = new AlertControl();
                alert.Show(loginForm, "Error", "No hay una conexión activa, revise su internet o solicite permiso para la conexion a la base de datos.");
            }
            //Session session = new Session();            
            if (loginForm.ShowDialog() == DialogResult.OK)
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
