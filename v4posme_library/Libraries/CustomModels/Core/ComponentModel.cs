using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class ComponentModel : IComponentModel
{
    public TbComponent? GetRowByName(string name)
    {
        using var context = new DataContext();
        return context.TbComponents.FirstOrDefault(component => component.Name == name);
    }
}