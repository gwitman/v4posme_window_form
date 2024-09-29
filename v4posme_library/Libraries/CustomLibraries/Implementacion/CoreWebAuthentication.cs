using System.Diagnostics;
using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion
{
    public class CoreWebAuthentication : ICoreWebAuthentication
    {
        private readonly IMembershipModel _membershipModel;
        private readonly IRoleModel _roleModel;
        private readonly ICoreWebMenu _coreWebMenu;
        private readonly ICoreWebParameter _coreWebParameter;
        private readonly ICoreWebTools _coreWebToolser;
        private readonly IUserModel _userModel;
        private readonly ICompanyModel _companyModel;
        private readonly IBranchModel _branchModel;
        private readonly DataContext _context;
        public CoreWebAuthentication(IMembershipModel membershipModel,
            IRoleModel roleModel,
            ICoreWebMenu coreWebMenu,
            ICoreWebParameter coreWebParameter,
            ICoreWebTools coreWebToolser,
            IUserModel userModel,
            ICompanyModel companyModel,
            IBranchModel branchModel,
            DataContext context)
        {
            _membershipModel = membershipModel;
            _roleModel = roleModel;
            _coreWebMenu = coreWebMenu;
            _coreWebParameter = coreWebParameter;
            _coreWebToolser = coreWebToolser;
            _userModel = userModel;
            _companyModel = companyModel;
            _branchModel = branchModel;
            _context = context;
        }

        public TbUser Validar(TbUser user, string? password)
        {
            if (user == null!) return null!;
            if (string.IsNullOrEmpty(password)) return null!;
            return user.Password == password ? user : null!;
        }

        public TbUser? GetUserByNickname(string? nickname)
        {
            if (string.IsNullOrEmpty(nickname))
            {
                return null;
            }

            try
            {
                var user = from u in _context.TbUsers where u.Nickname == nickname select u;
                return user.FirstOrDefault();
            }
            catch (Exception exception)
            {
                _coreWebToolser.Log(exception.Message);
                return null;
            }
        }

        //validar que usuario este activo en este metodo
        public TbUser? GetUserByPasswordAndNickname(string? nickname, string? password)
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

            var objCompany = _companyModel.GetRowByPk(user.CompanyID);
            if (objCompany is null)
            {
                throw new Exception("LA EMPRESA NO FUE ENCONTRADA ....");
            }

            var objBranch = _branchModel.GetRowByPk(user.CompanyID, user.BranchID);
            if (objBranch is null)
            {
                throw new Exception("LA SUCURSAL NO FUE ENCONTRADA ...");
            }

            VariablesGlobales.Instance.Membership = _membershipModel.GetRowByCompanyIdBranchIdUserId(user.CompanyID, user.BranchID, user.UserID);
            if (VariablesGlobales.Instance.Membership is null)
            {
                throw new Exception("EL USUARIO NO TIENE ASIGNADO UN ROL...");
            }

            var objRole = _roleModel.GetRowByPk(user.CompanyID, user.BranchID,
                VariablesGlobales.Instance.Membership.RoleID);
            if (objRole is null)
            {
                throw new Exception("EL USUARIO NO TIENE ASIGNADO UN ROL...");
            }

            Task.Run(() =>
            {
                VariablesGlobales.Instance.Company = objCompany;
                VariablesGlobales.Instance.Branch = objBranch;
                VariablesGlobales.Instance.Role = objRole;
                VariablesGlobales.Instance.User = user;
                VariablesGlobales.Instance.ListMenuTop = _coreWebMenu.GetMenuTop(user.CompanyID, user.BranchID, objRole.RoleID);
                VariablesGlobales.Instance.ListMenuLeft = _coreWebMenu.GetMenuLeft(user.CompanyID, user.BranchID, objRole.RoleID);
                VariablesGlobales.Instance.ListMenuBodyReport = _coreWebMenu.GetMenuBodyReport(user.CompanyID, user.BranchID, objRole.RoleID);
                VariablesGlobales.Instance.ListMenuHiddenPopup = _coreWebMenu.GetMenuHiddenPopup(user.CompanyID, user.BranchID, objRole.RoleID);
                VariablesGlobales.Instance.MessageLogin = GetLicenseMessage(user.CompanyID);
                var parameter = _coreWebParameter.GetParameter("CORE_LABEL_SISTEMA_SUPLANTATION", VariablesGlobales.Instance.Membership.CompanyID);
                VariablesGlobales.Instance.ParameterLabelSystem = parameter!.Value;
            });

            return user;
        }

        public TbUser? GetUserByEmail(string? email)
        {
            var user = _userModel.GetRowByEmail(email);
            if (user is null)
            {
                throw new Exception("EMAIL INCORRECTO...");
            }

            var objCompany = _companyModel.GetRowByPk(user.CompanyID);
            if (objCompany is null)
            {
                throw new Exception("LA EMPREA NO FUE ENCONTRADA ....");
            }

            var objBranch = _branchModel.GetRowByPk(user.CompanyID, user.BranchID);
            if (objBranch is null)
            {
                throw new Exception("LA SUCURSAL NO FUE ENCONTRADA ...");
            }

            var objMembership =
                _membershipModel.GetRowByCompanyIdBranchIdUserId(user.CompanyID, user.BranchID, user.UserID);
            if (objMembership is null)
            {
                throw new Exception("EL USUARIO NO TIENE ASIGNADO UN ROL...");
            }

            var objRole = _roleModel.GetRowByPk(user.CompanyID, user.BranchID, objMembership.RoleID);
            if (objRole is null)
            {
                throw new Exception("EL ROL DEL USUARIO NO FUE ENCONTRADO...");
            }

            VariablesGlobales.Instance.Company = objCompany;
            VariablesGlobales.Instance.Branch = objBranch;
            VariablesGlobales.Instance.Role = objRole;
            VariablesGlobales.Instance.User = user;
            VariablesGlobales.Instance.ListMenuTop = _coreWebMenu.GetMenuTop(user.CompanyID, user.BranchID,
                objRole.RoleID);
            VariablesGlobales.Instance.ListMenuLeft = _coreWebMenu.GetMenuLeft(user.CompanyID, user.BranchID,
                objRole.RoleID);
            VariablesGlobales.Instance.ListMenuBodyReport = _coreWebMenu.GetMenuBodyReport(user.CompanyID,
                user.BranchID, objRole.RoleID);
            VariablesGlobales.Instance.ListMenuHiddenPopup = _coreWebMenu.GetMenuHiddenPopup(user.CompanyID,
                user.BranchID, objRole.RoleID);

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
            var objParameterCreditosId = getParameter4!.ParameterID;
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