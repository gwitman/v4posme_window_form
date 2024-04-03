using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Interfaz
{
    public interface ICoreWebAuthentication
    {
        TbUser? GetUserByNickname(string nickname);
        TbUser? GetUserByPasswordAndNickname(string nickname, string password);
        TbUser? GetUserByEmail(string email);
        TbUser Validar(TbUser user, string password);
        string? GetLicenseMessage(int companyId);
    }
}
