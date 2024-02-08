using System.Collections.Generic;
using System.Linq;
using Unity;
using v4posme_window_form.Models.Tablas;
using v4posme_window_form.Properties;
namespace v4posme_window_form.Domain.Services
{
    public class CoreMenuService : ICoreMenu
    {
        private IElementSevice elementSevice = VariablesGlobales.Instance.UnityContainer.Resolve<IElementSevice>();
        private IUserPermissionService _userPermissionService = VariablesGlobales.Instance.UnityContainer.Resolve<IUserPermissionService>();
        private IMenuElementService _menuElementService = VariablesGlobales.Instance.UnityContainer.Resolve<IMenuElementService>();
        public List<MenuElement> getMenuTop(int companyId, int branchId, int roleId)
        {
            var role = VariablesGlobales.Instance.Role;
            var listMenuElement = new List<MenuElement>();
            if (role == null)
            {
                return null;
            }
            //Obtener la lista de elementos tipo pagina, que pertenescan al componente de seguridad
            var listElementSeguridad = elementSevice.getRowByTypeAndLayout(Settings.Default.ELEMENT_TYPE_PAGE, Settings.Default.MENU_TOP);
            if (listElementSeguridad == null) return null;
            //Obtener la lista del elementos de tipo pagina a la cual el usuario tiene permiso , segun el rol del usuario
            var listElementPermitido = _userPermissionService.getRowByCompanyIDyBranchIDyRoleID(companyId, branchId, roleId);
            //Obtener los id de los Elementos
            List<int> listElementIdSeguridad = new List<int>();
            List<int> listElementIdPermitied = new List<int>();
            foreach (var element in listElementSeguridad)
            {
                listElementIdSeguridad.Add(element.ElementID);
            }
            foreach (var userPermissionView in listElementPermitido)
            {
                listElementIdPermitied.Add(userPermissionView.ElementId);
            }
            listElementIdPermitied = listElementIdPermitied.Intersect(listElementIdSeguridad).ToList();
            if (role.IsAdmin && listElementIdSeguridad.Count() > 0)
            {
                listMenuElement = _menuElementService.GetRowByCompanyIdyElementId(companyId, listElementIdSeguridad);
            }else if (listElementIdPermitied.Count>0)
            {
                listMenuElement = _menuElementService.GetRowByCompanyIdyElementId(companyId, listElementIdPermitied);
            }
            return listMenuElement;
        }
    }
}
