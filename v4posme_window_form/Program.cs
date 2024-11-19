using System.Diagnostics;
using System.Globalization;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.XtraEditors;
using v4posme_library.Libraries;
using v4posme_window.Libraries;
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
        //ApplicationConfiguration.Initialize();
        // Establecer la cultura predeterminada para la aplicación
        var culture = VariablesGlobales.ConfigurationBuilder["APP_REGION_CULTURE"] ?? "es-NI";
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo(culture);
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo(culture);
        //Application.Run(new Form1());
        WindowsFormsSettings.UseAdvancedTextEdit = DevExpress.Utils.DefaultBoolean.True;
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        var loginForm = new LoginForm();
        try
        {
            var dataLayer = XpoDefault.GetDataLayer("XpoProvider=MySql;"+VariablesGlobales.ConnectionString, AutoCreateOption.None);
            XpoDefault.DataLayer = dataLayer;
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception);
            var coreWebRender = new CoreWebRenderInView();
            coreWebRender.GetMessageAlert(TypeError.Error,"Login", $"No hay una conexion activa o revise su conexion a internet, {exception.Message}",loginForm);
        }         
        if (loginForm.ShowDialog() == DialogResult.OK)
        {
            Application.Run(CoreFormList.Principal());            
        }
        else
        {
            Application.Exit();
        }
    }
}
