using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using v4posme_window_form.Models.Tablas;

namespace v4posme_window_form.Domain.Services
{
    public class UsuarioService : IUsuarioService
    {
        public User validar(User user, string password)
        {
            if (user == null) return null;
            if (string.IsNullOrEmpty(password)) return null;
            if (user.Password == password) return user;
            else return null;
        }

        public User validarNickname(string nickname)
        {
            if (string.IsNullOrEmpty(nickname))
            {
                return null;
            }
            try
            {
                XPQuery<User> users = Session.DefaultSession.Query<User>();
                var usuario = (from u in users where u.Nickname == nickname select u).First();
                return usuario;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public User validarPassword(string nickname, string password)
        {
            if (string.IsNullOrEmpty(nickname)) { return null; }
            if (string.IsNullOrEmpty(password)) { return null; }
            var user = validarNickname(nickname);
            if (user == null) { return null; }
            if (user.Password == password) { return user; }
            else { return null; }
        }
    }
}
