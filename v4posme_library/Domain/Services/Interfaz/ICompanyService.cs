using v4posme_library.ModelsCode;
namespace v4posme_library.Domain.Services.Interfaz
{
    public interface ICompanyService
    {
        Company GetRowByPk(int id);
    }
}
