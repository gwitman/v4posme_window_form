using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class SubElementModel : ISubElementModel
{
    public TbSubelement? GetRowByNameAndElementId(int elementId, string? name)
    {
        using var context = new DataContext();
        return context.TbSubelements.AsNoTracking()
            .FirstOrDefault(subelement => subelement!.ElementID == elementId
                                          && subelement.Name == name);
    }
}