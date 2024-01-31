using DevExpress.Xpo;
using System;
using System.Linq;
using v4posme_window_form.Models.Tablas;

namespace v4posme_window_form.Domain
{
    public class ValidarUsuario
    {
        public string Error { get; set; }
        public User validarNickName(string nickname)
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
            catch (Exception ex)
            {
                Error = ex.Message;
                return null;
            }
        }

        public User validarUsuario(string nickname, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            try
            {
                XPQuery<User> users = Session.DefaultSession.Query<User>();
                var usuario = (from u in users where u.Nickname == nickname && u.Password==password select u).First();
                return usuario;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return null;
            }
        }
        public User validarUsuario(User usuario, string password)
        {
            if (string.IsNullOrEmpty(password)) { return null; }
            if(usuario == null) {  return null; }
            if(usuario.Password == password)
            {
                return usuario;
            }
            else
            {
                return null;
            }
        }
    }
}
