using DevExpress.Xpo;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using v4posme_library.Domain.Services.Interfaz;
using v4posme_library.ModelsCode;

namespace v4posme_library.Domain.Services.Implementacion
{
    public class MenuElementModelService : IMenuElementModelService
    {

        public List<MenuElement> GetRowByCompanyIdyElementId(int companyId, List<int> elementIdArray)
        {
            var listMenuElement = new List<MenuElement>();
            var sql = new StringBuilder().Append("select x.companyID,x.elementID,x.menuElementID,x.parentMenuElementID, x.display,x.address,x.orden,x.icon,x.template,x.nivel")
                .Append(" from tb_menu_element x")
                .Append(" inner join  tb_element e on e.elementID = x.elementID")
                .Append(" inner join  tb_component_element ce on e.elementID = ce.elementID")
                .Append(" inner join  tb_company_component cco on ce.componentID = cco.componentID")
                .Append(" where x.companyID = ").Append(companyId)
                .Append(" and x.isActive = true")
                .Append(" and cco.companyID = ").Append(companyId);
            if (elementIdArray.Count > 0)
            {
                sql.Append(" and x.elementID in ( -1");
                foreach (var i in elementIdArray)
                {
                    sql.Append(", " + i);
                }
                sql.Append(" ) ");
            }
            sql.Append(" order by x.orden");
            // Retrieve data from a query into a collection of objects.
            var query = Session.DefaultSession.ExecuteQuery(sql.ToString());
            foreach (var row in query.ResultSet[0].Rows)
            {
                var menuElement = new MenuElement(Session.DefaultSession)
                {
                    //x.companyID,x.elementID,x.menuElementID,x.parentMenuElementID, x.display,x.address,x.orden,x.icon,x.template,x.nivel
                    CompanyID = (int)row.Values[0],
                    ElementID = (int)row.Values[1],
                    MenuElementID = (int)row.Values[2],
                    ParentMenuElementID = (int)row.Values[3],
                    Display = row.Values[4] as string,
                    Address = row.Values[5] as string,
                    Orden = row.Values[6] as string,
                    Icon = row.Values[7] as string,
                    Template = row.Values[8] as string,
                    Nivel = (int)row.Values[9]
                };
                listMenuElement.Add(menuElement);
            }
            return listMenuElement;
        }
        public List<MenuElement> GetRowByCompanyId(int companyId)
        {
            var queryMenuElement = Session.DefaultSession.Query<MenuElement>();
            var queryElement = Session.DefaultSession.Query<Element>();
            return queryMenuElement
                .Join(queryElement, menu=>menu.ElementID, element=>element.ElementID, (menu, element)=>menu)
                .Where(menu=>menu.CompanyID == companyId && menu.IsActive == 1)
                .OrderBy(menu=>menu.Orden)
                .ToList();
        }
    }

}
