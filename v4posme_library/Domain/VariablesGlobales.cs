using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using Unity;
using v4posme_library.Domain.Services.Implementacion;
using v4posme_library.Domain.Services.Interfaz;
using v4posme_library.ModelsCode;
namespace v4posme_library.Domain
{
    public class VariablesGlobales
    {

        private readonly IUnityContainer _unityContainer;

        private VariablesGlobales()
        {
            _unityContainer = new UnityContainer();
            _unityContainer.RegisterType<ICoreWebAuthentication, CoreWebAuthentication>();
            _unityContainer.RegisterType<IBranchService, BranchService>();
            _unityContainer.RegisterType<ICompanyService, CompanyService>();
            _unityContainer.RegisterType<IMembershipService, MembershipService>();
            _unityContainer.RegisterType<IRoleService, RoleService>();
            _unityContainer.RegisterType<IElementSevice, ElementService>();
            _unityContainer.RegisterType<ICoreMenuService, CoreMenuService>();
            _unityContainer.RegisterType<IUserPermissionService, UserPermissionService>();
            _unityContainer.RegisterType<IMenuElementModelService, MenuElementModelService>();
            _unityContainer.RegisterType<ICoreParameterService, CoreParameterService>();
            _unityContainer.RegisterType<ICompanyParameterService, CompanyParamterService>();
            _unityContainer.RegisterType<ICoreWebParameter, CoreWebParameter>();
            _unityContainer.RegisterType<ICoreWebPermission, CoreWebPermission>();
        }

        public static VariablesGlobales Instance { get; } = new VariablesGlobales();

        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["posme.netdbkroqnguhldo1"].ConnectionString;
            }
        }

        public IUnityContainer UnityContainer
        {
            get
            {
                return _unityContainer;
            }
        }
        public User User { get; set; }

        public Company Company { get; set; }

        public Branch Branch { get; set; }

        public Membership Membership { get; set; }

        public Role Role { get; set; }
        public List<MenuElement> ListMenuTop { get; set; }
        public List<MenuElement> ListMenuLeft { get; set; }
        public List<MenuElement> ListMenuBodyReport { get; set; }
        public List<MenuElement> ListMenuHiddenPopup { get; set; }
        public string MessageLogin { get; set; }
        public string ParameterLabelSystem { get; set; }
        public List<string> SubMenu { get; set; }

        public void sendEmail()
        {
            var fromAddress = new MailAddress("from@gmail.com", "From Name");
            var toAddress = new MailAddress("to@example.com", "To Name");
            const string fromPassword = "fromPassword";
            const string subject = "Subject";
            const string body = "Body";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress))
            {
                message.Subject = subject;
                message.Body = body;
                smtp.Send(message);
            }
        }
    }
}
