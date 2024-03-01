using Microsoft.EntityFrameworkCore;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion
{
    public class MenuElementModelService : IMenuElementModelService
    {

        public List<TbMenuElement> GetRowByCompanyIdyElementId(int companyId, List<int> elementIdArray)
        {
            var listMenuElement = new List<TbMenuElement>();
            using var context = new DataContext();
            var query = $$"""
                            select x.*
                            from tb_menu_element x
                            inner join tb_element e on e.elementID = x.elementID
                            inner join tb_component_element ce on e.elementID = ce.elementID
                            inner join tb_company_component cco on ce.componentID = cco.componentID
                            where x.companyID={{companyId}} and x.isActive=true and cco.companyID={{companyId}}
                          """;
            if (elementIdArray.Count > 0)
            {
                query += " and x.elementID in ( -1";
                foreach (var i in elementIdArray)
                {
                    query += ", " + i;
                }
                query += " ) ";
            }
            query += " order by x.orden";
            return context.Database.SqlQueryRaw<TbMenuElement>(query).ToList();
        }
        public List<TbMenuElement> GetRowByCompanyId(int companyId)
        {
            using var context = new DataContext();
            return context.TbMenuElements
                .Join(context.TbElements, menu=>menu.ElementId, element=>element.ElementId, (menu, element)=>menu)
                .Where(menu=>menu.CompanyId == companyId && menu.IsActive == 1)
                .OrderBy(menu=>menu.Orden)
                .ToList();
        }
    }

}
