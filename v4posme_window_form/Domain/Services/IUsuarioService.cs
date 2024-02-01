using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using v4posme_window_form.Models.Tablas;

namespace v4posme_window_form.Domain.Services
{
    public interface IUsuarioService
    {
        User validarNickname(string nickname);
        User validarPassword(string nickname, string password);
        User validar(User user, string password);
    }
}
