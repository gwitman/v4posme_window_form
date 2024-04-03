using System.Diagnostics;
using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion
{
    public class CoreWebAuthentication : ICoreWebAuthentication
    {
        private readonly IMembershipModel _membershipModel =
            VariablesGlobales.Instance.UnityContainer.Resolve<IMembershipModel>();

        private readonly IRoleModel _roleModel = VariablesGlobales.Instance.UnityContainer.Resolve<IRoleModel>();

        private readonly ICoreWebMenu _coreWebMenu =
            VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebMenu>();

        private readonly ICoreWebPermission _coreWebPermission =
            VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebPermission>();

        private readonly ICoreWebAuthentication _coreWebAutentication =
            VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebAuthentication>();

        private readonly ICoreWebParameter _coreWebParameter =
            VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebParameter>();

        private readonly ICoreWebTools _coreWebToolser =
            VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();

        private readonly IUserModel _userModel = VariablesGlobales.Instance.UnityContainer.Resolve<IUserModel>();

        private readonly ICompanyModel _companyModel =
            VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyModel>();

        private readonly IBranchModel _branchModel = VariablesGlobales.Instance.UnityContainer.Resolve<IBranchModel>();


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
                using var context = new DataContext();
                var user = from u in context.TbUsers where u.Nickname == nickname select u;
                return user.FirstOrDefault();
            }
            catch (Exception exception)
            {
                _coreWebToolser.Log(exception.Message);
                return null;
            }
        }

        //validar que usuario este activo en este metodo
        public TbUser? GetUserByPasswordAndNickname(string nickname, string password)
        {
            if (string.IsNullOrEmpty(nickname))
            {
                return null!;
            }

            if (string.IsNullOrEmpty(password))
            {
                return null!;
            }

            var user = _userModel.GetRowByNiknamePassword(nickname, password);
            if (user is null)
            {
                return null!;
            }

            var objCompany = _companyModel.GetRowByPk(user.CompanyId);
            if (objCompany is null)
            {
                throw new Exception("LA EMPREA NO FUE ENCONTRADA ....");
            }

            var objBranch = _branchModel.GetRowByPk(user.CompanyId, user.BranchId);
            if (objBranch is null)
            {
                throw new Exception("LA SUCURSAL NO FUE ENCONTRADA ...");
            }

            VariablesGlobales.Instance.Membership =
                _membershipModel.GetRowByCompanyIdBranchIdUserId(user.CompanyId, user.BranchId, user.UserId);
            if (VariablesGlobales.Instance.Membership is null)
            {
                throw new Exception("EL USUARIO NO TIENE ASIGNADO UN ROL...");
            }

            var objRole = _roleModel.GetRowByPk(user.CompanyId, user.BranchId,
                VariablesGlobales.Instance.Membership.RoleId);
            if (objRole is null)
            {
                throw new Exception("EL USUARIO NO TIENE ASIGNADO UN ROL...");
            }

            VariablesGlobales.Instance.Company = objCompany;
            VariablesGlobales.Instance.Branch = objBranch;
            VariablesGlobales.Instance.Role = objRole;
            VariablesGlobales.Instance.User = user;
            VariablesGlobales.Instance.ListMenuTop = _coreWebMenu.GetMenuTop(user.CompanyId, user.BranchId,
                objRole.RoleId);
            VariablesGlobales.Instance.ListMenuLeft = _coreWebMenu.GetMenuLeft(user.CompanyId, user.BranchId,
                objRole.RoleId);
            VariablesGlobales.Instance.ListMenuBodyReport = _coreWebMenu.GetMenuBodyReport(user.CompanyId,
                user.BranchId, objRole.RoleId);
            VariablesGlobales.Instance.ListMenuHiddenPopup = _coreWebMenu.GetMenuHiddenPopup(user.CompanyId,
                user.BranchId, objRole.RoleId);
            VariablesGlobales.Instance.MessageLogin = _coreWebAutentication.GetLicenseMessage(user.CompanyId);
            var parameter = _coreWebParameter.GetParameter("CORE_LABEL_SISTEMA_SUPLANTATION",
                VariablesGlobales.Instance.Membership.CompanyId);
            VariablesGlobales.Instance.ParameterLabelSystem = parameter!.Value;

            return user;
        }

        public TbUser? GetUserByEmail(string email)
        {
            var user = _userModel.GetRowByEmail(email);
            if (user is null)
            {
                throw new Exception("EMAIL INCORRECTO...");
            }

            var objCompany = _companyModel.GetRowByPk(user.CompanyId);
            if (objCompany is null)
            {
                throw new Exception("LA EMPREA NO FUE ENCONTRADA ....");
            }

            var objBranch = _branchModel.GetRowByPk(user.CompanyId, user.BranchId);
            if (objBranch is null)
            {
                throw new Exception("LA SUCURSAL NO FUE ENCONTRADA ...");
            }

            var objMembership =
                _membershipModel.GetRowByCompanyIdBranchIdUserId(user.CompanyId, user.BranchId, user.UserId);
            if (objMembership is null)
            {
                throw new Exception("EL USUARIO NO TIENE ASIGNADO UN ROL...");
            }

            var objRole = _roleModel.GetRowByPk(user.CompanyId, user.BranchId, objMembership.RoleId);
            if (objRole is null)
            {
                throw new Exception("EL ROL DEL USUARIO NO FUE ENCONTRADO...");
            }

            VariablesGlobales.Instance.Company = objCompany;
            VariablesGlobales.Instance.Branch = objBranch;
            VariablesGlobales.Instance.Role = objRole;
            VariablesGlobales.Instance.User = user;
            VariablesGlobales.Instance.ListMenuTop = _coreWebMenu.GetMenuTop(user.CompanyId, user.BranchId,
                objRole.RoleId);
            VariablesGlobales.Instance.ListMenuLeft = _coreWebMenu.GetMenuLeft(user.CompanyId, user.BranchId,
                objRole.RoleId);
            VariablesGlobales.Instance.ListMenuBodyReport = _coreWebMenu.GetMenuBodyReport(user.CompanyId,
                user.BranchId, objRole.RoleId);
            VariablesGlobales.Instance.ListMenuHiddenPopup = _coreWebMenu.GetMenuHiddenPopup(user.CompanyId,
                user.BranchId, objRole.RoleId);

            return user;
        }

        public string? GetLicenseMessage(int companyId)
        {
            var getParameter1 = _coreWebParameter.GetParameter("CORE_CUST_PRICE_LICENCES_EXPIRED", companyId);
            var parameterFechaExpiration = getParameter1!.Value;

            var getParameter2 = _coreWebParameter.GetParameter("CORE_CUST_PRICE_LICENCES_EXPIRED", companyId);
            var objParameterExpiredLicense = getParameter2!.Value;

            var getParameter3 = _coreWebParameter.GetParameter("CORE_CUST_PRICE_TIPO_PLAN", companyId);
            var objParameterTipoPlan = getParameter3!.Value;

            var getParameter4 = _coreWebParameter.GetParameter("CORE_CUST_PRICE_TIPO_PLAN", companyId);
            var objParameterCreditosId = getParameter4!.ParameterId;
            var objParameterCreditos = getParameter4.Value;

            var getParameter5 = _coreWebParameter.GetParameter("CORE_CUST_PRICE_BY_INVOICE", companyId);
            var objParameterPriceByInvoice = getParameter5!.Value;

            var fechaNow = DateTime.Now.AddDays(7);
            if (fechaNow > DateTime.Parse(parameterFechaExpiration))
            {
                //XtraMessageBox.Show("LICENCIA EXPIRA EN 7 DIAS", "Licencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return "LICENCIA EXPIRA EN 7 DIAS";
            }

            //Validar Saldo				
            if (objParameterTipoPlan == "CONSUMIBLE")
            {
                if (int.Parse(objParameterCreditos) < (int.Parse(objParameterPriceByInvoice) * 30))
                {
                    return "CREDITOS PRONTO VENCERAN";
                }
            }

            return "";
        }
    }
}