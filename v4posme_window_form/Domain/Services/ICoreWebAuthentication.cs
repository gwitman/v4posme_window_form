using System.Threading.Tasks;
using v4posme_window_form.Models.Tablas;

namespace v4posme_window_form.Domain.Services
{
    public interface ICoreWebAuthentication
    {
        User getUserByNickname(string nickname);
        User getUserByPasswordAndNickname(string nickname, string password);
        User Validar(User user, string password);
    }
}
