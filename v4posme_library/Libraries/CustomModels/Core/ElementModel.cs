using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class ElementModel : IElementModel
{
    public List<TbElement> GetRowByComponentIdNotIn(List<int> componentId, int elementTypeId)
    {
        using var context = new DataContext();
        return context.TbElements.AsNoTracking()
            .Join(context.TbComponentElements.AsNoTracking(),
                element => element.ElementId,
                component => component.ElementId,
                (element, componentElement) => new { element, componentElement })
            .Where(arg => !componentId.Contains(arg.componentElement.ElementId)
                          && arg.element.ElementTypeId == elementTypeId)
            .Select(arg => new TbElement
            {
                ElementId = arg.element.ElementId,
                Name = arg.element.Name
            }).ToList();
    }

    public TbElement? GetRowByName(string name, int elementTypeId)
    {
        using var context = new DataContext();
        return context.TbElements.AsNoTracking()
            .FirstOrDefault(element => element.Name == name
                              && element.ElementTypeId == elementTypeId);
    }

    public List<TbElement>? GetRowByTypeAndLayout(int elementTypeId, int layoutId)
    {
        using var context = new DataContext();
        var result = from e in context.TbElements.AsNoTracking()
            join ce in context.TbComponentElements.AsNoTracking() on e.ElementId equals ce.ElementId
            join me in context.TbMenuElements.AsNoTracking() on e.ElementId equals me.ElementId
            where me.TypeMenuElementId == layoutId  
                  && e.ElementTypeId == elementTypeId
            select new TbElement
            {
                ElementId = e.ElementId,
                Name = e.Name
            };
        return result.ToList();
    }

    public List<TbElement> GetRowByPk(int componentId, int elementTypeId)
    {
        using var context = new DataContext();
        var result = from e in context.TbElements.AsNoTracking()
            join ce in context.TbComponentElements.AsNoTracking() on e.ElementId equals ce.ElementId
            where ce.ComponentId == componentId  
                  && e.ElementTypeId == elementTypeId
            select new TbElement
            {
                ElementId = e.ElementId,
                Name = e.Name
            };
        return result.ToList();
    }
}