using v4posme_library.Domain.Services.Interfaz;
using v4posme_library.Models;
namespace v4posme_library.Domain.Services.Implementacion
{
    public class CoreParameterService : ICoreParameterService
    {

        public TbParameter GetRowByName(string name)
        {
            using var context = new DataContext();
            return context.TbParameters.First(parameter=>parameter.Name!.Contains(name));
        }
    }
}
