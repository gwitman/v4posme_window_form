using v4posme_library.ModelsCode;
namespace v4posme_library.Domain.Services.Interfaz
{
    public interface ICompanyParameterService
    {
        CompanyParameter GetRowByParameterIdCompanyId(int companyId,int parameterId);
    }
}
