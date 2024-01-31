using v4posme_window_form.Models;
using v4posme_window_form.Models.Tablas;

namespace v4posme_window_form.Domain
{
    public class VariablesGlobales
    {
        private readonly static VariablesGlobales _instance = new VariablesGlobales();

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

        public Usuario Usuario{ get; set; }

        public User User { get; set; }
    }
}
