using v4posme_library.Domain.Services.Interfaz;
using v4posme_library.Models;
namespace v4posme_library.Domain.Services.Implementacion
{
    public class ElementService : IElementSevice
    {
        public List<TbElement> getRowByTypeAndLayout(int elementTypeId, int layoutId)
        {
            using var context = new DataContext();
            return context.TbElements.Join(context.TbComponentElements, e=>e.ElementId, f=>f.ElementId, (ele, ce)=>ele)
                .Join(context.TbMenuElements, e=>e.ElementId, m=>m.ElementId, (e, m)=>new {e, m.TypeMenuElementId})
                .Where(f=>f.TypeMenuElementId == layoutId && f.e.ElementId == elementTypeId)
                .Select(x=>x.e)
                .ToList();
        }
    }
}
