using System.Collections.Generic;
using System.Linq;
using Unity;
using v4posme_library.ModelsCode;
using System.Configuration;
using System;
using v4posme_library.Properties;

namespace v4posme_library.Domain.Services
{
    public class CoreMenuService : ICoreMenuService
    {
        private readonly IElementSevice _elementService = VariablesGlobales.Instance.UnityContainer.Resolve<IElementSevice>();
        private readonly IUserPermissionService _userPermissionService = VariablesGlobales.Instance.UnityContainer.Resolve<IUserPermissionService>();
        private readonly IMenuElementService _menuElementService = VariablesGlobales.Instance.UnityContainer.Resolve<IMenuElementService>();
        public List<MenuElement> GetMenuTop(int companyId, int branchId, int roleId)
        {
            return GetMenu(companyId, branchId, roleId, Convert.ToInt32(Settings.Default.MENU_TOP));
        }
        public List<MenuElement> GetMenuLeft(int companyId, int branchId, int roleId)
        {
            return GetMenu(companyId, branchId, roleId, Convert.ToInt32(Settings.Default.MENU_LEFT));
        }
        public List<MenuElement> GetMenuBodyReport(int companyId, int branchId, int roleId)
        {
            return GetMenu(companyId, branchId, roleId, Convert.ToInt32(Settings.Default.MENU_BODY));
        }
        public List<MenuElement> GetMenuHiddenPopup(int companyId, int branchId, int roleId)
        {
            return GetMenu(companyId, branchId, roleId, Convert.ToInt32(Settings.Default.MENU_HIDDEN_POPUP));
        }

        private List<MenuElement> GetMenu(int companyId, int branchId, int roleId, int menu)
        {
            var role = VariablesGlobales.Instance.Role;
            var listMenuElement = new List<MenuElement>();
            if (role == null)
            {
                return null;
            }
            //Obtener la lista de elementos tipo pagina, que pertenescan al componente de seguridad
            var listElementSeguridad = _elementService.getRowByTypeAndLayout(Convert.ToInt16(Settings.Default.ELEMENT_TYPE_PAGE), menu);
            if (listElementSeguridad == null) return null;
            //Obtener la lista del elementos de tipo pagina a la cual el usuario tiene permiso , segun el rol del usuario
            var listElementPermitido = _userPermissionService.GetRowByCompanyIDyBranchIDyRoleId(companyId, branchId, roleId);
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
            if (role.IsAdmin && listElementIdSeguridad.Any())
            {
                listMenuElement = _menuElementService.GetRowByCompanyIdyElementId(companyId, listElementIdSeguridad);
            }
            else if (listElementIdPermitied.Any())
            {
                listMenuElement = _menuElementService.GetRowByCompanyIdyElementId(companyId, listElementIdPermitied);
            }
            return listMenuElement;
        }

        public List<string> RenderMenuLeft(Company company, List<MenuElement> menuElements)
        {
            return RenderItemLeft(company, menuElements, 0);
        }
        private List<string> RenderItemLeft(Company company, List<MenuElement> data, int parent)
        {
            /*
             * foreach ($data AS $obj){
                if ($obj->parentMenuElementID == $parent){
            $x = self::render_item_left($company,$data,$obj->menuElementID);
            $data_["icon"] = $obj->icon;
            $data_["address"] = base_url()."/".str_replace(URL_SUFFIX_OLD, URL_SUFFIX_NEW,$obj->address);
            $data_["display"] = getBehavio(strtoupper($company->type), "core_web_menu",$obj->display, "");
            $data_["submenu"] = $x;
            $template = view("core_template/".$obj->template,$data_);
            $html = $html. $template;
                }
            }
            */
            List<string> list = new List<string>();
            foreach (var item in data)
            {
                if (item.ParentMenuElementID == parent)
                {
                    var x = RenderItemLeft(company, data, parent);
                    list.Add(item.Display);
                    VariablesGlobales.Instance.SubMenu = x;
                }
            }
            return list;
        }
    }
}
