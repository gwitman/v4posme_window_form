using v4posme_window_form.Models.Tablas;
namespace v4posme_window_form.Domain.Services
{
    public interface ICoreWebParameter
    {
        CompanyParameter GetParameter(string parameterName, int companyId);
    }
}
