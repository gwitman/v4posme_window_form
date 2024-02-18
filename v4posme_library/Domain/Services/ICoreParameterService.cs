using v4posme_library.ModelsCode;
namespace v4posme_library.Domain.Services
{
    public interface ICoreParameterService
    {
        Parameter GetRowByName(string name);
    }
}
