using v4posme_library.Models;
namespace v4posme_library.Domain.Services.Interfaz
{
    public interface ICompanyParameterService
    {
        TbCompanyParameter GetRowByParameterIdCompanyId(int companyId,int parameterId);
    }
}
