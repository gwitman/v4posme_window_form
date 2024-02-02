using System.Threading.Tasks;
using v4posme_window_form.Models.Tablas;

namespace v4posme_window_form.Domain.Services
{
    public interface IUsuarioService
    {
        Task<User> validarNickname(string nickname);
        Task<User> validarPassword(string nickname, string password);
        Task<User> validar(Task<User> user, string password);
    }
}
