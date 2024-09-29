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
                element => element.ElementID,
                component => component.ElementID,
                (element, componentElement) => new { element, componentElement })
            .Where(arg => !componentId.Contains(arg.componentElement.ElementID)
                          && arg.element.ElementTypeID == elementTypeId)
            .Select(arg => new TbElement
            {
                ElementID = arg.element.ElementID,
                Name = arg.element.Name
            }).ToList();
    }

    public TbElement? GetRowByName(string? name, int elementTypeId)
    {
        using var context = new DataContext();
        return context.TbElements.AsNoTracking()
            .FirstOrDefault(element => element.Name == name
                              && element.ElementTypeID == elementTypeId);
    }

    public List<TbElement>? GetRowByTypeAndLayout(int elementTypeId, int layoutId)
    {
        using var context = new DataContext();
        var result = from e in context.TbElements.AsNoTracking()
            join ce in context.TbComponentElements.AsNoTracking() on e.ElementID equals ce.ElementID
            join me in context.TbMenuElements.AsNoTracking() on e.ElementID equals me.ElementID
            where me.TypeMenuElementID == layoutId  
                  && e.ElementTypeID == elementTypeId
            select new TbElement
            {
                ElementID = e.ElementID,
                Name = e.Name
            };
        return result.ToList();
    }

    public List<TbElement> GetRowByPk(int componentId, int elementTypeId)
    {
        using var context = new DataContext();
        var result = from e in context.TbElements.AsNoTracking()
            join ce in context.TbComponentElements.AsNoTracking() on e.ElementID equals ce.ElementID
            where ce.ComponentID == componentId  
                  && e.ElementTypeID == elementTypeId
            select new TbElement
            {
                ElementID = e.ElementID,
                Name = e.Name
            };
        return result.ToList();
    }
}