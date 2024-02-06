using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using v4posme_window_form.Models.Tablas;

namespace v4posme_window_form.Domain.Services
{
    public class CoreMenu : ICoreMenu
    {

        public CoreMenu() { }

        public MenuElement getMenuTop()
        {
            var role = VariablesGlobales.Instance.Role;
            if (role == null)
            {
                return null;
            }
            throw new NotImplementedException();
        }
    }
}
