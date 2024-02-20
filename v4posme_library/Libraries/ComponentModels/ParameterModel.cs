using v4posme_library.Models;
namespace v4posme_library.Libraries.ComponentModels;

class ParameterModel : IParameterModel
{
    public TbParameter GetRowByName(string name)
    {
        using var context = new DataContext();
        return context.TbParameters.Single(parameter=>parameter.Name != null && parameter.Name.Contains(name));
    }
    public List<TbParameter> GetAll()
    {
        using var context = new DataContext();
        return context.TbParameters.ToList();
    }
}
