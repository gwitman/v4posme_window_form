using DevExpress.Xpo;
using DevExpress.XtraEditors;
using System;
using System.Linq;
using System.Windows.Forms;
using Unity;
using v4posme_library.ModelsCode;
namespace v4posme_library.Domain.Services
{
    public class CoreWebAuthentication : ICoreWebAuthentication
    {
        private readonly IMembershipService _membershipService = VariablesGlobales.Instance.UnityContainer.Resolve<IMembershipService>();
        private readonly IRoleService _roleService = VariablesGlobales.Instance.UnityContainer.Resolve<IRoleService>();
        private readonly ICoreMenuService _coreMenu = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreMenuService>();
        private readonly ICoreWebPermission _coreWebPermission = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebPermission>();
        private readonly ICoreWebParameter _coreWebParameter = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebParameter>();
        
        public User Validar(User user, string password)
        {
            if (user == null) return null;
            if (string.IsNullOrEmpty(password)) return null;
            return user.Password == password ? user : null;
        }

        public User GetUserByNickname(string nickname)
        {
            if (string.IsNullOrEmpty(nickname))
            {
                return null;
            }
            try
            {
                XPQuery<User> users = Session.DefaultSession.Query<User>();
                return users.First(u=>u.Nickname==nickname);
            }
            catch (Exception)
            {
                return null;
            }
        }

        //validar que usuario este activo en este metodo
        public User GetUserByPasswordAndNickname(string nickname, string password)
        {
            if (string.IsNullOrEmpty(nickname)) { return null; }
            if (string.IsNullOrEmpty(password)) { return null; }
            var user = GetUserByNickname(nickname);
            if (user == null) { return null; }
            if (user.Password != password) return null;
            if (!user.IsActive)
            {
                //XtraMessageBox.Show("Usuario no está activo, ingresar con usario activo o comunicarse con el administrador para activar usuario.", "Usuario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            VariablesGlobales.Instance.Membership = _membershipService.GetRowByCompanyIdBranchIdUserId(user.CompanyID, user.BranchID, user.UserID);
            if (VariablesGlobales.Instance.Membership == null)
            {
                //XtraMessageBox.Show("El usuario no tiene asignado un rol, más información con el administrador de sistema.", "Usuario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            VariablesGlobales.Instance.Role = _roleService.GetRowByPk(user.CompanyID, user.BranchID, VariablesGlobales.Instance.Membership.RoleID);
            if (VariablesGlobales.Instance.Role == null)
            {
                //XtraMessageBox.Show("El usuario no tiene asignado un rol, más información con el administrador de sistema.", "Usuario");
                return null;
            }
            VariablesGlobales.Instance.ListMenuTop = _coreMenu.GetMenuTop(user.CompanyID, user.BranchID, VariablesGlobales.Instance.Role.RoleID);
            VariablesGlobales.Instance.ListMenuLeft = _coreMenu.GetMenuLeft(user.CompanyID, user.BranchID, VariablesGlobales.Instance.Role.RoleID);
            VariablesGlobales.Instance.ListMenuBodyReport = _coreMenu.GetMenuBodyReport(user.CompanyID, user.BranchID, VariablesGlobales.Instance.Role.RoleID);
            VariablesGlobales.Instance.ListMenuHiddenPopup = _coreMenu.GetMenuHiddenPopup(user.CompanyID, user.BranchID, VariablesGlobales.Instance.Role.RoleID);
            VariablesGlobales.Instance.MessageLogin = _coreWebPermission.GetLicenseMessage(user.CompanyID);
            var parameter = _coreWebParameter.GetParameter("CORE_LABEL_SISTEMA_SUPLANTATION", VariablesGlobales.Instance.Membership.CompanyID);
            VariablesGlobales.Instance.ParameterLabelSystem = parameter.Value;
            return user;
        }
    }
}
