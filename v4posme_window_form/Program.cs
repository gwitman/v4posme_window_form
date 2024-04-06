using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraEditors;
using Unity;
using v4posme_library.Libraries;
using v4posme_window.Views;

namespace v4posme_window;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        //Application.Run(new Form1());
        WindowsFormsSettings.UseAdvancedTextEdit = DevExpress.Utils.DefaultBoolean.True;
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        var loginForm = new LoginForm();
        var alert = new AlertControl();
        try
        {
            var dataLayer = XpoDefault.GetDataLayer(VariablesGlobales.ConnectionString, AutoCreateOption.None);
            XpoDefault.DataLayer = dataLayer;
        }
        catch (Exception exception)
        {
            XtraMessageBox.Show(exception.Message);
            alert.Show(loginForm, "Error", "No hay una conexión activa, revise su internet o solicite permiso para la conexion a la base de datos.");
        }         
        if (loginForm.ShowDialog() == DialogResult.OK)
        {
            Application.Run(new PrincipalForm());
            //Application.Run(new XtraForm1());
        }
        else
        {
            Application.Exit();
        }
    }
}
