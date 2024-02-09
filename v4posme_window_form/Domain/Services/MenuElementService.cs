using DevExpress.Xpo;
using System.Collections.Generic;
using System.Linq;
using v4posme_window_form.Models;
using v4posme_window_form.Models.Tablas;
namespace v4posme_window_form.Domain.Services
{
    public class MenuElementService : IMenuElementService
    {

        public List<MenuElement> GetRowByCompanyIdyElementId(int companyId, List<int> elementIdArray)
        {
            var listMenuElement = new List<MenuElement>();
            string sql = "select x.* " +
                         " from tb_menu_element x" +
                         " inner join  tb_element e on e.elementID = x.elementID" +
                         " inner join  tb_component_element ce on e.elementID = ce.elementID" +
                         " inner join  tb_company_component cco on ce.componentID = cco.componentID" +
                         " where x.companyID = " + companyId +
                         " and x.isActive = true" +
                         " and cco.companyID = " + companyId;
            if (elementIdArray.Count > 0)
            {
                sql += " and x.elementID in ( -1 ";
                foreach (var i in elementIdArray)
                {
                    sql += " , " + i;
                }
                sql += " ) ";
            }
            sql += " order by x.orden asc";
            var query = Session.DefaultSession.ExecuteQuery(sql);
            // Retrieve data from a query into a collection of objects.
            ICollection<MenuElementView> collection = Session.DefaultSession.GetObjectsFromQuery<MenuElementView>(sql);
            foreach (MenuElementView menuElementView in collection)
            {
                var menuElement = new MenuElement(Session.DefaultSession);
                menuElement.ElementID = menuElementView.ElementId;
                menuElement.MenuElementID = menuElementView.MenuElementId;
                menuElement.TypeMenuElementID = menuElementView.TypeMenuElementID;
                menuElement.Address = menuElementView.Address;
                menuElement.Display = menuElementView.Display;
                menuElement.Icon = menuElementView.Icon;
                menuElement.Orden = menuElementView.Orden;
                menuElement.Template = menuElementView.Template;
                menuElement.IsActive = (sbyte)menuElementView.IsActive;
                menuElement.CompanyID = menuElementView.CompanyId;
                menuElement.ParentMenuElementID = menuElementView.ParentMenuElementId;
                listMenuElement.Add(menuElement);
            }
            return listMenuElement;
        }
    }
}
