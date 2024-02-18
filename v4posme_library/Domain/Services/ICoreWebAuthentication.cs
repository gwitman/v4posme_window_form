using System.Threading.Tasks;
using v4posme_library.ModelsCode;

namespace v4posme_library.Domain.Services
{
    public interface ICoreWebAuthentication
    {
        User GetUserByNickname(string nickname);
        User GetUserByPasswordAndNickname(string nickname, string password);
        User Validar(User user, string password);
    }
}
