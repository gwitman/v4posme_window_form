using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class ComponentModel(DataContext context) : IComponentModel
{
    public TbComponent? GetRowByName(string? name)
    {     
        return context.TbComponents.FirstOrDefault(component => component.Name == name);
    }
}