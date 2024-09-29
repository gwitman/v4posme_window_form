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
                join e in context.TbElements.AsNoTracking() on x.ElementID equals e.ElementID
                join ce in context.TbComponentElements.AsNoTracking() on e.ElementID equals ce.ElementID
                join cco in context.TbCompanyComponents.AsNoTracking() on ce.ComponentID equals cco.ComponentID
                where x.CompanyID == companyId &&
                      x.IsActive == 1 &&
                      cco.CompanyID == companyId
                select new TbMenuElement
                {
                    CompanyID = x.CompanyID,
                    ElementID = x.ElementID,
                    MenuElementID = x.MenuElementID,
                    ParentMenuElementID = x.ParentMenuElementID,
                    Display = x.Display,
                    Address = x.Address,
                    Orden = x.Orden,
                    Icon = x.Icon,
                    Template = x.Template,
                    Nivel = x.Nivel,
                    IsActive = x.IsActive,
                    IconWindowForm = x.IconWindowForm,
                    FormRedirectWindowForm = x.FormRedirectWindowForm,
                    TypeMenuElementID = x.TypeMenuElementID
                };

            if (elementIdArray.Count > 0)
            {
                result = result.Where(element => elementIdArray.Contains(element.ElementID));
            }

            result = result.OrderBy(element => element.Orden);
            return result.ToList();
        }

        public List<TbMenuElement> GetRowByCompanyId(int companyId)
        {
            using var context = new DataContext();
            return context.TbMenuElements.AsNoTracking()
                .Join(context.TbElements.AsNoTracking(), 
                    menu => menu.ElementID, 
                    element => element.ElementID, 
                    (menu, element) => menu)
                .Where(menu => menu.CompanyID == companyId 
                               && menu.IsActive == 1)
                .OrderBy(menu => menu.Orden)
                .ToList();
        }
    }
}