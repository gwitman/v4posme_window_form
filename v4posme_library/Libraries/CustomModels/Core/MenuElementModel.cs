using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core
{
    public class MenuElementModel : IMenuElementModel
    {
        public List<TbMenuElement>? GetRowByCompanyIdyElementId(int companyId, List<int> elementIdArray)
        {
            var listMenuElement = new List<TbMenuElement>();
            using var context = new DataContext();
            var result = from x in context.TbMenuElements.AsNoTracking()
                join e in context.TbElements.AsNoTracking() on x.ElementId equals e.ElementId
                join ce in context.TbComponentElements.AsNoTracking() on e.ElementId equals ce.ElementId
                join cco in context.TbCompanyComponents.AsNoTracking() on ce.ComponentId equals cco.ComponentId
                where x.CompanyId == companyId &&
                      x.IsActive == 1 &&
                      cco.CompanyId == companyId
                select new TbMenuElement
                {
                    CompanyId = x.CompanyId,
                    ElementId = x.ElementId,
                    MenuElementId = x.MenuElementId,
                    ParentMenuElementId = x.ParentMenuElementId,
                    Display = x.Display,
                    Address = x.Address,
                    Orden = x.Orden,
                    Icon = x.Icon,
                    Template = x.Template,
                    Nivel = x.Nivel,
                    IsActive = x.IsActive,
                    IconWindowForm = x.IconWindowForm,
                    FormRedirectWindowForm = x.FormRedirectWindowForm,
                    TypeMenuElementId = x.TypeMenuElementId
                };

            if (elementIdArray.Count > 0)
            {
                result = result.Where(element => elementIdArray.Contains(element.ElementId));
            }

            result = result.OrderBy(element => element.Orden);
            return result.ToList();
        }

        public List<TbMenuElement> GetRowByCompanyId(int companyId)
        {
            using var context = new DataContext();
            return context.TbMenuElements.AsNoTracking()
                .Join(context.TbElements.AsNoTracking(), 
                    menu => menu.ElementId, 
                    element => element.ElementId, 
                    (menu, element) => menu)
                .Where(menu => menu.CompanyId == companyId 
                               && menu.IsActive == 1)
                .OrderBy(menu => menu.Orden)
                .ToList();
        }
    }
}