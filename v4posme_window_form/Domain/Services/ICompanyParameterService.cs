using v4posme_window_form.Models.Tablas;
namespace v4posme_window_form.Domain.Services
{
    public interface ICompanyParameterService
    {
        CompanyParameter GetRowByParameterIdCompanyId(int companyId,int parameterId);
    }
}
