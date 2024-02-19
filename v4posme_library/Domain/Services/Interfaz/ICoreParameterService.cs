using v4posme_library.Models;
namespace v4posme_library.Domain.Services.Interfaz
{
    public interface ICoreParameterService
    {
        TbParameter GetRowByName(string name);
    }
}
