using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using v4posme_window_form.Models.Tablas;

namespace v4posme_window_form.Domain.Services
{
    public class CoreMenuService : ICoreMenu
    {
        private IElementSevice elementSevice = VariablesGlobales.Instance.UnityContainer.Resolve<IElementSevice>();
        private IUserPermissionService _userPermissionService = VariablesGlobales.Instance.UnityContainer.Resolve<IUserPermissionService>();

        public MenuElement getMenuTop(int companyId, int branchId, int roleId)
        {
            var role = VariablesGlobales.Instance.Role;
            if (role == null)
            {
                return null;
            }
            //Obtener la lista de elementos tipo pagina, que pertenescan al componente de seguridad
            var listElementSeguridad = elementSevice.getRowByTypeAndLayout(Properties.Settings.Default.ELEMENT_TYPE_PAGE, Properties.Settings.Default.MENU_TOP);
            if (listElementSeguridad == null) return null;
            //Obtener la lista del elementos de tipo pagina a la cual el usuario tiene permiso , segun el rol del usuario
            var listElementPermitido = _userPermissionService.getRowByCompanyIDyBranchIDyRoleID(companyId, branchId, roleId);
            return null;
        }
    }
}
