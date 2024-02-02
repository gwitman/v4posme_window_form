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
        public async Task<User> validar(Task<User> user, string password)
        {
            if (user == null) return null;
            if (string.IsNullOrEmpty(password)) return null;
            if (user.Result.Password == password) return await user;
            return null;
        }

        public async Task<User> validarNickname(string nickname)
        {
            if (string.IsNullOrEmpty(nickname))
            {
                return null;
            }
            try
            {
                XPQuery<User> users = Session.DefaultSession.Query<User>();
                var usuario = (from u in users where u.Nickname == nickname select u).FirstAsync();
                return await usuario;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<User> validarPassword(string nickname, string password)
        {
            if (string.IsNullOrEmpty(nickname)) { return null; }
            if (string.IsNullOrEmpty(password)) { return null; }
            var user = validarNickname(nickname);
            if (user == null) { return null; }
            if (user.Result.Password == password) { return await user; }
            else { return null; }
        }
    }
}
