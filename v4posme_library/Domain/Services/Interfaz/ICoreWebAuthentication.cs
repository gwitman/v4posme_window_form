using v4posme_library.Models;
namespace v4posme_library.Domain.Services.Interfaz
{
    public interface ICoreWebAuthentication
    {
        TbUser? GetUserByNickname(string nickname);
        TbUser GetUserByPasswordAndNickname(string nickname, string password);
        TbUser Validar(TbUser user, string password);
    }
}
