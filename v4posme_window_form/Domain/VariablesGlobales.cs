using System.Configuration;
using v4posme_window_form.Models;
using v4posme_window_form.Models.Tablas;

namespace v4posme_window_form.Domain
{
    public class VariablesGlobales
    {
        private readonly static VariablesGlobales _instance = new VariablesGlobales();

        private readonly static string _connectionString= ConfigurationManager.ConnectionStrings["posme.netdbkroqnguhldo1"].ConnectionString;
        private VariablesGlobales()
        {
        }

        public static VariablesGlobales Instance
        {
            get
            {
                return _instance;
            }
        }

        public static string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        public Usuario Usuario{ get; set; }

        public User User { get; set; }
    }
}
