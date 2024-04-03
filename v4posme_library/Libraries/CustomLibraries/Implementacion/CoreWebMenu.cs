using Microsoft.Extensions.Configuration;
using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion
{
    public class CoreWebMenu : ICoreWebMenu
    {
        private readonly IConfigurationSection _section = VariablesGlobales.ConfigurationBuilder;

        private readonly IRoleModel _roleModel = VariablesGlobales.Instance.UnityContainer.Resolve<IRoleModel>();

        private readonly IElementModel _elementModel =
            VariablesGlobales.Instance.UnityContainer.Resolve<IElementModel>();

        private readonly IUserPermissionModel _userPermissionModel =
            VariablesGlobales.Instance.UnityContainer.Resolve<IUserPermissionModel>();

        private readonly IMenuElementModel _menuElementModel =
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
            var elementTypeId = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["ELEMENT_TYPE_PAGE"]);
            // Obtener el rol del usuario
            var objRole = _roleModel.GetRowByPk(companyId, branchId, roleId);
            if (objRole is null)
                throw new Exception("NO EXISTE EL ROL DEL USUARIO");

            // Obtener la lista de elementos tipo pagina, que pertenescan al componente de seguridad
            var listElementSeguridad = _elementModel.GetRowByTypeAndLayout(elementTypeId, menu);
            if (listElementSeguridad is null)
                return null;

            // Obtener la lista del elementos de tipo pagina a la cual el usuario tiene permiso, segun el rol del usuario
            var listElementPermitido =
                _userPermissionModel.GetRowByCompanyIdyBranchIdyRoleId(companyId, branchId, roleId);

            // Obtener los id de los Elementos
            var listElementIdSeguridad = listElementSeguridad.Select(i => i.ElementId).ToList();
            List<int> listElementIdPermitied = [];
            if (listElementPermitido.Count > 0)
            {
                listElementIdPermitied = listElementPermitido
                    .Select(i => i.ElementId!.Value)
                    .Intersect(listElementIdSeguridad)
                    .ToList();
            }

            // Obtener la lista de menu_element del componente de seguridad...
            List<TbMenuElement>? listMenuElement;
            if (objRole.IsAdmin!.Value && listElementIdSeguridad.Count > 0)
            {
                listMenuElement = _menuElementModel.GetRowByCompanyIdyElementId(companyId, listElementIdSeguridad);
            }
            else if (listElementIdPermitied.Count > 0)
            {
                listMenuElement = _menuElementModel.GetRowByCompanyIdyElementId(companyId, listElementIdPermitied);
            }
            else
            {
                return null;
            }

            return listMenuElement;
        }

    }
}