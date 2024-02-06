using DevExpress.Xpo;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using v4posme_window_form.Models.Tablas;

namespace v4posme_window_form.Domain.Services
{
    public class CoreWebAuthentication : ICoreWebAuthentication
    {
        private IMembershipService membershipService;
        private IRoleService roleService;
        private ICoreMenu coreMenu;
        public CoreWebAuthentication()
        {
            membershipService = VariablesGlobales.Instance.UnityContainer.Resolve<IMembershipService>();
            roleService = VariablesGlobales.Instance.UnityContainer.Resolve<IRoleService>();
            coreMenu = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreMenu>();
        }
        public User validar(User user, string password)
        {
            if (user == null) return null;
            if (string.IsNullOrEmpty(password)) return null;
            if (user.Password == password) return user;
            return null;
        }

        public User getUserByNickname(string nickname)
        {
            if (string.IsNullOrEmpty(nickname))
            {
                return null;
            }
            try
            {
                XPQuery<User> users = Session.DefaultSession.Query<User>();
                var usuario = (from u in users where u.Nickname == nickname select u).First();
                return usuario;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //validar que usuario este activo en este metodo
        public User getUserByPasswordAndNickname(string nickname, string password)
        {
            if (string.IsNullOrEmpty(nickname)) { return null; }
            if (string.IsNullOrEmpty(password)) { return null; }
            var user = getUserByNickname(nickname);
            if (user == null) { return null; }
            if (user.Password == password)
            {
                if (user.IsActive)
                {
                    VariablesGlobales.Instance.Membership = membershipService.getRowByCompanyIDBranchIDUserID(user.CompanyID, user.BranchID, user.UserID);
                    if (VariablesGlobales.Instance.Membership == null)
                    {
                        XtraMessageBox.Show("El usuario no tiene asignado un rol, más información con el administrador de sistema.", "Usuario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                    VariablesGlobales.Instance.Role = roleService.getRowByPK(user.CompanyID, user.BranchID, VariablesGlobales.Instance.Membership.RoleID);
                    if (VariablesGlobales.Instance.Role == null)
                    {
                        XtraMessageBox.Show("El usuario no tiene asignado un rol, más información con el administrador de sistema.", "Usuario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                    coreMenu.getMenuTop();
                    return user;
                }
                else
                {
                    XtraMessageBox.Show("Usuario no está activo, ingresar con usario activo o comunicarse con el administrador para activar usuario.", "Usuario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }
            }
            else { return null; }
        }
    }
}
