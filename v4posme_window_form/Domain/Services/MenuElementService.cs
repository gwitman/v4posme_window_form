using DevExpress.Xpo;
using System.Collections.Generic;
using System.Linq;
using v4posme_window_form.Models.Tablas;
namespace v4posme_window_form.Domain.Services
{
    public class MenuElementService : IMenuElementService
    {

        public List<MenuElement> GetRowByCompanyIdyElementId(int companyId, List<int> elementIdArray)
        {
            List<MenuElement> result;
            var menuElement = Session.DefaultSession.Query<MenuElement>();
            var element = Session.DefaultSession.Query<Element>();
            var componentElement = Session.DefaultSession.Query<ComponentElement>();
            var companyComponent = Session.DefaultSession.Query<CompanyComponent>();
            var query = menuElement
                .Join(element, menu=>menu.ElementID, ele=>ele.ElementID, (menu, ele)=>new {menu, ele})
                .Join(componentElement, arg=>arg.ele.ElementID, comp=>comp.ElementID, (arg1, ce)=>new {arg1, ce})
                .Join(companyComponent, arg=>arg.ce.ComponentID, cc=>cc.ComponentID, (arg1, component)=>new {arg1, component})
                .Where(arg=>arg.arg1.arg1.menu.CompanyID == companyId && arg.arg1.arg1.menu.IsActive == 1 && arg.component.CompanyID == companyId);
            if (elementIdArray.Count > 0)
            {
                var queryable = query.Where(arg=>elementIdArray.Contains(arg.arg1.arg1.menu.ElementID));
                result = queryable
                    .Select((arg, component)=>arg.arg1.arg1.menu)
                    .ToList();
            }
            else
            {
                result = query
                    .Select((arg, component)=>arg.arg1.arg1.menu)
                    .ToList();
            }

            return result;
        }
    }
}
