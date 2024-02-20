using v4posme_library.Models;
namespace v4posme_library.Libraries.CustomLibraries.Interfaz
{
    public interface ICoreWebParameter
    {
        TbCompanyParameter GetParameter(string parameterName, int companyId);
    }
}
