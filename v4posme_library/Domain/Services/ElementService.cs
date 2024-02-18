using System.Collections.Generic;
using DevExpress.Xpo;
using System.Linq;
using System.Windows.Forms;
using v4posme_library.ModelsCode;

namespace v4posme_library.Domain.Services
{
    public class ElementService : IElementSevice
    {
        public List<Element> getRowByTypeAndLayout(int elementTypeId, int layoutId)
        {
            if (elementTypeId == 0) return null;
            if (layoutId == 0) return null;
            XPQuery<Element> element = new XPQuery<Element>(Session.DefaultSession);
            XPQuery<ComponentElement> componentElement = Session.DefaultSession.Query<ComponentElement>();
            XPQuery<MenuElement> menuElement = Session.DefaultSession.Query<MenuElement>();
            return element.Join(componentElement, e => e.ElementID,f=>f.ElementID, (ele, ce) => ele)
                .Join(menuElement, e=>e.ElementID,m=>m.ElementID, (e, m) =>new {e, m.TypeMenuElementID})
                .Where(f=>f.TypeMenuElementID==layoutId && f.e.ElementID == elementTypeId)
                .Select(x=>x.e)
                .ToList();
        }
    }
}