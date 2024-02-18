using v4posme_library.ModelsCode;
namespace v4posme_library.Domain.Services.Interfaz
{
    public interface ICoreWebParameter
    {
        CompanyParameter GetParameter(string parameterName, int companyId);
    }
}
