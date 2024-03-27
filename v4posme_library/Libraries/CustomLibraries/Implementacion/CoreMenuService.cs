using Microsoft.Extensions.Configuration;
using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion
{
    public class CoreMenuService : ICoreMenuService
    {
        private readonly IConfigurationSection _section = VariablesGlobales.ConfigurationBuilder;

        private readonly IElementModel _elementService =
            VariablesGlobales.Instance.UnityContainer.Resolve<IElementModel>();

        private readonly IUserPermissionModel _userPermissionModel =
            VariablesGlobales.Instance.UnityContainer.Resolve<IUserPermissionModel>();

        private readonly IMenuElementModel _menuElementModelService =
            VariablesGlobales.Instance.UnityContainer.Resolve<IMenuElementModel>();

        public List<TbMenuElement>? GetMenuTop(int companyId, int branchId, int roleId)
        {
            return GetMenu(companyId, branchId, roleId, Convert.ToInt32(_section["MENU_TOP"]));
        }

        public List<TbMenuElement>? GetMenuLeft(int companyId, int branchId, int roleId)
        {
            return GetMenu(companyId, branchId, roleId, Convert.ToInt32(_section["MENU_LEFT"]));
        }

        public List<TbMenuElement>? GetMenuBodyReport(int companyId, int branchId, int roleId)
        {
            return GetMenu(companyId, branchId, roleId, Convert.ToInt32(_section["MENU_BODY"]));
        }

        public List<TbMenuElement>? GetMenuHiddenPopup(int companyId, int branchId, int roleId)
        {
            return GetMenu(companyId, branchId, roleId, Convert.ToInt32(_section["MENU_HIDDEN_POPUP"]));
        }

        private List<TbMenuElement>? GetMenu(int companyId, int branchId, int roleId, int menu)
        {
            var role = VariablesGlobales.Instance.Role;
            var listMenuElement = new List<TbMenuElement>();
            if (role == null!)
            {
                return null!;
            }

            //Obtener la lista de elementos tipo pagina, que pertenescan al componente de seguridad
            var listElementSeguridad =
                _elementService.GetRowByTypeAndLayout(Convert.ToInt16(_section["ELEMENT_TYPE_PAGE"]), menu);
            if (listElementSeguridad is null) return null;
            //Obtener la lista del elementos de tipo pagina a la cual el usuario tiene permiso , segun el rol del usuario
            var listElementPermitido =
                _userPermissionModel.GetRowByCompanyIdyBranchIdyRoleId(companyId, branchId, roleId);
            //Obtener los id de los Elementos
            var listElementIdSeguridad = listElementSeguridad.Select(element => element.ElementId).ToList();

            var listElementIdPermitied = listElementPermitido.Select(userPermissionView => userPermissionView.ElementId!.Value).ToList();

            listElementIdPermitied = listElementIdPermitied.Intersect(listElementIdSeguridad).ToList();
            if (role.IsAdmin!.Value && listElementIdSeguridad.Any())
            {
                listMenuElement =
                    _menuElementModelService.GetRowByCompanyIdyElementId(companyId, listElementIdSeguridad);
            }
            else if (listElementIdPermitied.Any())
            {
                listMenuElement =
                    _menuElementModelService.GetRowByCompanyIdyElementId(companyId, listElementIdPermitied);
            }

            return listMenuElement;
        }

        public List<string>? RenderMenuLeft(TbCompany? company, List<TbMenuElement>? menuElements)
        {
            return RenderItemLeft(company, menuElements, 0);
        }

        private List<string>? RenderItemLeft(TbCompany? company, List<TbMenuElement>? data, int parent)
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
            List<string>? list = new List<string>();
            foreach (var item in data)
            {
                if (item.ParentMenuElementId == parent)
                {
                    var x = RenderItemLeft(company, data, parent);
                    list.Add(item.Display!);
                    VariablesGlobales.Instance.SubMenu = x;
                }
            }

            return list;
        }
    }
}