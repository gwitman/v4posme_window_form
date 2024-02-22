using System.Diagnostics;
using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion
{
    public class CoreWebAuthentication : ICoreWebAuthentication
    {
        private Logger _logger = new Logger();

        private readonly IMembershipService _membershipService = VariablesGlobales.Instance.UnityContainer.Resolve<IMembershipService>();
        private readonly IRoleService _roleService = VariablesGlobales.Instance.UnityContainer.Resolve<IRoleService>();
        private readonly ICoreMenuService _coreMenu = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreMenuService>();
        private readonly ICoreWebPermission _coreWebPermission = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebPermission>();
        private readonly ICoreWebParameter _coreWebParameter = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebParameter>();

        public TbUser Validar(TbUser user, string password)
        {
            if (user == null!) return null!;
            if (string.IsNullOrEmpty(password)) return null!;
            return user.Password == password ? user : null!;
        }

        public TbUser? GetUserByNickname(string nickname)
        {
            if (string.IsNullOrEmpty(nickname))
            {
                return null;
            }
            try
            {
                using (var context = new DataContext())
                {
                    var user = from u in context.TbUsers where u.Nickname == nickname select u;
                    return user.First();
                }
            }
            catch (Exception exception)
            {
                _logger.Log(exception.Message);
                return null;
            }
        }

        //validar que usuario este activo en este metodo
        public TbUser? GetUserByPasswordAndNickname(string nickname, string password)
        {
            if (string.IsNullOrEmpty(nickname)) { return null!; }
            if (string.IsNullOrEmpty(password)) { return null!; }
            var user = GetUserByNickname(nickname);
            if (user == null) { return null!; }
            if (user.Password != password) return null!;
            Debug.Assert(user.IsActive != null, "user.IsActive != null");
            if (!user.IsActive.Value)
            {
                //XtraMessageBox.Show("Usuario no está activo, ingresar con usario activo o comunicarse con el administrador para activar usuario.", "Usuario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null!;
            }
            VariablesGlobales.Instance.Membership = _membershipService.GetRowByCompanyIdBranchIdUserId(user.CompanyId, user.BranchId, user.UserId);
            if (VariablesGlobales.Instance.Membership == null!)
            {
                //XtraMessageBox.Show("El usuario no tiene asignado un rol, más información con el administrador de sistema.", "Usuario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            VariablesGlobales.Instance.Role = _roleService.GetRowByPk(user.CompanyId, user.BranchId, VariablesGlobales.Instance.Membership.RoleId);
            if (VariablesGlobales.Instance.Role == null!)
            {
                //XtraMessageBox.Show("El usuario no tiene asignado un rol, más información con el administrador de sistema.", "Usuario");
                return null;
            }
            VariablesGlobales.Instance.ListMenuTop = _coreMenu.GetMenuTop(user.CompanyId, user.BranchId, VariablesGlobales.Instance.Role.RoleId);
            VariablesGlobales.Instance.ListMenuLeft = _coreMenu.GetMenuLeft(user.CompanyId, user.BranchId, VariablesGlobales.Instance.Role.RoleId);
            VariablesGlobales.Instance.ListMenuBodyReport = _coreMenu.GetMenuBodyReport(user.CompanyId, user.BranchId, VariablesGlobales.Instance.Role.RoleId);
            VariablesGlobales.Instance.ListMenuHiddenPopup = _coreMenu.GetMenuHiddenPopup(user.CompanyId, user.BranchId, VariablesGlobales.Instance.Role.RoleId);
            VariablesGlobales.Instance.MessageLogin = _coreWebPermission.GetLicenseMessage(user.CompanyId);
            var parameter = _coreWebParameter.GetParameter("CORE_LABEL_SISTEMA_SUPLANTATION", VariablesGlobales.Instance.Membership.CompanyId);
            VariablesGlobales.Instance.ParameterLabelSystem = parameter.Value;
            return user;
        }
    }
}
