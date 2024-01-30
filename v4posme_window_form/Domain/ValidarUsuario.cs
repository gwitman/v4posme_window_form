using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using v4posme_window_form.Models;
using ZstdSharp.Unsafe;

namespace v4posme_window_form.Domain
{
    public class ValidarUsuario
    {
        public string Error { get; set; }
        public Usuario validarNickName(string nickname)
        {
            if (string.IsNullOrEmpty(nickname))
            {
                return null;
            }
            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                conn.Open();
                using (var context = new PosmeContext(conn, false))
                {
                    try
                    {
                        var usuario = context.Usuarios.Where(u => u.Nickname == nickname).First();
                        return usuario;
                    }
                    catch (InvalidOperationException ex)
                    {
                        Error = ex.Message;
                        return null;
                    }
                }
            }
        }

        public Usuario validarUsuario(string nickname, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                conn.Open();
                using (var context = new PosmeContext(conn, false))
                {
                    try
                    {
                        var usuario = context.Usuarios.Where(u => u.Password == password && u.Nickname == nickname).First();
                        return usuario;
                    }
                    catch (InvalidOperationException ex)
                    {
                        Error = ex.Message;
                        return null;
                    }
                }
            }
        }
        public Usuario validarUsuario(Usuario usuario, string password)
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
